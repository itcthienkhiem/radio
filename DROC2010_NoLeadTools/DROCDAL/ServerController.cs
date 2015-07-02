using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using OleDbDAL;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class ServerController
    {
        //
        // TODO: Add constructor logic here
        //
        public ServerController()
        {
        }
        public ServerInfor Infor;


        public ServerController(ServerInfor Infor)
        {
            this.Infor = Infor;
        }
        public ServerInfor _Infor
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
                SQLstring = "INSERT INTO L_ServerList(IpAddress,CalledAETitle,CallingAETitle,Port,Timeout,IsActive,ServerType,ServerName,FilmSize,LocalAddress,LocalPort) VALUES('" + Infor.IpAddress + "','" + Infor.CalledAETitle + "','" + Infor.CallingAETitle + "'," + Infor.Port + "," + Infor.TimeOut + "," + Infor.IsActive + "," + Infor.ServerType + ",'"+ Infor.ServerName+"','"+ Infor.FilmSize+"','"+Infor.LocalAddress+"',"+Infor.LocalPort+")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.ID = Convert.ToInt16(Utility.getCurrentMaxID("ID", "L_ServerList"));
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
        /// Cập nhật đối tượng Server có trong bảng CSDL
        /// Đầu vào là đối tượng ServerInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            try
            {
                string SQLstring = null;
                //if (Exists())
                //{
                //    return ActionResult.ExistedRecord;
                //}
                //Cập nhật Server đang được chọn
                SQLstring = "UPDATE L_ServerList SET IpAddress='" + Infor.IpAddress + "',CalledAETitle='" + Infor.CalledAETitle + "',CallingAETitle='" + Infor.CallingAETitle + "',Port=" + Infor.Port + ",TimeOut=" + Infor.TimeOut + ",IsActive=" + Infor.IsActive + ",ServerType=" + Infor.ServerType + ",ServerName='"+Infor.ServerName+"',FilmSize='"+Infor.FilmSize+"',LocalAddress='"+Infor.LocalAddress+"',LocalPort="+Infor.LocalPort+" WHERE ID=" + Infor.ID;
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
        /// Xóa đối tượng Server có trong CSDL dựa vào Infor.IpAddress
        /// Đầu vào là đối tượng ServerInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_ServerList WHERE ID=" + Infor.ID ;
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
        public static ServerInfor GetInfor(DataRow dr)
        {
            ServerInfor Infor = new ServerInfor();
            if (dr != null)
            {
                Infor.ID = Utility.Int32Dbnull(dr["ID"]);
                Infor.IpAddress = Utility.sDbnull(dr["IpAddress"]);
                Infor.LocalAddress = Utility.sDbnull(dr["LocalAddress"]);
                Infor.LocalPort = Utility.Int16Dbnull(dr["LocalPort"]);
                Infor.CalledAETitle = Utility.sDbnull(dr["CalledAETitle"]);
                Infor.Port = Utility.Int16Dbnull(dr["Port"]);
                Infor.TimeOut = Utility.Int16Dbnull(dr["TimeOut"]);
                Infor.IsActive = Convert.ToByte(dr["IsActive"]);
                Infor.ServerType = Convert.ToByte(dr["ServerType"]);

                Infor.CallingAETitle = Utility.sDbnull(dr["CallingAETitle"]);
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
        public static ServerInfor GetInfor(int ID)
        {
            DataSet ds = new ServerController().GetData("ID=" + ID);
            ServerInfor Infor = new ServerInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.ID = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["ID"]);
                    Infor.IpAddress = Utility.sDbnull(ds.Tables[0].Rows[0]["IpAddress"]);
                    Infor.CalledAETitle = Utility.sDbnull(ds.Tables[0].Rows[0]["CalledAETitle"]);
                    Infor.LocalAddress = Utility.sDbnull(ds.Tables[0].Rows[0]["LocalAddress"]);
                    Infor.LocalPort = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["LocalPort"]);
                    Infor.Port = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Port"]);
                    Infor.TimeOut = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["TimeOut"]);
                    Infor.IsActive = Convert.ToByte(ds.Tables[0].Rows[0]["IsActive"]);
                    Infor.ServerType = Convert.ToByte(ds.Tables[0].Rows[0]["ServerType"]);
                    Infor.CallingAETitle = Utility.sDbnull(ds.Tables[0].Rows[0]["CallingAETitle"]);

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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, ServerInfor Infor)
        {

            if (Infor != null)
            {
                dr["ID"] = Infor.ID;
                dr["IpAddress"] = Infor.IpAddress;
                dr["CalledAETitle"] = Infor.CalledAETitle;
                dr["LocalAddress"] = Infor.LocalAddress;
                dr["LocalPort"] = Infor.LocalPort;
                dr["Port"] = Infor.Port;
                dr["TimeOut"] = Infor.TimeOut;
                dr["IsActive"] = Infor.IsActive;
                dr["ServerType"] = Infor.ServerType;
                dr["sServerType"] = Infor.ServerType == 0 ? "PACS Server" : "Dicom Printer";
                dr["CallingAETitle"] = Infor.CallingAETitle;
            }
        }
        /// <summary>
        /// Lấy dữ liệu Server dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,IIF( ServerType= 0,'PACS Server','Dicom Printer') AS sServerType FROM L_ServerList WHERE " + Condition);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetDcmPrinterList()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,'' as DISPLAY_NAME FROM L_ServerList WHERE ServerType=1");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Server có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData()
        {
            try
            {
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT *,IIF( ServerType= 0,'PACS Server','Dicom Printer') AS sServerType  FROM L_ServerList ORDER BY Port,CalledAETitle");
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_ServerList WHERE IpAddress='" + Infor.IpAddress + "' AND CalledAETitle='"+Infor.CalledAETitle+"' AND Port="+Infor.Port);
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
