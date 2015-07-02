using System;
namespace VietBaIT.DROC.Objects.ObjectInfor
{
    public class RegDetailInfor
    {
        #region "Attributes"
        public string StudyInstanceUID;
        public string SeriesInstanceUID;
        public string SOPInstanceUID;
        public Int64 REG_ID;
        public Int64 DETAIL_ID;
        public string ANATOMY_CODE;
        public string BODYSIZE_CODE;
        public string PROJECTION_CODE;
        public Int16 STATUS;
        public Int16 PRINTCOUNT;
        public Int16 EXPOSURECOUNT;
        public byte DirectionCapture;
        public string HOST;
        public string IMGNAME;
        public int UsingGrid;
        #endregion
        //Không cần dùng vùng này nữa cho đơn giản mà ta sẽ truy cập trực tiếp vào Attributes của Infor
        #region "Properties"

        #endregion
    }
}
