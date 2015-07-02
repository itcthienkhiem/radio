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
using ClearCanvas.Dicom.Iod.Macros;
using ClearCanvas.Dicom.Utilities;

namespace ClearCanvas.Dicom.Iod.Modules
{
	/// <summary>
	/// GeneralStudy Module
	/// </summary>
	/// <remarks>As defined in the DICOM Standard 2008, Part 3, Section C.7.2.1 (Table C.7-3)</remarks>
	public class GeneralStudyModuleIod : IodBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralStudyModuleIod"/> class.
		/// </summary>	
		public GeneralStudyModuleIod() : base() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneralStudyModuleIod"/> class.
		/// </summary>
		public GeneralStudyModuleIod(IDicomAttributeProvider dicomAttributeProvider) : base(dicomAttributeProvider) { }

		/// <summary>
		/// Gets or sets the value of StudyInstanceUid in the underlying collection.
		/// </summary>
		public string StudyInstanceUid
		{
			get { return base.DicomAttributeProvider[DicomTags.StudyInstanceUid].GetString(0, string.Empty); }
			set { base.DicomAttributeProvider[DicomTags.StudyInstanceUid].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of StudyDate and StudyTime in the underlying collection.
		/// </summary>
		public DateTime? StudyDateTime
		{
			get
			{
				string date = base.DicomAttributeProvider[DicomTags.StudyDate].GetString(0, string.Empty);
				string time = base.DicomAttributeProvider[DicomTags.StudyTime].GetString(0, string.Empty);
				return DateTimeParser.ParseDateAndTime(string.Empty, date, time);
			}
			set
			{
				DicomAttribute date = base.DicomAttributeProvider[DicomTags.StudyDate];
				DicomAttribute time = base.DicomAttributeProvider[DicomTags.StudyTime];
				DateTimeParser.SetDateTimeAttributeValues(value, date, time);
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferringPhysiciansName in the underlying collection.
		/// </summary>
		public string ReferringPhysiciansName
		{
			get { return base.DicomAttributeProvider[DicomTags.ReferringPhysiciansName].GetString(0, string.Empty); }
			set { base.DicomAttributeProvider[DicomTags.ReferringPhysiciansName].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of ReferringPhysicianIdentificationSequence in the underlying collection.
		/// </summary>
		public PersonIdentificationMacro ReferringPhysicianIdentificationSequence
		{
			get
			{
				if (base.DicomAttributeProvider[DicomTags.ReferringPhysicianIdentificationSequence].Count == 0)
				{
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[]) base.DicomAttributeProvider[DicomTags.ReferringPhysicianIdentificationSequence].Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomAttributeProvider[DicomTags.ReferringPhysicianIdentificationSequence] = null;
					return;
				}
				base.DicomAttributeProvider[DicomTags.ReferringPhysicianIdentificationSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of StudyId in the underlying collection.
		/// </summary>
		public string StudyId
		{
			get { return base.DicomAttributeProvider[DicomTags.StudyId].GetString(0, string.Empty); }
			set { base.DicomAttributeProvider[DicomTags.StudyId].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of AccessionNumber in the underlying collection.
		/// </summary>
		public string AccessionNumber
		{
			get { return base.DicomAttributeProvider[DicomTags.AccessionNumber].GetString(0, string.Empty); }
			set { base.DicomAttributeProvider[DicomTags.AccessionNumber].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of StudyDescription in the underlying collection.
		/// </summary>
		public string StudyDescription
		{
			get { return base.DicomAttributeProvider[DicomTags.StudyDescription].GetString(0, string.Empty); }
			set { base.DicomAttributeProvider[DicomTags.StudyDescription].SetString(0, value); }
		}

		/// <summary>
		/// Gets or sets the value of PhysiciansOfRecord in the underlying collection.
		/// </summary>
		public string PhysiciansOfRecord
		{
			get { return base.DicomAttributeProvider[DicomTags.PhysiciansOfRecord].ToString(); }
			set { base.DicomAttributeProvider[DicomTags.PhysiciansOfRecord].SetStringValue(value); }
		}

		/// <summary>
		/// Gets or sets the value of PhysiciansOfRecordIdentificationSequence in the underlying collection.
		/// </summary>
		public PersonIdentificationMacro PhysiciansOfRecordIdentificationSequence
		{
			get
			{
				if (base.DicomAttributeProvider[DicomTags.PhysiciansOfRecordIdentificationSequence].Count == 0)
				{
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[]) base.DicomAttributeProvider[DicomTags.PhysiciansOfRecordIdentificationSequence].Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomAttributeProvider[DicomTags.PhysiciansOfRecordIdentificationSequence] = null;
					return;
				}
				base.DicomAttributeProvider[DicomTags.PhysiciansOfRecordIdentificationSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of NameOfPhysiciansReadingStudy in the underlying collection.
		/// </summary>
		public string NameOfPhysiciansReadingStudy
		{
			get { return base.DicomAttributeProvider[DicomTags.NameOfPhysiciansReadingStudy].ToString(); }
			set { base.DicomAttributeProvider[DicomTags.NameOfPhysiciansReadingStudy].SetStringValue(value); }
		}

		/// <summary>
		/// Gets or sets the value of PhysiciansReadingStudyIdentificationSequence in the underlying collection.
		/// </summary>
		public PersonIdentificationMacro PhysiciansReadingStudyIdentificationSequence
		{
			get
			{
				if (base.DicomAttributeProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence].Count == 0)
				{
					return null;
				}
				return new PersonIdentificationMacro(((DicomSequenceItem[]) base.DicomAttributeProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence].Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomAttributeProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence] = null;
					return;
				}
				base.DicomAttributeProvider[DicomTags.PhysiciansReadingStudyIdentificationSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of ReferencedStudySequence in the underlying collection.
		/// </summary>
		public ISopInstanceReferenceMacro ReferencedStudySequence
		{
			get
			{
				if (base.DicomAttributeProvider[DicomTags.ReferencedStudySequence].Count == 0)
				{
					return null;
				}
				return new SopInstanceReferenceMacro(((DicomSequenceItem[]) base.DicomAttributeProvider[DicomTags.ReferencedStudySequence].Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomAttributeProvider[DicomTags.ReferencedStudySequence] = null;
					return;
				}
				base.DicomAttributeProvider[DicomTags.ReferencedStudySequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets or sets the value of ProcedureCodeSequence in the underlying collection.
		/// </summary>
		public CodeSequenceMacro ProcedureCodeSequence
		{
			get
			{
				if (base.DicomAttributeProvider[DicomTags.ProcedureCodeSequence].Count == 0)
				{
					return null;
				}
				return new CodeSequenceMacro(((DicomSequenceItem[]) base.DicomAttributeProvider[DicomTags.ProcedureCodeSequence].Values)[0]);
			}
			set
			{
				if (value == null)
				{
					base.DicomAttributeProvider[DicomTags.ProcedureCodeSequence] = null;
					return;
				}
				base.DicomAttributeProvider[DicomTags.ProcedureCodeSequence].Values = new DicomSequenceItem[] {value.DicomSequenceItem};
			}
		}

		/// <summary>
		/// Gets an enumeration of <see cref="DicomTag"/>s used by this module.
		/// </summary>
		public static IEnumerable<uint> DefinedTags {
			get {
				yield return DicomTags.AccessionNumber;
				yield return DicomTags.NameOfPhysiciansReadingStudy;
				yield return DicomTags.PhysiciansOfRecord;
				yield return DicomTags.PhysiciansOfRecordIdentificationSequence;
				yield return DicomTags.PhysiciansReadingStudyIdentificationSequence;
				yield return DicomTags.ProcedureCodeSequence;
				yield return DicomTags.ReferencedStudySequence;
				yield return DicomTags.ReferringPhysicianIdentificationSequence;
				yield return DicomTags.ReferringPhysiciansName;
				yield return DicomTags.StudyDate;
				yield return DicomTags.StudyTime;
				yield return DicomTags.StudyDescription;
				yield return DicomTags.StudyId;
				yield return DicomTags.StudyInstanceUid;
			}
		}
	}
}