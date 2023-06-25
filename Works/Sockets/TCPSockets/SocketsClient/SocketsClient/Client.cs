using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Sockets
{
    public partial class frmMain : Form
    {
        private TcpClient Client = new TcpClient();     // клиентский сокет
        private IPAddress IP;                           // IP-адрес клиента
        private bool _continue = true;

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();

            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());    // информация об IP-адресах и имени машины, на которой запущено приложение
            IP = hostEntry.AddressList[0];                                  // IP-адрес, который будет указан в заголовке окна для идентификации клиента
            int Port = 1011;
            // определяем IP-адрес машины в формате IPv4
            foreach (IPAddress address in hostEntry.AddressList)
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP = address;
                    break;
                }

            // вывод IP-адреса машины и номера порта в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + IP.ToString() + "  :  " + Port.ToString();

            new Thread(ReadMessages).Start();
        }

        private void ReadMessages()
        {
            while (_continue)
            {
                if (Client.Client.ReceiveBufferSize > 0 && Client.Client.Connected)
                {
                    try
                    {
                        byte[] buffer = new byte[1024];                           // буфер прочитанных из сокета байтов
                        Client.Client.Receive(buffer);                     // получаем последовательность байтов из сокета в буфер buff
                        string msg = System.Text.Encoding.Unicode.GetString(buffer);     // выполняем преобразование байтов в последовательность символов
                        messageText.Invoke((MethodInvoker)delegate
                        {
                            if (msg.Replace("\0", "") != "")
                            {
                                if(messageText.TextLength > 0)
                                {
                                    messageText.Text += "\n >> " + msg;             // выводим полученное сообщение на форму
                                }
                                else
                                {
                                    messageText.Text += msg.TrimStart('\n', '\0').Replace("\0", "");             // выводим полученное сообщение на форму
                                }
                            }
                        });
                    }
                    catch(Exception ex)
                    {
                        Client.Close();
                    }
                }
            }
        }
        // подключение к серверному сокету
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginBox.Text))
                {
                    int Port = 1010;                                // номер порта, через который выполняется обмен сообщениями
                    IPAddress IP = IPAddress.Parse(tbIP.Text);      // разбор IP-адреса сервера, указанного в поле tbIP
                    Client.Connect(IP, Port);                       // подключение к серверному сокету
                    btnConnect.Enabled = false;
                    btnSend.Enabled = true;
                    loginBox.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Введите логин");
                }
            }
            catch
            {
                MessageBox.Show("Введен неправильный IP-адрес");
            }
        }

        // отправка сообщения
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] buff = Encoding.Unicode.GetBytes(loginBox.Text + " >> " + tbMessage.Text);   // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт
            Stream stm = Client.GetStream();                                                    // получаем файловый поток клиентского сокета
            stm.Write(buff, 0, buff.Length);                                                // выполняем запись последовательности байт

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;
            Client.Close();         // закрытие клиентского сокета
        }
    }
}