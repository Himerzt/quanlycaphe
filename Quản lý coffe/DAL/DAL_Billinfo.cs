using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class DAL_Billinfo:DataProvider
    {
        public List<Billinfo> GetListBillInfo(int id)
        {
            List<Billinfo> listBillInfo = new List<Billinfo>();

            DataTable data = new DataTable();
               SqlDataAdapter da= new SqlDataAdapter ("SELECT * FROM Billinfo WHERE idBill = " + id, conn);

            foreach (DataRow item in data.Rows)
            {
                Billinfo info = new Billinfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }
        public void InsertBillInfo_DAL(int idBill, int idDrink, int count)
        {
           /* try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Billinfo(IDBill,IDDrink,Count) values (@IDBill, @IDDrink, @Count)", conn);
                cmd.Parameters.Add("@IDBill", SqlDbType.Int);
                cmd.Parameters.Add("@IDDrink", SqlDbType.Int);
                cmd.Parameters.Add("@Count", SqlDbType.Int);

                cmd.Parameters["@IDBill"].Value = idBill;
                cmd.Parameters["@IDDrink"].Value = idDrink;               
                cmd.Parameters["@Count"].Value = count;
              //  cmd.Parameters["@isExitsBillInfo"].Value = ; 
                
                if (cmd.ExecuteNonQuery() > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }*/
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idDrink , @count", new object[] { idBill, idDrink, count });

        }
        
    }

}
