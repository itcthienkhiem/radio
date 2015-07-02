using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
//-------------------------------------------------------------------------------------------------------
//Mục đích: Lớp User nhằm xử lý tất cả các nghiệp vụ liên quan đến người dùng
//Người tạo: CuongDV
//Ngày tạo :09/03/2005
//-------------------------------------------------------------------------------------------------------
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Data.OleDb;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class clsUser
    {
        string mv_sSql;
        public clsUser()
        {
            try
            {
                if (!globalVariables.gv_ConnectSuccess)
                {
                    globalVariables.gv_ConnectSuccess = KhoiTaoKetNoi();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private static string getDBPath()
        {
            string _DefaultDBFile = Application.StartupPath + @"\db\droc.mdb";
            string _DefaultDBFilePath = Application.StartupPath + @"\dbPath.txt";
            try
            {
                if (File.Exists(_DefaultDBFilePath))
                {
                    using (StreamReader _Reader = new StreamReader(_DefaultDBFilePath))
                    {
                        object obj = _Reader.ReadLine();
                        if (obj != null)
                        {
                            string _db2Run = Application.StartupPath + @"\db\" + obj.ToString().ToUpper().Replace(".MDB", "") + ".MDB";
                            if (File.Exists(_db2Run))
                                _DefaultDBFile = _db2Run;
                        }
                    }
                }
                return _DefaultDBFile;
            }
            catch
            {
                return _DefaultDBFile;
            }
        }

        static string gv_sDBName = "";
        static string gv_sComName = ""; 
        static string gv_sConnString = "";
        public static bool KhoiTaoKetNoi()
        {
            ect.Encrypt _ect = new ect.Encrypt("Rijndael");

            string fv_sUID = null;
            string fv_sPWD = null;
            try
            {
                if (bGetConfigInfor(ref fv_sUID, ref fv_sPWD))
                {


                    string sv_sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + getDBPath() + ";Jet OLEDB:Database Password=" + _ect.DBPWD;

                    gv_sConnString = sv_sConnectionString;
                    if ((globalVariables.OleDbConnection == null))
                    {
                        globalVariables.SqlConnectionString = gv_sConnString;
                        globalVariables.OleDbConnection = new System.Data.OleDb.OleDbConnection(sv_sConnectionString);
                        globalVariables.OleDbConnection.Open();

                        GetBranchInfor(globalVariables.Branch_ID);
                    }
                    else if (globalVariables.OleDbConnection.State == ConnectionState.Closed)
                    {
                        globalVariables.OleDbConnection.Open();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được vào CSDL. Liên hệ với quản trị hệ thống\n"+ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        private static void GetBranchInfor(string pv_sBranchID)
        {
            DataSet sv_Ds = new DataSet();
            OleDbDataAdapter sv_DA = new OleDbDataAdapter();
            try
            {
                sv_DA = new OleDbDataAdapter("SELECT * FROM Sys_ManagementUnit WHERE PK_sBranchID='" + pv_sBranchID + "'", globalVariables.OleDbConnection);
                sv_DA.Fill(sv_Ds, "Sys_ManagementUnit");
                if (sv_Ds!=null && sv_Ds.Tables.Count > 0 && sv_Ds.Tables[0].Rows.Count > 0)
                {
                    globalVariables.Branch_Name =Utility.sDbnull(sv_Ds.Tables[0].Rows[0]["sName"]);
                    globalVariables.Branch_Address = Utility.sDbnull(sv_Ds.Tables[0].Rows[0]["sAddress"]);
                    globalVariables.Branch_Phone = Utility.sDbnull(sv_Ds.Tables[0].Rows[0]["sPhone"]);
                    globalVariables.Branch_Email = Utility.sDbnull(sv_Ds.Tables[0].Rows[0]["sEMAIL"]);
                    globalVariables.Branch_Website = Utility.sDbnull(sv_Ds.Tables[0].Rows[0]["WebSite"]);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //------------------------------------------------------------------------------------------------------------
        //Mục đích        : Đọc file cấu hình để lấy về mã đơn vị quản lý, tên CSDL, UserName, Password
        //Đầu vào          :
        //Đầu ra            :Thành công=True. Không thành công=False
        //Người tạo       :CuongDV
        //Ngày tạo         :09/03/2005
        //Nhật kí sửa đổi:
        //------------------------------------------------------------------------------------------------------------
        public static bool bGetConfigInfor(ref string pv_sUID, ref string pv_sPWD)
        {
            DataSet fv_DS = new DataSet();
            string fv_sUID = null;
            string fv_sPWD = null;
            try
            {
                if (File.Exists(Application.StartupPath + "\\Config.xml"))
                {
                    // Tiến hành đọc File cấu hình vào DataSet

                    fv_DS.ReadXml(Application.StartupPath + "\\Config.xml");
                    if (fv_DS.Tables[0].Rows.Count > 0)
                    {
                        // Đọc dữ liệu vào các biến toàn cục
                        //Địa chỉ máy chủ CSDL
                        gv_sComName = fv_DS.Tables[0].Rows[0]["SERVERADDRESS"].ToString();
                        //Mã chi nhánh
                        globalVariables.Branch_ID = fv_DS.Tables[0].Rows[0]["BranchID"].ToString();
                        //UID côngkhai
                        fv_sUID = fv_DS.Tables[0].Rows[0]["USERNAME"].ToString();
                        //Mật khẩu công khai
                        fv_sPWD = fv_DS.Tables[0].Rows[0]["PASSWORD"].ToString();
                        //Tên Cơ sở dữ liệu
                        gv_sDBName = fv_DS.Tables[0].Rows[0]["DATABASE_ID"].ToString();
                        //Ngôn ngữ hiển thị
                        globalVariables.DisplayLanguage = fv_DS.Tables[0].Rows[0]["LANGUAGEDISPLAY"].ToString();


                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu trong File cấu hình! Bạn hãy xem lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Không tồn tại File cấu hình có tên: Config.XML!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static void GetSecretAccount( OleDbConnection pv_Conn, ref string pv_sUID, ref string pv_sPWD)
        {
            DataSet sv_Ds = new DataSet();
            OleDbDataAdapter sv_DA = default(OleDbDataAdapter);
            try
            {
                sv_DA = new OleDbDataAdapter("SELECT * FROM Sys_SECURITY", pv_Conn);
                sv_DA.Fill(sv_Ds, "Sys_SECURITY");
                if (sv_Ds.Tables[0].Rows.Count > 0)
                {
                    pv_sUID = sv_Ds.Tables[0].Rows[0]["sUID"].ToString();
                    pv_sPWD = sv_Ds.Tables[0].Rows[0]["sPWD"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tồn tại tài khoản đăng nhập trong bảng Sys_SECURITY! Đề nghị với DBAdmin tạo tài khoản đăng nhập trong bảng đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn cần gán lại quyền truy cập vào bảng Sys_SECURITY cho tài khoản công khai! Đề nghị với DBAdmin thực hiện công việc này bằng tiện ích CreateUser.exe", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool InsertUser(string pv_sUID, string pv_sFullName, string pv_sDepart, string pv_sDesc)
        {
            OleDbCommand sv_oCmd = null;
            mv_sSql = "INSERT INTO Sys_USERS(PK_sUID,sPWD,sFullName,sDepart,sDesc) VALUES('" + pv_sUID + "','','" + pv_sFullName + "','" + pv_sDepart + "','" + pv_sDesc + "')";
            try
            {
                
                sv_oCmd = new OleDbCommand(mv_sSql, globalVariables.OleDbConnection);
                sv_oCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUser(string pv_sUID, string pv_sFullName, string pv_sDepart, string pv_sDesc)
        {
            OleDbCommand sv_oCmd = null;
            mv_sSql = "UPDATE Sys_USERS SET sFullName='" + pv_sFullName + "',sDepart='" + pv_sDepart + "',sDesc='" + pv_sDesc + "' WHERE UCASE(PK_sUID)='" + pv_sUID + "'";
            try
            {
                sv_oCmd = new OleDbCommand(mv_sSql, globalVariables.OleDbConnection);
                sv_oCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool bDeleteUser(string pv_sUID)
        {
            OleDbCommand sv_oCmd = null;
            mv_sSql = "DELETE FROM  Sys_USERS  WHERE UCASE(PK_sUID)='" + pv_sUID + "'";
            try
            {
                sv_oCmd = new OleDbCommand(mv_sSql, globalVariables.OleDbConnection);
                sv_oCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet dsGetUserInfor(string pv_sUID)
        {
            DataSet sv_DS = new DataSet();
            OleDbDataAdapter sv_DA = null;
            mv_sSql = "SELECT * FROM  Sys_USERS  WHERE UCASE(PK_sUID)='" + pv_sUID + "'";
            try
            {
                sv_DA = new OleDbDataAdapter(mv_sSql, globalVariables.OleDbConnection);
                sv_DA.Fill(sv_DS, "Sys_USERS");
                return sv_DS;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool bChangePassword(string pv_sUID, string pv_sNewPWD)
        {
            OleDbCommand sv_oCmd = null;
            mv_sSql = "UPDATE Sys_USERS SET sPWD='" + pv_sNewPWD + "' WHERE UCASE(PK_sUID)='" + pv_sUID + "'";
            try
            {
                sv_oCmd = new OleDbCommand(mv_sSql, globalVariables.OleDbConnection);
                sv_oCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool bLoginSuccess(string pv_sUID, string pv_sPWD)
        {
            DataSet sv_oDS = new DataSet();
            OleDbDataAdapter sv_oDA = null;
            if (bIsExistedAmin(pv_sUID))
            {
                globalVariables.IsAdminLogin = true;
                return bLoginSuccessAdmin(pv_sUID, pv_sPWD);
            }

            else
            {
                globalVariables.IsAdminLogin = false;
                mv_sSql = "SELECT U.*  FROM Sys_USERS U  WHERE UCASE(PK_sUID)='" + pv_sUID + "' AND sPWD='" + pv_sPWD + "' AND FP_sBranchID='" + globalVariables.Branch_ID + "'";
                //mv_sSql = "SELECT U.* FROM Sys_USERS U  WHERE UCASE(PK_sUID)='" & pv_sUID & "' AND sPWD='" & pv_sPWD & "' AND FP_sBranchID='" &globalVariables.Branch_ID & "'"
                try
                {
                    sv_oDA = new OleDbDataAdapter(mv_sSql, globalVariables.OleDbConnection);
                    sv_oDA.Fill(sv_oDS, "Sys_USERS");
                    if (sv_oDS.Tables[0].Rows.Count > 0)
                    {
                      
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public DataSet dsGetAllUser()
        {
            DataSet sv_oDS = new DataSet();
            OleDbDataAdapter sv_oDA = null;
            try
            {
                mv_sSql = "SELECT * from Sys_USERS order by PK_sUID ASC";
                sv_oDA = new OleDbDataAdapter(mv_sSql, globalVariables.OleDbConnection);
                sv_oDA.Fill(sv_oDS, "Sys_USERS");
                return sv_oDS;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool bIsExisted(string pv_sUID)
        {
            DataSet sv_oDS = new DataSet();
            OleDbDataAdapter sv_oDA = null;
            try
            {
                if (bIsExistedAmin(pv_sUID))
                {
                    return true;
                }
                mv_sSql = "SELECT * from Sys_USERS WHERE UCASE(PK_sUID) ='" + pv_sUID + "' AND FP_sBranchID='" +globalVariables.Branch_ID + "'";
                sv_oDA = new OleDbDataAdapter(mv_sSql, globalVariables.OleDbConnection);
                sv_oDA.Fill(sv_oDS, "Sys_USERS");
                if (sv_oDS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool bIsAdmin(string pv_sUID)
        {
            DataSet sv_oDS = new DataSet();
            OleDbDataAdapter sv_oDA = null;
            try
            {
                mv_sSql = "SELECT * from Sys_ADMINISTRATOR WHERE UCASE(PK_sAdminID) ='" + pv_sUID + "' AND FP_sBranchID='" +globalVariables.Branch_ID + "'";
                sv_oDA = new OleDbDataAdapter(mv_sSql, globalVariables.OleDbConnection);
                sv_oDA.Fill(sv_oDS, "Sys_ADMINISTRATOR");
                if (sv_oDS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    sv_oDS.Tables.Clear();
                    mv_sSql = "SELECT * from Sys_USERS WHERE UCASE(PK_sUID) ='" + pv_sUID + "' AND FP_sBranchID='" +globalVariables.Branch_ID + "' AND iSecurityLevel=1";
                    sv_oDA = new OleDbDataAdapter(mv_sSql, globalVariables.OleDbConnection);
                    sv_oDA.Fill(sv_oDS, "Sys_USERS");
                    if (sv_oDS.Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool bIsExistedAmin(string pv_sUID)
        {
            DataSet sv_oDS = new DataSet();
            OleDbDataAdapter sv_oDA = null;
            try
            {
                mv_sSql = "SELECT * from Sys_Administrator WHERE UCASE(PK_sAdminID) ='" + pv_sUID + "'" + " AND FP_sBranchID='" +globalVariables.Branch_ID + "'";
                sv_oDA = new OleDbDataAdapter(mv_sSql,globalVariables.OleDbConnection);
                sv_oDA.Fill(sv_oDS, "Sys_Administrator");
                if (sv_oDS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        //Mục đích        : Thay đổi mật khẩu quản trị
        //Đầu vào          :Mã người dùng, mật khẩu mới
        //Đầu ra            :Thành công=True. Ngược lại=False
        //Người tạo       :CuongDV
        //Ngày tạo         :09/03/2005
        //Nhật kí sửa đổi:
        //------------------------------------------------------------------------------------------------------------
        public bool bChangePasswordForAdmin(string pv_sUID, string pv_sNewPWD)
        {
            OleDbCommand sv_oCmd = null;
            VietBaIT.Encrypt sv_oEncrypt = new VietBaIT.Encrypt("Rijndael");
            mv_sSql = "UPDATE Sys_ADMINISTRATOR SET sPWD='" + pv_sNewPWD + "' WHERE PK_sAdminID='" + pv_sUID + "'" + " AND FP_sBranchID='" +globalVariables.Branch_ID + "'";
            try
            {
                sv_oCmd = new OleDbCommand(mv_sSql,globalVariables.OleDbConnection);
                sv_oCmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        //Mục đích        : Kiểm tra sự tồn tại của một Account Admin trong CSDL
        //Đầu vào          :UserName của Admin, Password
        //Đầu ra            :Thành công=True. Ngược lại=False
        //Người tạo       :CuongDV
        //Ngày tạo         :09/03/2005
        //Nhật kí sửa đổi:
        //------------------------------------------------------------------------------------------------------------
        public bool bLoginSuccessAdmin(string pv_sUID, string pv_sPWD)
        {
            DataSet sv_oDS = new DataSet();
            OleDbDataAdapter sv_oDA = null;
            mv_sSql = "SELECT * FROM Sys_Administrator  WHERE PK_sAdminID='" + pv_sUID + "' AND sPWD='" + pv_sPWD + "'" + " AND FP_sBranchID='" +globalVariables.Branch_ID + "'";
            try
            {
                sv_oDA = new OleDbDataAdapter(mv_sSql,globalVariables.OleDbConnection);
                sv_oDA.Fill(sv_oDS, "Sys_Administrator");
                if (sv_oDS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
