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

using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects.ObjectInfor;
namespace VietBaIT.DROC
{
    public partial class frm_Modalities : BaseForm
    {
        #region "Attributes"
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
       
        /// <summary>
        /// Khai báo Entity chứa dữ liệu
        /// </summary>
        private DataTable ModalityEntity = new ModalityEntity.ModalityEntityDataTable();
        //Khai báo gói thông tin chứa ModalityEntity và Header truyền lên BL để xử lý nghiệp vụ
        private DataSet DataEntity = new DataSet();
        /// <summary>
        /// Datasource là danh sách Modality hiển thị trên lưới
        /// </summary>
        private DataTable DataSource = new DataTable();
        /// <summary>
        /// Có cho phép phản ánh dữ liệu trên lưới vào các Control hay không? 
        /// Mục đích khi nhấn Insert, Delete thì khi chọn trên lưới sẽ ko thay đổi dữ liệu trong các Control bên dưới.
        /// Ở chế độ bình thường thì khi chọn trên lưới sẽ phản ánh dữ liệu xuống các Control để sẵn sàng thao tác.
        /// </summary>
        private bool AllowCurrentCellChangedOnGridView = true;
        /// <summary>
        /// Thao tác đang thực hiện là gì: Insert, Delete, Update hay Select...
        /// </summary>
        private action Act;
        TraceInfor Trace;
        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool bLoaded = false;
        private Int16 OldPos = 0;
        #endregion
        public frm_Modalities()
        {
            InitializeComponent();
          
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
        }
        private void LoadData()
        {

        }
        private void frm_Modalities_Load(object sender, EventArgs e)
        {
            InitView();
        }
        #region "Entity setting"
        /// <summary>
        /// Thiết lập giá trị cho Entity 
        /// </summary>
        private void SetValueforEntity()
        {
            Utility.ResetEntity(ref DataEntity);
            Utility.ResetEntity(ref ModalityEntity);
            //Create new Row
            DataRow dr = ModalityEntity.NewRow();
            if (Act == action.Insert) dr["Modality_ID"] = -1;
            else dr["Modality_ID"] = Convert.ToInt16(txtID.Text.Trim());
            dr["Modality_Code"] = Utility.GetValue(txtCode.Text, false);
            dr["Modality_Name"] = Utility.GetValue(txtName.Text, false);
            dr["Mod_Type_ID"] = Convert.ToInt16(cboModType.SelectedValue);
            dr["Mod_Type_Name"] = cboModType.Text;
            dr["Manufacture_ID"] = Convert.ToInt16(cboManufacture.SelectedValue);
            dr["Manufacture_Name"] = cboManufacture.Text;
            dr["Country_ID"] = cboCountry.SelectedValue.ToString();
            dr["Country_Name"] = cboCountry.Text;
            dr["Room_ID"] = Convert.ToInt16(cboRoom.SelectedValue);
            dr["Room_Name"] = cboRoom.Text;

            dr["IP_ADDRESS"] = Utility.GetValue(txtIPAddress.Text, false);
            dr["AE_TITLE"] = Utility.GetValue(txtAETitle.Text, false);
            dr["RESTRICTION"] = Utility.GetValue(txtRestriction.Text, false);
            dr["Desc"] = Utility.GetValue(txtDesc.Text, false);
            dr["Pos"] = Convert.ToInt16(txtPos.Text);
            dr["PORT_NUM"] = Convert.ToInt16(txtPortNum.Text);
            dr["STATUS"] = chkStatus.Checked ? 0 : 1;
             dr["DEVICE_WORKLIST"] = chkDeviceWorkList.Checked ? 1 : 0;
            //Set giá trị cho cột STT cũ để tráo đổi số TT với bản ghi có STT=dr["Pos"]
            dr["OldPos"] = OldPos;
            dr["IMGW"] = nmrWidth.Value;
            dr["IMGH"] = nmrHeight.Value;
            dr["AUTO_FLIPH"] = chkFlipH.ImageIndex == 0;
            dr["AUTO_FLIPV"] = chkFlipV.ImageIndex == 0;
            ModalityEntity.Rows.Add(dr);
            ModalityEntity.AcceptChanges();
            //Add ModalityEntity into DataEntity
            DataEntity.Tables.Add(ModalityEntity);
        }
        #endregion

        #region "Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        //Khởi tạo View: Khởi tạo trạng thái các Control, khởi tạo dữ liệu ban đầu...
        //Implements from IModalityView
        public void InitView()
        {
            cmdAddCountry.Visible = !m_blnCallFromMenu;
            cmdAddDepart.Visible = !m_blnCallFromMenu;
            cmdAddDeviceType.Visible = !m_blnCallFromMenu;
            cmdAddManufacture.Visible = !m_blnCallFromMenu;
            cmdAddCountry.Click+=new EventHandler(cmdAddCountry_Click);
            cmdAddDepart.Click+=new EventHandler(cmdAddDepart_Click);
            cmdAddDeviceType.Click+=new EventHandler(cmdAddDeviceType_Click);
            cmdAddManufacture.Click+=new EventHandler(cmdAddManufacture_Click);
            cmdCancel.Click+=new EventHandler(cmdCancel_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            Initialize();
            //Lấy về danh sách các thiết bị để hiển thị lên DataGridView
            GetData();
            //Sau khi Binding dữ liệu vào GridView thì mới cho phép thực hiện lệnh trong sự kiện CurrentCellChanged
            bLoaded = true;
            //Gọi sự kiện CurrentCellChanged để hiển thị dữ liệu từ trên lưới xuống Controls
            CurrentCellChanged();
            //Thiết lập giá trị mặc định của DMLType
            Act = action.FirstOrFinished;
            //Thiết lập các giá trị mặc định cho các Control
            SetControlStatus();
            
            mdlStatic.SetMsg(lblMsg, "Mời bạn thao tác", false);
        }

        void grdList_CurrentCellChanged(object sender, EventArgs e)
        {
            CurrentCellChanged();
        }
        /// <summary>
        /// Thiết lập các giá trị mặc định cho class
        /// </summary>
        private void Initialize()
        {
           
            //Khởi tạo tính năng tùy biến cấu hình ẩn hiện cột trên DataGrid Chỉ có Form nào chứa DataGridView thì mới cần
            //khai báo mục này
           // CommonLibrary.GridViewUtils _GridViewUtils = new CommonLibrary.GridViewUtils(this, globalModule.Branch_ID, globalModule.UserName, CommonLibrary.globalModule.AssName, true);
            ////Tạo Trace cho chức năng này
            //if (MustSaveTrace == 1)
            //    Trace = new TraceInfor(globalModule.Branch_ID, globalModule.UserName, System.DateTime.Now.ToShortDateString(), Utility.GetIPAddress(), CommonLibrary.globalModule.AssName, globalModule.SubSystemName, globalModule.FunctionID, globalModule.FunctionName, Utility.GetComputerName(), Utility.GetAccountName());
            //else
            //    Trace = null;
        }
        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi đóng gói dữ liệu vào Entity
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (cboModType.Items.Count<=0)
            {
                mdlStatic.SetMsg(lblMsg, "Cần khởi tạo danh mục Loại thiết bị trước khi thực hiện thêm thiết bị.", true);
                cboModType.Focus();
                return false;
            }
            if (cboCountry.Items.Count <= 0)
            {
                mdlStatic.SetMsg(lblMsg, "Cần khởi tạo danh mục Nước SX trước khi thực hiện thêm thiết bị.", true);
                cboCountry.Focus();
                return false;
            }
            if (cboManufacture.Items.Count <= 0)
            {
                mdlStatic.SetMsg(lblMsg, "Cần khởi tạo danh mục Hãng SX trước khi thực hiện thêm thiết bị.", true);
                cboManufacture.Focus();
                return false;
            }
            if (cboRoom.Items.Count <= 0)
            {
                mdlStatic.SetMsg(lblMsg, "Cần khởi tạo danh mục Phòng ban đặt thiết bị trước khi thực hiện thêm thiết bị.", true);
                cboRoom.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtCode.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập mã thiết bị.",true);
                txtCode.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtPos.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Số thứ tự phải là chữ số.",true);
                txtPos.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtName.Text))
            {

               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập tên thiết bị.",true);
                txtName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtIPAddress.Text))
            {

                mdlStatic.SetMsg(lblMsg, "Bạn cần nhập địa chỉ IP của thiết bị.", true);
                txtIPAddress.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtPortNum.Text))
            {
                mdlStatic.SetMsg(lblMsg, "Cổng kết nối phải là chữ số.", true);
                txtPortNum.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtAETitle.Text))
            {

                mdlStatic.SetMsg(lblMsg, "Bạn cần nhập AETitle của thiết bị.", true);
                txtAETitle.Focus();
                return false;
            }
            return true;
        }
        public void PrepareInsertEvent()
        {
            //Đặt mã nghiệp vụ cần thực hiện là Insert. 
            //Chú ý luôn set Giá trị này trước khi gọi hàm SetControlStatus()
            Act = action.Insert;
            //Đưa trạng thái các Control về trạng thái cho phép thêm mới
            SetControlStatus();
        }
        public void PrepareUpdateEvent()
        {
            //Đặt mã nghiệp vụ cần thực hiện là Update
            //Chú ý luôn set Giá trị này trước khi gọi hàm SetControlStatus()
            Act = action.Update;
            //Đưa trạng thái các Control về trạng thái cho phép cập nhật
            SetControlStatus();
        }
        public void PrepareDeleteEvent()
        {
            //Kiểm tra nếu xóa thành công thì thiết lập lại trạng thái các Control
            Act = action.Delete;
            ExecuteAction();
            Act = action.FirstOrFinished;
            SetControlStatus();
        }
        public void Save()
        {
            ExecuteAction();
        }
       
        public void Print()
        {
        }
        /// <summary>
        /// Thiết lập trạng thái các Control trên Form theo thao tác nghiệp vụ cần thực hiện
        /// Insert, Update hoặc Delete...
        /// </summary>
        private void SetControlStatus()
        {
            switch (Act)
            {
                case action.Insert:
                    //Cho phép nhập liệu mã thiết bị,vị trí, tên thiết bị và mô tả thêm
                    txtID.Text = "Tự sinh";
                    txtCode.Clear();
                    txtName.Clear();
                    txtDesc.Clear();
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtCode);

                    chkFlipH.Enabled = true;
                    chkFlipV.Enabled = true;
                    chkFlipH.ImageIndex = 1;
                    chkFlipV.ImageIndex = 1;
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtIPAddress);
                    Utility.EnabledTextBox(txtPortNum);
                    Utility.EnabledTextBox(txtAETitle);
                    Utility.EnabledTextBox(txtRestriction);
                    Utility.EnabledComboBox(cboModType);
                    Utility.EnabledComboBox(cboCountry);
                    Utility.EnabledComboBox(cboManufacture);
                    Utility.EnabledComboBox(cboRoom);
                    Utility.EnabledTextBox(txtDesc);
                    nmrHeight.Enabled = true;
                    nmrWidth.Enabled = true;
                    txtPos.Text = Utility.getNextMaxID("Pos", "L_Modalities").ToString();
                    OldPos = Convert.ToInt16(txtPos.Text);
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Không cho phép nhấn Insert, Update,Delete
                    cmdInsert.Enabled = false;
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    cmdPrint.Enabled = false;
                    //Nút thoát biến thành nút hủy
                    cmdCancel.Enabled = true;
                    //--------------------------------------------------------------
                    //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                    AllowCurrentCellChangedOnGridView = false;
                    //Tự động Focus đến mục ID để người dùng nhập liệu
                    mdlStatic.SetMsg(lblMsg, "Mời bạn nhập thông tin để thêm mới", false);
                    txtCode.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã thiết bị
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên thiết bị và mô tả thêm
                    chkFlipH.Enabled = true;
                    chkFlipV.Enabled = true;
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtDesc);
                    Utility.EnabledTextBox(txtIPAddress);
                    Utility.EnabledTextBox(txtPortNum);
                    Utility.EnabledTextBox(txtAETitle);
                    Utility.EnabledTextBox(txtRestriction);
                    Utility.EnabledComboBox(cboModType);
                    Utility.EnabledComboBox(cboCountry);
                    Utility.EnabledComboBox(cboManufacture);
                    Utility.EnabledComboBox(cboRoom);
                    Utility.EnabledTextBox(txtPos);
                    OldPos = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "colPos", grdList.CurrentRow.Index), 0);
                    nmrHeight.Enabled = true;
                    nmrWidth.Enabled = true;
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Không cho phép nhấn Insert, Update,Delete
                    cmdInsert.Enabled = false;
                    cmdPrint.Enabled = false;
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;
                    //Cho phép nhấn nút Ghi
                    cmdSave.Enabled = true;
                    //Nút thoát biến thành nút hủy
                    cmdCancel.Enabled = true;
                    //--------------------------------------------------------------
                    //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                    AllowCurrentCellChangedOnGridView = false;
                    //Tự động Focus đến mục Position để người dùng nhập liệu
                    mdlStatic.SetMsg(lblMsg, "Mời bạn nhập thông tin để cập nhật", false);
                    txtPos.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã thiết bị, tên thiết bị và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtCode);
                    Utility.DisabledTextBox(txtName);
                    Utility.DisabledTextBox(txtDesc);
                    Utility.DisabledTextBox(txtPos);
                    Utility.DisabledTextBox(txtIPAddress);
                    Utility.DisabledTextBox(txtPortNum);
                    Utility.DisabledTextBox(txtAETitle);
                    Utility.DisabledTextBox(txtRestriction);
                    Utility.DisabledComboBox(cboModType);
                    Utility.DisabledComboBox(cboCountry);
                    Utility.DisabledComboBox(cboManufacture);
                    Utility.DisabledComboBox(cboRoom);
                     chkFlipH.Enabled = false;
                    chkFlipV.Enabled = false;
                    nmrHeight.Enabled = false;
                    nmrWidth.Enabled = false;
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                    //Cho phép thêm mới
                    cmdInsert.Enabled = true;
                    //Tùy biến nút Update và Delete tùy theo việc có hay không dữ liệu trên lưới
                    cmdUpdate.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdDelete.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdPrint.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdSave.Enabled = false;
                    //Nút Hủy biến thành nút thoát
                    cmdCancel.Enabled = false;
                    //--------------------------------------------------------------
                    //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                    AllowCurrentCellChangedOnGridView = true;
                    //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                    CurrentCellChanged();
                    //mdlStatic.SetMsg(lblMsg, "Mời bạn thao tác", false);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Xử lý sự kiện CurrentCellChanged của DataGridView
        /// Đưa dữ liệu đang chọn từ GridView vào các Controls để người dùng sẵn sàng thao tác Delete hoặc Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentCellChanged()
        {
            try
            {
                //Chỉ cho phép khi AllowCurrentCellChangedOnGridView=true và lưới có dữ liệu
                if (bLoaded && AllowCurrentCellChangedOnGridView && grdList.RowCount > 0 && grdList.CurrentRow != null)
                {
                    if (grdList.RowCount > 0 && grdList.CurrentRow != null)
                    {
                        txtID.Text = Utility.GetValueFromGridColumn(grdList, "colModality_ID", grdList.CurrentRow.Index);
                        txtCode.Text = Utility.GetValueFromGridColumn(grdList, "colModality_Code", grdList.CurrentRow.Index);
                        txtName.Text = Utility.GetValueFromGridColumn(grdList, "colModality_Name", grdList.CurrentRow.Index);
                        txtDesc.Text = Utility.GetValueFromGridColumn(grdList, "colDesc", grdList.CurrentRow.Index);
                        txtPos.Text = Utility.GetValueFromGridColumn(grdList, "colPos", grdList.CurrentRow.Index);
                        txtAETitle.Text = Utility.GetValueFromGridColumn(grdList, "colAE_Title", grdList.CurrentRow.Index);
                        txtRestriction.Text = Utility.GetValueFromGridColumn(grdList, "colRestriction", grdList.CurrentRow.Index);
                        txtIPAddress.Text = Utility.GetValueFromGridColumn(grdList, "colIP_Address", grdList.CurrentRow.Index);
                        txtPortNum.Text = Utility.GetValueFromGridColumn(grdList, "colPort_Num", grdList.CurrentRow.Index);
                        chkDeviceWorkList.Checked = Utility.GetValueFromGridColumn(grdList, "colDEVICE_WORKLIST", grdList.CurrentRow.Index)=="1"?true:false;
                        chkStatus.Checked = Utility.GetValueFromGridColumn(grdList, "colSTATUS", grdList.CurrentRow.Index)=="1"?false:true;
                        short Department_ID = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "colMod_Type_ID", grdList.CurrentRow.Index), 0);
                        cboModType.SelectedIndex = Utility.GetSelectedIndex(cboModType, Department_ID.ToString());
                        string Country_ID = Utility.sDbnull(Utility.GetValueFromGridColumn(grdList, "colCountry_ID", grdList.CurrentRow.Index), 0);
                        cboCountry.SelectedIndex = Utility.GetSelectedIndex(cboCountry, Country_ID.ToString());

                        short Manufacture_ID = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "colManufacture_ID", grdList.CurrentRow.Index), 0);
                        cboManufacture.SelectedIndex = Utility.GetSelectedIndex(cboManufacture, Manufacture_ID.ToString());
                        nmrHeight.Value = Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdList, "colIMGH", grdList.CurrentRow.Index), 0);
                        nmrWidth.Value = Utility.DecimaltoDbnull(Utility.GetValueFromGridColumn(grdList, "colIMGW", grdList.CurrentRow.Index), 0);
                        short Room_ID = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "colRoom_ID", grdList.CurrentRow.Index), 0);
                        cboRoom.SelectedIndex = Utility.GetSelectedIndex(cboRoom, Room_ID.ToString());

                        chkFlipH.ImageIndex = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "AUTO_FLIPH", grdList.CurrentRow.Index), 0) == 1 ? 0 : 1;
                        chkFlipV.ImageIndex = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "AUTO_FLIPV", grdList.CurrentRow.Index), 0) == 1 ? 0 : 1;
                    }
                    else
                    {
                        txtID.Text = "";
                        txtCode.Text = "";
                        txtName.Text ="";
                        txtDesc.Text ="0";
                        txtPos.Text = "";

                        txtAETitle.Clear();
                        txtRestriction.Clear();
                        txtIPAddress.Clear();
                        txtPortNum.Clear();
                        chkDeviceWorkList.Checked = true;
                        chkStatus.Checked = false;
                        chkFlipH.ImageIndex = 1;
                        chkFlipV.ImageIndex = 1;
                    }
                }
               
            }
            catch
            {
            }
        }
        #endregion

        #region "Insert, Delete, Update,Select,..."
        private void ExecuteAction()
        {
            try
            {
                //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
                if (!IsValidData())
                {
                    return;
                }
                //Gán ModalityEntity vào DataEntity
                SetValueforEntity();

                //Khởi tạo BusinessRule để xử lý nghiệp vụ
                ModalityInfor Infor = new ModalityInfor();
                Utility.MapValueFromEntityIntoObjectInfor(Infor, ModalityEntity);
                ModalityController _BusRule = new ModalityController(Infor);
                switch (Act)
                {
                    case action.Insert:
                        //Gọi nghiệp vụ Insert dữ liệu
                        ActionResult InsertResult = _BusRule.Insert();
                        if (InsertResult == ActionResult.Success)//Nếu thành công
                        {
                            //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                            //phải đảm bảo Datasource và ModalityEntity có cấu trúc giống nhau mới dùng hàm này
                            DataRow dr = Utility.CopyData(ModalityEntity.Rows[0], DataSource);
                            dr["Modality_ID"] = Infor.Modality_ID;
                            if (dr != null)//99.99% là sẽ !=null
                            {
                                DataSource.Rows.Add(dr);
                                DataSource.AcceptChanges();
                            }
                            //Return to the InitialStatus
                            Act = action.FirstOrFinished;
                            //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                            Utility.GotoNewRow(grdList, "colModality_ID", Infor.Modality_ID.ToString());
                            mdlStatic.SetMsg(lblMsg, "Thêm mới dữ liệu thành công!", false);

                            SetControlStatus();
                            CurrentCellChanged();
                        }
                        else//Có lỗi xảy ra
                        {
                            switch (InsertResult)
                            {
                                case ActionResult.ExistedRecord:
                                    mdlStatic.SetMsg(lblMsg, "Đã tồn tại thiết bị có mã: " + txtCode.Text.Trim() + ". Đề nghị bạn xem lại", true);
                                    Utility.FocusAndSelectAll(txtCode);
                                    break;
                                default:
                                    mdlStatic.SetMsg(lblMsg, "Lỗi trong quá trình thêm mới thiết bị. Liên hệ với VBIT", true);
                                    break;
                            }
                        }
                        break;
                    case action.Update:
                        //Gọi Business cập nhật dữ liệu
                        ActionResult UpdateResult = _BusRule.Update();
                        if (UpdateResult == ActionResult.Success)//Nếu thành công
                        {
                            //Cập nhật số thứ tự cho bản ghi tráo số thứ tự với bản ghi đang được cập nhật(nếu có)?
                            foreach (DataRow drUpdatePos in DataSource.Rows)
                            {
                                if (Utility.Int16Dbnull(drUpdatePos["Pos"]) == Convert.ToInt16(txtPos.Text))
                                {
                                    drUpdatePos["Pos"] = OldPos;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            DataSource.AcceptChanges();
                            //Cập nhật dòng hiện thời trong Datasource để cập nhật lại dữ liệu trên DataGridView
                            DataRow dr = Utility.GetDataRow(DataSource, "Modality_ID", txtID.Text.Trim());
                            if (dr != null)
                            {
                                Utility.CopyData(ModalityEntity.Rows[0], ref dr);
                                DataSource.AcceptChanges();
                            }
                            //Return to the InitialStatus
                            Act = action.FirstOrFinished;
                            //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                            Utility.GotoNewRow(grdList, "colModality_ID", txtCode.Text.Trim());
                            mdlStatic.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.", false);
                            SetControlStatus();
                            CurrentCellChanged();
                        }
                        else//Có lỗi xảy ra
                        {
                            switch (UpdateResult)
                            {
                                case ActionResult.Error:
                                    mdlStatic.SetMsg(lblMsg, "Lỗi khi cập nhật thiết bị. Liên hệ với VBIT", true);
                                    break;
                                default:
                                    mdlStatic.SetMsg(lblMsg, "Lỗi khi cập nhật thiết bị. Liên hệ với VBIT", true);
                                    break;
                            }
                        }
                        break;

                    case action.Delete:
                        if (Utility.AcceptQuestion("Bạn có muốn xóa thiết bị đang chọn hay không?", "Xác nhận xóa", true))
                        {
                            string Modality_ID = txtID.Text.Trim();
                            //Gọi nghiệp vụ xóa dữ liệu
                            ActionResult DeleteResult = _BusRule.Delete();
                            if (DeleteResult == ActionResult.Success)//Nếu xóa thành công trong CSDL
                            {
                                //Xóa dòng dữ liệu vừa chọn trong Datasource để cập nhật lại dữ liệu trên DataGridView
                                DataRow dr = Utility.GetDataRow(DataSource, "Modality_ID", Modality_ID);
                                if (dr != null)
                                {
                                    DataSource.Rows.Remove(dr);
                                    DataSource.AcceptChanges();
                                }
                                //Return to the InitialStatus
                                Act = action.FirstOrFinished;
                                mdlStatic.SetMsg(lblMsg, "Đã xóa thiết bị có ID: " + Modality_ID + " ra khỏi hệ thống.", false);
                                SetControlStatus();
                                CurrentCellChanged();
                            }
                            else//Có lỗi xảy ra
                            {
                                switch (DeleteResult)
                                {
                                    case ActionResult.DataHasUsedinAnotherTable:
                                        mdlStatic.SetMsg(lblMsg, "thiết bị có ID: " + Modality_ID + " đã được sử dụng trong bảng khác nên bạn không thể xóa!", true);
                                        break;
                                    default:
                                        mdlStatic.SetMsg(lblMsg, "Lỗi khi xóa thiết bị. Liên hệ với VBIT", true);
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Lấy danh sách thiết bị và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {
            try
            {
                DataSource = new ModalityController().GetAllData(true).Tables[0];
                Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "Pos,Mod_Type_Name");
                LoadDeviceType();
                LoadCountry();
                LoadManufacture();
                LoadRooms();
                
            }
            catch
            {
            }
        }
        private void LoadDeviceType()
        {
            try
            {
                DataTable dtmod_Type = new Mod_TypeController().GetAllData().Tables[0];
                DataTable dtmod_Type1 = dtmod_Type.Copy();
                Utility.AddColumnAlltoDataTable(ref dtmod_Type, "Mod_Type_ID", "Mod_Type_Name", "");
                dtmod_Type.DefaultView.Sort = "Mod_Type_Name ASC";
                //cboSearchModType.DataSource = dtmod_Type.DefaultView;
                //cboSearchModType.DisplayMember = "Mod_Type_Name";
                //cboSearchModType.ValueMember = "Mod_Type_ID";

                dtmod_Type1.DefaultView.Sort = "Mod_Type_Name ASC";
                cboModType.DataSource = dtmod_Type1.DefaultView;
                cboModType.DisplayMember = "Mod_Type_Name";
                cboModType.ValueMember = "Mod_Type_ID";
                //if (cboSearchModType.Items.Count > 0) cboSearchModType.SelectedIndex = 0;
                if (cboModType.Items.Count > 0) cboModType.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        private void LoadCountry()
        {
            try
            {
                DataTable dtCountry = new CountryController().GetAllData().Tables[0];
                DataTable dtCountry1 = dtCountry.Copy();
                Utility.AddColumnAlltoDataTable(ref dtCountry, "Country_Code", "Country_Name", "");
                dtCountry.DefaultView.Sort = "Country_Name ASC";
                //cboSearchCountry.DataSource = dtCountry.DefaultView;
                //cboSearchCountry.DisplayMember = "Country_Name";
                //cboSearchCountry.ValueMember = "Country_Code";

                dtCountry1.DefaultView.Sort = "Country_Name ASC";
                cboCountry.DataSource = dtCountry1.DefaultView;
                cboCountry.DisplayMember = "Country_Name";
                cboCountry.ValueMember = "Country_Code";
                //if (cboSearchCountry.Items.Count > 0) cboSearchCountry.SelectedIndex = 0;
                if (cboCountry.Items.Count > 0) cboCountry.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        private void LoadManufacture()
        {
            try
            {
                DataTable dtManufacture = new ManufactureController().GetAllData().Tables[0];
                DataTable dtManufacture1 = dtManufacture.Copy();
                Utility.AddColumnAlltoDataTable(ref dtManufacture, "Manufacture_ID", "Manufacture_Name", "");
                dtManufacture.DefaultView.Sort = "Manufacture_Name ASC";
                //cboSearchManufacture.DataSource = dtManufacture.DefaultView;
                //cboSearchManufacture.DisplayMember = "Manufacture_Name";
                //cboSearchManufacture.ValueMember = "Manufacture_ID";

                dtManufacture1.DefaultView.Sort = "Manufacture_Name ASC";
                cboManufacture.DataSource = dtManufacture1.DefaultView;
                cboManufacture.DisplayMember = "Manufacture_Name";
                cboManufacture.ValueMember = "Manufacture_ID";
                //if (cboSearchManufacture.Items.Count > 0) cboSearchManufacture.SelectedIndex = 0;
                if (cboManufacture.Items.Count > 0) cboManufacture.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        private void LoadRooms()
        {
            try
            {
                DataTable dtRoom = new RoomController().GetAllData().Tables[0];
                DataTable dtRoom1 = dtRoom.Copy();
                Utility.AddColumnAlltoDataTable(ref dtRoom, "Room_ID", "Room_Name", "");
                dtRoom.DefaultView.Sort = "Room_Name ASC";
                //cboSearchRoom.DataSource = dtRoom.DefaultView;
                //cboSearchRoom.DisplayMember = "Room_Name";
                //cboSearchRoom.ValueMember = "Room_ID";

                dtRoom1.DefaultView.Sort = "Room_Name ASC";
                cboRoom.DataSource = dtRoom1.DefaultView;
                cboRoom.DisplayMember = "Room_Name";
                cboRoom.ValueMember = "Room_ID";
                //if (cboSearchRoom.Items.Count > 0) cboSearchRoom.SelectedIndex = 0;
                if (cboRoom.Items.Count > 0) cboRoom.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        #endregion

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            Act = action.Insert;
            SetControlStatus();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            Act = action.Update;
            SetControlStatus();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            Act = action.Delete;
            ExecuteAction();
            Act = action.FirstOrFinished;
            SetControlStatus();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {                
            //SearchData(txtSearchID.Text.Trim(),cboSearchDepartment.SelectedValue.ToString(), txtSearchCode.Text.Trim(), txtSearchName.Text.Trim());
        }
        /// <summary>
        /// Lấy danh sách thiết bị và Binding vào DataGridView
        /// </summary>
        private void SearchData(string _ID,string DepartID, string _Code, string _Name)
        {
            try
            {
                string WhereCondition = "";
                if (_ID.Trim() != "")
                    WhereCondition = " Modality_ID=" + Utility.Int16Dbnull(_ID);
                if (_Code.Trim() != "")
                    if (WhereCondition != "") WhereCondition += " AND Modality_Code='" + _Code + "'";
                    else
                        WhereCondition = " Modality_Code='" + _Code + "'";

                if (_Name.Trim() != "")
                    if (WhereCondition != "") WhereCondition += " AND Modality_Name like '%" + _Name + "%'";
                    else
                        WhereCondition = " Modality_Name like '%" + _Name + "%'";
                if (DepartID.Trim() != "-1")
                    if (WhereCondition != "") WhereCondition += " AND  Department_ID=" + Utility.Int16Dbnull(DepartID);
                    else
                        WhereCondition = " Department_ID=" + Utility.Int16Dbnull(DepartID);
                if (DepartID.Trim() == "-1" && _ID.Trim() == "" && _Code.Trim() == "" && _Name.Trim() == "") WhereCondition = " 1=1";
                DataSource = new ModalityController().GetData(WhereCondition).Tables[0];
                Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "Pos,Modality_Name");
                CurrentCellChanged();
            }
            catch
            {
            }
        }

        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            ExecuteAction();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Act = action.FirstOrFinished;
            SetControlStatus();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAddDeviceType_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Mod_TypeList frm = new frm_Mod_TypeList();
                frm.ShowDialog();
                LoadDeviceType();
            }
            catch
            {
            }
        }

        private void cmdAddManufacture_Click(object sender, EventArgs e)
        {
            try
            {
                frm_ManufactureList frm = new frm_ManufactureList();
                frm.ShowDialog();
                LoadManufacture();
            }
            catch
            {
            }
        }

        private void cmdAddCountry_Click(object sender, EventArgs e)
        {
            try
            {
                frm_CountryList frm = new frm_CountryList();
                frm.ShowDialog();
                LoadCountry();
            }
            catch
            {
            }
        }

        private void cmdAddDepart_Click(object sender, EventArgs e)
        {
            try
            {
                frmRoomList frm = new frmRoomList();
                frm.ShowDialog();
                LoadRooms();
            }
            catch
            {
            }
        }

        private void nmrHeight_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkFlipV_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkFlipV.ImageIndex == 1)
                {

                    chkFlipV.ImageIndex = 0;
                }
                else
                {

                    chkFlipV.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void chkFlipH_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkFlipH.ImageIndex == 1)
                {
                    chkFlipH.ImageIndex = 0;
                }
                else
                {
                    chkFlipH.ImageIndex = 1;
                }
            }
            catch
            {
            }
        }
        
    }
}
