using System;
using ISIP523Zemdikhanova_PiTPM.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterTestSuccess()
        {
            var page = new RegistratPage();

            Assert.IsTrue(page.Register("user123", "Password12", "user@mail.com", new DateTime(2000, 1, 1)));
            Assert.IsTrue(page.Register("loginUser", "12345678", "test@gmail.com", new DateTime(1995, 5, 10)));
            Assert.IsTrue(page.Register("maxuser", "123456789012", "max@mail.com", new DateTime(1990, 1, 1)));
        }

        [TestMethod]
        public void RegisterTestFailure()
        {
            var page = new RegistratPage();

            Assert.IsFalse(page.Register("", "", "", null)); // Все поля пустые
            Assert.IsFalse(page.Register("user1", "", "user@mail.com", new DateTime(2000, 1, 1))); // Пустой пароль
            Assert.IsFalse(page.Register("", "12345678", "user@mail.com", new DateTime(2000, 1, 1))); // Пустой логин
            Assert.IsFalse(page.Register("user2", "12345678", "", new DateTime(2000, 1, 1))); // Пустой email
            Assert.IsFalse(page.Register("user3", "12345678", "invalid-email", new DateTime(2000, 1, 1))); // Некорректный email
            Assert.IsFalse(page.Register("user4", "1234", "user@mail.com", new DateTime(2000, 1, 1))); // Короткий пароль
            Assert.IsFalse(page.Register("user5", "12345678", "user@mail.com", null)); // Нет даты рождения
            Assert.IsFalse(page.Register("user6", "12345678", "user@mail.com", DateTime.Now.AddDays(1))); // Дата рождения в будущем
            Assert.IsFalse(page.Register("user7", "123456789012345678901234567890123", "user@mail.com", new DateTime(2000, 1, 1))); // >32 символов
        }
    }
}
