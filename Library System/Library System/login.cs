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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Library Management System\database\Mylibrarydb.mdf"";Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from LibrarianTbl where LibName = '"+Unamelogin.Text+"'and LibPassword='"+Passlogin.Text+"'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString()=="1")
            {
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }
            Con.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void Unamelogin_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void Unamelogin_MouseClick(object sender, MouseEventArgs e)
        {
            if (Unamelogin.Text == "Username")
            {
                Unamelogin.Clear();
            }
        }

        private void Passlogin_MouseClick(object sender, MouseEventArgs e)
        {
            if (Passlogin.Text == "Password")
            {
                Passlogin.Clear();
                Passlogin.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Unamelogin.Clear();
            Passlogin.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
