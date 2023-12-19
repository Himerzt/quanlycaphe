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
    public class DAL_TableDrink:DataProvider
    {
        public DataTable GetTableDrinkData()
        {
            DataTable dt = new DataTable();

            conn.Open();
            string query = "SELECT t.IDTableDrink, t.IDstatus,t.Name,s.Status FROM TableDrink t, StatusTableDrink s WHERE t.IDstatus=s.IDstatus";
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.Fill(dt);
                conn.Close();

            }

            return dt;
        }

        //Xem danh sách bàn
        public DataTable LoadTableDAL()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select * from TableDrink", conn);
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

        //Thêm bàn
        public bool AddTableDAL(TableDrink ban)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into TableDrink(IDTableDrink, Name, IDstatus) values (@IDTableDrink, @Name, @IDstatus)", conn);
                cmd.Parameters.Add("@IDTableDrink", SqlDbType.Int);
                cmd.Parameters["@IDTableDrink"].Value = ban.IDTableDrink;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = ban.Name;
                cmd.Parameters.Add("@IDstatus", SqlDbType.Int);
                cmd.Parameters["@IDstatus"].Value = ban.IDstatus;
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

        //Update bàn
        public bool UpdateTableDAL(TableDrink ban)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("Update TableDrink set Name = @Name, IDstatus = @IDstatus where IDTableDrink = @IDTableDrink", conn);

                cmd.Parameters.AddWithValue("@IDTableDrink", ban.IDTableDrink);
                cmd.Parameters.AddWithValue("@Name", ban.Name);
                cmd.Parameters.AddWithValue("@IDstatus", ban.IDstatus);

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

        //Xóa bàn
        public bool DeleteTableDAL(int IDTableDrink)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from TableDrink where IDTableDrink = @IDTableDrink", conn);
                cmd.Parameters.Add("@IDTableDrink", SqlDbType.Int).Value = IDTableDrink;
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

        //Lọc
        public DataTable GetStatusByStatusIDDAL(int IDstatus)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM TableDrink WHERE IDstatus = @IDstatus", conn);
                cmd.Parameters.Add("@IDstatus", SqlDbType.Int).Value = IDstatus;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, in ra hoặc ghi log
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
