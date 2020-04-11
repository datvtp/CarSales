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
    public partial class frmEditManufacturer : Form
    {
        Car_ManufacturerDTO dto;
        public frmEditManufacturer()
        {
            InitializeComponent();
        }

        public frmEditManufacturer(Car_ManufacturerDTO dto) : this()
        {
            this.dto = dto;
        }

        private void frmEditManufacturer_Load(object sender, EventArgs e)
        {
            txtName.Text = dto.Name;
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
            dto.Name = Manufacturer_Name;

            bool result = dao.UpdateManufacturer(dto);
            if (result)
            {
                MessageBox.Show("Update Manufacturer successfully!!");
            }
            else
            {
                MessageBox.Show("Update Manufacturer failed!!");
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
