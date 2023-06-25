using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsTest
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewForm f2 = new ViewForm();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditForm1 f3 = new EditForm1();
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            QueryForm f4 = new QueryForm();
            f4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    SqlDataAdapter SDA = new SqlDataAdapter($"SELECT * FROM UsersData WHERE Login = '{LoginText1.Text.Trim()}'", cnn);
                    //SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);
                    if(data.Rows.Count == 1)
                    {
                        SDA = new SqlDataAdapter($"SELECT * FROM UsersData WHERE Login = '{LoginText1.Text.Trim()}' AND Password = '{PasswordText1.Text.Trim()}'", cnn);
                        data = new DataTable();
                        SDA.Fill(data);
                        if(data.Rows.Count == 1)
                        {
                            if(data.Rows[0]["Mod"].ToString() == "True")
                            {
                                EditForm1 Edit = new EditForm1();
                                //this.Hide();
                                Edit.Show();
                            }
                            else
                            {
                                ViewForm View = new ViewForm();
                                //this.Hide();
                                View.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не удалось войти. Проверьте свои логин и пароль и попробуйте снова.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователя с таким логином не существует. ");
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            reg.Show();
        }
    }
}
