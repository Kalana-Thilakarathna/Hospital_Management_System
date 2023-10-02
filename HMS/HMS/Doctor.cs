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
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KLVMU8H\\MSSQLSERVER01;Initial Catalog=HMS;Integrated Security=True");
        public Doctor()
        {
            InitializeComponent();
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
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select patient.P_id,patient.P_name,patient.P_tele,gardian.G_name,gardian.G_tele from patient inner join gardian on patient.G_id = Gardian.G_id where Patient.P_name = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select patient.P_id,patient.P_name,patient.P_tele,gardian.G_name,gardian.G_tele from patient inner join gardian on patient.G_id = Gardian.G_id";
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void display()
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
