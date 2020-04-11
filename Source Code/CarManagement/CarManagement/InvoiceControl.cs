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
    public partial class InvoiceControl : UserControl
    {
        InvoiceDAO invoiceDAO = new InvoiceDAO();
        InvoiceDTO invoiceDTO = null;
        public InvoiceControl()
        {
            InitializeComponent();
        }

        private void InvoiceControl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dgvInvoice.DataSource = invoiceDAO.GetListInvoice();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNewInvoice frm = new frmAddNewInvoice();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (invoiceDTO != null)
            {
                frmViewInvoiceDetails frmViewInvoiceDetails = new frmViewInvoiceDetails(invoiceDTO);
                frmViewInvoiceDetails.ShowDialog();
                if (frmViewInvoiceDetails.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvInvoice.Rows[e.RowIndex];
            int id = int.Parse(selectedRow.Cells["ID"].Value.ToString());
            DateTime dateOfPur = DateTime.Parse(selectedRow.Cells["Date_Of_Purcharse"].Value.ToString());
            int cusID = int.Parse(selectedRow.Cells["Customer_ID"].Value.ToString());
            string cusName = selectedRow.Cells["Customer_Name"].Value.ToString();
            double total = double.Parse(selectedRow.Cells["Total"].Value.ToString());
            invoiceDTO = new InvoiceDTO
            {
                ID = id,
                Date_Of_Purcharse = dateOfPur,
                Customer_ID = cusID,
                Customer_Name = cusName,
                Total = total
            };
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.ShowDialog();
            if (frmReport.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}
