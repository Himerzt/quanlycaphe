using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Login:DataProvider
    {
        public Login KiemTraLogin(Login login)
        {
            conn.Open();

            string query = "SELECT UserName, PassWord, Type, DisplayName FROM Account WHERE UserName = @UserName AND PassWord = @Password";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", login.UserName);
                cmd.Parameters.AddWithValue("@Password", login.Password);


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Login result = new Login
                        {
                            UserName = reader["UserName"].ToString(),
                            Password = reader["PassWord"].ToString(),
                            Type = Convert.ToInt32(reader["Type"]),
                            DisplayName = reader["DisplayName"].ToString()
                        };

                        conn.Close();
                        return result;
                    }
                }
            }

            conn.Close();
            return null;
        }
    }
}
