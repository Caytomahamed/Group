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
    public partial class Home : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-259ET9H\\SQLEXPRESS01;Initial Catalog=SocialMedia;Integrated Security=True");

        public Home()
        {
            InitializeComponent();
            showData();
        }

        public Home(string value)
        {
            InitializeComponent();
            this.value = value;
        }
        public string value { get; set; }
        private void userinfo()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            string qry = "select username,picture from users where username = '" + value + "'";

            connection.Open();

            SqlCommand cmd = new SqlCommand(qry, connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();


            if (dataReader.HasRows)
            {
                userlabel.Text = dataReader[1].ToString();

              
                byte[] image = (byte[])dataReader[1];

                if (image == null)
                {
                    userImage.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    userImage.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("This data is not available...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            connection.Close();

        }

        public void showData()
        {
            string qry = "select id,name,price,date,picture from products";
            SqlDataAdapter sda = new SqlDataAdapter(qry, connection);

            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();

                byte[] imageData = (byte[])dr[4];
                MemoryStream ms = new MemoryStream(imageData);
                Image image = Image.FromStream(ms);

                dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = image;

            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            userinfo();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

     

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label1_Click_3(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void modify_Click(object sender, EventArgs e)
        {
            modify md = new modify();
            md.value = this.value;
            md.Show();
            this.Hide();
        }
    }
}
