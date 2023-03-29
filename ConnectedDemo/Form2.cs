using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ConnectedDemo
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into Employee1 values(@empname,@empcity,@empdepartment)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empname", txtempname.Text);
                cmd.Parameters.AddWithValue("@empcity", txtempcity.Text);
                cmd.Parameters.AddWithValue("@empdepartment", txtempdept.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from Employee1 where empid=@empid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(txtempid.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtempname.Text = dr["empname"].ToString();
                        txtempcity.Text = dr["empcity"].ToString();
                        txtempdept.Text = dr["empdepartment"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update Employee1 set empname=@empname,empcity=@empcity, empdepartment=@empdepartment where empid=@empid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empname", txtempname.Text);
                cmd.Parameters.AddWithValue("@empcity", txtempcity.Text);
                cmd.Parameters.AddWithValue("@empdepartment", txtempdept.Text);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(txtempid.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record updated..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "delete from Employee1 where empid=@empid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(txtempid.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record deleted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee1";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dr);
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
