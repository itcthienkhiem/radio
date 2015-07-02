using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class RegController
    {
        //
        // TODO: Add constructor logic here
        //
        public RegController()
        {
        }
        public RegInfor Infor;


        public RegController(RegInfor Infor)
        {
            this.Infor = Infor;
        }
        public RegInfor _Infor
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
                SQLstring = "INSERT INTO L_Regs(REG_NUMBER,PATIENT_ID,CREATED_DATE,PROCEDURELIST,PHYSICIAN,StudyInstanceUID) VALUES('" + Infor.REG_NUMBER + "'," + Infor.PATIENT_ID + ",CDate('" + Infor.CREATED_DATE.ToString("dd/MM/yyyy") + "'),'" + Infor.PROCEDURELIST + "','" + Infor.PHYSICIAN + "','"+Infor.StudyInstanceUID+"')";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.REG_ID =Convert.ToInt32( Utility.getCurrentMaxID("Reg_ID", "L_Regs"));
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
        /// Cập nhật đối tượng Reg có trong bảng CSDL
        /// Đầu vào là đối tượng RegInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            try
            {
                string SQLstring = null;
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Regs SET CREATED_DATE=CDate('" + Infor.CREATED_DATE.ToString("dd/MM/yyyy") + "')" + ",PROCEDURELIST='" + Infor.PROCEDURELIST + "',PHYSICIAN='" + Infor.PHYSICIAN + "' WHERE Reg_ID=" + Infor.REG_ID;
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
        public ActionResult UpdateDateAndPhysical()
        {
            try
            {
                string SQLstring = null;
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Regs SET CREATED_DATE=CDate('" + Infor.CREATED_DATE.ToString("dd/MM/yyyy") + "')" + ",PHYSICIAN='" + Infor.PHYSICIAN + "' WHERE Reg_ID=" + Infor.REG_ID;
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
        public ActionResult UpdateStatus(Int64 RegID,Int16 Status,DateTime Suspending_Time)
        {
            try
            {
                string SQLstring = null;
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Regs SET Status=" + Status + ",Suspending_Time=" + (Status == 0 ? "CDate('" + Suspending_Time.ToString("dd/MM/yyyy") + "')" : "null") + " WHERE Reg_ID=" + RegID;
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
        public ActionResult UpdateStatus(Int64 RegID, Int16 Status)
        {
            try
            {
                string SQLstring = null;
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Regs SET Status=" + Status + " WHERE Reg_ID=" + RegID+ " AND Status<>2";
                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                
                    return ActionResult.Success;
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }


        }
        /// <summary>
        /// Xóa đối tượng Reg có trong CSDL dựa vào Infor.Reg_Code
        /// Đầu vào là đối tượng RegInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_Regs WHERE Reg_Code='" + Infor.REG_NUMBER + "' AND Reg_ID=" + Infor.REG_ID;
                
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
        public ActionResult Delete(Int64 reg_ID)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_Regs WHERE Reg_ID=" + reg_ID;
               
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
        /// Kiểm tra sự tồn tại của một mã nước
        /// </summary>
        /// <returns>true nếu tồn tại. False nếu chưa tồn tại</returns>
        private bool Exists()
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Regs WHERE Reg_Code='" + Infor.REG_NUMBER + "'");
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
        public System.Collections.ArrayList GetDetailAgain(Int64 Reg_ID)
        {
            System.Collections.ArrayList arrProc = new System.Collections.ArrayList();
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_RegDetails WHERE Reg_ID=" + Reg_ID );
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        arrProc.Add("#"+dr["PROCEDURE_ID"].ToString() + "#TEMP");
                    }
                }
               return arrProc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
