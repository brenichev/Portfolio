using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsTest
{
    public partial class EditForm1 : Form
    {
        public EditForm1()
        {
            InitializeComponent();
            ComboLoad(TypeIdBox, "EventTypes", "idType", "EventType");
            ComboLoad(AgeComboBox, "Ages", "idAge", "Age");
            ComboLoad(FormComboBox, "EventForms", "idForm", "EventForm");

        }
        private string EventId;
        private string StageId;
        private string ManagerId;
        private string MemberId;
        private string ListId;
        private string AdressId;
        private string EventSql = "SELECT idEvents, EventName, EventTypes.EventType, Ages.Age, EventForms.EventForm, EventLink, " +
            "EventDesc FROM Events JOIN EventTypes ON Typeid = idType JOIN Ages ON Events.Ageid = Ages.idAge JOIN EventForms ON Events.Formid = EventForms.idForm";
        private string StagesSql = "SELECT idStage, StageNumber, Events.EventName, StageName, Adresses.Adress, " +
            "House, DateStart, DateFinish, StageCost, StageDesc FROM Stages JOIN Events ON EventId = idEvents JOIN Adresses ON AdressId = idAdress";
        private string ManagersSql = "SELECT idManager, ManagerSurname, ManagerName, ManagerOtch, ManagerTypes.ManagerType, ManagerLink, ManagerDesc FROM Managers JOIN ManagerTypes ON ManagerTypeId = idManagerType";
        private string MembersSql = "SELECT idMember, MemberSurname, MemberName, MemberOtch, MemberTypes.MemberType, MemberLink, MemberDesc FROM Members JOIN MemberTypes ON MemberTypeId = idMemberType";
        private string ListSql = "SELECT idPart, Stages.StageName, Isnull(MemberSurname,'') +' '+ Isnull(MemberName,'')+' '+ Isnull(MemberOtch,'') as MemberFIO FROM ParticipationList JOIN Stages ON StageId = idStage JOIN Members ON MemberId = idMember";
        private string ListManagerSql = "SELECT idForManager, Stages.StageName, Isnull(ManagerSurname,'') +' '+ Isnull(ManagerName,'')+' '+ Isnull(ManagerOtch,'') as ManagerFIO FROM ManagersList JOIN Stages ON StageId = idStage JOIN Managers ON ManagerId = idManager";
        private string StreetsSql = "SELECT idAdress, Adress FROM Adresses";

        private void Savebutton1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"INSERT INTO Events VALUES('{EventName.Text}',{TypeIdBox.SelectedIndex + 1},{AgeComboBox.SelectedIndex + 1}," +
                        $"{FormComboBox.SelectedIndex + 1},'{EventLink.Text}','{EventDesc.Text}');";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    Savebutton1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Savebutton1_MouseLeave(object sender, EventArgs e)
        {
            Savebutton1.BackColor = Color.Transparent;
        }

        private void FillTable(DataGridView dataGrid, string sql)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    //SqlDataAdapter SDA = new SqlDataAdapter($@"SELECT idEvents, EventName, EventTypes.EventType, Ages.Age, EventForms.EventForm, EventLink, EventDesc FROM Events JOIN EventTypes ON Typeid = idType JOIN Ages ON Events.Ageid = Ages.idAge JOIN EventForms ON Events.Formid = EventForms.idForm", cnn);
                    SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);
                    dataGrid.DataSource = data;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }



        private void EventsControl_Click(object sender, EventArgs e)
        {
            FillTable(dataGridView1, EventSql);
            ComboLoad(TypeIdBox2, "EventTypes", "idType", "EventType");
            ComboLoad(AgeComboBox2, "Ages", "idAge", "Age");
            ComboLoad(FormComboBox2, "EventForms", "idForm", "EventForm");
            FillTextBoxesEvents();
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string EventId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    string sql = $"DELETE FROM Events WHERE idEvents = {EventId}";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView1, EventSql);
                    DeleteButton1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesEvents();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesStages();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesManagers();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesMembers();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesList();
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesManagers();
        }
        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillTextBoxesStreet();
        }


        private void FillTextBoxesEvents()
        {
            EventName2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            EventId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                string sql = $"SELECT Typeid, Ageid, Formid FROM Events WHERE idEvents = {EventId}";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TypeIdBox2.SelectedIndex = (int)reader.GetValue(0) - 1;
                            AgeComboBox2.SelectedIndex = (int)reader.GetValue(1) - 1;
                            FormComboBox2.SelectedIndex = (int)reader.GetValue(2) - 1;
                        }
                    }
                }
            }
            //TypeIdBox2.SelectedIndex = 1;
            //AgeComboBox2.SelectedIndex = (int)dataGridView1.SelectedRows[0].Cells[3].Value;
            //FormComboBox2.SelectedIndex = (int)dataGridView1.SelectedRows[0].Cells[4].Value;
            EventLink2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            EventDesc2.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void FillTextBoxesStages()
        {
            StageId = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            StageNumeric2.Value = (int)dataGridView2.SelectedRows[0].Cells[1].Value;
            EventsComboBox2.SelectedIndex = EventsComboBox2.FindString(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
            StageName2.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
            AdressComboBox2.SelectedValue = AdressComboBox2.FindString(dataGridView2.SelectedRows[0].Cells[4].Value.ToString()) + 1;
            HouseText2.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
            dateTimeStart2.Value = Convert.ToDateTime(dataGridView2.SelectedRows[0].Cells[6].Value);
            dateTimeFinish2.Value = Convert.ToDateTime(dataGridView2.SelectedRows[0].Cells[7].Value);
            CostNumeric2.Value = int.Parse(dataGridView2.SelectedRows[0].Cells[8].Value.ToString());
            StageDesc2.Text = dataGridView2.SelectedRows[0].Cells[9].Value.ToString();
        }

        private void FillTextBoxesManagers()
        {
            ManagerId = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            ManagerSurname1.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            ManagerName1.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            ManagerOtch1.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            ManagerTypeCombo1.SelectedIndex = ManagerTypeCombo1.FindString(dataGridView3.SelectedRows[0].Cells[4].Value.ToString());
            ManagerLink1.Text = dataGridView3.SelectedRows[0].Cells[5].Value.ToString();
            ManagerDesc1.Text = dataGridView3.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void FillTextBoxesMembers()
        {
            MemberId = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
            MemberSurname1.Text = dataGridView4.SelectedRows[0].Cells[1].Value.ToString();
            MemberOtch1.Text = dataGridView4.SelectedRows[0].Cells[2].Value.ToString();
            MemberTypeCombo1.SelectedIndex = ManagerTypeCombo1.FindString(dataGridView3.SelectedRows[0].Cells[3].Value.ToString());
            MemberLink1.Text = dataGridView4.SelectedRows[0].Cells[4].Value.ToString();
            MemberDesc1.Text = dataGridView4.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void FillTextBoxesList()
        {
            ListId = dataGridView5.SelectedRows[0].Cells[0].Value.ToString();
            EventListCombo1.SelectedIndex = EventListCombo1.FindString(dataGridView5.SelectedRows[0].Cells[1].Value.ToString());
            MemberListCombo1.SelectedIndex = MemberListCombo1.FindString(dataGridView5.SelectedRows[0].Cells[2].Value.ToString());
        }

        private void FillTextBoxesManagersList()
        {
            ListId = dataGridView6.SelectedRows[0].Cells[0].Value.ToString();
            EventPartCombo2.SelectedIndex = EventPartCombo2.FindString(dataGridView6.SelectedRows[0].Cells[1].Value.ToString());
            ManagerPartCombo2.SelectedIndex = ManagerPartCombo2.FindString(dataGridView6.SelectedRows[0].Cells[2].Value.ToString());
        }

        private void FillTextBoxesStreet()
        {
            AdressId = dataGridView7.SelectedRows[0].Cells[0].Value.ToString();
            StreetText1.Text = dataGridView7.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void SaveButton2_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"UPDATE Events SET EventName = '{EventName2.Text}', Typeid = {TypeIdBox2.SelectedIndex + 1}," +
                        $" Ageid = {AgeComboBox2.SelectedIndex + 1}, Formid = {FormComboBox2.SelectedIndex + 1}, EventLink = '{EventLink2.Text}'," +
                        $" EventDesc = '{EventDesc2.Text}' WHERE idEvents = {EventId};";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView1, EventSql);
                    FillTextBoxesEvents();
                    SaveButton2.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ComboLoad(ComboBox comboBox, string table, string id, string col)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"SELECT {id}, {col} FROM {table}";
                    SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);
                    comboBox.DisplayMember = col;
                    comboBox.ValueMember = id;
                    comboBox.DataSource = data;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
        }
        private void ComboLoadFIO(ComboBox comboBox, string table, string id, string col, string colName)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"SELECT {id}, {col} FROM {table}";
                    SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);
                    comboBox.DisplayMember = colName;
                    comboBox.ValueMember = id;
                    comboBox.DataSource = data;
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
        }

        private void SaveButtonStages1_Click(object sender, EventArgs e)
        {
            bool ok = true;
            using (var cnn = new SqlConnection())
            {                
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    SqlDataAdapter SDA = new SqlDataAdapter($"SELECT * FROM Stages WHERE EventId = {EventsComboBox1.SelectedValue} AND StageNumber = '{StageNumeric1.Value}'", cnn);
                    //SqlDataAdapter SDA = new SqlDataAdapter(sql, cnn);
                    DataTable data = new DataTable();
                    SDA.Fill(data);
                    if (data.Rows.Count == 1)
                    {                        
                            MessageBox.Show("Такой номер этапа уже есть. Пожалуйста измените.");
                            ok = false;
                    }
                    else
                    {
                        
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (ok)
            {
                if (DateTime.Compare(dateTimeStart1.Value, dateTimeFinish1.Value) <= 0)
                    using (var cnn = new SqlConnection())
                    {
                        cnn.ConnectionString = Form1.connectionString;
                        cnn.Open();
                        try
                        {
                            string sql = $"INSERT INTO Stages VALUES('{StageNumeric1.Value}',{EventsComboBox1.SelectedValue},'{StageName1.Text}',{AdressComboBox1.SelectedValue},'{HouseText1.Text}','{dateTimeStart1.Value}','{dateTimeFinish1.Value}','{CostNumeric1.Value}','{StageDesc1.Text}');";
                            SqlCommand cmd = new SqlCommand(sql, cnn);
                            cmd.ExecuteNonQuery();
                            SaveButtonStages1.BackColor = Color.Green;
                        }
                        catch
                        {
                            MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                else
                    MessageBox.Show("Дата начала наступает позже, чем дата конца мероприятия", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StagesControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (StagesControl.SelectedIndex)
            {
                case 0:
                    ComboLoad(EventsComboBox2, "Events", "idEvents", "EventName");
                    ComboLoad(AdressComboBox2, "Adresses", "idAdress", "Adress");                    
                    break;
                case 1:
                    ComboLoad(EventsComboBox2, "Events", "idEvents", "EventName");
                    ComboLoad(AdressComboBox2, "Adresses", "idAdress", "Adress");                    
                    FillTable(dataGridView2, StagesSql);
                    break;
            }
        }

        private void DateLoad(DateTimePicker dateTime, string table, string col)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"SELECT {col} FROM {table}";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int ColIndex = reader.GetOrdinal(col);
                            //dateTimePicker1.Value = 0;
                            while (reader.Read())
                            {
                                var row = Convert.ToDateTime(reader.GetValue(ColIndex));
                                dateTime.Value = row;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
        }

        private void SaveButtonStages2_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(dateTimeStart2.Value, dateTimeFinish2.Value) <= 0)
                using (var cnn = new SqlConnection())
                {
                    cnn.ConnectionString = Form1.connectionString;
                    cnn.Open();
                    try
                    {
                        string sql = $"UPDATE Stages SET StageNumber = '{StageNumeric2.Value}', EventId = {EventsComboBox2.SelectedValue}, StageName = '{StageName2.Text}', AdressId = {AdressComboBox2.SelectedValue}, House = '{HouseText2.Text}', DateStart = '{dateTimeStart2.Value}', DateFinish = '{dateTimeFinish2.Value}', StageCost = {CostNumeric2.Value}, StageDesc = '{StageDesc2.Text}' WHERE idStage = {StageId};";
                        SqlCommand cmd = new SqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        FillTable(dataGridView2, StagesSql);
                        FillTextBoxesStages();
                        SaveButtonStages2.BackColor = Color.Green;
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось загрузить данные таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            else
                MessageBox.Show("Дата начала наступает позже, чем дата конца мероприятия", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteButtonStages_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    StageId = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    string sql = $"DELETE FROM Stages WHERE idStage = {StageId}";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView2, StagesSql);
                    DeleteButtonStages.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (MainTables.SelectedIndex)
            {
                case 0:
                    ComboLoad(TypeIdBox, "EventTypes", "idType", "EventType");
                    ComboLoad(AgeComboBox, "Ages", "idAge", "Age");
                    ComboLoad(FormComboBox, "EventForms", "idForm", "EventForm");
                    break;

                case 1:
                    ComboLoad(EventsComboBox1, "Events", "idEvents", "EventName");
                    ComboLoad(AdressComboBox1, "Adresses", "idAdress", "Adress");

                    ComboLoad(EventsComboBox2, "Events", "idEvents", "EventName");
                    ComboLoad(AdressComboBox2, "Adresses", "idAdress", "Adress");
                    FillTable(dataGridView2, StagesSql);
                    break;
                case 2:
                    ComboLoad(ManagerTypeCombo1, "ManagerTypes", "idManagerType", "ManagerType");

                    FillTable(dataGridView3, ManagersSql);
                    break;
                case 3:
                    ComboLoad(MemberTypeCombo1, "MemberTypes", "idMemberType", "MemberType");
                    FillTable(dataGridView4, MembersSql);
                    break;

                case 4:
                    //ComboLoad(EventListCombo1, "Stages", "idStage", "StageName");
                    ComboLoadFIO(EventListCombo1, "Stages", "idStage", "CAST(StageName as varchar) + ' ' + CAST(StageNumber as varchar) as Stage", "Stage");
                    ComboLoadFIO(MemberListCombo1, "Members", "idMember", "Isnull(MemberSurname,'') +' '+ Isnull(MemberName,'')+' '+ Isnull(MemberOtch,'') as MemberFIO", "MemberFIO");
                    FillTable(dataGridView5, ListSql);
                    break;
                case 5:
                    ComboLoad(EventPartCombo2, "Stages", "idStage", "StageName");
                    ComboLoadFIO(ManagerPartCombo2, "Managers", "idManager", "Isnull(ManagerSurname,'') +' '+ Isnull(ManagerName,'')+' '+ Isnull(ManagerOtch,'') as ManagerFIO", "ManagerFIO");
                    FillTable(dataGridView6, ListManagerSql);
                    break;
                case 6:
                    FillTable(dataGridView7, StreetsSql);
                    break;
                case 7:
                    MainTables.SelectedIndex = 0;
                    ViewForm view = new ViewForm();
                    view.Show();
                    break;

            }
        }

        private void SaveButtonStages1_MouseLeave(object sender, EventArgs e)
        {
            Savebutton1.BackColor = Color.Transparent;
        }

        private void SaveButtonManager1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"INSERT INTO Managers VALUES('{ManagerSurname1.Text}','{ManagerName1.Text}','{ManagerOtch1.Text}',{ManagerTypeCombo1.SelectedValue},'{ManagerLink1.Text}','{ManagerDesc1.Text}');";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    SaveButtonManager1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillTable(dataGridView3, ManagersSql);
        }

        private void SaveButtonManager1_MouseLeave(object sender, EventArgs e)
        {
            Savebutton1.BackColor = Color.Transparent;
        }

        private void DeleteButtonManager1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    ManagerId = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                    string sql = $"DELETE FROM Managers WHERE idManager = {ManagerId}";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView3, ManagersSql);
                    DeleteButtonManager1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateButtonManager1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"UPDATE Managers SET ManagerSurname = '{ManagerSurname1.Text}', ManagerName = '{ManagerName1.Text}', ManagerOtch = '{ManagerOtch1.Text}', ManagerTypeId = {ManagerTypeCombo1.SelectedValue}, ManagerLink = '{ManagerLink1.Text}', ManagerDesc = '{ManagerDesc1.Text}' WHERE idManager = {ManagerId};";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView3, ManagersSql);
                    FillTextBoxesManagers();
                    UpdateButtonManager1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось оновить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveButtonMember1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"INSERT INTO Members VALUES('{MemberSurname1.Text}','{MemberName1.Text}','{MemberOtch1.Text}',{MemberTypeCombo1.SelectedValue},'{MemberLink1.Text}','{MemberDesc1.Text}');";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    SaveButtonMember1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillTable(dataGridView4, MembersSql);
        }

        private void SaveButtonMember_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            temp.BackColor = Color.Green;
        }

        private void SaveButtonMember1_MouseLeave(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            temp.BackColor = Color.Transparent;
        }

        private void DeleteFromTable(DataGridView dataGrid, string Table, string idCol)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string Id = dataGrid.SelectedRows[0].Cells[0].Value.ToString();
                    string sql = $"DELETE FROM {Table} WHERE {idCol} = {Id}";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DeleteButtonMember1_Click(object sender, EventArgs e)
        {
            DeleteFromTable(dataGridView4, "Members", "idMember");
            FillTable(dataGridView4, MembersSql);
        }

        private void UpdateButtonMember1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"UPDATE Members SET MemberSurname = '{MemberSurname1.Text}', MemberName = '{MemberName1.Text}', MemberOtch = '{MemberOtch1.Text}', MemberTypeId = {MemberTypeCombo1.SelectedValue}, MemberLink = '{MemberLink1.Text}', MemberDesc = '{MemberDesc1.Text}' WHERE idMember = {MemberId};";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    UpdateButtonMember1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось обновить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillTable(dataGridView4, MembersSql);
            FillTextBoxesManagers();
        }

        private void SaveButtonList1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"INSERT INTO ParticipationList VALUES({EventListCombo1.SelectedValue},{MemberListCombo1.SelectedValue});";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    SaveButtonList1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillTable(dataGridView5, ListSql);
        }

        private void DeleteButtonList1_Click(object sender, EventArgs e)
        {
            DeleteFromTable(dataGridView5, "ParticipationList", "idPart");
            FillTable(dataGridView5, ListSql);
        }

        private void UpdateButtonList1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"UPDATE ParticipationList SET StageId = {EventListCombo1.SelectedValue}, MemberId = {MemberListCombo1.SelectedValue}  WHERE idPart = {ListId};";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView5, ListSql);
                    FillTextBoxesList();
                    UpdateButtonList1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось обновить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveButtonListManager1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"INSERT INTO ManagersList VALUES({EventPartCombo2.SelectedValue},{ManagerPartCombo2.SelectedValue});";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    SaveButtonListManager1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillTable(dataGridView6, ListManagerSql);
        }

        private void DeleteButtonListMaanger1_Click(object sender, EventArgs e)
        {
            DeleteFromTable(dataGridView6, "ManagersList", "idForManager");
            FillTable(dataGridView6, ListManagerSql);
        }

        private void UpdateButtonListManager1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"UPDATE ParticipationList SET StageId = {EventPartCombo2.SelectedValue}, ManagerId = {ManagerPartCombo2.SelectedValue}  WHERE idPart = {ListId};";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView6, ListManagerSql);
                    FillTextBoxesList();
                    UpdateButtonListManager1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не обновить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveButtonListManager1_MouseLeave(object sender, EventArgs e)
        {
            SaveButtonListManager1.BackColor = Color.Transparent;
        }

        private void StreetSave1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"INSERT INTO Adresses VALUES('{StreetText1.Text}');";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    StreetSave1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillTable(dataGridView7, StreetsSql);
        }

        private void StreetDelete1_Click(object sender, EventArgs e)
        {
            DeleteFromTable(dataGridView7, "idAdress", "Adress");
            FillTable(dataGridView7, StreetsSql);
        }

        private void StreetUpdate1_Click(object sender, EventArgs e)
        {
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = Form1.connectionString;
                cnn.Open();
                try
                {
                    string sql = $"UPDATE Adresses SET Adress = '{StreetText1.Text}' WHERE idAdress = {AdressId};";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    FillTable(dataGridView7, StreetsSql);
                    FillTextBoxesStreet();
                    StreetUpdate1.BackColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Не обновить данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HouseText1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 191 || e.KeyChar >= 206) && (e.KeyChar <= 223 || e.KeyChar >= 238) && (e.KeyChar <= 46 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }
    }
}
