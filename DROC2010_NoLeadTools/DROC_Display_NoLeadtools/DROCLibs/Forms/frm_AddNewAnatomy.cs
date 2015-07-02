using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.Controls;
using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.CommonLibrary;

namespace VietBaIT.DROC
{
    public partial class frm_AddNewAnotomy : BaseForm
    {
        public AnatomyControl _ACtrl;
        action Act;
        public frm_AddNewAnotomy(AnatomyControl _ACtrl)
        {
            InitializeComponent();
            this._ACtrl = _ACtrl;
            txtSTT.KeyPress += new KeyPressEventHandler(_KeyPress);
        }
        void _KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = VietBaIT.CommonLibrary.Utility.NumbersOnly(e.KeyChar, txt.Text);
            }
            catch
            {
            }
        }
        private void frm_AddNewAnotomy_Load(object sender, EventArgs e)
        {
            ShowInfo();
            txtCode.Focus();
        }
        void ShowInfo()
        {
            Act = action.Insert;
            if (_ACtrl != null)
            {
                Act = action.Update;
                txtCode.Text = _ACtrl.Code;
                txtVnName.Text = _ACtrl.Vn_Name;
                txtEngName.Text = _ACtrl.En_Name;
                txtSTT.Text = _ACtrl.STT.ToString();
                txtCode.Enabled = false;
                txtVnName.Focus();
            }
        }
        private bool CheckValidData()
        {
            errorProvider1.Clear();
            if (txtCode.Text.Trim() == "")
            {
                errorProvider1.SetError(txtCode, "Bạn phải nhập mã vị trí chụp");
                txtCode.Focus();
                return false;
            }
            if (txtVnName.Text.Trim() == "")
            {
                errorProvider1.SetError(txtVnName, "Bạn phải nhập tên vị trí chụp");
                txtVnName.Focus();
                return false;
            }
            if (txtSTT.Text.Trim() == "")
            {
                errorProvider1.SetError(txtSTT, "Bạn phải nhập số thứ tự hiển thị");
                txtSTT.Focus();
                return false;
            }
            return true;
        }
        private void cmdRegister_Click(object sender, EventArgs e)
        {
            if (!CheckValidData()) return;
            _ACtrl = new AnatomyControl(txtCode.Text.Trim(), txtVnName.Text.Trim(), txtEngName.Text.Trim(), Convert.ToInt32(txtSTT.Text.Trim()));
            DoctorController _Ctrl = new DoctorController();

            ActionResult _ActionResult;
            if (Act == action.Insert) _ActionResult = _Ctrl.InsertAnatomy(_ACtrl.Code, _ACtrl.Vn_Name, _ACtrl.En_Name, _ACtrl.STT);
            else _ActionResult = _Ctrl.UpdateAnatomy(_ACtrl.Code, _ACtrl.Vn_Name, _ACtrl.En_Name, _ACtrl.STT);

            if (_ActionResult == ActionResult.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if (_ActionResult == ActionResult.ExistedRecord)
            {
                errorProvider1.SetError(txtCode, "Đã tồn tại vị trí chụp có mã " + _ACtrl.Code + ". Đề nghị bạn nhập lại mã khác!");
                Utility.FocusAndSelectAll(txtCode);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdVK_Click(object sender, EventArgs e)
        {

            if (!ExistsVK())
                ShowVK();
            else
                KillVK();
        }
        void ShowVK()
        {
            try
            {
                System.Diagnostics.Process.Start("KEYBOARD.exe");
            }
            catch
            {
            }
        }
        void KillVK()
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName("KEYBOARD");
                if (arrProcess.Length > 0) arrProcess[0].Kill();
            }
            catch
            {
            }
        }
        bool ExistsVK()
        {
            try
            {
                System.Diagnostics.Process[] arrProcess = System.Diagnostics.Process.GetProcessesByName("KEYBOARD");
                return arrProcess.Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
