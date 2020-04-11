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
    public class CustomerDAO
    {
        public List<CustomerDTO> GetListCustomer()
        {
            List<CustomerDTO> list = null;
            string SQL = "SELECT ID, Fullname, Phone, Email FROM Customers";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            if (list == null)
                                list = new List<CustomerDTO>();
                            CustomerDTO cusDTO = new CustomerDTO
                            {
                                ID = rd.GetInt32(0),
                                Fullname = rd.GetString(1),
                                Phone = rd.GetString(2),
                                Email = rd.GetString(3)
                            };
                            list.Add(cusDTO);
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

        public String FindNameByID(int id)
        {
            string name = null;
            string SQL = "SELECT Fullname FROM Customers WHERE ID = @ID";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows)
                    {
                        if (rd.Read())
                        {
                            name = rd.GetString(0);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return name;
        }

        public CustomerDTO FindByPhone(string phone)
        {
            CustomerDTO result = null;
            string SQL = "SELECT ID, Fullname, Email FROM Customers WHERE Phone = @Phone";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Phone", phone);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows)
                    {
                        if (rd.Read())
                        {
                            result = new CustomerDTO
                            {
                                ID = rd.GetInt32(0),
                                Fullname = rd.GetString(1),
                                Email = rd.GetString(2),
                                Phone = phone
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int CreateCustomer(CustomerDTO cusDTO)
        {
            int result = -1;
            string SQL = "INSERT INTO Customers(Fullname, Phone, Email) VALUES(@Fullname, @Phone, @Email); " +
                "SELECT SCOPE_IDENTITY()";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Fullname", cusDTO.Fullname);
            cmd.Parameters.AddWithValue("@Phone", cusDTO.Phone);
            cmd.Parameters.AddWithValue("@Email", cusDTO.Email);

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
