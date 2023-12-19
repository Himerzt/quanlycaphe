using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_coffe
{
    public partial class fTableManager : Form
    {
        private Login loggedInUser;
        public fTableManager(Login user)
        {
            InitializeComponent();
            loggedInUser = user;
            // Hiển thị tên lên tab thông tin cá nhân
            thôngTinToolStripMenuItem.Text = $"Thông tin cá nhân ({loggedInUser.DisplayName})";
            this.FormClosing += fTableManager_FormClosing;


            // kiểm tra admin
            if (loggedInUser.Type == 1) // Admin
            {
                adminToolStripMenuItem.Enabled = true;
                thôngTinToolStripMenuItem.Enabled = true;
            }
            else
            {
                adminToolStripMenuItem.Enabled = false;
                thôngTinToolStripMenuItem.Enabled = true;
            }
        }
        
        private void fTableManager_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadDrinkCategory();
            LoadStaff();
           
        }
       
        private void fTableManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.FormClosing -= fTableManager_FormClosing;
            this.FormClosing -= fTableManager_FormClosing;


            if (MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi chương trình hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // User confirmed to exit, so exit the application
                Application.Exit();
            }
            else
            {
                // User canceled the exit, cancel the form closing event
                e.Cancel = true;
                this.FormClosing += fTableManager_FormClosing;

            }
        }


        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi chương trình hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(loggedInUser);
            Hide();
            f.FormClosing += (adminSender, adminE) =>
            {
                if (adminE.CloseReason == CloseReason.UserClosing)
                {
                    Show();
                }
            };

            f.ShowDialog();

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            Hide();
            f.FormClosing += (adminSender, adminE) =>
            {
                // Check if the fAdmin form is closing
                if (adminE.CloseReason == CloseReason.UserClosing)
                {
                    // Show the fTableManager form when fAdmin is closed
                    Show();
                    LoadTable();
                    LoadStaff();
                    LoadDrinkCategory();
                }
            };

            f.ShowDialog();
        }


        //
        BUS_TableDrink TableDrinkBUS = new BUS_TableDrink();
        void LoadTable()
        {
            flpTable.Controls.Clear();
           List<TableDrink> tableDrinks = TableDrinkBUS.GetListTableDrink();

            foreach (TableDrink item in tableDrinks)
            {
                StatusTableDrink a = new StatusTableDrink();
                
                Button btn = new Button() { Width=BUS_TableDrink.TableWidth, Height=BUS_TableDrink.TableHeight};
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.IDstatus)
                {
                    case "0":
                        btn.Text = "ID: "+item.IDTableDrink+Environment.NewLine+item.Name + Environment.NewLine + "Trống";
                        btn.BackColor = Color.Beige;
                       // btn.Text
                        break;
                    default:
                        btn.BackColor = Color.BurlyWood;
                        btn.Text = "ID: "+item.IDTableDrink + Environment.NewLine + item.Name + Environment.NewLine + "Có người";
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }
        BUS_Menu MenuBUS = new BUS_Menu();
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listBillInfo = BUS_Menu.Instance.GetListMenuByTable_BUS(id);
            float totalPrice = 0;
            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.Name.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }

           
            CultureInfo culture = new CultureInfo("vi-VN");

            txbTotalPrice.Text= totalPrice.ToString("c",culture );
        }
        TableDrink tableDrink = new TableDrink();
        public void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as TableDrink).IDTableDrink;
            lsvBill.Tag = (sender as Button);
            tableDrink.IDTableDrink = tableID;
            ShowBill(tableID);
        }
        BUS_DrinkCategory DrinkCategoryBUS = new BUS_DrinkCategory();
        void LoadDrinkCategory()
        {
            List<DrinkCategory> fc = DrinkCategoryBUS.GetListDrinkCategory();
            cbCategory.DataSource = fc;
            cbCategory.ValueMember = "IDDrinkCategory";
            cbCategory.DisplayMember = "Name";
        }
        void LoadDrinkListByCategoryID(int id)
        {
            List<Drink> listDrink = DrinkCategoryBUS.GetDrinkByCategoryID_BUS(id);
            cbDrink.DataSource = listDrink;
            cbDrink.ValueMember = "IDDrink";
            cbDrink.DisplayMember = "Name";
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            DrinkCategory selected = cb.SelectedItem as DrinkCategory;
            id = selected.IDDrinkCategory;

            LoadDrinkListByCategoryID(id);
        }
        BUS_Bill BillBUS = new BUS_Bill();
        BUS_Billinfo BillinfoBUS = new BUS_Billinfo();
        BUS_Payroll PayrollBUS = new BUS_Payroll();
        private void LoadStaff()
        {
            List<Staff> listStaff = PayrollBUS.GetListStaff();

            cbStaff.DisplayMember = "Name";
            cbStaff.ValueMember = "IDStaff";
            cbStaff.DataSource = listStaff;
        }
        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            
            int IDtable = tableDrink.IDTableDrink;
            int idBill = BUS_Bill.Instance.GetUncheckBillIDByTableID(IDtable);

            int DrinkID = (cbDrink.SelectedItem as Drink).IDDrink;
            int count = (int)nmDrinkCount.Value;
            int staffID = (cbStaff.SelectedItem as Staff).IDStaff;
            if (idBill == -1)
            {
                int billID = int.Parse(txbIDBill.Text);
               
                BUS_Bill.Instance.InsertBill_BUS(billID, IDtable, staffID);
                BUS_Billinfo.Instance.InsertBillInfo_BUS(billID, DrinkID, count);
                MessageBox.Show($"Thêm món cho bàn {tableDrink.IDTableDrink} thành công");
            }
            else
            {
                BillinfoBUS.InsertBillInfo_BUS(idBill, DrinkID, count);                
                if (count<=0) MessageBox.Show($"Xóa món cho bàn {tableDrink.IDTableDrink} thành công");
                else MessageBox.Show($"Thêm món cho bàn {tableDrink.IDTableDrink} thành công");

            }

            ShowBill(IDtable);
            LoadTable();

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            TableDrink table = lsvBill.Tag as TableDrink;

            int IDtable = tableDrink.IDTableDrink;
            int idBill = BUS_Bill.Instance.GetUncheckBillIDByTableID(IDtable);
            int discount = (int)nmDiscount.Value;
            
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn có id: {0} \nTổng tiền - (Tổng tiền / 100) x Giảm giá\n=> {1} - ({1} / 100) x {2} = {3}",IDtable, totalPrice, discount, finalTotalPrice),"Thông báo",MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillBUS.CheckOut_BUS(idBill, discount,(float)finalTotalPrice);
                    ShowBill(IDtable);

                    LoadTable();
                }
            }
        }

        
    }
}

