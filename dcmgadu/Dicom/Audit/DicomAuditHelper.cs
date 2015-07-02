#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ClearCanvas.Dicom.Network;
using ClearCanvas.Dicom.Network.Scu;

namespace ClearCanvas.Dicom.Audit
{
	/// <summary>
	/// Base class for Audit helpers.
	/// </summary>
	public abstract class DicomAuditHelper
	{
		#region Static Members
		private static string _processId;
		private static string _processIpAddress;
		private static readonly object _syncLock = new object();
		private static string _application;
		private static string _processName;
		private static string _operation;
		#endregion

		#region Members
		private readonly AuditMessage _message = new AuditMessage();
		protected readonly List<AuditMessageActiveParticipant> _participantList = new List<AuditMessageActiveParticipant>(3);
		protected readonly List<AuditSourceIdentificationType> _auditSourceList = new List<AuditSourceIdentificationType>(1);
		protected readonly Dictionary<string, AuditParticipantObject> _participantObjectList = new Dictionary<string, AuditParticipantObject>();
		#endregion

		#region Constructors
		public DicomAuditHelper(string operation)
		{
			_operation = operation;
		}
		#endregion

		#region Static Properties
		public static string ProcessIpAddress
		{
			get
			{
				lock (_syncLock)
				{
					if (_processIpAddress == null)
					{
						string hostName = Dns.GetHostName();
						IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
						foreach (IPAddress ip in ipAddresses)
						{
							if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
							{
								_processIpAddress = ip.ToString();
							}
							else if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
							{
								_processIpAddress = ip.ToString();
							}
						}
					}
					return _processIpAddress;
				}
			}
		}

		public static string ProcessName
		{
			get
			{
				lock (_syncLock)
				{
					if (_processName == null) _processName = Process.GetCurrentProcess().ProcessName;
					return _processName;
				}
			}
		
		}

		public static string ProcessId
		{
			get
			{
				lock (_syncLock)
				{
					if (_processId == null) _processId = Process.GetCurrentProcess().Id.ToString();
					return _processId;
				}
			}

		}
		public static string Application
		{
			get
			{
				lock (_syncLock)
					return _application;
			}
			set
			{
				lock (_syncLock)
				{
					_application = value;
				}
			}
		}
		#endregion

		#region Properties
		protected AuditMessage AuditMessage
		{
			get { return _message; }
		}

		public string Operation
		{
			get { return _operation; }
		}
		#endregion


		#region Public Methods
		public bool Verify(out string failureMessage)
		{
			XmlSchema schema;

			using (Stream stream = GetType().Assembly.GetManifestResourceStream(GetType(), "DicomAuditMessageSchema.xsd"))
			{
				if (stream == null)
					throw new DicomException("Unable to load script resource (is the script an embedded resource?): " + "DicomAuditMessageSchema.xsd");

				schema = XmlSchema.Read(stream, null);
			}
	
			try
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.Schemas = new XmlSchemaSet();
				xmlReaderSettings.Schemas.Add(schema);
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;

				XmlReader xmlReader = XmlTextReader.Create(new StringReader(Serialize()), xmlReaderSettings);
				while (xmlReader.Read()) ;
				xmlReader.Close();
			}
			catch (Exception e)
			{
				failureMessage = e.Message;
				return false;
			}

			failureMessage = string.Empty;
			return true;
		}

		public string Serialize()
		{
			return Serialize(false);
		}

		public string Serialize(bool format)
		{
			AuditMessage.ActiveParticipant = _participantList.ToArray();
			AuditMessage.AuditSourceIdentification = _auditSourceList.ToArray();

			List<ParticipantObjectIdentificationType> list = new List<ParticipantObjectIdentificationType>(_participantObjectList.Values.Count);
			foreach (AuditParticipantObject o in _participantObjectList.Values)
			{
				list.Add(new ParticipantObjectIdentificationType(o));
			}
			AuditMessage.ParticipantObjectIdentification = list.ToArray();

			TextWriter tw = new StringWriter();
			
			XmlWriterSettings settings = new XmlWriterSettings();

			settings.Encoding = Encoding.UTF8;
			if (format)
			{
				settings.NewLineOnAttributes = false;
				settings.Indent = true;
				settings.IndentChars = "  ";
			}
			else
			{
				settings.NewLineOnAttributes = false;
				settings.Indent = false;
			}
			XmlWriter writer = XmlWriter.Create(tw,settings);

			XmlSerializer serializer = new XmlSerializer(typeof(AuditMessage));
			serializer.Serialize(writer, AuditMessage);
			return tw.ToString();
		}
		#endregion

		#region Protected Methods
	

		protected void InternalAddActiveDicomParticipant(AssociationParameters parms)
		{
			if (parms is ClientAssociationParameters)
			{
				_participantList.Add(
					new AuditMessageActiveParticipant(CodedValueType.Source, "AETITLE=" + parms.CallingAE, null, null,
													  parms.LocalEndPoint.Address.ToString(), NetworkAccessPointTypeEnum.IpAddress, null));
				_participantList.Add(
					new AuditMessageActiveParticipant(CodedValueType.Destination, "AETITLE=" + parms.CalledAE, null, null,
													  parms.RemoteEndPoint.Address.ToString(), NetworkAccessPointTypeEnum.IpAddress, null));
			}
			else
			{
				_participantList.Add(
					new AuditMessageActiveParticipant(CodedValueType.Source, "AETITLE=" + parms.CallingAE, null, null,
													  parms.RemoteEndPoint.Address.ToString(), NetworkAccessPointTypeEnum.IpAddress, null));
				_participantList.Add(
					new AuditMessageActiveParticipant(CodedValueType.Destination, "AETITLE=" + parms.CalledAE, null,null,
													  parms.LocalEndPoint.Address.ToString(), NetworkAccessPointTypeEnum.IpAddress, null));
			}
		}

		protected void InternalAddActiveDicomParticipant(string sourceAE, string sourceHost, string destinationAE, string destinationHost)
		{
			IPAddress x;
			_participantList.Add(new AuditMessageActiveParticipant(CodedValueType.Source, "AETITLE=" + sourceAE, null, null,
				sourceHost, IPAddress.TryParse(sourceHost, out x) ? NetworkAccessPointTypeEnum.IpAddress : NetworkAccessPointTypeEnum.MachineName, null));
			_participantList.Add(new AuditMessageActiveParticipant(CodedValueType.Destination, "AETITLE=" + destinationAE, null, null,
				destinationHost, IPAddress.TryParse(destinationHost, out x) ? NetworkAccessPointTypeEnum.IpAddress : NetworkAccessPointTypeEnum.MachineName, null));
		}

		protected void InternalAddAuditSource(DicomAuditSource auditSource)
		{
			_auditSourceList.Add(new AuditSourceIdentificationType(auditSource));
		}
		
		protected void InternalAddParticipantObject(string key, AuditParticipantObject study)
		{
			_participantObjectList.Add(key, study);
		}

		protected void InternalAddActiveParticipant(AuditActiveParticipant participant)

		{
			_participantList.Add(new AuditMessageActiveParticipant(participant));
		}
		#endregion

		protected void InternalAddStorageInstance(StorageInstance instance)
		{
			if (_participantObjectList.ContainsKey(instance.StudyInstanceUid))
			{
				AuditStudyParticipantObject study = _participantObjectList[instance.StudyInstanceUid] as AuditStudyParticipantObject;

				if (study!=null)
				{
					study.AddStorageInstance(instance);
				}
			}
			else
			{
				AuditStudyParticipantObject o = new AuditStudyParticipantObject(instance.StudyInstanceUid);
				o.AddStorageInstance(instance);
				_participantObjectList.Add(instance.StudyInstanceUid, o);
			}
		}
	}
}
