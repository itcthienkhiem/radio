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
    public partial class frm_Choose_Anotomy_Projection : BaseForm
    {
        public DataTable AP_DataSource=new DataTable();
        public DataTable APParams_DataSource = new DataTable();
        public int CurrDevice_ID=-1;
        AnatomyControl _Anatomy = null;
        public string BODYSIZE_CODE = "";
        public string BODYSIZE_NAME = "";
        public frm_Choose_Anotomy_Projection(int CurrDevice_ID)
        {
            InitializeComponent();
            this.CurrDevice_ID = CurrDevice_ID;
             
        }
      
        private void GetAnatomyProjection()
        {
            try
            {
                AP_DataSource = new DoctorController().GetAnatomyProjection(CurrDevice_ID).Tables[0];
                APParams_DataSource = new DoctorController().GetAnatomyProjectionParams().Tables[0];
            }
            catch
            {
            }
        }
       
        bool isExistAnatomy(string Code)
        {
            foreach (Control ctr in pnlAnatomyList.Controls)
            {
                AnatomyControl _Anatomy = ctr as AnatomyControl;
                if (_Anatomy.Code == Code) return true;
            }
            return false;
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
        public void AutoSelectAnatomy(string ACODE,string P_Code,ref bool Success)
        {
            bool blnFound = false;
            Success = false;
            try
            {
                
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.Code == ACODE)
                    {
                        blnFound = true;
                        _NewAnatomy__OnClick(_Anatomy);
                        break;
                    }
                    blnFound = false;
                }
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.Code == P_Code)
                    {
                        blnFound = true;
                        _NewProjection__OnClick(_Projection);
                        break;
                    }
                    blnFound = false;
                }
            }
            catch
            {
            }
            finally
            {
                Success = blnFound;
                if (blnFound)
                    cmdOK_Click(cmdOK, new EventArgs());
            }
        }
        void _NewAnatomy__OnClick(AnatomyControl obj)
        {
            try
            {
                _Anatomy = obj.Copy();
                pnlProjectionList.Controls.Clear();
                ResetPreviousSelectedObject(obj.Code);
                obj.isPressed = !obj.isPressed;
                _Anatomy.isPressed = obj.isPressed;
                
                if (!obj.isPressed)
                {
                   
                    obj._AnatomyObject.BackColor = Color.WhiteSmoke;
                    obj._AnatomyObject.ForeColor = Color.Black;
                }
                else
                {
                    BuildProjectionOfAnatomy(obj);
                    obj._AnatomyObject.BackColor = Color.SteelBlue;
                    obj._AnatomyObject.ForeColor = Color.White;
                }
            }
            catch
            {
            }
           
        }
        void BuildProjectionOfAnatomy(AnatomyControl obj)
        {
            try
            {
                DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + obj.Code + "' AND PROJECTION_CODE<>'' ");//AND DEVICE_ID=" + CurrDevice_ID.ToString());
                foreach (DataRow dr in arrDR)
                {
                    if (!isExistProjection(dr["PROJECTION_CODE"].ToString()))
                    {
                        ProjectionControl _NewProjection = new ProjectionControl(dr["PROJECTION_CODE"].ToString(), Utility.sDbnull(dr["VN_Projection_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_Projection_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(0, 0), false);
                        _NewProjection._OnClick += new ProjectionControl.OnClick(_NewProjection__OnClick);
                        pnlProjectionList.Controls.Add(_NewProjection);
                        if (dr["CHON"].ToString() == "1") _NewProjection._ProjectionObject.PerformClick();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                
            }
        }
        bool isExistProjection(string Code)
        {
            foreach (Control ctr in pnlProjectionList.Controls)
            {
                ProjectionControl _Projection = ctr as ProjectionControl;
                if (_Projection.Code == Code) return true;
            }
            return false;
        }
      

        void _NewProjection__OnClick(ProjectionControl obj)
        {
            try
            {
                obj.isPressed = !obj.isPressed;
                if (!obj.isPressed)
                {
                    obj._ProjectionObject.BackColor = Color.WhiteSmoke;
                    obj._ProjectionObject.ForeColor = Color.Black;
                }
                else
                {
                    obj._ProjectionObject.BackColor = Color.SteelBlue;
                    obj._ProjectionObject.ForeColor = Color.White;
                }
            }
            catch
            {
            }
            finally
            {
                updateProjectionStatus(obj);
                cmdOK.Enabled = AP_DataSource.Select("CHON=1").Length>0;
            }

           
        }
        void updateProjectionStatus(ProjectionControl obj)
        {
            try
            {

                DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + obj.Code + "'");// AND DEVICE_ID=" +CurrDevice_ID.ToString());
                if (arrDR.Length >0)
                {
                    arrDR[0]["CHON"] = obj.isPressed ? 1 : 0;
                }
                
                AP_DataSource.AcceptChanges();
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
                return false;
            }
            catch
            {
                return false;
            }
        }
       
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        void LoadBodySize()
        {
            DataTable dtBodySize;
            try
            {
                dtBodySize = new ModalityController().GetAllBodySizeData().Tables[0];
                if (dtBodySize != null)
                {
                    cboBodySize.DataSource = dtBodySize.DefaultView;
                    cboBodySize.DisplayMember =globalVariables.DisplayLanguage=="VN"? "VN_BODYSIZE_NAME":"EN_BODYSIZE_NAME";
                    cboBodySize.ValueMember = "BODYSIZE_CODE";
                    cmdOK.Enabled = dtBodySize.Rows.Count > 0;
                   

                }
                if (cboBodySize.Items.Count > 0) cboBodySize.SelectedIndex = 0;
            }
            catch
            {
            }
           
        }

        private void frm_Choose_Anotomy_Projection_Load(object sender, EventArgs e)
        {
            InitComponents();
        }
        public void InitComponents()
        {
            LoadBodySize();
            GetAnatomyProjection();
            InitControls();
        }
        void InitControls()
        {
            try
            {
                int i = 0;
                DataRow[] arrDr = AP_DataSource.Select("1=1");//DEVICE_ID=" + CurrDevice_ID.ToString());
                foreach (DataRow dr in arrDr)
                {
                    if (!isExistAnatomy(dr["ANATOMY_CODE"].ToString()))
                    {
                        AnatomyControl _Anatomy = new AnatomyControl(dr["ANATOMY_CODE"].ToString(), Utility.sDbnull(dr["VN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(dr["A_STT"].ToString(), 0));
                        _Anatomy.Width = pnlAnatomyList.Width-10;
                        _Anatomy._OnClick += new AnatomyControl.OnClick(_NewAnatomy__OnClick);
                        pnlAnatomyList.Controls.Add(_Anatomy);

                        if (i == 0) _Anatomy._AnatomyObject.PerformClick();
                        i++;
                    }
                }
            }
            catch
            {
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            BODYSIZE_CODE = cboBodySize.SelectedValue.ToString();
            BODYSIZE_NAME = cboBodySize.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
    }
}
