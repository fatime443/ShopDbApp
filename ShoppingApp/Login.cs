using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingApp
{
    public partial class Login : Form
    {
        ShoppingDbEntities _context = new ShoppingDbEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (Utilities.IsEmpty(new string[] {username,password},string.Empty))
            {
                Worker selectedWorker = _context.Workers.FirstOrDefault(w => w.Username == username);
                if (selectedWorker != null)
                {
                    if (selectedWorker.Password == password.HashCode())
                    {
                        lblError.Visible = false;
                        Dashboard dashboard = new Dashboard();
                        dashboard.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        lblError.Text = "Password or Username wrong!";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Username doesn't exist!";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please, fill all field!";
                lblError.Visible = true;
            }
        }
    }
}
