using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Account:DataProvider
    {

        //Xem danh sách tài khoản
        public List<Account> GetTaikhoanDTO()
        {
            List<Account> accounts = new List<Account>();
            conn.Open();
            string query = "SELECT Staff.IDStaff AS IDStaff, Staff.Name, ACCOUNT.UserName, ACCOUNT.DisplayName, Account_Type.NameType, Account_Type.Type " +
                   "FROM Staff " +
                   "LEFT JOIN ACCOUNT ON Staff.IDStaff = ACCOUNT.IDStaff " +
                   "LEFT JOIN Account_Type ON ACCOUNT.Type = Account_Type.type";

            using (SqlCommand cmd = new SqlCommand(query, conn))

            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Account showtaikhoanDTO = new Account
                        {
                            IDStaff = reader["IDStaff"] != DBNull.Value ? Convert.ToInt32(reader["IDStaff"]) : 0,
                            Name = reader["Name"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            DisplayName = reader["DisplayName"].ToString(),
                            NameType = reader["NameType"].ToString(),
                            Type = reader["Type"] != DBNull.Value ? Convert.ToInt32(reader["Type"]) : 0,
                        };

                        accounts.Add(showtaikhoanDTO);
                    }

                }
            }
            conn.Close();
            return accounts;
        }

        //Sửa danh sách tài khoản
        public bool EditAccount(Account editAccountDTO)
        {

            conn.Open();

            string query = @"UPDATE Account SET DisplayName = @DisplayName, Type = @Type
                               WHERE UserName = @UserName; ";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", editAccountDTO.UserName);
                cmd.Parameters.AddWithValue("@DisplayName", editAccountDTO.DisplayName);
                cmd.Parameters.AddWithValue("@Type", editAccountDTO.Type);

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                return rowsAffected > 0;
            }



        }

        //Thêm tài khoản
        public bool AddAccount(Account themTaikhoanDAL)
        {
            conn.Open();
            string query = @"INSERT INTO ACCOUNT (UserName, DisplayName, Type, IDStaff) VALUES (@UserName, @DisplayName, @Type, @IDStaff)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", themTaikhoanDAL.UserName);
                cmd.Parameters.AddWithValue("@DisplayName", themTaikhoanDAL.DisplayName);
                cmd.Parameters.AddWithValue("@Type", themTaikhoanDAL.Type);
                cmd.Parameters.AddWithValue("@IDStaff", themTaikhoanDAL.IDStaff);


                int rowAffected = cmd.ExecuteNonQuery();
                conn.Close();
                return rowAffected > 0;
            }
        }


        //Xóa tài khoản
        public bool DeleteAccount(Account deleteaccountDAL)
        {
            conn.Open();
            string query = @"DELETE FROM ACCOUNT WHERE UserName = @UserName";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", deleteaccountDAL.UserName);

                int rowAffected = cmd.ExecuteNonQuery();
                conn.Close();
                return rowAffected > 0;
            }
        }


        // cài lại mật khẩu
        public bool ResetPassWord(string userName, string newPassword)
        {
            conn.Open();
            string query = @"UPDATE ACCOUNT SET PassWord = @NewPassword WHERE UserName = @UserName";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                int rowAffected = cmd.ExecuteNonQuery();
                conn.Close();
                return rowAffected > 0;
            }
        }


        // Tìm Tài khoản
        public DataTable SearchAccountByNameDAL(string searchText)
        {
            try
            {
                conn.Open();
                // Tìm theo tên, ID nhân viên hoặc loại tài khoản
                string query = @"SELECT Staff.IDStaff AS IDStaff, Staff.Name, ACCOUNT.UserName, ACCOUNT.DisplayName, Account_Type.NameType, Account_Type.Type " +
                    "FROM Staff " +
                    "LEFT JOIN ACCOUNT ON Staff.IDStaff = ACCOUNT.IDStaff " +
                    "LEFT JOIN Account_Type ON ACCOUNT.Type = Account_Type.type " +
                    "WHERE Staff.Name LIKE @SearchText OR ACCOUNT.UserName LIKE @SearchText OR ACCOUNT.DisplayName LIKE @SearchText " +
                    "   OR CONVERT(NVARCHAR(50), Staff.IDStaff) LIKE @SearchText OR Account_Type.NameType LIKE @SearchText";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ hoặc ghi log nếu cần thiết
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
