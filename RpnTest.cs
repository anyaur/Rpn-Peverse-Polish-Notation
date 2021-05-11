using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rpn.Tests
{
	[TestClass]
	public class RpnTests
	{
		[TestMethod]
		public void TestShortExpression()
		{
			string x = "1+1";
			double expected = 2;
			double actual = Program.RPN.Calculate(x);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestSimpleExpression()
		{
			string x = "(4-2)^2+5*2";
			double expected = 14;
			double actual = Program.RPN.Calculate(x);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestLongExpression()
		{
			string x = "(4-2)^2+5*2+100/2-8^3+0*4-(7+2)*2-8+9^2-10/5+(7-1)^2+((5-6*9)+(5-4))^2";
			double expected = -2699;
			double actual = Program.RPN.Calculate(x);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TestOnlyNumber()
		{
			string x = "5";
			double expected = 5;
			double actual = Program.RPN.Calculate(x);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		[ExpectedException(typeof(System.InvalidOperationException))]
		public void TestWrongBrackets()
		{
			string x = ")1+1(";
			double actual = Program.RPN.Calculate(x);
		}

		[TestMethod]
		[ExpectedException(typeof(System.InvalidOperationException))]
		public void TestEmpty()
		{
			string x = "";
			double actual = Program.RPN.Calculate(x);
		}
	}
}
