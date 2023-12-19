using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_coffe
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            bll = new BUS_Account();

        }
        BUS_Bill BillBUS = new BUS_Bill();
        BUS_Payroll PayrollBUS = new BUS_Payroll();
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDateTimePickerBill();
            LoadStaffByID();
           
            LoadDrinkCategoryByID();
            dtgvStaff.DataSource = stafflist;
            dtgvTable.DataSource = statuslist;
            
            LoadStatus();
            LoadStatus1();
            AddTableBinding();
            AddStaffBinding();
            LoadStaff();
            LoadTable();

        }

        //
        public event EventHandler DataChanged;
        private void SomeMethodThatChangesData()
        {
            // Code that updates or changes data

            // Trigger the event
            OnDataChanged(EventArgs.Empty);
        }

        // Method to raise the event
        protected virtual void OnDataChanged(EventArgs e)
        {
            DataChanged?.Invoke(this, e);
        }

        //
        //Thống kê doanh thu
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void ThongKeDoanhThu(DateTime checkIn, DateTime checkOut)
        {
            dtgvDoanhThu.DataSource = BillBUS.ThongKeDoanhThu_BUS(checkIn, checkOut);
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            ThongKeDoanhThu(dtpkFromDate.Value, dtpkToDate.Value);
        }

        //////Payroll
        public void LoadPayroll()
        {
            dtgvLuong.DataSource = PayrollBUS.LoadPayroll_BUS();
           
        }

        private void btnLoadPayroll_Click(object sender, EventArgs e)
        {
            LoadPayroll();
            btnAddPayroll.Enabled = true;
            txtIDPayroll.Enabled = true;
        }

        private void LoadStaffByID()
        {
            List<Staff> listStaff = PayrollBUS.GetListStaff();

            cbbIDStaff.DisplayMember = "Name";
            cbbIDStaff.ValueMember = "IDStaff"; 
            cbbIDStaff.DataSource = listStaff;
        }     

        private void btnAddPayroll_Click(object sender, EventArgs e)
        {
            Payroll payroll_DTO = new Payroll();

            payroll_DTO.IDPayroll = int.Parse(txtIDPayroll.Text);
            payroll_DTO.IDStaff = int.Parse(cbbIDStaff.SelectedValue.ToString());
            payroll_DTO.PayrollDate = dtpkPayrollDate.Value;
            payroll_DTO.Pay = decimal.Parse(txtPay.Text);
            payroll_DTO.Incentive = decimal.Parse(txtIncentive.Text);
            if (PayrollBUS.AddPayroll_BUS(payroll_DTO))
            {
                MessageBox.Show($"Thêm lương cho nhân viên {cbbIDStaff.SelectedValue.ToString()} thành công");
                LoadPayroll();
                
            }
            else MessageBox.Show($"Thêm lương cho nhân viên {cbbIDStaff.SelectedValue.ToString()} thất bại");
        }

        private void dtgvLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtgvLuong.Rows.Count)
            {
                DataGridViewRow selectedRow = dtgvLuong.Rows[e.RowIndex];

                // Assuming the columns are named "ID", "IDStaff", "PayrollDate", "Pay", "Incentive"
                txtIDPayroll.Text = selectedRow.Cells["IDPayroll"].Value.ToString();
                cbbIDStaff.Text = selectedRow.Cells["Name"].Value.ToString();
                dtpkPayrollDate.Value = Convert.ToDateTime(selectedRow.Cells["PayrollDate"].Value);
                txtPay.Text = selectedRow.Cells["Pay"].Value.ToString();
                txtIncentive.Text = selectedRow.Cells["Incentive"].Value.ToString();
                btnAddPayroll.Enabled = false;
                txtIDPayroll.Enabled = false;

            }

        }

        private void btnUpdatePayroll_Click(object sender, EventArgs e)
        {
          
              // Get data from controls
              int idPayroll = int.Parse(txtIDPayroll.Text);
              int idStaff = (int)cbbIDStaff.SelectedValue;
              DateTime payrollDate = dtpkPayrollDate.Value;
              decimal pay = decimal.Parse(txtPay.Text);
              decimal incentive = decimal.Parse(txtIncentive.Text);

              // Create a Payroll object with the updated data
              Payroll updatedPayroll = new Payroll()
              {
                  IDPayroll = idPayroll,
                  IDStaff = idStaff,
                  PayrollDate = payrollDate,
                  Pay = pay,
                  Incentive = incentive
              };
              // Update the database with the modified payroll data
              if (PayrollBUS.UpdatePayroll_BUS(updatedPayroll))
              {
                  MessageBox.Show($"Cập nhật lương có ID {idPayroll} thành công");
                  LoadPayroll(); 
                  btnAddPayroll.Enabled = true;
                  txtIDPayroll.Enabled = true;

            }
            else
              {
                  MessageBox.Show("Cập nhật lương thất bại");
              }     
        }

        private void btnDeletePayroll_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDPayroll.Text);
            if (MessageBox.Show($"Bạn có chắc chắn xóa lương có ID: {id} hay không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (PayrollBUS.DeletePayroll_BUS(id))
                {

                    MessageBox.Show($"Xóa thông tin lương có ID {id} thành công.");
                    LoadPayroll();
                }
                else
                {
                    MessageBox.Show($"Xóa thông tin lương có ID {id} thất bại.");
                }
            }         

        }

        private void btnSearchPayroll_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearchPayroll.Text.Trim();
            DataTable dt = PayrollBUS.SearchPayroll_BUS(searchKeyword);

            if (dt != null)
            {
                dtgvLuong.DataSource = dt;
            }
            else
            {
                
                MessageBox.Show("Không tải được dữ liệu.");
            }

        }      

        //DrinkCategory

        BUS_DrinkCategory DrinkCategoryBUS = new BUS_DrinkCategory();
        public void LoadDrinkCategory()
        {
            dtgvDrinkCategory.DataSource = DrinkCategoryBUS.LoadDrinkCategogy_BUS();
        }

        private void btnShowDrinkCategory_Click(object sender, EventArgs e)
        {
            LoadDrinkCategory();
            txbDrinkCategoryID.Enabled = true;
        }

        private void btnAddDrinkCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbDrinkCategoryID.Text);
            string name = txbDrinkCategoryName.Text;
            if (DrinkCategoryBUS.AddDrinkCategogy_BUS(id, name))
            {
                MessageBox.Show("Thêm danh mục đồ uống thành công");
                LoadDrinkCategory();
                LoadDrinkCategoryByID();

            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục đồ uống");
            }
        }

        private void dtgvDrinkCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtgvDrinkCategory.Rows.Count)
            {
                DataGridViewRow s = dtgvDrinkCategory.Rows[e.RowIndex];

                txbDrinkCategoryID.Text = s.Cells["IDDrinkCategory"].Value.ToString();
                txbDrinkCategoryName.Text = s.Cells["Name"].Value.ToString();
              
                
            }

        }

        private void btnEditDrinkCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbDrinkCategoryID.Text);
            string name = txbDrinkCategoryName.Text;
            if (DrinkCategoryBUS.UpdateDrinkCategogy_BUS(id, name))
            {
                MessageBox.Show("Cập nhật danh mục đồ uống thành công");
                LoadDrinkCategory();
                LoadDrinkCategoryByID();
                txbDrinkCategoryID.Enabled = true;

            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật danh mục đồ uống");
            }
        }

        private void btnDeleteDrinkCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbDrinkCategoryID.Text);
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa doanh mục đồ uống có ID :{id} hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
            {
                if (DrinkCategoryBUS.DeleteDrinkCategogy_BUS(id))
                {
                    MessageBox.Show($"Xóa danh mục đồ uống có ID {id} thành công.");
                    LoadDrinkCategory();
                    LoadDrinkCategoryByID();
                }
                else
                {
                    MessageBox.Show($"Xóa danh mục đồ uống có ID {id} thất bại.");
                }
            }    
           
                
        }

        private void btnSearchDrinkCategory_Click(object sender, EventArgs e)
        {
            string searchKeyword = txbSearchDrinkCategory.Text.Trim();
            DataTable dt = DrinkCategoryBUS.SearchDrinkCategory_BUS(searchKeyword);

            if (dt != null)
            {
                dtgvDrinkCategory.DataSource = dt;
            }
            else
            {
                
                MessageBox.Show("Không tải được dữ liệu .");
            }
        }
        
        //Drink
        BUS_Drink DrinkBUS = new BUS_Drink();
        void LoadDrink()
        {
            dtgvDrink.DataSource = DrinkBUS.LoadDrink_BUS();
        }
        private void btnShowDrink_Click(object sender, EventArgs e)
        {
            LoadDrink();
            txbDrinkID.Enabled = true;

        }

        private void dtgvDrink_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < dtgvDrink.Rows.Count)
            {
                DataGridViewRow s = dtgvDrink.Rows[e.RowIndex];

                txbDrinkID.Text = s.Cells["IDDrink"].Value.ToString();
                txbDrinkName.Text = s.Cells["Name"].Value.ToString();
                cbDrinkCategory.Text = s.Cells["NameCategory"].Value.ToString();
                nmDrinkPrice.Value = int.Parse(s.Cells["Price"].Value.ToString());                

            }
        }

       public void LoadDrinkCategoryByID()
        {
            List<DrinkCategory> fc = DrinkBUS.GetListDrinkCategory();
            cbDrinkCategory.DataSource = fc;
            cbDrinkCategory.ValueMember = "IDDrinkCategory";
            cbDrinkCategory.DisplayMember = "Name";
        }

        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbDrinkID.Text);
            string name = txbDrinkName.Text;
            int idCategory = int.Parse(cbDrinkCategory.SelectedValue.ToString());
            float price = float.Parse(nmDrinkPrice.Value.ToString());
            if(DrinkBUS.AddDrink_BUS(id, name, idCategory, price))
            {
                MessageBox.Show("Thêm đồ uống thành công");
                LoadDrink();
                txbDrinkID.Enabled = true;
            }
            else
            {
                MessageBox.Show("Thêm đồ uống thất bại");
            }
        }

        private void btnEditDrink_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbDrinkID.Text);
            string name = txbDrinkName.Text;
            int idCategory = int.Parse(cbDrinkCategory.SelectedValue.ToString());
            float price = float.Parse(nmDrinkPrice.Value.ToString());
            if (DrinkBUS.UpdateDrink_BUS(id, name, idCategory, price))
            {
                MessageBox.Show("Cập nhật đồ uống thành công");
                LoadDrink();
                txbDrinkID.Enabled = true;
            }
            else
            {
                MessageBox.Show("Cập nhật đồ uống thất bại");
            }
        }

        private void btnDeleteDrink_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbDrinkID.Text);
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa doanh mục đồ uống có ID :{id} hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (DrinkBUS.DeleteDrink_BUS(id))
                {
                    MessageBox.Show($"Xóa danh mục đồ uống có ID {id} thành công.");
                    LoadDrink();
                }
                else
                {
                    MessageBox.Show($"Xóa danh mục đồ uống có ID {id} thất bại.");
                }
            }
        }

        private void btnSearchDrink_Click(object sender, EventArgs e)
        {
            string searchKeyword = txbSearchDrinkName.Text;
            DataTable dt = DrinkBUS.SearchDrink_BUS(searchKeyword);

            if (dt != null)
            {
                dtgvDrink.DataSource = dt;
            }
            else
            {

                MessageBox.Show("Không tải được dữ liệu .");
            }
        }

        //Tài khoản

        private BUS_Account bll;
        private void SetupComboBoxDataSource()
        {
            // lấy dữ liệu từ bảng Account_Type table
            DataTable dataTable = new BUS_cbbTypeAccount().GetAccountTypes();

            cbAccountType.DataSource = dataTable;
            cbAccountType.DisplayMember = "NameType";
            cbAccountType.ValueMember = "Type";

        }

        private void dtgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dtgvAccount.Rows[e.RowIndex];

                cbAccountType.SelectedValue = selectedRow.Cells["Type"].Value?.ToString();
                txtUserName.Text = selectedRow.Cells["UserName"].Value.ToString();
                txtDisplayName.Text = selectedRow.Cells["DisplayName"].Value.ToString();
                txtStaffID.Text = selectedRow.Cells["IDStaff"].Value.ToString();
            }
        }
        // Xem danh sách tài khoản
        private void btnShowAccount_Click(object sender, EventArgs e)
        {

            List<Account> accounts = bll.GetTaikhoanDTO();

            dtgvAccount.DataSource = accounts;

            if (cbAccountType.DataSource == null)
            {
                SetupComboBoxDataSource();
            }

        }

        // Sửa  tài khoản
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ ô được chọn
            string userName = txtUserName.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            int accountType = Convert.ToInt32(cbAccountType.SelectedValue);

            // Tạo đối tượng TaikhoanDTO với các giá trị đã cập nhật
            Account editedAccountDTO = new Account
            {
                UserName = userName,
                DisplayName = displayName,
                Type = accountType

            };
            // Gọi phương thức EditAccount từ TaikhoanBLL để cập nhật tài khoản
            bool success = bll.EditAccount(editedAccountDTO);

            // Kiểm tra xem việc cập nhật có thành công hay không
            if (success)
            {
                // Làm mới DataGridView để phản ánh các thay đổi
                btnShowAccount_Click(sender, e);
                MessageBox.Show("Cập nhật tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể cập nhật tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Them tài khoản
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ ô được chọn
            int idstaff = Convert.ToInt32(txtStaffID.Text.Trim());
            string userName = txtUserName.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            int accountType = Convert.ToInt32(cbAccountType.SelectedValue);

            // Tạo đối tượng TaikhoanDTO với các giá trị đã cập nhật
            Account taikhoanDTO = new Account
            {
                UserName = userName,
                DisplayName = displayName,
                Type = accountType,
                IDStaff = idstaff,

            };
            // Gọi phương thức AddAccount từ TaikhoanBLL để thêm tài khoản
            bool success = bll.AddAccount(taikhoanDTO);

            // Kiểm tra xem việc thêm có thành công hay không
            if (success)
            {
                // Làm mới DataGridView để phản ánh các thay đổi
                btnShowAccount_Click(sender, e);
                MessageBox.Show("Thêm tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể thêm tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Xóa tài khoản
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ ô được chọn
            string userName = txtUserName.Text.Trim();

            // Tạo đối tượng TaikhoanDTO với UserName để xóa tài khoản
            Account deletetaikhoanDTO = new Account
            {
                UserName = userName,
            };

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Gọi phương thức DeleteAccount từ TaikhoanBLL để xóa tài khoản
                bool success = bll.DeleteAccount(deletetaikhoanDTO);

                // Kiểm tra xem việc xóa có thành công hay không
                if (success)
                {
                    // Làm mới DataGridView để phản ánh các thay đổi
                    btnShowAccount_Click(sender, e);
                    MessageBox.Show("Xóa tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh ComboBox data source
                    SetupComboBoxDataSource();
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Cập nhật lại mật khẩu mặc định
        private void btnResetPass_Click(object sender, EventArgs e)
        {
            // Lấy thông tin người dùng, chẳng hạn từ TextBox hoặc DataGridView
            string userName = txtUserName.Text.Trim();

            // Gọi phương thức ResetPassword từ TaikhoanBLL để thực hiện reset mật khẩu
            bool success = bll.ResetPassWord(userName);

            // Kiểm tra kết quả và hiển thị thông báo tương ứng
            if (success)
            {
                MessageBox.Show("Reset mật khẩu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể reset mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tìm kiếm tài khoản
        private void btnSearchAccount_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchAccount.Text;
            DataTable dt = bll.SearchAccountByNameBULL(searchText);

            if (dt != null)
            {
                dtgvAccount.DataSource = dt;
            }
            else
            {

                MessageBox.Show("Không tải được dữ liệu .");
            }
        }


        //

        BindingSource statuslist = new BindingSource();
        BindingSource stafflist = new BindingSource();
        BUS_TableDrink bUSTable = new BUS_TableDrink();
        BUS_Status bUSStatus = new BUS_Status();
        BUS_Staff bUSStaff = new BUS_Staff();
        public void LoadTable()
        {
            statuslist.DataSource = bUSTable.LoadTableBUS();
        }
        public void LoadStaff()
        {
            stafflist.DataSource = bUSStaff.LoadStaffBUS();
        }
        private void LoadStatus()
        {
            List<StatusTableDrink> status = bUSStatus.LoadStatusBUS();
            cbStatus.DataSource = status;
            cbStatus.DisplayMember = "Status";
            cbStatus.ValueMember = "IDstatus";
        }
        private void LoadStatus1()
        {
            List<StatusTableDrink> status = bUSStatus.LoadStatusBUS();
            cbbStatus.DataSource = status;
            cbbStatus.DisplayMember = "Status";
            cbbStatus.ValueMember = "IDstatus";
        }
        void AddTableBinding()
        {
            txtTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtIDTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "IDTableDrink", true, DataSourceUpdateMode.Never));
        }
        void AddStaffBinding()
        {
            txtIDStaff.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "IDStaff", true, DataSourceUpdateMode.Never));
            txtStaffName.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtPhone.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "Phone", true, DataSourceUpdateMode.Never));
            txtBirthDay.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "BirthDate", true, DataSourceUpdateMode.Never));
            txtStartDay.DataBindings.Add(new Binding("Text", dtgvStaff.DataSource, "StartDate", true, DataSourceUpdateMode.Never));
        }

        private void txtIDTable_TextChanged(object sender, EventArgs e)
        {
            if (dtgvTable.SelectedCells.Count > 0)
            {
                int IDstatus = (int)dtgvTable.SelectedCells[0].OwningRow.Cells["IDstatus"].Value;
                List<StatusTableDrink> statusList = bUSStatus.BUS_GetStatusByID();
                cbStatus.DataSource = statusList;
                int index = statusList.FindIndex(item => item.IDstatus == IDstatus);
                cbStatus.SelectedIndex = index;
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            TableDrink tb = new TableDrink();
            tb.IDTableDrink = int.Parse(txtIDTable.Text);
            tb.Name = txtTableName.Text;
            tb.IDstatus = Convert.ToString((cbStatus.SelectedItem as StatusTableDrink).IDstatus);
            if (bUSTable.AddTableBUS(tb))
            {
                MessageBox.Show($"Thêm {txtTableName.Text} thành công");
                LoadTable();
            }
            else MessageBox.Show($"Thêm {txtTableName.Text} thất bại");
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {

            TableDrink tb = new TableDrink();
            tb.IDTableDrink = int.Parse(txtIDTable.Text);
            tb.Name = txtTableName.Text;
            tb.IDstatus = Convert.ToString((cbStatus.SelectedItem as StatusTableDrink).IDstatus);
            if (bUSTable.UpdateTableBUS(tb))
            {
                MessageBox.Show($"Sửa {txtTableName.Text} thành công");
                LoadTable();
            }
            else MessageBox.Show($"Sửa {txtTableName.Text} thất bại");
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dtgvTable.SelectedRows.Count > 0)
            {
                // Kiểm tra xem cột có tồn tại hay không
                if (dtgvTable.Columns.Contains("IDTableDrink"))
                {
                    try
                    {
                        // Lấy giá trị IDTableFood từ hàng được chọn
                        int selectedIdTableFood = Convert.ToInt32(dtgvTable.SelectedCells[0].OwningRow.Cells["IDTableDrink"].Value);

                        // Gọi phương thức xóa từ lớp BUS
                        if (bUSTable.DeleteTableBUS(selectedIdTableFood))
                        {
                            MessageBox.Show($"Xóa bàn có ID {selectedIdTableFood} thành công");
                            LoadTable(); // Load lại dữ liệu sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show($"Xóa bàn có ID {selectedIdTableFood} thất bại");
                        }
                    }
                    catch (FormatException ex)
                    {
                        // Xử lý lỗi nếu giá trị không thể chuyển đổi thành int
                        MessageBox.Show("Lỗi chuyển đổi dữ liệu: " + ex.Message);
                    }
                }
                else
                {
                    // Xử lý khi tên cột không tồn tại
                    MessageBox.Show("Không tìm thấy cột IDTableDrink");
                }
            }
            else
            {
                // Xử lý khi không có hàng nào được chọn
                MessageBox.Show("Vui lòng chọn một hàng để xóa");
            }
        }

        //Lọc bàn
        private void btnLoc_Click(object sender, EventArgs e)
        {
            // Lấy IDstatus từ ComboBox
            int selectedStatusID = ((StatusTableDrink)cbbStatus.SelectedItem).IDstatus;

            // Gọi phương thức từ lớp BUS để lấy dữ liệu sản phẩm theo loại tình trạng
            DataTable dt = bUSTable.GetStatusByStatusIDBUS(selectedStatusID);

            // Hiển thị dữ liệu trong DataGridView 
            if (dt != null)
            {
                dtgvTable.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu tình trạng bàn theo ID");
            }
        }

        //Xem staff
        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Staff st = new Staff();
            st.IDStaff = int.Parse(txtIDStaff.Text);
            st.Name = txtStaffName.Text;
            st.Phone = txtPhone.Text;
            st.BirthDate = Convert.ToDateTime(txtBirthDay.Text);
            st.StartDate = Convert.ToDateTime(txtStartDay.Text);
            if (bUSStaff.UpdateStaffBUS(st))
            {
                MessageBox.Show($"Sửa nhân viên {txtStaffName.Text} thành công");
                LoadStaff();
            }
            else MessageBox.Show($"Sửa nhân viên {txtStaffName.Text} thất bại");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dtgvStaff.SelectedRows.Count > 0)
            {
                // Kiểm tra xem cột có tồn tại hay không
                if (dtgvStaff.Columns.Contains("IDStaff"))
                {
                    try
                    {
                        // Lấy giá trị IDTableFood từ hàng được chọn
                        int selectedIdStaff = Convert.ToInt32(dtgvStaff.SelectedCells[0].OwningRow.Cells["IDStaff"].Value);

                        // Gọi phương thức xóa từ lớp BUS
                        if (bUSStaff.DeleteStaffBUS(selectedIdStaff))
                        {
                            MessageBox.Show($"Xóa nhân viên có ID {selectedIdStaff} thành công");
                            LoadStaff(); // Load lại dữ liệu sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show($"Xóa nhân viên có ID {selectedIdStaff} thất bại");
                        }
                    }
                    catch (FormatException ex)
                    {
                        // Xử lý lỗi nếu giá trị không thể chuyển đổi thành int
                        MessageBox.Show("Lỗi chuyển đổi dữ liệu: " + ex.Message);
                    }
                }
                else
                {
                    // Xử lý khi tên cột không tồn tại
                    MessageBox.Show("Không tìm thấy cột IDTableFood");
                }
            }
            else
            {
                // Xử lý khi không có hàng nào được chọn
                MessageBox.Show("Vui lòng chọn một hàng để xóa");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            DataTable dt = bUSStaff.SearchStaffByNameBUS(searchText);

            if (dt != null)
            {
                dtgvStaff.DataSource = dt;
            }
            else
            {

                MessageBox.Show("Không tải được dữ liệu .");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            Staff st = new Staff();
            st.IDStaff = int.Parse(txtIDStaff.Text);
            st.Name = txtStaffName.Text;
            st.Phone = txtPhone.Text;
            st.BirthDate = Convert.ToDateTime(txtBirthDay.Text);
            st.StartDate = Convert.ToDateTime(txtStartDay.Text);
            if (bUSStaff.AddStaffBUS(st))
            {
                MessageBox.Show($"Thêm nhân viên {txtStaffName.Text} thành công");
                LoadStaff();
            }
            else MessageBox.Show($"Thêm nhân viên {txtStaffName.Text} thất bại");
        }

        
    }
}
