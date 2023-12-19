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
    public class BUS_Drink
    {
        DAL_Drink DrinkDAL = new DAL_Drink();
        public DataTable LoadDrink_BUS()
        {
            return DrinkDAL.LoadDrink_DAL();
        }
        public List<DrinkCategory> GetListDrinkCategory()
        {
            List<DrinkCategory> listDrinkCategory = new List<DrinkCategory>();
            DataTable dt = DrinkDAL.GetDrinkCategoryData();

            foreach (DataRow row in dt.Rows)
            {
                DrinkCategory fc = new DrinkCategory(row);
                listDrinkCategory.Add(fc);
            }
            return listDrinkCategory;
        }

        public bool AddDrink_BUS(int id, string name, int idCategory, float price)
        {
            return DrinkDAL.AddDrink_DAL(id, name, idCategory, price);
        }
        public bool UpdateDrink_BUS(int id, string name, int idCategory, float price)
        {
            return DrinkDAL.UpdateDrink_DAL(id, name, idCategory, price);
        }
        public bool DeleteDrink_BUS(int id)
        {
            return DrinkDAL.DeleteDrink_DAL(id);
        }
        public DataTable SearchDrink_BUS(string searchText)
        {
            return DrinkDAL.SearchDrink_DAL(searchText);
        }
    }
}
