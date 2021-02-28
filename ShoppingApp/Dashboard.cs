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
            lblWelcome.Text = "Welcome " + _activeWorker.Fullname;
            FillCategoryComboBox();
            FillMarkaComboBox();
            FillProductDataGridView();
        }
        private void ComponentVisible()
        {
          
            
                nmCount.Enabled = false;
                nmCount.Value = 1;
                lblAmount.Visible = false;
                lblStockCount.Visible = false;
                btnSell.Enabled = false;
            
           
        }
        private void FillProductDataGridView()
        {
            dtgProduct.DataSource = _context.Orders.Where(x => x
            .WorkerId == _activeWorker.Id)
                .Select(x => new
                {
                x.Product.Product_Name,
                x.Price,
                x.Amount,
                x.Purchase_Date,
                x.Product.Marka.Marka_Name,
                x.Product.Category.Category_Name,
                }).ToList();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComponentVisible();
            string category = cmbCategory.Text;
            string marka = cmbMarka.Text;
            cmbProduct.Items.Clear();
            cmbMarka.Items.Clear();
            FillMarkaComboBox();
            cmbProduct.Items.AddRange(_context.Products.Where(x => x.Category.Category_Name.Contains(category) && x.Marka.Marka_Name.Contains(marka)).Select(x => x.Product_Name).ToArray());

        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComponentVisible();
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
                if (selectedProduct.Quantity >0)
                {
                    lblAmount.Text = "Amount: " + selectedProduct.Product_Price + "Azn";
                    lblAmount.Visible = true;
                    nmCount.Maximum = selectedProduct.Quantity;
                    lblStockCount.Visible = true;
                    lblStockCount.Text = selectedProduct.Quantity + " pieces";
                    btnSell.Enabled = true;
                }
                else
                {
                    lblStockCount.Visible = true;
                    lblStockCount.Text = "Out of Stock!!!!!";
                    btnSell.Enabled = false;
                    nmCount.Enabled = false;
                }
               
            }
        }

        private void nmCount_ValueChanged(object sender, EventArgs e)
        {
            lblAmount.Text = "Amount: " + selectedProduct.Product_Price * nmCount.Value + "Azn";
            lblAmount.Visible = true;
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Purchase_Date = DateTime.Now;
            order.WorkerId = _activeWorker.Id;
            order.ProductId = selectedProduct.Id;
            order.Amount = (int)nmCount.Value;
            order.Price = selectedProduct.Product_Price * (int)nmCount.Value;
            selectedProduct.Quantity -= (int)nmCount.Value;
            _context.Orders.Add(order);
            MessageBox.Show($"Product: { selectedProduct.Product_Name} sold successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _context.SaveChanges();
            ComponentVisible();
            FillProductDataGridView();
            cmbCategory.Items.Clear();
            cmbMarka.Items.Clear();
            cmbProduct.Items.Clear();
            FillCategoryComboBox();
            FillMarkaComboBox();
        }
    }
}
