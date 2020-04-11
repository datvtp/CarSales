using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Infrastructure;
using DataSource.daos;
using DataSource.dtos;

namespace CarManagement
{
    public partial class carControl : UserControl
    {
        CarDAO carDAO = new CarDAO();

        public carControl()
        {
            InitializeComponent();
        }

        public void CarControl_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {
            List<CarDTO> listCars = carDAO.GetListCar();
            dgvCars.DataSource = listCars;
            if (listCars != null)
            {
                dgvCars.Rows[0].Selected = true;
            }

            Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
            Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
            Car_TypeDAO car_TypeDAO = new Car_TypeDAO();

            List<Car_ManufacturerDTO> car_ManufacturerDTOs = car_ManufacturerDAO.GetListManufacturer();
            cbManufacturer.Items.Clear();
            cbManufacturer.Items.Add("All");
            foreach (Car_ManufacturerDTO manufacturerDTO in car_ManufacturerDTOs)
            {
                cbManufacturer.Items.Add(manufacturerDTO.Name);
            }
            cbManufacturer.SelectedIndex = 0;

            List<Car_CategoryDTO> car_CategoryDTOs = car_CategoryDAO.GetListCategory();
            cbCategory.Items.Clear();
            cbCategory.Items.Add("All");
            foreach (Car_CategoryDTO categoryDTO in car_CategoryDTOs)
            {
                cbCategory.Items.Add(categoryDTO.Description);
            }
            cbCategory.SelectedIndex = 0;

            List<Car_TypeDTO> car_TypeDTOs = car_TypeDAO.GetListType();
            cbType.Items.Clear();
            cbType.Items.Add("All");
            foreach (Car_TypeDTO typeDTO in car_TypeDTOs)
            {
                cbType.Items.Add(typeDTO.Description);
            }
            cbType.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewCar frmAddNew = new frmAddNewCar();
            DialogResult result = frmAddNew.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sID = dgvCars.CurrentRow.Cells[0].Value.ToString();
            string Model = dgvCars.CurrentRow.Cells[1].Value.ToString();
            string sPrice = dgvCars.CurrentRow.Cells[2].Value.ToString();
            string sProducedYear = dgvCars.CurrentRow.Cells[3].Value.ToString();
            string sEngine = dgvCars.CurrentRow.Cells[5].Value.ToString();
            string sQuantity = dgvCars.CurrentRow.Cells[6].Value.ToString();
            string Manufacturer = dgvCars.CurrentRow.Cells[7].Value.ToString();
            string Tranmission = dgvCars.CurrentRow.Cells[8].Value.ToString();
            string Type = dgvCars.CurrentRow.Cells[9].Value.ToString();
            string Category = dgvCars.CurrentRow.Cells[10].Value.ToString();
            string Fuel = dgvCars.CurrentRow.Cells[11].Value.ToString();
            string Status = dgvCars.CurrentRow.Cells[12].Value.ToString();

            int ID = -1;
            double Price = -1;
            int ProducedYear = -1;
            int Engine = -1;
            int Quantity = -1;
            try
            {
                ID = int.Parse(sID);
                Price = double.Parse(sPrice);
                ProducedYear = int.Parse(sProducedYear);
                Engine = int.Parse(sEngine);
                Quantity = int.Parse(sQuantity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }

            CarDTO carDTO = new CarDTO
            {
                ID = ID,
                Price = Price,
                Produced_Year = ProducedYear,
                Engine = Engine,
                Quantity = Quantity,
                Model_Name = Model,
                Manufacturer_Name = Manufacturer,
                Tranmission_Description = Tranmission,
                Type_Description = Type,
                Category_Description = Category,
                Fuel_Description = Fuel,
                Status_Description = Status
            };



            frmEditCar frmEditCar = new frmEditCar(carDTO);
            DialogResult result = frmEditCar.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadData();
            }
        }

        private void search()
        {
            string manufacturer = cbManufacturer.Text.Trim();
            string category = cbCategory.Text.Trim();
            string type = cbType.Text.Trim();
            string sPriceFrom = txtPriceFrom.Text.Trim();
            string sPriceTo = txtPriceTo.Text.Trim();

            double PriceFrom = -1;
            double PriceTo = -1;

            if (!string.IsNullOrEmpty(sPriceFrom))
            {
                try
                {
                    PriceFrom = double.Parse(sPriceFrom);
                } 
                catch (Exception)
                {
                    MessageBox.Show("Please input valid Price From!");
                    return;
                }

                if (PriceFrom < 0)
                {
                    MessageBox.Show("Price must be greater or equal 0!");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(sPriceTo))
            {
                try
                {
                    PriceTo = double.Parse(sPriceTo);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please input valid Price To!");
                    return;
                }

                if (PriceTo < 0)
                {
                    MessageBox.Show("Price must be greater or equal 0!");
                    return;
                }
            }

            CarDAO carDAO = new CarDAO();
            List<CarDTO> list = carDAO.Search(manufacturer, category, type, PriceFrom, PriceTo);
            dgvCars.DataSource = list;
            if (list != null)
            {
                dgvCars.Rows[0].Selected = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
    }
}
