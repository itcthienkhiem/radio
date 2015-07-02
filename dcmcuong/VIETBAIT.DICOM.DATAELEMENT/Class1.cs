using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIETBAIT.DICOM.BASE
{
    public class DataElement
    {


        public byte[] DataValue { get; set; }
        public UInt32 DataTag { get; set; }
        public UInt32 Length { get; set; }



        byte[] CreateByteBuff()
        {
            var byteBuff = new byte[8];
            byte[] tmp = BitConverter.GetBytes((UInt16)(DataTag >> 16));
            Array.Copy(tmp, 0, byteBuff, 0, 2);
            tmp = BitConverter.GetBytes((UInt16)(DataTag & 0xFFFF));
            Array.Copy(tmp, 0, byteBuff, 2, 2);
            tmp = BitConverter.GetBytes(Length);
            Array.Copy(tmp, 0, byteBuff, 4, tmp.Length);
            if ((Length != 0xFFFFFFFFUL) && (Length > 0))
            {
                byteBuff = new byte[Length + 8];
                DataValue.CopyTo(byteBuff, 8);

            }
            return byteBuff;
        }
    }
}
