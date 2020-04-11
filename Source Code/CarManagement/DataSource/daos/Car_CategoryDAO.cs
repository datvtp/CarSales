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
    public class Car_CategoryDAO
    {
        public int GetCategoryIDByDescription(string Description)
        {
            int result = 0;
            string SQL = "SELECT ID FROM Car_Categories WHERE Description = @Description";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Description", Description);
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

        public string GetCategoryDescriptionByID(int ID)
        {
            string result = "";
            string SQL = "SELECT Description FROM Car_Categories WHERE ID = @ID";
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

        public List<Car_CategoryDTO> GetListCategory()
        {
            List<Car_CategoryDTO> list = null;
            string SQL = "SELECT ID, Description FROM Car_Categories";
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
                                list = new List<Car_CategoryDTO>();
                            Car_CategoryDTO cateDTO = new Car_CategoryDTO
                            {
                                ID = rd.GetInt32(0),
                                Description = rd.GetString(1)
                            };

                            list.Add(cateDTO);
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


        public void Insert(string description)
        {
            string SQL = "INSERT INTO Car_Categories VALUES (@Description)";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Description", description);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        public void Update(Car_CategoryDTO cateDTO)
        {
            string SQL = "UPDATE Car_Categories SET Description = @Description WHERE ID = @ID";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Description", cateDTO.Description);
            cmd.Parameters.AddWithValue("@ID", cateDTO.ID);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        //public List<Car_CategoryDTO> GetListCategory()
        //{
        //    List<Car_CategoryDTO> result = null;
        //    string SQL = "SELECT ID, Description FROM Car_Categories";
        //    SqlConnection cnn = DBUtils.GetConnection();
        //    SqlCommand cmd = new SqlCommand(SQL, cnn);
        //    try
        //    {
        //        if (cnn.State == ConnectionState.Closed)
        //        {
        //            cnn.Open();
        //            SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //            while (rd.Read())
        //            {
        //                if (result == null)
        //                    result = new List<Car_CategoryDTO>();
        //                Car_CategoryDTO dto = new Car_CategoryDTO
        //                {
        //                    ID = rd.GetInt32(0),
        //                    Description = rd.GetString(1)
        //                };
        //                result.Add(dto);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return result;
        //}
    }
}
