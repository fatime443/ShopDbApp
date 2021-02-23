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
    public partial class Dashboard : Form
    {
        private readonly Worker _activeWorker;
        ShoppingDbEntities _context = new ShoppingDbEntities();
        private Product selectedProduct;
        public Dashboard(Worker worker)
        {
            _activeWorker = worker;
            InitializeComponent();
        }
        private void FillCategoryComboBox()
        {
            string marka = cmbMarka.Text;
            cmbCategory.Items.AddRange(_context.Marka_to_Category.Where(x => x.Marka.Marka_Name.Contains(marka)).Select(x => x.Category.Category_Name).Distinct().ToArray());

        }
        private void FillMarkaComboBox()
        {
            string category = cmbCategory.Text;
            cmbMarka.Items.AddRange(_context.Marka_to_Category.Where(x => x.Category.Category_Name.Contains(category)).Select(x => x.Marka.Marka_Name).Distinct().ToArray());
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Location = new Point((lblWelcome.Parent.ClientSize.Width - lblWelcome.ClientSize.Width) / 2-300,20);
            lblWelcome.Text = "Welcome " + _activeWorker.Fullname;
            FillCategoryComboBox();
            FillMarkaComboBox();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            nmCount.Enabled = false;
            nmCount.Value = 1;
            lblAmount.Visible = false;
            string category = cmbCategory.Text;
            string marka = cmbMarka.Text;
            cmbProduct.Items.Clear();
            cmbMarka.Items.Clear();
            FillMarkaComboBox();
            cmbProduct.Items.AddRange(_context.Products.Where(x => x.Category.Category_Name.Contains(category) && x.Marka.Marka_Name.Contains(marka)).Select(x => x.Product_Name).ToArray());

        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            nmCount.Enabled = false;
            nmCount.Value = 1;
            lblAmount.Visible = false;
            string marka = cmbMarka.Text;
            string category = cmbCategory.Text;
            cmbProduct.Items.Clear();
            cmbProduct.Items.AddRange(_context.Products.Where(x => x.Marka.Marka_Name.Contains(marka) && x.Category.Category_Name.Contains(category)).Select(x => x.Product_Name).ToArray());
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            nmCount.Enabled = true;
            string product = cmbProduct.Text;
            selectedProduct = _context.Products.FirstOrDefault(x => x.Product_Name == product);
            if (selectedProduct != null)
            {
                lblAmount.Text = "Amount: " + selectedProduct.Product_Price + "Azn";
                lblAmount.Visible = true;
            }
        }

        private void nmCount_ValueChanged(object sender, EventArgs e)
        {
            string product = cmbProduct.Text;
            selectedProduct = _context.Products.FirstOrDefault(x => x.Product_Name == product);
            lblAmount.Text = "Amount: " + selectedProduct.Product_Price * nmCount.Value + "Azn";
            lblAmount.Visible = true;
        }
    }
}
