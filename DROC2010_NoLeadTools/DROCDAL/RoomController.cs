using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class RoomController
    {
        //
        // TODO: Add constructor logic here
        //
        public RoomController()
        {
        }
        public RoomInfor Infor;


        public RoomController(RoomInfor Infor)
        {
            this.Infor = Infor;
        }
        public RoomInfor _Infor
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
                SQLstring = "INSERT INTO L_RoomList(Room_Code,Room_Name,Department_ID,[Desc],Pos) VALUES('" + Infor.Room_Code + "','" + Infor.Room_Name + "',"+Infor.Department_ID+",'" + Infor.Desc + "'," + Infor.Pos + ")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Room_ID =Convert.ToInt16( Utility.getCurrentMaxID("Room_ID", "L_RoomLIST"));
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
        /// Cập nhật đối tượng Room có trong bảng CSDL
        /// Đầu vào là đối tượng RoomInfor
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
                    SQLstring = "UPDATE  L_RoomList SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos+" AND Department_ID="+Infor.Department_ID;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_RoomList SET Department_ID="+Infor.Department_ID+", Room_Code='" + Infor.Room_Code + "',Room_Name='" + Infor.Room_Name + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos + " WHERE Room_ID=" + Infor.Room_ID;
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
        /// Xóa đối tượng Room có trong CSDL dựa vào Infor.Room_Code
        /// Đầu vào là đối tượng RoomInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_RoomList WHERE Room_Code='" + Infor.Room_Code + "' AND Room_ID=" + Infor.Room_ID + "' AND Department_ID=" + Infor.Department_ID;
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
        public static RoomInfor GetInfor(DataRow dr)
        {
            RoomInfor Infor = new RoomInfor();
            if (dr != null)
            {

                Infor.Room_Code = Utility.sDbnull(dr["Room_Code"]);
                Infor.Room_ID = Utility.Int16Dbnull(dr["Room_ID"]);
                Infor.Department_ID = Utility.Int16Dbnull(dr["Department_ID"]);
                Infor.Department_Name = Utility.sDbnull(dr["Department_Name"]);
                Infor.Room_Name = Utility.sDbnull(dr["Room_Name"]);
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
        public static RoomInfor GetInfor(string ID)
        {
            DataSet ds = new RoomController().GetData("Room_Code='" + ID+"'");
            RoomInfor Infor = new RoomInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Department_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Department_ID"]);
                    Infor.Department_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Department_Name"]);
                    Infor.Room_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Room_ID"]);
                    Infor.Room_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Room_Code"]);
                    Infor.Room_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Room_Name"]);
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
        public static RoomInfor GetInfor(Int16 ID)
        {
            DataSet ds = new RoomController().GetData("Room_ID=" + ID );
            RoomInfor Infor = new RoomInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Department_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Department_ID"]);
                    Infor.Department_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Department_Name"]);
                    Infor.Room_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Room_ID"]);
                    Infor.Room_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Room_Code"]);
                    Infor.Room_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Room_Name"]);
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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, RoomInfor Infor)
        {

            if (Infor != null)
            {
                dr["Department_ID"] = Infor.Department_ID;
                dr["Department_Name"] = Utility.sDbnull(Infor.Department_Name);
                dr["Room_ID"] =Infor.Room_ID;
                dr["Room_Code"] = Utility.sDbnull(Infor.Room_Code);
                dr["Room_Name"] = Utility.sDbnull(Infor.Room_Name);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);
            }
        }
        /// <summary>
        /// Lấy dữ liệu Room dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,(Select TOP 1 Department_Name from L_DepartmentList where Department_ID= R.Department_ID) as Department_Name, 0 as OldPos FROM L_RoomList R WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Room có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,(Select TOP 1 Department_Name from L_DepartmentList where Department_ID= R.Department_ID) as Department_Name,0 as OldPos FROM L_RoomList R ORDER BY Pos,Room_Name");
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_RoomList WHERE Room_Code='" + Infor.Room_Code + "' AND Department_ID="+Infor.Department_ID);
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
