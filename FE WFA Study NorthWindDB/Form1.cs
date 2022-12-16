using FE_WFA_Study_NorthWindDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FE_WFA_Study_NorthWindDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _db = new NorthwindEntities();

        }

        NorthwindEntities _db;
        private void ListProductsAndCategories()
        {
            listBox1.DataSource = _db.Products.ToList();
            listBox1.DisplayMember = "ProductName";

            comboBox1.DataSource = _db.Categories.ToList();
            comboBox1.DisplayMember = "CategoryName";

            comboBox1.ValueMember = "CategoryID";

            listBox1.SelectedIndex = comboBox1.SelectedIndex = -1;
            textBox1.Text = textBox2.Text = string.Empty;


        }
        Product _selected;

        bool ControlSelected()
        {
            if(_selected is null)
            {
                MessageBox.Show("UUPPS There has been a mistake");
                return false;
            }
            return true;
        }
        void DeterminateCategoryID(Product item)
        {
            item.CategoryID = comboBox1.SelectedItem != null ? Convert.ToInt32(comboBox1.SelectedValue) : default(int?);
        }
        void CommitAndList()
        {
            _db.SaveChanges();
            ListProductsAndCategories();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListProductsAndCategories();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product
                {
                    ProductName = textBox1.Text,
                    UnitPrice = Convert.ToInt32(textBox2.Text)
                };
                DeterminateCategoryID(p);
                _db.Products.Add(p);
                CommitAndList();
                    
            }
            catch
            {
                MessageBox.Show("UPPS There has been a mistake");
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem !=null)
            {
                _selected = listBox1.SelectedItem as Product;
                textBox1.Text = _selected.ProductName;
                textBox2.Text = _selected.UnitPrice.ToString();
                comboBox1.SelectedValue = _selected.CategoryID != null ? _selected.CategoryID : -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ControlSelected()) return;

            _db.Products.Remove(_selected);
            CommitAndList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ControlSelected()) return;

            try
            {
                _selected.ProductName = textBox1.Text;
                _selected.UnitPrice = Convert.ToDecimal(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("UPPS There has been a mistake");
            }
        }
    }
}
