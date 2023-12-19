using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Menu
    {
        string name;
        float totalPrice;
        int count;
        float price;
        private DataRow item;

     
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public string Name { get => name; set => name = value; }

        public Menu(string drinkName, float totalPrice, int count, float price)
        {
            this.Name = drinkName;
            this.TotalPrice = totalPrice;
            this.Count = count;
            this.Price = price;
           
        }

        public Menu(DataRow item)
        {
            this.item = item;
            this.Name = item["Name"].ToString();
            this.TotalPrice = (float)Convert.ToDouble(item["totalPrice"]);
            this.Count = Convert.ToInt32(item["count"]);
            this.Price = (float)Convert.ToDouble(item["price"]);
        }
    }
}
