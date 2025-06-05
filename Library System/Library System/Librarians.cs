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

namespace Library_System
{
    public partial class Librarians : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Library Management System\database\Mylibrarydb.mdf"";Integrated Security=True;Connect Timeout=30");
        public Librarians()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Librarians_Load(object sender, EventArgs e)
        {
            populate();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrarianDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Libid.Text=="" || LibName.Text=="" || Libpass.Text=="" || Libphone.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into LibrarianTbl values(" + Libid.Text + ",'" + LibName.Text + "','" + Libpass.Text + "','" + Libphone.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Added Succesfully");
                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Libid.Text=="")
            {
                MessageBox.Show("Enter the Librarian Id");
            }
            else
            {
                Con.Open();
                string query = "delete from LibrarianTbl where LibId = " + Libid.Text + ";";
                SqlCommand cmd = new SqlCommand(query,Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Deleted");
                Con.Close() ;
                populate();
            }
        }

        private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Libid.Text == "" || LibName.Text == "" || Libpass.Text == "" || Libphone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open() ;
                string query = "update LibrarianTbl set LibName='" + LibName.Text + "',LibPassword='" + Libpass.Text + "',LibPhone='" + Libphone.Text + "'where Libid =" + Libid.Text + ";";
                SqlCommand cmd = new SqlCommand(query,Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Libid.Clear();
            LibName.Clear();
            Libpass.Clear();
            Libphone.Clear();
        }
    }
}
