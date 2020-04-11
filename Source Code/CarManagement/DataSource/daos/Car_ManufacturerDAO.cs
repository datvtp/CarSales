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
    public class Car_ManufacturerDAO
    {
        public int GetManufacturerIDByName(string Name)
        {
            int result = 0;
            string SQL = "SELECT ID FROM Car_Manufacturers WHERE Name = @Name";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Name", Name);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.Read())
                    {
                        result = rd.GetInt32(0);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateManufacturer(Car_ManufacturerDTO dto)
        {
            bool result = false;
            string SQL = "UPDATE Car_Manufacturers SET Name = @Name WHERE ID = @ID";

            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Name", dto.Name);
            cmd.Parameters.AddWithValue("@ID", dto.ID);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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

        public bool AddNewManufacturer(string manufacturer_Name)
        {
            bool result = false;
            string SQL = "INSERT INTO Car_Manufacturers(Name) VALUES (@Name)";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Name", manufacturer_Name);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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

        public bool DeleteManufacturer(Car_ManufacturerDTO dto)
        {
            bool result = false;
            string SQL = "DELETE Car_Manufacturers WHERE ID = @ID";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", dto.ID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                result = cmd.ExecuteNonQuery() > 0;
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

        public string GetManufacturerNameByID(int ID)
        {
            string result = "";
            string SQL = "SELECT Name FROM Car_Manufacturers WHERE ID = @ID";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", ID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.Read())
                    {
                        result = rd.GetString(0);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public List<Car_ManufacturerDTO> GetListManufacturer()
        {
            List<Car_ManufacturerDTO> result = null;
            string SQL = "SELECT ID, Name FROM Car_Manufacturers";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rd.Read())
                    {
                        if (result == null)
                            result = new List<Car_ManufacturerDTO>();
                        Car_ManufacturerDTO dto = new Car_ManufacturerDTO
                        {
                            ID = rd.GetInt32(0),
                            Name = rd.GetString(1)
                        };
                        result.Add(dto);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Car_ManufacturerDTO FindByName(string Name)
        {
            Car_ManufacturerDTO result = null;
            string SQL = "SELECT ID, Name FROM Car_Manufacturers WHERE Name = @Name";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Name", Name);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.Read())
                    {
                        result = new Car_ManufacturerDTO
                        {
                            ID = rd.GetInt32(0),
                            Name = rd.GetString(1)
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
