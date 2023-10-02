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
using System.Drawing.Text;
using System.Threading;

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
            button6.Visible = false;
            label4.Text = Log.instence.name;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void doctorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void receptionistToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select*from [Employee]";
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
            connection.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into [Employee] (E_id,E_name,E_type,E_password) values('" + textBox1.Text + "','" + textBox2.Text + "','" + job + "','" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Insertion successful");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            job = "Doctor";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            job = "Receptionist";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            job = "Admin";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from [Employee] where E_name = ('" + textBox4.Text + "'); select*from [Employee]";
                cmd.ExecuteNonQuery();
                MessageBox.Show("deletion done");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (int.TryParse(textBox5.Text, out id))
                {
                    cmd.CommandText = "select E_id, E_name, E_type from [Employee] where e_id = ('" + id + "')";
                }
                else
                {
                    cmd.CommandText = "select E_id, E_name, E_type from [Employee] where E_name = '" + textBox5.Text + "'";
                }
                cmd.ExecuteNonQuery();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (int.TryParse(textBox5.Text, out id))
                {
                    cmd.CommandText = "select E_id,E_name,E_password,E_type from [Employee] where e_id = ('" + id + "')";
                }
                else
                {
                    cmd.CommandText = "select E_id,E_name,E_password,E_type from [Employee] where E_name = '" + textBox5.Text + "'";
                }
                cmd.ExecuteNonQuery();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count < 0)
                {
                    MessageBox.Show("no data to show");
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    textBox1.Text = row["E_id"].ToString();
                    textBox2.Text = (string)row["E_name"];
                    textBox3.Text = (string)row["E_password"];
                    string job = (string)row["E_type"];
                    switch (job)
                    {
                        case "Doctor":
                            radioButton1.Checked = true;
                            break;
                        case "Admin":
                            radioButton3.Checked = true;
                            break;
                        case "Receptionist":
                            radioButton2.Checked = true;
                            break;
                    }
                    button6.Visible = true;
                }
                else
                {
                    MessageBox.Show("data not exists");
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    MessageBox.Show("nothing to update");
                }
                else
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update [Employee] set E_name = '" + textBox2.Text + "', E_password = '" + textBox3.Text + "', E_type = '" + job + "' where E_id = '" + textBox1.Text + "'; select*from [employee] where E_id = '" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    dataGridView1.DataSource = dt;
                    connection.Close();
                    button6.Visible = false;
                }
                
            }
            catch( Exception ex ) { MessageBox.Show(ex.Message); }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
