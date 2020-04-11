using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSource.dtos;
using DataSource.daos;

namespace CarManagement
{
    public partial class manufacturerControl : UserControl
    {
        Car_ManufacturerDAO dao = new Car_ManufacturerDAO();
        public manufacturerControl()
        {
            InitializeComponent();
        }

        private void manufacturerControl_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public void loadData()
        {
            List<Car_ManufacturerDTO> listManufacturers = dao.GetListManufacturer();
            dgvManufacturers.DataSource = listManufacturers;
            if (listManufacturers != null)
            {
                dgvManufacturers.Rows[0].Selected = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewManufacturer frm = new frmAddNewManufacturer();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sID = dgvManufacturers.CurrentRow.Cells[0].Value.ToString();
            string Name = dgvManufacturers.CurrentRow.Cells[1].Value.ToString();
            int ID = -1;
            try
            {
                ID = int.Parse(sID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            Car_ManufacturerDTO dto = new Car_ManufacturerDTO
            {
                ID = ID,
                Name = Name
            };

            frmEditManufacturer frm = new frmEditManufacturer(dto);
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            string sID = dgvManufacturers.CurrentRow.Cells[0].Value.ToString();
            string Name = dgvManufacturers.CurrentRow.Cells[1].Value.ToString();

            DialogResult confirmResult = MessageBox.Show("Do you want to delete Manufacturer: " + Name + "???",
                                     "Confirm Exit!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int ID = -1;
                try
                {
                    ID = int.Parse(sID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }

                Car_ManufacturerDTO dto = new Car_ManufacturerDTO
                {
                    ID = ID,
                    Name = Name
                };

                bool result = dao.DeleteManufacturer(dto);
                if (result)
                {
                    MessageBox.Show("Delete Manufacturer successfully!!");
                    loadData();
                }
                else
                {
                    MessageBox.Show("Delete Manufacturer failed!!");
                }
            }
        }

        private void dgvManufacturers_SelectionChanged(object sender, EventArgs e)
        {
            string sID = dgvManufacturers.CurrentRow.Cells[0].Value.ToString();
            int ID = -1;
            try
            {
                ID = int.Parse(sID);
            }
            catch (Exception ex)
            {
                
            }

            CarDAO carDAO = new CarDAO();
            bool check = carDAO.CheckUsingManufacturer(ID);
            if (check)
            {
                btnDelete.Enabled = false;
            } 
            else
            {
                btnDelete.Enabled = true;
            }
        }
    }
}
