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
    public class DAL_Menu:DataProvider
    {
        public List<Menu> GetListMenuByTable_DAL(int id)
        {
            
                List<Menu> listMenu = new List<Menu>();

                string query = "SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Drink AS f WHERE bi.idBill = b.IDBill AND bi.idDrink = f.IDDrink AND b.Status=0 AND b.IDTableDrink = " + id;
                DataTable data = DataProvider.Instance.ExecuteQuery(query);

                foreach (DataRow item in data.Rows)
                {
                    Menu menu = new Menu(item);
                    listMenu.Add(menu);
                }

                return listMenu;
            
            /*
            try
            {
                conn.Open();
                List<Menu> listMenu = new List<Menu>();

                DataTable dt = new DataTable();


                string query = "SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Drink AS f WHERE bi.idBill = b.IDBill AND bi.idDrink = f.IDDrink AND b.Status=0 AND b.idTable = " + id;

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }

                foreach (DataRow item in dt.Rows)
                {
                    Menu menu = new Menu(item);
                    listMenu.Add(menu);

                }

                return listMenu;
            }
            finally
            {
                conn.Close();
            }*/
        }
    }
}
