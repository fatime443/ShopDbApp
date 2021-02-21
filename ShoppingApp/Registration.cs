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
    public partial class Registration : Form
    {
        ShoppingDbEntities _context = new ShoppingDbEntities();
        public Registration()
        {
            InitializeComponent();
        }
        private void ClearAllField()
        {
            txtConPassword.Text = txtFullname.Text = txtPassword.Text = txtUsername.Text = string.Empty;
        }
        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegiter_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string conPassword = txtConPassword.Text;
            string[] array = {fullname, username, password, conPassword};
            if (Utilities.IsEmpty(array,string.Empty))
            {
                Worker selectedworker = _context.Workers.FirstOrDefault(w => w.Username == username);
                if (selectedworker == null)
                {
                    if (password == conPassword)
                    {
                        lblError.Visible = false;
                        Worker worker = new Worker();
                        worker.Fullname = fullname;
                        worker.Username = username;
                        worker.Password = Utilities.HashCode(password);
                        _context.Workers.Add(worker);
                        _context.SaveChanges();
                        ClearAllField();
                        MessageBox.Show($"User {username} registreted successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }
                    else
                    {
                        lblError.Text = "Password or Confirm Password is not the same!";
                        lblError.Visible = true;
                    }

                }
                else
                {
                    lblError.Text = "Username already exist!";
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
