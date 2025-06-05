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
    public partial class Student : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Library Management System\database\Mylibrarydb.mdf"";Integrated Security=True;Connect Timeout=30");
        public Student()
        {
            InitializeComponent();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from StudentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Stdid.Text == "" || StdName.Text == "" || StdPhone.Text == "" || StdSem.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into StudentTbl values(" + Stdid.Text + ",'" + StdName.Text + "','" + StdDep.Text + "','" + StdSem.SelectedItem.ToString()+"','"+ StdPhone.Text +"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Succesfully");
                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Stdid.Text == "")
            {
                MessageBox.Show("Enter the Student Id");
            }
            else
            {
                Con.Open();
                string query = "delete from StudentTbl where Stdid = " + Stdid.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Stdid.Text == "" || StdName.Text == "" || StdPhone.Text == "" || StdSem.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update StudentTbl set StdName='" + StdName.Text + "',StdDep='" + StdDep.Text + "',StdSem=" + StdSem.SelectedItem.ToString() + ",StdPhone='" + StdPhone.Text + "' where Stdid =" + Stdid.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Stdid.Clear();
            StdName.Clear();
            StdPhone.Clear();
            StdDep.Clear();
        }
    }
}
