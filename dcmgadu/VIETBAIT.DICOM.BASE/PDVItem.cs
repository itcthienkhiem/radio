using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VIETBAIT.DICOM.BASE
{
    public class PDVItem
    {
        public List<DataElement> DataElements {get;set;} 
        public Hashtable DataElementHashTable{get;set;}

        public UInt32 Length { get; set; }


        public byte PresentationContextID { get; set; }


        public byte MessageControlHeader { get; set; }

        public PDVItem ()
        {
            DataElements= new List<DataElement>();
            DataElementHashTable = new Hashtable();
        }
        ~PDVItem ()
        {
            DataElements.Clear();
            DataElements = null;
            DataElementHashTable.Clear();
            DataElementHashTable = null;
        }

        public bool IsCommand()
        {
            if ((MessageControlHeader & 0x01) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool IsLast()
        {
            if ((MessageControlHeader & 0x2) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Command(bool isCom)
        {
            if (isCom)
            {
                MessageControlHeader |= (0x01);
            }
            else
            {
                MessageControlHeader &= 0xFE;
            }
        }

        public void Last(bool isLast)
        {
            if (isLast)
            {
                MessageControlHeader |= 0x2;
            }
            else
            {
                MessageControlHeader &= 0xFD;
            }
        }

        public UInt32 GetLength(byte[] b, UInt32 off)
        {
            var tmp = new byte[4];
            Array.Copy(b, off, tmp, 0, 4);
            Array.Reverse(tmp);
            return (BitConverter.ToUInt32(tmp, 0));
        }

        //Public Overloads Sub AddDataSet(ByVal sData As String, ByVal TagGroup As UInt16, ByVal TagElement As UInt16)
        //    Dim TagTmp As New DataElement
        //    TagTmp.DataTag = (TagGroup << 16) + TagElement

        //    If Not String.IsNullOrEmpty(sData) Then
        //        If sData.Length Mod 2 = 1 Then
        //            sData = sData & " "
        //        End If
        //        TagTmp.DataValue = Encoding.ASCII.GetBytes(sData)
        //        TagTmp.Length = sData.Length
        //    Else
        //        TagTmp.Length = 0
        //    End If
        //    DataElements.Add(TagTmp)
        //End Sub

        public void AddDataSet(string sData, UInt32 dataTag)
        {
            var tagTmp = new DataElement();
            tagTmp.DataTag = dataTag;

            if (!string.IsNullOrEmpty(sData))
            {
                if (sData.Length%2 == 1)
                {
                    sData = sData + " ";
                }
                tagTmp.DataValue = Encoding.ASCII.GetBytes(sData);
                tagTmp.Length = (UInt32) sData.Length;
            }
            else
            {
                tagTmp.Length = 0;
            }
            DataElements.Add(tagTmp);
        }

        //Overloads Sub AddDataSet(ByVal iData As UInt16, ByVal TagGroup As UInt16, ByVal TagElement As UInt16)
        //    Dim TagTmp As New DataElement
        //    TagTmp.TagGroup = TagGroup
        //    TagTmp.TagElement = TagElement
        //    TagTmp.DataValue = BitConverter.GetBytes(CUShort(iData))
        //    TagTmp.Length = 2
        //    DataElements.Add(TagTmp)
        //End Sub

        //Overloads Sub AddDataSet(ByVal iData As UInt32, ByVal TagGroup As UInt16, ByVal TagElement As UInt16)
        //    Dim TagTmp As New DataElement
        //    TagTmp.TagGroup = TagGroup
        //    TagTmp.TagElement = TagElement
        //    TagTmp.DataValue = BitConverter.GetBytes(CUInt(iData))
        //    TagTmp.Length = 4
        //    DataElements.Add(TagTmp)
        //End Sub

        public void AddDataSet(UInt32 iData, UInt32 dataTag)
        {
            var tagTmp = new DataElement();
            tagTmp.DataTag = dataTag;
            tagTmp.DataValue = BitConverter.GetBytes(Convert.ToUInt32(iData));
            tagTmp.Length = 4;
            DataElements.Add(tagTmp);
        }

        public void AddDataSet(UInt16 iData, UInt32 dataTag)
        {
            var tagTmp = new DataElement();
            tagTmp.DataTag = dataTag;
            tagTmp.DataValue = BitConverter.GetBytes(Convert.ToUInt16(iData));
            tagTmp.Length = 2;
            DataElements.Add(tagTmp);
        }


        public void AddDelimiterDataSet(UInt32 tag)
        {
            var tagTmp = new DataElement(tag,(UInt32 ) 0xFFFFFFFFU);            
            DataElements.Add(tagTmp);
        }

        public void AddZeroLenDataSet(UInt32 tag)
        {
            var tagTmp = new DataElement(tag,(UInt32)0);            
            DataElements.Add(tagTmp);
        }

        public void CreateGroupLengthDataset(UInt32 groupLengthTag)
        {
            UInt32 grLen = 0;
            foreach (DataElement dataElement  in DataElements)
            {
                if (dataElement.Length != 0xFFFFFFFFU)
                {
                    grLen += dataElement.Length + 8;
                }
                else
                {
                    grLen += 12;
                }
            }
            var de = new DataElement();
            de.DataTag = groupLengthTag;
            de.Length = 4;
            de.DataValue = BitConverter.GetBytes(grLen);
            DataElements.Insert(0, de);
        }


        public  byte[] Parse(byte[] byteBuff, UInt16 off)
        {
            if (byteBuff == null) throw new ArgumentNullException("byteBuff");
            try
            {
                
                UInt32 pos;
                PresentationContextID = byteBuff[off];
                MessageControlHeader = byteBuff[off + 1];

                pos = (UInt32)off + 2;
                while ((pos < Length))
                {
                    if (pos > Length + off - 8)
                    {
                        break;
                    }


                    UInt32 key = (UInt32)((BitConverter.ToUInt16(byteBuff, (int)pos)) << 16) +
                                 BitConverter.ToUInt16(byteBuff, (int)pos + 2);
                    UInt32 Len = BitConverter.ToUInt32(byteBuff, (int)pos + 4);
                    var de = new DataElement();
                    Debug.WriteLine(key.ToString("X8"));
                    if (key.Equals(0xFFFEE0DDU) || key.Equals(0xFFFEE00DU) || key.Equals(0xFFFEE000U) ||
                        (Len == 0xFFFFFFFFU))
                    {
                        pos += 8;
                        //if (Len == 0xFFFFFFFFU) DataElementHashTable.Add(key, null);
                        de = new DataElement(key, 0);
                        DataElements.Add(de);
                        continue;
                    }
                    if (Len > 0)
                    {
                        if (pos + Len + 8 > Length + off)
                        {
                            break;
                        }

                        var dataValue = new byte[Len];
                        Array.Copy(byteBuff, pos + 8, dataValue, 0, Len);
                        if (Len > 7)
                        {
                            if (BitConverter.ToUInt32(dataValue,0) == 0xe000fffeuL)
                            {
                                Len = 0;
                                DataElementHashTable.Add(key, null );
                                de = new DataElement(key, Len );
                                DataElements.Add(de);
                            }
                            else
                            {
                                DataElementHashTable.Add(key, dataValue);
                                de = new DataElement(key, dataValue);
                                DataElements.Add(de);
                            }
                        }
                        else
                        {
                            DataElementHashTable.Add(key, dataValue);
                            de = new DataElement(key, dataValue);
                            DataElements.Add(de);
                        }
                    }
                    else
                    {
                        Len = 0;
                        DataElementHashTable.Add(key, null);
                        de = new DataElement(key, 0);
                        DataElements.Add(de);
                    }
                    pos += Len + 8;
                }

                if (pos == Length + off)
                {
                    return null;
                }
                else
                {
                    var tmp = new byte[Length + off - pos];
                    Array.Copy(byteBuff, pos, tmp, 0, Length + off - pos);
                    return tmp;
                }
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        


        private byte[] CreateDataElementsBuffer()
        {
            var buffer = new byte[4];
            UInt32 pos = 0;
            UInt32 len = 0;
            foreach (DataElement de in DataElements)
            {
                Debug .WriteLine(de.DataTag.ToString("X8"));

                byte[] tmp1 = de.CreateByteBuff();
                len += (UInt32)tmp1.Length;
                Array.Resize<byte>(ref buffer, (int) len);              
                Array.Copy(tmp1, 0, buffer, pos, tmp1.Length);
                pos += (UInt32) tmp1.Length;
            }
            return buffer;
        }

        //Function CreateHashTableBuffer() As Byte()
        //    Dim BB(4) As Byte
        //    Dim pos As UInt32 = 0
        //    Dim len As UInt32 = 0
        //    Dim tagList As New ArrayList(hashDataElement.Keys)

        //    For Each _tag As UInt32 In tagList
        //        Debug.WriteLine(Format(_tag, "X8"))
        //        Dim de As New DataElement
        //        de.DataTag = _tag
        //        de.DataValue = hashDataElement(_tag)
        //        de.Length = de.DataValue.Length
        //        Dim tmp1() As Byte = de.CreateByteBuff()
        //        len += de.Length + 8
        //        Array.Resize(BB, len)
        //        Array.Copy(tmp1, 0, BB, pos, tmp1.Length)
        //        pos += tmp1.Length
        //    Next
        //    Return BB
        //End Function
        //Function CreateBufferFromHashtable() As Byte()
        //    Return CreateByteBuffer(CreateHashTableBuffer, 4)
        //End Function
        public byte[] CreateByteBuffer(byte[] buffer, UInt32 off)
        {
            var bb = new byte[off + 2 + buffer.Length];

            bb[off] = PresentationContextID;
            bb[off + 1] = MessageControlHeader;
            Array.Copy(buffer, 0, bb, off + 2, buffer.Length);
            Length = (UInt32)buffer.Length + 2;
            byte[] tmp = BitConverter.GetBytes(Length);
            Array.Reverse(tmp);
            Array.Copy(tmp, bb, 4);
            return bb;
        }

        public byte[] CreateByteBuffer()
        {
            return CreateByteBuffer(CreateDataElementsBuffer(), 4);
        }

        

        //Overloads Function DataTagSearch(ByVal DataTagKey As UInt16) As Int32
        //    Dim Index As UInt32
        //    Dim de As DataElement
        //    For Index = 0 To DataElements.Count - 1
        //        de = DataElements(Index)
        //        If de.TagElement = DataTagKey Then
        //            Return Index
        //        End If
        //    Next
        //    Return -1
        //End Function

        public DataElement DataTagSearch(UInt32 dataTagKey)
        {
            DataElement de = DataElements.Find(
                dataElement => dataElement.DataTag == dataTagKey
                );
            return de;
        }
        
    }
}