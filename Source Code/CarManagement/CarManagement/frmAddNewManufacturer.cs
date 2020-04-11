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
    public partial class frmAddNewManufacturer : Form
    {
        public frmAddNewManufacturer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Manufacturer_Name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(Manufacturer_Name))
            {
                MessageBox.Show("Please input Name!");
                return;
            }

            Car_ManufacturerDAO dao = new Car_ManufacturerDAO();
            Car_ManufacturerDTO dto = dao.FindByName(Manufacturer_Name);
            if (dto != null)
            {
                MessageBox.Show("Manufacturer is exist!");
                return;
            }
            else
            {
                bool result = dao.AddNewManufacturer(Manufacturer_Name);
                if (result)
                {
                    MessageBox.Show("Add new Manufacturer successfully!!");
                } else
                {
                    MessageBox.Show("Add new Manufacturer failed!!");
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
