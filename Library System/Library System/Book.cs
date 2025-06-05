using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_System
{
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Library Management System\database\Mylibrarydb.mdf"";Integrated Security=True;Connect Timeout=30");
        private void AddBook_Load(object sender, EventArgs e)
        {
            populate();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Bookname.Text == "" || Author.Text == "" || Publisher.Text == "" || Price.Text == "" || Qty.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BookTbl values('" + Bookname.Text + "','" + Author.Text + "','" + Publisher.Text + "','" + Price.Text + "'," + Qty.Text + ")", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Added Succesfully");
                Con.Close();
                populate();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Bookname.Text == "")
            {
                MessageBox.Show("Enter the Book Name");
            }
            else
            {
                Con.Open();
                string query = "delete from BookTbl where BookName = '" + Bookname.Text + "';";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Bookname.Text == "" || Author.Text == "" || Publisher.Text == "" || Price.Text == "" || Qty.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update BookTbl set Author='" + Author.Text + "',Publisher='" + Publisher.Text + "',Price=" + Price.Text + ",Qty='" + Qty.Text +"' where BookName ='" + Bookname.Text + "';";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bookname.Clear();
            Author.Clear();
            Publisher.Clear();
            Price.Clear();
            Qty.Clear();
        }
    }
}
