#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Collections.Generic;
using System.ServiceModel;

namespace ClearCanvas.Dicom.ServiceModel.Query
{
	//NOTE: internal for now because we don't actually implement it anywhere.

	[ServiceContract(SessionMode = SessionMode.NotAllowed, Namespace = QueryNamespace.Value)]
	internal interface IPatientRootQuery
	{
		[FaultContract(typeof(DataValidationFault))]
		[FaultContract(typeof(QueryFailedFault))]
		[OperationContract(IsOneWay = false)]
		IList<PatientRootPatientIdentifier> PatientQuery(PatientRootPatientIdentifier queryCriteria);

		[FaultContract(typeof(DataValidationFault))]
		[FaultContract(typeof(QueryFailedFault))]
		[OperationContract(IsOneWay = false)]
		IList<PatientRootStudyIdentifier> StudyQuery(PatientRootStudyIdentifier queryCriteria);

		[FaultContract(typeof(DataValidationFault))]
		[FaultContract(typeof(QueryFailedFault))]
		[OperationContract(IsOneWay = false)]
		IList<SeriesIdentifier> SeriesQuery(SeriesIdentifier queryCriteria);

		[FaultContract(typeof(DataValidationFault))]
		[FaultContract(typeof(QueryFailedFault))]
		[OperationContract(IsOneWay = false)]
		IList<ImageIdentifier> ImageQuery(ImageIdentifier queryCriteria);
	}
}
