using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_TableDrink
    {
        DAL_TableDrink TableDrinkDAL = new DAL_TableDrink();
        public static int TableWidth = 90;
        public static int TableHeight = 90;
        public List<TableDrink> GetListTableDrink()
        {

            List<TableDrink> listTableDrink = new List<TableDrink>();
            DataTable dt = TableDrinkDAL.GetTableDrinkData();

            foreach (DataRow row in dt.Rows)
            {
                TableDrink tf = new TableDrink(row);
                listTableDrink.Add(tf);
            }
            return listTableDrink;
        }
        //xem danh sách bàn
        public DataTable LoadTableBUS()
        {
            return TableDrinkDAL.LoadTableDAL();
        }

        //Thêm bàn
        public bool AddTableBUS(TableDrink b)
        {
            return TableDrinkDAL.AddTableDAL(b);
        }

        //Update bàn
        public bool UpdateTableBUS(TableDrink b)
        {
            return TableDrinkDAL.UpdateTableDAL(b);
        }

        //Xóa bàn
        public bool DeleteTableBUS(int IDTableFood)
        {
            return TableDrinkDAL.DeleteTableDAL(IDTableFood);
        }

        //Lọc
        public DataTable GetStatusByStatusIDBUS(int IDstatus)
        {
            return TableDrinkDAL.GetStatusByStatusIDDAL(IDstatus);
        }
    }
}
