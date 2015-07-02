using System;
namespace VietBaIT.DROC.Objects.ObjectInfor
{
    public class ProcedureInfor
    {
        #region "Attributes"
        public Int16 Procedure_ID;
        public Int16 Parent_ID;
        public string Procedure_Code;
        public string DISPLAY_NAME;
        public string STANDARD_NAME;
        public Int16 Pos;
        public Int16 MODALITY_ID;
        public string MODALITY_Name;
        public Int16 OldPos;
        public double PRICE;
        public string Desc;
        public Int16 IsEmerency;
        public byte DirectionCapture;
        #endregion
        //Không cần dùng vùng này nữa cho đơn giản mà ta sẽ truy cập trực tiếp vào Attributes của Infor
        #region "Properties"

        #endregion
    }
}
