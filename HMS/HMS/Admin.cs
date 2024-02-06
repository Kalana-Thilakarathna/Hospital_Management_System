using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using BCrypt.Net;

namespace HMS
{
    public partial class Admin : Form
    {
        
        string job;
        int id;
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KLVMU8H\\MSSQLSERVER01;Initial Catalog=HMS;Integrated Security=True");
        public Admin()
        {
            InitializeComponent();
            Display();
            maleRadio.Checked = true;
            radioButton2.Checked = true;
            button9.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            label4.Text = Log.instence.name;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
  
            try
            {
                label2.Text = "Update Employee Details";
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

                if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Male") 
                    { maleRadio.Checked = true; }
                else 
                    { femaleRadio.Checked = true; }

                if (dataGridView1.SelectedRows[0].Cells[6].Value.ToString() == "Receptionist")
                    { radioButton2.Checked = true; }
                else if (dataGridView1.SelectedRows[0].Cells[6].Value.ToString() == "Doctor")
                    { radioButton1.Checked = true; }
                else 
                    { radioButton3.Checked = true; }

                button6.Visible = true;
                button7.Visible = true;
                button3.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                textBox5.Visible = false;
            }
            catch
            {
                MessageBox.Show("Select a whole row to Update.", "Row Selection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int ageValue;
            if (Int32.TryParse(textBox6.Text, out ageValue))
            {
                try
                {
                    string gender;
                    if (maleRadio.Checked) 
                        { gender = "Male"; }
                    else 
                        { gender = "Female"; }

                    string eType;
                    if (radioButton2.Checked) 
                        { eType = "Receptionist"; }
                    else if (radioButton1.Checked) 
                        { eType = "Doctor"; }
                    else 
                        { eType = "Admin"; }

                    string Eid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    int EidInt = int.Parse(Eid);
                    connection.Open();
                    string command = $"UPDATE Employee SET E_name = '{textBox2.Text}', E_gender = '{gender}', E_age = '{textBox6.Text}', E_address = '{textBox1.Text}', E_tele = '{textBox3.Text}', E_type = '{eType}', E_password = '{textBox7.Text}'  WHERE E_id = {EidInt}";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    label2.Text = "Add Employee Details";
                    textBox2.Clear();
                    textBox6.Clear();
                    textBox1.Clear();
                    textBox3.Clear();
                    textBox7.Clear();
                    maleRadio.Checked = true;
                    radioButton2.Checked = true;
                    Display();
                    MessageBox.Show("Successfully updated", "Update Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button6.Visible = false;
                    button7.Visible = false;
                    button3.Visible = true;
                    button2.Visible = true;
                    button4.Visible = true;
                    textBox5.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Some of the values entered for Employee's Detail is not valid.\n Age limit 15 - 100\n lenght of Telephone number should be 10", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Value entered for Employee's age is not valid.", "Age Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label2.Text = "Add Employee Details";
            textBox2.Clear();
            textBox6.Clear();
            textBox1.Clear();
            textBox3.Clear();
            textBox7.Clear();
            maleRadio.Checked = true;
            radioButton2.Checked = true;
            button6.Visible = false;
            button7.Visible = false;
            button3.Visible = true;
            button2.Visible = true;
            button4.Visible = true;
            textBox5.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox6.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox7.Text != "")
            {
                int ageValue;
                if (Int32.TryParse(textBox6.Text, out ageValue))
                {
                    try
                    {
                        string gender;
                        if (maleRadio.Checked) { gender = "Male"; }
                        else { gender = "Female"; }

                        string eType;
                        if (radioButton2.Checked) { eType = "Receptionist"; }
                        else if (radioButton1.Checked) { eType = "Doctor"; }
                        else { eType = "Admin"; }

                        string password = textBox7.Text;
                        //string salt = BCrypt.Net.BCrypt.GenerateSalt();
                        //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                        connection.Open();
                        string command = $"insert into [Employee] (E_name, E_gender, E_age,  E_address, E_tele, E_type, E_password) values('{textBox2.Text}', '{gender}', '{ageValue}', '{textBox1.Text}', '{textBox3.Text}', '{eType}', '{password}')";
                        SqlCommand cmd = new SqlCommand(command, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        Display();

                        textBox2.Clear();
                        textBox1.Clear();
                        textBox3.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        maleRadio.Checked = true;
                        radioButton2.Checked = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        //MessageBox.Show("Some of the values entered for Employee's Detail is not valid.\n Age limit 15 - 100\n lenght of Telephone number should be 10", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Value entered for Employee's age is not valid.", "Age Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("All the values are required.", "Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string Eid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int EidInt = int.Parse(Eid);

                try
                {
                    connection.Open();
                    string command = $"select * from Employee where E_id = {EidInt}";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();

                    if (MessageBox.Show("Are you sure you want to remove this Employee?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string command2 = $"delete from Employee where E_id = {EidInt}";
                        SqlCommand cmd2 = new SqlCommand(command2, connection);
                        cmd2.ExecuteNonQuery();
                        connection.Close();
                        Display();
                        textBox6.Clear();
                    }
                    else
                    {
                        textBox6.Clear();
                        connection.Close();
                    }
                }
                catch
                {
                    MessageBox.Show($"Patient {EidInt} not found", "Patient Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Select a whole row to Delete.", "Row Selection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //E_name, E_gender, E_address, E_tele, E_age, E_type, E_password
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string command = $"SELECT * FROM Employee WHERE E_id LIKE '%{textBox5.Text}%' OR E_name LIKE '%{textBox5.Text}%' OR E_gender = '{textBox5.Text}' OR E_address LIKE '%{textBox5.Text}%' OR E_tele LIKE '%{textBox5.Text}%' OR E_age LIKE '%{textBox5.Text}%' OR E_type LIKE '%{textBox5.Text}%';";
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dataAD1 = new SqlDataAdapter(cmd);
                DataTable dataTab1 = new DataTable();
                dataAD1.Fill(dataTab1);
                if (dataTab1.Rows.Count == 0)
                {
                    MessageBox.Show("No matching results");
                    textBox5.Clear();
                }
                else
                {
                    dataGridView1.DataSource = dataTab1;
                    button9.Visible = true;
                    textBox5.Clear();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Display();
            textBox5.Clear();
            button9.Visible = false;
        }

        private void Display()
        {
            connection.Open();
            string command = "select * from Employee";
            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter dataAD = new SqlDataAdapter(cmd);
            DataTable dataTab = new DataTable();
            dataAD.Fill(dataTab);
            dataGridView1.DataSource = dataTab;
            connection.Close();

            dataGridView1.Font = new Font("Arial", 9);

            dataGridView1.Columns["E_id"].Width = 35;
            dataGridView1.Columns["E_name"].Width = 112;
            dataGridView1.Columns["E_gender"].Width = 70;
            dataGridView1.Columns["E_age"].Width = 45;
            dataGridView1.Columns["E_address"].Width = 150;
            dataGridView1.Columns["E_tele"].Width = 70;
            dataGridView1.Columns["E_type"].Width = 80;
            dataGridView1.Columns["E_password"].Width = 70;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
