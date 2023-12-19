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
    public class BUS_DrinkCategory
    {
        DAL_DrinkCategory DrinkCategoryDAL = new DAL_DrinkCategory();
        public DataTable LoadDrinkCategogy_BUS()
        {
            return DrinkCategoryDAL.LoadDrinkCategogy_DAL();
        }
        public bool AddDrinkCategogy_BUS(int id, string name)
        {
            return DrinkCategoryDAL.AddDrinkCategogy_DAL(id, name);
        }
        public bool UpdateDrinkCategogy_BUS(int id, string name)
        {
            return DrinkCategoryDAL.UpdateDrinkCategory_DAL(id, name);
        }

        public bool DeleteDrinkCategogy_BUS(int id)
        {
            return DrinkCategoryDAL.DeleteDrinkCategory_DAL(id);
        }
        public DataTable SearchDrinkCategory_BUS (string searchText)
        {
            return DrinkCategoryDAL.SearchDrinkCategory_DAL(searchText);
        }
        public List<DrinkCategory> GetListDrinkCategory()
        {
            List<DrinkCategory> listDrinkCategory = new List<DrinkCategory>();
            DataTable dt = DrinkCategoryDAL.GetDrinkCategoryData();

            foreach (DataRow row in dt.Rows)
            {
                DrinkCategory fc = new DrinkCategory(row);
                listDrinkCategory.Add(fc);
            }
            return listDrinkCategory;
        }
        public List<Drink> GetDrinkByCategoryID_BUS(int id)
        {
            List<Drink> listDrink = DrinkCategoryDAL.GetDrinkByCategoryID_DAL(id);
            return listDrink;

        }
    }
}
