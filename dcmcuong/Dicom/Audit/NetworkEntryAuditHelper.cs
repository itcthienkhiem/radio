#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using ClearCanvas.Common;

namespace ClearCanvas.Dicom.Audit
{
	/// <summary>
	/// Enum for use with the <see cref="NetworkEntryAuditHelper"/> class.
	/// </summary>
	public enum NetworkEntryType
	{
		Attach,
		Detach
	}

	/// <summary>
	/// Network Entry Audit Message
	/// </summary>
	/// <remarks>
	/// <para>
	/// This message describes the event of a system, such as a mobile device, entering or leaving the network
	/// as a normal part of operations. It is not intended to report network problems, loose cables, or other
	/// unintentional detach and reattach situations.
	/// </para>
	/// <para>
	/// Note: The machine should attempt to send this message prior to detaching. If this is not possible, it should
	/// retain the message in a local buffer so that it can be sent later. The mobile machine can then capture
	/// audit messages in a local buffer while it is outside the secure domain. When it is reconnected to the
	/// secure domain, it can send the detach message (if buffered), followed by the buffered messages,
	/// followed by a mobile machine message for rejoining the secure domain. The timestamps on these
	/// messages is the time that the event occurred, not the time that the message is sent.
	/// </para>
	/// </remarks>
	public class NetworkEntryAuditHelper : DicomAuditHelper
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="outcome">The outcome of the audit event.</param>
		/// <param name="type">Network Attach or Detach</param>
		/// <param name="node">The identity of the node entering or leaving the network</param>
		/// <param name="auditSource">The source of the audit message.</param>
		public NetworkEntryAuditHelper(DicomAuditSource auditSource, EventIdentificationTypeEventOutcomeIndicator outcome, NetworkEntryType type, AuditProcessActiveParticipant node)
			: base("NetworkEntry")

		{
			AuditMessage.EventIdentification = new EventIdentificationType();
			AuditMessage.EventIdentification.EventID = CodedValueType.NetworkEntry;
			AuditMessage.EventIdentification.EventActionCode = EventIdentificationTypeEventActionCode.E;
			AuditMessage.EventIdentification.EventActionCodeSpecified = true;
			AuditMessage.EventIdentification.EventDateTime = Platform.Time.ToUniversalTime();
			AuditMessage.EventIdentification.EventOutcomeIndicator = outcome;

			if (type == NetworkEntryType.Attach)
				AuditMessage.EventIdentification.EventTypeCode = new CodedValueType[] {CodedValueType.Attach};
			else
				AuditMessage.EventIdentification.EventTypeCode = new CodedValueType[] {CodedValueType.Detach};

			node.UserIsRequestor = false;
			InternalAddActiveParticipant(node);

			InternalAddAuditSource(auditSource);
		}
	}
}
