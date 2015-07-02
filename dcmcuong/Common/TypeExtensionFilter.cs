#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;

namespace ClearCanvas.Common
{
    /// <summary>
    /// Implements an extension filter that performs matching on types.
    /// </summary>
    /// <remarks>
    /// The filter will test true if the extension in question implements all of the
    /// types supplied as criteria to this filter.  Typically these types are interfaces, however, a
    /// single class may be supplied, in which case the extension must be a subclass of that class.
    /// </remarks>
    public class TypeExtensionFilter : ExtensionFilter
    {
        private Type[] _types;

        /// <summary>
        /// Creates a filter that matches on multiple types.
        /// </summary>
        /// <param name="types">The types used as criteria to match.</param>
        public TypeExtensionFilter(Type[] types)
        {
            _types = types;
        }

        /// <summary>
        /// Creates a filter that matches on a single type.
        /// </summary>
        /// <param name="type">The type used as criteria to match.</param>
        public TypeExtensionFilter(Type type)
            : this(new Type[] { type })
        {
        }

        /// <summary>
        /// Checks whether the specified extension implements/subclasses all of the criteria types.
        /// </summary>
        /// <param name="extension">The extension to test.</param>
        /// <returns>True if the test succeeds.</returns>
        public override bool Test(ExtensionInfo extension)
        {
            foreach (Type filterType in _types)
            {
                if (!filterType.IsAssignableFrom(extension.ExtensionClass))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
