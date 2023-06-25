using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_KPO
{
    public partial class CanvasSize : Form
    {


        public CanvasSize()
        {
            InitializeComponent();
        }

        private void WidthTextBox_TextChanged(object sender, EventArgs e)
        {
            int w;
            int h;
            
            if(int.TryParse(WidthTextBox.Text, out w) && w>0 && int.TryParse(HeightTextBox.Text, out h) && h>0)
            {
                OkButton.Enabled = true;
            }
            else
            {
                OkButton.Enabled = false;
            }
        }
    }
}
