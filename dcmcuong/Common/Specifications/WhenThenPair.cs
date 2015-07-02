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

namespace ClearCanvas.Common.Specifications
{
    internal class WhenThenPair
    {
        private readonly ISpecification _when;
        private readonly ISpecification _then;

        public WhenThenPair(ISpecification when, ISpecification then)
        {
            _when = when;
            _then = then;
        }

        public ISpecification When
        {
            get { return _when; }
        }

        public ISpecification Then
        {
            get { return _then; }
        }
    }
}
