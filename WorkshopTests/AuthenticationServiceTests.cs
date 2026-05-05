using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using System;

namespace WorkshopTests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        [TestMethod]
        public void RegisterUser_NewUser_ReturnsSuccess()
        {
            // Arrange
            var service = new AuthenticationService();

            // Act
            service.RegisterUser(1, "user1", "password123", "Operator");

            // Assert
            var user = service.GetUserByUsername("user1");
            Assert.IsNotNull(user);
            Assert.AreEqual("user1", user.Username);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegisterUser_DuplicateUser_ThrowsException()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "user1", "password123", "Operator");

            // Act
            service.RegisterUser(2, "user1", "password456", "Operator");
        }

        [TestMethod]
        public void Login_CorrectCredentials_ReturnsTrue()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "user1", "password123", "Operator");

            // Act
            var result = service.Login("user1", "password123");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_WrongCredentials_ReturnsFalse()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "user1", "password123", "Operator");

            // Act
            var result = service.Login("user1", "wrongpassword");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangeUserRole_AdminUser_Success()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "admin", "adminpass", "Admin");
            service.RegisterUser(2, "user1", "password123", "Operator");
            service.Login("admin", "adminpass");

            // Act
            service.ChangeUserRole("user1", "Supervisor");

            // Assert
            var user = service.GetUserByUsername("user1");
            Assert.AreEqual("Supervisor", user.Role);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ChangeUserRole_NotAdmin_Fails()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "user1", "password123", "Operator");
            service.Login("user1", "password123");

            // Act
            service.ChangeUserRole("user1", "Supervisor");


        }
     
        //[TestMethod]
        ////public void ChangeUserRole_Admin_CannotChangeAnotherAdminRole()
        ////{

        ////    var service = new AuthenticationService();

        ////    service.RegisterUser(1, "User1", "adminpass1", "User");
        ////    service.RegisterUser(2, "User2", "adminpass2", "User");

        ////    service.Login("User1", "adminpass1");

        ////    service.ChangeUserRole("User2", "Operator");


        ////}
    }
}