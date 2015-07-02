﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
//
using DROCLibs.Entities;

using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects.ObjectInfor;
namespace VietBaIT.DROC
{
    public partial class frmRoomList : BaseForm
    {
        #region "Attributes"
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
       
        /// <summary>
        /// Khai báo Entity chứa dữ liệu
        /// </summary>
        private DataTable RoomEntity = new RoomEntity.RoomEntityDataTable();
        //Khai báo gói thông tin chứa RoomEntity và Header truyền lên BL để xử lý nghiệp vụ
        private DataSet DataEntity = new DataSet();
        /// <summary>
        /// Datasource là danh sách Room hiển thị trên lưới
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
        public frmRoomList()
        {
            InitializeComponent();
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
        }
        private void LoadData()
        {

        }
        private void frmRoomList_Load(object sender, EventArgs e)
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
            Utility.ResetEntity(ref RoomEntity);
            //Create new Row
            DataRow dr = RoomEntity.NewRow();
            if (Act == action.Insert) dr["Room_ID"] = -1;
            else dr["Room_ID"] = Convert.ToInt16(txtID.Text.Trim());
            dr["Room_Code"] = Utility.GetValue(txtCode.Text, false);
            dr["Room_Name"] = Utility.GetValue(txtName.Text, false);
            dr["Department_ID"] = Convert.ToInt16(cboDepartment.SelectedValue);
            dr["Department_Name"] = cboDepartment.Text;
            dr["Desc"] = Utility.GetValue(txtDesc.Text, false);
            dr["Pos"] = Convert.ToInt16(txtPos.Text);
            //Set giá trị cho cột STT cũ để tráo đổi số TT với bản ghi có STT=dr["Pos"]
            dr["OldPos"] = OldPos;
            RoomEntity.Rows.Add(dr);
            RoomEntity.AcceptChanges();
            //Add RoomEntity into DataEntity
            DataEntity.Tables.Add(RoomEntity);
        }
        #endregion

        #region "Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        //Khởi tạo View: Khởi tạo trạng thái các Control, khởi tạo dữ liệu ban đầu...
        //Implements from IRoomView
        public void InitView()
        {
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            cmdCancel.Click+=new EventHandler(cmdCancel_Click);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            Initialize();
            //Lấy về danh sách các phòng để hiển thị lên DataGridView
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
            if (cboDepartment.Items.Count<=0)
            {
                mdlStatic.SetMsg(lblMsg, "Cần khởi tạo danh mục Khoa trước khi thực hiện thêm Phòng.", true);
                cboDepartment.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtCode.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập mã phòng.",true);
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

               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập tên phòng.",true);
                txtName.Focus();
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
                    //Cho phép nhập liệu mã phòng,vị trí, tên phòng và mô tả thêm
                    txtID.Text = "Tự sinh";
                    txtCode.Clear();
                    txtName.Clear();
                    txtDesc.Clear();
                    Utility.DisabledTextBox(txtID);
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtDesc);
                    txtPos.Text = Utility.getNextMaxID("Pos", "L_RoomList").ToString();
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
                    //Không cho phép cập nhật lại mã phòng
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên phòng và mô tả thêm
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtDesc);
                    Utility.EnabledTextBox(txtPos);
                    OldPos = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "colPos", grdList.CurrentRow.Index), 0);
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
                    //Không cho phép nhập liệu mã phòng, tên phòng và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtCode);
                    Utility.DisabledTextBox(txtName);
                    Utility.DisabledTextBox(txtDesc);
                    Utility.DisabledTextBox(txtPos);
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
                    mdlStatic.SetMsg(lblMsg, "Mời bạn thao tác", false);
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
                        txtID.Text = Utility.GetValueFromGridColumn(grdList, "colRoom_ID", grdList.CurrentRow.Index);
                        txtCode.Text = Utility.GetValueFromGridColumn(grdList, "colRoom_Code", grdList.CurrentRow.Index);
                        txtName.Text = Utility.GetValueFromGridColumn(grdList, "colRoom_Name", grdList.CurrentRow.Index);
                        txtDesc.Text = Utility.GetValueFromGridColumn(grdList, "colDesc", grdList.CurrentRow.Index);
                        txtPos.Text = Utility.GetValueFromGridColumn(grdList, "colPos", grdList.CurrentRow.Index);
                        short Department_ID = Utility.Int16Dbnull(Utility.GetValueFromGridColumn(grdList, "colDepartment_ID", grdList.CurrentRow.Index), 0);
                        cboDepartment.SelectedIndex = Utility.GetSelectedIndex(cboDepartment, Department_ID.ToString());
                    }
                    else
                    {
                        txtID.Text = "";
                        txtCode.Text = "";
                        txtName.Text ="";
                        txtDesc.Text ="0";
                        txtPos.Text = "";
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
            //Kiểm tra tính hợp lệ của dữ liệu trước khi thêm mới
            if (!IsValidData())
            {
                return;
            }
            //Gán RoomEntity vào DataEntity
            SetValueforEntity();

            //Khởi tạo BusinessRule để xử lý nghiệp vụ
            RoomInfor Infor = new RoomInfor();
            Utility.MapValueFromEntityIntoObjectInfor(Infor, RoomEntity);
            RoomController _BusRule = new RoomController(Infor);
            switch (Act)
            {
                case action.Insert:
                    //Gọi nghiệp vụ Insert dữ liệu
                    ActionResult InsertResult = _BusRule.Insert();
                    if (InsertResult == ActionResult.Success)//Nếu thành công
                    {
                        //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                        //phải đảm bảo Datasource và RoomEntity có cấu trúc giống nhau mới dùng hàm này
                        DataRow dr = Utility.CopyData(RoomEntity.Rows[0], DataSource);
                        dr["Room_ID"] = Infor.Room_ID;
                        if (dr != null)//99.99% là sẽ !=null
                        {
                            DataSource.Rows.Add(dr);
                            DataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colRoom_ID", Infor.Room_ID.ToString());
                       mdlStatic.SetMsg(lblMsg,"Thêm mới dữ liệu thành công!",false);

                        SetControlStatus();
                        CurrentCellChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (InsertResult)
                        {
                            case ActionResult.ExistedRecord:
                                mdlStatic.SetMsg(lblMsg, "Đã tồn tại Phòng có mã: " + txtCode.Text.Trim() + ". Đề nghị bạn xem lại",true);
                               Utility.FocusAndSelectAll( txtCode);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi trong quá trình thêm mới Phòng. Liên hệ với VBIT",true);
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
                        DataRow dr = Utility.GetDataRow(DataSource, "Room_ID", txtID.Text.Trim());
                        if (dr != null)
                        {
                            Utility.CopyData(RoomEntity.Rows[0], ref dr);
                            DataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colRoom_ID", txtCode.Text.Trim());
                       mdlStatic.SetMsg(lblMsg,"Cập nhật dữ liệu thành công.",false);
                        SetControlStatus();
                        CurrentCellChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (UpdateResult)
                        {
                            case ActionResult.Error:
                               mdlStatic.SetMsg(lblMsg,"Lỗi khi cập nhật Phòng. Liên hệ với VBIT",true);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi khi cập nhật Phòng. Liên hệ với VBIT",true);
                                break;
                        }
                    }
                    break;

                case action.Delete:
                    if (Utility.AcceptQuestion("Bạn có muốn xóa Phòng đang chọn hay không?", "Xác nhận xóa", true))
                    {
                        string Room_ID = txtID.Text.Trim();
                        //Gọi nghiệp vụ xóa dữ liệu
                        ActionResult DeleteResult = _BusRule.Delete();
                        if (DeleteResult == ActionResult.Success)//Nếu xóa thành công trong CSDL
                        {
                            //Xóa dòng dữ liệu vừa chọn trong Datasource để cập nhật lại dữ liệu trên DataGridView
                            DataRow dr = Utility.GetDataRow(DataSource, "Room_ID", Room_ID);
                            if (dr != null)
                            {
                                DataSource.Rows.Remove(dr);
                                DataSource.AcceptChanges();
                            }
                            //Return to the InitialStatus
                            Act = action.FirstOrFinished;
                            mdlStatic.SetMsg(lblMsg, "Đã xóa Phòng có ID: " + Room_ID + " ra khỏi hệ thống.", false);
                            SetControlStatus();
                            CurrentCellChanged();
                        }
                        else//Có lỗi xảy ra
                        {
                            switch (DeleteResult)
                            {
                                case ActionResult.DataHasUsedinAnotherTable:
                                    mdlStatic.SetMsg(lblMsg, "Phòng có ID: " + Room_ID + " đã được sử dụng trong bảng khác nên bạn không thể xóa!", true);
                                    break;
                                default:
                                   mdlStatic.SetMsg(lblMsg,"Lỗi khi xóa Phòng. Liên hệ với VBIT",true);
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
        /// Lấy danh sách Phòng và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {
            try
            {
                DataSource = new RoomController().GetAllData().Tables[0];
                Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "Pos,Room_Name");
                LoadDepartment();
            }
            catch
            {
            }
        }
        private void LoadDepartment()
        {
            try
            {
               
                DataTable dtDepartment = new DepartmentController().GetAllData().Tables[0];
                DataTable dtDepartment1 = dtDepartment.Copy();
                Utility.AddColumnAlltoDataTable(ref dtDepartment, "Department_ID", "Department_Name", "");
                dtDepartment.DefaultView.Sort = "Department_Name ASC";
                cboSearchDepartment.DataSource = dtDepartment.DefaultView;
                cboSearchDepartment.DisplayMember = "Department_Name";
                cboSearchDepartment.ValueMember = "Department_ID";

                dtDepartment1.DefaultView.Sort = "Department_Name ASC";
                cboDepartment.DataSource = dtDepartment1.DefaultView;
                cboDepartment.DisplayMember = "Department_Name";
                cboDepartment.ValueMember = "Department_ID";
                if (cboSearchDepartment.Items.Count > 0) cboSearchDepartment.SelectedIndex = 0;
                if (cboDepartment.Items.Count > 0) cboDepartment.SelectedIndex = 0;
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

        private void cmdSearch_Click(object sender, EventArgs e)
        {
           
                
            SearchData(txtSearchID.Text.Trim(),cboSearchDepartment.SelectedValue.ToString(), txtSearchCode.Text.Trim(), txtSearchName.Text.Trim());
        }
        /// <summary>
        /// Lấy danh sách Phòng và Binding vào DataGridView
        /// </summary>
        private void SearchData(string _ID,string DepartID, string _Code, string _Name)
        {
            try
            {
                string WhereCondition = "";
                if (_ID.Trim() != "")
                    WhereCondition = " Room_ID=" + Utility.Int16Dbnull(_ID);
                if (_Code.Trim() != "")
                    if (WhereCondition != "") WhereCondition += " AND Room_Code='" + _Code + "'";
                    else
                        WhereCondition = " Room_Code='" + _Code + "'";

                if (_Name.Trim() != "")
                    if (WhereCondition != "") WhereCondition += " AND Room_Name like '%" + _Name + "%'";
                    else
                        WhereCondition = " Room_Name like '%" + _Name + "%'";
                if (DepartID.Trim() != "-1")
                    if (WhereCondition != "") WhereCondition += " AND  Department_ID=" + Utility.Int16Dbnull(DepartID);
                    else
                        WhereCondition = " Department_ID=" + Utility.Int16Dbnull(DepartID);
                if (DepartID.Trim() == "-1" && _ID.Trim() == "" && _Code.Trim() == "" && _Name.Trim() == "") WhereCondition = " 1=1";
                DataSource = new RoomController().GetData(WhereCondition).Tables[0];
                Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "Pos,Room_Name");
                CurrentCellChanged();
            }
            catch
            {
            }
        }

        private void cmdAddDepart_Click(object sender, EventArgs e)
        {
            try
            {
                frmDepartmentList frm = new frmDepartmentList();
                frm.ShowDialog();
                LoadDepartment();
            }
            catch
            {
            }
        }

       
    }
}