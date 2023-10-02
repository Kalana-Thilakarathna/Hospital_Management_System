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

namespace HMS
{
    public partial class Receptionist : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KLVMU8H\\MSSQLSERVER01;Initial Catalog=HMS;Integrated Security=True");
        public static Receptionist instance;
        public TextBox tb;
        public Receptionist()
        {
            InitializeComponent();
            label1.Text = Log.instence.name;
            instance = this;
            tb = textBox5;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select patient.P_id,patient.P_name,patient.P_tele,patient.Symtepms,gardian.G_name,gardian.G_tele from patient inner join gardian on patient.G_id = Gardian.G_id where Patient.P_name = '" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Gardian gardian = new Gardian();
            gardian.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Patient (P_id,P_name,G_id,P_tele,Symtepms) values ('" + textBox2.Text + "','" + textBox3.Text + "','"+textBox5.Text+"','" + textBox4.Text + "','"+richTextBox1.Text+"')";
            cmd.ExecuteNonQuery();
            MessageBox.Show("insertion complete");
            connection.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("are you sure!!!");
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from patient where p_id = '" + textBox6.Text + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("discharged");
        }
    }
}
