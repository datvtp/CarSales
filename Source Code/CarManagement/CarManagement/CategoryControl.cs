using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSource.daos;
using DataSource.dtos;

namespace CarManagement
{
    public partial class CategoryControl : UserControl
    {
        Car_CategoryDAO cateDAO = new Car_CategoryDAO();
        Car_CategoryDTO cateDTO = null;
        public CategoryControl()
        {
            InitializeComponent();
        }

        private void CategoryControl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dgvCategory.DataSource = cateDAO.GetListCategory();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewCategory frmAddNewCategory = new frmAddNewCategory();
            frmAddNewCategory.ShowDialog();
            if (frmAddNewCategory.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cateDTO != null)
            {
                frmEditCategory frmEditCategory = new frmEditCategory(cateDTO);
                frmEditCategory.ShowDialog();
                if (frmEditCategory.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }

        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvCategory.Rows[e.RowIndex];
            int id = int.Parse(selectedRow.Cells["ID"].Value.ToString());
            string description = selectedRow.Cells["Description"].Value.ToString();
            cateDTO = new Car_CategoryDTO
            {
                ID = id,
                Description = description
            };
        }
    }
}
