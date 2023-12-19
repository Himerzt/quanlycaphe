using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_cbbTypeAccount:DataProvider
    {

        public DataTable GetAccountTypes()
        {
            DataTable dataTable = new DataTable();

            conn.Open();
            string query = "SELECT * FROM Account_Type";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            conn.Close();
            return dataTable;
        }

    }
}
