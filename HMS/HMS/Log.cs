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
using System.Linq.Expressions;

namespace HMS
{
    public partial class Log : Form
    {
        public static Log instence;
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-KLVMU8H\\MSSQLSERVER01;Initial Catalog=HMS;Integrated Security=True");
        public string name;
        public Log()
        {
            InitializeComponent();
            instence = this;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string ePass = "";
                string eType = "";
                SqlCommand cmd = new SqlCommand("select E_password,E_type from Employee where e_name = '"+textBox1.Text+"'", connection);
                SqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    ePass = result[0].ToString();
                    eType = result[1].ToString();
                }
                
                if (textBox2.Text == ePass)
                {
                    switch (eType)
                    {
                        case "Admin":
                            name = textBox1.Text;
                            Admin admin = new Admin();
                            admin.Show();
                            break;
                        case "Receptionist":
                            name = textBox1.Text;
                            Receptionist receptionist = new Receptionist(); 
                            receptionist.Show();
                            break;
                        case "Doctor":
                            name = textBox1.Text;
                            Doctor doctor = new Doctor();
                            doctor.Show();
                            break;
                        default:
                            MessageBox.Show("error");
                            break;
                    }                    
                }
                else
                {
                    MessageBox.Show("incorrect");
                }
                connection.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
