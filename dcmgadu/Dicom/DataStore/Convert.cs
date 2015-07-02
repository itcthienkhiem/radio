#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ClearCanvas.Dicom.Utilities;

namespace ClearCanvas.Dicom.DataStore
{
	internal static class Convert
	{
		internal static IEnumerable<T> Cast<T>(IEnumerable original)
		{
			foreach (T item in original)
				yield return item;
		}

		private static string ToString(object value, TypeConverter converter)
		{
			if (value is string)
				return (string)value;
			else
				return converter.ConvertToInvariantString(value);
		}

		public static string[] ToStringArray(object value, TypeConverter converter)
		{
			if (value == null)
				return new string[]{ };

			if (value.GetType().IsArray)
			{
				Array array = value as Array;
				if (array == null)
					return new string[]{ };

				string[] stringArray = new string[array.Length];
				int i = 0;
				foreach (object arrayValue in array)
					stringArray[i++] = ToString(arrayValue, converter);

				return stringArray;
			}
			else if (value is string)
			{
				//Assume strings are (potentially) multi-valued.  If they're not, then this has no effect anyway.
				return DicomStringHelper.GetStringArray(((string)value) ?? "");
			}
			else
			{
				return new string[] { ToString(value, converter) };
			}
		}
	}
}
