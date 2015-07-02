using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using NLog;
using NLog.Config;
using NLog.Targets;
using VIETBAIT.AASSOCIATE;
using VIETBAIT.DICOM.DIMSE_N;
using VIETBAIT.DICOMHelper;
using VIETBAIT.DICOMPDU;

namespace VIETBAIT.DICOMPrint
{
    public class DICOMPrint
    {
        private readonly Timer _arTimer = new Timer(5000);
        private readonly Logger _logger;

        private readonly Stream _stream;
        private readonly TcpClient tcpClient;
        private byte _presentationContextID = 1;
        private byte bMessageID = 1;

        public DICOMPrint()
        {
            try
            {
                LogConfig();
                _logger = LogManager.GetLogger("DICOM_PRINT");
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

                byte[] sentmsg = nget.CreateNGETRequestCommand(_presentationContextID, Ultility.PrinterSOPClassUID,
                                                               bMessageID, Ultility.PrinterSOPInstanceUID);
                _stream.Write(sentmsg, 0, sentmsg.Length);
                u = new UnparsePDU(_stream, buff);
                if (u.PDUType.Equals(4))
                {
                    var ParsedPDU = new PDataTFPDU(u);
                    //ParsedPDU.PDUItem[DICOMTag.MessageIDBeingRespondedToTag] == bMessageID;
                    if (Convert.ToInt16(ParsedPDU.PDVContent .DataElementHashTable [DICOMTag.StatusTag]).Equals(0))
                    {
                        _logger.Info("NGET RESPOND SUCCESS!");
                        if (!Convert.ToInt16(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.DataSetTypeTag]).Equals(CommandFieldConst.NoDataSet))
                        {
                            u = new UnparsePDU(_stream, buff);
                            ParsedPDU = new PDataTFPDU(u);
                            if (Convert.ToString(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusTag]).Equals("FAILURE"))
                            {
                                _logger.Info("{0}{1}{2}", "Printer failure!", "\r\n", Convert.ToString(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusInforTag]));
                                return 0;
                            }
                            else if (Convert.ToString(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusTag]).Equals("WARNING"))
                            {
                                _logger.Info("{0}{1}{2}", "Printer warning!", "\r\n", Convert.ToString(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusInforTag]));
                                return 1;
                            }
                            else if (Convert.ToString(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusTag]).Equals("NORMAL"))
                            {
                                _logger.Info("{0}{1}{2}", "Printer normal!", "\r\n", Convert.ToString(ParsedPDU.PDVContent.DataElementHashTable[DICOMTag.PrintStatusInforTag]));
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
        int CreateFilmSession()
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                _logger.FatalException("Exception", ex);
            }
            return 0;
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