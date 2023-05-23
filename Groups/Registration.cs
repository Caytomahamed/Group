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
using System.IO;

namespace Groups
{
    public partial class Registration : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-259ET9H\\SQLEXPRESS01;Initial Catalog=SocialMedia;Integrated Security=True");
        string imgLocation;
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            // image filters  
            od.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (od.ShowDialog() == DialogResult.OK)
            {
                imgLocation = od.FileName.ToString();
                pictureBox.ImageLocation = imgLocation;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            string username = inputName.Text;
            string email = inputEmail.Text;
            int password = Int32.Parse(InputPassword.Text);
            int phone = Int32.Parse(inputPhone.Text);

            byte[] imageBt = null;
            FileStream fStream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            imageBt = br.ReadBytes((int)fStream.Length);

            connection.Open();
            string qry = "insert into users(username,password,email,phone,picture) values('" + username + "','" + password + "','" + email + "','" + phone + "' ,@imageBt)";
            SqlCommand sc = new SqlCommand(qry, connection);
            sc.Parameters.Add(new SqlParameter("@imageBt", imageBt));
            int i = sc.ExecuteNonQuery();
            if (i >= 1)
            {  
                MessageBox.Show("Your account created ","Ok");
            }
            else
            {
                MessageBox.Show("data not saved");
            }
            connection.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }
    }
}
