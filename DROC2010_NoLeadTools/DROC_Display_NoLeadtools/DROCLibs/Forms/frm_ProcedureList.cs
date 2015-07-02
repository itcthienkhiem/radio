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
    public partial class frm_ProcedureList : BaseForm
    {
        #region "Attributes"
        /// <summary>
        /// Tên của DLL chứa Form này. Được sử dụng cho mục đích SetMultilanguage và cấu hình DataGridView
        /// </summary>
        private string AssName = "";
       
        /// <summary>
        /// Khai báo Entity chứa dữ liệu
        /// </summary>
        private DataTable ProcedureEntity = new ProcEntity.ProcedureEntityDataTable();
        //Khai báo gói thông tin chứa ProcedureEntity và Header truyền lên BL để xử lý nghiệp vụ
        private DataSet DataEntity = new DataSet();
        /// <summary>
        /// Datasource là danh sách Procedure hiển thị trên lưới
        /// </summary>
        private DataTable DataSource = new DataTable();
        /// <summary>
        /// Có cho phép phản ánh dữ liệu trên lưới vào các Control hay không? 
        /// Mục đích khi nhấn Insert, Delete thì khi chọn trên lưới sẽ ko thay đổi dữ liệu trong các Control bên dưới.
        /// Ở chế độ bình thường thì khi chọn trên lưới sẽ phản ánh dữ liệu xuống các Control để sẵn sàng thao tác.
        /// </summary>
        private bool AllowSelecttionChangedOnTreeView = true;
        /// <summary>
        /// Thao tác đang thực hiện là gì: Insert, Delete, Update hay Select...
        /// </summary>
        private action Act;
        TraceInfor Trace;
        /// <summary>
        /// Biến để tránh trường hợp khi gán Datasource cho GridView thì xảy ra sự kiện SelecttionChanged
        /// Điều này là do 2 Thread thực hiện song song nhau. Do vậy ta cần xử lý nếu chưa Loaded xong thì
        /// chưa cho phép binding dữ liệu từ Gridview vào Control trên Form trong sự kiện SelecttionChanged
        /// </summary>
        private bool bLoaded = false;
        private Int16 OldPos = 0;
        #endregion
        public frm_ProcedureList()
        {
            InitializeComponent();
            tvwProcedure.KeyDown += new KeyEventHandler(tvwProcedure_KeyDown);
            tvwProcedure.KeyPress += new KeyPressEventHandler(tvwProcedure_KeyPress);
            tvwProcedure.AfterSelect += new TreeViewEventHandler(tvwProcedure_AfterSelect);
            tvwProcedure.MouseDown += new MouseEventHandler(tvwProcedure_MouseDown);
            tvwProcedure.ItemDrag += new ItemDragEventHandler(tvwProcedure_ItemDrag);
            tvwProcedure.DragEnter += new DragEventHandler(tvwProcedure_DragEnter);
            tvwProcedure.DragDrop += new DragEventHandler(tvwProcedure_DragDrop);
            //mdlStatic._virtualKeyboard.TopMost = true;
            //mdlStatic.AttatchVirtualKeyboard(this);
        }

        void tvwProcedure_DragDrop(object sender, DragEventArgs e)
        {
            //TreeNode DragNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            //TreeNode TemNode = null;
            //TreeNode DestinationNode = null;
            //int intID = 0;
            //int intParenID = 0;
            //int intDesParentRoleID = 0;
            //int iFuntionID = 0;
            //if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            //{
            //    Point pt = default(Point);
            //    pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            //    DestinationNode = ((TreeView)sender).GetNodeAt(pt);

            //    if ((DestinationNode != null))
            //    {
            //        //Kiểm tra đảm bảo chỉ thực hiện kéo thả giữa các Role Node và Node nguồn không được là Node phân hệ
            //        if (DragNode.Tag.ToString().ToUpper().Contains("LEAF") && DestinationNode.Tag.ToString().ToUpper().Contains("ROOT"))
            //        {
            //            //Kiểm tra xem node đích và Node nguồn có là một hay không?
            //            if ((!object.ReferenceEquals(DestinationNode, DragNode)))
            //            {
            //                //Ngăn không cho kéo Node vào chính cha của nó
            //                if ((!object.ReferenceEquals(DestinationNode, DragNode.Parent)))
            //                {
            //                    //Lấy về mã Role và Mã ParentRole
            //                    intSourceID = (int)DragNode.Tag.ToString().Substring(DragNode.Tag.ToString().IndexOf("#") + 1);
            //                    intSourceParentID = (int)DestinationNode.Tag.ToString().Substring(DestinationNode.Tag.ToString().IndexOf("#") + 1);
            //                    if (txtFunctionID.Text.Trim == string.Empty)
            //                    {
            //                        txtFunctionID.Text = "-1";
            //                    }
            //                    iFuntionID = (int)txtFunctionID.Text;
            //                    //Gán ChildNode cho DesNode
            //                    TemNode = ((TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", false)).Clone();

            //                    //----------------------------------------------------------------------------
            //                    if ((e.KeyState & CtrlMask) != CtrlMask)
            //                    {
            //                        //Cut
            //                        //Cập nhật vào CSDL
            //                        UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(false) + 1, GetDataRow(intSourceRoleID), true);
            //                        //RecursiveNode(DragNode, False)
            //                        DragNode.Remove();
            //                    }
            //                    else
            //                    {
            //                        //Copy
            //                        //Cập nhật vào CSDL
            //                        UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(false) + 1, GetDataRow(intSourceRoleID), false);
            //                        //Cập nhật lại FunctionID và RoleID cho Role đích
            //                        {
            //                            TemNode.Tag = "LEAFROLES|" + iFuntionID + "#" + _clsRole.iGetNewestRole;
            //                        }
            //                        //Gọi thủ tục cập nhật đệ quy khi Node được kéo thả là một ParentNode
            //                        RecursiveNode(TemNode, false);
            //                    }

            //                    DestinationNode.Nodes.Add(TemNode);
            //                    gv_bRoleHasChanged = true;
            //                    //-----------------------------------------------------------------------------
            //                    if (!mv_bLoading) mdlStatic.SetMsg(lblMsg,"Đã kéo thả vị trí thành công",false);
            //                    tvwAdminSystem.SelectedNode = DestinationNode;
            //                    DestinationNode.Expand();
            //                }
            //            }

            //        }
            //        else
            //        {
            //           mdlStatic.SetMsg(lblMsg,"Đã kéo thả Role thành công",false);
            //        }
            //    }
            //    else
            //    {
            //       mdlStatic.SetMsg(lblMsg, "Không được kéo thả Node phân hệ", false);
            //    }
            //}

        }

        void tvwProcedure_DragEnter(object sender, DragEventArgs e)
        {
            //try
            //{
            //    if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            //    {
            //        if (((TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode")).Tag.ToString().ToUpper().Contains("LEAF"))
            //        {
            //            e.Effect = DragDropEffects.Copy;
            //        }
            //    }
            //}
            //catch
            //{
            //}
        }

        void tvwProcedure_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //try
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        if (((TreeNode)e.Item).Tag.ToString().Contains("LEAF"))//Nút lá mới được kéo thả
            //        {
            //            DoDragDrop(e.Item, DragDropEffects.Move || DragDropEffects.Copy);
            //        }
            //    }
            //}
            //catch
            //{
            //}
        }
        void SelectNode(TreeNode currentNode)
        {

            try
            {
                if (!AllowSelecttionChangedOnTreeView) return;
                string Tag = currentNode.Tag.ToString();
                if (Tag.Contains("ROOT"))
                {
                    txtID.Clear();
                    txtParent.Clear();
                    txtPos.Clear();
                    txtCode.Clear();
                    txtStandardName.Clear();
                    txtDisplayName.Clear();
                    txtDesc.Clear();
                    txtPrice.Clear();
                    chkEmerency.Checked = false;
                    cmdInsert.Enabled = true;
                    cmdUpdate.Enabled = false;
                    cmdDelete.Enabled = false;

                }
                else
                {
                    cmdInsert.Enabled = true;
                    cmdUpdate.Enabled = true;
                    cmdDelete.Enabled = true;
                    DataRow[] arrDr = DataSource.Select("Procedure_ID=" + Tag.Split('#')[0]);
                    if (arrDr.GetLength(0) > 0)
                    {
                        txtID.Text = Utility.sDbnull(arrDr[0]["Procedure_ID"]);
                        txtParent.Text = currentNode.Parent.Text;
                        txtPos.Text = Utility.sDbnull(arrDr[0]["Pos"]);
                        txtCode.Text = Utility.sDbnull(arrDr[0]["PROCEDURE_CODE"]);
                        txtStandardName.Text = Utility.sDbnull(arrDr[0]["STANDARD_NAME"]);
                        txtDisplayName.Text = Utility.sDbnull(arrDr[0]["DISPLAY_NAME"]);
                        txtDesc.Text = Utility.sDbnull(arrDr[0]["Desc"]);
                        txtPrice.Text = Utility.sDbnull(arrDr[0]["Price"]);
                        chkEmerency.Checked = Utility.sDbnull(arrDr[0]["ISEmerency"], "0") == "0" ? false : true;
                        cboDirectionCapture.SelectedIndex = Convert.ToInt32(arrDr[0]["DirectionCapture"]);
                    }
                    if (Tag.Contains("LEAF")) cmdInsert.Enabled = false;
                }
            }
            catch
            {
            }
        }
        void tvwProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (!tvwProcedure.SelectedNode.Equals(tvwProcedure.GetNodeAt(e.X, e.Y))) tvwProcedure.SelectedNode = tvwProcedure.GetNodeAt(e.X, e.Y);
                if (tvwProcedure.SelectedNode.Tag.ToString().ToUpper().Contains("ROOT") || tvwProcedure.SelectedNode.Tag.ToString().ToUpper().Contains("PARENT"))
                {
                    cmdInsert.Enabled = true;
                }
                else
                {
                    cmdInsert.Enabled = false;
                }
                SelectNode(tvwProcedure.SelectedNode);

            }

            catch
            {
            }
        }

        void tvwProcedure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        void tvwProcedure_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        void tvwProcedure_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
            }
            catch
            {
            }
           
        }
        private void LoadData()
        {

        }
        private void frm_ProcedureList_Load(object sender, EventArgs e)
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
            Utility.ResetEntity(ref ProcedureEntity);
            //Create new Row
            DataRow dr = ProcedureEntity.NewRow();
            if (Act == action.Insert)
            {
                dr["Procedure_ID"] = -1;
                dr["PARENT_ID"] = Convert.ToInt16(tvwProcedure.SelectedNode.Tag.ToString().Split('#')[0]);
            }
            else
            {
                dr["Procedure_ID"] = Convert.ToInt16(txtID.Text.Trim());
                dr["PARENT_ID"] = Convert.ToInt16(tvwProcedure.SelectedNode.Parent.Tag.ToString().Split('#')[0]);
            }
            dr["Procedure_Code"] = Utility.GetValue(txtCode.Text, false);
            dr["STANDARD_NAME"] = Utility.GetValue(txtStandardName.Text, false);
            dr["DISPLAY_NAME"] = Utility.GetValue(txtDisplayName.Text, false);
            dr["Price"] = Convert.ToDouble(txtPrice.Text);
            dr["Desc"] = Utility.GetValue(txtDesc.Text, false);
            dr["Pos"] = Convert.ToInt16(txtPos.Text);
            dr["IsEmerency"] = chkEmerency.Checked ? 1 : 0;
            dr["DirectionCapture"] = Convert.ToByte(cboDirectionCapture.SelectedIndex);
            dr["MODALITY_ID"] = Convert.ToInt16(cboModality.SelectedValue);
            dr["MODALITY_Name"] = cboModality.Text;
            //Set giá trị cho cột STT cũ để tráo đổi số TT với bản ghi có STT=dr["Pos"]
            dr["OldPos"] = OldPos;
            ProcedureEntity.Rows.Add(dr);
            ProcedureEntity.AcceptChanges();
            //Add ProcedureEntity into DataEntity
            DataEntity.Tables.Add(ProcedureEntity);
        }
        #endregion

        #region "Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        //Khởi tạo View: Khởi tạo trạng thái các Control, khởi tạo dữ liệu ban đầu...
        //Implements from IProcedureView
        public void InitView()
        {
            //grdList.SelecttionChanged += new EventHandler(grdList_SelecttionChanged);
            Initialize();
            //Lấy về danh sách các Vị trí để hiển thị lên DataGridView
            GetData();
            //Sau khi Binding dữ liệu vào GridView thì mới cho phép thực hiện lệnh trong sự kiện SelecttionChanged
            bLoaded = true;
            //Gọi sự kiện SelecttionChanged để hiển thị dữ liệu từ trên lưới xuống Controls
            SelecttionChanged();
            //Thiết lập giá trị mặc định của DMLType
            Act = action.FirstOrFinished;
            //Thiết lập các giá trị mặc định cho các Control
            SetControlStatus();
        }

        void grdList_SelecttionChanged(object sender, EventArgs e)
        {
            SelecttionChanged();
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
               mdlStatic.SetMsg(lblMsg,"Bạn cần nhập mã Vị trí.",true);
                txtCode.Focus();
                return false;
            }
            if (!Utility.IsNumeric(txtPos.Text))
            {
               mdlStatic.SetMsg(lblMsg,"Số thứ tự phải là chữ số.",true);
                txtPos.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtStandardName.Text))
            {

                mdlStatic.SetMsg(lblMsg, "Bạn cần nhập tên chuẩn.", true);
                txtStandardName.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtDisplayName.Text))
            {

                mdlStatic.SetMsg(lblMsg, "Bạn cần nhập tên hiển thị.", true);
                txtDisplayName.Focus();
                return false;
            }
           
            if (!Utility.IsNumeric(txtPrice.Text))
            {
                mdlStatic.SetMsg(lblMsg, "Đơn giá phải là chữ số.", true);
                txtPrice.Focus();
                return false;
            }
            if (cboModality.Items.Count <= 0)
            {

                mdlStatic.SetMsg(lblMsg, "Bạn phải khởi tạo danh mục thiết bị trước khi thêm vị trí chụp.", true);
                cboModality.Focus();
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
                    //Cho phép nhập liệu mã Vị trí,vị trí, tên Vị trí và mô tả thêm
                    txtID.Text = "Tự sinh";
                    txtCode.Clear();
                    txtStandardName.Clear();
                    txtDisplayName.Clear();
                    txtPrice.Clear();
                    txtDesc.Clear();
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtParent);
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtStandardName);
                    Utility.EnabledTextBox(txtDisplayName);
                    Utility.EnabledTextBox(txtPrice);
                    Utility.EnabledComboBox(cboModality);
                    Utility.EnabledComboBox(cboDirectionCapture);
                    Utility.EnabledTextBox(txtDesc);
                    chkEmerency.Enabled = true;
                    Int16 Parent_ID = 0;
                    Parent_ID = Convert.ToInt16(tvwProcedure.SelectedNode.Tag.ToString().Split('#')[0]);
                    txtPos.Text = Utility.getNextMaxID("Pos", "L_Procedures", Parent_ID).ToString();
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
                    AllowSelecttionChangedOnTreeView = false;
                    //Tự động Focus đến mục ID để người dùng nhập liệu
                    mdlStatic.SetMsg(lblMsg, "Mời bạn nhập thông tin để thêm mới", false);
                    txtCode.Focus();
                    break;
                case action.Update:
                    //Không cho phép cập nhật lại mã Vị trí
                    Utility.DisabledTextBox(txtID);
                    //Cho phép cập nhật lại vị trí, tên Vị trí và mô tả thêm
                    Utility.DisabledTextBox(txtParent);
                    Utility.EnabledTextBox(txtCode);
                    Utility.EnabledTextBox(txtPos);
                    Utility.EnabledTextBox(txtStandardName);
                    Utility.EnabledTextBox(txtDisplayName);
                    Utility.EnabledTextBox(txtPrice);
                    Utility.EnabledComboBox(cboModality);
                    Utility.EnabledComboBox(cboDirectionCapture);
                    Utility.EnabledTextBox(txtDesc);
                    chkEmerency.Enabled = true;
                    OldPos =Convert.ToInt16( tvwProcedure.SelectedNode.Tag.ToString().Split('$')[1]);
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
                    AllowSelecttionChangedOnTreeView = false;
                    //Tự động Focus đến mục Position để người dùng nhập liệu
                    mdlStatic.SetMsg(lblMsg, "Mời bạn nhập thông tin để cập nhật", false);
                    txtPos.Focus();
                    break;
                case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                    //Không cho phép nhập liệu mã Vị trí, tên Vị trí và mô tả thêm
                    Utility.DisabledTextBox(txtID);
                    Utility.DisabledTextBox(txtCode);
                    Utility.DisabledTextBox(txtStandardName);
                    Utility.DisabledTextBox(txtDesc);
                    Utility.DisabledTextBox(txtPos);
                    Utility.DisabledTextBox(txtDisplayName);
                    Utility.DisabledTextBox(txtPrice);
                    Utility.DisabledComboBox(cboModality);
                    Utility.DisabledComboBox(cboDirectionCapture);
                    chkEmerency.Enabled = false;
                    //--------------------------------------------------------------
                    //Thiết lập trạng thái các nút Insert, Update, Delete...
                    //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                    //Cho phép thêm mới
                    cmdInsert.Enabled = true;
                    //Tùy biến nút Update và Delete tùy theo việc có hay không dữ liệu trên lưới
                    //cmdUpdate.Enabled = grdList.RowCount <= 0 ? false : true;
                    //cmdDelete.Enabled = grdList.RowCount <= 0 ? false : true;
                    //cmdPrint.Enabled = grdList.RowCount <= 0 ? false : true;
                    cmdSave.Enabled = false;
                    //Nút Hủy biến thành nút thoát
                    cmdCancel.Enabled = false;
                    //--------------------------------------------------------------
                    //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                    AllowSelecttionChangedOnTreeView = true;
                    //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                    SelecttionChanged();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Xử lý sự kiện SelecttionChanged của DataGridView
        /// Đưa dữ liệu đang chọn từ GridView vào các Controls để người dùng sẵn sàng thao tác Delete hoặc Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelecttionChanged()
        {
            try
            {
                if (AllowSelecttionChangedOnTreeView)
                {
                    SelectNode(tvwProcedure.SelectedNode);
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
            //Gán ProcedureEntity vào DataEntity
            SetValueforEntity();

            //Khởi tạo BusinessRule để xử lý nghiệp vụ
            ProcedureInfor Infor = new ProcedureInfor();
            Utility.MapValueFromEntityIntoObjectInfor(Infor, ProcedureEntity);
            ProcedureController _BusRule = new ProcedureController(Infor);
            switch (Act)
            {
                case action.Insert:
                    //Gọi nghiệp vụ Insert dữ liệu
                    ActionResult InsertResult = _BusRule.Insert();
                    if (InsertResult == ActionResult.Success)//Nếu thành công
                    {
                        //Thêm mới một dòng vào Datasource để cập nhật lại dữ liệu trên DataGridView
                        //phải đảm bảo Datasource và ProcedureEntity có cấu trúc giống nhau mới dùng hàm này
                        DataRow dr = Utility.CopyData(ProcedureEntity.Rows[0], DataSource);
                        dr["Procedure_ID"] = Infor.Procedure_ID;
                        if (dr != null)//99.99% là sẽ !=null
                        {
                            DataSource.Rows.Add(dr);
                            DataSource.AcceptChanges();
                        }
                        //Thêm vào treeView
                         string Code =Infor.Procedure_Code;
                         string newText = "";
                         if (Code.Trim() != "")
                             newText = Code + "-" + Infor.DISPLAY_NAME;
                         else
                             newText = Infor.DISPLAY_NAME;

                         TreeNode newNode = new TreeNode(newText);
                         if (tvwProcedure.SelectedNode.Tag.ToString().Contains("ROOT"))
                         {
                             newNode.Tag = Infor.Procedure_ID.ToString() + "#PARENT";
                             newNode.ForeColor = System.Drawing.Color.DarkBlue;
                         }
                         else
                         {
                             newNode.Tag = Infor.Procedure_ID.ToString() + "#LEAF$" + Infor.Pos.ToString();
                             newNode.ForeColor = System.Drawing.Color.Black;

                         }
                         newNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                         tvwProcedure.SelectedNode.Nodes.Add(newNode);
                         tvwProcedure.SelectedNode.Expand();
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa thêm mới trên lưới. Do txtID chưa bị reset nên dùng luôn
                       mdlStatic.SetMsg(lblMsg,"Thêm mới dữ liệu thành công!",false);

                        SetControlStatus();
                        SelecttionChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (InsertResult)
                        {
                            case ActionResult.ExistedRecord:
                                mdlStatic.SetMsg(lblMsg, "Đã tồn tại Vị trí có mã: " + txtCode.Text.Trim() + ". Đề nghị bạn xem lại",true);
                               Utility.FocusAndSelectAll( txtCode);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi trong quá trình thêm mới Vị trí. Liên hệ với VBIT",true);
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
                        DataRow dr = Utility.GetDataRow(DataSource, "Procedure_ID", txtID.Text.Trim());
                        if (dr != null)
                        {
                            Utility.CopyData(ProcedureEntity.Rows[0], ref dr);
                            DataSource.AcceptChanges();
                        }
                        string Code = Infor.Procedure_Code;
                        string newText = "";
                        if (Code.Trim() != "")
                            newText = Code + "-" + Infor.DISPLAY_NAME;
                        else
                            newText = Infor.DISPLAY_NAME;

                        tvwProcedure.SelectedNode.Text=newText;
                        if (tvwProcedure.SelectedNode.Tag.ToString().Contains("PARENT"))
                            tvwProcedure.SelectedNode.Tag = Infor.Procedure_ID.ToString() + "#PARENT$" + Infor.Pos.ToString();
                        else
                            tvwProcedure.SelectedNode.Tag = Infor.Procedure_ID.ToString() + "#LEAF$" + Infor.Pos.ToString();
                        //Return to the InitialStatus
                        Act = action.FirstOrFinished;
                        //Nhảy đến bản ghi vừa cập nhật trên lưới. Do txtID chưa bị reset nên dùng luôn
                        //Utility.GotoNewRow(grdList, "colProcedure_ID", txtCode.Text.Trim());
                       mdlStatic.SetMsg(lblMsg,"Cập nhật dữ liệu thành công.",false);
                        SetControlStatus();
                        SelecttionChanged();
                    }
                    else//Có lỗi xảy ra
                    {
                        switch (UpdateResult)
                        {
                            case ActionResult.Error:
                               mdlStatic.SetMsg(lblMsg,"Lỗi khi cập nhật Vị trí. Liên hệ với VBIT",true);
                                break;
                            default:
                               mdlStatic.SetMsg(lblMsg,"Lỗi khi cập nhật Vị trí. Liên hệ với VBIT",true);
                                break;
                        }
                    }
                    break;

                case action.Delete:
                    if (Utility.AcceptQuestion("Bạn có muốn xóa Vị trí đang chọn hay không?", "Xác nhận xóa", true))
                    {
                        string Procedure_ID = txtID.Text.Trim();
                        //Gọi nghiệp vụ xóa dữ liệu
                        ActionResult DeleteResult = _BusRule.Delete();
                        if (DeleteResult == ActionResult.Success)//Nếu xóa thành công trong CSDL
                        {
                            //Xóa dòng dữ liệu vừa chọn trong Datasource để cập nhật lại dữ liệu trên DataGridView
                            DataRow dr = Utility.GetDataRow(DataSource, "Procedure_ID", Procedure_ID);
                            if (dr != null)
                            {
                                DataSource.Rows.Remove(dr);
                                DataSource.AcceptChanges();
                            }
                            //remove khỏi TreeView
                            tvwProcedure.Nodes.Remove(tvwProcedure.SelectedNode);
                            //Return to the InitialStatus
                            Act = action.FirstOrFinished;
                            mdlStatic.SetMsg(lblMsg, "Đã xóa Vị trí có ID: " + Procedure_ID + " ra khỏi hệ thống.", false);
                            SetControlStatus();
                            SelecttionChanged();
                        }
                        else//Có lỗi xảy ra
                        {
                            switch (DeleteResult)
                            {
                                case ActionResult.DataHasUsedinAnotherTable:
                                    mdlStatic.SetMsg(lblMsg, "Vị trí có ID: " + Procedure_ID + " đã được sử dụng trong bảng khác nên bạn không thể xóa!", true);
                                    break;
                                default:
                                   mdlStatic.SetMsg(lblMsg,"Lỗi khi xóa Vị trí. Liên hệ với VBIT",true);
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
        /// Lấy danh sách Vị trí và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {

            tvwProcedure.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Danh sách các vị trí chụp");
            rootNode.Tag = "-1#ROOT$-1";
            rootNode.ForeColor = System.Drawing.Color.DarkGreen;
            rootNode.NodeFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold, GraphicsUnit.Point);
            tvwProcedure.Nodes.Add(rootNode);
            tvwProcedure.SelectedNode = rootNode;
            DataSource = new ProcedureController().GetAllData().Tables[0];
            //Bat dau dung cay
            foreach (DataRow dr in DataSource.Rows)
            {
                Int16 Parent_ID = Utility.Int16Dbnull(dr["Parent_ID"]);
                if (Parent_ID == -1)
                {
                    string Code = Utility.sDbnull(dr["PROCEDURE_CODE"]);
                    string newText = "";
                    if (Code.Trim() != "")
                        newText = Code + "-" + dr["DISPLAY_NAME"].ToString();
                    else
                        newText = dr["DISPLAY_NAME"].ToString();
                    //Dung goc cua cay
                    TreeNode NewNode = new TreeNode(newText);
                    NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#PARENT$" + dr["POS"].ToString();
                     NewNode.ForeColor = System.Drawing.Color.DarkBlue;
                     NewNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                    rootNode.Nodes.Add(NewNode);
                    CreateChildNode(NewNode, Convert.ToInt16(dr["PROCEDURE_ID"]), DataSource);
                }
            }
            rootNode.Expand();
            DataTable dtMod = new ModalityController().GetAllData(true).Tables[0];
            dtMod.DefaultView.Sort = "Modality_Name ASC";
            cboModality.DataSource = dtMod.DefaultView;
            cboModality.DisplayMember = "Modality_Name";
            cboModality.ValueMember = "Modality_ID";
        }
        private void CreateChildNode(TreeNode parentNode,Int16 Parent_ID, DataTable dt)
        {
            try
            {
                DataRow[] arrDr = dt.Select("Parent_ID=" + Parent_ID);
                foreach (DataRow dr in arrDr)
                {
                    string Code = Utility.sDbnull(dr["PROCEDURE_CODE"]);
                        string newText = "";
                        if (Code.Trim() != "")
                            newText = Code + "-" + dr["DISPLAY_NAME"].ToString();
                        else
                            newText = dr["DISPLAY_NAME"].ToString();
                        //Dung goc cua cay
                        TreeNode NewNode = new TreeNode(newText);
                        NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#LEAF$" + dr["POS"].ToString();
                        NewNode.ForeColor = System.Drawing.Color.Black;
                        NewNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                        parentNode.Nodes.Add(NewNode);
                   
                }
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

       

      

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData(txtSearchID.Text.Trim(), txtSearchCode.Text.Trim(), txtSearchName.Text.Trim());
        }
        /// <summary>
        /// Lấy danh sách Vị trí và Binding vào DataGridView
        /// </summary>
        private void SearchData(string _ID, string _Code, string _Name)
        {
            string WhereCondition = "";
            if (_ID.Trim() != "")
                WhereCondition = " Procedure_ID=" + Utility.Int16Dbnull(_ID) ;
            if(_Code.Trim()!="")
                if (WhereCondition != "") WhereCondition += " AND Procedure_Code='" + _Code + "'";
                else
                    WhereCondition = " Procedure_Code='" + _Code + "'";
                
            if (_Name.Trim() != "")
                if (WhereCondition != "") WhereCondition += " AND Procedure_Name like '%" + _Name + "%'";
                else
                    WhereCondition = " Procedure_Name like '%" + _Name + "%'";
            if (_ID.Trim()=="" &&  _Code.Trim() == "" && _Name.Trim() == "") WhereCondition = " 1=1";
            DataSource = new ProcedureController().GetData(WhereCondition).Tables[0];
           // Utility.SetDataSourceForDataGridView(grdList, DataSource, false, true, "", "Pos,Procedure_Name");
            SelecttionChanged();
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
    }
}
