using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ClearCanvas.Common;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Iod.Iods;
using ClearCanvas.Dicom.Iod.Modules;
using ClearCanvas.Dicom.Network;
using ClearCanvas.Dicom.Utilities.Statistics;
using SubSonic;

namespace VietBaIT.DICOM
{
    public class WorklistScp : IDicomServerHandler
    {
        private static bool _started;
        private static ServerAssociationParameters _staticAssocParameters;
        private AssociationStatisticsRecorder _statsRecorder;
        #region Constructors
        private WorklistScp(DicomServer server, ServerAssociationParameters assoc)
        {
            _statsRecorder = new AssociationStatisticsRecorder(server);
        }
        #endregion

        #region Public Properties

        public static bool Started
        {
            get { return _started; }
        }
        #endregion

        #region Private Methods

        private static void AddPresentationContexts(ServerAssociationParameters assoc)
        {
            byte pcid = assoc.AddPresentationContext(SopClass.ModalityWorklistInformationModelFind);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

        }

        private static DataSet GetPatientList(DateTime pToDate, DateTime pFromDate, string PID, string PatientName, short pLoai)
        {
            DataSet ds;
            StoredProcedure spin;
            string sToDate = pToDate.ToString("dd/MM/yyyy");
            string sFromDate = pFromDate.ToString("dd/MM/yyyy");
            spin = RISLink.DataAccessLayer.SPs.SpGetTestList(sToDate, sFromDate, PID, PatientName, pLoai);
            try
            {
                ds = spin.GetDataSet();
                return ds;

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private static DataTable GetTestList(string pID)
        {
            DataSet ds;
            StoredProcedure spin;
            spin = RISLink.DataAccessLayer.SPs.SpGetTestListFromPid(pID);
            try
            {
                ds = spin.GetDataSet();
                return ds.Tables[0];
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Static Public Methods

        public static void StartListening(string aeTitle, int port)
        {
            if (_started)
                return;

            _staticAssocParameters = new ServerAssociationParameters(aeTitle, new IPEndPoint(IPAddress.Any//Parse("192.168.1.29")
                , port));

            AddPresentationContexts(_staticAssocParameters);

            DicomServer.StartListening(_staticAssocParameters,
                                       (server, assoc) => new WorklistScp(server, assoc));
            _started = true;
        }

        public static void StopListening(int port)
        {
            if (_started)
            {
                DicomServer.StopListening(_staticAssocParameters);
                _started = false;
            }
        }
        #endregion

        #region IDicomServerHandler Members

        void IDicomServerHandler.OnReceiveAssociateRequest(DicomServer server, ServerAssociationParameters association)
        {
            server.SendAssociateAccept(association);
        }

        void IDicomServerHandler.OnReceiveRequestMessage(DicomServer server, ServerAssociationParameters association,
                                                         byte presentationID, DicomMessage message)
        {
            if (message.CommandField == DicomCommandField.CEchoRequest)
            {
                server.SendCEchoResponse(presentationID, message.MessageId, DicomStatuses.Success);
                return;
            }
            ushort id = message.MessageId;
            String studyInstanceUid = null;
            String seriesInstanceUid = null;
            DicomUid sopInstanceUid;
            String patientId = null;
            string patientsName = string.Empty;
            DateTime scheduledProcedureStepStartDate = new DateTime();
            PatientIdentificationModuleIod patientIdentificationModuleIod = new PatientIdentificationModuleIod(message.DataSet);
            PatientMedicalModule patientMedicalModule = new PatientMedicalModule(message.DataSet);
            ImagingServiceRequestModule imagingServiceRequestModule = new ImagingServiceRequestModule(message.DataSet);
            ModalityWorklistIod modalityWorklistIod = new ModalityWorklistIod();
            modalityWorklistIod.SetCommonTags();

            DicomSequenceItem[] seq = message.DataSet[DicomTags.ScheduledProcedureStepSequence].Values as DicomSequenceItem[];
            bool patientIdExist = message.DataSet[DicomTags.PatientId].TryGetString(0, out patientId);
            message.DataSet[DicomTags.PatientsName].TryGetString(0, out patientsName);
            if (seq != null)
            {
                bool ok = seq[0][DicomTags.ScheduledProcedureStepStartDate].TryGetDateTime(0, out scheduledProcedureStepStartDate);
                

                if (!(ok|patientIdExist))
                {
                    Platform.Log(LogLevel.Error, "Unable to retrieve UIDs from request message, sending failure status.");

                    server.SendCFindCancelRequest(presentationID, message.MessageId);
                    return;
                }
            }

            message.DataSet[DicomTags.SpecificCharacterSet].SetStringValue("ISO_IR 100");
            message.DataSet.RemoveAttribute(DicomTags.ReferencedStudySequence);

            DataSet ds = GetPatientList(scheduledProcedureStepStartDate, scheduledProcedureStepStartDate, patientId, patientsName, 1);
            if (ds != null)
            {
                
                foreach (DataRow _row in ds.Tables[0].Rows)
                {
                    message.CommandSet.RemoveAttribute(DicomTags.MessageId);
                    Platform.Log(LogLevel.Debug, "Test", _row);
                    message.DataSet[DicomTags.PatientId].SetStringValue(_row["Patient_ID"].ToString());
                    message.DataSet[DicomTags.PatientsName].SetStringValue(_row["Nosign_name"].ToString());
                    message.DataSet[DicomTags.AccessionNumber].SetStringValue(_row["Barcode"].ToString());
                    if(_row["Sex"].ToString() == "1")
                    {
                        message.DataSet[DicomTags.PatientsSex].SetStringValue("M");
                    }
                    else
                    {
                        message.DataSet[DicomTags.PatientsSex].SetStringValue("F");
                    }
                    message.DataSet[DicomTags.PatientsBirthDate].SetNullValue();
                    message.DataSet[DicomTags.PatientsAge].SetStringValue(_row["AGE"].ToString());

                    //message.DataSet[DicomTags.RequestedProcedureId].SetStringValue("0000018705");
                    message.DataSet[DicomTags.StudyInstanceUid].SetUid(0, DicomUid.GenerateUid());

                    seq[0].RemoveAttribute(DicomTags.ScheduledProtocolCodeSequence);

                    //message.DataSet[DicomTags.ScheduledProcedureStepSequence].SetEmptyValue();
                    seq[0][DicomTags.Modality].SetStringValue("CR");
                    seq[0][DicomTags.ScheduledStationAeTitle].SetStringValue("VIETBAIT");

                    seq[0][DicomTags.ScheduledProcedureStepStartDate].SetDateTime(0, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                    seq[0][DicomTags.ScheduledProcedureStepStartTime].SetNullValue();
                    
                    DataTable tblTestList = GetTestList(_row["PID"].ToString());
                    string _testListString = string.Empty;
                    foreach (DataRow testListRow in tblTestList.Rows)
                    {
                       _testListString = string.Concat(_testListString, testListRow["CodeMeaning"].ToString(),"\r\n");
                    }
                    seq[0][DicomTags.ScheduledProcedureStepDescription].SetStringValue(_testListString);
                    seq[0].RemoveAttribute(DicomTags.ScheduledProcedureStepId);

                    server.SendCFindResponse(presentationID, id, message, DicomStatuses.Pending);
                }
            }                                       

            
            

            //Platform.Log(LogLevel.Info, "Received SOP Instance: {0} for patient {1} in syntax {2}", sopInstanceUid,
            //             patientName, syntax.Name);
            
            
                        
            
            
            DicomMessage noDataSetMessage = new DicomMessage();

            server.SendCFindResponse(presentationID, id, noDataSetMessage, DicomStatuses.Success);
        }

        void IDicomServerHandler.OnReceiveResponseMessage(DicomServer server, ServerAssociationParameters association,
                                                          byte presentationID, DicomMessage message)
        {
            Platform.Log(LogLevel.Error, "Unexpectedly received response mess on server.");

            server.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.UnrecognizedPDU);
        }


        void IDicomServerHandler.OnReceiveReleaseRequest(DicomServer server, ServerAssociationParameters association)
        {
            Platform.Log(LogLevel.Info, "Received association release request from  {0}.", association.CallingAE);
        }

        void IDicomServerHandler.OnReceiveAbort(DicomServer server, ServerAssociationParameters association,
                                                DicomAbortSource source, DicomAbortReason reason)
        {
            Platform.Log(LogLevel.Error, "Unexpected association abort received.");
        }

        void IDicomServerHandler.OnNetworkError(DicomServer server, ServerAssociationParameters association, Exception e)
        {
            Platform.Log(LogLevel.Error, e, "Unexpected network error over association from {0}.", association.CallingAE);
        }

        void IDicomServerHandler.OnDimseTimeout(DicomServer server, ServerAssociationParameters association)
        {
            Platform.Log(LogLevel.Info, "Received DIMSE Timeout, continuing listening for messages");
        }

        #endregion
    }
    
    internal class PatientInformation
    {
        SqlConnection gv_WLConn = new SqlConnection();
        Boolean spGetPatientInfo(String patientId,String pPatientName,String pFromDate,String pToDate,String pLoai, DataTable dt)
        {
            //SqlCommand cmd = new SqlCommand("spGetTestList",gv_WLConn);
            //cmd.CommandText = ;

            return true;
        }
    
    }
}
