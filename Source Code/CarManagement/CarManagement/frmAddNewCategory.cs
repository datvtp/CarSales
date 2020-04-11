using DataSource.daos;
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
    public partial class frmAddNewCategory : Form
    {
        Car_CategoryDAO cateDAO = new Car_CategoryDAO();
        public frmAddNewCategory()
        {
            InitializeComponent();
        }

        private void frmAddNewCategory_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description can't be blank.");
                return;
            }
            if (txtDescription.Text.Length > 100)
            {
                MessageBox.Show("Description must be <= 100 chars.");
                return;
            }
            cateDAO.Insert(txtDescription.Text);
            MessageBox.Show("Add new category successfully.");
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
