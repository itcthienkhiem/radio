using System;
namespace VietBaIT.DROC.Objects.ObjectInfor
{
    public class ServerInfor
    {
        #region "Attributes"
        public int ID;
        public string IpAddress;
        public string LocalAddress;
        public Int16 LocalPort;
        public string FilmSize;
        public string ServerName;
        public string CalledAETitle;
        public string CallingAETitle;
        public Int16 Port;
        public Int16 TimeOut;
        public byte IsActive;
        public byte ServerType;
        #endregion
        //Không cần dùng vùng này nữa cho đơn giản mà ta sẽ truy cập trực tiếp vào Attributes của Infor
        #region "Properties"

        #endregion
    }
}
