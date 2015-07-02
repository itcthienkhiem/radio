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
using System.Collections;
namespace VietBaIT.DROC
{
    public partial class frm_ChooseProcedure4Config : Form
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
        public bool m_blnCancel = true;
        public string ProList = "";
        public string ProID = "";
        public ArrayList arrProc = new ArrayList();
        public DataTable AcquisitionDataSource = null;
        public bool CheckExists = false;
        #endregion
        public frm_ChooseProcedure4Config()
        {
            InitializeComponent();
            ProList = "";
            tvwProcedure.MouseDown += new MouseEventHandler(tvwProcedure_MouseDown);
            tvwProcedure.AfterCheck += new TreeViewEventHandler(tvwProcedure_AfterCheck);
            txtValue.KeyDown += new KeyEventHandler(txtValue_KeyDown);
        }

        void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtValue.Text.Trim() != "")
                Search();
        }

        void tvwProcedure_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode _Node in e.Node.Nodes)
            {
                _Node.Checked = e.Node.Checked;
                foreach (TreeNode _Node1 in _Node.Nodes)
                {
                    _Node1.Checked = _Node.Checked;
                }
            }
        }
        void tvwProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (!tvwProcedure.SelectedNode.Equals(tvwProcedure.GetNodeAt(e.X, e.Y))) tvwProcedure.SelectedNode = tvwProcedure.GetNodeAt(e.X, e.Y);


            }

            catch
            {
            }
        }


        private void frm_ChooseProcedure4Config_Load(object sender, EventArgs e)
        {
            InitView();
        }


        #region "Common methods and functions: Initialize,IsValidData, SetControlStatus,..."
        //Khởi tạo View: Khởi tạo trạng thái các Control, khởi tạo dữ liệu ban đầu...
        //Implements from IProcedureView
        public void InitView()
        {
            //grdList.SelecttionChanged += new EventHandler(grdList_SelecttionChanged);
            Initialize();
            //Lấy về danh sách các Vị trí để hiển thị lên DataGridView
            GetData();

        }
        /// <summary>
        /// Lấy danh sách Vị trí và Binding vào DataGridView
        /// </summary>
        private void GetData()
        {
            try
            {

                DataSource = new ProcedureController().GetAllData().Tables[0];
                //Bat dau dung cay
                foreach (DataRow dr in DataSource.Rows)
                {
                    if (CheckExists)
                    {
                        //Tạo cây dịch vụ
                        if (AcquisitionDataSource != null && AcquisitionDataSource.Select("Procedure_ID=" + dr["PROCEDURE_ID"].ToString()).GetLength(0) <= 0)
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
                                NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#PARENT$" + dr["POS"].ToString() + "@" + dr["Standard_Name"].ToString() + "|" + dr["Display_Name"].ToString() + "^" + dr["DirectionCapture"].ToString();
                                NewNode.ForeColor = System.Drawing.Color.DarkBlue;
                                NewNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                                tvwProcedure.Nodes.Add(NewNode);
                                CreateChildNode(NewNode, Convert.ToInt16(dr["PROCEDURE_ID"]), DataSource);
                            }
                        }

                    }
                    else
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
                            NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#PARENT$" + dr["POS"].ToString() + "@" + dr["Standard_Name"].ToString() + "|" + dr["Display_Name"].ToString() + "^" + dr["DirectionCapture"].ToString();
                            NewNode.ForeColor = System.Drawing.Color.DarkBlue;
                            NewNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                            tvwProcedure.Nodes.Add(NewNode);
                            CreateChildNode(NewNode, Convert.ToInt16(dr["PROCEDURE_ID"]), DataSource);
                        }
                    }
                }
                if (CheckExists)
                {
                    //Loại bỏ các node cha 
                    int iParentCount = tvwProcedure.GetNodeCount(false);
                    int i = 0;
                _Continue:
                    foreach (TreeNode _oNode in tvwProcedure.Nodes)
                    {
                        string Code = _oNode.Tag.ToString().Split('#')[0];
                        i += 1;
                        if (_oNode.GetNodeCount(true) <= 0 && DataSource.Select("Parent_ID=" + Code).GetLength(0) > 0)//Loại bỏ các nút cha ko có con và đồng thời con ko nằm trong acqDataSource
                        {
                            tvwProcedure.Nodes.Remove(_oNode);
                        }
                        if (i >= iParentCount) goto _goon;
                        else goto _Continue;
                    }
                }
            _goon:
                //AutoCheck();
                if (tvwProcedure.GetNodeCount(true) <= 0)
                {
                    cmdOK.Enabled = false;
                    cmdSearch.Enabled = false;
                    txtValue.Text = "There is no data to find...";
                    Utility.DisabledTextBox(txtValue);
                }
            }
            catch
            {
            }

        }
        void AutoCheck()
        {
            try
            {
                if (arrProc == null) return;
                foreach (TreeNode _Node in tvwProcedure.Nodes)
                {
                    if (ContainsObject("#" + _Node.Tag.ToString().Split('#')[0] + "#"))
                        _Node.Checked = true;
                    foreach (TreeNode _Node1 in _Node.Nodes)
                    {
                        if (ContainsObject("#" + _Node1.Tag.ToString().Split('#')[0] + "#"))
                            _Node1.Checked = true;

                    }
                }
            }
            catch
            {
            }
        }
        bool ContainsObject(string Value)
        {
            try
            {
                foreach (string s in arrProc)
                {
                    if (s.Contains(Value)) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private void CreateChildNode(TreeNode parentNode, Int16 Parent_ID, DataTable dt)
        {
            try
            {
                DataRow[] arrDr = dt.Select("Parent_ID=" + Parent_ID);
                foreach (DataRow dr in arrDr)
                {
                    if (CheckExists)
                    {
                        if (AcquisitionDataSource != null && AcquisitionDataSource.Select("Procedure_ID=" + dr["PROCEDURE_ID"].ToString()).GetLength(0) <= 0)
                        {
                            string Code = Utility.sDbnull(dr["PROCEDURE_CODE"]);
                            string newText = "";
                            if (Code.Trim() != "")
                                newText = Code + "-" + dr["DISPLAY_NAME"].ToString();
                            else
                                newText = dr["DISPLAY_NAME"].ToString();
                            //Dung goc cua cay
                            TreeNode NewNode = new TreeNode(newText);
                            NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#LEAF$" + dr["POS"].ToString() + "@" + dr["Standard_Name"].ToString() + "|" + dr["Display_Name"].ToString() + "^" + dr["DirectionCapture"].ToString();
                            NewNode.ForeColor = System.Drawing.Color.Black;
                            NewNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                            parentNode.Nodes.Add(NewNode);

                        }
                    }
                    else
                    {
                        string Code = Utility.sDbnull(dr["PROCEDURE_CODE"]);
                        string newText = "";
                        if (Code.Trim() != "")
                            newText = Code + "-" + dr["DISPLAY_NAME"].ToString();
                        else
                            newText = dr["DISPLAY_NAME"].ToString();
                        //Dung goc cua cay
                        TreeNode NewNode = new TreeNode(newText);
                        NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#LEAF$" + dr["POS"].ToString() + "@" + dr["Standard_Name"].ToString() + "|" + dr["Display_Name"].ToString() + "^" + dr["DirectionCapture"].ToString();
                        NewNode.ForeColor = System.Drawing.Color.Black;
                        NewNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold, GraphicsUnit.Point);
                        parentNode.Nodes.Add(NewNode);

                    }
                }
            }
            catch
            {
            }
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

        #endregion

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        private void Search()
        {
            foreach (TreeNode _node in tvwProcedure.Nodes)
            {
                if (_node.Text.Contains(txtValue.Text.Trim()) || txtValue.Text.Trim().Contains(_node.Text))
                {
                    tvwProcedure.SelectedNode = _node;
                    return;
                }
                else
                {
                    foreach (TreeNode _node1 in _node.Nodes)
                    {
                        if (_node1.Text.Contains(txtValue.Text.Trim()) || txtValue.Text.Trim().Contains(_node1.Text))
                        {
                            tvwProcedure.SelectedNode = _node1;
                            return;
                        }
                        else
                        {
                        }
                    }
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            ProList = "";

            arrProc.Clear();
            string _Name = "";
            string Standard_Name = "";
            string Display_Name = "";
            string DirectionCapture = "0";
           TreeNode _Node = tvwProcedure.SelectedNode;


            //NewNode.Tag = dr["PROCEDURE_ID"].ToString() + "#PARENT$" + dr["POS"].ToString() + "@" + dr["Standard_Name"].ToString() + "|" + dr["Display_Name"].ToString();
            ProList += _Node.Text.Split('-')[0] + ";";
            _Name = _Node.Tag.ToString().Split('@')[1].Split('^')[0];
            Standard_Name = _Name.Split('|')[0];
            Display_Name = _Name.Split('|')[1];
            DirectionCapture = _Node.Tag.ToString().Split('@')[1].Split('^')[1];
            arrProc.Add("#" + _Node.Tag.ToString().Split('#')[0] + "#" + _Node.Text.Split('-')[0] + "#" + Standard_Name + "#" + Display_Name + "#" + DirectionCapture);



            if (tvwProcedure.SelectedNode == null || tvwProcedure.SelectedNode.GetNodeCount(true) > 0)
            {
                Utility.ShowMsg(MultiLanguage.GetText(globalVariables.DisplayLanguage, "You have to check at least one Procedure", "You have to check at least one Procedure"));
                return;
            }
            m_blnCancel = false;
            this.Close();
        }


    }
}
