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
    public partial class Book : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Book()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into Books values(@bookname,@bookauthor,@bookpeice)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookname", txtbookname.Text);
                cmd.Parameters.AddWithValue("@bookauthor", txtbookauthor.Text);
                cmd.Parameters.AddWithValue("@bookpeice", Convert.ToInt32(txtbookprice.Text));

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
                string qry = "select * from Books where bookid=@bookid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(txtbookid.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtbookname.Text = dr["bookname"].ToString();
                        txtbookauthor.Text = dr["bookauthor"].ToString();
                        txtbookprice.Text = dr["bookpeice"].ToString();
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
                string query = "update Books set bookname=@bookname,bookauthor=@bookauthor, bookpeice=@bookpeice where bookid=@bookid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookname", txtbookname.Text);
                cmd.Parameters.AddWithValue("@bookauthor", txtbookauthor.Text);
                cmd.Parameters.AddWithValue("@bookpeice", Convert.ToInt32(txtbookprice.Text));
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(txtbookid.Text));
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
                string query = "delete from Books where bookid=@bookid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(txtbookid.Text));
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
                string qry = "select * from Books";
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
