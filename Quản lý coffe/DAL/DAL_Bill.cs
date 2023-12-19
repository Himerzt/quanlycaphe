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
    public class DAL_Bill : DataProvider
    {
        public DataTable ThongKeDoanhThu_DAL(DateTime checkIn, DateTime chekOut)
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT t.name AS [Tên bàn], b.total AS [Tổng tiền], b.DateCheckIn AS [Ngày vào], b.DateCheckOut AS [Ngày ra], b.discount AS [Giảm giá], s.Name AS [Nhân viên thanh toán] FROM Bill AS b, TableDrink AS t, Staff AS s WHERE b.DateCheckIn >= @checkIn AND b.DateCheckOut <= @checkOut AND b.status = 1 AND t.idTableDrink = b.idTableDrink AND b.IDStaff = s.IDStaff", conn);

                // Tạo tham số và thêm chúng vào SelectCommand
                da.SelectCommand.Parameters.Add("@checkIn", SqlDbType.DateTime).Value = checkIn;
                da.SelectCommand.Parameters.Add("@checkOut", SqlDbType.DateTime).Value = chekOut;

                DataTable dt = new DataTable();
                da.Fill(dt);
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
        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE IDTableDrink = " + id + " AND status = 0");

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.IDBill;
            }

            return -1;
            

        }
        private static DAL_Bill instance;

        public static DAL_Bill Instance
        {
            get { if (instance == null) instance = new DAL_Bill(); return DAL_Bill.instance; }
            private set { DAL_Bill.instance = value; }
        }

      

       public DAL_Bill() { }
        public void InsertBill_DAL( int idBill, int idTable, int idStaff)
        {
            
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idBill , @idTable , @idStaff", new object[] {idBill,idTable, idStaff });


        }
        public void CheckOut_DAL(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET status = 1, " + "discount = " + discount + ","+"total = "+ totalPrice +", DateCheckOut = GetDate()  WHERE IDBill = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
