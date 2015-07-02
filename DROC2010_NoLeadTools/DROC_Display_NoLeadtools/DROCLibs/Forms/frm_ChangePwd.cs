using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using System.Data.SqlClient;
using System.IO;
using VietBaIT.DROC.Objects.ObjectController;
namespace VietBaIT.DROC
{
    public partial class frm_ChangePwd : Form
    {
        public frm_ChangePwd()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(frm_ChangePwd_KeyDown);
        }

        void frm_ChangePwd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        ProcessTabKey(true);
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdLogin_Click(object sender, EventArgs e)
        {
            string sv_sPWD = string.Empty;
            VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt("Rijndael");
            clsUser sv_oUser = new clsUser();
            try
            {

                sv_sPWD = sv_oEncrypt.Mahoa(txtOldPwd.Text.Trim());
                if (!txtNewPwd.Text.Trim().Equals(txtConfirm.Text.Trim()))
                {
                    MessageBox.Show("Mật khẩu xác nhận phải giống mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirm.Focus();
                    return;
                }
                //Kiểm tra xem đã nhập mật khẩu cũ đúng hay chưa?
                if (globalVariables.IsAdminLogin)
                {
                    if (!sv_oUser.bLoginSuccessAdmin(globalVariables.UserName, sv_sPWD))
                    {
                        MessageBox.Show("Sai mật khẩu đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOldPwd.Focus();
                        return;
                    }
                }
                else
                {
                    if (!sv_oUser.bLoginSuccess(globalVariables.UserName, sv_sPWD))
                    {
                        MessageBox.Show("Sai mật khẩu đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOldPwd.Focus();
                        return;
                    }
                }

                //Kiểm tra xem mật khẩu cũ và mật khẩu mới có giống nhau không
                if (txtNewPwd.Text.Trim().Equals(txtOldPwd.Text.Trim()))
                {
                    MessageBox.Show("Đã thay đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                if (globalVariables.IsAdminLogin)
                {
                    if (sv_oUser.bChangePasswordForAdmin(globalVariables.UserName, sv_oEncrypt.Mahoa(txtNewPwd.Text.Trim())))
                    {
                        MessageBox.Show("Đã thay đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }
                else
                {
                    if (sv_oUser.bChangePassword(globalVariables.UserName, sv_oEncrypt.Mahoa(txtNewPwd.Text.Trim())))
                    {
                        MessageBox.Show("Đã thay đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_ChangePwd_Load(object sender, EventArgs e)
        {
            txtOldPwd.Focus();
        }
    }
}
