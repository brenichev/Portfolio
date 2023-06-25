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
        private Socket udpSocket;
        private IPEndPoint remotePoint;
        private bool _continue = true;

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();


            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);

            btnSend.Enabled = false;
            /*byte[] buff = Encoding.Unicode.GetBytes("newConnection");
            int bytes = udpSocket.SendTo(buff, remotePoint);
            
            new Thread(ReadMessages).Start();*/
        }

        private void ReadMessages()
        {
            while (_continue)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    udpSocket.Receive(buffer);
                    var message = Encoding.UTF8.GetString(buffer);
                    messageText.Invoke((MethodInvoker)delegate
                    {
                        if (message.Replace("\0", "") != "")
                        {
                            if (message.StartsWith("\n"))
                            {
                                messageText.Text += message;             // выводим полученное сообщение на форму
                            }
                            else
                            {
                                messageText.Text += "\n >> " + message;             // выводим полученное сообщение на форму
                            }
                        }
                    });
                }
                catch(Exception ex) { }
            }
        }

        // отправка сообщения
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loginBox.Text))
            {
                if (!string.IsNullOrEmpty(tbMessage.Text))
                {
                    loginBox.Enabled = false;
                    byte[] buff = Encoding.UTF8.GetBytes(loginBox.Text + " >> " + tbMessage.Text);   // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт
                    int bytes = udpSocket.SendTo(buff, remotePoint);
                }
                else
                {
                    MessageBox.Show("Вы не ввели сообщение");
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели логин");
            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;
            udpSocket.Close();         // закрытие клиентского сокета
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            byte[] buff = Encoding.Unicode.GetBytes(loginBox.Text + " >> " + "newConnection");
            int bytes = udpSocket.SendTo(buff, remotePoint);
            btnSend.Enabled = true;
            btnConnect.Enabled = false;

            new Thread(ReadMessages).Start();
        }
    }
}