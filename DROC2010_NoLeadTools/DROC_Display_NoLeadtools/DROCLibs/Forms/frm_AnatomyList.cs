using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using DROCLibs.Entities;
using VietBaIT.DROC.Objects;
using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects.ObjectInfor;
using System.Collections;
using VietBaIT.Controls;
namespace VietBaIT.DROC
{
    public partial class frm_AnotomyList : BaseForm
    {
        public DataTable AnatomyDataSource = new DataTable();
        private AnatomyControl _currA;
        public frm_AnotomyList()
        {
            InitializeComponent();
           
            Application.DoEvents();
            GetData();
            this.WindowState = FormWindowState.Maximized;
           
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

        public void GetSelectedAnatomyName()
        {
            try
            {
               
            }
            catch
            {
            }
        }
        public FlowLayoutPanel _pnlAnotomy
        {
            get { return pnlAnatomyList; }
        }

        public void ResetPreviousSelectedObject(string CODE)
        {
            try
            {
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.isPressed && _Anatomy.Code != CODE)
                        _Anatomy.Reset();
                }
            }
            catch
            {
            }
        }
        private void GetData()
        {
            try
            {
                AnatomyDataSource = new DoctorController().GetAnatomy().Tables[0];
                AnatomyControl _Anatomy = null;
                foreach (DataRow dr in AnatomyDataSource.Rows)
                {
                    _Anatomy = new AnatomyControl( dr["Code"].ToString(), Utility.sDbnull(dr["VN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(dr["STT"].ToString(), 0));
                    _Anatomy._OnClick += new AnatomyControl.OnClick(_Anatomy__OnClick);
                    pnlAnatomyList.Controls.Add(_Anatomy);
                }
                cmdNew.Enabled = AnatomyDataSource.Rows.Count > 0;
            }
            catch
            {
            }
        }

        void _Anatomy__OnClick(AnatomyControl obj)
        {
            try
            {
                ResetPreviousSelectedObject(obj.Code);
                obj.isPressed = !obj.isPressed;
                if (!obj.isPressed)
                {
                    obj._AnatomyObject.BackColor = Color.WhiteSmoke;
                    obj._AnatomyObject.ForeColor = Color.Black;
                }
                else
                {
                    _currA = obj.Copy();
                    obj._AnatomyObject.BackColor = Color.SteelBlue;
                    obj._AnatomyObject.ForeColor = Color.White;
                }
                cmdDel.Enabled = HasSelectedAnatomy();
                cmdUpdate.Enabled = cmdDel.Enabled;
            }
            catch
            {
            }
        }

     

        private bool HasSelectedAnatomy()
        {

            try
            {
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.isPressed)
                        return true;
                }
                _currA = null;
                return false;
            }
            catch
            {
                return false;
            }
        }
        private void InitView()
        {
           
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Insert();
        }
        void Insert()
        {
             try
            {
                frm_AddNewAnotomy _AddNew = new frm_AddNewAnotomy(null);
                if (_AddNew.ShowDialog() == DialogResult.OK)
                {
                    AnatomyControl _NewAnatomy = _AddNew._ACtrl.Copy();
                    _NewAnatomy._OnClick += new AnatomyControl.OnClick(_Anatomy__OnClick);
                    pnlAnatomyList.Controls.Add(_NewAnatomy);
                    pnlAnatomyList.ScrollControlIntoView(_NewAnatomy);

                }
            }
             catch
             {
             }
        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }
        void Update()
        {
            try
            {
                frm_AddNewAnotomy _AddNew = new frm_AddNewAnotomy(_currA);
                if (_AddNew.ShowDialog() == DialogResult.OK)
                {
                    foreach (Control ctr in pnlAnatomyList.Controls)
                    {
                        AnatomyControl _Anatomy = ctr as AnatomyControl;
                        if (_Anatomy.isPressed && _Anatomy.Code == _currA.Code)
                        {
                            int idx = pnlAnatomyList.Controls.IndexOf(_Anatomy);
                            pnlAnatomyList.Controls.Remove(_Anatomy);
                            AnatomyControl _NewAnatomy = _AddNew._ACtrl.Copy();
                            _NewAnatomy._OnClick += new AnatomyControl.OnClick(_Anatomy__OnClick);
                            pnlAnatomyList.Controls.Add(_NewAnatomy);
                            pnlAnatomyList.Controls.SetChildIndex(_NewAnatomy, idx);
                            _NewAnatomy._AnatomyObject.PerformClick();
                          
                        }
                    }
                }
            }
            catch
            {
            }
        }

        
        void DeleteFromDataTable(string ACode)
        {
            try
            {

               
            }
            catch
            {
            }
        }
        private void cmdDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (new frm_LargeMsgBox("Xác nhận xóa vị trí chụp", "Bạn có muốn xóa vị trí chụp có mã " + _currA.Code + "(" + _currA.Vn_Name + ") không?", "Đồng ý xóa", "Không xóa").ShowDialog() == DialogResult.OK)
                {
                    DoctorController _DoctorController = new DoctorController();
                    if (_DoctorController.AnatomyHasUsed(_currA.Code) != ActionResult.Success)
                    {
                        new frm_LargeMsgBoxOK("Thông báo", "Vị trí chụp này đã được sử dụng trong bảng khác nên bạn không thể xóa. Đề nghị bạn kiểm tra lại", "Đồng ý", "").ShowDialog();
                        return;
                    }
                    foreach (Control ctr in pnlAnatomyList.Controls)
                    {
                        AnatomyControl _Anatomy = ctr as AnatomyControl;
                        if (_Anatomy.isPressed && _Anatomy.Code == _currA.Code)
                        {
                            if (_DoctorController.DeleteAnatomy(_currA.Code) == ActionResult.Success)
                            {
                                pnlAnatomyList.Controls.Remove(_Anatomy);
                                new frm_LargeMsgBoxOK("Thông báo", "Đã xóa thành công vị trí chụp có mã " + _currA.Code + "(" + _currA.Vn_Name + ")", "Đồng ý", "").ShowDialog();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                cmdDel.Enabled = HasSelectedAnatomy();
                cmdUpdate.Enabled = cmdDel.Enabled;
            }
        }
    }
}
