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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(LoginText1.Text == "")
                MessageBox.Show("Логин не введен.");
            else
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
                    if (data.Rows.Count == 1)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует. Попробуйте изменить логин.");
                        
                    }
                    else
                    {
                        if(PasswordText1.Text != "")
                        {
                            if (PasswordText1.Text == textBox1.Text)
                            {
                                using (var cnn2 = new SqlConnection())
                                {
                                    cnn2.ConnectionString = Form1.connectionString;
                                    cnn2.Open();
                                    try
                                    {
                                        string sql = $"INSERT INTO UsersData VALUES('{LoginText1.Text.Trim()}','{PasswordText1.Text.Trim()}','False');";
                                        SqlCommand cmd = new SqlCommand(sql, cnn2);
                                        cmd.ExecuteNonQuery();
                                        button2.BackColor = Color.Green;
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают, проверьте правильность ввода.");
                            }
                        }
                        else
                            MessageBox.Show("Пароль не введен.");
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось запустить приложение, обратитесь к администратору", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
