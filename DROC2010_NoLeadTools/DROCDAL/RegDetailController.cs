using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using System.Collections;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class RegDetailController
    {
        //
        // TODO: Add constructor logic here
        //
        public RegDetailController()
        {
        }
        public RegDetailInfor Infor;


        public RegDetailController(RegDetailInfor Infor)
        {
            this.Infor = Infor;
        }
        public RegDetailInfor _Infor
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

                SQLstring = "INSERT INTO L_RegDetails(REG_ID,ANATOMY_CODE,PROJECTION_CODE,BODYSIZE_CODE,STATUS,IMGNAME,PRINTCOUNT,EXPOSURECOUNT,HOST,DIRECTIONCAPTURE,StudyInstanceUID,SeriesInstanceUID,SOPInstanceUID,UsingGrid) VALUES(" + Infor.REG_ID + ",'" + Infor.ANATOMY_CODE + "','" + Infor.PROJECTION_CODE + "','" + Infor.BODYSIZE_CODE + "',0,'',0,0,'127.0.0.1'," + Infor.DirectionCapture + ",'" + Infor.StudyInstanceUID + "','" + Infor.SeriesInstanceUID + "','" + Infor.SOPInstanceUID + "',"+Infor.UsingGrid+")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.DETAIL_ID = Convert.ToInt32(Utility.getCurrentMaxID("DETAIL_ID", "L_RegDetails"));
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
        public ActionResult InsertPrintLog(Hashtable htbDetail, string FILMSIZE_ID, string PRINTER_NAME, string IPADDRESS, string PRINT_TIME, string PRINT_BY, bool RePrint)
        {
            string SQLstring = "";
            try
            {
                if (RePrint)
                {
                    foreach (int _detail_ID in htbDetail.Keys)
                    {
                        DeletePrintLogByDetailID(_detail_ID);
                    }
                }
                long PRINT_ID = -1;
                SQLstring = "INSERT INTO T_PRINT_LOG(FILMSIZE_ID,PRINTER_NAME,IPADDRESS,PRINT_TIME,PRINT_BY) VALUES('" + FILMSIZE_ID + "','" + PRINTER_NAME + "','" + IPADDRESS + "',CDate('" + PRINT_TIME + "'),'" + PRINT_BY + "')";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    PRINT_ID = Convert.ToInt32(Utility.getCurrentMaxID("PRINT_ID", "T_PRINT_LOG"));
                    foreach (int _detail_ID in htbDetail.Keys)
                    {
                        InsertPrintLogDetail(PRINT_ID, _detail_ID);
                    }
                    return ActionResult.Success;
                }
                else
                {
                    return ActionResult.Error;
                }
            }
            catch (Exception ex)
            {
                // Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        public void DeletePrintLogByDetailID(int Detail_ID)
        {
            try
            {
                int PrintID = Utility.GetValueOfField("PRINT_ID", "T_PRINT_LOG_DETAIL", " DEtail_ID=" + Detail_ID.ToString());
                if (PrintID != -1)
                {
                    //Xóa Prindetail
                    Utility.DeleteValueFromTable("T_PRINT_LOG_DETAIL", "PRINT_ID=" + PrintID.ToString());
                    Utility.DeleteValueFromTable("T_PRINT_LOG", "PRINT_ID=" + PrintID.ToString());
                }
            }
            catch
            {
            }
        }
        public void InsertPrintLogDetail(long PRINT_ID, int Detail_ID)
        {
            try
            {
                if (HasPrintLog(PRINT_ID, Detail_ID)) return;
                string SQLstring = "INSERT INTO T_PRINT_LOG_DETAIL(PRINT_ID,Detail_ID) VALUES(" + PRINT_ID + "," + Detail_ID + ")";
                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);

            }
            catch
            {
            }
        }
        public bool HasPrintLog(long PRINT_ID, int Detail_ID)
        {
            try
            {
                DataSet ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT 1 FROM T_PRINT_LOG_DETAIL P WHERE PRINT_ID=" + PRINT_ID + " AND Detail_ID=" + Detail_ID);
                return ds.Tables[0].Rows.Count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ActionResult DeleteDetail(Int64 Detail_ID)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_RegDetails WHERE Detail_ID=" + Detail_ID;
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
        public ActionResult UpdateStatusAndImg(Int64 Detail_ID,Int16 Status,string ImgName,int UsingGrid,decimal kVp,decimal mAs)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "Update  L_RegDetails set Status =" + Status + ",ImgName='" + ImgName + "',UsingGrid=" + UsingGrid + ",kVp="+kVp.ToString()+",mAs="+mAs.ToString()+" WHERE Detail_ID=" + Detail_ID;
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
        public ActionResult UpdateStatusAndImg(Int64 Detail_ID, Int16 Status, string ImgName, int UsingGrid)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "Update  L_RegDetails set Status =" + Status + ",ImgName='" + ImgName + "',UsingGrid=" + UsingGrid + " WHERE Detail_ID=" + Detail_ID;
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
        public ActionResult RejectImage(Int64 Detail_ID)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "Update  L_RegDetails set Status =0,ImgName='',UsingGrid=0,kVp=0,mAs=0 WHERE Detail_ID=" + Detail_ID;
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
        public ActionResult UpdateCountFields(Int64 Detail_ID,string FieldName, int _AddValue)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "Update  L_RegDetails set " + FieldName + " =" + FieldName + " + " + _AddValue + " WHERE Detail_ID=" + Detail_ID;
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
        public object GetValueOfField(Int64 Detail_ID, string FieldName)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "select "+FieldName+" from  L_RegDetails WHERE Detail_ID=" + Detail_ID;
                DataSet ds= DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                    return 0;
                else
                    return ds.Tables[0].Rows[0][0];
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public ActionResult UpdateProcedureList(Int64 Reg_ID, string sValue)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "Update  L_Regs set ProcedureList ='" + sValue + "' WHERE Reg_ID=" + Reg_ID;
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
        public ActionResult UpdateStringField(Int64 Detail_ID,string fieldName,string FieldValue)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "Update  L_RegDetails set " + fieldName + " ='" + FieldValue + "' WHERE Detail_ID=" + Detail_ID;
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
        
        public DataSet GetAllData(Int64 reg_ID)
        {
            try
            {
                DataSet ds = new DataSet();
                //return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,(Select top 1 Reg_number from L_Regs where reg_id=p.reg_id) as Reg_Number,(Select TOP 1 Display_Name from L_Procedures where Procedure_ID= P.Procedure_ID) as Display_Name,(Select TOP 1 Standard_Name from L_Procedures where Procedure_ID= P.Procedure_ID) as Standard_Name,(case status when 1 then N'Image' else N'' end) as Status_name FROM L_RegDetails P WHERE REG_ID=" + reg_ID);
                string Sql = "SELECT P.*,(Select top 1 VN_ANATOMY_NAME from L_ANATOMY where CODE=p.ANATOMY_CODE) as VN_ANATOMY_NAME,(Select top 1 EN_ANATOMY_NAME from L_ANATOMY where CODE=p.ANATOMY_CODE) as EN_ANATOMY_NAME,(Select top 1 STT from L_ANATOMY where CODE=p.ANATOMY_CODE) as A_STT,(Select top 1 VN_PROJECTION_NAME from L_PROJECTION where CODE=p.PROJECTION_CODE) as VN_PROJECTION_NAME,(Select top 1 EN_PROJECTION_NAME from L_PROJECTION where CODE=p.PROJECTION_CODE) as EN_PROJECTION_NAME,(Select top 1 VN_BODYSIZE_NAME from L_BODYSIZE where BODYSIZE_CODE=p.BODYSIZE_CODE) as VN_BODYSIZE_NAME,(Select top 1 EN_BODYSIZE_NAME from L_BODYSIZE where BODYSIZE_CODE=p.BODYSIZE_CODE) as EN_BODYSIZE_NAME,(Select top 1 STT from L_PROJECTION where CODE=p.PROJECTION_CODE) as P_STT,(Select top 1 Reg_number from L_Regs where reg_id=p.reg_id) as Reg_Number,ANATOMY_CODE as Display_Name,ANATOMY_CODE as Standard_Name,IIF(status = 1 ,'Image','' ) as Status_name FROM L_RegDetails P WHERE REG_ID=" + reg_ID;
                ds= DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, Sql);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool HasResult(Int64 reg_ID)
        {
            try
            {
                DataSet ds= DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT 1 FROM L_RegDetails P WHERE STATUS=1 AND REG_ID=" + reg_ID);
                if (ds.Tables[0].Rows.Count > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CanDelete()
        {
            return true;
        }
        public ActionResult Delete(Int64 reg_ID)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_RegDetails WHERE REG_ID=" + reg_ID + " AND status=0";
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
        /// Xóa đối tượng RegDetail có trong CSDL dựa vào Infor.RegDetail_Code
        /// Đầu vào là đối tượng RegDetailInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_RegDetails WHERE REG_ID=" + Infor.REG_ID;
                
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
