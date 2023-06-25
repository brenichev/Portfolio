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
using System.Threading;
using System.Messaging;
using System.Security.Cryptography;
using System.IO;

namespace MSMQ
{
    public partial class frmMain : Form
    {
        private MessageQueue q = null;          // очередь сообщений
        private Thread t = null;                // поток, отвечающий за работу с очередью сообщений
        private bool _continue = true;          // флаг, указывающий продолжается ли работа с мэйлслотом
        private List<string> connectedUsers = new List<string>();

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();
            string path = Dns.GetHostName() + "\\private$\\ServerQueue";    // путь к очереди сообщений, Dns.GetHostName() - метод, возвращающий имя текущей машины

            // если очередь сообщений с указанным путем существует, то открываем ее, иначе создаем новую
            if (MessageQueue.Exists(path))
                q = new MessageQueue(path);
            else
                q = MessageQueue.Create(path);

            // задаем форматтер сообщений в очереди
            q.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

            // вывод пути к очереди сообщений в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + q.Path;

            // создание потока, отвечающего за работу с очередью сообщений
            Thread t = new Thread(ReceiveMessage);
            t.Start();
        }

        // получение сообщения
        private void ReceiveMessage()
        {
            if (q == null)
                return;

            System.Messaging.Message msg = null;
            MessageQueue output;


            // входим в бесконечный цикл работы с очередью сообщений
            while (_continue)
            {
                if (q.Peek() != null)   // если в очереди есть сообщение, выполняем его чтение, интервал до следующей попытки чтения равен 10 секундам
                    msg = q.Receive(TimeSpan.FromSeconds(10.0));

                rtbMessages.Invoke((MethodInvoker)delegate
                {
                    string message = msg.Body.ToString().Split(new string[] { " >> " }, StringSplitOptions.None)[1];
                    if (message != null && message != "")
                    {
                        rtbMessages.Text += "\n >> " + msg.Label + " : " + msg.Body; // выводим полученное сообщение на форму
                        string username = msg.Body.ToString().Split(new string[] { " >> " }, StringSplitOptions.None)[0];
                        if (!connectedUsers.Contains(username))
                        {
                            connectedUsers.Add(username);
                            usersList.Text = "";
                            usersList.Text = string.Join("\n", connectedUsers.ToArray());
                            //rtbMessages.Invoke((MethodInvoker)delegate { usersList.Text += "\n" + username; });
                        }
                    }

                });

                foreach (string user in connectedUsers.ToList())
                {
                    string name = ".\\private$\\" + user;

                    if (MessageQueue.Exists(name))
                    {
                        output = new MessageQueue(name);
                        output.Send(msg);
                    }
                    else
                    {
                        connectedUsers.Remove(user);
                        UpdateList();
                    }  
                }
                Thread.Sleep(500);          // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
            }
        }

        private void UpdateList()
        {
            usersList.Invoke((MethodInvoker)delegate
            {
                usersList.Text = string.Join("\n", connectedUsers.ToArray());
            });
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;      // сообщаем, что работа с очередью сообщений завершена

            if (t != null)
            {
                t.Abort();          // завершаем поток
            }

            if (q != null)
            {
                MessageQueue.Delete(q.Path);      // в случае необходимости удаляем очередь сообщений
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