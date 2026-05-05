using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using System;

namespace WorkshopTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void ValidatePassword_CorrectPassword_ReturnsTrue()
        {
            // Arrange (Подготовка)
            var user = new User(1, "testuser", "password123", "Operator");

            // Act (Действие)
            var result = user.ValidatePassword("password123");

            // Assert (Проверка)
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatePassword_WrongPassword_ReturnsFalse()
        {
            // Arrange
            var user = new User(1, "testuser", "password123", "Operator");

            // Act
            var result = user.ValidatePassword("wrongpassword");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_ValidOldPassword_Success()
        {
            // Arrange
            var user = new User(1, "testuser", "oldpassword", "Operator");

            // Act
            user.ChangePassword("oldpassword", "newpassword");

            // Assert
            Assert.IsTrue(user.ValidatePassword("newpassword"));
            Assert.IsFalse(user.ValidatePassword("oldpassword"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ChangePassword_InvalidOldPassword_ThrowsException()
        {
            // Arrange
            var user = new User(1, "testuser", "password123", "Operator");

            // Act
            user.ChangePassword("wrongpassword", "newpassword");
        }
    }
}