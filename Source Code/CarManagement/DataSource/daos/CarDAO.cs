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
    public class CarDAO
    {
        public List<CarDTO> GetListCar()
        {
            List<CarDTO> list = null;
            string SQL = "SELECT ID, Model_Name, Price, Produced_Year, Accquired_Date, Engine, Quantity, " +
                "Manufacturer_ID, Tranmission_ID, Type_ID, Category_ID, Fuel_ID, Status_ID FROM Cars";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
                    Car_TranmissionDAO car_TranmissionDAO = new Car_TranmissionDAO();
                    Car_TypeDAO car_TypeDAO = new Car_TypeDAO();
                    Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
                    Car_FuelsDAO car_FuelsDAO = new Car_FuelsDAO();
                    Car_StatusDAO car_StatusDAO = new Car_StatusDAO();
                    while (rd.Read())
                    {
                        if (list == null)
                            list = new List<CarDTO>();
                        CarDTO carDTO = new CarDTO
                        {
                            ID = rd.GetInt32(0),
                            Model_Name = rd.GetString(1),
                            Price = rd.GetDouble(2),
                            Produced_Year = rd.GetInt32(3),
                            Accquired_Date = rd.GetDateTime(4),
                            Engine = rd.GetInt32(5),
                            Quantity = rd.GetInt32(6),
                            Manufacturer_Name = car_ManufacturerDAO.GetManufacturerNameByID(rd.GetInt32(7)),
                            Tranmission_Description = car_TranmissionDAO.GetTranmissionDescriptionByID(rd.GetInt32(8)),
                            Type_Description = car_TypeDAO.GetTypeDescriptionByID(rd.GetInt32(9)),
                            Category_Description = car_CategoryDAO.GetCategoryDescriptionByID(rd.GetInt32(10)),
                            Fuel_Description = car_FuelsDAO.GetFuelDescriptionByID(rd.GetInt32(11)),
                            Status_Description = car_StatusDAO.GetStatusDescriptionByID(rd.GetInt32(12))
                        };

                        list.Add(carDTO);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public CarDTO GetCarDetailsByModelName(string model)
        {
            CarDTO result = null;
            string SQL = "SELECT ID, Price, Produced_Year, Engine, Tranmission_ID, Type_ID, " +
                "Category_ID, Fuel_ID FROM Cars";
            if (!string.IsNullOrEmpty(model))
            {
                SQL = SQL + " WHERE Model_Name = @Model_Name";
            }
            else
            {
                return null;
            }
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            if (!string.IsNullOrEmpty(model))
            {
                cmd.Parameters.AddWithValue("@Model_Name", model);
            }

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Car_TranmissionDAO car_TranmissionDAO = new Car_TranmissionDAO();
                    Car_TypeDAO car_TypeDAO = new Car_TypeDAO();
                    Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
                    Car_FuelsDAO car_FuelsDAO = new Car_FuelsDAO();
                    Car_StatusDAO car_StatusDAO = new Car_StatusDAO();
                    if (rd.Read())
                    {
                        result = new CarDTO
                        {
                            ID = rd.GetInt32(0),
                            Price = rd.GetDouble(1),
                            Produced_Year = rd.GetInt32(2),
                            Engine = rd.GetInt32(3),
                            Tranmission_Description = car_TranmissionDAO.GetTranmissionDescriptionByID(rd.GetInt32(4)),
                            Type_Description = car_TypeDAO.GetTypeDescriptionByID(rd.GetInt32(5)),
                            Category_Description = car_CategoryDAO.GetCategoryDescriptionByID(rd.GetInt32(6)),
                            Fuel_Description = car_FuelsDAO.GetFuelDescriptionByID(rd.GetInt32(7))
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

        public bool UpdateQuantity(CarDTO dto, int quantity)
        {
            bool result = false;
            string SQL = "UPDATE Cars SET Quantity = Quantity - @Quantity " +
                "WHERE ID = @ID";

            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
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

        public List<CarDTO> GetListCarModelByManufacturer(string manu)
        {
            List<CarDTO> list = null;
            string SQL = "SELECT Model_Name FROM Cars";
            if (!manu.Equals("All"))
            {
                Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
                int manuID = car_ManufacturerDAO.GetManufacturerIDByName(manu);
                SQL = SQL + " WHERE Manufacturer_ID = " + manuID;
            }
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
                        if (list == null)
                            list = new List<CarDTO>();
                        CarDTO carDTO = new CarDTO
                        {
                            Model_Name = rd.GetString(0)
                        };

                        list.Add(carDTO);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public bool AddNewCar(CarDTO carDTO)
        {
            bool result = false;
            string SQL = "INSERT INTO Cars(Model_Name, Price, Produced_Year, Accquired_Date, " +
                "Engine, Quantity, Manufacturer_ID, Tranmission_ID, Type_ID, Category_ID, Fuel_ID, Status_ID) " +
                "VALUES(@Model_Name, @Price, @Produced_Year, @Accquired_Date, @Engine, @Quantity, " +
                "@Manufacturer_ID, @Tranmission_ID, @Type_ID, @Category_ID, @Fuel_ID, @Status_ID)";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Model_Name", carDTO.Model_Name);
            cmd.Parameters.AddWithValue("@Price", carDTO.Price);
            cmd.Parameters.AddWithValue("@Produced_Year", carDTO.Produced_Year);
            cmd.Parameters.AddWithValue("@Accquired_Date", System.DateTime.Now);
            cmd.Parameters.AddWithValue("@Engine", carDTO.Engine);
            cmd.Parameters.AddWithValue("@Quantity", carDTO.Quantity);

            Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
            int manuID = car_ManufacturerDAO.GetManufacturerIDByName(carDTO.Manufacturer_Name);
            cmd.Parameters.AddWithValue("@Manufacturer_ID", manuID);

            Car_TranmissionDAO car_TranmissionDAO = new Car_TranmissionDAO();
            int tranID = car_TranmissionDAO.GetTranmissionIDByDescription(carDTO.Tranmission_Description);
            cmd.Parameters.AddWithValue("@Tranmission_ID", tranID);

            Car_TypeDAO car_TypeDAO = new Car_TypeDAO();
            int typeID = car_TypeDAO.GetTypeIDByDescription(carDTO.Type_Description);
            cmd.Parameters.AddWithValue("@Type_ID", typeID);

            Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
            int cateID = car_CategoryDAO.GetCategoryIDByDescription(carDTO.Category_Description);
            cmd.Parameters.AddWithValue("@Category_ID", cateID);

            Car_FuelsDAO car_FuelsDAO = new Car_FuelsDAO();
            int fuelID = car_FuelsDAO.GetFuelIDByDescription(carDTO.Fuel_Description);
            cmd.Parameters.AddWithValue("@Fuel_ID", fuelID);

            cmd.Parameters.AddWithValue("@Status_ID", 1);


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

        public int GetNumberAvailableCarByID(int ID)
        {
            int result = 0;
            string SQL = "SELECT Quantity FROM Cars WHERE ID = @ID ";
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

        public bool CheckUsingManufacturer(int manuID)
        {
            bool result = false;
            string SQL = "SELECT ID FROM Cars WHERE Manufacturer_ID = @Manufacturer_ID ";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Manufacturer_ID", manuID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows)
                    {
                        result = true;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateCar(CarDTO carDTO)
        {
            bool result = false;
            string SQL = "UPDATE Cars SET Model_Name = @Model_Name, Price = @Price, Produced_Year = @Produced_Year, " +
                "Engine = @Engine, Quantity = @Quantity, Manufacturer_ID = @Manufacturer_ID, Tranmission_ID = @Tranmission_ID, " +
                "Type_ID = @Type_ID, Category_ID = @Category_ID, Fuel_ID = @Fuel_ID, Status_ID = @Status_ID " +
                "WHERE ID = @ID";

            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Model_Name", carDTO.Model_Name);
            cmd.Parameters.AddWithValue("@Price", carDTO.Price);
            cmd.Parameters.AddWithValue("@Produced_Year", carDTO.Produced_Year);
            cmd.Parameters.AddWithValue("@Engine", carDTO.Engine);
            cmd.Parameters.AddWithValue("@Quantity", carDTO.Quantity);

            Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
            int manuID = car_ManufacturerDAO.GetManufacturerIDByName(carDTO.Manufacturer_Name);
            cmd.Parameters.AddWithValue("@Manufacturer_ID", manuID);

            Car_TranmissionDAO car_TranmissionDAO = new Car_TranmissionDAO();
            int tranID = car_TranmissionDAO.GetTranmissionIDByDescription(carDTO.Tranmission_Description);
            cmd.Parameters.AddWithValue("@Tranmission_ID", tranID);

            Car_TypeDAO car_TypeDAO = new Car_TypeDAO();
            int typeID = car_TypeDAO.GetTypeIDByDescription(carDTO.Type_Description);
            cmd.Parameters.AddWithValue("@Type_ID", typeID);

            Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
            int cateID = car_CategoryDAO.GetCategoryIDByDescription(carDTO.Category_Description);
            cmd.Parameters.AddWithValue("@Category_ID", cateID);

            Car_FuelsDAO car_FuelsDAO = new Car_FuelsDAO();
            int fuelID = car_FuelsDAO.GetFuelIDByDescription(carDTO.Fuel_Description);
            cmd.Parameters.AddWithValue("@Fuel_ID", fuelID);

            Car_StatusDAO car_StatusDAO = new Car_StatusDAO();
            int statusID = car_StatusDAO.GetStatusIDByDescription(carDTO.Status_Description);
            cmd.Parameters.AddWithValue("@Status_ID", statusID);

            cmd.Parameters.AddWithValue("@ID", carDTO.ID);

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

        public List<CarDTO> Search(string manufacturer, string category, string type, double priceFrom, double priceTo)
        {
            List<CarDTO> list = null;

            Car_ManufacturerDAO car_ManufacturerDAO = new Car_ManufacturerDAO();
            Car_TranmissionDAO car_TranmissionDAO = new Car_TranmissionDAO();
            Car_TypeDAO car_TypeDAO = new Car_TypeDAO();
            Car_CategoryDAO car_CategoryDAO = new Car_CategoryDAO();
            Car_FuelsDAO car_FuelsDAO = new Car_FuelsDAO();
            Car_StatusDAO car_StatusDAO = new Car_StatusDAO();

            string SQL = "SELECT ID, Model_Name, Price, Produced_Year, Accquired_Date, Engine, Quantity, " +
                "Manufacturer_ID, Tranmission_ID, Type_ID, Category_ID, Fuel_ID, Status_ID FROM Cars";
            if (!manufacturer.Equals("All"))
            {
                int manuID = car_ManufacturerDAO.GetManufacturerIDByName(manufacturer);
                if (!SQL.Contains("WHERE "))
                {
                    SQL = SQL + " WHERE Manufacturer_ID = " + manuID;
                }
            }

            if (!category.Equals("All"))
            {
                int cateID = car_CategoryDAO.GetCategoryIDByDescription(category);
                if (!SQL.Contains("WHERE "))
                {
                    SQL = SQL + " WHERE Category_ID = " + cateID;
                }
                else
                {
                    SQL = SQL + " AND Category_ID = " + cateID;
                }
            }

            if (!type.Equals("All"))
            {
                int typeID = car_TypeDAO.GetTypeIDByDescription(type);
                if (!SQL.Contains("WHERE "))
                {
                    SQL = SQL + " WHERE Type_ID = " + typeID;
                }
                else
                {
                    SQL = SQL + " AND Type_ID = " + typeID;
                }
            }

            if (!(priceFrom == -1 && priceTo == -1))
            {
                if (priceFrom > -1 && priceTo > -1)
                {
                    if (!SQL.Contains("WHERE "))
                    {
                        SQL = SQL + " WHERE Price BETWEEN " + priceFrom + " AND " + priceTo;
                    }
                    else
                    {
                        SQL = SQL + " AND Price BETWEEN " + priceFrom + " AND " + priceTo;
                    }
                }
                else if (priceFrom > -1 && priceTo == -1)
                {
                    if (!SQL.Contains("WHERE "))
                    {
                        SQL = SQL + " WHERE Price >= " + priceFrom;
                    }
                    else
                    {
                        SQL = SQL + " AND Price >= " + priceFrom;
                    }
                }
                else if (priceFrom == -1 && priceTo > -1)
                {
                    if (!SQL.Contains("WHERE "))
                    {
                        SQL = SQL + " WHERE Price <= " + priceTo;
                    }
                    else
                    {
                        SQL = SQL + " AND Price <= " + priceTo;
                    }
                }
            }

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
                        if (list == null)
                            list = new List<CarDTO>();
                        CarDTO carDTO = new CarDTO
                        {
                            ID = rd.GetInt32(0),
                            Model_Name = rd.GetString(1),
                            Price = rd.GetDouble(2),
                            Produced_Year = rd.GetInt32(3),
                            Accquired_Date = rd.GetDateTime(4),
                            Engine = rd.GetInt32(5),
                            Quantity = rd.GetInt32(6),
                            Manufacturer_Name = car_ManufacturerDAO.GetManufacturerNameByID(rd.GetInt32(7)),
                            Tranmission_Description = car_TranmissionDAO.GetTranmissionDescriptionByID(rd.GetInt32(8)),
                            Type_Description = car_TypeDAO.GetTypeDescriptionByID(rd.GetInt32(9)),
                            Category_Description = car_CategoryDAO.GetCategoryDescriptionByID(rd.GetInt32(10)),
                            Fuel_Description = car_FuelsDAO.GetFuelDescriptionByID(rd.GetInt32(11)),
                            Status_Description = car_StatusDAO.GetStatusDescriptionByID(rd.GetInt32(12))
                        };

                        list.Add(carDTO);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
