using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Drink
    {
        int iDDrink;
        string name;
        int iDDrinkCategory;
        float price;
        private DataRow item;

        public Drink(DataRow item)
        {
            this.item = item;
            this.IDDrink = Convert.ToInt32(item["IDDrink"]);
            this.Name = item["Name"].ToString();
            this.IDDrinkCategory = Convert.ToInt32(item["IDDrinkCategory"]);
            this.Price = (float)Convert.ToDouble(item["Price"]);
        }

        public int IDDrink { get => iDDrink; set => iDDrink = value; }
        public string Name { get => name; set => name = value; }
        public int IDDrinkCategory { get => iDDrinkCategory; set => iDDrinkCategory = value; }
        public float Price { get => price; set => price = value; }
    }
}
