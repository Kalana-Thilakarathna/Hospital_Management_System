using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace HMS
{
    public partial class Receptionist : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-7U6TD0GQ\\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        public static Receptionist instance;
        public Receptionist()
        {
            InitializeComponent();
            Display();
            button3.Visible = true;
            button6.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            maleRadio.Checked = true;
            label1.Text = Log.instence.name;
            instance = this;
        }

        public TextBox TextBox5
        {
            get { return textBox5; }
        }

        public System.Windows.Forms.Label Label6
        {
            get { return label6; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string command = $"SELECT * FROM Patient WHERE P_id LIKE '%{search.Text}%' OR P_name LIKE '%{search.Text}%' OR P_gender = '{search.Text}' OR P_age LIKE '%{search.Text}%' OR P_tele LIKE '%{search.Text}%' OR P_address LIKE '%{search.Text}%' OR P_bloodT LIKE '%{search.Text}%' OR Symptoms LIKE '%{search.Text}%';";
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dataAD1 = new SqlDataAdapter(cmd);
                DataTable dataTab1 = new DataTable();
                dataAD1.Fill(dataTab1);
                if (dataTab1.Rows.Count == 0)
                {
                    MessageBox.Show("No matching results");
                    search.Clear();
                }
                else
                {
                    dataGridView11.DataSource = dataTab1;
                    search.Clear();
                    button9.Visible = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Gardian gardian = new Gardian();
            gardian.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox7.Text != "" && textBox4.Text != "" && textBox1.Text != "" && textBox2.Text != "" && richTextBox1.Text != "" && textBox5.Text != "")
            {
                int ageValue;
                if (Int32.TryParse(textBox7.Text, out ageValue))
                {
                    string gender;
                    if (maleRadio.Checked)
                    { gender = "Male"; }
                    else
                    { gender = "Female"; }

                    try
                    {
                        connection.Open();
                        string command = $"insert into Patient (P_name, P_gender, P_age, P_tele, P_address, P_bloodT, Symptoms, G_id) values ('{textBox3.Text}', '{gender}', '{ageValue}', '{textBox4.Text}', '{textBox1.Text}', '{textBox2.Text}', '{richTextBox1.Text}', '{textBox5.Text}')";
                        SqlCommand cmd = new SqlCommand(command, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        Display();
                        textBox3.Clear();
                        textBox7.Clear();
                        textBox4.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        richTextBox1.Clear();
                        textBox5.Clear();
                        maleRadio.Checked = true;
                    }
                    catch
                    {
                        MessageBox.Show("Some of the values entered for Patient's Detail is not valid.\n Age limit 15 - 100\n lenght of contact should be 10", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Value entered for Patient's age is not valid.", "Age Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("All information is required.", "Information Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string Pid = dataGridView11.SelectedRows[0].Cells[0].Value.ToString();
                int PidInt = int.Parse(Pid);

                try
                {
                    connection.Open();
                    string command = $"select * from Patient where P_id = {PidInt}";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();

                    if (MessageBox.Show("Are you sure you want to discharge this patient?", "Confirm Discharge", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string command2 = $"delete from patient where P_id = '{PidInt}'";
                        SqlCommand cmd2 = new SqlCommand(command2, connection);
                        cmd2.ExecuteNonQuery();
                        connection.Close();
                        Display();
                    }
                    else
                    {
                        connection.Close();
                    }
                }
                catch
                {
                    MessageBox.Show($"Patient {PidInt} not found", "Patient Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Select a whole row to Delete.", "Row Selection Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Display()
        {
            connection.Open();
            string command = "select * from Patient";
            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter dataAD = new SqlDataAdapter(cmd);
            DataTable dataTab = new DataTable();
            dataAD.Fill(dataTab);
            dataGridView11.DataSource = dataTab;
            connection.Close();

            dataGridView11.Columns["P_id"].Width = 35;
            dataGridView11.Columns["P_name"].Width = 90;
            dataGridView11.Columns["P_gender"].Width = 60;
            dataGridView11.Columns["P_age"].Width = 50;
            dataGridView11.Columns["P_tele"].Width = 70;
            dataGridView11.Columns["G_id"].Width = 35;
            dataGridView11.Columns["P_bloodT"].Width = 60;
            dataGridView11.Columns["Symptoms"].Width = 100;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    label8.Text = "Update Patient Details";
                    textBox3.Text = dataGridView11.SelectedRows[0].Cells[1].Value.ToString();
                    textBox7.Text = dataGridView11.SelectedRows[0].Cells[3].Value.ToString();
                    textBox4.Text = dataGridView11.SelectedRows[0].Cells[4].Value.ToString();
                    textBox1.Text = dataGridView11.SelectedRows[0].Cells[5].Value.ToString();
                    textBox2.Text = dataGridView11.SelectedRows[0].Cells[6].Value.ToString();
                    richTextBox1.Text = dataGridView11.SelectedRows[0].Cells[7].Value.ToString();
                    textBox5.Text = dataGridView11.SelectedRows[0].Cells[8].Value.ToString();

                    if (dataGridView11.SelectedRows[0].Cells[2].Value.ToString() == "Male")
                    { maleRadio.Checked = true; }
                    else
                    { femaleRadio.Checked = true; }

                    button6.Visible = true;
                    button8.Visible = true;
                    button3.Visible = false;
                    search.Visible = false;
                    button1.Visible = false;
                    button5.Visible = false;
                }
                catch
                {
                    MessageBox.Show("Select a whole row to Update ");
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            int ageValue;
            if (Int32.TryParse(textBox7.Text, out ageValue))
            {
                try
                {
                    string gender;
                    if (maleRadio.Checked)
                    { gender = "Male"; }
                    else
                    { gender = "Female"; }

                    string Pid = dataGridView11.SelectedRows[0].Cells[0].Value.ToString();
                    int PidInt = int.Parse(Pid);
                    connection.Open();
                    string command = $"UPDATE Patient SET P_name = '{textBox3.Text}', P_gender = '{gender}', P_age = '{textBox7.Text}', P_tele = '{textBox4.Text}', P_address = '{textBox1.Text}', P_bloodT = '{textBox2.Text}', Symptoms = '{richTextBox1.Text}', G_id = '{textBox5.Text}' WHERE P_id = {PidInt}";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    textBox3.Clear();
                    textBox7.Clear();
                    textBox4.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    richTextBox1.Clear();
                    textBox5.Clear();
                    maleRadio.Checked = true;
                    Display();
                    MessageBox.Show("Successfully updated", "Update Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button6.Visible = false;
                    button8.Visible = false;
                    button3.Visible = true;
                    search.Visible = true;
                    button1.Visible = true;
                    button5.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Some of the values entered for Patient's Detail is not valid.\n Age limit 15 - 100\n lenght of contact should be 10", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Value entered for Patient's age is not valid.", "Age Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Display();
            button9.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox7.Clear();
            textBox4.Clear();
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            textBox5.Clear();
            maleRadio.Checked = true;
            button6.Visible = false;
            button8.Visible = false;
            button3.Visible = true;
            search.Visible = true;
            button1.Visible = true;
            button5.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
