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

namespace Pipes
{
    public partial class frmMain : Form
    {
        private Int32 PipeHandle;                                                       // дескриптор канала
        private string PipeName = "\\\\" + Dns.GetHostName() + "\\pipe\\ServerPipe";    // имя канала, Dns.GetHostName() - метод, возвращающий имя машины, на которой запущено приложение
        private Thread t;                                                               // поток для обслуживания канала
        private bool _continue = true;                                                  // флаг, указывающий продолжается ли работа с каналом
        private List<string> connectedUsers = new List<string>();
        private List<string> connectedUsers2 = new List<string>();

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();

            // создание именованного канала
            PipeHandle = DIS.Import.CreateNamedPipe("\\\\.\\pipe\\ServerPipe", DIS.Types.PIPE_ACCESS_DUPLEX, DIS.Types.PIPE_TYPE_BYTE | DIS.Types.PIPE_WAIT, DIS.Types.PIPE_UNLIMITED_INSTANCES, 0, 1024, DIS.Types.NMPWAIT_WAIT_FOREVER, (uint)0);

            // вывод имени канала в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + PipeName;
            
            // создание потока, отвечающего за работу с каналом
            t = new Thread(ReceiveMessage);
            t.Start();
        }
        private void UpdateList()
        {
            usersList.Invoke((MethodInvoker)delegate
            {
                usersList.Text = string.Join("\n", connectedUsers.ToArray());
            });
        }    
        private void ReceiveMessage()
        {
            string msg = "";            // прочитанное сообщение
            uint realBytesReaded = 0;   // количество реально прочитанных из канала байтов

            // входим в бесконечный цикл работы с каналом
            while (_continue)
            {
                if (DIS.Import.ConnectNamedPipe(PipeHandle, 0))
                {
                    byte[] buff = new byte[1024];                                           // буфер прочитанных из канала байтов
                    DIS.Import.FlushFileBuffers(PipeHandle);                                // "принудительная" запись данных, расположенные в буфере операционной системы, в файл именованного канала
                    DIS.Import.ReadFile(PipeHandle, buff, 1024, ref realBytesReaded, 0);    // считываем последовательность байтов из канала в буфер buff
                    msg = Encoding.Unicode.GetString(buff);                                 // выполняем преобразование байтов в последовательность символов
                    string username = msg.Split(new string[] { ">> " }, StringSplitOptions.None)[0];
                    rtbMessages.Invoke((MethodInvoker)delegate
                    {
                        if(!connectedUsers.Contains(username))
                        {
                            connectedUsers.Add(username);
                            usersList.Text = "";
                            usersList.Text = string.Join("\n", connectedUsers.ToArray());
                            //rtbMessages.Invoke((MethodInvoker)delegate { usersList.Text += "\n" + username; });
                        }
                        if (msg != "")
                            rtbMessages.Text += "\n >> " + msg;                             // выводим полученное сообщение на форму
                    });

                    DIS.Import.DisconnectNamedPipe(PipeHandle);                             // отключаемся от канала клиента 
                        foreach (string user in connectedUsers.ToList())
                        {
                            string name = "\\\\.\\pipe\\" + user;
                            uint BytesWritten = 0;                              // количество реально записанных в канал байт
                            byte[] buff2 = Encoding.Unicode.GetBytes(msg);   // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт

                            // открываем именованный канал, имя которого указано в поле tbPipe
                            int pipeSender = DIS.Import.CreateFile(name,
                                DIS.Types.EFileAccess.GenericWrite,
                                DIS.Types.EFileShare.Read,
                                0,
                                DIS.Types.ECreationDisposition.OpenExisting,
                                0,
                                0);
                            if (pipeSender == -1)
                        {
                            connectedUsers.Remove(user);
                            UpdateList();
                        }
                                
                            DIS.Import.WriteFile(pipeSender, buff2, Convert.ToUInt32(buff2.Length), ref BytesWritten, 0);         // выполняем запись последовательности байт в канал
                            DIS.Import.CloseHandle(pipeSender);                 // закрываем дескриптор канала
                        }                    
                    Thread.Sleep(500);                                                      // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Выключить сервер?", "Отключение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                PipeHandle = DIS.Import.CreateNamedPipe("\\\\.\\pipe\\ServerPipe", DIS.Types.PIPE_ACCESS_DUPLEX, DIS.Types.PIPE_TYPE_BYTE | DIS.Types.PIPE_WAIT, DIS.Types.PIPE_UNLIMITED_INSTANCES, 0, 1024, DIS.Types.NMPWAIT_WAIT_FOREVER, (uint)0);
                _continue = false;      // сообщаем, что работа с каналом завершена

                if (t != null)
                    t.Abort();          // завершаем поток

                if (PipeHandle != -1)
                {
                    DIS.Import.CloseHandle(PipeHandle);     // закрываем дескриптор канала
                    Application.ExitThread();
                    Environment.Exit(0);
                }
            }
        }
    }
}