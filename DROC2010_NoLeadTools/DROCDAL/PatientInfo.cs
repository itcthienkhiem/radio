using System;
namespace VietBaIT.DROC.Objects.ObjectInfor
{
    public class PatientInfor
    {
        #region "Attributes"
        public Int64 Patient_ID;
        public string Patient_Code;
        public string Patient_Name;
        public string PATIENT_NAME_UNSIGNED;
        public string PATIENT_NAME_NOSPACE;
        public DateTime BIRTH_DATE;
        public string sBIRTH_DATE;
        public string CREATED_BY;
        public Int16 Sex;
        public Int32 Age;
        public Int16 EMERGENCY;
        #endregion
        //Không cần dùng vùng này nữa cho đơn giản mà ta sẽ truy cập trực tiếp vào Attributes của Infor
        #region "Properties"

        #endregion
    }
}
