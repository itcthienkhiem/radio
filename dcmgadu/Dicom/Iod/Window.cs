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
using System.Text;

namespace ClearCanvas.Dicom.Iod
{
    /// <summary>
    /// Represents a window/level value pair.
    /// </summary>
	public class Window : IEquatable<Window>
    {
		#region Private Members

		private double _width;
		private double _center;

		#endregion

    	/// <summary>
		/// Constructor.
		/// </summary>
		public Window(double width, double center)
		{
			_width = width;
			_center = center;
		}

		public Window(Window window)
			: this(window.Width, window.Center)
		{
		}

    	/// <summary>
		/// Protected constructor.
		/// </summary>
		protected Window()
		{
		}

		public static List<Window> GetWindowCenterAndWidth(IDicomAttributeProvider provider)
		{
			List<Window> windowValues = new List<Window>();
			DicomAttribute windowWidthAttribute;
			DicomAttribute windowCenterAttribute;

			windowCenterAttribute = provider[DicomTags.WindowCenter];
			if (windowCenterAttribute.IsNull || windowCenterAttribute.IsEmpty)
				return windowValues;

			windowWidthAttribute = provider[DicomTags.WindowWidth];
			if (windowWidthAttribute.IsNull || windowWidthAttribute.IsEmpty)
				throw new DicomDataException("Window Center exists without Window Width.");	

			if (windowWidthAttribute.Count != windowCenterAttribute.Count)
				throw new DicomDataException("Number of Window Center and Width entries differ.");

			for (int i = 0; i < windowCenterAttribute.Count; i++)
				windowValues.Add(new Window(windowWidthAttribute.GetFloat64(i, 0), windowCenterAttribute.GetFloat64(i, 0)));
			
			return windowValues;
		}

		public static void SetWindowCenterAndWidth(IDicomAttributeProvider provider, IEnumerable<Window> windowValues)
		{
			StringBuilder widthValues = new StringBuilder();
			StringBuilder centerValues = new StringBuilder();

			foreach (Window value in windowValues)
			{
				if (widthValues.Length > 0)
					widthValues.Append("\\");

				widthValues.AppendFormat("{0:G12}", value.Width);

				if (centerValues.Length > 0)
					centerValues.Append("\\");

				centerValues.AppendFormat("{0:G12}", value.Center);
			}

			if (centerValues.Length != 0 && widthValues.Length != 0)
			{
				provider[DicomTags.WindowCenter].SetStringValue(centerValues.ToString());
				provider[DicomTags.WindowWidth].SetStringValue(widthValues.ToString());
			}
			else
			{
				// Remove the value from the dataset entirely
				provider[DicomTags.WindowCenter] = null;
				provider[DicomTags.WindowWidth] = null;
			}
		}

    	#region Public Properties

		/// <summary>
		/// Gets the window width.
		/// </summary>
		public virtual double Width
        {
            get { return _width; }
			protected set { _width = value; }
        }

		/// <summary>
		/// Gets the window center.
		/// </summary>
		public virtual double Center
        {
            get { return _center; }
			protected set { _center = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets a string representing the window width/center pair.
		/// </summary>
		public override string ToString()
		{
			return String.Format(@"{0:F2}/{1:F2}", _width, _center);
		}

    	#region IEquatable<Window> Members

		public bool Equals(Window other)
		{
			if (other == null)
				return false;

			return _width == other._width && _center == other._center;
		}

		#endregion

    	public override bool Equals(object obj)
    	{
    		if (obj == null)
    			return false;

			return this.Equals(obj as Window);
    	}

    	/// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
		}
		#endregion
	}
}