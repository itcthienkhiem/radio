using System;
namespace VietBaIT.DROC.Objects.ObjectInfor
{
    public class RegInfor
    {
        #region "Attributes"
        public string StudyInstanceUID;
        public Int64 REG_ID;
        public string REG_NUMBER;
        public Int64 PATIENT_ID;
        public string PROCEDURELIST;
        public string PHYSICIAN;
        public Int16 Status;
        public DateTime CREATED_DATE;
        public string DESC;
        #endregion
        //Không cần dùng vùng này nữa cho đơn giản mà ta sẽ truy cập trực tiếp vào Attributes của Infor
        #region "Properties"

        #endregion
    }
}
