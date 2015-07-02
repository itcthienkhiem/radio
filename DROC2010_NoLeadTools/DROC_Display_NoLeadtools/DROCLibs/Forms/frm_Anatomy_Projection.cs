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
    public partial class frm_Anatomy_Projection : BaseForm
    {
        public DataTable AP_DataSource=new DataTable();
        public DataTable APParams_DataSource = new DataTable();
        private int CurrDevice_ID=-1;
        AnatomyControl _Anatomy = null;
        ProjectionControl _lastSelectedP = null;
        public frm_Anatomy_Projection()
        {
            InitializeComponent();
           this.Load+=new EventHandler(frm_Anatomy_Projection_Load);
           this.KeyDown += new KeyEventHandler(frm_Anatomy_Projection_KeyDown);
        }

        void frm_Anatomy_Projection_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.R)
                {
                    GetAnatomyProjection(CurrDevice_ID);
                    InitControls();
                }
            }
            catch
            {
            }
        }
        private void GetAnatomyProjection(int CurrDevice_ID)
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

        private void GetAnatomyProjection()
        {
            try
            {
                AP_DataSource = new DoctorController().GetAnatomyProjection(-1).Tables[0];
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
       
        void AddRow(string ACode, string PCode, string A_VnName, string A_EnName, string P_VnName, string P_EnName, int Device_ID, int isUsed4Emerency)
        {
            try
            {
                if (ACode != "" && PCode != "" && Device_ID!=-1) new DoctorController().InsertAP(ACode, PCode, A_VnName, A_EnName, P_VnName, P_EnName, Device_ID, isUsed4Emerency);
                DataRow[] arrDR=AP_DataSource.Select("ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString());
                if (arrDR.Length <= 0)
                {
                    DataRow dr = AP_DataSource.NewRow();
                    dr["ANATOMY_CODE"] = ACode;
                    dr["PROJECTION_CODE"] = PCode;
                    dr["DEVICE_ID"] = Device_ID;
                    dr["VN_ANATOMY_NAME"] = A_VnName;
                    dr["VN_PROJECTION_NAME"] =P_VnName ;
                    dr["EN_ANATOMY_NAME"] = A_EnName;
                    dr["EN_PROJECTION_NAME"] = P_EnName;
                    dr["IS_USED_FOR_EMERENCY"] = isUsed4Emerency;
                    dr["RAD_CODE"] = "";
                    AP_DataSource.Rows.Add(dr);
                }
                else
                {
                    arrDR[0]["ANATOMY_CODE"] = ACode;
                    arrDR[0]["PROJECTION_CODE"] = PCode;
                    arrDR[0]["DEVICE_ID"] = Device_ID;
                    arrDR[0]["VN_ANATOMY_NAME"] = A_VnName;
                    arrDR[0]["VN_PROJECTION_NAME"] = P_VnName;
                    arrDR[0]["EN_ANATOMY_NAME"] =A_EnName ;
                    arrDR[0]["EN_PROJECTION_NAME"] = P_EnName;
                    arrDR[0]["IS_USED_FOR_EMERENCY"] = isUsed4Emerency;
                }
                AP_DataSource.AcceptChanges();
            }
            catch
            {
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
                cmdChooseProjection.Enabled = obj.isPressed;
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
            finally
            {
                cmdDeleteAnatomy.Enabled = HasSelectedAnatomy();
                cmdDelProjection.Enabled = HasSelectedProjection();
                cmdMakeAsEmerency.Enabled = cmdDelProjection.Enabled;
                pnlParams.Enabled = cmdDelProjection.Enabled;
            }
        }
        void BuildProjectionOfAnatomy(AnatomyControl obj)
        {
            try
            {
                DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + obj.Code + "' AND PROJECTION_CODE<>'' AND DEVICE_ID=" + CurrDevice_ID.ToString(),"P_STT");
                foreach (DataRow dr in arrDR)
                {
                    ProjectionControl _NewProjection = new ProjectionControl( dr["PROJECTION_CODE"].ToString(), Utility.sDbnull(dr["VN_Projection_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_Projection_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(0, 0),Utility.sDbnull(dr["IS_USED_FOR_EMERENCY"],"0")=="1"?true:false);
                    _NewProjection.RAD_Code = Utility.sDbnull(dr["RAD_CODE"], "");
                    _NewProjection._OnClick += new ProjectionControl.OnClick(_NewProjection__OnClick);
                    pnlProjectionList.Controls.Add(_NewProjection);
                }
            }
            catch
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
       
        private void cmdChooseProjection_Click(object sender, EventArgs e)
        {
            try
            {
                frm_ChooseProjection _ChooseProjection = new frm_ChooseProjection();
                if (_ChooseProjection.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = _ChooseProjection.SelectedObjectCount;
                    progressBar1.Value = 0;
                    foreach (Control ctr in _ChooseProjection._pnlProjection.Controls)
                    {
                        ProjectionControl _Projection = ctr as ProjectionControl;
                        if (_Projection.isPressed)
                        {
                            progressBar1.Value += 1;
                            if (!isExistProjection(_Projection.Code))
                            {
                                ProjectionControl _NewProjection = _Projection.Copy();
                                AddRow(_Anatomy.Code, _NewProjection.Code, _Anatomy.Vn_Name, _Anatomy.En_Name, _NewProjection.Vn_Name, _NewProjection.En_Name, CurrDevice_ID, 0);
                                _NewProjection._OnClick += new ProjectionControl.OnClick(_NewProjection__OnClick);
                                pnlProjectionList.Controls.Add(_NewProjection);
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
                progressBar1.Visible = false;
            }
        }
       
        void _NewProjection__OnClick(ProjectionControl obj)
        {
            try
            {
                if (lblMultiCheck.ImageIndex==1) ResetPreviousSelectedPObject(obj.Code);
                obj.isPressed = !obj.isPressed;
                if (!obj.isPressed)
                {
                    obj._ProjectionObject.BackColor = Color.WhiteSmoke;
                    obj._ProjectionObject.ForeColor = Color.Black;
                    if (cboBodySize.Items.Count > 0)
                    {
                        cboBodySize.SelectedIndex = 0;
                        nmrKVP.Value = 15;
                        nmrMA.Value = 0;
                        nmrMAS.Value = 1;
                        CHK_FILMHD.ImageIndex = 1;
                        chkIsLargeFocus.ImageIndex = 0;
                        chkAutoHFlip.ImageIndex = 1;
                        chkAutoVFlip.ImageIndex = 1;
                    }
                    txtRadCode.Text = "";
                }
                else
                {
                    _lastSelectedP=obj;
                    obj._ProjectionObject.BackColor = Color.SteelBlue;
                    obj._ProjectionObject.ForeColor = Color.White;
                    //Load tham số
                    if (cboBodySize.Items.Count > 0)
                    {
                        DataRow[] arrDr = APParams_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + obj.Code + "' AND BODYSIZE_CODE='" + cboBodySize.SelectedValue.ToString() + "'");
                        if (arrDr.Length > 0)
                        {
                            CHK_FILMHD.ImageIndex = Utility.Int32Dbnull(arrDr[0]["FILM_HD"], 0) == 1 ? 0 : 1;
                            nmrKVP.Value = Convert.ToDecimal(arrDr[0]["KVP"]);
                            nmrMA.Value = Convert.ToInt32(arrDr[0]["MA"]);
                            nmrMAS.Value = Convert.ToInt32(arrDr[0]["MAS"]);
                            DataRow[] arrDrAp = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + obj.Code + "' AND DEVICE_ID=" + CurrDevice_ID);
                            if (arrDrAp.Length > 0)
                            {
                                chkIsLargeFocus.ImageIndex = Utility.Int32Dbnull(arrDrAp[0]["LARGE_FOCUS"], 0) == 1 ? 0 : 1;
                                chkAutoHFlip.ImageIndex = Utility.Int32Dbnull(arrDrAp[0]["AUTO_FLIPH"], 0) == 1 ? 0 : 1;
                                chkAutoVFlip.ImageIndex = Utility.Int32Dbnull(arrDrAp[0]["AUTO_FLIPV"], 0) == 1 ? 0 : 1;
                               
                            }
                            else
                            {
                                chkIsLargeFocus.ImageIndex = 0;
                                chkAutoHFlip.ImageIndex = 1;
                                chkAutoVFlip.ImageIndex = 1;
                               
                            }
                        }
                        else
                        {
                            CHK_FILMHD.ImageIndex = 1;
                            DataRow[] arrDrAp = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + obj.Code + "' AND DEVICE_ID=" + CurrDevice_ID);
                            if (arrDrAp.Length > 0)
                            {
                                chkIsLargeFocus.ImageIndex = Utility.Int32Dbnull(arrDrAp[0]["LARGE_FOCUS"], 0) == 1 ? 0 : 1;
                                chkAutoHFlip.ImageIndex = Utility.Int32Dbnull(arrDrAp[0]["AUTO_FLIPH"], 0) == 1 ? 0 : 1;
                                chkAutoVFlip.ImageIndex = Utility.Int32Dbnull(arrDrAp[0]["AUTO_FLIPV"], 0) == 1 ? 0 : 1;
                            }
                            else
                            {
                                chkIsLargeFocus.ImageIndex = 0;
                                chkAutoHFlip.ImageIndex = 1;
                                chkAutoVFlip.ImageIndex = 1;
                            }
                        }
                    }
                    txtRadCode.Text = obj.RAD_Code;
                }
            }
            catch
            {
            }
            finally
            {
                cmdDelProjection.Enabled = HasSelectedProjection();
                cmdMakeAsEmerency.Enabled = cmdDelProjection.Enabled;
                pnlParams.Enabled = cmdDelProjection.Enabled;
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
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void ResetPreviousSelectedPObject(string CODE)
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
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRegister_Click(object sender, EventArgs e)
        {

        }

        private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDevice.Items.Count <= 0) return;
               
                if (CurrDevice_ID != Convert.ToInt32(cboDevice.SelectedValue))
                {
                    CurrDevice_ID = Convert.ToInt32(cboDevice.SelectedValue);
                    ResetInitialize();
                    InitControls();
                    LoadDeviceInfor();
                }

               
            }
            catch
            {
            }
        }
        void LoadDevice()
        {
            DataTable dtDevice;
            try
            {
                 dtDevice = new ModalityController().GetAllData(true).Tables[0];
                if (dtDevice != null)
                {
                    cboDevice.DataSource = dtDevice.DefaultView;
                    cboDevice.DisplayMember = "MODALITY_NAME";
                    cboDevice.ValueMember = "MODALITY_ID";
                    cmdChooseAnatomy.Enabled = dtDevice.Rows.Count > 0;
                    cmdChooseProjection.Enabled = dtDevice.Rows.Count > 0;
                }
            }
            catch
            {
            }
            finally
            {
               
                if (cboDevice.Items.Count > 0) cboDevice.SelectedIndex = 0;
                cboDevice_SelectedIndexChanged(cboDevice, new EventArgs());
            }
        }
        void LoadDeviceInfor()
        {
            try
            {
               
            }
            catch
            {
                
            }
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
                    cboBodySize.DisplayMember = globalVariables.DisplayLanguage == "VN" ? "VN_BODYSIZE_NAME" : "EN_BODYSIZE_NAME";
                    cboBodySize.ValueMember = "BODYSIZE_CODE";
                    nmrKVP.Enabled = dtBodySize.Rows.Count > 0;
                    nmrMA.Enabled = dtBodySize.Rows.Count > 0;
                    nmrMAS.Enabled = dtBodySize.Rows.Count > 0;
                    cmdSaveParam.Enabled = dtBodySize.Rows.Count > 0;
                    
                }
            }
            catch
            {
            }
            finally
            {

                if (cboDevice.Items.Count > 0) cboDevice.SelectedIndex = 0;
                cboDevice_SelectedIndexChanged(cboDevice, new EventArgs());
            }
        }
        private void frm_Anatomy_Projection_Load(object sender, EventArgs e)
        {
            LoadBodySize();
            LoadDevice();
            GetAnatomyProjection();
            InitControls();
        }
        void InitControls()
        {
            try
            {
               
                int i = 0;
                DataRow[] arrDr = AP_DataSource.Select("DEVICE_ID=" + CurrDevice_ID.ToString(),"A_STT");
                foreach (DataRow dr in arrDr)
                {
                    if (!isExistAnatomy(dr["ANATOMY_CODE"].ToString()))
                    {
                        AnatomyControl _Anatomy = new AnatomyControl(dr["ANATOMY_CODE"].ToString(), Utility.sDbnull(dr["VN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.sDbnull(dr["EN_ANATOMY_NAME"].ToString(), "NO NAME"), Utility.Int32Dbnull(dr["A_STT"].ToString(), 0));
                        _Anatomy.Width = pnlAnatomyList.Width-25;
                        _Anatomy._OnClick += new AnatomyControl.OnClick(_NewAnatomy__OnClick);
                        pnlAnatomyList.Controls.Add(_Anatomy);
                        _Anatomy.Size = new Size(191, 64);
                        if (i == 0) _Anatomy._AnatomyObject.PerformClick();
                        i++;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                cmdDelProjection.Enabled = HasSelectedProjection();
                cmdMakeAsEmerency.Enabled = cmdDelProjection.Enabled;
                pnlParams.Enabled = cmdDelProjection.Enabled;
            }
        }
        void ResetInitialize()
        {
            pnlAnatomyList.Controls.Clear();
            pnlProjectionList.Controls.Clear();

        }
        void DeleteFromDataTable(string ACode, string PCode, int Device_ID)
        {
            try
            {
               
                DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString());
                if (arrDR.Length >0)
                {
                    AP_DataSource.Rows.Remove(arrDR[0]);
                }
               
                AP_DataSource.AcceptChanges();
            }
            catch
            {
            }
        }
        void DeleteFromDataTable_A(string ACode, int Device_ID)
        {
            try
            {
            _Begin:
                foreach (DataRow dr in AP_DataSource.Rows)
                {
                    if (dr["ANATOMY_CODE"].ToString().Trim().ToUpper() == ACode && dr["DEVICE_ID"].ToString().Trim().ToUpper() == Device_ID.ToString())
                    {
                        AP_DataSource.Rows.Remove(dr);
                        goto _Begin;
                    }
                }
            }
            catch
            {

            }
            finally
            {
                AP_DataSource.AcceptChanges();
            }
        }
        private void cmdDelProjection_Click(object sender, EventArgs e)
        {
            try
            {
                _Begin:
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
                    {
                       
                        if (new DoctorController().DeleteAP(_Anatomy.Code, _Projection.Code, CurrDevice_ID) == ActionResult.Success)
                        {
                            DeleteFromDataTable(_Anatomy.Code, _Projection.Code, CurrDevice_ID);
                            pnlProjectionList.Controls.Remove(ctr);
                            goto _Begin;
                        }
                        
                    }
                }
                
            }
            catch
            {
               
            }
            finally
            {
                cmdDelProjection.Enabled = HasSelectedProjection();
                cmdMakeAsEmerency.Enabled = cmdDelProjection.Enabled;
                pnlParams.Enabled = cmdDelProjection.Enabled;
            }
        }
        void InsertParamDataTable(string _Anatomy_Code, string _Projection_Code, string BCODE, decimal kvp, int MA, int MAS, int FILM_HD)
        {
            try
            {
                DataRow[] arrDr = APParams_DataSource.Select("ANATOMY_CODE='" + _Anatomy_Code + "' AND PROJECTION_CODE='" + _Projection_Code + "' AND BODYSIZE_CODE='" + BCODE + "'");
                if (arrDr.Length > 0)
                {
                    arrDr[0]["ANATOMY_CODE"] = _Anatomy_Code;
                    arrDr[0]["PROJECTION_CODE"] = _Projection_Code;
                    arrDr[0]["BODYSIZE_CODE"] = BCODE;
                    arrDr[0]["VN_BODYSIZE_NAME"] = cboBodySize.Text;
                    arrDr[0]["EN_BODYSIZE_NAME"] = cboBodySize.Text;
                    arrDr[0]["KVP"] = kvp;
                    arrDr[0]["MA"] = MA;
                    arrDr[0]["MAS"] = MAS;
                    arrDr[0]["FILM_HD"] = FILM_HD;
                   
                }
                else
                {
                    DataRow dr = APParams_DataSource.NewRow();
                    dr["ANATOMY_CODE"] = _Anatomy_Code;
                    dr["PROJECTION_CODE"] = _Projection_Code;
                    dr["VN_BODYSIZE_NAME"] = cboBodySize.Text;
                    dr["EN_BODYSIZE_NAME"] = cboBodySize.Text;
                    dr["BODYSIZE_CODE"] = BCODE;
                    dr["KVP"] = kvp;
                    dr["FILM_HD"] = FILM_HD;
                  
                    dr["MA"] = MA;
                    dr["MAS"] = MAS;
                    APParams_DataSource.Rows.Add(dr);
                }
                APParams_DataSource.AcceptChanges();
            }
            catch
            {
            }
        }
        void UpdateAPDataTable(string _Anatomy_Code, string _Projection_Code, int AUTO_FLIPV, int AUTO_FLIPH, string RCODE)
        {
            try
            {
                DataRow[] arrDr = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy_Code + "' AND PROJECTION_CODE='" + _Projection_Code + "' ");
                if (arrDr.Length > 0)
                {
                    arrDr[0]["AUTO_FLIPV"] = AUTO_FLIPV;
                    arrDr[0]["AUTO_FLIPH"] = AUTO_FLIPH;
                    arrDr[0]["RAD_CODE"] = RCODE;

                }

                AP_DataSource.AcceptChanges();
            }
            catch
            {
            }
        }
        public bool isGCOM_VN = false;
        void AutoSetMinMaxGCOMParams()
        {
        }
        private void cmdSaveParam_Click(object sender, EventArgs e)
        {
            try
            {
                
                string ErrMsg = "";
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
                    {
                        string errMsg = "";
                        int AutoFlipV = chkAutoVFlip.ImageIndex == 0 ? 1 : 0;
                        int AutoFlipH = chkAutoHFlip.ImageIndex == 0 ? 1 : 0;
                        int FILM_HD = CHK_FILMHD.ImageIndex == 0 ? 1 : 0;

                        ActionResult actResult = new DoctorController().InsertAPParam(_Anatomy.Code, _Projection.Code, cboBodySize.SelectedValue.ToString(), nmrKVP.Value, (int)nmrMA.Value, (int)nmrMAS.Value,FILM_HD, txtRadCode.Text.Trim(), CurrDevice_ID,AutoFlipV,AutoFlipH, ref errMsg);
                        if ( actResult== ActionResult.Success)
                        {
                            UpdateAPDataTable(_Anatomy.Code, _Projection.Code, AutoFlipV, AutoFlipH, txtRadCode.Text);
                            InsertParamDataTable(_Anatomy.Code, _Projection.Code, cboBodySize.SelectedValue.ToString(), nmrKVP.Value, (int)nmrMA.Value, (int)nmrMAS.Value, FILM_HD);
                            _Projection.RAD_Code = txtRadCode.Text.Trim();
                            DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='"+_Projection.Code+"' AND DEVICE_ID=" + CurrDevice_ID.ToString());
                            if (arrDR.Length > 0) arrDR[0]["RAD_Code"] = _Projection.RAD_Code;
                            AP_DataSource.AcceptChanges();
                        }
                        else if (actResult == ActionResult.ExistedRecord)
                        {
                            if (errMsg != "")
                            {
                                new frm_LargeMsgBoxOK("Báo lỗi", errMsg, "OK", "CANCEL").ShowDialog();
                                txtRadCode.Focus();
                            }
                        }
                        else
                        {
                            ErrMsg += "(" + _Anatomy.Code + "+" + _Projection.Code + "+" + cboBodySize.SelectedValue.ToString() + "," + nmrKVP.Value.ToString() + "," + nmrMA.Value.ToString() + "," + nmrMAS.Value.ToString() + "\n";
                        }

                    }
                }
                if (ErrMsg != "")
                {
                    new frm_LargeMsgBoxOK("THÔNG BÁO LỖI", ErrMsg.Substring(0, ErrMsg.Length - 1), "OK", "CANCEL").ShowDialog();
                }

            }
            catch
            {

            }
            finally
            {
                cmdDelProjection.Enabled = HasSelectedProjection();
                cmdMakeAsEmerency.Enabled = cmdDelProjection.Enabled;
                pnlParams.Enabled = cmdDelProjection.Enabled;
            }
        }

        private void cboBodySize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_lastSelectedP != null)
                {
                    DataRow[] arrDr = APParams_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + _lastSelectedP.Code + "' AND BODYSIZE_CODE='" + cboBodySize.SelectedValue.ToString() + "'");
                    if (arrDr.Length > 0)
                    {
                        nmrKVP.Value = Convert.ToDecimal(arrDr[0]["KVP"]);
                        nmrMA.Value = Convert.ToInt32(arrDr[0]["MA"]);
                        nmrMAS.Value = Convert.ToInt32(arrDr[0]["MAS"]);
                    }
                    else
                    {
                        nmrKVP.Value = 15;
                        nmrMA.Value = 0;
                        nmrMAS.Value = 1;
                    }
                }
            }
            catch
            {
            }
        }

        private void cmdMakeAsEmerency_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
                    {

                        if (new DoctorController().MakeAPAsEmerencyOrNot(_Anatomy.Code, _Projection.Code, CurrDevice_ID,(short) (_Projection.IsUsed4Emerency?0:1)) == ActionResult.Success)
                        {
                            DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + _Projection.Code + "' AND DEVICE_ID=" + CurrDevice_ID.ToString());
                            foreach (DataRow dr in arrDR)
                            {
                                dr["IS_USED_FOR_EMERENCY"] = (short)(_Projection.IsUsed4Emerency ? 0 : 1);
                            }
                            AP_DataSource.AcceptChanges();
                            ProjectionControl _NewProjection = _Projection.Copy();
                            int idx = pnlProjectionList.Controls.IndexOf(_Projection);
                            pnlProjectionList.Controls.Remove(_Projection);
                            _NewProjection.MakeAsEmerency(!_Projection.IsUsed4Emerency);
                            _NewProjection._OnClick += new ProjectionControl.OnClick(_NewProjection__OnClick);
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

        private void lblMultiCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblMultiCheck.ImageIndex == 1)
                    lblMultiCheck.ImageIndex = 0;
                else
                    lblMultiCheck.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

            try
            {
                if (new frm_LargeMsgBox("Cảnh báo trước khi xóa", "Bạn có muốn xóa vị trí chụp đang chọn hay không?", "ĐỒNG Ý XÓA", "KHÔNG XÓA").ShowDialog() != DialogResult.OK) return;
            _Begin:
                foreach (Control ctr in pnlAnatomyList.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.isPressed)
                    {

                        if (new DoctorController().DeleteAnatomyOfDevice(_Anatomy.Code, CurrDevice_ID) == ActionResult.Success)
                        {
                            pnlProjectionList.Controls.Clear();
                            DeleteFromDataTable_A(_Anatomy.Code, CurrDevice_ID);
                            pnlAnatomyList.Controls.Remove(ctr);
                            goto _Begin;
                        }

                    }
                }

            }
            catch
            {

            }
            finally
            {
                cmdDeleteAnatomy.Enabled = HasSelectedAnatomy();
                cmdDelProjection.Enabled = HasSelectedProjection();
                cmdMakeAsEmerency.Enabled = cmdDelProjection.Enabled;
                pnlParams.Enabled = cmdDelProjection.Enabled;
            }

        }

        private void cmdChooseAnatomy_Click(object sender, EventArgs e)
        {
            frm_ChooseAnatomy _ChooseAnatomy = new frm_ChooseAnatomy();
            if (_ChooseAnatomy.ShowDialog() == DialogResult.OK)
            {
                foreach (Control ctr in _ChooseAnatomy._pnlAnatomy.Controls)
                {
                    AnatomyControl _Anatomy = ctr as AnatomyControl;
                    if (_Anatomy.isPressed)
                    {

                        if (!isExistAnatomy(_Anatomy.Code))
                        {
                            AnatomyControl _NewAnatomy = _Anatomy.Copy();
                            AddRow(_NewAnatomy.Code, "", _NewAnatomy.Vn_Name, _NewAnatomy.En_Name, "", "", CurrDevice_ID, 0);
                            _NewAnatomy._OnClick += new AnatomyControl.OnClick(_NewAnatomy__OnClick);
                            _NewAnatomy.Width = pnlAnatomyList.Width - 10;
                            pnlAnatomyList.Controls.Add(_NewAnatomy);
                            _NewAnatomy.Size = new Size(191, 64);
                            _NewAnatomy._AnatomyObject.PerformClick();

                        }
                    }
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkAutoVFlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoVFlip.ImageIndex == 1)
                {

                    chkAutoVFlip.ImageIndex = 0;
                }
                else
                {

                    chkAutoVFlip.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                
            }
        }

        private void chkAutoHFlip_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoHFlip.ImageIndex == 1)
                {

                    chkAutoHFlip.ImageIndex = 0;
                }
                else
                {

                    chkAutoHFlip.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {

            }
        }

        private void CHK_FILMHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHK_FILMHD.ImageIndex == 1)
                {

                    CHK_FILMHD.ImageIndex = 0;
                }
                else
                {

                    CHK_FILMHD.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {

            }
        }
        void UpdateLSFValue(int LargeFocus)
        {
            try
            {
                foreach (Control ctr in pnlProjectionList.Controls)
                {
                    ProjectionControl _Projection = ctr as ProjectionControl;
                    if (_Projection.isPressed)
                    {
                        if (new DoctorController().UpdateLSFVal(_Anatomy.Code, _Projection.Code, -1, LargeFocus) == ActionResult.Success)
                        {
                            DataRow[] arrDR = AP_DataSource.Select("ANATOMY_CODE='" + _Anatomy.Code + "' AND PROJECTION_CODE='" + _Projection.Code + "'");
                            foreach (DataRow dr in arrDR)
                            {
                                dr["LARGE_FOCUS"] = LargeFocus;
                            }
                        }

                    }
                }
                AP_DataSource.AcceptChanges();
                
            }
            catch
            {
            }
        }
        private void chkIsLargeFocus_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkIsLargeFocus.ImageIndex == 1)
                {

                    chkIsLargeFocus.ImageIndex = 0;
                }
                else
                {

                    chkIsLargeFocus.ImageIndex = 1;
                }
            }
            catch
            {
            }
            finally
            {
                UpdateLSFValue(chkIsLargeFocus.ImageIndex == 0 ? 1 : 0);
            }
        }
    }
}
