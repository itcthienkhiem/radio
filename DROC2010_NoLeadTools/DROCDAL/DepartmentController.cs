using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class DepartmentController
    {
        //
        // TODO: Add constructor logic here
        //
        public DepartmentController()
        {
        }
        public DepartmentInfor Infor;


        public DepartmentController(DepartmentInfor Infor)
        {
            this.Infor = Infor;
        }
        public DepartmentInfor _Infor
        {
            get { return Infor; }
            set { Infor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Trans"></param>
        /// <returns></returns>
        public ActionResult Insert()
        {
            string SQLstring = "";
            try
            {
                if (Exists())
                {
                    return ActionResult.ExistedRecord;
                }
                SQLstring = "INSERT INTO L_DepartmentList(Department_Code,Department_Name,[Desc],Pos) VALUES('" + Infor.Department_Code + "','" + Infor.Department_Name + "','" + Infor.Desc + "'," + Infor.Pos + ")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Department_ID =Convert.ToInt16( Utility.getCurrentMaxID("Department_ID", "L_DEPARTMENTLIST"));
                    return ActionResult.Success;
                }
                else
                {
                    return ActionResult.Error;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        /// <summary>
        /// Cập nhật đối tượng Department có trong bảng CSDL
        /// Đầu vào là đối tượng DepartmentInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            try
            {
                string SQLstring = null;
                //Cập nhật số thứ tự cho nước đổi số thứ tự với nước đang được chọn
                if (Infor.Pos != Infor.OldPos)
                {
                    SQLstring = "UPDATE  L_DepartmentList SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_DepartmentList SET Department_Code='" + Infor.Department_Code + "',Department_Name='" + Infor.Department_Name + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos + " WHERE Department_ID=" + Infor.Department_ID;
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    return ActionResult.Success;
                }
                else
                {
                    return ActionResult.Error;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }

            
        }
        /// <summary>
        /// Xóa đối tượng Department có trong CSDL dựa vào Infor.Department_Code
        /// Đầu vào là đối tượng DepartmentInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_DepartmentList WHERE Department_Code='" + Infor.Department_Code + "' AND Department_ID=" + Infor.Department_ID;
                if (!CanDelete())
                {
                    return ActionResult.DataHasUsedinAnotherTable;
                }
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    return ActionResult.Success;
                }
                else
                {
                    return ActionResult.Error;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        /// <summary>
        /// Trả về đối tượng Infor dựa vào Primary key của nó
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DepartmentInfor GetInfor(DataRow dr)
        {
            DepartmentInfor Infor = new DepartmentInfor();
            if (dr != null)
            {

                Infor.Department_Code = Utility.sDbnull(dr["Department_Code"]);
                Infor.Department_ID = Utility.Int16Dbnull(dr["Department_ID"]);
                Infor.Department_Name = Utility.sDbnull(dr["Department_Name"]);
                Infor.Pos = Utility.Int16Dbnull(dr["Pos"]);
                Infor.Desc = Utility.sDbnull(dr["Desc"]);
                    return Infor;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Trả về đối tượng Infor dựa vào Primary key của nó
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DepartmentInfor GetInfor(string ID)
        {
            DataSet ds = new DepartmentController().GetData("Department_Code='" + ID+"'");
            DepartmentInfor Infor = new DepartmentInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Department_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Department_ID"]);
                    Infor.Department_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Department_Code"]);
                    Infor.Department_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Department_Name"]);
                    Infor.Pos = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Pos"]);
                    Infor.Desc = Utility.sDbnull(ds.Tables[0].Rows[0]["Desc"]);
                    return Infor;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Trả về đối tượng Infor dựa vào Primary key của nó
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DepartmentInfor GetInfor(Int16 ID)
        {
            DataSet ds = new DepartmentController().GetData("Department_ID=" + ID );
            DepartmentInfor Infor = new DepartmentInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Department_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Department_ID"]);
                    Infor.Department_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Department_Code"]);
                    Infor.Department_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Department_Name"]);
                    Infor.Pos = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Pos"]);
                    Infor.Desc = Utility.sDbnull(ds.Tables[0].Rows[0]["Desc"]);
                    return Infor;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Tạo DataRow của Entity từ ObjectInfor
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="Infor">ObjectInfor</param>
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, DepartmentInfor Infor)
        {

            if (Infor != null)
            {
                dr["Department_ID"] =Infor.Department_ID;
                dr["Department_Code"] = Utility.sDbnull(Infor.Department_Code);
                dr["Department_Name"] = Utility.sDbnull(Infor.Department_Name);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);
            }
        }
        /// <summary>
        /// Lấy dữ liệu Department dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *, 0 as OldPos FROM L_DepartmentList WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Department có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,0 as OldPos FROM L_DepartmentList ORDER BY Pos,Department_Name");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private bool CanDelete()
        {
            return true;
        }
      
        /// <summary>
        /// Kiểm tra sự tồn tại của một mã nước
        /// </summary>
        /// <returns>true nếu tồn tại. False nếu chưa tồn tại</returns>
        private bool Exists()
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_DepartmentList WHERE Department_Code='" + Infor.Department_Code + "'");
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
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
