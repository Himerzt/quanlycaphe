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
    public class DAL_Payroll : DataProvider
    {
        //Phương thức Load
        public DataTable LoadPayroll_DAL()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT p.IDPayroll, p.IDStaff, s.Name, p.PayrollDate, p.Pay, p.Incentive  FROM Payroll as p, Staff as s WHERE s.IDStaff =p.IDStaff", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();  //giải phóng da để máy chạy nhanh hơn
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
        //Lấy dữ liệu của bảng Staff
        public DataTable GetStaffData()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Staff";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }

                return dt;
            }
            finally
            {
                conn.Close();
            }
        }
        //Lấy dữ liệu của bảng Payroll
       /* public DataTable GetPayrollData()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM Payroll";
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                da.Fill(dt);
            }

            return dt;
        }*/
        //Phương thức Add
        public bool AddPayroll_DAL(Payroll pr)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into Payroll(IDPayroll, IDStaff, PayrollDate, Pay, Incentive ) values (@IDPayroll, @IDStaff, @PayrollDate, @Pay, @Incentive)", conn);
                cmd.Parameters.Add("@IDPayroll", SqlDbType.Int);
                cmd.Parameters.Add("@IDStaff", SqlDbType.Int);
                cmd.Parameters.Add("@PayrollDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@Pay", SqlDbType.Money);
                cmd.Parameters.Add("@Incentive", SqlDbType.Money);

                cmd.Parameters["@IDPayroll"].Value = pr.IDPayroll;
                cmd.Parameters["@IDStaff"].Value = pr.IDStaff;
                cmd.Parameters["@PayrollDate"].Value = pr.PayrollDate;
                cmd.Parameters["@Pay"].Value = pr.Pay;
                cmd.Parameters["@Incentive"].Value = pr.Incentive;

                if (cmd.ExecuteNonQuery() > 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }

        }
        //Phương thức Update
        public bool UpdatePayroll_DAL(Payroll pr)
        {
            string query = "UPDATE Payroll SET IDStaff = @IDStaff, Pay = @Pay, Incentive = @Incentive, PayrollDate = @PayrollDate WHERE IDPayroll = @IDPayroll";

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {                 
                    cmd.Parameters.Add("@IDPayroll", SqlDbType.Int);
                    cmd.Parameters.Add("@IDStaff", SqlDbType.Int);
                    cmd.Parameters.Add("@PayrollDate", SqlDbType.DateTime);
                    cmd.Parameters.Add("@Pay", SqlDbType.Money);
                    cmd.Parameters.Add("@Incentive", SqlDbType.Money);

                    cmd.Parameters["@IDPayroll"].Value = pr.IDPayroll;
                    cmd.Parameters["@IDStaff"].Value = pr.IDStaff;
                    cmd.Parameters["@PayrollDate"].Value = pr.PayrollDate;
                    cmd.Parameters["@Pay"].Value = pr.Pay;
                    cmd.Parameters["@Incentive"].Value = pr.Incentive;
                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        //Phương thức Delete
        public bool DeletePayroll_DAL(int payrollID)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Payroll WHERE IDPayroll = @PayrollID", conn);
                cmd.Parameters.AddWithValue("@PayrollID", payrollID);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        //Phương thức Search
        public DataTable SearchPayroll_DAL(string searchText)
        {
            try
            {
                conn.Open();
                //Tìm theo tên hoặc ID nhân viên hoặc IDPayroll
                string query = "SELECT p.IDPayroll, p.IDStaff, s.Name, p.PayrollDate, p.Pay, p.Incentive FROM Payroll AS p, Staff AS s  WHERE s.IDStaff = p.IDStaff and (s.Name LIKE @SearchText or p.IDStaff LIKE @SearchText or p.IDPayroll LIKE @SearchText)";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);                                        
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            { conn.Close(); }
        }       
    }
}
