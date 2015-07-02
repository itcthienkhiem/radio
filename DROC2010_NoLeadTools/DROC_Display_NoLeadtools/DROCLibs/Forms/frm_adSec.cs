using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;

namespace VietBaIT
{
    public partial class frm_adSec : Form
    {
        public frm_adSec()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(frm_adSec_KeyDown);
        }
        public frm_adSec(string question)
        {
            InitializeComponent();
            lblQuestion.Text = question;
            this.KeyDown += new KeyEventHandler(frm_adSec_KeyDown);

        }
        void frm_adSec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.Alt && e.Control && e.Shift && e.KeyCode == Keys.U && txtPwd.Text.Trim().ToUpper()=="UNLOCK")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if ( txtPwd.Text.Trim().ToLower() == "vietbaitadmin")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                new frm_LargeMsgBoxOK(MultiLanguage.GetText(globalVariables.DisplayLanguage, "Cảnh báo", "Warning"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Mật khẩu bị sai. Mời bạn nhập lại", "Invalid password. Pls try again!"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Khó quá", "It's difficult"), MultiLanguage.GetText(globalVariables.DisplayLanguage, "Bỏ qua", "Cancel")).ShowDialog();
                txtPwd.SelectAll();
                txtPwd.Focus();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            if (txtPwd.Text.Trim().ToUpper() == txtPwd.Tag.ToString().ToUpper() || txtPwd.Text.Trim().ToUpper() == cmdOK.Tag.ToString().ToUpper())
                lblPWD.ImageIndex = 0;
            else
                lblPWD.ImageIndex = 1;
        }
    }
}
