using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_coffe
{
    public partial class fAccountProfile : Form
    {
        private BUS_ChangePassword bll;
        private Login loggedInUser;
        public fAccountProfile(Login user)
        {
            InitializeComponent();
            bll = new BUS_ChangePassword();
            loggedInUser = user;

            txtUserName.Text = loggedInUser.UserName;
            txtDisPlayName.Text = loggedInUser.DisplayName;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string oldPassWord = txtPassWord.Text;
            string newPassWord = txtNewPass.Text;
            string reEnterPass = txtReEnterPass.Text;

            if (newPassWord != reEnterPass)
            {
                MessageBox.Show("Mật khẩu mới không khớp. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChangePassword changePasswordDTO = new ChangePassword
            {
                UserName = userName,
                OldPassword = oldPassWord,
                NewPassword = newPassWord
            };



            bool result = bll.CapNhatMatKhau(changePasswordDTO);

            if (result)
            {
                MessageBox.Show("Cập nhật mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật mật khẩu thất bại. Vui lòng thử lại tên đăng nhập hoặc mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
           
        }
    }
}
