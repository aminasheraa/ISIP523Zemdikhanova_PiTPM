using System;
using System.Windows.Input;
using ISIP523Zemdikhanova_PiTPM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class СalculateTests
    {

        [TestMethod]
        public void Calculate_Division_ReturnsCorrectResult()
        {
            var _window = new MainWindow();

            bool success = _window.Calculate(10, 2, 1, out double result, out bool hasErrors);

            Assert.IsTrue(success);
            Assert.IsFalse(hasErrors);
            Assert.AreEqual(5.0, result);
        }

        [TestMethod]
        public void Calculate_Multiplication_ReturnsCorrectResult()
        {
            var _window = new MainWindow();
            bool success = _window.Calculate(4.5, 2, 2, out double result, out bool hasErrors);

            Assert.IsTrue(success);
            Assert.IsFalse(hasErrors);
            Assert.AreEqual(9.0, result);
        }

        [TestMethod]
        public void Calculate_DivisionByZero_ReturnsError()
        {
            var _window = new MainWindow();
            bool success = _window.Calculate(10, 0, 1, out double result, out bool hasErrors);

            Assert.IsFalse(success);
            Assert.IsTrue(hasErrors);
            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public void Calculate_InvalidMode_ReturnsError()
        {
            var _window = new MainWindow();
            bool success = _window.Calculate(5, 5, 99, out double result, out bool hasErrors);

            Assert.IsFalse(success);
            Assert.IsTrue(hasErrors);
        }

        [TestMethod]
        public void Calculate_NegativeNumbers_WorkCorrectly()
        {
            var _window = new MainWindow();
            bool success = _window.Calculate(-8, 4, 1, out double result, out bool hasErrors);
            Assert.IsTrue(success);
            Assert.IsFalse(hasErrors);
            Assert.AreEqual(-2.0, result);
        }
    }
}