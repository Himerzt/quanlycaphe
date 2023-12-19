using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DrinkCategory
    {
        int iDDrinkCategory;
        string name;
        public DataRow row;

        
        public DrinkCategory(DataRow row)
        {
            this.row = row;
            IDDrinkCategory = Convert.ToInt32(row["IDDrinkCategory"]);
            Name = Convert.ToString(row["Name"]);
        }

        public int IDDrinkCategory { get => iDDrinkCategory; set => iDDrinkCategory = value; }
        public string Name { get => name; set => name = value; }
    }
}
