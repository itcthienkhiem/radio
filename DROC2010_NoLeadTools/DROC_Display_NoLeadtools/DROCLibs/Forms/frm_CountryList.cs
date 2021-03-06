﻿using System;
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
    public partial class frm_CountryList : BaseForm
    {
        #region "Attributes"
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
       
        /// <summary>
        /// Khai báo Entity chứa dữ liệu
        /// </summary>
        private DataTable CountryEntity = new CountryEntity.CountryEntityDataTable();
        //Khai báo gói thông tin chứa CountryEntity và Header truyền lên BL để xử lý nghiệp vụ
        private DataSet DataEntity = new DataSet();
        /// <summary>
        /// Datasource là danh sách Country hiển thị trên lưới
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
       
        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện CurrentCellChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện CurrentCellChanged
        /// </summary>
        private bool bLoaded = false;
        private Int16 OldPos = 0;
        #endregion
        public frm_CountryList()
        {
            InitializeComponent();
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
        }
        private void LoadData()
        {

        }
        private void frm_CountryList_Load(object sender, EventArgs e)
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
            Utility.ResetEntity(ref CountryEntity);
            //Create new Row
            DataRow dr = CountryEntity.NewRow();
            dr["Country_Code"] = Utility.GetValue(txtCode.Text, false);
            dr["Country_Name"] = Utility.GetValue(txtName.Text, false);
            dr["Desc"] = Utility.GetValue(txtDesc.Text, false);
            dr["Pos"] = Convert.ToInt16(txtPos.Text);
            //Set giá trị cho cột STT cũ để tráo đổi số TT với bản ghi có STT=dr["Pos"]
            dr["OldPos"] = OldPos;
            CountryEntity.Rows.Add(dr);
            CountryEntity.AcceptChanges();
            //Add CountryEntity into DataEntity
            DataEntity.Tables.Add(CountryEntity);
        }
        #endregion

        #region "Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        //Khởi tạo View: Khởi tạo trạng thái các Control, khởi tạo dữ liệu ban đầu...
        //Implements from ICountryView
        public void InitView()
        {
            cmdSearch.Click += new EventHandler(cmdSearch_Click);
            cmdCancel.Click += new EventHandler(cmdCancel_Click);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            grdList.CurrentCellChanged += new EventHandler(grdList_CurrentCellChanged);
            Initialize();
            //Lấy về danh sách các nước để hiển thị lên DataGridView
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
            if (String.IsNullOrEmpty(txtCode.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập mã nước.",true);
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

               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập tên nước.",true);
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
                    //Cho phép nhập liệu mã nước,vị trí, tên nước và mô tả thêm
                    txtCode.Clear();
                    txtName.Clear();
                    txtDesc.Clear();
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtName);
                    Utility.EnabledTextBox(txtDesc);
                    txtPos.Text = Utility.getNextMaxID("Pos", "L_CountryList").ToString();
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
                    txtCode.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã nước
                    Utility.DisabledTextBox(txtCode);
                    //Cho phép cập nhật lại vị trí, tên nước và mô tả thêm
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
                    txtPos.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã nước, tên nước và mô tả thêm
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
                        txtCode.Text = Utility.GetValueFromGridColumn(grdList, "colCountryCode", grdList.CurrentRow.Index);
                        txtName.Text = Utility.GetValueFromGridColumn(grdList, "colCountryName", grdList.CurrentRow.Index);
                        txtDesc.Text = Utility.GetValueFromGridColumn(grdList, "colDesc", grdList.CurrentRow.Index);
                        txtPos.Text = Utility.GetValueFromGridColumn(grdList, "colPos", grdList.CurrentRow.Index);
                    }
                    else
                    {

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
            //Gán CountryEntity vào DataEntity
            SetValueforEntity();

            //Khởi tạo BusinessRule để xử lý nghiệp vụ
            CountryInfor Infor = new CountryInfor();
            Utility.MapValueFromEntityIntoObjectInfor(Infor, CountryEntity);
            CountryController _BusRule = new CountryController(Infor);
            switch (Act)
            {
                case action.Insert:
                    //Gọi nghiệp vụ Insert dữ liệu
                    ActionResult InsertResult = _BusRule.Insert();
                    if (InsertResult == ActionResult.Success)//Nếu thành công
                    {
                        //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                        //phải đảm bảo Datasource và CountryEntity có cấu trúc giống nhau mới dùng hàm này
                        DataRow dr = Utility.CopyData(CountryEntity.Rows[0], DataSource);
                        if (dr != null)//99.99% là sẽ !=null
                        {
                            DataSource.Rows.Add(dr);
                            DataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colCountryCode", txtCode.Text.Trim());
                       mdlStatic.SetMsg(lblMsg,"Thêm mới dữ liệu thành công!",false);

                        SetControlStatus();
                        CurrentCellChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (InsertResult)
                        {
                            case ActionResult.ExistedRecord:
                                mdlStatic.SetMsg(lblMsg, "Đã tồn tại Quốc gia có mã: " + txtCode.Text.Trim() + ". Đề nghị bạn xem lại",true);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi trong quá trình thêm mới Quốc gia. Liên hệ với VBIT",true);
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
                        DataRow dr = Utility.GetDataRow(DataSource, "Country_Code", txtCode.Text.Trim());
                        if (dr != null)
                        {
                            Utility.CopyData(CountryEntity.Rows[0], ref dr);
                            DataSource.AcceptChanges();
                        }
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                        Utility.GotoNewRow(grdList, "colCountryCode", txtCode.Text.Trim());
                       mdlStatic.SetMsg(lblMsg,"Cập nhật dữ liệu thành công.",false);
                        SetControlStatus();
                        CurrentCellChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (UpdateResult)
                        {
                            case ActionResult.Error:
                               mdlStatic.SetMsg(lblMsg,"Lỗi khi cập nhật Quốc gia. Liên hệ với VBIT",true);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi khi cập nhật Quốc gia. Liên hệ với VBIT",true);
                                break;
                        }
                    }
                    break;

                case action.Delete:
                    if (Utility.AcceptQuestion("Bạn có muốn xóa Quốc gia đang chọn hay không?", "Xác nhận xóa", true))
                    {
                        string Country_Code = txtCode.Text.Trim();
                        //Gọi nghiệp vụ xóa dữ liệu
                        ActionResult DeleteResult = _BusRule.Delete();
                        if (DeleteResult == ActionResult.Success)//Nếu xóa thành công trong CSDL
                        {
                            //Xóa dòng dữ liệu vừa chọn trong Datasource để cập nhật lại dữ liệu trên DataGridView
                            DataRow dr = Utility.GetDataRow(DataSource, "Country_Code", Country_Code);
                            if (dr != null)
                            {
                                DataSource.Rows.Remove(dr);
                                DataSource.AcceptChanges();
                            }
                            //Return to the InitialStatus
                            Act = action.FirstOrFinished;
                           mdlStatic.SetMsg(lblMsg,"Đã xóa Quốc gia có mã: " + Country_Code + " ra khỏi hệ thống.",false);
                            SetControlStatus();
                            CurrentCellChanged();
                        }
                        else//Có lỗi xảy ra
                        {
                            switch (DeleteResult)
                            {
                                case ActionResult.DataHasUsedinAnotherTable:
                                   mdlStatic.SetMsg(lblMsg,"Quốc gia có mã: " + Country_Code + " đã được sử dụng trong bảng khác nên bạn không thể xóa!",true);
                                    break;
                                default:
                                   mdlStatic.SetMsg(lblMsg,"Lỗi khi xóa Quốc gia. Liên hệ với VBIT",true);
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
        /// Lấy danh sách quốc gia và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {
            DataSource = new CountryController().GetAllData().Tables[0];
            Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true,"", "Pos,Country_Name");
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
            SearchData(txtSearchCode.Text.Trim(), txtSearchName.Text.Trim());
        }
        /// <summary>
        /// Lấy danh sách quốc gia và Binding vào DataGridView
        /// </summary>
        private void SearchData(string _Code,string _Name)
        {
            string WhereCondition = "";
            if(_Code.Trim()!="")
                WhereCondition= " Country_Code='" + _Code + "'";
            if (_Name.Trim() != "")
                if (WhereCondition != "") WhereCondition += " AND Country_Name like '%" + _Name + "%'";
                else
                    WhereCondition = " Country_Name like '%" + _Name + "%'";
            if (_Code.Trim() == "" && _Name.Trim() == "") WhereCondition = " 1=1";
            DataSource = new CountryController().GetData(WhereCondition).Tables[0];
            Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "Pos,Country_Name");
            CurrentCellChanged();
        }
    }
}
