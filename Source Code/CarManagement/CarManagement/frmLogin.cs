using DataSource.daos;
using DataSource.dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarManagement
{
    public partial class frmLogin : Form
    {
        AccountDAO accountDAO = new AccountDAO();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string UserID = txtUserID.Text.Trim();
            string Password = txtPassword.Text;

            if (string.IsNullOrEmpty(UserID))
            {
                MessageBox.Show("Please input Username!");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please input Password!");
                return;
            }

            AccountDTO account = accountDAO.CheckLogin(UserID, Password);
            if (account != null)
            {
                frmHome frmHome = new frmHome(account);
                frmHome.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            closeApp();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeApp();
        }

        private void closeApp()
        {
            DialogResult confirmResult = MessageBox.Show("Do you want to exit program ???",
                                     "Confirm Exit!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }
        }

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
