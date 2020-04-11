using DataSource.daos;
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
    public partial class frmReport : Form
    {
        InvoiceDAO invoiceDAO = new InvoiceDAO();
        public frmReport()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            LoadcbYear();
            cbYear.SelectedItem = int.Parse(DateTime.Now.Year.ToString());
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        }

        private void LoadcbYear()
        {
            int begin = int.Parse(DateTime.Now.Year.ToString()) - 5;
            int end = int.Parse(DateTime.Now.Year.ToString()) + 5;
            for (int i = begin; i <= end; i++)
            {
                cbYear.Items.Add(i);
            }
        }

        private void LoadChart()
        {
            chart.Series["Revenue"].Points.Clear();
            chart.Series["Revenue"].Points.AddXY("Jan", invoiceDAO.GetTotalByMonthAndYear(1 , int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Feb", invoiceDAO.GetTotalByMonthAndYear(2, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Mar", invoiceDAO.GetTotalByMonthAndYear(3, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Apr", invoiceDAO.GetTotalByMonthAndYear(4, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("May", invoiceDAO.GetTotalByMonthAndYear(5, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Jun", invoiceDAO.GetTotalByMonthAndYear(6, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Jul", invoiceDAO.GetTotalByMonthAndYear(7, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Aug", invoiceDAO.GetTotalByMonthAndYear(8, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Sep", invoiceDAO.GetTotalByMonthAndYear(9, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Oct", invoiceDAO.GetTotalByMonthAndYear(10, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Nov", invoiceDAO.GetTotalByMonthAndYear(11, int.Parse(cbYear.SelectedItem.ToString())));
            chart.Series["Revenue"].Points.AddXY("Dec", invoiceDAO.GetTotalByMonthAndYear(12, int.Parse(cbYear.SelectedItem.ToString())));
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }
    }
}
