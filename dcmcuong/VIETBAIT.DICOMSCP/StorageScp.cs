#region License

// Copyright (c) 2009, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Network;
using SubSonic;
using VietBaIT.CommonLibrary;
using VietBaIT.RISLink.DataAccessLayer;

namespace VIETBAIT.DICOMSCP
{
    /// <summary>
    /// DICOM Storage SCP Sample Application
    /// </summary>
    internal class StorageScp : IDicomServerHandler
    {
        #region Attributies

        private static bool _started;
        private static ServerAssociationParameters _staticAssocParameters;
        private ServerAssociationParameters _assocParameters;

        #endregion

        #region Constructors

        private StorageScp(ServerAssociationParameters assoc)
        {
            _assocParameters = assoc;
        }

        #endregion

        #region Public Properties

        public static bool Started
        {
            get { return _started; }
        }

        /// <summary>
        /// The path (directory) to store incoming images.
        /// </summary>
        public static String StorageLocation { get; set; }

        #endregion

        #region Private Methods

        private static void AddPresentationContexts(ServerAssociationParameters assoc)
        {
            byte pcid = assoc.AddPresentationContext(SopClass.VerificationSopClass);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.MrImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.CtImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.SecondaryCaptureImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.UltrasoundImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.UltrasoundImageStorageRetired);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.UltrasoundMultiFrameImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.UltrasoundMultiFrameImageStorageRetired);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.NuclearMedicineImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.DigitalIntraOralXRayImageStorageForPresentation);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.DigitalIntraOralXRayImageStorageForProcessing);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.DigitalMammographyXRayImageStorageForPresentation);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.DigitalMammographyXRayImageStorageForProcessing);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.DigitalXRayImageStorageForPresentation);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.DigitalXRayImageStorageForProcessing);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.ComputedRadiographyImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.GrayscaleSoftcopyPresentationStateStorageSopClass);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.KeyObjectSelectionDocumentStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.OphthalmicPhotography16BitImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.OphthalmicPhotography8BitImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VideoEndoscopicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VideoMicroscopicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VideoPhotographicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VlEndoscopicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VlMicroscopicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VlPhotographicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.VlSlideCoordinatesMicroscopicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.XRayAngiographicBiPlaneImageStorageRetired);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.XRayAngiographicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.XRayRadiofluoroscopicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.XRayRadiationDoseSrStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.ChestCadSrStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.XRay3dAngiographicImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.XRay3dCraniofacialImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.EncapsulatedCdaStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.OphthalmicTomographyImageStorage);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);
        }

        private Bitmap CreateBitmap(UInt16[] pixelData, ushort min, ushort max, int width, int height,
                                    string photometricInterpretation)
        {
            //int width = 512, height = 512;
            var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, width, height),
                                          ImageLockMode.ReadOnly, bmp.PixelFormat);

            // This 'unsafe' part of the code populates the bitmap bmp with data stored in pixel16.
            // It does so using pointers, and therefore the need for 'unsafe'. 
            unsafe
            {
                int pixelSize = 3;
                byte b = 0;

                for (int i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*) bmd.Scan0 + (i*bmd.Stride);

                    int j;
                    for (j = 0; j < bmd.Width; ++j)
                    {
                        double lPixval = (double) pixelData[i*width + j]/(max - min)*255;
                        if (lPixval > 255) lPixval = 255;
                        if (lPixval < 0) lPixval = 0;
                        switch (photometricInterpretation)
                        {
                            case "MONOCHROME1":
                                b = (byte) ((byte) (lPixval) ^ 0xFF);
                                break;
                            case "MONOCHROME2":
                                b = (byte) (lPixval);
                                break;
                        }
                        int j1 = j*pixelSize;
                        row[j1] = b; // Red
                        row[j1 + 1] = b; // Green
                        row[j1 + 2] = b; // Blue
                    }
                }
            }
            bmp.UnlockBits(bmd);
            return bmp;
        }

        #endregion

        #region Static Public Methods

        public static void StartListening(string aeTitle, int port)
        {
            if (_started)
                return;
            _staticAssocParameters = new ServerAssociationParameters(aeTitle, new IPEndPoint(IPAddress.Any, port));
            AddPresentationContexts(_staticAssocParameters);
            DicomServer.StartListening(_staticAssocParameters,
                                       delegate(DicomServer server, ServerAssociationParameters assoc) { return new StorageScp(assoc); });
            _started = true;
            VBLogger.LogInfo("Dicom Store Server Running with AETitle:" + aeTitle + " Port:" + port);
        }

        public static void StopListening(int port)
        {
            if (_started)
            {
                DicomServer.StopListening(_staticAssocParameters);
                _started = false;
                VBLogger.LogInfo("Dicom Store Server Stop at Port:" + port);
            }
        }

        #endregion

        #region IDicomServerHandler Members

        void IDicomServerHandler.OnReceiveAssociateRequest(DicomServer server, ServerAssociationParameters association)
        {
            VBLogger.LogDebug("Receive Associate Request from" + association.CallingAE);
            //WriteLog("----------------------------------------------");
            server.SendAssociateAccept(association);
            VBLogger.LogDebug("Send Associate Respose To " + association.CallingAE);
        }

        //private static void WriteLog(string s)
        //{
        //    File.AppendAllText("C:\\_store.txt", string.Format("{0}\n",s));
        //}

        /// <summary>
        /// Hàm xử lý khi nhận xong dữ liệu Dicom
        /// </summary>
        /// <param name="server"></param>
        /// <param name="association"></param>
        /// <param name="presentationId"></param>
        /// <param name="message"></param>
        void IDicomServerHandler.OnReceiveRequestMessage(DicomServer server, ServerAssociationParameters association,
                                                         byte presentationId, DicomMessage message)
        {
            //wc.Stop();
            //WriteLog(wc.ElapsedMilliseconds.ToString());

            if (message.CommandField == DicomCommandField.CEchoRequest)
            {
                server.SendCEchoResponse(presentationId, message.MessageId, DicomStatuses.Success);
                return;
            }

            String studyInstanceUid = null;
            String seriesInstanceUid = null;
            DicomUid sopInstanceUid;
            //String patientName = null;
            String sex = null;
            sex = message.DataSet[DicomTags.PatientsSex].GetString(0, "O");

            bool ok = message.DataSet[DicomTags.SopInstanceUid].TryGetUid(0, out sopInstanceUid);
            if (ok) ok = message.DataSet[DicomTags.SeriesInstanceUid].TryGetString(0, out seriesInstanceUid);
            if (ok) ok = message.DataSet[DicomTags.StudyInstanceUid].TryGetString(0, out studyInstanceUid);
            //if (ok) ok = message.DataSet[DicomTags.PatientsName].TryGetString(0, out patientName);

            if (!ok)
            {
                VBLogger.LogError("Unable to retrieve UIDs from request message, sending failure status.");

                server.SendCStoreResponse(presentationId, message.MessageId, sopInstanceUid.UID,
                                          DicomStatuses.ProcessingFailure);
                return;
            }
            TransferSyntax syntax = association.GetPresentationContext(presentationId).AcceptedTransferSyntax;

            server.SendCStoreResponse(presentationId, message.MessageId,
                                      sopInstanceUid.UID,
                                      DicomStatuses.Success);
            string pathImage = "";
            var path = new StringBuilder();
            path.AppendFormat("{0}{1}{2}{3}{4}", StorageLocation, Path.DirectorySeparatorChar,
                              studyInstanceUid, Path.DirectorySeparatorChar, seriesInstanceUid);

            try
            {
                // Save File
                if (!Directory.Exists(StorageLocation))
                    Directory.CreateDirectory(StorageLocation);
                if (!Directory.Exists(path.ToString()))
                    Directory.CreateDirectory(path.ToString());
                path.AppendFormat("{0}{1}.dcm", Path.DirectorySeparatorChar, sopInstanceUid.UID);

                var dicomFile = new DicomFile(message, path.ToString())
                                    {
                                        TransferSyntaxUid = syntax.UidString,
                                        MediaStorageSopInstanceUid = sopInstanceUid.UID,
                                        ImplementationClassUid = DicomImplementation.ClassUID.UID,
                                        ImplementationVersionName = DicomImplementation.Version,
                                        SourceApplicationEntityTitle = association.CallingAE,
                                        MediaStorageSopClassUid = message.SopClass.Uid
                                    };
                dicomFile.Save(DicomWriteOptions.None);

                //WriteLog(string.Format("Save File OK!: {0}", wc.ElapsedMilliseconds));

                var pd = new DicomUncompressedPixelData(message.DataSet);
                pathImage = path.ToString();
                byte[] thePixels = pd.GetFrame(0);
                var pixData = new UInt16[thePixels.Length/2];
                int h = dicomFile.DataSet[DicomTags.Rows].GetUInt16(0, 0);
                int w = dicomFile.DataSet[DicomTags.Columns].GetUInt16(0, 0);
                UInt16 max = 0, min = UInt16.MaxValue;
                //min = pd.ge
                unsafe
                {
                    fixed (byte* pixPointer = thePixels)
                    {
                        var pP = (UInt16*) pixPointer;
                        for (int i = 0; i < w*h; ++i)
                        {
                            pixData[i] = *pP;
                            if (min > pixData[i]) min = pixData[i];
                            if (max < pixData[i]) max = pixData[i];
                            pP++;
                        }
                    }
                }
                int indexOf = pathImage.LastIndexOf(".dcm");
                pathImage = pathImage.Substring(0, indexOf);
                pathImage = string.Concat(pathImage, "_thumb.jpg");
                string photometricInterpretation;
                message.DataSet[DicomTags.PhotometricInterpretation].TryGetString(0, out photometricInterpretation);
                Bitmap bmp = CreateBitmap(pixData, min, max, w, h, photometricInterpretation);
                bmp.Save(pathImage, ImageFormat.Jpeg);
                bmp.Dispose();
            }
            catch (Exception ex)
            {
                // File.WriteAllText(@"C:\log.txt", ex.ToString());
            }
            //WriteLog(string.Format(" Create Thumbnail OK: {0}", wc.ElapsedMilliseconds));

            String patientName = message.DataSet[DicomTags.PatientsName].GetString(0, "");

            //Xóa các ký tự thừa trong Patient Name
            patientName = patientName.Replace('^', ' ');

            VBLogger.LogInfo("Received SOP Instance: {0} for patient {1}", sopInstanceUid, patientName);


            //Insert Install To DB
            //InsertInstanceToImageServer(association, message.DataSet);

            // FeedBack to Rislink
            string patientId = message.DataSet[DicomTags.PatientId].GetString(0, "");
            string bodyPart = message.DataSet[DicomTags.BodyPartExamined].GetString(0, "");
            string viewPosition = message.DataSet[DicomTags.ViewPosition].GetString(0, "");
            string accessionNumber = message.DataSet[DicomTags.AccessionNumber].GetString(0, "").Trim();
            accessionNumber = accessionNumber == "" ? patientId : accessionNumber;
            //var t = message.DataSet[DicomTags.SourceSerialNumber].GetString(0, "").Trim();
            string requestingAe = association.CallingAE;
            if (viewPosition.Trim() == "")
                viewPosition = message.DataSet[DicomTags.PatientOrientation].GetString(0, "");
            //WriteLog(string.Format("Finish time: {0}",wc.ElapsedMilliseconds));
            var updateOk = SpMergeData(bodyPart, patientId, patientName, path.ToString(), sex, studyInstanceUid, seriesInstanceUid,
                        sopInstanceUid.ToString(), viewPosition, accessionNumber, requestingAe);
            Console.WriteLine(updateOk.ToString(CultureInfo.InvariantCulture));
        }


        // Create the Bitmap object and populate its pixel data with the stored pixel values.

        void IDicomServerHandler.OnReceiveResponseMessage(DicomServer server, ServerAssociationParameters association,
                                                          byte presentationId, DicomMessage message)
        {
            VBLogger.LogError("Unexpectedly received response mess on server.");

            server.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.UnrecognizedPDU);
        }


        void IDicomServerHandler.OnReceiveReleaseRequest(DicomServer server, ServerAssociationParameters association)
        {
            VBLogger.LogInfo("Received association release request from  {0}.", association.CallingAE);
        }

        void IDicomServerHandler.OnReceiveAbort(DicomServer server, ServerAssociationParameters association,
                                                DicomAbortSource source, DicomAbortReason reason)
        {
            VBLogger.LogError("Unexpected association abort received.");
        }

        void IDicomServerHandler.OnNetworkError(DicomServer server, ServerAssociationParameters association, Exception e)
        {
            VBLogger.LogErrorException(e, "Unexpected network error over association from {0}.", association.CallingAE);
        }

        void IDicomServerHandler.OnDimseTimeout(DicomServer server, ServerAssociationParameters association)
        {
            VBLogger.LogInfo("Received DIMSE Timeout, continuing listening for messages");
        }

        #endregion

        #region RisLink FeedBack

        /// <summary>
        /// FeedBack dữ liệu lại cho RIslink
        /// </summary>
        /// <param name="pBodyPart"></param>
        /// <param name="pPatientId"></param>
        /// <param name="pPatientName"> </param>
        /// <param name="pFilePath"></param>
        /// <param name="sex"> </param>
        /// <param name="studyInstanseUid"> </param>
        /// <param name="seriesInstanceUid"> </param>
        /// <param name="sopInstanceUid"> </param>
        /// <param name="viewPosition"> </param>
        /// <param name="accessionNumber"> </param>
        /// <param name="requestingAe"> </param>
        private bool SpMergeData(string pBodyPart, string pPatientId, string pPatientName, string pFilePath, string sex,
                                 string studyInstanseUid, string seriesInstanceUid, string sopInstanceUid,
                                 string viewPosition, string accessionNumber, string requestingAe)
        {
            try
            {
                //B1:Lấy về thông tin bệnh nhân bằng cách kiểm tra pPatientId
                var oBenhNhan = DicomScpHelper.GetBenhNhan(pPatientId, pPatientName, sex);
                //Nếu oBenhNhan==null thì thoát và có lỗi trong quá trình lấy thông tin bệnh nhân.
                if (oBenhNhan == null) return false;

                //B2:Lấy về đối tượng phiếu chỉ định
                var oPhieuCd = DicomScpHelper.GetPhieuChiDinh(oBenhNhan, accessionNumber);
                //Nếu là null thì thoát luôn
                if (oPhieuCd == null) return false;

                //B3:Lấy về đối tượng chỉ định chi tiết
                var oChiTiet = DicomScpHelper.GetPhieuChiDinhChiTiet(oPhieuCd, requestingAe, pBodyPart,
                                                                     viewPosition, studyInstanseUid, seriesInstanceUid, sopInstanceUid);
                //Nếu là null thì thoát luôn
                if (oChiTiet == null) return false;

                //Thực hiện Update kết quả
                var sp = SPs.SpInsertTestResultGT(oChiTiet.IdPhieuCtiet, oBenhNhan.IdBnhan, oChiTiet.IdLoaiDvu,
                                                  DateTime.Now.ToString("dd/MM/yyyy"), oPhieuCd.MaPhieu,
                                                  pBodyPart, pFilePath, pPatientName, studyInstanseUid,
                                                  seriesInstanceUid,
                                                  sopInstanceUid, viewPosition);
                sp.Execute();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion
    }
}