using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Billinfo
    {
        int iDBill;
        int iDDrink;
        int count;
        private DataRow item;

        public int IDBill { get => iDBill; set => iDBill = value; }
        public int IDDrink { get => iDDrink; set => iDDrink = value; }
        public int Count { get => count; set => count = value; }
        public Billinfo() { }
        public Billinfo(int iDBill, int iDDrink, int count)
        {
            this.IDBill = iDBill;
            this.IDDrink = iDDrink;
            this.Count = count;
        }

        public Billinfo(DataRow item)
        {
            this.item = item;
        }
    }
}
