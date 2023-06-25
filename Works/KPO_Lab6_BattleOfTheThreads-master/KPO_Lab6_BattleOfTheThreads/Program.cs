using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvoke;
using System.Threading;
using System.Diagnostics;
using System.Windows;

namespace KPO_Lab6_BattleOfTheThreads
{

    class Program
    {
        #region PInvoke

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool ReadConsoleOutputCharacter(
        IntPtr hConsoleOutput,
        [Out] StringBuilder lpCharacter,
        uint nLength,
        COORD dwReadCoord,
        out uint lpNumberOfCharsRead
        );


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        const int STD_OUTPUT_HANDLE = -11;



        #endregion

        static IntPtr cnsl; //Дескриптор консоли
        static Mutex screenlock = new Mutex(false); 
        static ManualResetEvent startEvt = new ManualResetEvent(false); //Начало игры
        static Semaphore bulletsem3 = new Semaphore(3, 3);
        //static Semaphore bulletsem4 = new Semaphore(4, 4);
        //static Semaphore bulletsem5 = new Semaphore(5, 5);
        static Semaphore currSem = bulletsem3;
        


        static bool gotHit = false;
        static long hit = 0;
        static long miss = 0;
        static char[] badchar =  "-\\|/".ToCharArray();
        static Random rnd = new Random();
        static object obj = new object();
        static short playerX;
        static short playerY;



        static void WriteAt(short x, short y, string str, ConsoleColor clr)
        {
            screenlock.WaitOne();
            Console.ForegroundColor = clr;
            Console.SetCursorPosition(x, y);
            //long pos = 65536 * y + x;
            Console.Write(str);
            screenlock.ReleaseMutex();
        }

        static char GetAt(short x, short y)
        {
            StringBuilder res = new StringBuilder(1);
            COORD org = new COORD() { X = x, Y = y };
            screenlock.WaitOne();
            ReadConsoleOutputCharacter(cnsl, res, 1, org, out uint tmp);
            screenlock.ReleaseMutex();
            return res[0];
        }

        static void badguys()
        {
            startEvt.WaitOne(20000);
            while (true)
            {
                if(rnd.Next(0,101) <(hit+miss)/25 + 20 )
                {
                    Thread nextEn = new Thread(badguy);
                    nextEn.IsBackground = true;
                    nextEn.Start();
                }
                Thread.Sleep(1500);
            }
        }

        static void PlayerBullet(object obj)
        {
            if (obj.GetType() != typeof(COORD)) return;
            COORD xy = (COORD)obj;

            short x = xy.X;
            short y = xy.Y;

            if (GetAt(x, y) == '*') return;
            if (!currSem.WaitOne(0)) return;
            while(y-- != 1)
            {
                WriteAt(x, y, "*", ConsoleColor.Blue);
                Thread.Sleep(110);
                WriteAt(x, y, " ", ConsoleColor.Black);
                WriteAt((short)(x - 1),(short)(y - 1), " ", ConsoleColor.Black);
            }
            currSem.Release();

        }



        static void badguy()
        {
            short y = (short) rnd.Next(3, 14);
            int dir;
            short x;
            if (y % 2 > 0) { x = 1; }
            else x = (short)(Console.BufferWidth - 1);
            dir = (x == 1) ? 1 : -1;
            bool hitme = false;
            while ((dir == 1 && x != Console.BufferWidth - 1 ) || (dir == -1 && x != 1))
            {
                WriteAt(x, y, badchar[x % 4].ToString(), ConsoleColor.DarkRed);
                for (int i = 0; i < 15; i++)
                {
                    Thread.Sleep(40);
                    if (GetAt(x, y) == '*')
                    {
                        hitme = true;
                        break;
                    }
                }
                WriteAt(x, y, " ", ConsoleColor.Black);
                if (hitme)
                {
                    // в противника попали!
                    Console.Beep();
                    Interlocked.Increment(ref hit);
                   
                    Score();
                    return;
                }
                x += (short) dir;
            }
            Interlocked.Increment(ref miss);
            Score();
            return;
        }

        static void Score()
        {
            Console.Title = $"Война потоков - Попаданий:{hit}, Промахов:{miss}";
            if (miss > 0)
            {
                lock (obj)
                {
                    MessageBox.Show("Nice try!", "Game Over", MessageBoxButton.OK);
                    Console.SetCursorPosition(1, 1);
                    Environment.Exit(0);
                }
            }
        }


        static void Main(string[] args)
        {
            cnsl = GetStdHandle(STD_OUTPUT_HANDLE);
            Console.Title = $"Война потоков - Попаданий:{hit}, Промахов:{miss}";
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);


             playerX = (short) (Console.BufferWidth / 2);
             playerY = (short) (Console.BufferHeight - 1);

            Thread enems = new Thread(badguys);
            enems.IsBackground = true;
            //startEvt.Set();
            enems.Start();

            while (true)
            {
                ConsoleKeyInfo c;
                WriteAt(playerX, playerY, "^", ConsoleColor.Blue);
                c = Console.ReadKey(false);
                
               
                switch (c.Key)
                {
                    case (ConsoleKey.Spacebar):
                        {
                            Thread shot = new Thread(PlayerBullet);
                            shot.IsBackground = true;
                            shot.Start(new COORD()
                            {
                                X = playerX,
                                Y = playerY
                            });
                            Thread.Sleep(100);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            startEvt.Set();
                            WriteAt(playerX, playerY, " ", ConsoleColor.Black);
                            if (playerX > 4) playerX--;
                            
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            startEvt.Set();
                            WriteAt(playerX, playerY, " ", ConsoleColor.Black);
                            if (playerX < Console.BufferWidth - 3) playerX++;
                            
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }


                //COORD xy = new COORD()
                //{
                //    X = playerX,
                //    Y = playerY
                //};

            }
            
            
        }

        

        
    }
}
