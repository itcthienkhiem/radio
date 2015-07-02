using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using VB6 = Microsoft.VisualBasic;
using DROCLibs.Entities;
using VietBaIT.DROC.Objects;
using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects.ObjectInfor;
using System.Collections;
using VietBaIT.Entities;
using System.IO;
namespace VietBaIT.DROC
{
    public partial class frm_Registration : BaseForm
    {
        public DataTable WLDataSource=new DataTable();
        public DataRow dr;
        public bool IsBeginExam=false;
        public bool Hasresult = false;
        public DataGridView grdList;
        public bool EMERGENCY = false;
        private DataSet DataEntity = new DataSet();
        private ArrayList arrProc = new ArrayList();
        public action Act;
        public ToolStripLabel lblmsg1;
        private DataTable PatientEntity = new PatientEntity.PatientEntityDataTable();
        private DataTable RegEntity = new RegEntity.RegEntityDataTable();
        private DataTable RegDetailEntity = new RegDetailEntity.RegDetailEntityDataTable();
        public bool blnRegOK = false;
        int CurrDevice_ID;
        public string BODYSIZE_CODE = "";
        public string BODYSIZE_NAME = "";
        public string oldName = "";
        public string oldAge = "";
        public string oldSex = "";
        public string oldPID = "";
        public string NewName = "";
        public string NewAge = "";
        public string NewSex = "";
        public string NewPID = "";
        public bool AutoGenPID = true;
        public frm_Registration()
        {
            InitializeComponent();
           
        }
        public frm_Registration(int CurrDevice_ID)
        {
            InitializeComponent();
            this.CurrDevice_ID = CurrDevice_ID;
            txtDay.KeyPress += new KeyPressEventHandler(_KeyPress);
            txtMonth.KeyPress += new KeyPressEventHandler(_KeyPress);
            txtYear.KeyPress += new KeyPressEventHandler(_KeyPress);
            txtPName.GotFocus += new EventHandler(txtPName_GotFocus);
            txtPName.LostFocus += new EventHandler(txtPName_LostFocus);
            
            GetData();
            Utility.DisabledTextBox(txtProcedure);
            this.KeyDown += new KeyEventHandler(frm_Registration_KeyDown);
            //mdlStatic._virtualKeyboard = new VirtualKeyboard.DVC_VirtualKeyboard();
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
        }
        void txtPName_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Try2CalculateAgeFromName();
                this.AcceptButton = Act == action.Insert ? cmdBeginExam : cmdRegister;
            }
            catch
            {
            }
        }

        void txtPName_GotFocus(object sender, EventArgs e)
        {
            try
            {
                this.AcceptButton = null;
            }
            catch
            {
            }
        }
        void frm_Registration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (!ExistsVK())
                    ShowVK();
                else
                    KillVK();
            }
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

        void _KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
             e.Handled=   VietBaIT.CommonLibrary.Utility.NumbersOnly(e.KeyChar, txt.Text);
            }
            catch
            {
            }
        }

       
        private void GetData()
        {
            try
            {
                DataTable dtPhysician = new DoctorController().GetAllData().Tables[0];
                dtPhysician.DefaultView.Sort = "Pos";
                cboPhysician.DataSource = dtPhysician.DefaultView;
                cboPhysician.DisplayMember = "Doctor_Name";
                cboPhysician.ValueMember = "Doctor_Code";
                if (cboPhysician.Items.Count > 0) cboPhysician.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        public string ImgPath = "";
        public string oldFolderName = "";
        public string newFolderName = "";
        private void InitView()
        {
            switch (Act)
            {
                    
                case action.Insert:
                    lblAct.Text = "Bạn đang thực hiện thao tác Thêm mới bệnh nhân. Mời bạn thực hiện tiếp...";
                    //Point l1 = cmdBeginExam.Location;
                    //cmdBeginExam.Location = cmdRegister.Location;
                    //cmdRegister.Location = l1;
                    if (AutoGenPID) Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtRegID);
                    string id = Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
                    txtID.Text =AutoGenPID?id:"";// 
                    txtRegID.Text = id;//txtID.Text;
                    txtID.Focus();
                     break;
                case action.Update:
                     lblAct.Text = "Bạn đang thực hiện thao tác Cập nhật bệnh nhân. Mời bạn thực hiện tiếp...";
                     cmdRegister.Text = "Cập nhật";
                     cmdBeginExam.Visible = false;
                     this.AcceptButton = cmdRegister;
                     cmdRegister.DialogResult = DialogResult.OK;
                     lnkAdd.Enabled = !Hasresult;
                     cmdChooseProc.Enabled = lnkAdd.Enabled;
                     //Utility.DisabledTextBox(txtID);
                     Utility.DisabledTextBox(txtRegID);
                     int _SelectedIndex = -1;
                     if (dr != null)
                     {
                         txtID.Text = Utility.sDbnull(dr["PATIENT_CODE"]);
                         if (txtID.Text.Trim().Contains("_"))
                         {
                             txtID.Text = txtID.Text.Trim().Split('_')[0];
                             AutoGenPID = false;
                         }
                         txtRegID.Text = Utility.sDbnull(dr["REG_NUMBER"]);
                         txtPName.Text = Utility.sDbnull(dr["PATIENT_NAME"]);
                         txtAge.Text = Utility.sDbnull(dr["Age"]);
                         oldAge = txtAge.Text.Trim();
                         oldName = Bodau(txtPName.Text.Trim());
                         oldPID = Utility.sDbnull(dr["PATIENT_CODE"]);
                         oldFolderName = ImgPath + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient(dr);
                         txtDay.Text = Utility.sDbnull(dr["sBirth_Date"]).Split('/')[0];
                         txtMonth.Text = Utility.sDbnull(dr["sBirth_Date"]).Split('/')[1];
                         txtYear.Text = Utility.sDbnull(dr["sBirth_Date"]).Split('/')[2];
                         dtpRegDate.Text = Utility.sDbnull(dr["CREATED_DATE"]);
                         txtProcedure.Text = Utility.sDbnull(dr["PROCEDURELIST"]);
                          _SelectedIndex = Utility.GetSelectedIndex(cboPhysician, Utility.sDbnull(dr["PHYSICIAN"]));
                         //Thuc hien lay lai ID cua chi tiet
                         arrProc = new RegController().GetDetailAgain(Convert.ToInt64(dr["REG_ID"]));
                     }
                    cboPhysician.SelectedIndex = _SelectedIndex >= 0 ? _SelectedIndex : (cboPhysician.Items.Count < 0 ? -1 : 0);
                     switch (dr["SEX"].ToString())
                     {
                         case "0":
                             chkNam.ImageIndex = 1;
                             chkNam_Click(chkNam, new EventArgs());
                             oldSex = "M";
                             break;
                         case "1":
                             chkNu.ImageIndex = 1;
                             chkNu_Click(chkNu, new EventArgs());
                             oldSex = "F";
                             break;
                         case "2":
                             chkO.ImageIndex = 1;
                             chkO_Click(chkO, new EventArgs());
                             oldSex = "O";
                             break;
                     }
                     
                     txtPName.Focus();
                     break;
                default:
                    break;
            }
        }
        string SubDirLv1()
        {
            if (txtRegID.Text.Trim() == "" || txtRegID.Text.Trim().Length <= 7) return Utility.GetYYYYMMDD(DateTime.Now);
            return txtRegID.Text.Trim().Substring(0, 8);
        }
        string SubDirLv2_Patient()
        {
            return (AutoGenPID ? Utility.GetValue(txtID.Text, false) : Utility.GetValue(txtID.Text, false) + "_" + Utility.GetValue(txtRegID.Text, false)) + "_" + Bodau(txtPName.Text.Trim()).Replace(txtAge.Text.Trim(), "").Trim().Replace(" ", "_").Trim() + "_" + txtAge.Text;
        }
        string SubDirLv2_Patient(DataRow dr)
        {
            return Utility.sDbnull(dr["PATIENT_CODE"]) + "_" + Bodau(Utility.sDbnull(dr["PATIENT_NAME"]).Trim()).Replace(" ", "_").Replace(Utility.sDbnull(dr["Age"]).Trim(), "") + "_" + Utility.sDbnull(dr["Age"]);
        }
        string SubDirLv2_Patient(string PCode, string PName, string pAge)
        {
            return Utility.sDbnull(PCode) + "_" + Bodau(Utility.sDbnull(PName).Trim()).Replace(" ", "_").Replace(Utility.sDbnull(pAge).Trim(), "") + "_" + Utility.sDbnull(pAge);
        }

        string SubDirLv2_Patient_0Age(DataRow dr)
        {
            return Utility.sDbnull(dr["PATIENT_CODE"]) + "_" + Bodau(Utility.sDbnull(dr["PATIENT_NAME"]).Trim()).Replace(" ", "_").Replace(Utility.sDbnull(dr["Age"]).Trim(), "") + "_0";

        }
        public string Bodau(string s)
        {

            int i = 0;
            string CH = null;
            //###################################################
            if (!string.IsNullOrEmpty(s.Trim()))
            {
                for (i = 1; i <= s.Length; i++)
                {

                    CH = VB6.Strings.Mid(s, i, 1);
                    switch (CH)
                    {
                        case "â":
                        case "ă":
                        case "ấ":
                        case "ầ":
                        case "ậ":
                        case "ẫ":
                        case "ẩ":
                        case "ắ":
                        case "ằ":
                        case "ẵ":
                        case "ẳ":
                        case "ặ":
                        case "á":
                        case "à":
                        case "ả":
                        case "ã":
                        case "ạ":
                            s = s.Replace(CH, "a");
                            break;
                        case "Â":
                        case "Ă":
                        case "Ấ":
                        case "Ầ":
                        case "Ậ":
                        case "Ẫ":
                        case "Ẩ":
                        case "Ắ":
                        case "Ằ":
                        case "Ẵ":
                        case "Ẳ":
                        case "Ặ":
                        case "Á":
                        case "À":
                        case "Ả":
                        case "Ã":
                        case "Ạ":
                            s = s.Replace(CH, "A");
                            break;
                        case "ó":
                        case "ò":
                        case "ỏ":
                        case "õ":
                        case "ọ":
                        case "ô":
                        case "ố":
                        case "ồ":
                        case "ổ":
                        case "ỗ":
                        case "ộ":
                        case "ơ":
                        case "ớ":
                        case "ờ":
                        case "ợ":
                        case "ở":
                        case "ỡ":
                            s = s.Replace(CH, "o");
                            break;
                        case "Ó":
                        case "Ò":
                        case "Ỏ":
                        case "Õ":
                        case "Ọ":
                        case "Ô":
                        case "Ố":
                        case "Ồ":
                        case "Ổ":
                        case "Ỗ":
                        case "Ộ":
                        case "Ơ":
                        case "Ớ":
                        case "Ờ":
                        case "Ợ":
                        case "Ở":
                        case "Ỡ":
                            s = s.Replace(CH, "O");
                            break;
                        case "ư":
                        case "ứ":
                        case "ừ":
                        case "ự":
                        case "ử":
                        case "ữ":
                        case "ù":
                        case "ú":
                        case "ủ":
                        case "ũ":
                        case "ụ":
                            s = s.Replace(CH, "u");
                            break;
                        case "Ư":
                        case "Ứ":
                        case "Ừ":
                        case "Ự":
                        case "Ử":
                        case "Ữ":
                        case "Ù":
                        case "Ú":
                        case "Ủ":
                        case "Ũ":
                        case "Ụ":
                            s = s.Replace(CH, "U");
                            break;
                        case "ê":
                        case "ế":
                        case "ề":
                        case "ệ":
                        case "ể":
                        case "ễ":
                        case "è":
                        case "é":
                        case "ẻ":
                        case "ẽ":
                        case "ẹ":
                            s = s.Replace(CH, "e");
                            break;
                        case "Ê":
                        case "Ế":
                        case "Ề":
                        case "Ệ":
                        case "Ể":
                        case "Ễ":
                        case "È":
                        case "É":
                        case "Ẻ":
                        case "Ẽ":
                        case "Ẹ":
                            s = s.Replace(CH, "E");
                            break;
                        case "í":
                        case "ì":
                        case "ị":
                        case "ỉ":
                        case "ĩ":
                            s = s.Replace(CH, "i");
                            break;
                        case "Í":
                        case "Ì":
                        case "Ỉ":
                        case "Ĩ":
                        case "Ị":
                            s = s.Replace(CH, "I");
                            break;
                        case "ý":
                        case "ỳ":
                        case "ỵ":
                        case "ỷ":
                        case "ỹ":
                            s = s.Replace(CH, "y");
                            break;
                        case "Ý":
                        case "Ỳ":
                        case "Ỵ":
                        case "Ỷ":
                        case "Ỹ":
                            s = s.Replace(CH, "Y");
                            break;
                        case "đ":
                            s = s.Replace(CH, "d");
                            break;
                        case "Đ":
                            s = s.Replace(CH, "D");
                            break;
                    }
                }
            }
            return s;
        }
        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_ChooseProcedureList _newForm = new frm_ChooseProcedureList();
            _newForm.arrProc = arrProc;
            _newForm.ShowDialog();
            if (!_newForm.m_blnCancel)
                txtProcedure.Text = _newForm.ProList;
            arrProc = _newForm.arrProc;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            txtID.Text = Utility.GetYYYYMMDDHHMMSS(DateTime.Now);
            txtRegID.Text = txtID.Text;
        }
        private bool IsValidData()
        {
            try
            {
                if (String.IsNullOrEmpty(txtPName.Text))
                {
                    mdlStatic.SetMsg(lblMsg, "Bạn cần nhập tên Bệnh nhân.", true);
                    txtPName.Focus();
                    return false;
                }
                if (String.IsNullOrEmpty(txtAge.Text))
                {
                    mdlStatic.SetMsg(lblMsg, "Bạn cần nhập tuổi Bệnh nhân.", true);
                    txtAge.Focus();
                    return false;
                }
                try
                {
                    DateTime _dtime = new DateTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text), Convert.ToInt32(txtDay.Text));
                    dtpBirthday.Value = _dtime;
                }
                catch
                {
                    mdlStatic.SetMsg(lblMsg, "Sai thông tin ngày tháng năm sinh của Bệnh nhân. Đề nghị nhập lại.", true);
                    txtDay.Focus();
                    return false;
                }
                //if (String.IsNullOrEmpty(txtProcedure.Text))
                //{
                //    mdlStatic.SetMsg(lblMsg, "Bạn cần chọn vị trí chụp bằng cách nhấn nút Chọn vị trí chụp.", true);
                //    cmdChooseProc.Focus();
                //    return false;
                //}
                if (cboPhysician.Items.Count <= 0)
                {
                    mdlStatic.SetMsg(lblMsg, "Cần khởi tạo danh mục Bác sĩ chỉ định .", true);
                    cboPhysician.Focus();
                    return false;
                }
                if (cboPhysician.SelectedIndex < 0)
                {
                    mdlStatic.SetMsg(lblMsg, "Bạn cần chọn bác sỹ chỉ định...", true);
                    cboPhysician.Focus();
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void cmdRegister_Click(object sender, EventArgs e)
        {
            ExecuteAction();
        }
        private void ExecuteAction()
        {
            //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
            if (!IsValidData())
            {
                return;
            }
            //Gán RoomEntity vào DataEntity
            SetValueforEntity();
            WLRules _BusRule = new WLRules();
            switch (Act)
            {
                case action.Insert:
                    //Gọi nghiệp vụ Insert dữ liệu
                    PatientInfor _PatientInfor = new PatientInfor();
                    RegInfor _RegInfor = new RegInfor();
                    ActionResult InsertResult = _BusRule.Insert(DataEntity, _PatientInfor, _RegInfor);
                    if (InsertResult == ActionResult.Success)//Nếu thành công
                    {
                       
                        //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                        //phải đảm bảo Datasource và RoomEntity có cấu trúc giống nhau mới dùng hàm này
                        DataRow newRow = WLDataSource.NewRow();
                        newRow["Patient_ID"] = _PatientInfor.Patient_ID;
                        newRow["PATIENT_CODE"] =  AutoGenPID ? Utility.GetValue(txtID.Text, false) : Utility.GetValue(txtID.Text, false) + "_" + Utility.GetValue(txtRegID.Text, false);
                        newRow["Patient_Name"] = Utility.GetValue(txtPName.Text, false).Replace(txtAge.Text.Trim(), "").Trim();
                        newRow["PATIENT_NAME_UNSIGNED"] = Bodau(newRow["Patient_Name"].ToString());
                        newRow["PATIENT_NAME_NOSPACE"] = newRow["PATIENT_NAME_UNSIGNED"].ToString().Replace(" ", "");
                        //------------------------------------------------------------------------------
                        newRow["StudyInstanceUID"] = RegEntity.Rows[0]["StudyInstanceUID"];
                        newRow["Birth_Date"] = dtpBirthday.Value;
                        newRow["Age"] = Convert.ToInt32(txtAge.Text.Trim());
                        newRow["sBirth_Date"] = dtpBirthday.Value.ToString("dd/MM/yyyy");
                        newRow["Sex"] = chkNam.ImageIndex == 0 ? 0 : (chkNu.ImageIndex == 0 ? 1 : 2);
                        newRow["EMERGENCY"] = EMERGENCY ? 1 : 0;
                        newRow["REG_ID"] = _RegInfor.REG_ID;
                        newRow["COMPANY_NAME"] = "VIETBAIT DROC";
                        newRow["REG_NUMBER"] = Utility.GetValue(txtRegID.Text, false);
                        //newRow["DESC"] = Utility.GetValue(txtDesc.Text, false);
                        newRow["PROCEDURELIST"] = Utility.GetValue(txtProcedure.Text, false);
                        newRow["PHYSICIAN"] = cboPhysician.SelectedValue.ToString();
                        newRow["Doctor_Name"] = cboPhysician.Text;
                        newRow["CREATED_DATE"] = dtpRegDate.Value;
                        newRow["REGSTATUS"] =0;
                        newRow["sCREATED_DATE"] = dtpRegDate.Value.ToString("dd/MM/yyyy");
                        newRow["SEX_NAME"] = chkNam.ImageIndex == 0 ? MultiLanguage.GetText(globalVariables.DisplayLanguage, "NAM", "MALE") : (chkNu.ImageIndex == 0 ? MultiLanguage.GetText(globalVariables.DisplayLanguage, "NỮ", "FEMALE") : MultiLanguage.GetText(globalVariables.DisplayLanguage, "OTHER", ""));
                        newRow["CanDel"] = 1;
                        newRow["HasProcessed"] = 0;
                        newRow["NoneProcessed"] = 1;
                        newRow["TotalProc"] = 1;
                      
                        if (newRow != null)//99.99% là sẽ !=null
                        {
                            WLDataSource.Rows.Add(newRow);
                            WLDataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colPatient_ID", _PatientInfor.Patient_ID.ToString());
                        mdlStatic.SetMsg(lblMsg, "Thêm mới dữ liệu thành công!", false);
                        blnRegOK = true;
                        this.Close();
                    }
                    else//Có lỗi xảy ra
                    {
                        blnRegOK = false;
                        switch (InsertResult)
                        {
                            case ActionResult.ExistedRecord:
                                mdlStatic.SetMsg(lblMsg, "Đã tồn tại Bệnh nhân có mã: " + _PatientInfor.Patient_Code + ". Đề nghị bạn xem lại", true);
                                Utility.FocusAndSelectAll(txtID);
                                break;
                            default:
                                mdlStatic.SetMsg(lblMsg, "Lỗi trong quá trình thêm mới Bệnh nhân. Liên hệ với VBIT", true);
                                break;
                        }
                    }
                    break;
                case action.Update:
                    
                    //Gọi Business cập nhật dữ liệu
                    ActionResult UpdateResult = _BusRule.Update(DataEntity, Hasresult);
                    if (UpdateResult == ActionResult.Success)//Nếu thành công
                    {
                        newFolderName = ImgPath + @"\" + SubDirLv1() + @"\" + SubDirLv2_Patient();
                        Try2RenameFolderAndFileName();
                        dr["PATIENT_CODE"] = AutoGenPID ? Utility.GetValue(txtID.Text, false) : Utility.GetValue(txtID.Text, false) + "_" + Utility.GetValue(txtRegID.Text, false);
                        dr["Patient_Name"] = Utility.GetValue(txtPName.Text, false).Replace(txtAge.Text.Trim(), "").Trim();
                        dr["PATIENT_NAME_UNSIGNED"] = Bodau(dr["Patient_Name"].ToString());
                        dr["PATIENT_NAME_NOSPACE"] = dr["PATIENT_NAME_UNSIGNED"].ToString().Replace(" ", "");
                        //------------------------------------------------------------------------------
                        dr["Birth_Date"] = dtpBirthday.Value;
                        dr["sBirth_Date"] = dtpBirthday.Value.ToString("dd/MM/yyyy");
                        dr["Age"] = Convert.ToInt32(txtAge.Text.Trim());
                        dr["Sex"] = chkNam.ImageIndex == 0 ? 0 : (chkNu.ImageIndex == 0 ? 1 : 2);
                        dr["PROCEDURELIST"] = Utility.GetValue(txtProcedure.Text, false);
                        dr["PHYSICIAN"] = cboPhysician.SelectedValue.ToString();
                        //dr["DESC"] = Utility.GetValue(txtDesc.Text, false);
                        dr["Doctor_Name"] = cboPhysician.Text;
                        dr["CREATED_DATE"] = dtpRegDate.Value;
                        dr["sCREATED_DATE"] = dtpRegDate.Text;
                        NewPID = dr["PATIENT_CODE"].ToString();
                        dr["SEX_NAME"] = chkNam.ImageIndex == 0 ? MultiLanguage.GetText(globalVariables.DisplayLanguage, "NAM", "MALE") : (chkNu.ImageIndex == 0 ? MultiLanguage.GetText(globalVariables.DisplayLanguage, "NỮ", "FEMALE") : MultiLanguage.GetText(globalVariables.DisplayLanguage, "OTHER", ""));
                        NewAge = txtAge.Text.Trim();
                        NewName =Bodau( txtPName.Text.Trim());
                        NewSex = chkNam.ImageIndex == 0 ? "M" : (chkNu.ImageIndex == 0 ? "F" : "O");
                        WLDataSource.AcceptChanges();
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                        //Utility.GotoNewRow(grdList, "colPatient_ID", Utility.sDbnull(dr["Patient_ID"]));
                        mdlStatic.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.", false);
                        blnRegOK = true;
                        this.Close();
                    }
                    else//Có lỗi xảy ra
                    {
                        blnRegOK = false;
                        switch (UpdateResult)
                        {
                            case ActionResult.Error:
                                mdlStatic.SetMsg(lblMsg, "Lỗi khi cập nhật Bệnh nhân. Liên hệ với VBIT", true);
                                break;
                            default:
                                mdlStatic.SetMsg(lblMsg, "Lỗi khi cập nhật Bệnh nhân. Liên hệ với VBIT", true);
                                break;
                        }
                    }
                    break;

                case action.Delete:
                    
                    break;
                default:
                    break;
            }
        }
        void Try2RenameFolderAndFileName()
        {
            try
            {
                if (oldFolderName.Trim().ToUpper() != newFolderName.Trim().ToUpper())
                {
                    Directory.Move(oldFolderName, newFolderName);
                }
            }
            catch
            {
            }
        }
       
        private void SetValueforEntity()
        {
            Utility.ResetEntity(ref DataEntity);
            Utility.ResetEntity(ref PatientEntity);
            Utility.ResetEntity(ref RegDetailEntity);
            Utility.ResetEntity(ref RegEntity);
            string StudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
            //Create new Row
            DataRow dr0 = PatientEntity.NewRow();
            if (Act == action.Insert)
            {
                
                dr0["Patient_ID"] = -1;
            }
            else dr0["Patient_ID"] = Convert.ToInt16(dr["Patient_ID"]);
            dr0["PATIENT_CODE"] = AutoGenPID ? Utility.GetValue(txtID.Text, false) : Utility.GetValue(txtID.Text, false) + "_" + Utility.GetValue(txtRegID.Text, false);
            dr0["Patient_Name"] = Utility.GetValue(txtPName.Text, false).Replace(txtAge.Text.Trim(), "").Trim();
            dr0["PATIENT_NAME_UNSIGNED"] = Bodau(dr0["Patient_Name"].ToString());
            dr0["PATIENT_NAME_NOSPACE"] = dr0["PATIENT_NAME_UNSIGNED"].ToString().Replace(" ", "");
            dr0["Birth_Date"] = dtpBirthday.Value;
            dr0["Age"] = Utility.Int32Dbnull(txtAge.Text);
            dr0["sBirth_Date"] = dtpBirthday.Value.ToString("dd/MM/yyyy");
            //dr0["Doctor_Name"] = cboPhysician.Text;
            dr0["Sex"] = chkNam.ImageIndex == 0 ? 0 : (chkNu.ImageIndex == 0 ? 1 : 2);
            dr0["EMERGENCY"] = EMERGENCY ? 1 : 0;
            dr0["CREATED_BY"] = "DROC"; 
            PatientEntity.Rows.Add(dr0);
            PatientEntity.AcceptChanges();
          //Tao thong tin dang ky chup
            DataRow dr1 = RegEntity.NewRow();
            if (Act == action.Insert)
            {
                dr1["StudyInstanceUID"] = StudyInstanceUID;
                dr1["REG_ID"] = -1;
            }
            else dr1["REG_ID"] = Convert.ToInt64(dr["REG_ID"]);
            dr1["REG_NUMBER"] = Utility.GetValue(txtRegID.Text, false);
            dr1["PATIENT_ID"] = dr0["Patient_ID"];
            dr1["DESC"] = Utility.GetValue(txtDesc.Text, false);

            dr1["PROCEDURELIST"] = Utility.GetValue(txtProcedure.Text, false);
            dr1["PHYSICIAN"] = cboPhysician.SelectedValue.ToString();
            dr1["CREATED_DATE"] = dtpRegDate.Value;
            
            RegEntity.Rows.Add(dr1);
            RegEntity.AcceptChanges();
            if (Act == action.Insert)
            {
                DataTable dtDefaultAP = new ProcedureController().GetEmerencyData().Tables[0];
                //Lọc chỉ các dịch vụ được gửi từ worklistServer
                if (dtAP == null && dtDefaultAP != null && dtDefaultAP.Columns.Count > 0)
                    dtAP = dtDefaultAP.Clone();
                if (dtAP != null && dtDefaultAP != null && dtAP.Columns.Count > 0 && dtDefaultAP.Columns.Count > 0)
                {
                    try
                    {
                        foreach (DataRow drDefaultAP in dtDefaultAP.Rows)
                        {
                            if (dtAP.Select("ANATOMY_CODE='" + drDefaultAP["ANATOMY_CODE"].ToString() + "' AND PROJECTION_CODE='" + drDefaultAP["PROJECTION_CODE"].ToString() + "'").Length <= 0)
                                dtAP.ImportRow(drDefaultAP);
                        }
                    }
                    catch
                    {
                    }
                }
                //Tao thong tin dang ky chup chi tiet
                if (dtAP != null)
                {
                    int CurrIdx = 1;
                    string SeriesInstanceUID = "";
                    foreach (DataRow drAP in dtAP.Rows)
                    {
                        DataRow dr2 = RegDetailEntity.NewRow();
                        SeriesInstanceUID = StudyInstanceUID + "." + CurrIdx.ToString();
                        dr2["DETAIL_ID"] = -1;
                        dr2["UsingGrid"] = 0;
                        dr2["StudyInstanceUID"] = StudyInstanceUID;
                        //SeriesInstanceUID=StudyInstanceUID+Số thứ tự dịch vụ trong lần đăng ký đó
                        dr2["SeriesInstanceUID"] = SeriesInstanceUID;
                        //SOPInstanceUID=SeriesInstanceUID+Số lần chụp của dịch vụ đó
                        dr2["SOPInstanceUID"] = SeriesInstanceUID + ".1";
                        dr2["REG_ID"] = dr1["REG_ID"];
                        dr2["ANATOMY_CODE"] = drAP["ANATOMY_CODE"];
                        dr2["PROJECTION_CODE"] = drAP["PROJECTION_CODE"];
                        dr2["BODYSIZE_CODE"] = this.BODYSIZE_CODE;
                        //dr2["DISPLAY_NAME"] = arrProc[i].ToString().Split('#')[1];
                        //dr2["STANDARD_NAME"] = Convert.ToInt16(cboPhysician.SelectedValue);
                        dr2["STATUS"] = 0;
                        RegDetailEntity.Rows.Add(dr2);
                        CurrIdx++;
                        RegDetailEntity.AcceptChanges();
                    }
                }
            }
            DataEntity.Tables.Add(PatientEntity);
            DataEntity.Tables.Add(RegEntity);
            DataEntity.Tables.Add(RegDetailEntity);
        }
        string GetNextSeriesInstanceUID(string StudyInstanceUID)
        {
            try
            {
                int lastIdx =Convert.ToInt32( StudyInstanceUID.Substring(StudyInstanceUID.LastIndexOf(".")));
                lastIdx++;
                return StudyInstanceUID + "." + lastIdx.ToString();
            }
            catch
            {
                return StudyInstanceUID + ".1";
            }

        }

        string GetProcedure()
        {
            try
            {
                string s="";
                foreach(DataRow dr in dtAP.Rows)
                {
                    s += dr["ANATOMY_CODE"].ToString() + "/" + dr["PROJECTION_CODE"].ToString()+";";
                }
                if (s.Length > 0) s = s.Substring(0, s.Length - 1);
                return s;
            }
            catch
            {
                return "";
            }
        }
        private void frm_Registration_Load(object sender, EventArgs e)
        {
            if (AutoGenPID) cmdRefresh.Visible = true;
            else cmdRefresh.Visible = false;
            InitView();
            lblKSK.ImageIndex = globalVariables.KSK ? 1 : 0;
            txtAge.Enabled = lblKSK.ImageIndex == 0;
            txtYear.Enabled = lblKSK.ImageIndex == 1;
            cmdRefresh.Visible = AutoGenPID;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmdBeginExam_Click(object sender, EventArgs e)
        {
            ExecuteAction();
            if (blnRegOK)
            {
                IsBeginExam = true;
                //mdlStatic.mainform.ModifyWorkListButtons();
                //mdlStatic.mainform.BeginExam();
                blnRegOK = false;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        DataTable dtAP;
        private void cmdFindPro_Click(object sender, EventArgs e)
        {
            //frm_ChooseProcedureList _newForm = new frm_ChooseProcedureList();
            //_newForm.arrProc = arrProc;
            //_newForm.ShowDialog();
            //if (!_newForm.m_blnCancel)
            //    txtProcedure.Text = _newForm.ProList;
            //arrProc = _newForm.arrProc;
            frm_Choose_Anotomy_Projection _Choose_Anotomy_Projection = new frm_Choose_Anotomy_Projection(-1);
            if (_Choose_Anotomy_Projection.ShowDialog() == DialogResult.OK)
            {
                this.BODYSIZE_CODE = _Choose_Anotomy_Projection.BODYSIZE_CODE;
                this.BODYSIZE_NAME = _Choose_Anotomy_Projection.BODYSIZE_NAME;
                dtAP = _Choose_Anotomy_Projection.AP_DataSource.Select("CHON=1").CopyToDataTable();
                txtProcedure.Text = GetProcedure();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Utility.IsNumeric(txtAge.Text.Trim()))
                {
                    txtDay.Text = "01";
                    txtMonth.Text = "01";
                    txtYear.Text = DateTime.Now.AddYears(-1 * Convert.ToInt32(txtAge.Text.Trim())).Year.ToString();
                }
            }
            catch
            {
            }
        }
        void Try2CalculateAgeFromName()
        {
            try
            {
                string _NUM = "0123456789";
                string[] _words = txtPName.Text.Split(' ');
                string Age = "";
                string Age2 = "";
                foreach (string s in _words)
                {
                    //Chỉ phân tích các từ có chữ số
                    if (s.IndexOfAny(_NUM.ToCharArray()) >= 0)
                    {
                        if (Utility.IsNumeric(s))
                        {
                            Age += s.Trim();
                        }
                        for (int i = 0; i <= s.Length - 1; i++)
                        {
                            if (Utility.IsNumeric(s.Substring(i, 1)))
                                Age2 += s.Substring(i, 1).Trim();
                        }
                    }
                }
                if (!Utility.IsNumeric(Age))
                {
                    if (!Utility.IsNumeric(Age2))
                        Age = "0";
                    else
                        Age = Age2;
                }
                if (Act == action.Insert) txtAge.Text = Age;
                if (Act == action.Update)
                    if (Age != "0") txtAge.Text = Age;
                    else txtAge.Text = oldAge;
            }
            catch
            {
            }
            finally
            {
                txtPName.Text = txtPName.Text.Replace(txtAge.Text.Trim(), "").Trim();
            }
        }
        private void txtPName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chkNam_Click(object sender, EventArgs e)
        {
            try
            {
                chkNu.ImageIndex = 1;
                chkO.ImageIndex = 1;
                if (chkNam.ImageIndex == 1)
                {
                    chkNam.ImageIndex = 0;
                }
                else
                    chkNam.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void chkNu_Click(object sender, EventArgs e)
        {
            try
            {
                chkNam.ImageIndex = 1;
                chkO.ImageIndex = 1;
                if (chkNu.ImageIndex == 1)
                {
                    chkNu.ImageIndex = 0;
                }
                else
                    chkNu.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void chkO_Click(object sender, EventArgs e)
        {
            try
            {
                chkNam.ImageIndex = 1;
                chkNu.ImageIndex = 1;
                if (chkO.ImageIndex == 1)
                {
                    chkO.ImageIndex = 0;
                }
                else
                    chkO.ImageIndex = 1;
            }
            catch
            {
            }
        }

        private void lblKSK_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblKSK.ImageIndex == 1)
                {
                    lblKSK.ImageIndex = 0;
                    txtAge.Enabled = lblKSK.ImageIndex == 0;
                    txtYear.Enabled = lblKSK.ImageIndex == 1;
                    txtAge.SelectAll();
                    txtAge.Focus();
                }
                else
                {
                    lblKSK.ImageIndex = 1;
                    txtAge.Enabled = lblKSK.ImageIndex == 0;
                    txtYear.Enabled = lblKSK.ImageIndex == 1;
                    txtYear.SelectAll();
                    txtYear.Focus();
                }
            }
            catch
            {
            }
            finally
            {
                
                globalVariables.KSK = lblKSK.ImageIndex == 1;
            }
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Utility.IsNumeric(txtYear.Text.Trim()))
                {
                    txtDay.Text = "01";
                    txtMonth.Text = "01";
                    txtAge.Text = DateTime.Now.AddYears(-1 * Convert.ToInt32(txtYear.Text.Trim())).Year.ToString();
                }
            }
            catch
            {
            }
        }
    }
}
