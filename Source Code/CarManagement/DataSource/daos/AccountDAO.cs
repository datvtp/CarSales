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
    public class AccountDAO
    {
        public AccountDTO CheckLogin(string userID, string password)
        {
            AccountDTO dto = null;
            string SQL = "SELECT UserID, Fullname, Phone, Account_Role_ID " +
                "FROM Accounts WHERE UserID = @UserID AND Password = @Password AND Account_Status_ID = @Account_Status_ID";
            SqlConnection cnn = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Account_Status_ID", 1);


            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.Read())
                    {
                        Account_RoleDAO account_RoleDAO = new Account_RoleDAO();
                        if (dto == null)
                            dto = new AccountDTO
                            {
                                UserID = rd.GetString(0),
                                Fullname = rd.GetString(1),
                                Phone = rd.GetString(2),
                                Account_Role = account_RoleDAO.GetAccountRoleDescriptionByID(rd.GetInt32(3))
                            };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return dto;
        }
    }
}
