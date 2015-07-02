using System;
using System.Collections;
using System.Diagnostics;

namespace VIETBAIT.DICOMHelper
{
    public static class Ultility
    {
        public const UInt16 NoDataSet = 0x101;

        public const String DICOMApplicationContextName = "1.2.840.10008.3.1.1.1";
        public const String ImplicitLittleEndian = "1.2.840.10008.1.2";
        public const String ImplementationClassUID = "1.2.826.0.1.3680043.2.1545.4.3.0.0";
        public const String BasicFilmSessionSOPClassUID = "1.2.840.10008.5.1.1.1";
        public const String BasicFilmBoxSOPClassUID = "1.2.840.10008.5.1.1.2";
        public const String BasicGrayScaleImageSOPClassUID = "1.2.840.10008.5.1.1.4";
        public const String BasicGrayScalePrintManagementMetaSOPClassUID = "1.2.840.10008.5.1.1.9";
        public const String BasicAnnotationBoxSOPClassUID = "1.2.840.10008.5.1.1.15";
        public const String PrinterSOPClassUID = "1.2.840.10008.5.1.1.16";
        public const String PrinterSOPInstanceUID = "1.2.840.10008.5.1.1.17";


        public static byte[] ParseToHashtable(byte[] ByteBuff, UInt32 Length, UInt32 off, Hashtable hashDataElements)
        {
            UInt32 pos;
            pos = off;
            while ((pos < Length))
            {
                if (pos > Length - 8)
                {
                    break;
                }


                UInt32 key = (UInt32) ((BitConverter.ToUInt16(ByteBuff, (int) pos)) << 16) +
                             BitConverter.ToUInt16(ByteBuff, (int) pos + 2);
                UInt32 Len = BitConverter.ToUInt32(ByteBuff, (int) pos + 4);

                Debug.WriteLine(key.ToString("X8"));
                if (key.Equals(0xFFFEE0DDU) || key.Equals(0xFFFEE00DU) || key.Equals(0xFFFEE000U) ||
                    (Len == 0xFFFFFFFFU))
                {
                    pos += 8;
                    hashDataElements.Add(key, null);
                    continue;
                }

                if (Len > 0)
                {
                    if (pos + Len + 8 > Length + off)
                    {
                        break;
                    }

                    var DataValue = new byte[Len];
                    Array.Copy(ByteBuff, pos + 8, DataValue, 0, Len);
                    if (Len > 7)
                    {
                        if (BitConverter.ToUInt32(DataValue, 0) == 0xe000fffeuL)
                        {
                            Len = 0;
                        }
                        else hashDataElements.Add(key, DataValue);
                    }
                    else hashDataElements.Add(key, DataValue);
                }
                else
                {
                    Len = 0;
                    hashDataElements.Add(key, null);
                }
                pos += Len + 8;
            }
            if (pos == Length)
            {
                return null;
            }
            else
            {
                var tmp = new byte[Length - pos];
                Array.Copy(ByteBuff, pos, tmp, 0, Length - pos);
                return tmp;
            }
        }

        public static UInt32 CalculateGroupLengthValue(Hashtable hashTable)
        {
            try
            {
                UInt32 GrLen = 0;
                foreach (object value in hashTable.Values)
                {
                    GrLen += (UInt32 ) Convert.ToString(value).Length;
                }

                return GrLen;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}