using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using VB6 = Microsoft.VisualBasic.Strings;
using Information = Microsoft.VisualBasic.Information;
using SubSonic;
namespace VietBaIT.CommonLibrary
{
    /// <summary>
    /// Global Module chứa các biến dùng chung cho hệ thống và các hàm dùng chung có liên quan đến thao tác trong CSDL của hệ thống
    /// </summary>
    public class globalVariables
    {
        public static bool LoginSucess = false;
        public static int G_DUOCNOITRU = 1;
        public static int G_DUOCNGOAITRU = 1;
        public static bool gv_ConnectSuccess = false;
        public static string MaKieuDV = "XQ";
        public static string ACCOUNTCLINIC = "01065";
        public static string SERVEY_CODE = "01";
        public static string BHYT = "BHYT";
        public static int CURRENT_YEAR = DateTime.Now.Year;
        public static string ServerName = ".";
        public static string sUName = "sa";
        public static string sPwd = "sa";
        public static string sDbName = "RISLINK_DB";
        public static string ConfigRadio = "RADIO";
        public static string thumbnail_img = "_thumb";
        public static string Thumbnail_NoImage = "noimage.jpg";
        //Khai báo các biến toàn cục sử dụng trong CT
        //Biến kết nối tới CSDL-->Bạn chỉ việc dùng biến này mà không cần khởi tạo chúng
        /// <summary>
        /// Biến kết nối tới CSDL. Sẽ được khởi tạo ở phần Core của hệ thống
        /// </summary>
        public static SqlConnection SqlConn;

        public static System.Data.OleDb.OleDbConnection OleDbConnection;

        /// <summary>
        /// Chuỗi kết nối tới CSDL SqlConn.ConnectionString
        /// </summary>
        public static string SqlConnectionString;

        /// <summary>
        /// Provider Name của Subsonic mặc định là ORM
        /// </summary>
        public static string ProviderName = "ORM";

        public static SqlDataProvider SQLProvider;

        /// <summary>
        /// Mã nơi khám chữa bệnh ban đầu
        /// </summary>
        public static string ClinicCode = "065";

        public static string Branch_ID = "HIS";
        public static string InsCityCode = "01";

        /// <summary>
        /// Tên đơn vị làm việc
        /// </summary>
        /// <summary>
        /// <summary>
        public static string Branch_Name = "HIS";

        /// <summary>
        /// Tên đơn vị cấp trên
        /// </summary>
        public static string ParentBranch_Name = "Lĩnh Nam";

        /// <summary>
        /// Admin đăng nhập?
        /// </summary>
        public static bool IsAdminLogin = false;

        /// <summary>
        /// Địa chỉ đơn vị làm việc
        /// </summary>
        public static string Branch_Address = "Hà Nội";

        public static string Branch_Phone = "0904 648006";
        public static string Branch_Email = "trangdm@daosen.com.vn";
        public static string Branch_Website = "www.daosen.com.vn";

        /// <summary>
        /// Tên đăng nhập vào hệ thống
        /// </summary>
        public static string UserName = "";

        //Tháng Năm hệ thống
        //Tháng làm việc(Chưa dùng)
        public static int gv_intCurrMonth;
        //Năm làm việc(Chưa dùng)
        public static int gv_intCurrYear;

        /// <summary>
        /// Ngôn ngữ hiển thị của hệ thống: VN hoặc EN
        /// </summary>
        public static string DisplayLanguage = "VN";

        public static string gv_sFormat = "### ### ### ### ###.##";
        //Bạn phải Load lại từ File Config nếu muốn dùng. Tiếng Việt có mã=VN. Tiếng Anh có mã=EN
        public static string gv_sAnnouce = "Thông báo";
        public static DataGridView CurrDtGridView;
        public static ArrayList gv_arrKeySearch = new ArrayList();
        public static bool gv_bCrptHasCached = false;
        public static bool IsAdmin = false;
        public static System.DateTime SysDate = System.DateTime.Now;
        public static int gv_intServiceType_ID = -1;
        public static string gv_sStaffName = "";
        public static string DepartmentCode= "-1";
        public static Int16 gv_StaffID = -1;
        public static Int32 gv_DepartmentID = -1;
        public static string gv_MaTBi = "";
        public static bool gv_ExistsThietBi = false;
        public static int AnnounceTime = 10;
        public static int CheckByService = 0;
        public static string DelegateUser = "";
        public static ArrayList ArrDelegateUser;
        public static string PaymentList = "";
        public static string ServiceList = "";
        public static string FunctionName = "";
        public static string SubSystemName = "";
        public static int FunctionID = -1;
        public static bool FinishProcess = false;
        public static string CurrCodeGenStatus = "";
        public static string sFolderName =Application.StartupPath+"/File_Config/";
        public static string AssName = "";
        public static string AppActiveKey = "";
        public static string StoreKeepers = "";
        public static string Doctors = "";
        public static string Guardian = "";
        public static string Clerk = "";
        public static string Assistant = "";
        public static string Receptionist = "";
        public static string DiagnosticDoctor = "";
        public static string AssignDoctor = "";
        public static string Accountant = "";
        public static string Druggist = "";
        public static string FlowName = "FLOW1";
        public static int Display = 0;

        ///<summary>
        ///</summary>
        public static string FORMTITLE = "VietBaIT.JSC";

        /// <summary>
        /// 
        /// </summary>
        ///bien khai bao cua TuDN
        public static bool b_ConnectedLis = false;

        public static bool b_LISConnectionState = false;
        public static DateTime v_dtGetDateTime = DateTime.Now;
        public static  DataTable p_DataDiaChinh=new DataTable();
        public static  DataTable p_DataQuocGia=new DataTable();
        public static DataTable p_DataDanhMucDungChung=new DataTable();
        public static DataTable P_DataChuVu = new DataTable();

        public static System.Drawing.Color SystemColor = Color.FromArgb(((int) (((byte) (255)))),
                                                                        ((int) (((byte) (224)))),
                                                                        ((int) (((byte) (192)))));



        //BIẾN SỬ DỤNG ĐỂ CÀI ĐẶT THUỘC TÍNH CHO CỘT GIÁ TRONG KÊ ĐƠN
        public static int g_Lock_Pres = 0;
        // public static string="System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))))";

        /// <summary>
        /// Lấy thời gian của hệ thống
        /// </summary>
        /// <returns></returns>
        //public static DateTime GetSysDateTime = new InlineQuery().ExecuteScalar<DateTime>("SELECT GETDATE()");    
    }




}
