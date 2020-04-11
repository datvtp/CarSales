using DataSource.dtos;
using DataSource.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.daos
{
    public class InvoiceDAO
    {
        public List<InvoiceDTO> GetListInvoice()
        {
            List<InvoiceDTO> list = null;
            string SQL = "SELECT ID, Date_Of_Purcharse, Customer_ID FROM Invoices";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    CustomerDAO cusDAO = new CustomerDAO();
                    InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            if (list == null)
                                list = new List<InvoiceDTO>();
                            InvoiceDTO invoiceDTO = new InvoiceDTO
                            {
                                ID = rd.GetInt32(0),
                                Date_Of_Purcharse = rd.GetDateTime(1),
                                Customer_ID = rd.GetInt32(2),
                            };
                            invoiceDTO.Customer_Name = cusDAO.FindNameByID(invoiceDTO.Customer_ID);
                            invoiceDTO.Total = invoiceDetailsDAO.GetTotalPriceOfInvoice(invoiceDTO.ID);
                            list.Add(invoiceDTO);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }


        public double GetTotalByMonthAndYear(int month, int year)
        {
            double total = 0;
            string SQL = "SELECT ID FROM Invoices WHERE MONTH(Date_Of_Purcharse) = @Month AND YEAR(Date_Of_Purcharse) = @Year";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Year", year);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    CustomerDAO cusDAO = new CustomerDAO();
                    InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAO();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            InvoiceDTO invoiceDTO = new InvoiceDTO
                            {
                                ID = rd.GetInt32(0)
                            };
                            total += invoiceDetailsDAO.GetTotalPriceOfInvoice(invoiceDTO.ID);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return total;
        }

        public int CreateNewInvoice(int cusID)
        {
            int result = -1;
            string SQL = "INSERT INTO Invoices(Date_Of_Purcharse, Customer_ID) VALUES(@Date_Of_Purcharse, @Customer_ID); " +
                "SELECT SCOPE_IDENTITY()";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Date_Of_Purcharse", System.DateTime.Now);
            cmd.Parameters.AddWithValue("@Customer_ID", cusID);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
    }
    
}
