using System;
using System.Collections;
using System.IO;
using VIETBAIT.DICOMHelper;
using VIETBAIT.DICOMPDU;

namespace VIETBAIT.DICOM.DIMSE_N
{
    public class N_GET
    {
//        Table 10.3-3
//N-GET-RQ MESSAGE FIELDS
//Message Field Tag VR VM Description of Field
//Command Group Length (0000,0000) UL 1 The even number of bytes from the end of the value field to the beginning of the next group.
//Requested SOP Class UID (0000,0003) UI 1 SOP Class UID of the SOP Instance for which Attribute Values are to be retrieved.
//Command Field (0000,0100) US 1 This field distinguishes the DIMSE-N operation conveyed by this Message. The value of this field shall be set to 0110H for the N-GET-RQ Message.
//Message ID (0000,0110) US 1 Implementation-specific value which distinguishes this Message from other Messages.
//Command Data Set Type (0000,0800) US 1 This field indicates that no Data Set shall be present in the Message. This field shall be set to the value of 0101H).
//Requested SOP Instance UID (0000,1001) UI 1 Contains the UID of the SOP Instance for which Attribute Values are to be retrieved.
//Attribute Identifier List (0000,1005) AT 1-n This field contains an Attribute Tag for each of the n Attributes applicable to the N-GET operation.
        public void CreateNGETRequestCommand(Stream os, byte preCtxID, string requestSOPClassUID, UInt16 messageID,
                                              string requestedSOPInstanceUID)
        {
            var nsetRequestCmd = new PDataTFPDU(4);

            nsetRequestCmd.PDVContent.Command(true);

            nsetRequestCmd.PDVContent.AddDataSet(requestSOPClassUID, DICOMTag.RequestedSOPClassUIDTag);
            nsetRequestCmd.PDVContent.AddDataSet(CommandFieldConst.N_GET_RQ, DICOMTag.CommandFieldTag);
            nsetRequestCmd.PDVContent.AddDataSet(messageID, DICOMTag.MessageIDTag);
            nsetRequestCmd.PDVContent.AddDataSet( CommandFieldConst.NoDataSet, DICOMTag.DataSetTypeTag);
            nsetRequestCmd.PDVContent.AddDataSet(requestedSOPInstanceUID, DICOMTag.RequestedSOPInstanceUIDTag);
            nsetRequestCmd.PDVContent.CreateGroupLengthDataset(DICOMTag.CommandGroupLengthTag);

            var sentmsg = nsetRequestCmd.CreateByteBuff(preCtxID, true, true);
            os.Write(sentmsg, 0, sentmsg.Length);
            
        }
    }
}