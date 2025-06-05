using System;
using System.Collections;
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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Library Management System\database\Mylibrarydb.mdf"";Integrated Security=True;Connect Timeout=30");
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
            string query = " select * from StudentTbl where Stdid=" + Rtdcb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Rname.Text = dr["StdName"].ToString();
                Rdep.Text = dr["StdDep"].ToString();
                Rphone.Text = dr["StdPhone"].ToString();
            }
            Con.Close();
        }
        private void Fillstudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Stdid from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Stdid", typeof(int));
            dt.Load(rdr);
            Rtdcb.ValueMember = "Stdid";
            Rtdcb.DataSource = dt;
            Con.Close();
        }
        public void populatereturn()
        {
            Con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            Returnbookdgv.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Fillbook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>" + 0 + "", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            Rbook.ValueMember = "BookName";
            Rbook.DataSource = dt;
            Con.Close();
        }
        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populatereturn();
            Fillbook();
            Fillstudent();
            fetchstddata();
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void Updatebook()
        {
            int Qty, newQty;
            Con.Open();
            string query = " select * from BookTbl where BookName='" + Rbook.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty=" + newQty + "where BookName='" + Rbook.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }

        private void Returnbookdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Rnum.Text == "" || Rname.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                string idate = RIdate.Value.Day.ToString() + "/" + RIdate.Value.Month.ToString() + "/" + RIdate.Value.Year.ToString();
                string ridate = RRdate.Value.Day.ToString() + "/" + RRdate.Value.Month.ToString() + "/" + RRdate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ReturnTbl values(" + Rnum.Text + "," + Rtdcb.SelectedItem.ToString() + ",'" + Rname.Text + "','" + Rdep.Text + "','" + Rphone.Text + "','" + Rbook.SelectedValue.ToString() + "','" + idate + "','"+ ridate+"')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Succesfully Returned");
                Con.Close();
                Updatebook();
                populate();
                populatereturn();
            }
        }
        private void Stdcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Rnum.Clear();
            Rname.Clear();
            Rdep.Clear();
            Rphone.Clear();
        }

        private void Rtdcb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Rtdcb_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Rname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
