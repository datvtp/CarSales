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
    public partial class frmEditCar : Form
    {
        CarDTO car;
        public frmEditCar()
        {
            InitializeComponent();
        }

        public frmEditCar(CarDTO car) : this()
        {
            this.car = car;
        }

        private void frmEditCar_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            txtModel.Text = car.Model_Name;
            txtEngine.Text = car.Engine.ToString();
            txtPrice.Text = car.Price.ToString();
            txtProduceYear.Text = car.Produced_Year.ToString();
            txtQuantity.Text = car.Quantity.ToString();

            Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
            Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
            Car_FuelsDAO car_FuelsDAO = new Car_FuelsDAO();
            Car_TranmissionDAO car_TranmissionDAO = new Car_TranmissionDAO();
            Car_TypeDAO car_TypeDAO = new Car_TypeDAO();
            Car_StatusDAO car_StatusDAO = new Car_StatusDAO();

            List<Car_ManufacturerDTO> car_ManufacturerDTOs = car_ManufacturerDAO.GetListManufacturer();
            cbManufacturer.Items.Clear();
            foreach (Car_ManufacturerDTO manufacturerDTO in car_ManufacturerDTOs)
            {
                cbManufacturer.Items.Add(manufacturerDTO.Name);
            }
            cbManufacturer.SelectedItem = cbManufacturer.Items.OfType<String>().FirstOrDefault(x => x.Equals(car.Manufacturer_Name));


            List<Car_CategoryDTO> car_CategoryDTOs = car_CategoryDAO.GetListCategory();
            cbCategory.Items.Clear();
            foreach (Car_CategoryDTO categoryDTO in car_CategoryDTOs)
            {
                cbCategory.Items.Add(categoryDTO.Description);
            }
            cbCategory.SelectedItem = cbCategory.Items.OfType<String>().FirstOrDefault(x => x.Equals(car.Category_Description));


            List<Car_FuelsDTO> car_Fuels = car_FuelsDAO.GetListFuel();
            cbFuel.Items.Clear();
            foreach (Car_FuelsDTO fuelsDTO in car_Fuels)
            {
                cbFuel.Items.Add(fuelsDTO.Description);
            }
            cbFuel.SelectedItem = cbFuel.Items.OfType<String>().FirstOrDefault(x => x.Equals(car.Fuel_Description));



            List<Car_TranmissionDTO> car_TranmissionDTOs = car_TranmissionDAO.GetListTranmission();
            cbTranmission.Items.Clear();
            foreach (Car_TranmissionDTO tranmissionDTO in car_TranmissionDTOs)
            {
                cbTranmission.Items.Add(tranmissionDTO.Description);
            }
            cbTranmission.SelectedItem = cbTranmission.Items.OfType<String>().FirstOrDefault(x => x.Equals(car.Tranmission_Description));


            List<Car_TypeDTO> car_TypeDTOs = car_TypeDAO.GetListType();
            cbType.Items.Clear();
            foreach (Car_TypeDTO typeDTO in car_TypeDTOs)
            {
                cbType.Items.Add(typeDTO.Description);
            }
            cbType.SelectedItem = cbType.Items.OfType<String>().FirstOrDefault(x => x.Equals(car.Type_Description));


            List<Car_StatusDTO> car_StatusDTOs = car_StatusDAO.GetListStatus();
            cbStatus.Items.Clear();
            foreach (Car_StatusDTO statusDTO in car_StatusDTOs)
            {
                cbStatus.Items.Add(statusDTO.Description);
            }
            cbStatus.SelectedItem = cbStatus.Items.OfType<String>().FirstOrDefault(x => x.Equals(car.Status_Description));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string model = txtModel.Text.Trim();
            string sPrice = txtPrice.Text.Trim();
            string sProducedYear = txtProduceYear.Text.Trim();
            string sEngine = txtEngine.Text.Trim();
            string sQuantity = txtQuantity.Text.Trim();
            string manufacturer = cbManufacturer.Text.Trim();
            string tranmission = cbTranmission.Text.Trim();
            string type = cbType.Text.Trim();
            string category = cbCategory.Text.Trim();
            string fuel = cbFuel.Text.Trim();
            string status = cbStatus.Text.Trim();

            //Check null or empty
            if (string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Please input Model name!");
                return;
            }

            if (string.IsNullOrEmpty(sPrice))
            {
                MessageBox.Show("Please input Price!");
                return;
            }

            if (string.IsNullOrEmpty(sProducedYear))
            {
                MessageBox.Show("Please input Produced Year!");
                return;
            }

            if (string.IsNullOrEmpty(sEngine))
            {
                MessageBox.Show("Please input Engine!");
                return;
            }

            if (string.IsNullOrEmpty(sQuantity))
            {
                MessageBox.Show("Please input Quantity!");
                return;
            }

            if (string.IsNullOrEmpty(manufacturer))
            {
                MessageBox.Show("Please input Manufacturer!");
                return;
            }

            if (string.IsNullOrEmpty(tranmission))
            {
                MessageBox.Show("Please input Tranmission!");
                return;
            }

            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please input Type!");
                return;
            }

            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please input Category!");
                return;
            }

            if (string.IsNullOrEmpty(fuel))
            {
                MessageBox.Show("Please input Fuel!");
                return;
            }

            if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Please input Status!");
                return;
            }

            int Price = -1;
            int ProducedYear = -1;
            int Engine = -1;
            int Quantity = -1;

            try
            {
                Price = int.Parse(sPrice);
            }
            catch
            {
                MessageBox.Show("Please input valid Price!");
                return;
            }

            try
            {
                ProducedYear = int.Parse(sProducedYear);
            }
            catch
            {
                MessageBox.Show("Please input valid Produced Year!");
                return;
            }

            try
            {
                Engine = int.Parse(sEngine);
            }
            catch
            {
                MessageBox.Show("Please input valid Engine!");
                return;
            }

            try
            {
                Quantity = int.Parse(sQuantity);
            }
            catch
            {
                MessageBox.Show("Please input valid Quantity!");
                return;
            }

            if (Price <= 0)
            {
                MessageBox.Show("Price must be greater than 0!");
                return;
            }

            if (ProducedYear <= 0)
            {
                MessageBox.Show("Produced Year must be greater than 0!");
                return;
            }

            if (Engine <= 0)
            {
                MessageBox.Show("Engine must be greater than 0!");
                return;
            }

            if (Quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0!");
                return;
            }

            //string model = txtModel.Text.Trim();
            //string sPrice = txtPrice.Text.Trim();
            //string sProducedYear = txtProduceYear.Text.Trim();
            //string sEngine = txtEngine.Text.Trim();
            //string sQuantity = txtQuantity.Text.Trim();
            //string manufacturer = cbManufacturer.Text.Trim();
            //string tranmission = cbTranmission.Text.Trim();
            //string type = cbType.Text.Trim();
            //string category = cbCategory.Text.Trim();
            //string fuel = cbFuel.Text.Trim();
            //string status = cbStatus.Text.Trim();

            car.Model_Name = model;
            car.Price = Price;
            car.Produced_Year = ProducedYear;
            car.Engine = Engine;
            car.Quantity = Quantity;
            car.Manufacturer_Name = manufacturer;
            car.Tranmission_Description = tranmission;
            car.Type_Description = type;
            car.Category_Description = category;
            car.Fuel_Description = fuel;
            car.Status_Description = status;

            CarDAO carDAO = new CarDAO();
            bool result = carDAO.UpdateCar(car);

            if (result)
            {
                MessageBox.Show("Edit car successfully!");
            }
            else
            {
                MessageBox.Show("Edit car failed!");
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
