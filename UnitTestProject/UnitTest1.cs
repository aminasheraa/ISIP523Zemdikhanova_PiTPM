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

    }
}
