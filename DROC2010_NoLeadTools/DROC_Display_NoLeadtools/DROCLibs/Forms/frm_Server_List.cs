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
using VietBaIT.Entities;
using ClearCanvas.Dicom.Network.Scu;
namespace VietBaIT.DROC
{
    public partial class frm_ServerList : BaseForm
    {
        #region "Attributes"
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
       
        /// <summary>
        /// Khai báo Entity chứa dữ liệu
        /// </summary>
        private DataTable ServerEntity = new ServerEntity.ServerEntityDataTable();
        //Khai báo gói thông tin chứa ServerEntity và Header truyền lên BL để xử lý nghiệp vụ
        private DataSet DataEntity = new DataSet();
        /// <summary>
        /// Datasource là danh sách Server hiển thị trên lưới
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
        private Int16 OldPort = 0;
        #endregion

        public frm_ServerList()
        {
            InitializeComponent();
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
            txtID.Text = "0";
        }
        private void LoadData()
        {

        }
        private void frm_ServerList_Load(object sender, EventArgs e)
        {
            InitView();
        }
        #region "Entity setting"
        /// <summary>
        /// Thiết lập giá trị cho Entity 
        /// </summary>
        private void SetValueforEntity()
        {
            try
            {
                Utility.ResetEntity(ref DataEntity);
                Utility.ResetEntity(ref ServerEntity);
                //Create new Row
                DataRow dr = ServerEntity.NewRow();
                dr["IpAddress"] = Utility.GetValue(txtIpAddress.Text, false);
                dr["LocalAddress"] = Utility.GetValue(txtLocalAddress.Text, false);
                dr["CalledAETitle"] = Utility.GetValue(txtCalledAETitle.Text, false);
                dr["CallingAETitle"] = Utility.GetValue(txtCallingAETitle.Text, false);
                dr["Port"] = Convert.ToInt16(txtport.Text);
                dr["LocalPort"] = Convert.ToInt16(txtLocalPort.Text);
                dr["TimeOut"] = Convert.ToInt16(txtTimeOut.Text);
                dr["ID"] = Convert.ToInt16(txtID.Text);
                dr["IsActive"] = Convert.ToByte(chkIsActive.Checked ? 1 : 0);
                dr["ServerType"] = Convert.ToByte(cboServerType.SelectedIndex);
                dr["ServerName"] = Utility.GetValue(txtName.Text, false);
                dr["FilmSize"] =cboFSize.Text.Trim();
                //Set giá trị cho cột STT cũ để tráo đổi số TT với bản ghi có STT=dr["Port"]

                ServerEntity.Rows.Add(dr);
                ServerEntity.AcceptChanges();
                //Add ServerEntity into DataEntity
                DataEntity.Tables.Add(ServerEntity);
            }
            catch
            {
            }
        }
        #endregion

        #region "Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        //Khởi tạo View: Khởi tạo trạng thái các Control, khởi tạo dữ liệu ban đầu...
        //Implements from IServerView
        public void InitView()
        {
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            cmdCancel.Click+=new EventHandler(cmdCancel_Click);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            Initialize();
            //Lấy về danh sách các Server để hiển thị lên DataGridView
            GetData();
            //Sau khi Binding dữ liệu vào GridView thì mới cho phép thực hiện lệnh trong sự kiện CurrentCellChanged
            bLoaded = true;
            //Gọi sự kiện CurrentCellChanged để hiển thị dữ liệu từ trên lưới xuống Controls
            CurrentCellChanged();
            //Thiết lập giá trị mặc định của DMLType
            Act = action.FirstOrFinished;
            //Thiết lập các giá trị mặc định cho các Control
            SetControlStatus();
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
            if (String.IsNullOrEmpty(txtName.Text))
            {
                mdlStatic.SetMsg(lblMsg, "Bạn cần nhập tên máy in.", true);
                txtName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtIpAddress.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập địa chỉ SPC.",true);
                txtIpAddress.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtport.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Cổng kết nối phải là chữ số.",true);
                txtport.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtCalledAETitle.Text))
            {

               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập Called AETitle.",true);
                txtCalledAETitle.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtTimeOut.Text))
            {
                mdlStatic.SetMsg(lblMsg, "TimeOut phải là chữ số.", true);
                txtTimeOut.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtTimeOut.Text))
            {

                mdlStatic.SetMsg(lblMsg, "Bạn cần nhập TimeOut.", true);
                txtTimeOut.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtLocalPort.Text))
            {
                mdlStatic.SetMsg(lblMsg, "Cổng kết nối phải là chữ số.", true);
                txtLocalPort.Focus();
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
        private void ModifyButtons()
        {
            cmdInsert.Enabled = true;
            cmdUpdate.Enabled = grdList.RowCount > 0;
            cmdDelete.Enabled = grdList.RowCount > 0;
            cmdSearchOnGrid.Enabled = grdList.RowCount > 0;
            cmdPrint.Enabled = grdList.RowCount > 0;
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
                    //Cho phép nhập liệu địa chỉ SPC,vị trí, Called AETitle và mô tả thêm
                    txtIpAddress.Clear();
                    txtLocalAddress.Clear();
                    txtName.Clear();
                    txtCalledAETitle.Clear();
                    txtCallingAETitle.Clear();
                    txtTimeOut.Clear();
                    txtport.Clear();
                    Utility.EnabledTextBox(txtIpAddress);
                    Utility.EnabledTextBox(txtLocalAddress);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtport);
                    Utility.EnabledTextBox(txtCalledAETitle);
                    Utility.EnabledTextBox(txtCallingAETitle);
                    Utility.EnabledTextBox(txtTimeOut);
                    Utility.EnabledComboBox(cboFSize);
                    chkIsActive.Enabled = true;
                    
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
                    txtName.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại địa chỉ SPC
                    Utility.EnabledTextBox(txtIpAddress);
                    Utility.EnabledTextBox(txtLocalAddress);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledComboBox(cboFSize);
                    //Cho phép cập nhật lại vị trí, Called AETitle và mô tả thêm
                    Utility.EnabledTextBox(txtCalledAETitle);
                    Utility.EnabledTextBox(txtCallingAETitle);
                    Utility.EnabledTextBox(txtport);
                    Utility.EnabledTextBox(txtTimeOut);
                    chkIsActive.Enabled = true;
                   
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
                    //Tự động Focus đến mục Portition để người dùng nhập liệu
                    txtName.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu địa chỉ SPC, Called AETitle và mô tả thêm
                    Utility.DisabledTextBox(txtIpAddress);
                    Utility.DisabledTextBox(txtLocalAddress);
                    Utility.DisabledComboBox(cboFSize);
                    Utility.DisabledTextBox(txtName);
                    Utility.DisabledTextBox(txtCalledAETitle);
                    Utility.DisabledTextBox(txtCallingAETitle);
                    Utility.DisabledTextBox(txtport);
                    Utility.DisabledTextBox(txtTimeOut);
                    chkIsActive.Enabled = false;

                    chkIsActive.Checked = false;
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
                        txtIpAddress.Text = Utility.GetValueFromGridColumn(grdList, "colIpAddress", grdList.CurrentRow.Index);
                        txtCalledAETitle.Text = Utility.GetValueFromGridColumn(grdList, "colCalledAETitle", grdList.CurrentRow.Index);
                        txtCallingAETitle.Text = Utility.GetValueFromGridColumn(grdList, "colCallingAETitle", grdList.CurrentRow.Index);
                        txtport.Text = Utility.GetValueFromGridColumn(grdList, "colPort", grdList.CurrentRow.Index);
                        txtTimeOut.Text = Utility.GetValueFromGridColumn(grdList, "colTimeOut", grdList.CurrentRow.Index);
                        chkIsActive.Checked = (Utility.GetValueFromGridColumn(grdList, "colIsActive", grdList.CurrentRow.Index) == "0" ? false : true);
                        cboServerType.SelectedIndex = Convert.ToInt32((Utility.GetValueFromGridColumn(grdList, "colServerType", grdList.CurrentRow.Index) == "0" ? false : true));
                        txtID.Text = Utility.GetValueFromGridColumn(grdList, "colID", grdList.CurrentRow.Index);
                        txtName.Text = Utility.GetValueFromGridColumn(grdList, "colServerName", grdList.CurrentRow.Index);
                        txtLocalAddress.Text = Utility.GetValueFromGridColumn(grdList, "colLocalAddress", grdList.CurrentRow.Index);
                        txtLocalPort.Text = Utility.GetValueFromGridColumn(grdList, "colLocalPort", grdList.CurrentRow.Index);

                        cboFSize.Text = Utility.GetValueFromGridColumn(grdList, "colFilmSize", grdList.CurrentRow.Index);

                    }
                    else
                    {
                        chkIsActive.Checked = false;
                        txtIpAddress.Text = "";
                        txtCalledAETitle.Text = "";
                        txtCallingAETitle.Text = "";
                        txtport.Text = "";
                        txtID.Text = "0";
                        txtLocalAddress.Text = "";
                        txtLocalPort.Text = "0";
                        txtTimeOut.Clear();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                txtLocalPort.Text = txtLocalPort.Text.Trim() == "" ? "0" : txtLocalPort.Text.Trim();
            }
        }
        #endregion

        #region "Insert, Delete, Update,Select,..."
        private void ExecuteAction()
        {
            //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
            if (!IsValidData())
            {
                return;
            }
            //Gán ServerEntity vào DataEntity
            SetValueforEntity();

            //Khởi tạo BusinessRule để xử lý nghiệp vụ
            ServerInfor Infor = new ServerInfor();
            Utility.MapValueFromEntityIntoObjectInfor(Infor, ServerEntity);
            ServerController _BusRule = new ServerController(Infor);
            switch (Act)
            {
                case action.Insert:
                    //Gọi nghiệp vụ Insert dữ liệu
                    ActionResult InsertResult = _BusRule.Insert();
                    if (InsertResult == ActionResult.Success)//Nếu thành công
                    {
                        //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                        //phải đảm bảo Datasource và ServerEntity có cấu trúc giống nhau mới dùng hàm này
                        DataRow dr = Utility.CopyData(ServerEntity.Rows[0], DataSource);
                        dr["ID"] = Infor.ID;
                        if (dr != null)//99.99% là sẽ !=null
                        {
                            DataSource.Rows.Add(dr);
                            DataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colIpAddress", txtIpAddress.Text.Trim());
                        mdlStatic.SetMsg(lblMsg, "Thêm mới dữ liệu thành công!", false);
                        SetControlStatus();
                        CurrentCellChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (InsertResult)
                        {
                            case ActionResult.ExistedRecord:
                                mdlStatic.SetMsg(lblMsg, "Đã tồn tại Server có địa chỉ: " + txtIpAddress.Text.Trim() + " và Called AETitle:"+txtCalledAETitle.Text.Trim() +" và cổng :"+txtport.Text.Trim()+"\n Đề nghị bạn xem lại",true);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi trong quá trình thêm mới Server. Liên hệ với VBIT",true);
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
                        foreach (DataRow drUpdatePort in DataSource.Rows)
                        {
                            if (Utility.Int16Dbnull(drUpdatePort["Port"]) == Convert.ToInt16(txtport.Text))
                            {
                                drUpdatePort["Port"] = OldPort;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        DataSource.AcceptChanges();
                        //Cập nhật dòng hiện thời trong Datasource để cập nhật lại dữ liệu trên DataGridView
                        DataRow dr = Utility.GetDataRow(DataSource, "ID", Utility.GetValueFromGridColumn(grdList, "colID", grdList.CurrentRow.Index));
                        if (dr != null)
                        {
                            Utility.CopyData(ServerEntity.Rows[0], ref dr);
                            DataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colIpAddress", txtIpAddress.Text.Trim());
                        mdlStatic.SetMsg(lblMsg, "Cập nhật dữ liệu thành công.", false);
                        SetControlStatus();
                        CurrentCellChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (UpdateResult)
                        {
                            case ActionResult.Error:
                                mdlStatic.SetMsg(lblMsg, "Lỗi khi cập nhật Server. Liên hệ với VBIT", true);
                                break;
                            default:
                                mdlStatic.SetMsg(lblMsg, "Lỗi khi cập nhật Server. Liên hệ với VBIT", true);
                                break;
                        }
                    }
                    break;

                case action.Delete:
                    if (Utility.AcceptQuestion("Bạn có muốn xóa Server đang chọn hay không?", "Xác nhận xóa", true))
                    {
                        string IpAddress = txtIpAddress.Text.Trim();
                        //Gọi nghiệp vụ xóa dữ liệu
                        ActionResult DeleteResult = _BusRule.Delete();
                        if (DeleteResult == ActionResult.Success)//Nếu xóa thành công trong CSDL
                        {
                            //Xóa dòng dữ liệu vừa chọn trong Datasource để cập nhật lại dữ liệu trên DataGridView
                            DataRow dr = Utility.GetDataRow(DataSource, "IpAddress", IpAddress);
                            if (dr != null)
                            {
                                DataSource.Rows.Remove(dr);
                                DataSource.AcceptChanges();
                            }
                            //Return to the InitialStatus
                            Act = action.FirstOrFinished;
                           mdlStatic.SetMsg(lblMsg,"Đã xóa Server có mã: " + IpAddress + " ra khỏi hệ thống.",false);
                            SetControlStatus();
                            CurrentCellChanged();
                        }
                        else//Có lỗi xảy ra
                        {
                            switch (DeleteResult)
                            {
                                case ActionResult.DataHasUsedinAnotherTable:
                                   mdlStatic.SetMsg(lblMsg,"Server có mã: " + IpAddress + " đã được sử dụng trong bảng khác nên bạn không thể xóa!",true);
                                    break;
                                default:
                                   mdlStatic.SetMsg(lblMsg,"Lỗi khi xóa Server. Liên hệ với VBIT",true);
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Lấy danh sách Server và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {
            DataSource = new ServerController().GetAllData().Tables[0];
            Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true,"", "IpAddress,CalledAETitle");
            ModifyButtons();
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
            ModifyButtons();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ExecuteAction();
            ModifyButtons();
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

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData(txtSearchIpAddress.Text.Trim(), txtSearchCalledAETitle.Text.Trim());
        }
        /// <summary>
        /// Lấy danh sách Server và Binding vào DataGridView
        /// </summary>
        private void SearchData(string _Code,string _Name)
        {
            string WhereCondition = "";
            if(_Code.Trim()!="")
                WhereCondition= " IpAddress='" + _Code + "'";
            if (_Name.Trim() != "")
                if (WhereCondition != "") WhereCondition += " AND CalledAETitle like '%" + _Name + "%'";
                else
                    WhereCondition = " CalledAETitle like '%" + _Name + "%'";
            if (_Code.Trim() == "" && _Name.Trim() == "") WhereCondition = " 1=1";
            DataSource = new ServerController().GetData(WhereCondition).Tables[0];
            Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "IpAddress,CalledAETitle");
            CurrentCellChanged();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {

        }
        #region Verify Server
        VerificationScu _verificationScu = null;

        private const string STR_Cancel = "Cancel";
        private const string STR_Verify = "Test";
        private void TestStoreServer()
        {
            try
            {
                _verificationScu = new VerificationScu();
                //if (cmdTest.Text == STR_Verify)
                    StartVerify();
                //else
                //    CancelVerify();
            }
            catch
            {
            }
        }
        private void StartVerify()
        {
            try
            {
                string LocalAETitle = txtCallingAETitle.Text.Trim();
                string printerName = txtName.Text;
                string RemoteAETitle = txtCalledAETitle.Text.Trim();
                string RemoteHost = txtIpAddress.Text.Trim();
                int Port = Convert.ToInt32(txtport.Text);
                int timeout = Convert.ToInt32(txtTimeOut.Text);
                _verificationScu.BeginVerify(LocalAETitle, RemoteAETitle, RemoteHost, Port, VerifyComplete, null);
                //SetVerifyButton(true);
            }
            catch
            {
            }
        }

        private void VerifyComplete(IAsyncResult ar)
        {
            VerificationResult verificationResult = _verificationScu.EndVerify(ar);
            Utility.ShowMsg("Kết quả: " + verificationResult);
            //SetVerifyButton(false);
        }

        private void SetVerifyButton(bool running)
        {
            //if (!InvokeRequired)
            //{
            //    cmdTest.Text = running ? STR_Cancel : STR_Verify;
            //}
            //else
            //{
            //    BeginInvoke(new Action<bool>(SetVerifyButton), new object[] { running });
            //}
        }

        private void CancelVerify()
        {
            _verificationScu.Cancel();
        }
        #endregion
    
    }
}
