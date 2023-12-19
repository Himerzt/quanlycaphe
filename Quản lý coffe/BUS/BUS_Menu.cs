using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Menu
    {
        DAL_Menu MenuDAL = new DAL_Menu();

        private static BUS_Menu instance;

        public static BUS_Menu Instance
        {
            get { if (instance == null) instance = new BUS_Menu(); return BUS_Menu.instance; }
            private set { BUS_Menu.instance = value; }
        }

        
        public BUS_Menu() { }

        public List<Menu> GetListMenuByTable_BUS(int id)
        {
            List<Menu> lstMenu = MenuDAL.GetListMenuByTable_DAL(id);
            return lstMenu;
        }
    }
}
