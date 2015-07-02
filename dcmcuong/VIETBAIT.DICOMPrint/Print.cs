using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ClearCanvas.Dicom.Iod.Modules;
using ClearCanvas.Dicom.Iod.Sequences;
using ClearCanvas.Dicom.Network.Scu;

namespace VietBaIT.DICOM
{
    public class Print
    {
        private readonly Hashtable _hashtableFiles = new Hashtable();
        private readonly string _localAETitle;
        private readonly string _remoteAETitle;
        private readonly string _remoteHost;
        private readonly int _remotePort;
        private readonly VerificationScu _verificationScu = new VerificationScu();
        private string _filmOrientation;
        private string _filmSizeIDItem;
        private string _imageDisplayFormatItem;
        private VerificationResult _verificationResult;
        private AutoResetEvent m_WaitHandle = new AutoResetEvent(false);

        public Print(string RemoteHost, int RemotePort, string RemoteAETitle, string LocalAETitle)
        {
            _remoteHost = RemoteHost;
            _remotePort = RemotePort;
            _remoteAETitle = RemoteAETitle;
            _localAETitle = LocalAETitle;
        }

        public bool isActive(int timeout, ref string ErrMsg)
        {
            try
            {
                _verificationScu.ReadTimeout = _verificationScu.WriteTimeout = timeout;

                StartVerify();
                if (_verificationResult != VerificationResult.Success)
                {
                    ErrMsg = _verificationResult.ToString();
                    return false;
                }
                return true;
            }
            catch (Exception exception)
            {
                ErrMsg = exception.Message;
                return false;
            }
        }

        public void SendtoDicomPrinter(Hashtable htb, int row, int col, int noofcopies, string filmsizeid,
                                       string filmorient, int timeout, ref string newErrMsg)
        {
            try
            {
                _imageDisplayFormatItem = string.Concat(@"STANDARD\", col.ToString(), ",", row.ToString());
                _filmSizeIDItem = filmsizeid;
                _filmOrientation = filmorient;
                var printScu = new PrintScu();
                printScu.ColorMode = ColorMode.Grayscale;
                PrintScu.GetPixelDataDelegate getPixelDataDelegate = GetImageFromFile;
                var list = new List<PrintScu.IPrintItem>();

                for (int rowIdx = 1; rowIdx <= row; rowIdx++)
                {
                    for (int colIdx = 1; colIdx <= col; colIdx++)
                    {

                        {
                            //Load DICOM File 
                            string rc = string.Concat(rowIdx.ToString(), colIdx.ToString());
                            //Tìm file ở hàng i cột j
                            string fileToPrint = htb[rc].ToString();
                            var printItem = new PrintScu.PrintItem(getPixelDataDelegate);
                            printItem.PrintObject = fileToPrint;
                            list.Add(printItem);
                        }
                    }
                }


                PrintScu.CreateFilmBoxDelegate mDelegate = CreateBasicFilmBox;

                var filmSession = new PrintScu.FilmSession(list, mDelegate) {NumberOfCopies = noofcopies};
                printScu.ReadTimeout = timeout;
                printScu.WriteTimeout = timeout;

                printScu.Print(_localAETitle, _remoteAETitle, _remoteHost, _remotePort, filmSession);
                newErrMsg = printScu.ResultStatus.ToString();
            }
            catch (Exception exception)
            {
                newErrMsg = exception.ToString();
            }
        }

        private PrintScu.FilmBox CreateBasicFilmBox(IList<PrintScu.IPrintItem> currentQueue)
        {
            FilmOrientation filmorient;
            Enum.TryParse(_filmOrientation, true, out filmorient);
            var filmBox = new PrintScu.FilmBox(_imageDisplayFormatItem, _filmSizeIDItem) {FilmOrientation = filmorient};

            foreach (DictionaryEntry dictionaryEntry  in  _hashtableFiles)
            {
                var printItem = new PrintScu.PrintItem();
                printItem.PrintObject = dictionaryEntry.Value.ToString();
                currentQueue.Add(printItem);
            }
            return filmBox;
        }

        private void GetImageFromFile(BasicGrayscaleImageSequenceIod imageBox, object fileName)
        {
            imageBox.AddDicomFileValues(fileName.ToString());
        }

        private void StartVerify()
        {
            _verificationScu.BeginVerify(_localAETitle, _remoteAETitle, _remoteHost, _remotePort, VerifyComplete, null);
            m_WaitHandle.WaitOne();
        }

        private void VerifyComplete(IAsyncResult ar)
        {
            //VerificationScu verificationScu = (VerificationScu)ar.AsyncState;
            _verificationResult = _verificationScu.EndVerify(ar);
            m_WaitHandle.Set();
        }
    }
}