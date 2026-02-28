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
using System.Configuration;

namespace IT1753Project
{
    public partial class Login : Form
    {
        // class variables
        int tries = 0;
        string date = DateTime.Now.ToString();

        // database connection
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\StationeryShop.mdf;Integrated Security = True");

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUser.Text;
                string pass = txtPass.Text;

                SqlCommand com = new SqlCommand("SELECT username, CONVERT(varchar(50), DECRYPTBYPASSPHRASE('key1234', password)) as password FROM Logins WHERE username=@USERNAME and CONVERT(varchar(50), DECRYPTBYPASSPHRASE('key1234', password))=@PASSWORD", con);
                con.Open();
                com.Parameters.AddWithValue("@USERNAME", user);
                com.Parameters.AddWithValue("@PASSWORD", pass);
                SqlDataReader Dr = com.ExecuteReader();
                if (user == "admin" && pass == "password")
                {
                    this.Hide();
                    Main main = new Main(user);
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    tries += 1;
                    MessageBox.Show("Authentication Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

            // closes application after 3 unsuccessful tries
            if (tries == 3)
            {
                this.Close();
            }

        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.textbox = txtUser.Text;
            Properties.Settings.Default.Save();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            txtUser.Text = Properties.Settings.Default.textbox;
            txtPass.Focus();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Down))
            {
                e.Handled = true;
                txtPass.Focus();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                e.Handled = true;
                txtUser.Focus();
            }

            if (e.KeyCode.Equals(Keys.Down))
            {
                e.Handled = true;
                btnLogin.Focus();
            }
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "username")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.White;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "username";
                txtUser.ForeColor = Color.Silver;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "password")
            {
                txtPass.Text = "";
                txtPass.UseSystemPasswordChar = true;
                txtPass.ForeColor = Color.White;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "password";
                txtPass.UseSystemPasswordChar = false;
                txtPass.ForeColor = Color.Silver;
            }
        }
    }
}