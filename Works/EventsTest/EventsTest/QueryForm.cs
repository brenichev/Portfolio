using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EventsTest
{
    public partial class QueryForm : Form
    {
        private string sql;
        private bool click = false;
        public QueryForm()
        {
            InitializeComponent();
            FillComboFields();
            FillTables();
            FillFields();
            FillList();
        }

        private void FillTables()
        {
            DataTable fields = new DataTable();
            fields.Columns.Add("Value", typeof(string));
            fields.Columns.Add("Display", typeof(string));

            fields.Rows.Add("Events", "Мепроприятия");
            fields.Rows.Add("Stages", "Этапы мероприятий");
            fields.Rows.Add("Managers", "Организаторы");
            fields.Rows.Add("Members", "Участники");
            fields.Rows.Add("ManagersList", "Список организаторов");
            fields.Rows.Add("ParticipationList", "Список участников");
            TablesComboBox.DataSource = fields;
            TablesComboBox.DisplayMember = "Display";
            TablesComboBox.ValueMember = "Value";
        }
        private void FillComboFields()
        {
            DataTable fields = new DataTable();
            fields.Columns.Add("Value", typeof(string));
            fields.Columns.Add("Display", typeof(string));
            switch (FieldsComboBox.SelectedValue)
            {
                case "Events":
                    fields.Rows.Add("EventName", "Название мероприятия");
                    fields.Rows.Add("EventTypes.EventType", "Тип мероприятия");
                    fields.Rows.Add("Ages.Age", "Возрастное ограничение");
                    fields.Rows.Add("EventForms.EventForm", "Форма проведения");
                    fields.Rows.Add("EventTypes.EventType", "Тип мероприятия");
                    break;
            }

            FieldsComboBox.DataSource = fields;
            FieldsComboBox.DisplayMember = "Display";
            FieldsComboBox.ValueMember = "Value";

            OperationComboBox.Items.AddRange(new string[] { "=", "!=", ">", "<" });
            OperationComboBox.SelectedIndex = 0;

            radioButton1.Checked = true;
        }

        private void FillFields()
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"SELECT COLUMN_NAME AS [ColumnsName] FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{TablesComboBox.SelectedValue}'";
                    SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);

                    //FieldsComboBox.ValueMember = "ColumnsName";
                    FieldsComboBox.DataSource = data;
                    FieldsComboBox.DisplayMember = "ColumnsName";
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string s = "";
            if (listBox1.Items.Count != 0)
                if (radioButton1.Checked)
                    s = radioButton1.Text + " ";
                else
                    s = radioButton2.Text + " ";
            SqlCommand sqlComm = new SqlCommand("@text");
            sqlComm.Parameters.AddWithValue("@text", textBox1.Text);
            s += FieldsComboBox.Text + " " + OperationComboBox.SelectedItem + " ";
            if (int.TryParse(textBox1.Text, out int a))
            s += textBox1.Text;
            else
                s += "'" + textBox1.Text + "'";
            listBox1.Items.Add(s);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }


        private void TablesComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillFields();
            FillList();
        }

        private void FillList()
        {
            listBox2.DataSource = null;
            listBox3.Items.Clear();
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"SELECT COLUMN_NAME AS [ColumnsName] FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{TablesComboBox.SelectedValue}'";
                    SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);

                    //FieldsComboBox.ValueMember = "ColumnsName";
                    listBox2.DataSource = data;
                    listBox2.DisplayMember = "ColumnsName";
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            /*foreach(object temp in listBox3.Items)
            {
                if (!listBox2.Items.Contains(temp.ToString()))
                {
                    DataRow dr = ((DataRowView)temp).Row;
                    ((DataTable)listBox2.DataSource).Rows.Remove(dr);
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox3.Items.Add(listBox2.Text);
            DataRow dr = ((DataRowView)listBox2.SelectedItem).Row;
            ((DataTable)listBox2.DataSource).Rows.Remove(dr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((DataTable)listBox2.DataSource).Rows.Add(listBox3.SelectedItem);
            listBox3.Items.Remove(listBox3.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillSql();
            richTextBox1.Text = sql;
        }

        private void FillSql()
        {
            sql = "SELECT ";
            foreach (object item in listBox3.Items)
            {
                sql += item.ToString() + ", ";
            }
            int x1 = sql.Length - 2;
            sql = sql.Remove(x1);
            sql += " FROM " + TablesComboBox.SelectedValue + " WHERE ";
            foreach (object item in listBox1.Items)
            {
                sql += item.ToString() + " ";
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            ViewForm View = new ViewForm();
            this.Close();
            View.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           /* SaveFileDialog sfd1 = new SaveFileDialog();
            sfd1.Filter = "Книга Excel|*.xlsx";
            if (sfd1.ShowDialog() == DialogResult.OK)
            */
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                //Книга.
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                //Таблица
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    ExcelApp.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;

                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                    }
                }

                ExcelApp.Columns.EntireColumn.AutoFit();
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
            
        }

        private void SqlRun()
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    if(click != true && richTextBox1.Text != "")
                        sql = richTextBox1.Text;

                        SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                        DataTable data = new DataTable();
                        SDA.Fill(data);

                        //FieldsComboBox.ValueMember = "ColumnsName";
                        dataGridView1.DataSource = data;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные. Проверьте правильность запроса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SqlRun();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            click = true;
            FillSql();
            SqlRun();
            click = false;
        }
    }
}
