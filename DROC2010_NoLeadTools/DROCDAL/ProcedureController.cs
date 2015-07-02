using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class ProcedureController
    {
        //
        // TODO: Add constructor logic here
        //
        public ProcedureController()
        {
        }
        public ProcedureInfor Infor;


        public ProcedureController(ProcedureInfor Infor)
        {
            this.Infor = Infor;
        }
        public ProcedureInfor _Infor
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
                SQLstring = "INSERT INTO L_Procedures(PARENT_ID,Procedure_Code,DISPLAY_NAME,[Desc],Pos,MODALITY_ID,STANDARD_NAME,PRICE,DirectionCapture,ISEmerency) VALUES(" + Infor .Parent_ID+ ",'"
                    + Infor.Procedure_Code + "','" + Infor.DISPLAY_NAME + "','" + Infor.Desc + "'," + Infor.Pos + "," + Infor.MODALITY_ID + ",'" + Infor.STANDARD_NAME + "'," + Infor .PRICE+ ","+Infor.DirectionCapture+ ","+Infor.IsEmerency+")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Procedure_ID =Convert.ToInt16( Utility.getCurrentMaxID("Procedure_ID", "L_Procedures"));
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
        /// Cập nhật đối tượng Procedure có trong bảng CSDL
        /// Đầu vào là đối tượng ProcedureInfor
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
                    SQLstring = "UPDATE  L_Procedures SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Procedures SET Parent_ID=" + Infor.Parent_ID + ",Procedure_Code='" + Infor.Procedure_Code + "',DISPLAY_NAME='" + Infor.DISPLAY_NAME + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos + ",MODALITY_ID=" + Infor.MODALITY_ID + ",STANDARD_NAME='" + Infor.STANDARD_NAME + "',PRICE=" + Infor.PRICE + ",IsEmerency=" + Infor.IsEmerency + ",DirectionCapture=" + Infor .DirectionCapture+ " WHERE Procedure_ID=" + Infor.Procedure_ID;
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
        /// Xóa đối tượng Procedure có trong CSDL dựa vào Infor.Procedure_Code
        /// Đầu vào là đối tượng ProcedureInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_Procedures WHERE Procedure_Code='" + Infor.Procedure_Code + "' AND Procedure_ID=" + Infor.Procedure_ID;
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
        public static ProcedureInfor GetInfor(DataRow dr)
        {
            ProcedureInfor Infor = new ProcedureInfor();
            if (dr != null)
            {

                Infor.Procedure_Code = Utility.sDbnull(dr["Procedure_Code"]);
                Infor.Procedure_ID = Utility.Int16Dbnull(dr["Procedure_ID"]);
                Infor.Parent_ID = Utility.Int16Dbnull(dr["Parent_ID"]);
                Infor.DISPLAY_NAME = Utility.sDbnull(dr["DISPLAY_NAME"]);
                Infor.STANDARD_NAME = Utility.sDbnull(dr["STANDARD_NAME"]);
                Infor.MODALITY_ID = Utility.Int16Dbnull(dr["MODALITY_ID"]);
                Infor.MODALITY_Name = Utility.sDbnull(dr["MODALITY_Name"]);
                Infor.PRICE = Utility.fDbnull(dr["PRICE"]);
                Infor.Pos = Utility.Int16Dbnull(dr["Pos"]);
                Infor.IsEmerency = Utility.Int16Dbnull(dr["IsEmerency"]);
                Infor.DirectionCapture = Convert.ToByte(dr["DirectionCapture"]);
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
        public static ProcedureInfor GetInfor(string ID)
        {
            DataSet ds = new ProcedureController().GetData("Procedure_Code='" + ID+"'");
            ProcedureInfor Infor = new ProcedureInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Procedure_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Procedure_Code"]);
                    Infor.Procedure_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Procedure_ID"]);
                    Infor.Parent_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Parent_ID"]);
                    Infor.DISPLAY_NAME = Utility.sDbnull(ds.Tables[0].Rows[0]["DISPLAY_NAME"]);
                    Infor.STANDARD_NAME = Utility.sDbnull(ds.Tables[0].Rows[0]["STANDARD_NAME"]);
                    Infor.MODALITY_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["MODALITY_ID"]);
                    Infor.MODALITY_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["MODALITY_Name"]);
                    Infor.PRICE = Utility.fDbnull(ds.Tables[0].Rows[0]["PRICE"]);
                    Infor.Pos = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Pos"]);
                    Infor.IsEmerency = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["IsEmerency"]);
                    Infor.Desc = Utility.sDbnull(ds.Tables[0].Rows[0]["Desc"]);
                    Infor.DirectionCapture = Convert.ToByte(ds.Tables[0].Rows[0]["DirectionCapture"]);
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
        public static ProcedureInfor GetInfor(Int16 ID)
        {
            DataSet ds = new ProcedureController().GetData("Procedure_ID=" + ID );
            ProcedureInfor Infor = new ProcedureInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Procedure_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Procedure_Code"]);
                    Infor.Procedure_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Procedure_ID"]);
                    Infor.Parent_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Parent_ID"]);
                    Infor.DISPLAY_NAME = Utility.sDbnull(ds.Tables[0].Rows[0]["DISPLAY_NAME"]);
                    Infor.STANDARD_NAME = Utility.sDbnull(ds.Tables[0].Rows[0]["STANDARD_NAME"]);
                    Infor.MODALITY_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["MODALITY_ID"]);
                    Infor.MODALITY_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["MODALITY_Name"]);
                    Infor.PRICE = Utility.fDbnull(ds.Tables[0].Rows[0]["PRICE"]);
                    Infor.Pos = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Pos"]);
                    Infor.IsEmerency = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["IsEmerency"]);
                    Infor.Desc = Utility.sDbnull(ds.Tables[0].Rows[0]["Desc"]);
                    Infor.DirectionCapture = Convert.ToByte(ds.Tables[0].Rows[0]["DirectionCapture"]);
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
        public  string GetCode(Int16 ID)
        {
            DataSet ds = new ProcedureController().GetData("Procedure_ID=" + ID);
            ProcedureInfor Infor = new ProcedureInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["Procedure_Code"].ToString();
                }
                else
                {
                    return "Undefined";
                }
            }
            else
            {
                return "Undefined";
            }
        }
        /// <summary>
        /// Tạo DataRow của Entity từ ObjectInfor
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="Infor">ObjectInfor</param>
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, ProcedureInfor Infor)
        {

            if (Infor != null)
            {
                dr["Procedure_ID"] =Infor.Procedure_ID;
                dr["Procedure_Code"] = Utility.sDbnull(Infor.Procedure_Code);
                dr["DISPLAY_NAME"] = Utility.sDbnull(Infor.DISPLAY_NAME);
                dr["STANDARD_NAME"] = Utility.sDbnull(Infor.STANDARD_NAME);
                dr["Parent_ID"] = Utility.Int16Dbnull(Infor.Parent_ID);
                dr["MODALITY_ID"] = Utility.Int16Dbnull(Infor.MODALITY_ID);
                dr["MODALITY_Name"] = Utility.sDbnull(Infor.MODALITY_Name);
                dr["PRICE"] = Utility.fDbnull(Infor.PRICE);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["DirectionCapture"] = Infor.DirectionCapture;
                dr["IsEmerency"] = Utility.Int16Dbnull(Infor.IsEmerency);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);
            }
        }
        /// <summary>
        /// Lấy dữ liệu Procedure dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *, 0 as OldPos,(Select Modality_Name from L_Modalities where modality_ID=p.modality_ID) as Modality_Name FROM L_Procedures P WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetEmerencyData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT P.*,0 as CHON FROM L_ANATOMY_PROJECTION P WHERE IS_USED_FOR_EMERENCY=1");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Procedure có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,0 as OldPos,(Select Modality_Name from L_Modalities where modality_ID=p.modality_ID) as Modality_Name FROM L_Procedures P ORDER BY Pos,DISPLAY_NAME");
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Procedures WHERE Procedure_Code='" + Infor.Procedure_Code + "'");
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
