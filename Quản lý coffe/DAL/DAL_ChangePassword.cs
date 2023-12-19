using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ChangePassword:DataProvider
    {
        public bool KiemTraMatKhauCu(ChangePassword changePasswordDTO)
        {
            string query = "SELECT COUNT(*) FROM Account  WHERE PassWord = @PassWord AND UserName = @UserName";
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", changePasswordDTO.UserName);
                cmd.Parameters.AddWithValue("@PassWord", changePasswordDTO.OldPassword);

                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                return count > 0;
            }


        }
        public bool CapNhatMatKhau(ChangePassword changePasswordDTO)
        {

            conn.Open();
            string query = "UPDATE Account SET PassWord = @NewPassword WHERE UserName = @UserName AND PassWord = @OldPassword";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NewPassword", changePasswordDTO.NewPassword);
                cmd.Parameters.AddWithValue("@UserName", changePasswordDTO.UserName);
                cmd.Parameters.AddWithValue("@OldPassword", changePasswordDTO.OldPassword);

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }


        }
    }
}
