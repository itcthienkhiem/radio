using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIETBAIT.DICOM.BASE
{
    public class DataElement
    {
        #region Public Property
        public byte[] DataValue {get; set;}
        public UInt32 DataTag {get; set; }
        public UInt32 Length {get; set;}
        public DataElement Parent { get; set; }
        private DataElement _parent;
        #endregion

        #region Contructor

        public DataElement()
        {
           
        }
        public DataElement (UInt32 tag,UInt32 len)
        {
            DataTag = tag;
            Length = len;
        }

        public DataElement (UInt32 tag,byte [] datavalue)
        {
            DataTag = tag;
            DataValue = datavalue;
            Length = (UInt32 ) datavalue.Length;
        }

        #endregion

        #region Destructor

         ~DataElement ()
        {
            DataValue = null;
        }

        #endregion


        public  byte[] CreateByteBuff()
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
                Array.Resize<byte>(ref byteBuff, (int) (Length + 8));
                DataValue.CopyTo(byteBuff, 8);

            }
            return byteBuff;
        }
    }
}
