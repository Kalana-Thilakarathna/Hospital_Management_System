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

namespace HMS
{
    public partial class Doctor : Form
    {
        public static Doctor instance;
        public Label ld1;
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-7U6TD0GQ\\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        public Doctor()
        {
            InitializeComponent();
            Display();
            instance = this;
            ld1 = label1;
            label1.Text = Log.instence.name;
        }

        private void Doctor_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
                    dataGridView1.DataSource = dataTab1;
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

        public void Display()
        {
            connection.Open();
            string command = "select * from Patient";
            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter dataAD = new SqlDataAdapter(cmd);
            DataTable dataTab = new DataTable();
            dataAD.Fill(dataTab);
            dataGridView1.DataSource = dataTab;
            connection.Close();

            dataGridView1.Columns["P_id"].Width = 35;
            dataGridView1.Columns["P_name"].Width = 90;
            dataGridView1.Columns["P_gender"].Width = 60;
            dataGridView1.Columns["P_age"].Width = 50;
            dataGridView1.Columns["P_tele"].Width = 70;
            dataGridView1.Columns["G_id"].Width = 35;
            dataGridView1.Columns["P_bloodT"].Width = 60;
            dataGridView1.Columns["Symptoms"].Width = 100;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Display();
        }
    }
}
