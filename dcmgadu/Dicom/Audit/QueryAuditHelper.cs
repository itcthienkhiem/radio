#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Net;
using ClearCanvas.Common;
using ClearCanvas.Dicom.Network;
using ClearCanvas.Dicom.Network.Scu;

namespace ClearCanvas.Dicom.Audit
{
	/// <summary>
	/// Query Audit Message Helper
	/// </summary>
	/// <remarks>
	/// <para>
	/// This message describes the event of a Query being issued or received. The message does not record the
	/// response to the query, but merely records the fact that a query was issued. For example, this would report
	/// queries using the DICOM SOP Classes:
	/// <list>
	/// <item>
	/// a. Modality Worklist
	/// </item>
	/// <item>
	/// b. General Purpose Worklist
	/// </item>
	/// <item>
	/// c. Composite Instance Query
	/// </item>
	/// </list>
	/// </para>
	/// <para>
	/// Notes: 1. The response to a query may result in one or more Instances Transferred or Instances Accessed
	/// messages, depending on what events transpire after the query. If there were security-related failures,
	/// such as access violations, when processing a query, those failures should show up in other audit
	/// messages, such as a Security Alert message.
	/// </para>
	/// <para>
	/// 2. Non-DICOM queries may also be captured by this message. The Participant Object ID Type Code,
	/// the Participant Object ID, and the Query fields may have values related to such non-DICOM queries.
	/// </para>
	/// </remarks>
	public class QueryAuditHelper : DicomAuditHelper
	{
		public QueryAuditHelper(DicomAuditSource auditSource,
	EventIdentificationTypeEventOutcomeIndicator outcome,
	AuditActiveParticipant sourceUser, string destinationAE, string destinationHost, string queryParameters)
			: base("Query")
		{
			AuditMessage.EventIdentification = new EventIdentificationType
			{
				EventID = CodedValueType.Query,
				EventActionCode = EventIdentificationTypeEventActionCode.E,
				EventActionCodeSpecified = true,
				EventDateTime = Platform.Time.ToUniversalTime(),
				EventOutcomeIndicator = outcome
			};

			InternalAddAuditSource(auditSource);

			InternalAddActiveParticipant(sourceUser);

			IPAddress x;
			_participantList.Add(new AuditMessageActiveParticipant(CodedValueType.Destination, "AETITLE=" + destinationAE, null,
			                                                       null,
			                                                       destinationHost,
			                                                       IPAddress.TryParse(destinationHost, out x)
			                                                       	? NetworkAccessPointTypeEnum.IpAddress
			                                                       	: NetworkAccessPointTypeEnum.MachineName, null));

			AuditQueryMessageParticipantObject o =
				new AuditQueryMessageParticipantObject(queryParameters);

			InternalAddParticipantObject("Query", o);
			
		}

		public QueryAuditHelper(DicomAuditSource auditSource,
			EventIdentificationTypeEventOutcomeIndicator outcome,
			string sourceAE, string sourceHost, string destinationAE, string destinationHost, string sopClassUid, DicomAttributeCollection msg)
			: base("Query")
		{
			AuditMessage.EventIdentification = new EventIdentificationType
			                                   	{
			                                   		EventID = CodedValueType.Query,
			                                   		EventActionCode = EventIdentificationTypeEventActionCode.E,
			                                   		EventActionCodeSpecified = true,
			                                   		EventDateTime = Platform.Time.ToUniversalTime(),
			                                   		EventOutcomeIndicator = outcome
			                                   	};

			InternalAddActiveDicomParticipant(sourceAE, sourceHost, destinationAE, destinationHost);

			InternalAddAuditSource(auditSource);

			AuditQueryMessageParticipantObject o =
				new AuditQueryMessageParticipantObject(sopClassUid, msg);

			InternalAddParticipantObject("Query", o);
		}

		public QueryAuditHelper(DicomAuditSource auditSource,
							EventIdentificationTypeEventOutcomeIndicator outcome,
							AssociationParameters parms, string sopClassUid, DicomAttributeCollection msg)
			: base("Query")
		{
			AuditMessage.EventIdentification = new EventIdentificationType
			{
				EventID = CodedValueType.Query,
				EventActionCode = EventIdentificationTypeEventActionCode.E,
				EventActionCodeSpecified = true,
				EventDateTime = Platform.Time.ToUniversalTime(),
				EventOutcomeIndicator = outcome
			};

			InternalAddActiveDicomParticipant(parms);

			InternalAddAuditSource(auditSource);

			AuditQueryMessageParticipantObject o =
				new AuditQueryMessageParticipantObject(sopClassUid, msg);

			InternalAddParticipantObject("Query", o);
		}

		/// <summary>
		/// The identity of any other participants that might be involved and
		/// known, especially third parties that are the requestor
		/// </summary>
		/// <param name="participant">The participant to add.</param>
		public void AddOtherParticipant(AuditActiveParticipant participant)
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
