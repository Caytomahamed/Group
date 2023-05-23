using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Groups
{
    public partial class modify : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-259ET9H\\SQLEXPRESS01;Initial Catalog=SocialMedia;Integrated Security=True");
        string imgLocation = "";
        int id;

        public modify()
        {
            InitializeComponent();
        }

        public modify(string value)
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
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("This data is not available...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            connection.Close();

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                byte[] imageBt = null;
                FileStream fStream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                imageBt = br.ReadBytes((int)fStream.Length);


               // string id = txtstudentid.Text;
                string name = inputName.Text;
                int price = Int32.Parse(inputPrice.Text);
                string date = dateTimePicker1.Text;
      
                connection.Open();
                string qry = "insert into products(name,price,date,picture) values('" + name + "','" + price + "', '" + date + "',@imageBt)";
                SqlCommand sc = new SqlCommand(qry, connection);
                sc.Parameters.Add(new SqlParameter("@imageBt", imageBt));
                int i = sc.ExecuteNonQuery();
                if (i >= 1)
                {
                    MessageBox.Show("data saved");
                    Home hm = new Home();
                    hm.value = value;
                    hm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("data not saved");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void modify_Load(object sender, EventArgs e)
        {
            userinfo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            connection.Open();

            id = Int32.Parse(inputSearch.Text);



            string qry = "select name,price,date,picture from products where id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(qry,connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();

            if (dataReader.HasRows)
            {
                inputName.Text = dataReader[0].ToString();
                inputPrice.Text = dataReader[1].ToString();
                dateTimePicker1.Text = dataReader[2].ToString();

                byte[] image = (byte[])dataReader[3];

                if(image == null)
                {
                    pictureBox.Image = null;
                }else
                {
                    MemoryStream ms = new MemoryStream(image);
                    pictureBox.Image = Image.FromStream(ms);
                }
            }else
            {
                MessageBox.Show("This data is not available...","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }


            byte[] imageBt = null;
          

            string name = inputName.Text;
            int price = Int32.Parse(inputPrice.Text);
            string date = dateTimePicker1.Text;

            connection.Open();
            string qry;
            if (imgLocation.Length > 0)
            {
              FileStream fStream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                imageBt = br.ReadBytes((int)fStream.Length);
                qry = "update products set name = '" + name + "', price = '" + price + "', date= '" + date + "', picture = @image where id = '" + id + "'";
            }else
            {
                qry = "update products set name = '" + name + "', price = '" + price + "', date= '" + date + "' where id = '" + id + "'";
            }


            SqlCommand cmd = new SqlCommand(qry,connection);
            if(imgLocation.Length > 0)
            {
                  cmd.Parameters.AddWithValue("@image", imageBt);
            }

            int i = cmd.ExecuteNonQuery();

            if (i >= 1)
            {
                Home hm = new Home();
                hm.Show();
                hm.value = value;
                this.Hide();
                MessageBox.Show("data updated");

            }
            else
            {
                MessageBox.Show("data not updated");
            }

            connection.Close();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            int id = Int32.Parse(inputSearch.Text);

            connection.Open();
            string qry = "delete from products where id='" + id + "'";
            SqlCommand cmd = new SqlCommand(qry,connection);

            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                Home hm = new Home();
                hm.Show();
                hm.value = value;
                this.Hide();
                MessageBox.Show("data saved");
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

        private void inputName_TextChanged(object sender, EventArgs e)
        {

        }

        private void inputPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            hm.value = value;
            hm.Show();
            this.Hide();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }
    }
}
