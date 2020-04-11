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

namespace CarManagement
{
    public partial class CustomerControl : UserControl
    {
        CustomerDAO cusDAO = new CustomerDAO();
        public CustomerControl()
        {
            InitializeComponent();
        }

        private void CustomerControl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dgvCustomer.DataSource = cusDAO.GetListCustomer();
        }
    }
}
