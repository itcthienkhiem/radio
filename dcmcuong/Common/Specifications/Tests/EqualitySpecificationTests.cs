#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

#if UNIT_TESTS

#pragma warning disable 1591

using System;
using NUnit.Framework;

namespace ClearCanvas.Common.Specifications.Tests
{
	[TestFixture]
	public class EqualitySpecificationTests : TestsBase
	{
		enum Color
		{
			Red,
			Blue,
			Green
		}


		[Test]
		public void Test_Equal_ValueType()
		{
			EqualSpecification s = new EqualSpecification();
			s.RefValueExpression = new ConstantExpression(1);
			Assert.IsTrue(s.Test(1).Success);
			Assert.IsFalse(s.Test(0).Success);
			Assert.IsFalse(s.Test(null).Success);
		}

		[Test]
		public void Test_Equal_ReferenceType()
		{
			object x = new object();
			object y = new object();

			EqualSpecification s = new EqualSpecification();
			s.RefValueExpression = new ConstantExpression(x);

			Assert.IsTrue(s.Test(x).Success);
			Assert.IsFalse(s.Test(y).Success);
			Assert.IsFalse(s.Test(null).Success);
		}

		[Test]
		public void Test_Equal_Null()
		{
			EqualSpecification s = new EqualSpecification();
			s.RefValueExpression = new ConstantExpression(null);

			Assert.IsTrue(s.Test(null).Success);
			Assert.IsFalse(s.Test(new object()).Success);
			Assert.IsFalse(s.Test(1).Success);
		}

		[Test]
		public void Test_Equal_CoerceTypes()
		{
			EqualSpecification s = new EqualSpecification();
			s.RefValueExpression = new ConstantExpression("1");

			Assert.IsTrue(s.Test(1).Success);
			Assert.IsTrue(s.Test(1.0).Success);
			Assert.IsTrue(s.Test("1").Success);
			Assert.IsFalse(s.Test(null).Success);
			Assert.IsFalse(s.Test(0).Success);
			Assert.IsFalse(s.Test(0.0).Success);
			Assert.IsFalse(s.Test("0").Success);
		}

		[Test]
		// this test is related to bug #5909 - should be able to compare enums to strings
		public void Test_Equal_CoerceEnum()
		{
			EqualSpecification s = new EqualSpecification();

			s.RefValueExpression = new ConstantExpression("Blue");
			Assert.IsTrue(s.Test(Color.Blue).Success);
			Assert.IsFalse(s.Test(null).Success);
			Assert.IsFalse(s.Test(Color.Red).Success);

			s.RefValueExpression = new ConstantExpression(Color.Red);
			Assert.IsTrue(s.Test(Color.Red).Success);
			Assert.IsTrue(s.Test("Red").Success);
			Assert.IsFalse(s.Test(null).Success);
			Assert.IsFalse(s.Test("").Success);
			Assert.IsFalse(s.Test(Color.Blue).Success);
		}

		[Test]
		public void Test_Equal_Strict()
		{
			EqualSpecification s = new EqualSpecification();
			s.RefValueExpression = new ConstantExpression(1.0);
			s.Strict = true;

			// this should fail because in strict mode we don't do type coercion,
			// and Object.Equals(x, y) returns false when comparing different types
			Assert.IsFalse(s.Test(1).Success);
		}


		[Test]
		public void Test_NotEqual_ValueType()
		{
			NotEqualSpecification s = new NotEqualSpecification();
			s.RefValueExpression = new ConstantExpression(1);
			Assert.IsFalse(s.Test(1).Success);
			Assert.IsTrue(s.Test(0).Success);
			Assert.IsTrue(s.Test(null).Success);
		}

		[Test]
		public void Test_NotEqual_ReferenceType()
		{
			object x = new object();
			object y = new object();

			NotEqualSpecification s = new NotEqualSpecification();
			s.RefValueExpression = new ConstantExpression(x);

			Assert.IsFalse(s.Test(x).Success);
			Assert.IsTrue(s.Test(y).Success);
			Assert.IsTrue(s.Test(null).Success);
		}

		[Test]
		public void Test_NotEqual_Null()
		{
			NotEqualSpecification s = new NotEqualSpecification();
			s.RefValueExpression = new ConstantExpression(null);

			Assert.IsFalse(s.Test(null).Success);
			Assert.IsTrue(s.Test(new object()).Success);
			Assert.IsTrue(s.Test(1).Success);
		}

		[Test]
		// This test is currently failing because coercion code hasn't been merged to trunk yet
		public void Test_NotEqual_CoerceTypes()
		{
			NotEqualSpecification s = new NotEqualSpecification();
			s.RefValueExpression = new ConstantExpression("1");

			Assert.IsFalse(s.Test(1).Success);
			Assert.IsFalse(s.Test(1.0).Success);
			Assert.IsFalse(s.Test("1").Success);

			Assert.IsTrue(s.Test(null).Success);
			Assert.IsTrue(s.Test(0).Success);
			Assert.IsTrue(s.Test(0.0).Success);
			Assert.IsTrue(s.Test("0").Success);
		}

		[Test]
		public void Test_NotEqual_Strict()
		{
			NotEqualSpecification s = new NotEqualSpecification();
			s.RefValueExpression = new ConstantExpression(1.0);
			s.Strict = true;

			// this should pass because in strict mode we don't do type coercion,
			// and Object.Equals(x, y) returns false when comparing different types
			Assert.IsTrue(s.Test(1).Success);
		}

	}
}

#endif
