using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAL
{
    public class DAL_Drink:DataProvider
    {
        public DataTable LoadDrink_DAL()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select f.IDDrink, f.Name, fc.Name AS [NameCategory], f.Price From Drink AS f , DrinkCategory AS fc Where f.IDDrinkCategory =fc.IDDrinkCategory", conn);

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
        public bool AddDrink_DAL(int id, string name, int idCategory, float price)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Drink(IDDrink,Name,IDDrinkCategory,Price) values (@IDDrink, @Name, @IDDrinkCategory, @Price)", conn);
                cmd.Parameters.Add("@IDDrink", SqlDbType.Int);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@IDDrinkCategory", SqlDbType.Int);
                cmd.Parameters.Add("@Price", SqlDbType.Float);


                cmd.Parameters["@IDDrink"].Value = id;
                cmd.Parameters["@Name"].Value = name;
                cmd.Parameters["@IDDrinkCategory"].Value = idCategory;
                cmd.Parameters["@Price"].Value = price;

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

        public bool UpdateDrink_DAL(int id, string name, int idCategory, float price)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update Drink Set Name=@Name,  IDDrinkCategory=@IDDrinkCategory, Price=@Price Where IDDrink=@IDDrink ", conn);

                cmd.Parameters.Add("@IDDrink", SqlDbType.Int);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@IDDrinkCategory", SqlDbType.Int);
                cmd.Parameters.Add("@Price", SqlDbType.Float);


                cmd.Parameters["@IDDrink"].Value = id;
                cmd.Parameters["@Name"].Value = name;
                cmd.Parameters["@IDDrinkCategory"].Value = idCategory;
                cmd.Parameters["@Price"].Value = price;

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

        public bool DeleteDrink_DAL(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Drink WHERE IDDrink=@IDDrink", conn);
                cmd.Parameters.Add("@IDDrink", SqlDbType.Int);



                cmd.Parameters["@IDDrink"].Value = id;

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

        public DataTable SearchDrink_DAL(string searchText)
        {
            try
            {
                conn.Open();
                //Tìm theo ID hoặc tên đồ uống, hoặc tên danh mục đồ uống
                string query = "Select f.IDDrink, f.Name, fc.Name AS [NameCategory], f.Price From Drink AS f, DrinkCategory AS fc Where f.IDDrinkCategory=fc.IDDrinkCategory and (f.Name LIKE @SearchText or f.IDDrink LIKE @SearchText or fc.Name LIKE @SearchText)";

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
    }
}
