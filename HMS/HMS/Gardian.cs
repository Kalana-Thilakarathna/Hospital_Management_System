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
    
    public partial class Gardian : Form
    {
        //public static Gardian instance;
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KLVMU8H\\MSSQLSERVER01;Initial Catalog=HMS;Integrated Security=True");
        public Gardian()
        {
            InitializeComponent();
            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            string command = $"insert into Gardian (G_name, G_tele) values ('{textBox1.Text}', '{textBox3.Text}')";
            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();

            string command2 = $"select G_id from Gardian where G_name = '{textBox1.Text}' and G_tele = '{textBox3.Text}'";
            SqlCommand cmd2 = new SqlCommand(command2, connection);
            cmd2.ExecuteNonQuery();
            SqlDataAdapter GIDAdapter = new SqlDataAdapter(cmd2);
            DataTable GIDs = new DataTable();
            GIDAdapter.Fill(GIDs);


            Receptionist.instance.TextBox5.Text = GIDs.Rows[0]["G_id"].ToString();
            Receptionist.instance.Label6.Text = $"ID : {GIDs.Rows[0]["G_id"]}  {textBox1.Text}";
            connection.Close();
            this.Close();
        }

        public void Display()
        {
            connection.Open();
            string command = "select * from Gardian";
            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();
            SqlDataAdapter dataAD = new SqlDataAdapter(cmd);
            DataTable dataTab = new DataTable();
            dataAD.Fill(dataTab);
            dataGridView1.DataSource = dataTab;
            connection.Close();

            dataGridView1.Columns["G_id"].Width = 35;
            dataGridView1.Columns["G_name"].Width = 60;
            dataGridView1.Columns["G_tele"].Width = 70;
        }
    }
}
