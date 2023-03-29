using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Net.NetworkInformation;

namespace ConnectedDemo
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;


        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString);

        }

        private void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
                string query = "insert into Product1 values(@prodname,@prodcomapany,@prodprice)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@prodname", txtname.Text);
                cmd.Parameters.AddWithValue("@prodcomapany", txtcompany.Text);
                cmd.Parameters.AddWithValue("@prodprice", Convert.ToInt32(txtprice.Text));
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
                string qry = "select * from Product1 where prodid=@prodid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@prodid", Convert.ToInt32(txtid.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtname.Text = dr["prodname"].ToString();
                        txtcompany.Text = dr["prodcomapany"].ToString();
                        txtprice.Text = dr["prodprice"].ToString();
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
                string query = "update Product1 set prodname=@prodname,prodcomapany=@prodcomapany, prodprice=@prodprice where prodid=@prodid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@prodname", txtname.Text);
                cmd.Parameters.AddWithValue("@prodcomapany", txtcompany.Text);
                cmd.Parameters.AddWithValue("@prodprice", Convert.ToInt32(txtprice.Text));
                cmd.Parameters.AddWithValue("@prodid", Convert.ToInt32(txtid.Text));
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
                string query = "delete from Product1 where prodid=@prodid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@prodid", Convert.ToInt32(txtid.Text));
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
                string qry = "select * from Product1";
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
