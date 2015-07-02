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
    public partial class frm_ProjectionList : BaseForm
    {
        public DataTable ProjectionDataSource = new DataTable();
        private ProjectionControl _currP;
        public frm_ProjectionList()
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

        public void GetSelectedProjectionName()
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
            get { return pnlProjectionList; }
        }

        public void ResetPreviousSelectedObject(string CODE)
        {
            try
            {
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed && _Projection.Code != CODE)
                        _Projection.Reset();
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
                ProjectionDataSource = new DoctorController().GetProjection().Tables[0];
                ProjectionControl _Projection = null;
                foreach (DataRow dr in ProjectionDataSource.Rows)
                {
                    _Projection = new ProjectionControl( dr["Code"].ToString(), Utility.sDbnull(dr["VN_Projection_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_Projection_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(dr["STT"].ToString(), 0),false);
                    _Projection._OnClick += new ProjectionControl.OnClick(_Projection__OnClick);
                    pnlProjectionList.Controls.Add(_Projection);
                }
               
            }
            catch
            {
            }
        }

        void _Projection__OnClick(ProjectionControl obj)
        {
            try
            {
                ResetPreviousSelectedObject(obj.Code);
                obj.isPressed = !obj.isPressed;
                if (!obj.isPressed)
                {
                    obj._ProjectionObject.BackColor = Color.WhiteSmoke;
                    obj._ProjectionObject.ForeColor = Color.Black;
                }
                else
                {
                    _currP = obj.Copy();
                    obj._ProjectionObject.BackColor = Color.SteelBlue;
                    obj._ProjectionObject.ForeColor = Color.White;
                }
                cmdDel.Enabled = HasSelectedProjection();
                cmdUpdate.Enabled = cmdDel.Enabled;
            }
            catch
            {
            }
        }

     

        private bool HasSelectedProjection()
        {

            try
            {
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
                        return true;
                }
                _currP = null;
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
                frm_AddNewProjection _AddNew = new frm_AddNewProjection(null);
                if (_AddNew.ShowDialog() == DialogResult.OK)
                {
                    ProjectionControl _NewProjection = _AddNew._ACtrl.Copy();
                    _NewProjection._OnClick += new ProjectionControl.OnClick(_Projection__OnClick);
                    pnlProjectionList.Controls.Add(_NewProjection);
                    pnlProjectionList.ScrollControlIntoView(_NewProjection);

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
                frm_AddNewProjection _AddNew = new frm_AddNewProjection(_currP);
                if (_AddNew.ShowDialog() == DialogResult.OK)
                {
                    foreach (Control ctr in pnlProjectionList.Controls)
                    {
                        ProjectionControl _Projection = ctr as ProjectionControl;
                        if (_Projection.isPressed && _Projection.Code == _currP.Code)
                        {
                            int idx = pnlProjectionList.Controls.IndexOf(_Projection);
                            pnlProjectionList.Controls.Remove(_Projection);
                            ProjectionControl _NewProjection = _AddNew._ACtrl.Copy();
                            _NewProjection._OnClick += new ProjectionControl.OnClick(_Projection__OnClick);
                            pnlProjectionList.Controls.Add(_NewProjection);
                            pnlProjectionList.Controls.SetChildIndex(_NewProjection, idx);
                            _NewProjection._ProjectionObject.PerformClick();
                          
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
                if (new frm_LargeMsgBox("Xác nhận xóa hướng chụp", "Bạn có muốn xóa hướng chụp có mã " + _currP.Code + "(" + _currP.Vn_Name + ") không?", "Đồng ý xóa", "Không xóa").ShowDialog() == DialogResult.OK)
                {
                    DoctorController _DoctorController = new DoctorController();
                    if (_DoctorController.ProjectionHasUsed(_currP.Code) != ActionResult.Success)
                    {
                        new frm_LargeMsgBoxOK("Thông báo", "hướng chụp này đã được sử dụng trong bảng khác nên bạn không thể xóa. Đề nghị bạn kiểm tra lại", "Đồng ý", "").ShowDialog();
                        return;
                    }
                    foreach (Control ctr in pnlProjectionList.Controls)
                    {
                        ProjectionControl _Projection = ctr as ProjectionControl;
                        if (_Projection.isPressed && _Projection.Code == _currP.Code)
                        {
                            if (_DoctorController.DeleteProjection(_currP.Code) == ActionResult.Success)
                            {
                                pnlProjectionList.Controls.Remove(_Projection);
                                new frm_LargeMsgBoxOK("Thông báo", "Đã xóa thành công hướng chụp có mã " + _currP.Code + "(" + _currP.Vn_Name + ")", "Đồng ý", "").ShowDialog();
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
                cmdDel.Enabled = HasSelectedProjection();
                cmdUpdate.Enabled = cmdDel.Enabled;
            }
        }
    }
}
