using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_DrinkCategory:DataProvider
    {

        //Phương thức Load
        public DataTable LoadDrinkCategogy_DAL()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from DrinkCategory", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();  //giải phóng da để máy chạy nhanh hơn
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool AddDrinkCategogy_DAL(int id, string name)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into DrinkCategory(IDDrinkCategory, Name) values (@IDDrinkCategory, @Name)", conn);
                cmd.Parameters.Add("@IDDrinkCategory", SqlDbType.Int);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
             

                cmd.Parameters["@IDDrinkCategory"].Value = id;
                cmd.Parameters["@Name"].Value = name;
              

                if (cmd.ExecuteNonQuery() > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }
        }
        public bool UpdateDrinkCategory_DAL(int id, string name)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update DrinkCategory Set IDDrinkCategory=@IDDrinkCategory, Name=@Name Where IDDrinkCategory=@IDDrinkCategory ", conn);
                cmd.Parameters.Add("@IDDrinkCategory", SqlDbType.Int);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);


                cmd.Parameters["@IDDrinkCategory"].Value = id;
                cmd.Parameters["@Name"].Value = name;


                if (cmd.ExecuteNonQuery() > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }
        }
        
        public bool DeleteDrinkCategory_DAL(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DrinkCategory WHERE IDDrinkCategory=@IDDrinkCategory", conn);
                cmd.Parameters.Add("@IDDrinkCategory", SqlDbType.Int);
            


                cmd.Parameters["@IDDrinkCategory"].Value = id;
                
                return cmd.ExecuteNonQuery() > 0;
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
       
        public DataTable SearchDrinkCategory_DAL(string searchText)
        {
            try
            {
                conn.Open();
                //Tìm theo ID hoạc tên
                string query = "SELECT IDDrinkCategory, Name FROM DrinkCategory  WHERE Name LIKE @SearchText or IDDrinkCategory LIKE @SearchText";

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
                return null;
            }
            finally
            { conn.Close(); }
        }
        public DataTable GetDrinkCategoryData()
        {
            try
            {

                DataTable dt = new DataTable();

                conn.Open();
                string query = "SELECT * FROM DrinkCategory";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }

                return dt;
            }
            finally
            { conn.Close(); }
        }
        public List<Drink> GetDrinkByCategoryID_DAL(int id)
        {
            List<Drink> list = new List<Drink>();
            DataTable dt = new DataTable();
            string query = "select * from Drink where IDDrinkCategory = " + id;
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.Fill(dt);
            }
            foreach (DataRow item in dt.Rows)
            {
                Drink Drink = new Drink(item);
                list.Add(Drink);
            }

            return list;
        }
    }
}
