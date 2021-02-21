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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pcImage_Click(object sender, EventArgs e)
        {
            pcImage.Location = new Point((pcImage.Parent.ClientSize.Width - pcImage.ClientSize.Width) / 2,
               (pcImage.Parent.ClientSize.Height - pcImage.ClientSize.Height) / 2
                );
        }

        private void pcClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }
    }
}
