using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class PatientController
    {
        //
        // TODO: Add constructor logic here
        //
        public PatientController()
        {
        }
        public PatientInfor Infor;


        public PatientController(PatientInfor Infor)
        {
            this.Infor = Infor;
        }
        public PatientInfor _Infor
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
                SQLstring = "INSERT INTO L_Patients(Patient_Code,Patient_Name,BIRTH_DATE,SEX,EMERGENCY,CREATED_BY,Age,PATIENT_NAME_UNSIGNED,PATIENT_NAME_NOSPACE) VALUES('" + Infor.Patient_Code + "','" + Infor.Patient_Name + "',CDate('" + Infor.sBIRTH_DATE + "')," + Infor.Sex + "," + Infor.EMERGENCY + ",'" + Infor.CREATED_BY + "'," + Infor.Age + ",'"+Infor.PATIENT_NAME_UNSIGNED+"','"+Infor.PATIENT_NAME_NOSPACE+"')";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Patient_ID =Convert.ToInt16( Utility.getCurrentMaxID("Patient_ID", "L_Patients"));
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
        /// Cập nhật đối tượng Patient có trong bảng CSDL
        /// Đầu vào là đối tượng PatientInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            try
            {
                string SQLstring = null;
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Patients SET PATIENT_NAME_NOSPACE='" + Infor.PATIENT_NAME_NOSPACE + "',PATIENT_NAME_UNSIGNED='" + Infor.PATIENT_NAME_UNSIGNED + "',BIRTH_DATE=CDate('" + Infor.sBIRTH_DATE + "')" + ",Patient_Code='" + Infor.Patient_Code + "',Patient_Name='" + Infor.Patient_Name + "',Sex=" + Infor.Sex + ",EMERGENCY=" + Infor.EMERGENCY + ",Age=" + Infor.Age + " WHERE Patient_ID=" + Infor.Patient_ID;
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
        /// Xóa đối tượng Patient có trong CSDL dựa vào Infor.Patient_Code
        /// Đầu vào là đối tượng PatientInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_Patients WHERE Patient_Code='" + Infor.Patient_Code + "' AND Patient_ID=" + Infor.Patient_ID;
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
        public ActionResult Delete(Int64 Patient_ID)
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_Patients WHERE Patient_ID=" + Patient_ID;
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
        public static PatientInfor GetInfor(DataRow dr)
        {
            PatientInfor Infor = new PatientInfor();
            if (dr != null)
            {

                Infor.Patient_Code = Utility.sDbnull(dr["Patient_Code"]);
                Infor.Patient_ID = Utility.Int16Dbnull(dr["Patient_ID"]);
                Infor.Patient_Name = Utility.sDbnull(dr["Patient_Name"]);
                Infor.PATIENT_NAME_NOSPACE = Utility.sDbnull(dr["PATIENT_NAME_NOSPACE"]);
                Infor.PATIENT_NAME_UNSIGNED = Utility.sDbnull(dr["PATIENT_NAME_UNSIGNED"]);
                Infor.Sex = Utility.Int16Dbnull(dr["Sex"]);
                Infor.Age = Utility.Int32Dbnull(dr["Age"]);
                Infor.EMERGENCY = Utility.Int16Dbnull(dr["EMERGENCY"]);
                Infor.BIRTH_DATE =Convert.ToDateTime(dr["BIRTH_DATE"]);
                Infor.sBIRTH_DATE = Infor.BIRTH_DATE.ToString("dd/MM/yyyy");
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
        public static PatientInfor GetInfor(string ID)
        {
            DataSet ds = new PatientController().GetData("Patient_Code='" + ID+"'");
            PatientInfor Infor = new PatientInfor();
            if (ds != null)
            {
                if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Patient_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Patient_Code"]);
                    Infor.Patient_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Patient_ID"]);
                    Infor.Patient_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Patient_Name"]);
                    Infor.PATIENT_NAME_NOSPACE = Utility.sDbnull(ds.Tables[0].Rows[0]["PATIENT_NAME_NOSPACE"]);
                    Infor.PATIENT_NAME_UNSIGNED = Utility.sDbnull(ds.Tables[0].Rows[0]["PATIENT_NAME_UNSIGNED"]);
                    Infor.Sex = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Sex"]);
                    Infor.Age = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["Age"]);
                    Infor.EMERGENCY = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["EMERGENCY"]);
                    Infor.BIRTH_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["BIRTH_DATE"]);
                    Infor.sBIRTH_DATE = Infor.BIRTH_DATE.ToString("dd/MM/yyyy");
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
        public static PatientInfor GetInfor(Int16 ID)
        {
            DataSet ds = new PatientController().GetData("Patient_ID=" + ID );
            PatientInfor Infor = new PatientInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Patient_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Patient_Code"]);
                    Infor.Patient_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Patient_ID"]);
                    Infor.Patient_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Patient_Name"]);
                    Infor.PATIENT_NAME_NOSPACE = Utility.sDbnull(ds.Tables[0].Rows[0]["PATIENT_NAME_NOSPACE"]);
                    Infor.PATIENT_NAME_UNSIGNED = Utility.sDbnull(ds.Tables[0].Rows[0]["PATIENT_NAME_UNSIGNED"]);
                    Infor.Sex = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Sex"]);
                    Infor.Age = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["Age"]);
                    Infor.EMERGENCY = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["EMERGENCY"]);
                    Infor.BIRTH_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["BIRTH_DATE"]);
                    Infor.sBIRTH_DATE = Infor.BIRTH_DATE.ToString("dd/MM/yyyy");
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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, PatientInfor Infor)
        {

            if (Infor != null)
            {
                dr["Patient_ID"] =Infor.Patient_ID;
                dr["Patient_Code"] = Utility.sDbnull(Infor.Patient_Code);
                dr["Patient_Name"] = Utility.sDbnull(Infor.Patient_Name);
                dr["PATIENT_NAME_UNSIGNED"] = Utility.sDbnull(Infor.PATIENT_NAME_UNSIGNED);
                dr["PATIENT_NAME_NOSPACE"] = Utility.sDbnull(Infor.PATIENT_NAME_NOSPACE);
                dr["Sex"] = Utility.Int16Dbnull(Infor.Sex);
                dr["Age"] = Utility.Int32Dbnull(Infor.Age);
                dr["EMERGENCY"] = Utility.sDbnull(Infor.EMERGENCY);
                dr["BIRTH_DATE"] = Infor.BIRTH_DATE;
                dr["sBIRTH_DATE"] = Infor.BIRTH_DATE.ToString("dd/MM/yyyy");
            }
        }
        /// <summary>
        /// Lấy dữ liệu Patient dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *  FROM L_Patients WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Patient có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Patients ORDER BY Pos,Patient_Name");
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Patients WHERE Patient_Code='" + Infor.Patient_Code + "'");
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
        public bool Exists(string PCode,string pName)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Patients WHERE ucase(Patient_Code)='" + PCode.ToUpper() + "' ");//and PATIENT_NAME_NOSPACE='" + pName.ToUpper() + "'");
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
    }
}
