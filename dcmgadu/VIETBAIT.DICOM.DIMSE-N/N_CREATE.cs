using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VIETBAIT.DICOMHelper;
using VIETBAIT.DICOMPDU;

namespace VIETBAIT.DICOM.DIMSE_N
{
    class N_CREATE
    {
        public byte[] NCREATERequestCommand(byte PreCtxID, string affectedSOPClass, byte messageID, string affectedSOPInstanceUID)
        {
            var htNCREATERequestCMD = new Hashtable();
            var ngetRequestCmd = new PDataTFPDU(4);
            ngetRequestCmd.PDVContent.Command(true);

            htNCREATERequestCMD.Add(DICOMTag.AffectedSOPClassUIDTag, affectedSOPClass);
            htNCREATERequestCMD.Add(DICOMTag.CommandFieldTag, CommandFieldConst.N_CREATE_RQ);
            htNCREATERequestCMD.Add(DICOMTag.MessageIDTag, messageID);
            htNCREATERequestCMD.Add(DICOMTag.DataSetTypeTag, CommandFieldConst.NoDataSet);
            htNCREATERequestCMD.Add(DICOMTag.AffectedSOPInstanceUIDTag, affectedSOPInstanceUID);
            htNCREATERequestCMD.Add(DICOMTag.CommandGroupLengthTag, Ultility.CalculateGroupLengthValue(htNCREATERequestCMD));
            ArrayList listTag = new ArrayList(htNCREATERequestCMD.Keys);
            listTag.Sort();
            return ngetRequestCmd.CreateByteBuff(PreCtxID, true, true);
        }
    }
}
