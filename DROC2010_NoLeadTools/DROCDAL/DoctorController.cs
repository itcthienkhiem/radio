using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class DoctorController
    {
        //
        // TODO: Add constructor logic here
        //
        public DoctorController()
        {
        }
        public DoctorInfor Infor;


        public DoctorController(DoctorInfor Infor)
        {
            this.Infor = Infor;
        }
        public DoctorInfor _Infor
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
                SQLstring = "INSERT INTO L_DoctorList(Doctor_Code,Doctor_Name,[Desc],Pos,IsEmerency) VALUES('" + Infor.Doctor_Code + "','" + Infor.Doctor_Name + "','" + Infor.Desc + "'," + Infor.Pos + ","+Infor.IsEmerency+")";
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
        public ActionResult InsertAP(string ACode, string PCode, string A_VnName, string A_EnName, string P_VnName, string P_EnName, int Device_ID, int isUsed4Emerency)
        {
            string SQLstring = "";
            try
            {
                if (ExistsAP( ACode,  PCode,Device_ID))
                {
                    return ActionResult.ExistedRecord;
                }
                SQLstring = "INSERT INTO L_ANATOMY_PROJECTION (ANATOMY_CODE,PROJECTION_CODE,DEVICE_ID,VN_ANATOMY_NAME,VN_PROJECTION_NAME,EN_ANATOMY_NAME,EN_PROJECTION_NAME,IS_USED_FOR_EMERENCY,AUTO_FLIPV,AUTO_FLIPH)";
                SQLstring += " VALUES('" + ACode + "','" + PCode + "'," + Device_ID + ",'" + A_VnName + "','" + P_VnName + "','" + A_EnName+ "','" + P_EnName + "',"+ isUsed4Emerency +  ",0,0)";
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
        public ActionResult DeleteAP(string ACode, string PCode,  int Device_ID)
        {
            string SQLstring = "";
            try
            {
                
                SQLstring = "DELETE FROM L_ANATOMY_PROJECTION  WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString();
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
        public ActionResult DeleteAnatomyOfDevice(string ACode,  int Device_ID)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "DELETE FROM L_ANATOMY_PROJECTION  WHERE ANATOMY_CODE='" + ACode + "' AND DEVICE_ID=" + Device_ID.ToString();
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
        public ActionResult InsertAPParam(string ACode, string PCode, string BCODE,decimal Kvp,int MA,int MAS,int FILM_HD,string RAD_CODE,Int32 Device_ID,int AutoFlipV,int AutoFlipH, ref string  errMsg)
        {
            string SQLstring = "";
            errMsg = "";
            try
            {
                if (ExistsAPParams(ACode, PCode, BCODE))
                    SQLstring = "UPDATE L_AP_PARAMS SET FILM_HD=" + FILM_HD + ",KVP=" + Kvp + ",MA=" + MA + ",MAS=" + MAS + "  WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND BODYSIZE_CODE='" + BCODE + "'";
                else
                    SQLstring = "INSERT INTO L_AP_PARAMS(ANATOMY_CODE,PROJECTION_CODE,BODYSIZE_CODE,KVP,MA,MAS,FILM_HD) VALUES ('" + ACode + "','" + PCode + "','" + BCODE + "'," + Kvp + "," + MA + "," + MAS + ","+FILM_HD+")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    if (RAD_CODE.Trim() != "")
                    {
                        SQLstring = "Select * from  L_ANATOMY_PROJECTION  WHERE RAD_CODE='" + RAD_CODE + "' AND (( ANATOMY_CODE<>'" + ACode + "' AND PROJECTION_CODE<>'" + PCode + "') OR  (ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE<>'" + PCode + "')  OR  (ANATOMY_CODE<>'" + ACode + "' AND PROJECTION_CODE='" + PCode + "')) ";
                        DataTable dt = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SQLstring).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            errMsg = "Rad Code có giá trị :" + RAD_CODE + " đã được dùng cho vị trí chụp " + dt.Rows[0]["ANATOMY_CODE"].ToString() + " và hướng chụp " + dt.Rows[0]["PROJECTION_CODE"].ToString() + "\nĐề nghị bạn nhập mã RAD_Code khác";
                            return ActionResult.ExistedRecord;
                        }
                    }
                    SQLstring = "UPDATE L_ANATOMY_PROJECTION set AUTO_FLIPV="+AutoFlipV+",AUTO_FLIPH="+AutoFlipH+", RAD_CODE='" + RAD_CODE + "' WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString();
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
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
        public ActionResult InsertAPParam_Acq(string ACode, string PCode, string BCODE, decimal Kvp, int MA, int MAS, string RAD_CODE, Int32 Device_ID, int AutoFlipV, int AutoFlipH, ref string errMsg)
        {
            string SQLstring = "";
            errMsg = "";
            try
            {
                if (ExistsAPParams(ACode, PCode, BCODE))
                    SQLstring = "UPDATE L_AP_PARAMS SET KVP=" + Kvp + ",MAS=" + MAS + "  WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND BODYSIZE_CODE='" + BCODE + "'";
                else
                    SQLstring = "INSERT INTO L_AP_PARAMS(ANATOMY_CODE,PROJECTION_CODE,BODYSIZE_CODE,KVP,MA,MAS) VALUES ('" + ACode + "','" + PCode + "','" + BCODE + "'," + Kvp + "," + MA + "," + MAS + ")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    //if (RAD_CODE.Trim() != "")
                    //{
                    //    SQLstring = "Select * from  L_ANATOMY_PROJECTION  WHERE RAD_CODE='" + RAD_CODE + "' AND (( ANATOMY_CODE<>'" + ACode + "' AND PROJECTION_CODE<>'" + PCode + "') OR  (ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE<>'" + PCode + "')  OR  (ANATOMY_CODE<>'" + ACode + "' AND PROJECTION_CODE='" + PCode + "')) ";
                    //    DataTable dt = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SQLstring).Tables[0];
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        errMsg = "Rad Code có giá trị :" + RAD_CODE + " đã được dùng cho vị trí chụp " + dt.Rows[0]["ANATOMY_CODE"].ToString() + " và hướng chụp " + dt.Rows[0]["PROJECTION_CODE"].ToString() + "\nĐề nghị bạn nhập mã RAD_Code khác";
                    //        return ActionResult.ExistedRecord;
                    //    }
                    //}
                    //SQLstring = "UPDATE L_ANATOMY_PROJECTION set AUTO_FLIPV=" + AutoFlipV + ",AUTO_FLIPH=" + AutoFlipH + ", RAD_CODE='" + RAD_CODE + "' WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString();
                    //DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
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
        private bool ExistsAPParams(string ACode, string PCode, string BCODE)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_AP_PARAMS WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='"+PCode+"' AND BODYSIZE_CODE='"+BCODE+"'");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        public ActionResult DeleteDetail( int Detail_ID)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "DELETE FROM L_RegDetails  WHERE Detail_ID=" + Detail_ID;
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
        /// Cập nhật đối tượng Doctor có trong bảng CSDL
        /// Đầu vào là đối tượng DoctorInfor
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
                    SQLstring = "UPDATE  L_DoctorList SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_DoctorList SET Doctor_Name='" + Infor.Doctor_Name + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos + ",IsEmerency="+Infor.IsEmerency+" WHERE Doctor_Code='" + Infor.Doctor_Code + "'";
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
        /// Xóa đối tượng Doctor có trong CSDL dựa vào Infor.Doctor_Code
        /// Đầu vào là đối tượng DoctorInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_DoctorList WHERE Doctor_Code='" + Infor.Doctor_Code + "'";
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
        public ActionResult SetState(int ID,int IsEnabled)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "UPDATE TBL_IECONFIG SET IS_ENABLE =" + IsEnabled + " WHERE ID=" + ID;
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
        public static DoctorInfor GetInfor(DataRow dr)
        {
            DoctorInfor Infor = new DoctorInfor();
            if (dr != null)
            {

                Infor.Doctor_Code = Utility.sDbnull(dr["Doctor_Code"]);
                Infor.Doctor_Name = Utility.sDbnull(dr["Doctor_Name"]);
                Infor.Pos = Utility.Int16Dbnull(dr["Pos"]);
                Infor.IsEmerency = Utility.Int16Dbnull(dr["IsEmerency"]);
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
        public static DoctorInfor GetInfor(string ID)
        {
            DataSet ds = new DoctorController().GetData("Doctor_Code='" + ID+"'");
            DoctorInfor Infor = new DoctorInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Doctor_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Doctor_Code"]);
                    Infor.Doctor_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Doctor_Name"]);
                    Infor.Pos = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Pos"]);
                    Infor.IsEmerency = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["IsEmerency"]);
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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, DoctorInfor Infor)
        {

            if (Infor != null)
            {

                dr["Doctor_Code"] = Utility.sDbnull(Infor.Doctor_Code);
                dr["Doctor_Name"] = Utility.sDbnull(Infor.Doctor_Name);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["IsEmerency"] = Utility.Int16Dbnull(Infor.IsEmerency);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);
            }
        }
        public DataSet GetEmerencyData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_DoctorList WHERE IsEmerency=1 " );
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy dữ liệu Doctor dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *, 0 as OldPos FROM L_DoctorList WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetAPParams(string ACode, string PCode, string BCODE)
        {
           
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_AP_PARAMS WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND BODYSIZE_CODE='" + BCODE + "'");
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Doctor có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,0 as OldPos FROM L_DoctorList ORDER BY Pos,Doctor_Name");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region Anatomy
        private bool ExistsAnatomy(string ACode)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY WHERE CODE='" + ACode + "'");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        public ActionResult AnatomyHasUsed(string ACode)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_REGDETAILS WHERE ANATOMY_CODE='" + ACode + "'");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                   
                    return ActionResult.DataHasUsedinAnotherTable;
                }
                else
                {
                    ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY_PROJECTION WHERE ANATOMY_CODE='" + ACode + "'");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ActionResult.DataHasUsedinAnotherTable;
                    }
                    else

                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }
        public ActionResult InsertAnatomy(string ACode,  string A_VnName, string A_EnName, int STT)
        {
            string SQLstring = "";
            try
            {
                if (ExistsAnatomy(ACode))
                {
                    return ActionResult.ExistedRecord;
                }
                SQLstring = "INSERT INTO L_ANATOMY (CODE,VN_ANATOMY_NAME,EN_ANATOMY_NAME,STT)";
                SQLstring += " VALUES('" + ACode + "','" + A_VnName + "','" + A_EnName + "'," + STT + ")";
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
        public ActionResult UpdateAnatomy(string ACode, string A_VnName, string A_EnName, int STT)
        {
            string SQLstring = "";
            try
            {
               
                SQLstring = "UPDATE  L_ANATOMY ";
                SQLstring += "Set VN_ANATOMY_NAME='" + A_VnName + "',EN_ANATOMY_NAME='" + A_EnName + "',STT=" + STT + " WHERE CODE='" + ACode + "'";
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
        public ActionResult MakeAPAsEmerencyOrNot(string ACode, string PCODE,int Device_ID,Int16 Status)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "UPDATE  L_ANATOMY_PROJECTION ";
                SQLstring += "Set IS_USED_FOR_EMERENCY=" + Status + " WHERE ANATOMY_CODE='" + ACode + "' AND  PROJECTION_CODE='" + PCODE + "' AND  DEVICE_ID=" + Device_ID ;
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
        public ActionResult DeleteAnatomy(string ACode)
        {
            string SQLstring = "";
            try
            {
             
                SQLstring = "DELETE FROM L_ANATOMY  WHERE CODE='" + ACode + "'";
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
        public DataSet GetAnatomy()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY ORDER BY STT");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
      
        public DataSet GetAnatomyProjection(int Device_ID)
        {
            try
            {
                string sqlSelect = "";
                if(Device_ID!=-1)
                    sqlSelect = "SELECT *,0 as CHON  FROM L_ANATOMY_PROJECTION WHERE DEVICE_ID= " + Device_ID.ToString() + "  ORDER BY A_STT,ANATOMY_CODE,P_STT,PROJECTION_CODE";
                else
                    sqlSelect = "SELECT *,0 as CHON  FROM L_ANATOMY_PROJECTION ORDER BY A_STT,ANATOMY_CODE,P_STT,PROJECTION_CODE";
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, sqlSelect);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public DataSet GetAnatomyProjectionParams()
        {
            try
            {
                string sqlSelect = "";

                sqlSelect = "SELECT P.*,(SELECT TOP 1 VN_BODYSIZE_NAME FROM L_BODYSIZE WHERE BODYSIZE_CODE=P.BODYSIZE_CODE) AS VN_BODYSIZE_NAME,(SELECT TOP 1 EN_BODYSIZE_NAME FROM L_BODYSIZE WHERE BODYSIZE_CODE=P.BODYSIZE_CODE) AS EN_BODYSIZE_NAME FROM L_AP_PARAMS P ";
               
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, sqlSelect);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region Projection
        public DataSet GetProjection()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_PROJECTION ORDER BY STT");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private bool ExistsProjection(string ACode)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Projection WHERE CODE='" + ACode + "'");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        public ActionResult ProjectionHasUsed(string ACode)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_REGDETAILS WHERE Projection_CODE='" + ACode + "'");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return ActionResult.DataHasUsedinAnotherTable;
                }
                else
                {
                    ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY_PROJECTION WHERE Projection_CODE='" + ACode + "'");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ActionResult.DataHasUsedinAnotherTable;
                    }
                    else

                        return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }
        public ActionResult InsertProjection(string ACode, string A_VnName, string A_EnName, int STT)
        {
            string SQLstring = "";
            try
            {
                if (ExistsProjection(ACode))
                {
                    return ActionResult.ExistedRecord;
                }
                SQLstring = "INSERT INTO L_Projection (CODE,VN_Projection_NAME,EN_Projection_NAME,STT)";
                SQLstring += " VALUES('" + ACode + "','" + A_VnName + "','" + A_EnName + "'," + STT + ")";
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
        public ActionResult UpdateProjection(string ACode, string A_VnName, string A_EnName, int STT)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "UPDATE  L_Projection ";
                SQLstring += "Set VN_Projection_NAME='" + A_VnName + "',EN_Projection_NAME='" + A_EnName + "',STT=" + STT + " WHERE CODE='" + ACode + "'";
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
        public ActionResult DeleteProjection(string ACode)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "DELETE  FROM L_Projection  WHERE CODE='" + ACode + "'";
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
       
        #endregion
       
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_DoctorList WHERE Doctor_Code='" + Infor.Doctor_Code + "'");
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
        private bool ExistsAP(string ACode, string PCode,  int Device_ID)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY_PROJECTION WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
        public void GetAutoFlip(string ACode, string PCode, int Device_ID,ref int FLIPV,ref int FLIPH,ref int Large_Focus)
        {
            DataSet ds = null;
            FLIPV = 0;
            FLIPH = 0;
            try
            {
                if (Device_ID != -1)
                    ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY_PROJECTION WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString());
                else
                    ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ANATOMY_PROJECTION WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "'");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    FLIPV =Utility.Int32Dbnull( ds.Tables[0].Rows[0]["AUTO_FLIPV"],0);
                    FLIPH = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["AUTO_FLIPH"], 0);
                    Large_Focus = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["LARGE_FOCUS"], 0);
                }
                
            }
            catch (Exception ex)
            {
               
            }
        }
        public ActionResult UpdateLSFVal(string ACode, string PCode, int Device_ID, int Large_Focus)
        {
            string SQLstring = "";
            try
            {
                if (Device_ID != -1)
                {
                    SQLstring = "UPDATE  L_ANATOMY_PROJECTION ";
                    SQLstring += "Set Large_Focus=" + Large_Focus + " WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "' AND DEVICE_ID=" + Device_ID.ToString();
                }
                else
                {
                    SQLstring = "UPDATE  L_ANATOMY_PROJECTION ";
                    SQLstring += "Set Large_Focus=" + Large_Focus + " WHERE ANATOMY_CODE='" + ACode + "' AND PROJECTION_CODE='" + PCode + "'";
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
    }
}
