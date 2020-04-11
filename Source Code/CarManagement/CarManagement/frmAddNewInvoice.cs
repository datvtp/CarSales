using DataSource.daos;
using DataSource.dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarManagement
{
    public partial class frmAddNewInvoice : Form
    {
        CustomerDTO cusDTO;
        CarDTO carDTO;
        public frmAddNewInvoice()
        {
            InitializeComponent();
        }

        private void frmAddNewInvoice_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();

            List<Car_ManufacturerDTO> car_ManufacturerDTOs = car_ManufacturerDAO.GetListManufacturer();
            cbManufacturer.Items.Clear();
            cbManufacturer.Items.Add("All");
            foreach (Car_ManufacturerDTO manufacturerDTO in car_ManufacturerDTOs)
            {
                cbManufacturer.Items.Add(manufacturerDTO.Name);
            }
            cbManufacturer.SelectedIndex = 0;
        }

        private void cbManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string manu = cbManufacturer.Text.Trim();
            CarDAO carDAO = new CarDAO();
            List<CarDTO> carDTOs = carDAO.GetListCarModelByManufacturer(manu);
            cbModel.Items.Clear();
            if (carDTOs != null)
            {
                foreach (CarDTO carDTO in carDTOs)
                {
                    cbModel.Items.Add(carDTO.Model_Name);
                }
                cbModel.SelectedIndex = 0;
            }
            else
            {
                cbModel_SelectedIndexChanged(sender, e);
            }
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string model = cbModel.Text.Trim();
            CarDAO carDAO = new CarDAO();
            carDTO = carDAO.GetCarDetailsByModelName(model);
            if (carDTO != null)
            {
                txtUnitPrice.Clear();
                txtProduceYear.Clear();
                txtEngine.Clear();
                txtTranmission.Clear();
                txtType.Clear();
                txtCategory.Clear();
                txtFuel.Clear();
                txtQuantity.Clear();

                txtUnitPrice.Text = carDTO.Price.ToString();
                txtProduceYear.Text = carDTO.Produced_Year.ToString();
                txtEngine.Text = carDTO.Engine.ToString();
                txtTranmission.Text = carDTO.Tranmission_Description;
                txtType.Text = carDTO.Type_Description;
                txtCategory.Text = carDTO.Category_Description;
                txtFuel.Text = carDTO.Fuel_Description;
                txtQuantity.Text = "0";
                lbTotal.Text = "Total: " + 0;
                txtQuantity.ReadOnly = false;
            }
            else
            {
                txtUnitPrice.Clear();
                txtProduceYear.Clear();
                txtEngine.Clear();
                txtTranmission.Clear();
                txtType.Clear();
                txtCategory.Clear();
                txtFuel.Clear();
                txtQuantity.Clear();
                txtQuantity.Text = "0";
                txtQuantity.ReadOnly = true;
                lbTotal.Text = "Total: " + 0;
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            string phone = txtPhone.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please input phone number!");
                txtPhone.Focus();
                return;
            }
            else
            {
                bool result = validatePhoneNumber(phone);
                if (result)
                {
                    CustomerDAO cusDAO = new CustomerDAO();
                    cusDTO = cusDAO.FindByPhone(phone);
                    if (cusDTO != null)
                    {
                        txtFullname.Text = cusDTO.Fullname;
                        txtFullname.ReadOnly = true;

                        txtEmail.Text = cusDTO.Email;
                        txtEmail.ReadOnly = true;
                    }
                    else
                    {
                        txtFullname.Clear();
                        txtFullname.ReadOnly = false;

                        txtEmail.Clear();
                        txtEmail.ReadOnly = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please input follow: \n(xxx)xxxxxxx \n(xxx) xxxxxxx " +
                        "\n(xxx)xxx - xxxx \n(xxx) xxx - xxxx \nxxxxxxxxxx \nxxx - xxx - xxxxx");
                    return;
                }
            }
        }

        private static bool validatePhoneNumber(String phoneNo)
        {
            string strRegex1 = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            Regex re1 = new Regex(strRegex1);

            if (re1.IsMatch(phoneNo))
                return true;
            return false;
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            string model = cbModel.Text.Trim();
            string sQuantity = txtQuantity.Text.Trim();
            if (string.IsNullOrEmpty(sQuantity))
            {
                MessageBox.Show("Please input Quantity!");
                return;
            }
            int Quantity = -1;
            try
            {
                Quantity = int.Parse(sQuantity);
            }
            catch
            {
                MessageBox.Show("Quantity must be an integer number!");
                return;
            }
            if (Quantity <= 0 && !string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Quantity must be greater than 0!");
                return;
            }

            string sUnitPrice = txtUnitPrice.Text;
            double UnitPrice = double.Parse(sUnitPrice);

            if (carDTO != null)
            {
                lbTotal.Text = "Total: " + (Quantity * UnitPrice);
            }
            else
                lbTotal.Text = "Total: " + 0;
        }

        private void txtQuantity_MouseClick(object sender, MouseEventArgs e)
        {
            string model = cbModel.Text.Trim();
            if (string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Please choose an model!");
                cbModel.Focus();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (carDTO == null)
            {
                MessageBox.Show("Please choose a model!");
                return;
            }

            string sQuantity = txtQuantity.Text.Trim();
            int Quantity = int.Parse(sQuantity);

            if (Quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0!");
                return;
            }

            CarDAO carDAO = new CarDAO();
            int numberAvailable = carDAO.GetNumberAvailableCarByID(carDTO.ID);
            if (numberAvailable - Quantity < 0)
            {
                MessageBox.Show("Quantity is out of stock!");
                return;
            }

            string Fullname = txtFullname.Text.Trim();
            string Email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(Fullname) || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please complete customer's info!");
                return;
            }

            int insertCusResult = -1;
            if (cusDTO == null)
            {
                cusDTO = new CustomerDTO
                {
                    Fullname = Fullname,
                    Email = Email,
                    Phone = txtPhone.Text
                };

                CustomerDAO cusDAO = new CustomerDAO();
                insertCusResult = cusDAO.CreateCustomer(cusDTO);
                if (insertCusResult == -1)
                {
                    MessageBox.Show("Insert new customer failed!");
                    return;
                }
            }
            else
            {
                insertCusResult = cusDTO.ID;
            }

            InvoiceDAO invoiceDAO = new InvoiceDAO();
            int invoiceID = invoiceDAO.CreateNewInvoice(insertCusResult);

            InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
            bool createInvoiceDetails = invoiceDetailsDAO.CreateInvoiceDetails(carDTO, invoiceID, Quantity);
            if (createInvoiceDetails)
            {
                MessageBox.Show("Add new invoice successfully!");
                cusDTO = null;
            }
            else
            {
                MessageBox.Show("Add new invoice failed!");
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string Email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please input Email!");
                txtEmail.Focus();
                return;
            }

            bool result = validateEmail(Email);
            if (!result)
            {
                MessageBox.Show("Please input valid Email!");
                txtEmail.Focus();
                return;
            }
        }

        private bool validateEmail(string Email)
        {
            try
            {
                MailAddress m = new MailAddress(Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
