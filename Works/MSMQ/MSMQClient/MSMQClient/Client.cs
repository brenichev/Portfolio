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
using System.Messaging;
using System.Threading;

namespace MSMQ
{
    public partial class frmMain : Form
    {
        private MessageQueue q = null;      // очередь сообщений, в которую будет производиться запись сообщений
        private bool _continue = true;
        private MessageQueue input = null;

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (MessageQueue.Exists(tbPath.Text))
            {
                // если очередь, путь к которой указан в поле tbPath существует, то открываем ее
                q = new MessageQueue(tbPath.Text);
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
                tbLogin.ReadOnly = true;

                string path = Dns.GetHostName() + "\\private$\\";    // путь к очереди сообщений, Dns.GetHostName() - метод, возвращающий имя текущей машины

                // если очередь сообщений с указанным путем существует, то открываем ее, иначе создаем новую
                if (MessageQueue.Exists(path + tbLogin.Text))
                    input = new MessageQueue(path + tbLogin.Text);
                else
                    input = MessageQueue.Create(path + tbLogin.Text);

                // задаем форматтер сообщений в очереди
                input.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

                // вывод пути к очереди сообщений в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
                this.Text += "     " + input.Path;

                // создание потока, отвечающего за работу с очередью сообщений
                Thread t = new Thread(ReceiveMessage);
                t.Start();
            }
            else
                MessageBox.Show("Указан неверный путь к очереди, либо очередь не существует");
        }

        // получение сообщения
        private void ReceiveMessage()
        {
            if (input == null)
                return;

            System.Messaging.Message msg = null;

            // входим в бесконечный цикл работы с очередью сообщений
            while (_continue)
            {
                if (input.Peek() != null)   // если в очереди есть сообщение, выполняем его чтение, интервал до следующей попытки чтения равен 10 секундам
                    msg = input.Receive(TimeSpan.FromSeconds(10.0));

                rtbMessages.Invoke((MethodInvoker)delegate
                {
                    string message = msg.Body.ToString().Split(new string[] { " >> " }, StringSplitOptions.None)[1];
                    if (message != null && message != "")
                    {
                        rtbMessages.Text += "\n >> " + msg.Label + " : " + msg.Body; // выводим полученное сообщение на форму                       
                    }

                });
                Thread.Sleep(500);          // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // выполняем отправку сообщения в очередь
            if (tbMessage.Text == "")
                MessageBox.Show("Пустое сообщение");
            else
                q.Send(tbLogin.Text + " >> " + tbMessage.Text, Dns.GetHostName());
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;      // сообщаем, что работа с очередью сообщений завершена

            if (input != null)
            {
                MessageQueue.Delete(input.Path);      // в случае необходимости удаляем очередь сообщений
            }

            Application.ExitThread();
            Environment.Exit(0);
        }

        private void rtbMessages_TextChanged(object sender, EventArgs e)
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }
    }
}