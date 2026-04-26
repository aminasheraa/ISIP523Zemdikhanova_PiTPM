using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPR7part2
{
    [TestClass]
    public class EncryptTests
    {
        [TestMethod]
        public void Encrypt_ShouldReturnDifferentText() // шифр не равен изначальному тексту
        {
            string original = "HELLO123";
            string key = "KEY";

            string encrypted = AdfgvxCipher.Encrypt(original, key);

            Assert.AreNotEqual(original, encrypted);
        }

        [TestMethod]
        public void Encrypt_EmptyText_ShouldNotReturnValidResult() // пустой текст
        {
            string key = "KEY";

            string encrypted = AdfgvxCipher.Encrypt("", key);

            Assert.IsTrue(string.IsNullOrEmpty(encrypted));
        }

        [TestMethod]
        public void Encrypt_NullText_ShouldNotReturnValidResult() // null текст
        {
            string key = "KEY";

            string encrypted = AdfgvxCipher.Encrypt(null, key);

            Assert.IsTrue(string.IsNullOrEmpty(encrypted));
        }

        [TestMethod]
        public void Encrypt_EmptyKey_ShouldNotReturnValidResult() // пустой ключ
        {
            string text = "HELLO";

            string encrypted = AdfgvxCipher.Encrypt(text, "");

            Assert.IsTrue(string.IsNullOrEmpty(encrypted));
        }

        [TestMethod]
        public void Encrypt_NullKey_ShouldNotReturnValidResult() // null ключ
        {
            string text = "HELLO";

            string encrypted = AdfgvxCipher.Encrypt(text, null); // ❗ исправил (у тебя было "")

            Assert.IsTrue(string.IsNullOrEmpty(encrypted));
        }

        [TestMethod]
        public void Encrypt_DifferentKeys_ShouldProduceDifferentResults() // ключ влияет на результат
        {
            string text = "HELLO";

            string enc1 = AdfgvxCipher.Encrypt(text, "KEY1");
            string enc2 = AdfgvxCipher.Encrypt(text, "KEY2");

            Assert.AreNotEqual(enc1, enc2);
        }

        [TestMethod]
        public void Encrypt_ShouldBeCaseInsensitive() // регистр не влияет
        {
            string text = "Hello";

            string enc1 = AdfgvxCipher.Encrypt(text, "KEY");
            string enc2 = AdfgvxCipher.Encrypt(text, "key");

            Assert.AreEqual(enc1, enc2);
        }
    }

    [TestClass]
    public class DecryptTests
    {
        [TestMethod]
        public void Decrypt_ShouldNotReturnEncryptedText() // результат не равен шифру
        {
            string text = "HELLO";
            string key = "KEY";

            string encrypted = AdfgvxCipher.Encrypt(text, key);
            string decrypted = AdfgvxCipher.Decrypt(encrypted, key);

            Assert.AreNotEqual(encrypted, decrypted);
        }

        [TestMethod]
        public void Decrypt_EmptyText_ShouldNotReturnValidResult() // пустой шифр
        {
            string key = "KEY";

            string decrypted = AdfgvxCipher.Decrypt("", key);

            Assert.IsTrue(string.IsNullOrEmpty(decrypted));
        }

        [TestMethod]
        public void Decrypt_NullText_ShouldNotReturnValidResult() // null шифр
        {
            string key = "KEY";

            string decrypted = AdfgvxCipher.Decrypt(null, key);

            Assert.IsTrue(string.IsNullOrEmpty(decrypted));
        }

        [TestMethod]
        public void Decrypt_EmptyKey_ShouldNotReturnValidResult() // пустой ключ
        {
            string text = "HELLO";

            string encrypted = AdfgvxCipher.Encrypt(text, "KEY");
            string decrypted = AdfgvxCipher.Decrypt(encrypted, "");

            Assert.IsTrue(string.IsNullOrEmpty(decrypted));
        }

        [TestMethod]
        public void Decrypt_NullKey_ShouldNotReturnValidResult() // null ключ
        {
            string text = "HELLO";

            string encrypted = AdfgvxCipher.Encrypt(text, "KEY");
            string decrypted = AdfgvxCipher.Decrypt(encrypted, null);

            Assert.IsTrue(string.IsNullOrEmpty(decrypted));
        }

        [TestMethod]
        public void Decrypt_WrongKey_ShouldNotReturnOriginal() // неправильный ключ
        {
            string text = "HELLO";
            string key = "KEY";
            string wrongKey = "BAD";

            string encrypted = AdfgvxCipher.Encrypt(text, key);
            string decrypted = AdfgvxCipher.Decrypt(encrypted, wrongKey);

            Assert.AreNotEqual(text, decrypted);
        }

        [TestMethod]
        public void Decrypt_InvalidCipher_ShouldReturnEmptyOrInvalid() // повреждённый шифр
        {
            string key = "KEY";

            string result = AdfgvxCipher.Decrypt("%%%###INVALID###%%%", key);

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void EncryptDecrypt_ShouldBeSymmetric() // симметрия
        {
            string text = "INEEDTODOTHISPRACTICEORELSEIWILLGETF";
            string key = "KEYWORD";

            string encrypted = AdfgvxCipher.Encrypt(text, key);
            string decrypted = AdfgvxCipher.Decrypt(encrypted, key);

            Assert.AreEqual(text, decrypted);
            Assert.IsFalse(string.IsNullOrEmpty(encrypted));
        }
    }
}