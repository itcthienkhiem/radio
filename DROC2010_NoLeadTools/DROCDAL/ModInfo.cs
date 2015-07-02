using System;
namespace VietBaIT.DROC.Objects.ObjectInfor
{
    public class ModalityInfor
    {
        #region "Attributes"
        public Int16 Modality_ID;
        public string Modality_Code;
        public string Modality_Name;
        public Int16 Pos;
        public Int16 OldPos;
        public byte STATUS;
        public Int16 PORT_NUM;
        public string Desc;
        public byte DEVICE_WORKLIST;
        public string RESTRICTION;
        public string AE_TITLE;
        public string IP_ADDRESS;

        public Int16 Manufacture_ID;
        public Int16 Room_ID;
        public Int16 Mod_Type_ID;
        public string Country_ID;

        public string Manufacture_Name;
        public string Room_Name;
        public string Mod_Type_Name;
        public string Country_Name;
        public int IMGW;
        public int IMGH;
        public int LOW;
        public int HIGH;
        public int AUTO_FLIPH;
        public int AUTO_FLIPV;
        public string Range;
        public int StartColor;
        public int EndColor;

        
        #endregion
        //Không cần dùng vùng này nữa cho đơn giản mà ta sẽ truy cập trực tiếp vào Attributes của Infor
        #region "Properties"

        #endregion
    }
    public class IECONFIGInfor
    {
       
        public Int32 ID;
        public string IE_NAME;
        public Int32 START_WIDTH;
        public Int32 START_CENTER;
        public Int32 END_WIDTH;
        public Int32 END_CENTER;
        public Int32 APPLY_GAMMA;
        public Int32 APPLY_CONTRAST;
        public Int32 APPLY_BRIGHTNESS;
        public Int32 APPLY_MSE;
        public Int32 GAMMA_VALUE;
        public Int32 CONTRAST_VALUE;
        public Int32 BRIGHTNESS_VALUE;
        public Int32 MSE_VALUE;
        public Int32 MED_VALUE;
        public Int32 MSE_APPLY_ELGE_EHANCEMENT;
        public Int32 MSE_APPLY_LATITUDE_REDUCTION;
        public Int32 MSE_EC;
        public Int32 MSE_EL;
        public Int32 MSE_LC;
        public Int32 MSE_LL;
        public Int32 MSE_TYPE;
        public Int32 MSE_ORDER;
        public Int32 CONTRAST_ORDER;
        public Int32 BRIGHTNESS_ORDER;
        public Int32 GAMMA_ORDER;
        public Int32 WOB;
        public Int32 INVERT_STT;
        public Int32 INVERT_AFTER;
        public Int32 APPLY_INVERT;
        public int AUTO_MIN_MAX_BIT = 0;
        public int APPLY_INVERT_FIRST = 0;
        public int HEC_AFTER = 0;
        public int HEC_STT = 0;
        public int MED_STT = 0;
        public int APPLY_HEC = 0;

        public int SE_AFTER = 0;
        public int SE_STT = 0;
        
        public int APPLY_SE = 0;

        public int APPLY_MED = 0;
        public Int32 APPLY_WL;
        public Int32 WL_WIDTH;
        public Int32 WL_CENTER;
        public Int32 WL_STT;

        public Int32 LOW;
        public Int32 HIGH;
        public Int32 WL_LOW;
        public Int32 WL_HIGH;

        public Int32 APPLY_MOTIONBLUR;
        public Int32 MB_DIMENSION;
        public Int32 MB_ANGLE;
        public Int32 MB_STT;

        public Int32 APPLY_ANTIALIAS;
        public Int32 ANTIALIAS_DIMENSION;
        public Int32 ANTIALIAS_THRESHOLD;
        public Int32 ANTIALIAS_FILTER;
        public Int32 ANTIALIAS_STT;


        public Int32 WL_WOB;
        public Int32 IS_ENABLE;


        public Int32 XRES;
        public Int32 YRES;
        public Int32 NBINS;
        public Int32 LLV;
        public Int32 HLV;


        public decimal SLOPE;
        public Int32 NEWAPI;
        public Int32 LUTTYPE;
    }
}
