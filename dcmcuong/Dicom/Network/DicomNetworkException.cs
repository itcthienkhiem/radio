#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;

namespace ClearCanvas.Dicom.Network
{
	[Serializable]
    public class DicomNetworkException : DicomException
    {
        public DicomNetworkException(String desc)
            : base(desc)
        {
        }
    }
}
