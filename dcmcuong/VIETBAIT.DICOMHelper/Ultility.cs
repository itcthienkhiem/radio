using System;
using System.Collections;
using System.Diagnostics;

namespace VIETBAIT.DICOMHelper
{
    public static class Ultility
    {
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
        public static string CreateInstanceUID(object instanceObj)
        {
            return string.Concat(SOP.InstanceUID,"." , DateTime.Now.ToString("yyyyMMdd.hmmss.") , ".", Convert.ToString(instanceObj.GetHashCode()));
        }
    }
}