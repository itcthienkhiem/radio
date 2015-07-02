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
    public partial class frm_Login : Form
    {
        public bool mv_bCallFromLogin = false;
        public bool mv_bLoginSuccess = false;
        private string UID = "VBIT";
        private string PWD = "";
        public frm_Login()
        {
            InitializeComponent();
          mdlStatic.mainform = new DROC_Ribbon();
            this.Load += new EventHandler(frm_Login_Load);
            this.KeyDown += new KeyEventHandler(frm_Login_KeyDown);
            cmdLogin.Click += new EventHandler(cmdLogin_Click);
            cmdExit.Click += new EventHandler(cmdExit_Click);
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
        }

        void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
            }
        }

        void frm_Login_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter) this.ProcessTabKey(true);
            }
            catch (Exception ex)
            {
            }
        }


        void frm_Login_Load(object sender, EventArgs e)
        {
            string sv_sUID = null;
            string sv_sPwd = null;
            try
            {

                sv_sUID = hrk.RegConfiguration.GetSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_UID");
                sv_sPwd = hrk.RegConfiguration.GetSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_PWD");
                if (sv_sUID.Trim() == "")
                {
                    sv_sUID = UID;
                    sv_sPwd = PWD;
                }
                else
                {
                    UID = sv_sUID;
                    PWD = sv_sPwd;
                }
                txtUID.Text = UID.Trim();
                txtPWD.Text = "";
                //sv_sPwd.Trim
                txtUID.Focus();
            }
            catch (Exception ex)
            {

            }
        }


        private bool mf_bCheckData()
        {
            try
            {
                if (txtUID.Text.Trim().Equals(string.Empty))
                {
                    Utility.ShowMsg("Bạn phải nhập tên đăng nhập!");
                    txtUID.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private bool bLoginSuccess()
        {
            clsUser sv_oUser = new clsUser();
            VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt("Rijndael");

            if (!globalVariables.gv_ConnectSuccess)
            {
                return false;
            }
            if (!sv_oUser.bIsExisted(txtUID.Text.Trim()))
            {
                Utility.ShowMsg("Không tồn tại người dùng có tên đăng nhập là " + txtUID.Text.Trim() + ". Đề nghị nhập lại", "Thông báo");
                txtUID.Focus();
                return false;
            }
            if (!sv_oUser.bLoginSuccess(txtUID.Text.Trim(), sv_oEncrypt.Mahoa(txtPWD.Text.Trim())))
            {
                MessageBox.Show("Sai mật khẩu đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Focus();
                return false;
            }
            return true;
        }

        private void cmdLogin_Click(object sender, System.EventArgs e)
        {
            string sv_sUID = string.Empty;
            string sv_sPWD = string.Empty;
            VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt("Rijndael");

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (mf_bCheckData())
                {
                    if (bLoginSuccess())
                    {
                        globalVariables.UserName = txtUID.Text.Trim();
                        //globalVariables.pư = sv_oEncrypt.Mahoa(txtPWD.Text.Trim);
                        hrk.RegConfiguration.SaveSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_UID", txtUID.Text.Trim());
                        hrk.RegConfiguration.SaveSettings("DVC_COMPANY", "VBIT_DRTech_DVC", "APP_PWD", txtPWD.Text.Trim());
                        globalVariables.gv_ConnectSuccess = true;
                  
                        this.Cursor = Cursors.Default;
                        //this.Close();
                        mv_bLoginSuccess = true;
                        mdlStatic.mainform.ShowDialog();

                        return;
                    }

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
        }
        private void frm_Login_Load_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mdlStatic._virtualKeyboard = new VirtualKeyboard.DVC_VirtualKeyboard();
            //mdlStatic._virtualKeyboard.TopMost = true;
            mdlStatic._virtualKeyboard.Show();
        }
    }
}
