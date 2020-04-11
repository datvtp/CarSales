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
    public partial class frmEditCategory : Form
    {
        Car_CategoryDAO cateDAO = new Car_CategoryDAO();
        Car_CategoryDTO cateDTO = null;
        public frmEditCategory(Car_CategoryDTO dto)
        {
            this.cateDTO = dto;
            InitializeComponent();
        }

        private void frmEditCategory_Load(object sender, EventArgs e)
        {
            txtDescription.Text = cateDTO.Description;
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
            Car_CategoryDTO updateDTO = new Car_CategoryDTO
            {
                ID = this.cateDTO.ID,
                Description = txtDescription.Text
            };
            cateDAO.Update(updateDTO);
            MessageBox.Show("Update category successfully.");
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
