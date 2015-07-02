#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Runtime.Serialization;

namespace ClearCanvas.Dicom.ServiceModel.Query
{
	public interface IIdentifier
	{
		[DicomField(DicomTags.SpecificCharacterSet)]
		string SpecificCharacterSet { get; }

		[DicomField(DicomTags.RetrieveAeTitle)]
		string RetrieveAeTitle { get; }

		[DicomField(DicomTags.InstanceAvailability)]
		string InstanceAvailability { get; }
	}

	/// <summary>
	/// Base class for Dicom query Identifiers.
	/// </summary>
	[DataContract(Namespace = QueryNamespace.Value)]
	public abstract class Identifier : IIdentifier
	{
		#region Private Fields

		private string _specificCharacterSet = "";
		private string _retrieveAeTitle = "";
		private string _instanceAvailability = "";

		#endregion

		#region Internal Constructors

		internal Identifier()
		{
		}

		internal Identifier(IIdentifier other)
		{
			SpecificCharacterSet = other.SpecificCharacterSet;
			InstanceAvailability = other.InstanceAvailability;
			RetrieveAeTitle = other.RetrieveAeTitle;
		}

		internal Identifier(DicomAttributeCollection attributes)
		{
			Initialize(attributes);
		}

		#endregion

		internal void Initialize(DicomAttributeCollection attributes)
		{
			attributes.LoadDicomFields(this);
		}

		#region Public Properties

		/// <summary>
		/// Gets the level of the query.
		/// </summary>
		public abstract string QueryRetrieveLevel { get; }

		/// <summary>
		/// Gets or sets the Specific Character set of the identified instance.
		/// </summary>
		[DicomField(DicomTags.SpecificCharacterSet)] //only include in rq when set explicitly.
		[DataMember(IsRequired = false)]
		public string SpecificCharacterSet
		{
			get { return _specificCharacterSet; }
			set { _specificCharacterSet = value; }
		}

		/// <summary>
		/// Gets or sets the AE Title the identified instance can be retrieved from.
		/// </summary>
		[DicomField(DicomTags.RetrieveAeTitle)]
		[DataMember(IsRequired = false)]
		public string RetrieveAeTitle
		{
			get { return _retrieveAeTitle; }
			set { _retrieveAeTitle = value; }
		}

		/// <summary>
		/// Gets or sets the availability of the identified instance.
		/// </summary>
		[DicomField(DicomTags.InstanceAvailability)]
		[DataMember(IsRequired = false)]
		public string InstanceAvailability
		{
			get { return _instanceAvailability; }
			set { _instanceAvailability = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Converts this object into a <see cref="DicomAttributeCollection"/>.
		/// </summary>
		public DicomAttributeCollection ToDicomAttributeCollection()
		{
			DicomAttributeCollection attributes = new DicomAttributeCollection();
			if (!string.IsNullOrEmpty(_specificCharacterSet))
				attributes.SpecificCharacterSet = _specificCharacterSet;

			attributes[DicomTags.SpecificCharacterSet] = null;
			attributes[DicomTags.QueryRetrieveLevel].SetStringValue(QueryRetrieveLevel);

			attributes.SaveDicomFields(this);

			return attributes;
		}

		/// <summary>
		/// Factory method to create an <see cref="Identifier"/> of type <typeparamref name="T"/> from
		/// the given <see cref="DicomAttributeCollection"/>.
		/// </summary>
		public static T FromDicomAttributeCollection<T>(DicomAttributeCollection attributes) where T : Identifier, new()
		{
			T identifier = new T();
			identifier.Initialize(attributes);
			return identifier;
		}

		#endregion
	}
}
