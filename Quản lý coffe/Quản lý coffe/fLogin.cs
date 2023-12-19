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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            bll = new BUS_Login();

        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }
        private BUS_Login bll;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassWord.Text;

            Login login = new Login { UserName = username, Password = password };

            Login result = bll.KiemTraLogin(login);

            if (result != null)
            {
                fTableManager f = new fTableManager(result);
                Hide();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại, vui lòng nhập lại");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pOff_Click(object sender, EventArgs e)
        {
            if (txtPassWord.PasswordChar == '*')
            {
                pOn.BringToFront();
                txtPassWord.PasswordChar = '\0';
            }
        }

        private void pOn_Click(object sender, EventArgs e)
        {
            if (txtPassWord.PasswordChar == '\0')
            {
                pOff.BringToFront();
                txtPassWord.PasswordChar = '*';
            }
        }
    }


   
}
