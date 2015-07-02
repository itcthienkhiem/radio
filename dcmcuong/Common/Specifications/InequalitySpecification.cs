#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

#pragma warning disable 1591

using System;

namespace ClearCanvas.Common.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// From MSDN IComparable.CompareTo docs: 
    /// "By definition, any object compares greater than a null reference, and two null references compare equal to each other."
    /// </remarks>
    public abstract class InequalitySpecification : ComparisonSpecification
    {
        private bool _inclusive;
        private readonly int _multiplier;

        internal InequalitySpecification(int multiplier)
        {
            _multiplier = multiplier;
        }

        public bool Inclusive
        {
            get { return _inclusive; }
            set { _inclusive = value; }
        }

        protected override bool CompareValues(object testValue, object refValue)
        {
            // two nulls compare equal
            if (testValue == null && refValue == null)
                return true;

            // if testValue is null, refValue is greater by definition
            if (testValue == null)
                return (_multiplier == -1);

            // if refValue is null, testValue is greater by definition
            if (refValue == null)
                return (_multiplier == 1);

            // need to perform a comparison - ensure IComparable is implemented
            if (!(testValue is IComparable))
                throw new SpecificationException("Test expression does not evaluate to an IComparable object");

            int x = (testValue as IComparable).CompareTo(refValue) * _multiplier;
            return x > 0 || x == 0 && _inclusive;
        }
    }
}
