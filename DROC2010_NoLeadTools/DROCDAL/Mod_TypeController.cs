using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class Mod_TypeController
    {
        //
        // TODO: Add constructor logic here
        //
        public Mod_TypeController()
        {
        }
        public Mod_TypeInfor Infor;


        public Mod_TypeController(Mod_TypeInfor Infor)
        {
            this.Infor = Infor;
        }
        public Mod_TypeInfor _Infor
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
                SQLstring = "INSERT INTO L_MODALITY_TYPE(Mod_Type_Code,Mod_Type_Name,[Desc],Pos) VALUES('" + Infor.Mod_Type_Code + "','" + Infor.Mod_Type_Name + "','" + Infor.Desc + "'," + Infor.Pos + ")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Mod_Type_ID =Convert.ToInt16( Utility.getCurrentMaxID("Mod_Type_ID", "L_MODALITY_TYPE"));
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
        /// Cập nhật đối tượng Mod_Type có trong bảng CSDL
        /// Đầu vào là đối tượng Mod_TypeInfor
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
                    SQLstring = "UPDATE  L_MODALITY_TYPE SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_MODALITY_TYPE SET Mod_Type_Code='" + Infor.Mod_Type_Code + "',Mod_Type_Name='" + Infor.Mod_Type_Name + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos + " WHERE Mod_Type_ID=" + Infor.Mod_Type_ID;
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
        /// Xóa đối tượng Mod_Type có trong CSDL dựa vào Infor.Mod_Type_Code
        /// Đầu vào là đối tượng Mod_TypeInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_MODALITY_TYPE WHERE Mod_Type_Code='" + Infor.Mod_Type_Code + "' AND Mod_Type_ID=" + Infor.Mod_Type_ID;
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
        public static Mod_TypeInfor GetInfor(DataRow dr)
        {
            Mod_TypeInfor Infor = new Mod_TypeInfor();
            if (dr != null)
            {

                Infor.Mod_Type_Code = Utility.sDbnull(dr["Mod_Type_Code"]);
                Infor.Mod_Type_ID = Utility.Int16Dbnull(dr["Mod_Type_ID"]);
                Infor.Mod_Type_Name = Utility.sDbnull(dr["Mod_Type_Name"]);
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
        public static Mod_TypeInfor GetInfor(string ID)
        {
            DataSet ds = new Mod_TypeController().GetData("Mod_Type_Code='" + ID+"'");
            Mod_TypeInfor Infor = new Mod_TypeInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Mod_Type_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Mod_Type_ID"]);
                    Infor.Mod_Type_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Mod_Type_Code"]);
                    Infor.Mod_Type_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Mod_Type_Name"]);
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
        public static Mod_TypeInfor GetInfor(Int16 ID)
        {
            DataSet ds = new Mod_TypeController().GetData("Mod_Type_ID=" + ID );
            Mod_TypeInfor Infor = new Mod_TypeInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Mod_Type_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Mod_Type_ID"]);
                    Infor.Mod_Type_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Mod_Type_Code"]);
                    Infor.Mod_Type_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Mod_Type_Name"]);
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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, Mod_TypeInfor Infor)
        {

            if (Infor != null)
            {
                dr["Mod_Type_ID"] =Infor.Mod_Type_ID;
                dr["Mod_Type_Code"] = Utility.sDbnull(Infor.Mod_Type_Code);
                dr["Mod_Type_Name"] = Utility.sDbnull(Infor.Mod_Type_Name);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);
            }
        }
        /// <summary>
        /// Lấy dữ liệu Mod_Type dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *, 0 as OldPos FROM L_MODALITY_TYPE WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Mod_Type có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,0 as OldPos FROM L_MODALITY_TYPE ORDER BY Pos,Mod_Type_Name");
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_MODALITY_TYPE WHERE Mod_Type_Code='" + Infor.Mod_Type_Code + "'");
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
