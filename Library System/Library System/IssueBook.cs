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
    public partial class IssueBook : Form
    {
        public IssueBook()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Library Management System\database\Mylibrarydb.mdf"";Integrated Security=True;Connect Timeout=30");
        private void Fillstudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Stdid from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Stdid", typeof(int));
            dt.Load(rdr);
            Stdcb.ValueMember = "Stdid";
            Stdcb.DataSource = dt;
            Con.Close();
        }
        private void Fillbook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>"+0+"", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            Ibook.ValueMember = "BookName";
            Ibook.DataSource = dt;
            Con.Close();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void fetchstddata()
        {
            Con.Open();
            string query = " select * from StudentTbl where Stdid=" + Stdcb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Iname.Text = dr["StdName"].ToString();
                Idep.Text = dr["StdDep"].ToString();
                Iphone.Text = dr["StdPhone"].ToString();
            }
            Con.Close();
        }
        private void Updatebook()
        {
            int Qty , newQty;
            Con.Open();
            string query = " select * from BookTbl where BookName='" + Ibook.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty - 1;
                string query1 = "update BookTbl set Qty=" + newQty + "where BookName='" + Ibook.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }
        private void Updatebookcancellation()
        {
            int Qty, newQty;
            Con.Open();
            string query = " select * from BookTbl where BookName='" + Ibook.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + "where BookName='" + Ibook.SelectedItem.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void IssueBook_Load(object sender, EventArgs e)
        {
            Fillstudent();
            Fillbook();
            populate();
        }

        private void Stdcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }

        private void Stdcb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Inum.Clear();
            Iname.Clear();
            Idep.Clear();
            Iphone.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Inum.Text == "" || Iname.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                string idate = Idate.Value.Day.ToString() + "/" + Idate.Value.Month.ToString() + "/" + Idate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into IssueTbl values(" + Inum.Text + "," + Stdcb.SelectedValue.ToString() + ",'" + Iname.Text + "','" + Idep.Text + "','"+Iphone.Text+"','"+Ibook.SelectedValue.ToString()+ "','"+ idate+ "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Succesfully Issued");
                Con.Close();
                Updatebook();
                populate();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Inum.Text == "")
            {
                MessageBox.Show("Enter the Issue Number");
            }
            else
            {
                Con.Open();
                string query = "delete from IssueTbl where IssueNum = " + Inum.Text + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Issue Successfully Canceled");
                Con.Close();
                Updatebookcancellation();
                populate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Iname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Idep_TextChanged(object sender, EventArgs e)
        {

        }

        private void Iphone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
