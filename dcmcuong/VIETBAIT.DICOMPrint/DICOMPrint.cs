using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using NLog;
using NLog.Config;
using NLog.Targets;
using VIETBAIT.AASSOCIATE;
using VIETBAIT.DICOM.BASE;
using VIETBAIT.DICOM.DIMSE_N;
using VIETBAIT.DICOMHelper;
using VIETBAIT.DICOMPDU;
using VIETBAIT.WORKLIST.WorkList;

namespace VIETBAIT.DICOMPrint
{
    public class DICOMPrint
    {
        private object FilmSessionInstance;
        private object FilmBoxInstance;
        private object ImageBoxInstance;
        private string FilmSessionInstanceUID;
        private string FilmBoxInstanceUID;
        private string ImageBoxInstanceUID;
        private readonly Timer _arTimer = new Timer(5000);
        private readonly Logger _logger = LogManager.GetLogger("DICOM_PRINT");

        private readonly Stream _stream;
        private readonly TcpClient tcpClient;
        private string BorderDensity = "BLACK ";
        private string FilmOrientation;
        private string FilmSizeID;
        private string ImageDisplayFormat;
        private byte _presentationContextID = 1;
        private byte bMessageID = 1;

        public DICOMPrint()
        {
            try
            {
                LogConfig();
                tcpClient = new TcpClient();
                _stream = tcpClient.GetStream();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DICOMPrint(string prnAddress, string prnPort, string prnAETitle, string localAETitle)
            : this()
        {
            PrinterAddress = prnAddress;
            PrinterPort = prnPort;
            PrinterAETitle = prnAETitle;
            LocalAETitle = localAETitle;
            _arTimer.Interval = PrinterTimeout;
            _arTimer.Elapsed += _arTimer_Elapsed;
        }

        public string PrinterAddress { get; set; }
        public string PrinterPort { get; set; }
        public string PrinterAETitle { get; set; }
        public uint PrinterTimeout { get; set; }
        public string LocalAETitle { get; set; }

        private int CreateAssociateSession()
        {
            UnparsePDU u;
            try
            {
                var associateRq = new AAssociateRQ(16384, LocalAETitle, PrinterAETitle);

                tcpClient.Connect(IPAddress.Parse(PrinterAddress), Convert.ToInt16(PrinterPort));


                if (tcpClient.Connected)
                {
                    byte[] byteBuffer = associateRq.CreateByteBuff();
                    _stream.Write(byteBuffer, 0, byteBuffer.Length);
                    var _receiveBuffer = new byte[16384];

                    u = new UnparsePDU(_stream, _receiveBuffer);
                    if (u.PDUType.Equals(2))
                    {
                        var associateAC = new AAssociateAC();

                        //var nget = new N_GET();
                    }
                    else if (u.PDUType.Equals(7))
                    {
                        tcpClient.Close();
                        return u.Buff[9];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("Exception", ex);
            }
            return -1;
        }

        private int GetPrinterJobStatus()
        {
            UnparsePDU u;
            var buff = new byte[0];
            try
            {
                var nget = new N_GET();

                nget.CreateNGETRequestCommand(_stream,_presentationContextID, SOP.PrinterSOPClassUID,
                                                               bMessageID, SOP.PrinterSOPInstanceUID);
               
                u = new UnparsePDU(_stream, buff);
                if (u.PDUType.Equals(4))
                {
                    var parsedPDU = new PDataTFPDU(u);
                    //ParsedPDU.PDUItem[DICOMTag.MessageIDBeingRespondedToTag] == bMessageID;
                    if (Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.StatusTag]).Equals(0))
                    {
                        _logger.Info("NGET RESPOND SUCCESS!");
                        if (
                            !Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.DataSetTypeTag]).Equals(
                                CommandFieldConst.NoDataSet))
                        {
                            u = new UnparsePDU(_stream, buff);
                            parsedPDU = new PDataTFPDU(u);
                            if (
                                Convert.ToString(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusTag]).
                                    Equals("FAILURE"))
                            {
                                _logger.Info("{0}{1}{2}", "Printer failure!", "\r\n",
                                             Convert.ToString(
                                                 parsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusInforTag]));
                                return 0;
                            }
                            else if (
                                Convert.ToString(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusTag])
                                    .Equals("WARNING"))
                            {
                                _logger.Info("{0}{1}{2}", "Printer warning!", "\r\n",
                                             Convert.ToString(
                                                 parsedPDU.PDVContent.DataElementHashTable[
                                                     DICOMTag.PrintStatusInforTag]));
                                return 1;
                            }
                            else if (
                                Convert.ToString(
                                    parsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusTag]).Equals(
                                        "NORMAL"))
                            {
                                _logger.Info("{0}{1}{2}", "Printer normal!", "\r\n",
                                             Convert.ToString(
                                                 parsedPDU.PDVContent.DataElementHashTable[
                                                     DICOMTag.PrintStatusInforTag]));
                                return 2;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("Exception", ex);
            }
            return 0;
        }

        private Int16 CreateFilmSession(UInt16 NoOfCopies)
        {
            UnparsePDU u;
            var buff = new byte[0];
            try
            {
                FilmSessionInstance = new object();
                var ncreate = new N_CREATE();
                var listAttributes = new List<DataElement>();
                var dataElement = new DataElement(DICOMTag.NumberOfCopiesTag,
                                                  Encoding.ASCII.GetBytes(NoOfCopies.ToString("D2")));
                listAttributes.Add(dataElement);
                FilmSessionInstanceUID = Ultility.CreateInstanceUID(FilmSessionInstance);
                ncreate.NCREATERequestCommand(_stream, _presentationContextID, SOP.BasicFilmSessionSOPClassUID,
                                              bMessageID,FilmSessionInstanceUID , listAttributes);
                u = new UnparsePDU(_stream, buff);
                if (u.PDUType.Equals(4))
                {
                    var parsedPDU = new PDataTFPDU(u);
                    string ncreateRespondstring = ncreate.NCREATERespondParse(parsedPDU);
                    if (!ncreateRespondstring.Contains("FAILURE"))
                    {
                        Int16 retstatus = Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.StatusTag]);
                        _logger.Info("NCREATE RESPOND SUCCESS!");
                        if (
                            !Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.DataSetTypeTag]).Equals(
                                CommandFieldConst.NoDataSet))
                        {
                            u = new UnparsePDU(_stream, buff);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("Exception", ex);
            }
            return 0;
        }

//        (0x20100000,UL,0x000000AA) # 
//(0x20100010,ST,"STANDARD\1,1") # Image Display Format
//(0x20100050,CS,"8INX10IN") # Film Size ID
//(0x20100100,CS,"BLACK ") # Border Density
//(0x20100150,ST,"128 ") # Configuration Information
//(0x20100500,SQ,
//# Item Number 1
//# (0xFFFEE000, UNDEFINED) # ItemIntroducer 
//  >(0x00081150,UI,"1.2.840.10008.5.1.1.1") # Referenced SOP Class UID
//  >(0x00081155,UI,"1.2.826.0.1.3680043.2.1211.9.1") # Referenced SOP Instance UID
//# (0xFFFEE00D, 0) # ItemDelimiter 
//# (0xFFFEE0DD, 0) # SequenceDelimiter
//) # Referenced Film Session Sequence with length of: 0

        private Int16 CreateBasicFilmBox(UInt16 row,UInt16 col)
        {
            UnparsePDU u;
            var buff = new byte[0];
            try
            {
                var ncreate = new N_CREATE();
                var listAttributes = new List<DataElement>();
                DataElement dataElement;
                ImageDisplayFormat = String.Concat(@"STANDARD\",row.ToString( "D"),col.ToString( "D"));
                if ((ImageDisplayFormat.Length%2) != 0) ImageDisplayFormat += ' ';
                dataElement = new DataElement(DICOMTag.ImageDisplayFormatTag,
                                              Encoding.ASCII.GetBytes(ImageDisplayFormat));
                listAttributes.Add(dataElement);
                dataElement = new DataElement(DICOMTag.FilmOrientationTag, Encoding.ASCII.GetBytes(FilmOrientation));
                listAttributes.Add(dataElement);
                dataElement = new DataElement(DICOMTag. FilmSizeIDTag , Encoding.ASCII.GetBytes(FilmSizeID));
                listAttributes.Add(dataElement);
                
                //reference film session
                dataElement = new DataElement(DICOMTag.ReferencedFilmSessionSequenceTag, 0xFFFFFFFFU);
                listAttributes.Add(dataElement);                
                dataElement = new DataElement(DICOMTag.ItemIntroducerTag , 0xFFFFFFFFU);
                listAttributes.Add(dataElement);
                dataElement = new DataElement(DICOMTag.ReferencedSOPClassUIDTag, Encoding.ASCII.GetBytes(SOP.BasicFilmSessionSOPClassUID));
                listAttributes.Add(dataElement);
                dataElement = new DataElement(DICOMTag.ReferencedSOPInstanceUIDTag  , Encoding.ASCII.GetBytes(FilmSessionInstanceUID ));
                listAttributes.Add(dataElement);

                FilmBoxInstanceUID = Ultility.CreateInstanceUID(FilmBoxInstance);
                ncreate.NCREATERequestCommand(_stream, _presentationContextID, SOP.BasicFilmBoxSOPClassUID,
                                              bMessageID, FilmBoxInstanceUID, listAttributes);
                u = new UnparsePDU(_stream, buff);
                if (u.PDUType.Equals(4))
                {
                    var parsedPDU = new PDataTFPDU(u);
                    string ncreateRespondstring = ncreate.NCREATERespondParse(parsedPDU);
                    if (!ncreateRespondstring.Contains("FAILURE"))
                    {
                        Int16 retstatus = Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.StatusTag]);
                        _logger.Info("NCREATE RESPOND SUCCESS!");
                        if (!Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.DataSetTypeTag]).Equals(
                                CommandFieldConst.NoDataSet))
                        {
                            u = new UnparsePDU(_stream, buff);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("Exception", ex);
            }
            return 0;
        }

        Int16 SetImageBox()
        {
            UnparsePDU u;
            var buff = new byte[0];

            try
            {
                var nSet = new N_SET();
                ImageBoxInstanceUID = Ultility.CreateInstanceUID(ImageBoxInstance);
                DataElement dataElement = new DataElement(DICOMTag.ImageBoxPositionTag,BitConverter.GetBytes( 1));
                FileMetaFormat fs = new FileMetaFormat() ;
                string DCMPath=@"C:\x.dcm";
                nSet.NSETRequestCommand(_stream, _presentationContextID, SOP.BasicGrayScaleImageSOPClassUID, bMessageID,
                                        ImageBoxInstanceUID, fs.ReadDCMFile(DCMPath));
                u = new UnparsePDU(_stream, buff);
                if (u.PDUType.Equals(4))
                {
                    var parsedPDU = new PDataTFPDU(u);
                    string nsetRespondstring = nSet.NSETRespondParse(parsedPDU);
                    if (!nsetRespondstring.Contains("FAILURE"))
                    {
                        Int16 retstatus = Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.StatusTag]);
                        _logger.Info("NCREATE RESPOND SUCCESS!");
                        if (!Convert.ToInt16(parsedPDU.PDVContent.DataElementHashTable[DICOMTag.DataSetTypeTag]).Equals(
                            CommandFieldConst.NoDataSet))
                        {
                            u = new UnparsePDU(_stream, buff);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("Exception", ex);
            }
        }

        private void _arTimer_Elapsed(object obj, ElapsedEventArgs args)
        {
        }

        /// <summary>
        /// Cấu hình Log của hệ thống
        /// </summary>
        private static void LogConfig()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName =
                //baseLogDir + "/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
                "${basedir}/MyLog/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
            fileTarget.Layout = "${date:format=HH\\:mm\\:ss}|${level}|${logger}|${stacktrace}|${message}";
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
            LogManager.Configuration = config;
        }
    }
}