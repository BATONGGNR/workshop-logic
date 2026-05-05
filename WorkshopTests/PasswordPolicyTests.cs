using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;

namespace WorkshopTests
{
    [TestClass]
    public class PasswordPolicyTests
    {
        // ТЕСТ 1: Пароль короче 8 символов → false
        [TestMethod]
        public void IsPasswordStrong_PasswordShorterThan8_ReturnsFalse()
        {
            // Arrange
            var authService = new AuthenticationService();
            string weakPassword = "short";

            // Act
            bool result = authService.IsPasswordStrong(weakPassword);

            // Assert
            Assert.IsFalse(result);
        }
        // ТЕСТ 2: Пароль длиной 8+, но без цифры → false
        [TestMethod]
        public void IsPasswordStrong_NoDigit_ReturnsFalse()
        {
            // Arrange
            var authService = new AuthenticationService();
            string passwordNoDigit = "VeryLong"; // 8 символов, но нет цифры

            // Act
            bool result = authService.IsPasswordStrong(passwordNoDigit);

            // Assert
            Assert.IsFalse(result);
        }
        // ТЕСТ 3: Надёжный пароль (8+ символов + цифра) → true
        [TestMethod]
        public void IsPasswordStrong_ValidPassword_ReturnsTrue()
        {
            // Arrange
            var authService = new AuthenticationService();
            string strongPassword = "StrongPass1"; // 11 символов, есть цифра

            // Act
            bool result = authService.IsPasswordStrong(strongPassword);

            // Assert
            Assert.IsTrue(result);
        }
    }
}