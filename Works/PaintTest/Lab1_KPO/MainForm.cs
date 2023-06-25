using PluginInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_KPO
{
    public partial class MainForm : Form
    {
        public static Color CurrentColor { get; set; }
        public static int CurWidth { get; set; }

        public static string CurTool { get; set; }

        public static Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        public MainForm()
        {
            InitializeComponent();
            CurrentColor = Color.Black;
            CurWidth = 3;
            toolStripTextBox1.Text = "3";
            TurnOffSaveButtons();
            CurTool = "кисть";
            GetExtList();
            CreatePluginMenu();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private int dCounter = 1;

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Canvas d = new Canvas();
            d.Text = $"Документ {dCounter++}";
            d.MdiParent = this;
            TurnOffSaveButtons();
            d.Show();
        }

        private void вертикальноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void каскадноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new AboutForm();
            f.ShowDialog();
        }

        

        public void SetStatusText(string text)
        {
            toolStripStatusLabel1.Text = text;
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Blue;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Green;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                CurrentColor = cd.Color;
            }
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void рисунокToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSize cs = new CanvasSize();
            cs.WidthTextBox.Text = ((Canvas)ActiveMdiChild).CanvasWidth.ToString();
            cs.HeightTextBox.Text = ((Canvas)ActiveMdiChild).CanvasHeight.ToString();
            if(cs.ShowDialog(this) == DialogResult.OK)
            {
                int w = int.Parse(cs.WidthTextBox.Text);
                int h = int.Parse(cs.HeightTextBox.Text);

                ((Canvas)ActiveMdiChild).CanvasWidth = w;
                ((Canvas)ActiveMdiChild).CanvasHeight = h;
            }


        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(toolStripTextBox1.Text == "")
            {
                CurWidth = 3;
                toolStripTextBox1.Text = "3";
            }
            try
            {
                CurWidth = int.Parse(toolStripTextBox1.Text);
            }
            catch
            {
                MessageBox.Show("Значение должно быть числом.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ((Canvas)ActiveMdiChild).SaveAs();
            }
            catch
            {
                MessageBox.Show("Нет активного документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                OpenFileDialog dlg = new OpenFileDialog(); 
                dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg|Все файлы ()*.*|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Canvas frmChild = new Canvas(dlg.FileName);
                    frmChild.MdiParent = this;
                    frmChild.Show();

                }
            
            dlg.Dispose();
            TurnOffSaveButtons();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Canvas)ActiveMdiChild).Save();
        }

       

        public void TurnOffSaveButtons()
        {
            if (ActiveMdiChild == null)
            {
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьКакToolStripMenuItem.Enabled = false;
                
            }
            else
            {
                сохранитьToolStripMenuItem.Enabled = true;
                сохранитьКакToolStripMenuItem.Enabled = true;
                
            }

        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TurnOffSaveButtons();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurTool = "кисть";
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            CurTool = "эллипс";
        }

        private void rectButton_Click(object sender, EventArgs e)
        {
            CurTool = "прямоугольник";
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            CurTool = "ластик";
        }

        private void scalePlusButton_Click(object sender, EventArgs e)
        {
            try
            {
                ((Canvas)ActiveMdiChild).ScalePlus();
            }
            catch
            {
                MessageBox.Show("Ошибка! Нет активного документа");
            }
        }

        private void scaleMinusButton_Click(object sender, EventArgs e)
        {
            try
            {
                ((Canvas)ActiveMdiChild).ScaleMinus();
            }
            catch
            {
                MessageBox.Show("Ошибка! Нет активного документа");
            }
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            CurTool = "линия";
        }

        private void StarButton_Click(object sender, EventArgs e)
        {
            CurTool = "звезда";
        }

        private void GetExtList()
        {
            try
            {
                if (ConfigurationManager.AppSettings.Get("mode") == "Auto") { FindPlugins(); return; }
                if (ConfigurationManager.AppSettings.Count == 1) { FindPlugins(); return; }
                List<string> Names = new List<string>();
                foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                {
                    if (key == "mode") continue;
                    Names.Add(ConfigurationManager.AppSettings.Get(key));
                }
                FindPlugins(Names);
            }
            catch
            {
                FindPlugins();
            }
        }


        void FindPlugins()
        {
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");
                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }

        void FindPlugins(List<string> Names)
        {
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;
            foreach (string filename in Names)
            {
                string[] files = Directory.GetFiles(folder, filename);

                foreach (string file in files)
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(file);
                        foreach (Type type in assembly.GetTypes())
                        {
                            Type iface = type.GetInterface("PluginInterface.IPlugin");
                            if (iface != null)
                            {
                                IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                                plugins.Add(plugin.Name, plugin);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки плагина {filename}\n" + ex.Message);
                    }
            }
        }

        private void CreatePluginMenu()
        {
            foreach (IPlugin plugin in plugins.Values)
            {
                var item = new ToolStripMenuItem(plugin.Name);
                item.Click += OnPluginClick;
                фильтрыToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            ((Canvas)ActiveMdiChild).PictureTransorm(sender, args);
        }

       /* private void всеПлагиныToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string pluginsList = "";
            int i = 1;
            foreach(IPlugin plugin in plugins.Values)
            {
                pluginsList += $"{i++}) Название: {plugin.Name}  Автор: {plugin.Author}  Версия: {plugin.Vers.Major}.{plugin.Vers.Minor}\n";
            }
            MessageBox.Show(pluginsList, "Список расширений");
            
        }*/
    }
}
