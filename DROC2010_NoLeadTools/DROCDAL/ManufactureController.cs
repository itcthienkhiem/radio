using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class ManufactureController
    {
        //
        // TODO: Add constructor logic here
        //
        public ManufactureController()
        {
        }
        public ManufactureInfor Infor;


        public ManufactureController(ManufactureInfor Infor)
        {
            this.Infor = Infor;
        }
        public ManufactureInfor _Infor
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
                SQLstring = "INSERT INTO L_ManufactureList(Manufacture_Code,Manufacture_Name,[Desc],Pos) VALUES('" + Infor.Manufacture_Code + "','" + Infor.Manufacture_Name + "','" + Infor.Desc + "'," + Infor.Pos + ")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Manufacture_ID =Convert.ToInt16( Utility.getCurrentMaxID("Manufacture_ID", "L_ManufactureLIST"));
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
        /// Cập nhật đối tượng Manufacture có trong bảng CSDL
        /// Đầu vào là đối tượng ManufactureInfor
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
                    SQLstring = "UPDATE  L_ManufactureList SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_ManufactureList SET Manufacture_Code='" + Infor.Manufacture_Code + "',Manufacture_Name='" + Infor.Manufacture_Name + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos + " WHERE Manufacture_ID=" + Infor.Manufacture_ID;
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
        /// Xóa đối tượng Manufacture có trong CSDL dựa vào Infor.Manufacture_Code
        /// Đầu vào là đối tượng ManufactureInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_ManufactureList WHERE Manufacture_Code='" + Infor.Manufacture_Code + "' AND Manufacture_ID=" + Infor.Manufacture_ID;
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
        public static ManufactureInfor GetInfor(DataRow dr)
        {
            ManufactureInfor Infor = new ManufactureInfor();
            if (dr != null)
            {

                Infor.Manufacture_Code = Utility.sDbnull(dr["Manufacture_Code"]);
                Infor.Manufacture_ID = Utility.Int16Dbnull(dr["Manufacture_ID"]);
                Infor.Manufacture_Name = Utility.sDbnull(dr["Manufacture_Name"]);
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
        public static ManufactureInfor GetInfor(string ID)
        {
            DataSet ds = new ManufactureController().GetData("Manufacture_Code='" + ID+"'");
            ManufactureInfor Infor = new ManufactureInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Manufacture_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Manufacture_ID"]);
                    Infor.Manufacture_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Manufacture_Code"]);
                    Infor.Manufacture_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Manufacture_Name"]);
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
        public static ManufactureInfor GetInfor(Int16 ID)
        {
            DataSet ds = new ManufactureController().GetData("Manufacture_ID=" + ID );
            ManufactureInfor Infor = new ManufactureInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Manufacture_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Manufacture_ID"]);
                    Infor.Manufacture_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Manufacture_Code"]);
                    Infor.Manufacture_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Manufacture_Name"]);
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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, ManufactureInfor Infor)
        {

            if (Infor != null)
            {
                dr["Manufacture_ID"] =Infor.Manufacture_ID;
                dr["Manufacture_Code"] = Utility.sDbnull(Infor.Manufacture_Code);
                dr["Manufacture_Name"] = Utility.sDbnull(Infor.Manufacture_Name);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);
            }
        }
        /// <summary>
        /// Lấy dữ liệu Manufacture dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *, 0 as OldPos FROM L_ManufactureList WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Manufacture có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,0 as OldPos FROM L_ManufactureList ORDER BY Pos,Manufacture_Name");
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ManufactureList WHERE Manufacture_Code='" + Infor.Manufacture_Code + "'");
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
