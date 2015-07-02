using System;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectInfor;
using System.Collections;
using System.Collections.Generic;
using OleDbDAL;
using System.IO;
namespace VietBaIT.DROC.Objects.ObjectController
{
    public class ModalityController
    {
        //
        // TODO: Add constructor logic here
        //
        public ModalityController()
        {
        }
        public ModalityInfor Infor;


        public ModalityController(ModalityInfor Infor)
        {
            this.Infor = Infor;
        }
        public ModalityInfor _Infor
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
                SQLstring = "INSERT INTO L_Modalities(Modality_Code,Modality_Name,[Desc],Pos,IP_ADDRESS,AE_TITLE,RESTRICTION,DEVICE_WORKLIST,PORT_NUM,STATUS,ROOM_ID,MOD_TYPE_ID,COUNTRY_ID,MANUFACTURE_ID,IMGW,IMGH,AUTO_FLIPH,AUTO_FLIPV) VALUES('"
                            + Infor.Modality_Code + "','" + Infor.Modality_Name + "','" + Infor.Desc + "'," + Infor.Pos + ",'" + Infor.IP_ADDRESS + "','" + Infor.AE_TITLE + "','" + Infor.RESTRICTION + "'," +Infor.DEVICE_WORKLIST+","+ Infor.PORT_NUM + "," + Infor.STATUS + "," + Infor.Room_ID+
                            "," + Infor.Mod_Type_ID + ",'" + Infor.Country_ID + "'," + Infor .Manufacture_ID+","+Infor.IMGW+","+Infor.IMGH+ ","+Infor.AUTO_FLIPH+","+Infor.AUTO_FLIPV+")";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    Infor.Modality_ID =Convert.ToInt16( Utility.getCurrentMaxID("Modality_ID", "L_Modalities"));
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
        bool isUseNewAPI()
        {
            return File.Exists("isnewapi.api");
        }
        public ActionResult InsertIE(IECONFIGInfor infor)
        {
            string SQLstring = "";
            try
            {
                if (ExistsIEOnInsert(infor))
                {
                    return ActionResult.ExistedRecord;
                }
                if (!isUseNewAPI())
                {
                    SQLstring = "INSERT INTO TBL_IECONFIG (IE_NAME, START_WIDTH ,START_CENTER,END_WIDTH,END_CENTER";
                    SQLstring += ",APPLY_GAMMA ,APPLY_CONTRAST,APPLY_BRIGHTNESS,APPLY_MSE,GAMMA_VALUE,CONTRAST_VALUE";
                    SQLstring += ",BRIGHTNESS_VALUE,MSE_VALUE,MSE_APPLY_ELGE_EHANCEMENT,MSE_APPLY_LATITUDE_REDUCTION";
                    SQLstring += ",MSE_EC,MSE_EL,MSE_LC,MSE_LL,MSE_TYPE ,MSE_ORDER,CONTRAST_ORDER,BRIGHTNESS_ORDER,GAMMA_ORDER,WOB,INVERT_STT,INVERT_AFTER,APPLY_INVERT,APPLY_WL,WL_WIDTH,WL_CENTER,WL_STT,WL_WOB,LOW,HIGH,WL_LOW,WL_HIGH,APPLY_INVERT_FIRST,AUTO_MIN_MAX_BIT,APPLY_HEC,HEC_STT,APPLY_MED,MED_STT,MED_VALUE,IS_ENABLE,APPLY_MOTIONBLUR,MB_DIMENSION,MB_ANGLE,MB_STT,APPLY_ANTIALIAS,ANTIALIAS_DIMENSION,ANTIALIAS_THRESHOLD,ANTIALIAS_FILTER,ANTIALIAS_STT,APPLY_SE,SE_STT)";

                    SQLstring += " VALUES('" + infor.IE_NAME + "'," + infor.START_WIDTH + "," + infor.START_CENTER + "," + infor.END_WIDTH + "," + infor.END_CENTER + "," + infor.APPLY_GAMMA + ",";
                    SQLstring += infor.APPLY_CONTRAST + "," + infor.APPLY_BRIGHTNESS + "," + infor.APPLY_MSE + "," + infor.GAMMA_VALUE + "," + infor.CONTRAST_VALUE + "," + infor.BRIGHTNESS_VALUE + ",";
                    SQLstring += infor.MSE_VALUE + "," + infor.MSE_APPLY_ELGE_EHANCEMENT + "," + infor.MSE_APPLY_LATITUDE_REDUCTION + "," + infor.MSE_EC + "," + infor.MSE_EL + ",";
                    SQLstring += infor.MSE_LC + "," + infor.MSE_LL + "," + infor.MSE_TYPE + "," + infor.MSE_ORDER + "," + infor.CONTRAST_ORDER + "," + infor.BRIGHTNESS_ORDER + ",";
                    SQLstring += infor.GAMMA_ORDER + "," + infor.WOB + "," + infor.INVERT_STT + "," + infor.INVERT_AFTER + "," + infor.APPLY_INVERT + "," + infor.APPLY_WL + "," + infor.WL_WIDTH + "," + infor.WL_CENTER + "," + infor.WL_STT + "," + infor.WL_WOB + ",";
                    SQLstring += infor.LOW + "," + infor.HIGH + "," + infor.WL_LOW + "," + infor.WL_HIGH + "," + infor.APPLY_INVERT_FIRST + "," + infor.AUTO_MIN_MAX_BIT + "," + infor.APPLY_HEC + "," + infor.HEC_STT + "," + infor.APPLY_MED + "," + infor.MED_STT + "," + infor.MED_VALUE + "," + infor.IS_ENABLE + "," + infor.APPLY_MOTIONBLUR + "," + infor.MB_DIMENSION + "," + infor.MB_ANGLE + "," + infor.MB_STT + "," + infor.APPLY_ANTIALIAS + "," + infor.ANTIALIAS_DIMENSION + "," + infor.ANTIALIAS_THRESHOLD + "," + infor.ANTIALIAS_FILTER + "," + infor.ANTIALIAS_STT + "," + infor.APPLY_SE + "," + infor.SE_STT + ")";
                }
                else
                {
                    SQLstring = "INSERT INTO TBL_IECONFIG (IE_NAME, START_WIDTH ,START_CENTER,END_WIDTH,END_CENTER";
                    SQLstring += ",APPLY_GAMMA ,APPLY_CONTRAST,APPLY_BRIGHTNESS,APPLY_MSE,GAMMA_VALUE,CONTRAST_VALUE";
                    SQLstring += ",BRIGHTNESS_VALUE,MSE_VALUE,MSE_APPLY_ELGE_EHANCEMENT,MSE_APPLY_LATITUDE_REDUCTION";
                    SQLstring += ",MSE_EC,MSE_EL,MSE_LC,MSE_LL,MSE_TYPE ,MSE_ORDER,CONTRAST_ORDER,BRIGHTNESS_ORDER,GAMMA_ORDER,WOB,INVERT_STT,INVERT_AFTER,APPLY_INVERT,APPLY_WL,WL_WIDTH,WL_CENTER,WL_STT,WL_WOB,LOW,HIGH,WL_LOW,WL_HIGH,APPLY_INVERT_FIRST,AUTO_MIN_MAX_BIT,APPLY_HEC,HEC_STT,APPLY_MED,MED_STT,MED_VALUE,IS_ENABLE,APPLY_MOTIONBLUR,MB_DIMENSION,MB_ANGLE,MB_STT,APPLY_ANTIALIAS,ANTIALIAS_DIMENSION,ANTIALIAS_THRESHOLD,ANTIALIAS_FILTER,ANTIALIAS_STT,APPLY_SE,SE_STT,XRES,YRES,NBINS,LLV,HLV,SLOPE,NEWAPI,LUTTYPE)";

                    SQLstring += " VALUES('" + infor.IE_NAME + "'," + infor.START_WIDTH + "," + infor.START_CENTER + "," + infor.END_WIDTH + "," + infor.END_CENTER + "," + infor.APPLY_GAMMA + ",";
                    SQLstring += infor.APPLY_CONTRAST + "," + infor.APPLY_BRIGHTNESS + "," + infor.APPLY_MSE + "," + infor.GAMMA_VALUE + "," + infor.CONTRAST_VALUE + "," + infor.BRIGHTNESS_VALUE + ",";
                    SQLstring += infor.MSE_VALUE + "," + infor.MSE_APPLY_ELGE_EHANCEMENT + "," + infor.MSE_APPLY_LATITUDE_REDUCTION + "," + infor.MSE_EC + "," + infor.MSE_EL + ",";
                    SQLstring += infor.MSE_LC + "," + infor.MSE_LL + "," + infor.MSE_TYPE + "," + infor.MSE_ORDER + "," + infor.CONTRAST_ORDER + "," + infor.BRIGHTNESS_ORDER + ",";
                    SQLstring += infor.GAMMA_ORDER + "," + infor.WOB + "," + infor.INVERT_STT + "," + infor.INVERT_AFTER + "," + infor.APPLY_INVERT + "," + infor.APPLY_WL + "," + infor.WL_WIDTH + "," + infor.WL_CENTER + "," + infor.WL_STT + "," + infor.WL_WOB + ",";
                    SQLstring += infor.LOW + "," + infor.HIGH + "," + infor.WL_LOW + "," + infor.WL_HIGH + "," + infor.APPLY_INVERT_FIRST + "," + infor.AUTO_MIN_MAX_BIT + "," + infor.APPLY_HEC + "," + infor.HEC_STT + "," + infor.APPLY_MED + "," + infor.MED_STT + "," + infor.MED_VALUE + "," + infor.IS_ENABLE + "," + infor.APPLY_MOTIONBLUR + "," + infor.MB_DIMENSION + "," + infor.MB_ANGLE + "," + infor.MB_STT + "," + infor.APPLY_ANTIALIAS + "," + infor.ANTIALIAS_DIMENSION + "," + infor.ANTIALIAS_THRESHOLD + "," + infor.ANTIALIAS_FILTER + "," + infor.ANTIALIAS_STT + "," + infor.APPLY_SE + "," + infor.SE_STT + "," + infor.XRES + "," + infor.YRES + "," + infor.NBINS + "," + infor.LLV + "," + infor.HLV + "," + infor.SLOPE + ",1," + infor.LUTTYPE + ")";
                }
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    infor.ID = Convert.ToInt16(Utility.getCurrentMaxID("ID", "TBL_IECONFIG"));
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
        public IECONFIGInfor CopyIE(int SourceID, string newName)
        {
            string SQLstring = "";
            try
            {

                IECONFIGInfor infor = GetIEInfor(SourceID);
                infor.IE_NAME = newName;

                if (infor != null)
                {
                    if (isUseNewAPI())
                    {
                        SQLstring = "INSERT INTO TBL_IECONFIG (IE_NAME, START_WIDTH ,START_CENTER,END_WIDTH,END_CENTER";
                        SQLstring += ",APPLY_GAMMA ,APPLY_CONTRAST,APPLY_BRIGHTNESS,APPLY_MSE,GAMMA_VALUE,CONTRAST_VALUE";
                        SQLstring += ",BRIGHTNESS_VALUE,MSE_VALUE,MSE_APPLY_ELGE_EHANCEMENT,MSE_APPLY_LATITUDE_REDUCTION";
                        SQLstring += ",MSE_EC,MSE_EL,MSE_LC,MSE_LL,MSE_TYPE ,MSE_ORDER,CONTRAST_ORDER,BRIGHTNESS_ORDER,GAMMA_ORDER,WOB,INVERT_STT,INVERT_AFTER,APPLY_INVERT,APPLY_WL,WL_WIDTH,WL_CENTER,WL_STT,WL_WOB,LOW,HIGH,WL_LOW,WL_HIGH,APPLY_INVERT_FIRST,AUTO_MIN_MAX_BIT,APPLY_HEC,HEC_STT,APPLY_MED,MED_STT,MED_VALUE,IS_ENABLE,APPLY_MOTIONBLUR,MB_DIMENSION,MB_ANGLE,MB_STT,APPLY_ANTIALIAS,ANTIALIAS_DIMENSION,ANTIALIAS_THRESHOLD,ANTIALIAS_FILTER,ANTIALIAS_STT,APPLY_SE,SE_STT,XRES,YRES,NBINS,LLV,HLV,SLOPE,NEWAPI,LUTTYPE)";
                        SQLstring += " VALUES('" + infor.IE_NAME + "'," + infor.START_WIDTH + "," + infor.START_CENTER + "," + infor.END_WIDTH + "," + infor.END_CENTER + "," + infor.APPLY_GAMMA + ",";
                        SQLstring += infor.APPLY_CONTRAST + "," + infor.APPLY_BRIGHTNESS + "," + infor.APPLY_MSE + "," + infor.GAMMA_VALUE + "," + infor.CONTRAST_VALUE + "," + infor.BRIGHTNESS_VALUE + ",";
                        SQLstring += infor.MSE_VALUE + "," + infor.MSE_APPLY_ELGE_EHANCEMENT + "," + infor.MSE_APPLY_LATITUDE_REDUCTION + "," + infor.MSE_EC + "," + infor.MSE_EL + ",";
                        SQLstring += infor.MSE_LC + "," + infor.MSE_LL + "," + infor.MSE_TYPE + "," + infor.MSE_ORDER + "," + infor.CONTRAST_ORDER + "," + infor.BRIGHTNESS_ORDER + ",";
                        SQLstring += infor.GAMMA_ORDER + "," + infor.WOB + "," + infor.INVERT_STT + "," + infor.INVERT_AFTER + "," + infor.APPLY_INVERT + "," + infor.APPLY_WL + "," + infor.WL_WIDTH + "," + infor.WL_CENTER + "," + infor.WL_STT + "," + infor.WL_WOB + ",";
                        SQLstring += infor.LOW + "," + infor.HIGH + "," + infor.WL_LOW + "," + infor.WL_HIGH + "," + infor.APPLY_INVERT_FIRST + "," + infor.AUTO_MIN_MAX_BIT + "," + infor.APPLY_HEC + "," + infor.HEC_STT + "," + infor.APPLY_MED + "," + infor.MED_STT + "," + infor.MED_VALUE + "," + infor.IS_ENABLE + "," + infor.APPLY_MOTIONBLUR + "," + infor.MB_DIMENSION + "," + infor.MB_ANGLE + "," + infor.MB_STT + "," + infor.APPLY_ANTIALIAS + "," + infor.ANTIALIAS_DIMENSION + "," + infor.ANTIALIAS_THRESHOLD + "," + infor.ANTIALIAS_FILTER + "," + infor.ANTIALIAS_STT + "," + infor.APPLY_SE + "," + infor.SE_STT + "," + infor.XRES + "," + infor.YRES + "," + infor.NBINS + "," + infor.LLV + "," + infor.HLV + "," + infor.SLOPE + ",1," + infor.LUTTYPE + ")";
                    }
                    else
                    {
                        SQLstring = "INSERT INTO TBL_IECONFIG (IE_NAME, START_WIDTH ,START_CENTER,END_WIDTH,END_CENTER";
                        SQLstring += ",APPLY_GAMMA ,APPLY_CONTRAST,APPLY_BRIGHTNESS,APPLY_MSE,GAMMA_VALUE,CONTRAST_VALUE";
                        SQLstring += ",BRIGHTNESS_VALUE,MSE_VALUE,MSE_APPLY_ELGE_EHANCEMENT,MSE_APPLY_LATITUDE_REDUCTION";
                        SQLstring += ",MSE_EC,MSE_EL,MSE_LC,MSE_LL,MSE_TYPE ,MSE_ORDER,CONTRAST_ORDER,BRIGHTNESS_ORDER,GAMMA_ORDER,WOB,INVERT_STT,INVERT_AFTER,APPLY_INVERT,APPLY_WL,WL_WIDTH,WL_CENTER,WL_STT,WL_WOB,LOW,HIGH,WL_LOW,WL_HIGH,APPLY_INVERT_FIRST,AUTO_MIN_MAX_BIT,APPLY_HEC,HEC_STT,APPLY_MED,MED_STT,MED_VALUE,IS_ENABLE,APPLY_MOTIONBLUR,MB_DIMENSION,MB_ANGLE,MB_STT,APPLY_ANTIALIAS,ANTIALIAS_DIMENSION,ANTIALIAS_THRESHOLD,ANTIALIAS_FILTER,ANTIALIAS_STT,APPLY_SE,SE_STT)";
                        SQLstring += " VALUES('" + infor.IE_NAME + "'," + infor.START_WIDTH + "," + infor.START_CENTER + "," + infor.END_WIDTH + "," + infor.END_CENTER + "," + infor.APPLY_GAMMA + ",";
                        SQLstring += infor.APPLY_CONTRAST + "," + infor.APPLY_BRIGHTNESS + "," + infor.APPLY_MSE + "," + infor.GAMMA_VALUE + "," + infor.CONTRAST_VALUE + "," + infor.BRIGHTNESS_VALUE + ",";
                        SQLstring += infor.MSE_VALUE + "," + infor.MSE_APPLY_ELGE_EHANCEMENT + "," + infor.MSE_APPLY_LATITUDE_REDUCTION + "," + infor.MSE_EC + "," + infor.MSE_EL + ",";
                        SQLstring += infor.MSE_LC + "," + infor.MSE_LL + "," + infor.MSE_TYPE + "," + infor.MSE_ORDER + "," + infor.CONTRAST_ORDER + "," + infor.BRIGHTNESS_ORDER + ",";
                        SQLstring += infor.GAMMA_ORDER + "," + infor.WOB + "," + infor.INVERT_STT + "," + infor.INVERT_AFTER + "," + infor.APPLY_INVERT + "," + infor.APPLY_WL + "," + infor.WL_WIDTH + "," + infor.WL_CENTER + "," + infor.WL_STT + "," + infor.WL_WOB + ",";
                        SQLstring += infor.LOW + "," + infor.HIGH + "," + infor.WL_LOW + "," + infor.WL_HIGH + "," + infor.APPLY_INVERT_FIRST + "," + infor.AUTO_MIN_MAX_BIT + "," + infor.APPLY_HEC + "," + infor.HEC_STT + "," + infor.APPLY_MED + "," + infor.MED_STT + "," + infor.MED_VALUE + "," + infor.IS_ENABLE + "," + infor.APPLY_MOTIONBLUR + "," + infor.MB_DIMENSION + "," + infor.MB_ANGLE + "," + infor.MB_STT + "," + infor.APPLY_ANTIALIAS + "," + infor.ANTIALIAS_DIMENSION + "," + infor.ANTIALIAS_THRESHOLD + "," + infor.ANTIALIAS_FILTER + "," + infor.ANTIALIAS_STT + "," + infor.APPLY_SE + "," + infor.SE_STT + ")";
                    }
                }
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                {
                    infor.ID = Convert.ToInt16(Utility.getCurrentMaxID("ID", "TBL_IECONFIG"));
                    
                    return infor;
                }
                else
                {
                    return null;
                }
                return infor;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }
        public ActionResult UpdateIE(IECONFIGInfor infor)
        {
            string SQLstring = "";
            try
            {
                if (ExistsIEOnUpdate(infor))
                {
                    return ActionResult.ExistedRecord;
                }
                if (!isUseNewAPI())
                {
                    SQLstring = "Update TBL_IECONFIG set IE_NAME='" + infor.IE_NAME + "', START_WIDTH=" + infor.START_WIDTH + " ,START_CENTER=" + infor.START_CENTER + " ,END_WIDTH=" + infor.END_WIDTH + " ,END_CENTER=" + infor.END_CENTER + " ";
                    SQLstring += ",APPLY_GAMMA=" + infor.APPLY_GAMMA + "  ,APPLY_CONTRAST=" + infor.APPLY_CONTRAST + " ,APPLY_BRIGHTNESS=" + infor.APPLY_BRIGHTNESS + " ,APPLY_MSE=" + infor.APPLY_MSE + " ,GAMMA_VALUE=" + infor.GAMMA_VALUE + " ,CONTRAST_VALUE=" + infor.CONTRAST_VALUE + " ";
                    SQLstring += ",BRIGHTNESS_VALUE=" + infor.BRIGHTNESS_VALUE + " ,MSE_VALUE=" + infor.MSE_VALUE + " ,MSE_APPLY_ELGE_EHANCEMENT=" + infor.MSE_APPLY_ELGE_EHANCEMENT + " ,MSE_APPLY_LATITUDE_REDUCTION=" + infor.MSE_APPLY_LATITUDE_REDUCTION + " ";
                    SQLstring += ",MSE_EC=" + infor.MSE_EC + " ,MSE_EL=" + infor.MSE_EL + " ,MSE_LC=" + infor.MSE_LC + " ,MSE_LL=" + infor.MSE_LL + " ,MSE_TYPE=" + infor.MSE_TYPE + "  ,MSE_ORDER=" + infor.MSE_ORDER + " ,CONTRAST_ORDER=" + infor.CONTRAST_ORDER + " ,BRIGHTNESS_ORDER=" + infor.BRIGHTNESS_ORDER + " ,GAMMA_ORDER=" + infor.GAMMA_ORDER + " ,WOB=" + infor.WOB + ",INVERT_STT= " + infor.INVERT_STT + ",INVERT_AFTER= " + infor.INVERT_AFTER + ",APPLY_INVERT= " + infor.APPLY_INVERT + " ";
                    SQLstring += ",APPLY_WL=" + infor.APPLY_WL + " ,WL_WIDTH=" + infor.WL_WIDTH + " ,WL_CENTER=" + infor.WL_CENTER + " ,WL_STT=" + infor.WL_STT + " ,WL_WOB=" + infor.WL_WOB + " ";
                    SQLstring += ",LOW=" + infor.LOW + " ,HIGH=" + infor.HIGH + " ,WL_LOW=" + infor.WL_LOW + " ,WL_HIGH=" + infor.WL_HIGH + " ";
                    SQLstring += ",APPLY_INVERT_FIRST=" + infor.APPLY_INVERT_FIRST + " ,AUTO_MIN_MAX_BIT=" + infor.AUTO_MIN_MAX_BIT + " ,APPLY_HEC=" + infor.APPLY_HEC + " ,HEC_STT=" + infor.HEC_STT + " ";
                    SQLstring += ",APPLY_MED=" + infor.APPLY_MED + " ,MED_VALUE=" + infor.MED_VALUE + " ,MED_STT=" + infor.MED_STT + " ";
                    SQLstring += ",APPLY_MOTIONBLUR=" + infor.APPLY_MOTIONBLUR + " ,MB_DIMENSION=" + infor.MB_DIMENSION + " ,MB_ANGLE=" + infor.MB_ANGLE + " ,MB_STT=" + infor.MB_STT + " ";
                    SQLstring += ",APPLY_ANTIALIAS=" + infor.APPLY_ANTIALIAS + " ,ANTIALIAS_DIMENSION=" + infor.ANTIALIAS_DIMENSION + " ,ANTIALIAS_THRESHOLD=" + infor.ANTIALIAS_THRESHOLD + " ,ANTIALIAS_FILTER=" + infor.ANTIALIAS_FILTER + " ,ANTIALIAS_STT=" + infor.ANTIALIAS_STT + ",APPLY_SE=" + infor.APPLY_SE + ",SE_STT=" + infor.SE_STT + " ";
                    SQLstring += " WHERE ID=" + infor.ID;
                }
                else
                {
                    SQLstring = "Update TBL_IECONFIG set IE_NAME='" + infor.IE_NAME + "', START_WIDTH=" + infor.START_WIDTH + " ,START_CENTER=" + infor.START_CENTER + " ,END_WIDTH=" + infor.END_WIDTH + " ,END_CENTER=" + infor.END_CENTER + " ";
                    SQLstring += ",APPLY_GAMMA=" + infor.APPLY_GAMMA + "  ,APPLY_CONTRAST=" + infor.APPLY_CONTRAST + " ,APPLY_BRIGHTNESS=" + infor.APPLY_BRIGHTNESS + " ,APPLY_MSE=" + infor.APPLY_MSE + " ,GAMMA_VALUE=" + infor.GAMMA_VALUE + " ,CONTRAST_VALUE=" + infor.CONTRAST_VALUE + " ";
                    SQLstring += ",BRIGHTNESS_VALUE=" + infor.BRIGHTNESS_VALUE + " ,MSE_VALUE=" + infor.MSE_VALUE + " ,MSE_APPLY_ELGE_EHANCEMENT=" + infor.MSE_APPLY_ELGE_EHANCEMENT + " ,MSE_APPLY_LATITUDE_REDUCTION=" + infor.MSE_APPLY_LATITUDE_REDUCTION + " ";
                    SQLstring += ",MSE_EC=" + infor.MSE_EC + " ,MSE_EL=" + infor.MSE_EL + " ,MSE_LC=" + infor.MSE_LC + " ,MSE_LL=" + infor.MSE_LL + " ,MSE_TYPE=" + infor.MSE_TYPE + "  ,MSE_ORDER=" + infor.MSE_ORDER + " ,CONTRAST_ORDER=" + infor.CONTRAST_ORDER + " ,BRIGHTNESS_ORDER=" + infor.BRIGHTNESS_ORDER + " ,GAMMA_ORDER=" + infor.GAMMA_ORDER + " ,WOB=" + infor.WOB + ",INVERT_STT= " + infor.INVERT_STT + ",INVERT_AFTER= " + infor.INVERT_AFTER + ",APPLY_INVERT= " + infor.APPLY_INVERT + " ";
                    SQLstring += ",APPLY_WL=" + infor.APPLY_WL + " ,WL_WIDTH=" + infor.WL_WIDTH + " ,WL_CENTER=" + infor.WL_CENTER + " ,WL_STT=" + infor.WL_STT + " ,WL_WOB=" + infor.WL_WOB + " ";
                    SQLstring += ",LOW=" + infor.LOW + " ,HIGH=" + infor.HIGH + " ,WL_LOW=" + infor.WL_LOW + " ,WL_HIGH=" + infor.WL_HIGH + " ";
                    SQLstring += ",APPLY_INVERT_FIRST=" + infor.APPLY_INVERT_FIRST + " ,AUTO_MIN_MAX_BIT=" + infor.AUTO_MIN_MAX_BIT + " ,APPLY_HEC=" + infor.APPLY_HEC + " ,HEC_STT=" + infor.HEC_STT + " ";
                    SQLstring += ",APPLY_MED=" + infor.APPLY_MED + " ,MED_VALUE=" + infor.MED_VALUE + " ,MED_STT=" + infor.MED_STT + " ";
                    SQLstring += ",APPLY_MOTIONBLUR=" + infor.APPLY_MOTIONBLUR + " ,MB_DIMENSION=" + infor.MB_DIMENSION + " ,MB_ANGLE=" + infor.MB_ANGLE + " ,MB_STT=" + infor.MB_STT + " ";
                    SQLstring += ",APPLY_ANTIALIAS=" + infor.APPLY_ANTIALIAS + " ,ANTIALIAS_DIMENSION=" + infor.ANTIALIAS_DIMENSION + " ,ANTIALIAS_THRESHOLD=" + infor.ANTIALIAS_THRESHOLD + " ,ANTIALIAS_FILTER=" + infor.ANTIALIAS_FILTER + " ,ANTIALIAS_STT=" + infor.ANTIALIAS_STT + ",APPLY_SE=" + infor.APPLY_SE + ",SE_STT=" + infor.SE_STT + " ";
                    SQLstring += ",XRES=" + infor.XRES + " ,YRES=" + infor.YRES + " ,NBINS=" + infor.NBINS + " ,LLV=" + infor.LLV + " ,HLV=" + infor.HLV + ",SLOPE=" + infor.SLOPE + ",LUTTYPE=" + infor.LUTTYPE + ",NEWAPI= "+infor.NEWAPI+" ";
                    SQLstring += " WHERE ID=" + infor.ID;
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
        public ActionResult UpdateIE(int IEID,int XRES,int YRES,int NBINS,int LLV,int HLV,decimal SLOPE,int LUTTYPE,int NEWAPI)
        {
            string SQLstring = "";
            try
            {
               
               
                  
                    SQLstring = "Update TBL_IECONFIG set XRES=" + XRES + " ,YRES=" + YRES + " ,NBINS=" + NBINS + " ,LLV=" + LLV + " ,HLV=" + HLV + ",SLOPE=" + SLOPE + ",LUTTYPE=" + LUTTYPE + ",NEWAPI= " + NEWAPI + " ";
                    SQLstring += " WHERE ID=" + IEID;
               

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
        public ActionResult UpdateIE(int IEID, string IE_NAME)
        {
            string SQLstring = "";
            try
            {
                SQLstring = "Update TBL_IECONFIG set IE_NAME='" + IE_NAME + "'";
                SQLstring += " WHERE ID=" + IEID;

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
        public ActionResult Delete_IE_Device_Pos(int Device_ID,string Anatomy,string projection)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "Delete from TBL_IE_DEVICE_POS_RELATION WHERE DEVICE_ID=" + Device_ID + " AND ANATOMY_CODE='" + Anatomy + "' AND PROJECTION_CODE='" + projection + "'";


                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);


                return ActionResult.Success;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        public bool Delete_IE(int IEID)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "Delete from tbl_ieconfig WHERE ID=" + IEID.ToString();


                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);


                return true;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        public ActionResult DeleteAll_IE_Device_Pos(int Device_ID)
        {
            string SQLstring = "";
            try
            {

                SQLstring = "Delete from TBL_IE_DEVICE_POS_RELATION WHERE DEVICE_ID=" + Device_ID;


                DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
               
                    return ActionResult.Success;
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        public ActionResult Insert_IE_Device_Pos_Relation(int Device_ID, string ANATOMY_CODE, string PROJECTION_CODE, int IE_ID)
        {
            string SQLstring = "";
            try
            {
                SQLstring = "UPDATE  TBL_IE_DEVICE_POS_RELATION SET IE_ID=" + IE_ID.ToString() + ",CommonApplied=0 WHERE DEVICE_ID=" + Device_ID + " AND ANATOMY_CODE='" + ANATOMY_CODE + "' AND PROJECTION_CODE='" + PROJECTION_CODE + "'";
                if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) <= 0)
                {
                    SQLstring = "INSERT INTO TBL_IE_DEVICE_POS_RELATION(DEVICE_ID,ANATOMY_CODE,PROJECTION_CODE,IE_ID,CommonApplied) VALUES(" + Device_ID + ",'" + ANATOMY_CODE + "','" + PROJECTION_CODE + "'," + IE_ID.ToString() + ",0)";
                    if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) <= 0)
                    {
                        return ActionResult.Error;
                    }
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        public ActionResult Insert_IE_Device_Pos_Relation(int Device_ID,List<string> lstAP,int IE_ID,bool CommonApplied)
        {
            string SQLstring = "";
            try
            {
                if (lstAP.Count > 1 && CommonApplied)
                {
                    //Xóa tất cả các cấu hình ứng với thiết bị để áp dụng lại cấu hình mới
                    if (DeleteAll_IE_Device_Pos(Device_ID) != ActionResult.Success)
                        return ActionResult.Error;
                }
                else
                {
                    //Chỉ xóa các thằng được áp
                    foreach (string APCode in lstAP)
                    {
                        string A_CODE = APCode.Split('@')[0];
                        string P_CODE = APCode.Split('@')[1];

                        //Xóa cấu hình của vị trí của thiết bị để áp dụng cấu hình mới
                        if (Delete_IE_Device_Pos(Device_ID, A_CODE, P_CODE) != ActionResult.Success)
                            return ActionResult.Error;
                    }
                }
                //Insert lại các thằng được áp mới
                foreach (string A_CODE in lstAP)
                {
                    SQLstring = "INSERT INTO TBL_IE_DEVICE_POS_RELATION(DEVICE_ID,ANATOMY_CODE,PROJECTION_CODE,IE_ID,CommonApplied) VALUES(" + Device_ID + ",'" + A_CODE.Split('@')[0] + "','" + A_CODE.Split('@')[1] + "'," + IE_ID.ToString() + "," + (CommonApplied ? 1 : 0).ToString() + ")";
                    if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) <= 0)
                    {
                        return ActionResult.Error;
                    }
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }
        }
        /// <summary>
        /// Cập nhật đối tượng Modality có trong bảng CSDL
        /// Đầu vào là đối tượng ModalityInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            try
            {
                string SQLstring = null;
                //Cập nhật số thứ tự cho nước đổi số thứ tự với nước đang được chọn
                if (Infor.Pos != Infor.OldPos)
                {
                    SQLstring = "UPDATE  L_Modalities SET pos=" + Infor.OldPos + " WHERE pos=" + Infor.Pos;
                    DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring);
                }
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Modalities SET Modality_Code='" + Infor.Modality_Code + "',Modality_Name='" + Infor.Modality_Name + "',[Desc]='" + Infor.Desc + "',Pos=" + Infor.Pos
                    + ",IP_ADDRESS='" + Infor.IP_ADDRESS + "',AE_TITLE='" + Infor.AE_TITLE + "',RESTRICTION='" + Infor.RESTRICTION
                    + "',PORT_NUM=" + Infor.PORT_NUM + ",DEVICE_WORKLIST=" + Infor.DEVICE_WORKLIST + ",STATUS=" + Infor.STATUS + ",ROOM_ID=" + Infor.Room_ID
                    + ",MOD_TYPE_ID=" + Infor.Mod_Type_ID + ",COUNTRY_ID='" + Infor.Country_ID + "',MANUFACTURE_ID=" + Infor.Manufacture_ID
                    + ",IMGW=" + Infor.IMGW + ",IMGH=" + Infor.IMGH + ",AUTO_FLIPH=" + Infor.AUTO_FLIPH + ",AUTO_FLIPV=" + Infor.AUTO_FLIPV
                    + " WHERE Modality_ID=" + Infor.Modality_ID;
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
        public ActionResult UpdateAutoFlip(int ModID,int AFlipV,int AFlipH)
        {
            try
            {
                string SQLstring = null;
                //Cập nhật số thứ tự cho nước đổi số thứ tự với nước đang được chọn

                SQLstring = "UPDATE  L_Modalities SET AUTO_FLIPH=" + AFlipH + ",AUTO_FLIPV=" + AFlipV + " WHERE MODALITY_ID=" + ModID;
                    if (DataAccess.ExecuteNonQuery(globalVariables.OleDbConnection, CommandType.Text, SQLstring) > 0)
                        return ActionResult.Success;
                    return ActionResult.Error;
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }


        }
        public ActionResult UpdateWindowLeveling(string DeviceID,string LOW,string HIGH,string StartColor,string EndColor,string Range,ref string ErrMsg)
        {
            try
            {
                string SQLstring = null;
               
                //Cập nhật Nước đang được chọn
                SQLstring = "UPDATE L_Modalities SET LOW=" + LOW+",HIGH="+HIGH+",StartColor="+StartColor+",EndColor="+EndColor+",Range='"+Range+"'"
                    + " WHERE Modality_ID=" + DeviceID;
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
                ErrMsg = ex.Message;
                Utility.ShowMsg(ex.Message);
                return ActionResult.Exception;
            }


        }
        /// <summary>
        /// Xóa đối tượng Modality có trong CSDL dựa vào Infor.Modality_Code
        /// Đầu vào là đối tượng ModalityInfor
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                string SQLstring = null;
                SQLstring = "DELETE FROM L_Modalities WHERE Modality_Code='" + Infor.Modality_Code + "' AND Modality_ID=" + Infor.Modality_ID;
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
        public static ModalityInfor GetInfor(DataRow dr)
        {
            ModalityInfor Infor = new ModalityInfor();
            if (dr != null)
            {

                Infor.Modality_Code = Utility.sDbnull(dr["Modality_Code"]);
                Infor.Modality_ID = Utility.Int16Dbnull(dr["Modality_ID"]);
                Infor.Modality_Name = Utility.sDbnull(dr["Modality_Name"]);
                Infor.Pos = Utility.Int16Dbnull(dr["Pos"]);
                Infor.Desc = Utility.sDbnull(dr["Desc"]);
                Infor.Mod_Type_ID = Utility.Int16Dbnull(dr["Mod_Type_ID"]);
                Infor.Manufacture_ID = Utility.Int16Dbnull(dr["Manufacture_ID"]);
                Infor.Country_ID = Utility.sDbnull(dr["Country_ID"]);
                Infor.Room_ID = Utility.Int16Dbnull(dr["Room_ID"]);
                Infor.AE_TITLE = Utility.sDbnull(dr["AE_TITLE"]);
                Infor.RESTRICTION = Utility.sDbnull(dr["RESTRICTION"]);
                Infor.STATUS = Utility.ByteDbnull(dr["STATUS"]);
                Infor.PORT_NUM = Utility.Int16Dbnull(dr["PORT_NUM"]);
                Infor.DEVICE_WORKLIST = Utility.ByteDbnull(dr["DEVICE_WORKLIST"]);

                Infor.Country_Name = Utility.sDbnull(dr["Country_Name"]);
                Infor.Room_Name = Utility.sDbnull(dr["Room_Name"]);
                Infor.Manufacture_Name = Utility.sDbnull(dr["Manufacture_Name"]);
                Infor.Mod_Type_Name = Utility.sDbnull(dr["Mod_Type_Name"]);

                Infor.IMGH = Utility.Int32Dbnull(dr["IMGH"]);
                Infor.IMGW = Utility.Int32Dbnull(dr["IMGW"]);
                Infor.LOW = Utility.Int32Dbnull(dr["LOW"]);
                Infor.HIGH = Utility.Int32Dbnull(dr["HIGH"]);
                Infor.StartColor = Utility.Int32Dbnull(dr["StartColor"]);
                Infor.EndColor = Utility.Int32Dbnull(dr["EndColor"]);
                Infor.Range = Utility.sDbnull(dr["Range"]);

                Infor.AUTO_FLIPH = Utility.Int32Dbnull(dr["AUTO_FLIPH"]);
                Infor.AUTO_FLIPV = Utility.Int32Dbnull(dr["AUTO_FLIPV"]);

                
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
        public static IECONFIGInfor GetIEInfor(int ID)
        {
            DataSet ds = new ModalityController().GetIEData("ID=" + ID.ToString());
            IECONFIGInfor _infor = new IECONFIGInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    _infor.ID = -1;
                    _infor.IE_NAME = Utility.sDbnull(dr["IE_NAME"]);

                    _infor.GAMMA_VALUE = Utility.Int32Dbnull(dr["GAMMA_VALUE"]);
                    _infor.CONTRAST_VALUE = Utility.Int32Dbnull(dr["CONTRAST_VALUE"]);
                    _infor.BRIGHTNESS_VALUE = Utility.Int32Dbnull(dr["BRIGHTNESS_VALUE"]);
                    _infor.MSE_VALUE = Utility.Int32Dbnull(dr["MSE_VALUE"]);
                    _infor.MSE_EC = Utility.Int32Dbnull(dr["MSE_EC"]);
                    _infor.MSE_EL = Utility.Int32Dbnull(dr["MSE_EL"]);
                    _infor.MSE_LC = Utility.Int32Dbnull(dr["MSE_LC"]);
                    _infor.MSE_LL = Utility.Int32Dbnull(dr["MSE_LL"]);
                    _infor.MSE_TYPE = Utility.Int32Dbnull(dr["MSE_TYPE"]);
                    _infor.MSE_ORDER = Utility.Int32Dbnull(dr["MSE_ORDER"]);
                    _infor.CONTRAST_ORDER = Utility.Int32Dbnull(dr["CONTRAST_ORDER"]);
                    _infor.BRIGHTNESS_ORDER = Utility.Int32Dbnull(dr["BRIGHTNESS_ORDER"]);
                    _infor.GAMMA_ORDER = Utility.Int32Dbnull(dr["GAMMA_ORDER"]);
                    _infor.WOB = Utility.Int32Dbnull(dr["WOB"]);

                    _infor.LOW = Utility.Int32Dbnull(dr["LOW"]);
                    _infor.HIGH = Utility.Int32Dbnull(dr["HIGH"]);
                    _infor.WL_HIGH = Utility.Int32Dbnull(dr["WL_HIGH"]);
                    _infor.WL_LOW = Utility.Int32Dbnull(dr["WL_LOW"]);

                    _infor.MSE_APPLY_ELGE_EHANCEMENT = Utility.Int32Dbnull(dr["MSE_APPLY_ELGE_EHANCEMENT"]);
                    _infor.MSE_APPLY_LATITUDE_REDUCTION = Utility.Int32Dbnull(dr["MSE_APPLY_LATITUDE_REDUCTION"]);

                    _infor.START_WIDTH = Utility.Int32Dbnull(dr["START_WIDTH"]);
                    _infor.START_CENTER = Utility.Int32Dbnull(dr["START_CENTER"]);
                    _infor.END_WIDTH = Utility.Int32Dbnull(dr["END_WIDTH"]);
                    _infor.END_CENTER = Utility.Int32Dbnull(dr["END_CENTER"]);

                    _infor.APPLY_GAMMA = Utility.Int32Dbnull(dr["APPLY_GAMMA"]);
                    _infor.APPLY_CONTRAST = Utility.Int32Dbnull(dr["APPLY_CONTRAST"]);
                    _infor.APPLY_BRIGHTNESS = Utility.Int32Dbnull(dr["APPLY_BRIGHTNESS"]);
                    _infor.APPLY_MSE = Utility.Int32Dbnull(dr["APPLY_MSE"]);
                    _infor.INVERT_STT = Utility.Int32Dbnull(dr["INVERT_STT"]);
                    _infor.INVERT_AFTER = Utility.Int32Dbnull(dr["INVERT_AFTER"]);
                    _infor.APPLY_INVERT = Utility.Int32Dbnull(dr["APPLY_INVERT"]);

                    _infor.WL_CENTER = Utility.Int32Dbnull(dr["WL_CENTER"]);
                    _infor.WL_STT = Utility.Int32Dbnull(dr["WL_STT"]);
                    _infor.WL_WIDTH = Utility.Int32Dbnull(dr["WL_WIDTH"]);
                    _infor.APPLY_WL = Utility.Int32Dbnull(dr["APPLY_WL"]);
                    _infor.WL_WOB = Utility.Int32Dbnull(dr["WL_WOB"]);

                    _infor.APPLY_HEC = Utility.Int32Dbnull(dr["APPLY_HEC"]);
                    _infor.HEC_STT = Utility.Int32Dbnull(dr["HEC_STT"]);

                    _infor.APPLY_SE = Utility.Int32Dbnull(dr["APPLY_SE"]);
                    _infor.SE_STT = Utility.Int32Dbnull(dr["SE_STT"]);
                    return _infor;
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
        public static ModalityInfor GetInfor(Int16 ID)
        {
            DataSet ds = new ModalityController().GetData("Modality_ID=" + ID );
            ModalityInfor Infor = new ModalityInfor();
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Infor.Modality_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Modality_ID"]);
                    Infor.Modality_Code = Utility.sDbnull(ds.Tables[0].Rows[0]["Modality_Code"]);
                    Infor.Modality_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Modality_Name"]);
                    Infor.Pos = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Pos"]);
                    Infor.Desc = Utility.sDbnull(ds.Tables[0].Rows[0]["Desc"]);
                    Infor.Mod_Type_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Mod_Type_ID"]);
                    Infor.Manufacture_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Manufacture_ID"]);
                    Infor.Country_ID = Utility.sDbnull(ds.Tables[0].Rows[0]["Country_ID"]);
                    Infor.Room_ID = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["Room_ID"]);
                    Infor.AE_TITLE = Utility.sDbnull(ds.Tables[0].Rows[0]["AE_TITLE"]);
                    Infor.RESTRICTION = Utility.sDbnull(ds.Tables[0].Rows[0]["RESTRICTION"]);
                    Infor.STATUS = Utility.ByteDbnull(ds.Tables[0].Rows[0]["STATUS"]);
                    Infor.PORT_NUM = Utility.Int16Dbnull(ds.Tables[0].Rows[0]["PORT_NUM"]);
                    Infor.DEVICE_WORKLIST = Utility.ByteDbnull(ds.Tables[0].Rows[0]["DEVICE_WORKLIST"]);

                    Infor.Country_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Country_Name"]);
                    Infor.Room_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Room_Name"]);
                    Infor.Manufacture_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Manufacture_Name"]);
                    Infor.Mod_Type_Name = Utility.sDbnull(ds.Tables[0].Rows[0]["Mod_Type_Name"]);

                    Infor.IMGH = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["IMGH"]);
                    Infor.IMGW = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["IMGW"]);
                    Infor.LOW = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["LOW"]);
                    Infor.HIGH = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["HIGH"]);
                    Infor.StartColor = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["StartColor"]);
                    Infor.EndColor = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["EndColor"]);
                    Infor.Range = Utility.sDbnull(ds.Tables[0].Rows[0]["Range"]);
                    Infor.AUTO_FLIPH = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["AUTO_FLIPH"]);
                    Infor.AUTO_FLIPV = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["AUTO_FLIPV"]);
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
        public static void CreateDatarowFromObjectInfor(ref DataRow dr, ModalityInfor Infor)
        {

            if (Infor != null)
            {
                dr["Modality_ID"] =Infor.Modality_ID;
                dr["Modality_Code"] = Utility.sDbnull(Infor.Modality_Code);
                dr["Modality_Name"] = Utility.sDbnull(Infor.Modality_Name);
                dr["Pos"] = Utility.Int16Dbnull(Infor.Pos);
                dr["Desc"] = Utility.sDbnull(Infor.Desc);

               dr["Mod_Type_ID"]= Utility.Int16Dbnull(Infor.Mod_Type_ID );
               dr["Manufacture_ID"]=  Utility.Int16Dbnull(Infor.Manufacture_ID);
               dr["Country_ID"] = Utility.sDbnull(Infor.Country_ID);
                dr["Room_ID"]=Utility.Int16Dbnull(Infor.Room_ID );
               dr["AE_TITLE"]=Utility.sDbnull( Infor.AE_TITLE);
               dr["RESTRICTION"]= Utility.sDbnull(Infor.RESTRICTION);
               dr["STATUS"] = Utility.ByteDbnull(Infor.STATUS);
                dr["PORT_NUM"]= Utility.Int16Dbnull(Infor.PORT_NUM );
                dr["DEVICE_WORKLIST"] = Utility.ByteDbnull(Infor.DEVICE_WORKLIST);

                dr["Country_Name"] = Utility.sDbnull(Infor.Country_Name);
                dr["Manufacture_Name"] = Utility.sDbnull(Infor.Manufacture_Name);
                dr["Room_Name"] = Utility.sDbnull(Infor.Room_Name);
                dr["Mod_Type_Name"] = Utility.sDbnull(Infor.Mod_Type_Name);

                dr["IMGH"]=Infor.IMGH;
                dr["IMGW"]=Infor.IMGW;
                dr["LOW"]=Infor.LOW;
                dr["HIGH"]=Infor.HIGH ;
                dr["StartColor"]=Infor.StartColor ;
                dr["EndColor"] = Infor.EndColor;
                dr["Range"]=Infor.Range ;
            }
        }
        /// <summary>
        /// Lấy dữ liệu Modality dựa vào điều kiện truyền vào
        /// </summary>
        /// <param name="Condition">Điều kiện truyền vào</param>
        /// <returns>Dataset chứa dữ liệu</returns>
        public DataSet GetData(string Condition)
        {
            try
            {
                string SqlStr = "";
                SqlStr = "SELECT *, 0 as OldPos,"
                    + "(Select TOP 1 Country_Name from L_CountryList where Country_ID= R.Country_ID) as Country_Name "
                    + ",(Select TOP 1 Room_Name from L_RoomList where Room_ID= R.Room_ID) as Room_Name"
                    + ",(Select TOP 1 Mod_Type_Name from L_Modality_Type where Mod_Type_ID= R.Mod_Type_ID) as Mod_Type_Name"
                    + ",(Select TOP 1 Manufacture_Name from L_ManufactureList where Manufacture_ID= R.Manufacture_ID) as Manufacture_Name"
                    +" FROM L_Modalities R WHERE " + Condition;
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetAllIEData()
        {
            try
            {
                string SqlStr = "SELECT *,iif((select top 1 device_id from tbl_ie_device_pos_relation where ie_id=id) is null,-1,(select top 1 device_id from tbl_ie_device_pos_relation where ie_id=id)) as Device_ID from TBL_IECONFIG ORDER BY IE_NAME";
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
         public DataSet GetIEData(string Condition)
        {
            try
            {
                string SqlStr = "";
                SqlStr = "SELECT * from tbl_ieconfig R WHERE " + Condition;
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Modality có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllData(bool GetAllDevice)
        {
            try
            {
                string SqlStr = "SELECT *, 0 as OldPos,"
                   + "(Select TOP 1 Country_Name from L_CountryList where Country_ID= R.Country_ID) as Country_Name "
                   + ",(Select TOP 1 Room_Name from L_RoomList where Room_ID= R.Room_ID) as Room_Name"
                   + ",(Select TOP 1 Mod_Type_Name from L_Modality_Type where Mod_Type_ID= R.Mod_Type_ID) as Mod_Type_Name"
                   + ",(Select TOP 1 Mod_Type_Code from L_Modality_Type where Mod_Type_ID= R.Mod_Type_ID) as Mod_Type_Code"
                   + ",(Select TOP 1 Manufacture_Name from L_ManufactureList where Manufacture_ID= R.Manufacture_ID) as Manufacture_Name"
                   + " FROM L_Modalities R where "+(GetAllDevice?" 1=1 ":" status=1")+"  ORDER BY Pos,Modality_Name ";
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy tất cả danh sách dữ liệu Modality có trong bảng CSDL
        /// </summary>
        /// <returns>Dataset chứa danh sách dữ liệu</returns>
        public DataSet GetAllBodySizeData()
        {
            try
            {
                string SqlStr = "SELECT * from L_BODYSIZE ORDER BY STT";
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetIECONFIG(int Device_ID,string A_CODE,string P_CODE)
        {
            try
            {
                string SqlStr = "SELECT * from TBL_IE_DEVICE_POS_RELATION where DEVICE_ID=" + Device_ID + " AND ANATOMY_CODE='" + A_CODE + "' AND PROJECTION_CODE='" + P_CODE + "'";
                DataSet ds= DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SqlStr = "SELECT * from TBL_IECONFIG where ID=" + ds.Tables[0].Rows[0]["IE_ID"].ToString();
                    return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetIECONFIG(int Device_ID, string A_CODE, string P_CODE,ref int WW,ref int WC)
        {
            try
            {
                string SqlStr = "SELECT * from TBL_IE_DEVICE_POS_RELATION where DEVICE_ID=" + Device_ID + " AND ANATOMY_CODE='" + A_CODE + "' AND PROJECTION_CODE='" + P_CODE + "'";
                DataSet ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    WW = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["WW"], 0);
                    WC = Utility.Int32Dbnull(ds.Tables[0].Rows[0]["WC"], 0);
                    SqlStr = "SELECT * from TBL_IECONFIG where ID=" + ds.Tables[0].Rows[0]["IE_ID"].ToString();
                    return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetIECONFIG(int IE_ID)
        {
            try
            {
                string SqlStr = "SELECT *,(select TOP 1 MODALITY_NAME FROM L_Modalities where MODALITY_ID=p.DEVICE_ID) as FPD from TBL_IE_DEVICE_POS_RELATION p where IE_ID=" + IE_ID.ToString();
                    return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetIECONFIG(int IE_ID, int Device_ID)
        {
            try
            {
                string SqlStr = "SELECT *,(select TOP 1 MODALITY_NAME FROM L_Modalities where MODALITY_ID=p.DEVICE_ID) as FPD from TBL_IE_DEVICE_POS_RELATION p where IE_ID=" + IE_ID.ToString() + " AND DEVICE_ID=" + Device_ID;
                return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetIECONFIG( string A_CODE, string P_CODE)
        {
            try
            {
                string SqlStr = "SELECT * from TBL_IE_DEVICE_POS_RELATION where ANATOMY_CODE='" + A_CODE + "' AND PROJECTION_CODE='" + P_CODE + "'";
                DataSet ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    SqlStr = "SELECT * from TBL_IECONFIG where ID=" + ds.Tables[0].Rows[0]["IE_ID"].ToString();
                    return DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, SqlStr);
                }
                else
                    return null;
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
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM L_Modalities WHERE Modality_Code='" + Infor.Modality_Code + "'");
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
        private bool ExistsIEOnUpdate(IECONFIGInfor _infor)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM TBL_IECONFIG WHERE IE_NAME='" + _infor.IE_NAME + "' AND ID<>" + _infor.ID);
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
        private bool ExistsIEOnInsert(IECONFIGInfor _infor)
        {
            DataSet ds = null;
            try
            {
                ds = DataAccess.ExecuteDataset(globalVariables.OleDbConnection, CommandType.Text, "SELECT * FROM TBL_IECONFIG WHERE IE_NAME='" + _infor .IE_NAME+ "'");
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
