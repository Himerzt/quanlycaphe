using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Staff:DataProvider
    {
        //Xem danh sách Staff
        public DataTable LoadStaffDAL()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Staff", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch
            {
                return null;
            }
        }

        //Thêm Staff
        public bool AddStaffDAL(Staff st)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Staff(IDStaff, Name, Phone, BirthDate, StartDate) values (@IDStaff, @Name, @Phone, @BirthDate, @StartDate)", conn);
                cmd.Parameters.Add("@IDStaff", SqlDbType.Int);
                cmd.Parameters["@IDStaff"].Value = st.IDStaff;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = st.Name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar);
                cmd.Parameters["@Phone"].Value = st.Phone;
                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime);
                cmd.Parameters["@BirthDate"].Value = st.BirthDate;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                cmd.Parameters["@StartDate"].Value = st.StartDate;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        //Update Staff
        public bool UpdateStaffDAL(Staff st)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update Staff set Name = @Name, Phone = @Phone, BirthDate = @BirthDate, StartDate = @StartDate where IDStaff = @IDStaff", conn);
                cmd.Parameters.AddWithValue("@IDStaff", st.IDStaff);
                cmd.Parameters.AddWithValue("@Name", st.Name);
                cmd.Parameters.AddWithValue("@Phone", st.Phone);
                cmd.Parameters.AddWithValue("@BirthDate", st.BirthDate);
                cmd.Parameters.AddWithValue("@StartDate", st.StartDate);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        //Xóa Staff
        public bool DeleteTableDAL(int IDStaff)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from Staff where IDStaff = @IDStaff", conn);
                cmd.Parameters.Add("@IDStaff", SqlDbType.Int).Value = IDStaff;
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, in ra hoặc ghi log
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        //Search
        public DataTable SearchStaffByNameDAL(string searchText)
        {
            try
            {
                conn.Open();
                //Tìm theo tên hoặc ID nhân viên
                string query = "SELECT s.IDStaff, s.Name, s.Phone, s.BirthDate, s.StartDate FROM Staff AS s WHERE (s.Name LIKE @SearchText or s.IDStaff LIKE @SearchText)";

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
            { conn.Close(); }
        }
    }
}
