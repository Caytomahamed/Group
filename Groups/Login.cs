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

namespace Groups
{
    public partial class Login : Form
    {
        // 1. Address of SQL server Database and establish connection
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-259ET9H\\SQLEXPRESS01;Initial Catalog=SocialMedia;Integrated Security=True");

        private void connnectionClose()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

        }

        private void alertSuccessMessage(int i)
        {
            if (i >= 1)
            {
                MessageBox.Show("data saved");

            }
            else
            {
                MessageBox.Show("data not saved");
            }

        }


        public Login()
        {
            InitializeComponent();
        }
        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(username.Text == "")
            {
                MessageBox.Show("username can not be empty ","Input Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                username.Focus();
                return;
            }

            if(password.Text == "")
            {
                MessageBox.Show("password can not be empty","Input Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                password.Focus();
                return;
            }

            login();
        }
        private void login()
        {
            string myname = username.Text;
            string mypassword = password.Text;

            try
            {
                connnectionClose();

                string query = "select * from users where username = '" + myname + "' and password = '" + mypassword + "'";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader dataReader;

                connection.Open();
                dataReader = cmd.ExecuteReader();

                int count = 0;

                while (dataReader.Read())
                {
                    count++;
                }

                if (count == 1)
                {


                    Home hm = new Home();
                    hm.value = username.Text;
                    hm.Show();
                    this.Hide();
                  

                }else if (count > 1)
                {
                    MessageBox.Show("Dublicate records found... Access Denied ");

                }else
                {
                    MessageBox.Show("Username and Password is not Correct... Pls Try again ");
                }
                
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegistor_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            rg.Show();
            this.Hide();

        }
    }
}
