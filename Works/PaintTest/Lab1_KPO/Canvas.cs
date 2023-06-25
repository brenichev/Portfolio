using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_KPO
{
    public partial class Canvas : Form
    {
        //private static bool leftButtonDown;
        public Bitmap bmp;
        private string DocName = "";

         int X;
         int Y;
         int W;
         int H;


        int unstX;
        int unstY;
         int unstW;
         int unstH;



        public int CanvasWidth
        {
            get
            {
                return pictureBox1.Width;
            }
            set
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox1.Width = value;
                Bitmap tbmp = new Bitmap(value, pictureBox1.Height);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public int CanvasHeight
        {
            get
            {
                return pictureBox1.Height;
            }
            set
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                pictureBox1.Height = value;
                Bitmap tbmp = new Bitmap(pictureBox1.Width, value);
                Graphics g = Graphics.FromImage(tbmp);
                g.Clear(Color.White);
                g.DrawImage(bmp, new Point(0, 0));
                bmp = tbmp;
                pictureBox1.Image = bmp;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public Canvas()
        {
            InitializeComponent();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
        }

        public Canvas(string fileName)
        {
            InitializeComponent();

            bmp = GetBmp(fileName);
            DocName = fileName;
            
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            pictureBox1.Image = bmp;
            this.Activate();
        }
        



        private void DocumentForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MainForm.CurTool == "кисть")
            {
                //leftButtonDown = true;
                X = StretchCoords(e.X);
                Y = StretchCoords(e.Y);
                unstX = 0;
                unstY = 0;
            }
        }

        private void DocumentForm_MouseMove(object sender, MouseEventArgs e)
        {
            
             switch (MainForm.CurTool)
            {
                case "кисть":
                    {
                        BrushDraw(sender, e);
                        break;
                    }

                case "линия":
                    {
                        GetCoords(sender, e);
                        break;
                    }

                case "эллипс":
                    {
                        GetCoords(sender, e);
                        break;
                    }

                case "прямоугольник":
                    {
                        GetCoords(sender, e);
                        break;
                    }

                case "звезда":
                    {
                        GetCoords(sender, e);
                        break;
                    }

                case "ластик":
                    {
                        Color temp = MainForm.CurrentColor;
                        MainForm.CurrentColor = Color.White;
                        BrushDraw(sender, e);
                        MainForm.CurrentColor = temp;

                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            

           MainForm parent = (MainForm) ParentForm;
            parent.SetStatusText($"X: {e.X} Y: {e.Y}");
            pictureBox1.Invalidate();
        }

        private int StretchCoords (int a)
        {
            return (int)(((double)pictureBox1.Image.Width) / ((double)pictureBox1.Width) * (double)a);
        }

        private int UnStretchCoords(int a)
        {
            return (int)( ((double)pictureBox1.Width) / ((double)pictureBox1.Image.Width) * (double)a);
        }

        private void GetCoords(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (X == 0 || Y == 0)
                {
                     X = StretchCoords(e.X);
                     Y = StretchCoords(e.Y);
                    unstX = e.X;
                    unstY = e.Y;
                }
                else
                {
                    W = StretchCoords(e.X) - X;
                    H = StretchCoords(e.Y) - Y;
                    unstW = e.X - unstX;
                    unstH = e.Y - unstY;

                }
            }
        }

        private void BrushDraw(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Graphics g = Graphics.FromImage(bmp);

                g.DrawLine(new Pen(MainForm.CurrentColor, MainForm.CurWidth), X, Y, StretchCoords(e.X), StretchCoords(e.Y));
                X = StretchCoords(e.X);
                Y = StretchCoords(e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void DocumentForm_MouseLeave(object sender, EventArgs e)
        {
            MainForm parent = (MainForm)ParentForm;
            parent.SetStatusText(string.Empty);
        }

        public void SaveAs()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.Filter = "Windows Bitmap (*.bmp)|*.bmp|Файлы JPEG (*.jpeg," +
                    "*.jpg) |*.jpeg;*.jpg|Все файлы ()*.*|*.*";
                ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(sfd.FileName, ff[sfd.FilterIndex - 1]);
                    DocName = sfd.FileName;
                }
            }
            catch
            {
                MessageBox.Show("Сохранение файла под данным именем недоступно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Save()
        {
            if (DocName == "") SaveAs();
            else
            {
                bmp.Save(DocName); 
            }
        }

        private Bitmap GetBmp(string fileName)
        {
            Bitmap bmp;
            using (var fs = File.OpenRead(fileName))
                bmp = new Bitmap(fs);

            return bmp;
        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            string temp = MainForm.CurTool;
            MainForm.CurTool = "кисть";
            DialogResult dr = MessageBox.Show($"Сохранить изменения?\n{DocName}", "Сохранение", MessageBoxButtons.YesNoCancel);
            
            if (dr == DialogResult.Cancel)
            {
                MainForm.CurTool = temp;
                e.Cancel = true;
                
                return;
            }
            else if (dr == DialogResult.Yes)
            {
                Save();
            }
            
        }

        private void Canvas_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm parent = (MainForm)ParentForm;
            parent.TurnOffSaveButtons();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (MainForm.CurTool)
                {
                    case "эллипс":
                        {
                            DrawEllipse();
                            break;
                        }
                    case "прямоугольник":
                        {
                            DrawRect();
                            break;
                        }
                    case "линия":
                        {
                            DrawLine();
                            break;
                        }
                    case "звезда":
                        {
                            DrawStar();
                            break;
                        }
                }
                unstX = 0;
                unstY = 0;
                unstW = 0;
                unstH = 0;
                //leftButtonDown = false;
            }
        }

        private PointF[] GetStarLines()
        {
            double cX = X + W / 2;
            double cY = Y + H / 2;
            int n = 5;
            double alpha = -Math.PI / 2;
            double R = Math.Sqrt(W * W + H * H) / 2;
            double r = R / 2;

            PointF[] points = new PointF[2 * n + 1];
            double da = Math.PI / n;
            double currRad;

            for (int k = 0; k < 2 * n + 1; k++)
            {
                if (k % 2 == 0) currRad = R;
                else currRad = r;
                points[k] = new PointF((float)(cX + currRad * Math.Cos(alpha)), (float)(cY + currRad * Math.Sin(alpha)));
                alpha += da;
            }
            return points;
        }

        private PointF[] GetUnstrStarLines()
        {
            double cX = unstX + unstW / 2;
            double cY = unstY + unstH / 2;
            int n = 5;
            double alpha = -Math.PI / 2;
            double R = Math.Sqrt(unstW * unstW + unstH * unstH) / 2;
            double r = R / 2;

            PointF[] points = new PointF[2 * n + 1];
            double da = Math.PI / n;
            double currRad;

            for (int k = 0; k < 2 * n + 1; k++)
            {
                if (k % 2 == 0) currRad = R;
                else currRad = r;
                points[k] = new PointF((float)(cX + currRad * Math.Cos(alpha)), (float)(cY + currRad * Math.Sin(alpha)));
                alpha += da;
            }
            return points;
        }

        private void StarPreview(object sender, PaintEventArgs e, Pen pen)
        {
            PointF[] points = GetUnstrStarLines();
            e.Graphics.DrawLines(pen, points);

        }

        private void DrawStar()
        {

            Graphics g = Graphics.FromImage(bmp);
            PointF[] points = GetStarLines();

            g.DrawLines(new Pen(MainForm.CurrentColor,MainForm.CurWidth), points);
            pictureBox1.Invalidate();

            X = 0;
            Y = 0;
            W = 0;
            H = 0;

        }

        private void DrawLine()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.DrawLine(new Pen(MainForm.CurrentColor, MainForm.CurWidth), X, Y, X+W, Y+H);
            pictureBox1.Invalidate();
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        private void DrawEllipse()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.DrawEllipse(new Pen(MainForm.CurrentColor, MainForm.CurWidth), X, Y, W, H);
            pictureBox1.Invalidate();
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        private void DrawRect()
        {
            Graphics g = Graphics.FromImage(bmp);
            if(W<0)
            {
                X = X + W;
                W = Math.Abs(W);
            }
            if(H<0)
            {
                Y = Y + H;
                H = Math.Abs(H);
            }
            g.DrawRectangle(new Pen(MainForm.CurrentColor, MainForm.CurWidth), X, Y, W, H);
            pictureBox1.Invalidate();
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }

        public void ScalePlus()
        { 
            pictureBox1.Width *=  2;
            pictureBox1.Height *= 2;
            
        }

        public void ScaleMinus()
        {
            pictureBox1.Width /= 2;
            pictureBox1.Height /= 2;
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            Pen pen = new Pen(MainForm.CurrentColor, MainForm.CurWidth);

            if(unstX !=0 && unstY !=0 && unstW !=0 && unstH !=0)
            switch (MainForm.CurTool) 
            {
                case "эллипс":
                    {
                        e.Graphics.DrawEllipse(pen, unstX,unstY,unstW , unstH);
                        break;
                    }
                case "прямоугольник":
                    {
                            int x = unstX;
                            int y = unstY;
                            int w = unstW;
                            int h = unstH;
                            if (unstW < 0)
                            {
                                x = unstX + unstW;
                                w = Math.Abs(unstW);
                            }
                            if (unstH < 0)
                            {
                                y = unstY + unstH;
                                h = Math.Abs(unstH);
                            }
                            e.Graphics.DrawRectangle(new Pen(MainForm.CurrentColor, MainForm.CurWidth), x, y, w, h);
                        break;
                    }
                case "линия":
                    {
                        e.Graphics.DrawLine(pen,unstX, unstY,unstX+unstW,unstY+unstH);
                        break;
                    }
                case "звезда":
                    {
                        StarPreview(sender, e, pen);
                        break;
                    }

            }
            
        }

        public void PictureTransorm(object sender, EventArgs args)
        {
            IPlugin plugin =MainForm.plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform((Bitmap)pictureBox1.Image);
            pictureBox1.Refresh();
        }
    }
}
