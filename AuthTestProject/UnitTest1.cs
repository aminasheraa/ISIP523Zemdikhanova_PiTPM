using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISIP523Zemdikhanova_PiTPM.Pages;
using System.Collections.Generic;

namespace AuthTestProject
{
    [TestClass]
    public class UnitTest1
    {
        // пример тестового метода

        /* [TestMethod] 
        public void AuthTest()
        {
            var page = new AuthorisPage();
            Assert.IsTrue(page.Auth("test", "test"));
            Assert.IsFalse(page.Auth("user1", "12345"));
            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth(" ", " "));
        }*/

        [TestMethod]
        public void AuthTestSuccess()
        {
            var page = new AuthorisPage();
            Assert.IsTrue(page.Auth("vaskapiska", "87654321"));
            Assert.IsTrue(page.Auth("VASKAPISKA", "87654321"));
            Assert.IsTrue(page.Auth("newuser", "password"));
            Assert.IsTrue(page.Auth("user2", "PASSWORD"));
            Assert.IsTrue(page.Auth("am", "12345678"));
        }

        [TestMethod]
        public void AuthTestFailure() 
        { 
            var page = new AuthorisPage();
            Assert.IsFalse(page.Auth("", "")); // Оба пустые
            Assert.IsFalse(page.Auth("vaskapiska", "")); // Пустой пароль
            Assert.IsFalse(page.Auth("", "87654321")); // Пустой логин
            Assert.IsFalse(page.Auth("wronguser", "87654321")); // Несуществующий логин
            Assert.IsFalse(page.Auth("vaskapiska", "wrongpass")); // Неверный пароль
            Assert.IsFalse(page.Auth("newuser", "PASSWORD")); // Поле для ввода пароля должно зависеть от регистра
            Assert.IsFalse(page.Auth("vaskapiska", "123")); // Короткий пароль
            Assert.IsFalse(page.Auth("vaskapiska", "123456789012345678901234567890123")); // >32 символов
        }
    }


}
