#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using ClearCanvas.Common;
using ClearCanvas.Dicom.Network.Scu;

namespace ClearCanvas.Dicom.Audit
{
	/// <summary>
	/// DICOM Instances Accessed Audit Message Helper
	/// </summary>
	/// <remarks>
	/// This message describes the event of DICOM SOP Instances being viewed, utilized, updated, or deleted.
	/// This event is summarized at the level of studies. This message may only include information about a
	/// single patient.
	/// </remarks>
	public class DicomInstancesAccessedAuditHelper : DicomAuditHelper
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public DicomInstancesAccessedAuditHelper(DicomAuditSource auditSource, EventIdentificationTypeEventOutcomeIndicator outcome,
			EventIdentificationTypeEventActionCode action)
			: base("DicomInstancesAccessed")
		{
			AuditMessage.EventIdentification = new EventIdentificationType();
			AuditMessage.EventIdentification.EventID = CodedValueType.DICOMInstancesAccessed;
			AuditMessage.EventIdentification.EventActionCode = action;
			AuditMessage.EventIdentification.EventActionCodeSpecified = true;
			AuditMessage.EventIdentification.EventDateTime = Platform.Time.ToUniversalTime();
			AuditMessage.EventIdentification.EventOutcomeIndicator = outcome;

			InternalAddAuditSource(auditSource);
		}

		/// <summary>
		/// The identity of the person or process manipulating the data. If both
		/// the person and the process are known, both shall be included.
		/// </summary>
		/// <param name="participant">The participant</param>
		public void AddUser(AuditActiveParticipant participant)
		{
			participant.UserIsRequestor = true;
			InternalAddActiveParticipant(participant);
		}

		/// <summary>
		/// Add details of a Patient.
		/// </summary>
		/// <param name="study"></param>
		public void AddPatientParticipantObject(AuditPatientParticipantObject patient)
		{
			InternalAddParticipantObject(patient.PatientId + patient.PatientsName, patient);
		}

		/// <summary>
		/// Add details of a study.
		/// </summary>
		/// <param name="study"></param>
		public void AddStudyParticipantObject(AuditStudyParticipantObject study)
		{
			InternalAddParticipantObject(study.StudyInstanceUid, study);
		}

		/// <summary>
		/// Add details of images within a study.  SOP Class information is automatically updated.
		/// </summary>
		/// <param name="instance">Descriptive object being audited</param>
		public void AddStorageInstance(StorageInstance instance)
		{
			InternalAddStorageInstance(instance);
		}
	}
}
