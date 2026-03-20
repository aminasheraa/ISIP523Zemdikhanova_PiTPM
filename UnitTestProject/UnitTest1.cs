using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISIP523Zemdikhanova_PiTPM.Pages;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int res = 2 + 2;
            Assert.AreEqual(res, 4);
            Assert.AreNotEqual(res, 5);
            Assert.IsFalse(res > 5);
            Assert.IsTrue(res < 5);
        }

        [TestMethod]
        public void Page1_Calculate_ValidValues_ReturnsTrue()
        {
            var page = new Page1();

            bool success = page.Calculate(0.5, 1, 2, out double result);

            Assert.IsTrue(success);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod]
        public void Page1_Calculate_XOutOfRange_ReturnsFalse()
        {
            var page = new Page1();

            bool success = page.Calculate(2, 1, 2, out double result);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page1_Calculate_DivisionByZero_ReturnsFalse()
        {
            var page = new Page1();

            bool success = page.Calculate(0, 0, 0, out double result);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page1_Calculate_CorrectResult_CheckValue()
        {
            var page = new Page1();

            bool success = page.Calculate(0.5, 1, 2, out double result);

            Assert.IsTrue(success);
            Assert.AreEqual(1.846999, result, 0.0001);
        }

        [TestMethod]
        public void Page2_Calculate_Sinh_ReturnsCorrectValue()
        {
            var page = new Page2();

            bool success = page.Calculate(2, 2, 1, out double result);

            Assert.IsTrue(success);
            Assert.AreEqual(Math.Exp(Math.Sinh(2)), result, 0.001);
        }

        [TestMethod]
        public void Page2_Calculate_Pow_ReturnsCorrectValue()
        {
            var page = new Page2();

            bool success = page.Calculate(3, 5, 2, out double result);

            Assert.IsTrue(success);

            double fx = Math.Pow(3, 2);
            double expected = Math.Sqrt(Math.Abs(fx + 4 * 5));

            Assert.AreEqual(expected, result, 0.001);
        }

        [TestMethod]
        public void Page2_Calculate_Exp_ReturnsCorrectValue()
        {
            var page = new Page2();

            bool success = page.Calculate(0.5, 0.5, 3, out double result);

            Assert.IsTrue(success);

            double fx = Math.Exp(0.5);
            double expected = 0.5 * Math.Pow(fx, 2);

            Assert.AreEqual(expected, result, 0.001);
        }

        [TestMethod]
        public void Page2_Calculate_InvalidFunction_ReturnsFalse()
        {
            var page = new Page2();

            bool success = page.Calculate(1, 1, 0, out double result);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void  Page3_Calculate_ValidData_ReturnsTrue()
        {
            var page = new Page3();

            bool success = page.Calculate(1, 3, 1, 2, out var result, out bool hasErrors);

            Assert.IsTrue(success);
            Assert.IsTrue(result.Count > 0);
            Assert.IsFalse(hasErrors);
        }

        [TestMethod]
        public void Page3_Calculate_X0MoreThanXk_ReturnsFalse()
        {
            var page = new Page3();

            bool success = page.Calculate(5, 1, 1, 2, out var result, out bool hasErrors);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page3_Calculate_DxTooBig_ReturnsFalse()
        {
            var page = new Page3();

            bool success = page.Calculate(1, 2, 5, 2, out var result, out bool hasErrors);

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page3_Calculate_NegativeUnderRoot_HasErrorsTrue()
        {
            var page = new Page3();

            bool success = page.Calculate(-5, -1, 1, 0, out var result, out bool hasErrors);

            Assert.IsTrue(success);
            Assert.IsTrue(hasErrors);
        }

        [TestMethod]
        public void Page3_Calculate_AllValuesInvalid_ResultEmpty()
        {
            var page = new Page3();

            bool success = page.Calculate(-5, -1, 1, 0, out var result, out bool hasErrors);

            Assert.IsTrue(success);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Page3_Calculate_CheckCorrectValue()
        {
            var page = new Page3();

            bool success = page.Calculate(1, 2, 1, 1, out var result, out bool hasErrors);

            Assert.IsTrue(success);

            double x = 1;
            double underRoot = Math.Pow(x, 3) + Math.Pow(1, 3);
            double expected = 9 * (x + 15 * Math.Sqrt(underRoot));

            Assert.AreEqual(expected, result[0].y, 0.001);
        }
    }
}
