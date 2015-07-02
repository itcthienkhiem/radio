#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Collections;
using System.Collections.Generic;

namespace ClearCanvas.Common.Utilities
{
    /// <summary>
    /// Utility class used to wrap an untyped <see cref="IEnumerable"/> as a type-safe one.
    /// </summary>
    /// <typeparam name="T">The type of the items to be enumerated.</typeparam>
    public class TypeSafeEnumerableWrapper<T> : IEnumerable<T>
    {
        private IEnumerable _inner;

        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="inner">The untyped <see cref="IEnumerable"/> object to wrap.</param>
		public TypeSafeEnumerableWrapper(IEnumerable inner)
        {
            _inner = inner;
        }

        #region IEnumerable<T> Members

		/// <summary>
		/// Gets an <see cref="IEnumerator{T}"/> for the wrapped object.
		/// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new TypeSafeEnumeratorWrapper<T>(_inner.GetEnumerator());
        }

        #endregion

        #region IEnumerable Members

		/// <summary>
		/// Gets an <see cref="IEnumerator"/> for the wrapped object.
		/// </summary>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        #endregion
    }
}
