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

namespace DataCommsWinforms
{
    public partial class Login : Form
    {
        // class variables
        int tries = 0;
        string date = DateTime.Now.ToString();
        string strConnectionString = ConfigurationManager.ConnectionStrings["DataCommsDBConnection"].ConnectionString;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnectionString);
            try
            {               
                string user = txtUser.Text;
                string pass = txtPass.Text;
                string user_id;
                string house_id;

                SqlDataAdapter login_sda = new SqlDataAdapter("SELECT * FROM Login WHERE Username='" + user + "' AND Password='" + pass + "'", con);
                DataTable login_dt = new DataTable();
                login_sda.Fill(login_dt);

                if (login_dt.Rows.Count == 1)
                {
                    user_id = login_dt.Rows[0]["Id"].ToString();
                    house_id = login_dt.Rows[0]["House_Id"].ToString();

                    if (login_dt.Rows[0]["Account_Type"].ToString() == "Admin")
                    {
                        MessageBox.Show("Admins, please use webform to login");
                    }
                    else
                    {
                        this.Hide();
                        Form1 main = new Form1(user_id, house_id);
                        main.ShowDialog();
                        this.Close();
                    }                   
                }
                else
                {
                    tries += 1;
                    MessageBox.Show("Authentication Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }

            // closes application after 3 unsuccessful tries
            if (tries == 3)
            {
                this.Close();
            }

        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Properties.Settings.Default.textbox = txtUser.Text;
            Properties.Settings.Default.Save();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            //txtUser.Text = Properties.Settings.Default.textbox;
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