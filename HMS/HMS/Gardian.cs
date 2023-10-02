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
        public static Gardian instance;
        public string g_id;
        int check = 1;
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KLVMU8H\\MSSQLSERVER01;Initial Catalog=HMS;Integrated Security=True");
        public Gardian()
        {
            InitializeComponent();
            instance = this;
            textBox1.TextChanged += textBox2_TextChanged;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            
            if (check == 0)
            {
                cmd.CommandText = "insert into gardian (G_id, G_name, G_tele) values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "')";
            }
            else
            {
                cmd.CommandText = "update gardian set G_name = '" + textBox1.Text + "', G_tele = '" + textBox3.Text + "' where g_id = '"+textBox2.Text+"'";
            }
            cmd.ExecuteNonQuery();
            connection.Close();
            g_id = textBox2.Text;
            Receptionist.instance.tb.Text = g_id;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                textBox2.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            getId();
            
        }

        void getId ()
        {
            connection.Open();
            SqlCommand cmd2 = connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select*from gardian where g_name = '" + textBox1.Text + "'";
            cmd2.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            if (dt.Rows.Count < 0)
            {
                check = 0;
            }
            connection.Close();
        }
    }
}
