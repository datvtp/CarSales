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
    public partial class frmViewInvoiceDetails : Form
    {
        InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
        InvoiceDTO invoiceDTO = new InvoiceDTO();
        public frmViewInvoiceDetails(InvoiceDTO dto)
        {
            invoiceDTO = dto;
            InitializeComponent();
        }

        private void frmViewInvoiceDetails_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dgvInvoiceDetails.DataSource = invoiceDetailsDAO.GetListInvoiceDetailByInvoiceID(invoiceDTO.ID);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
