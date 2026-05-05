using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using System;
using System.IO;

namespace WorkshopTests
{
    [TestClass]
    public class IntegrationTests
    {
        private const string TestStoragePath = "./test_user_storage";

        [TestInitialize]
        public void Setup()
        {
            if (Directory.Exists(TestStoragePath))
                Directory.Delete(TestStoragePath, true);
            Directory.CreateDirectory(TestStoragePath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(TestStoragePath))
                Directory.Delete(TestStoragePath, true);
        }

        // ==================== ТЕСТЫ ДЛЯ USER (интеграционные) ====================

        [TestMethod]
        public void User_PasswordHash_IntegrationTest()
        {
            var user = new User(1, "worker1", "strongPass123", "Operator");
            Assert.IsNotNull(user.PasswordHash);
            Assert.AreNotEqual("strongPass123", user.PasswordHash);
            Assert.IsTrue(user.ValidatePassword("strongPass123"));
        }

        [TestMethod]
        public void User_ChangeRole_IntegrationTest()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "admin", "adminpass", "Admin");
            service.RegisterUser(2, "user1", "password123", "Operator");
            service.Login("admin", "adminpass");

            // Act: Админ меняет роль ДРУГОМУ пользователю (не себе!)
            service.ChangeUserRole("user1", "Engineer");

            // Assert
            var user = service.GetUserByUsername("user1");
            Assert.AreEqual("Engineer", user.Role);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void User_ChangeRole_CannotChangeOwnRole()
        {
            // Arrange
            var service = new AuthenticationService();
            service.RegisterUser(1, "admin", "adminpass", "Admin");
            service.Login("admin", "adminpass");

            // Act: Попытка изменить свою собственную роль (должна выбросить исключение)
            service.ChangeUserRole("admin", "Operator");

            // Assert: Исключение должно быть выброшено
        }

        // ==================== ТЕСТЫ ДЛЯ AuthenticationService ====================

        [TestMethod]
        public void AuthenticationService_RegisterAndLogin_IntegrationTest()
        {
            var service = new AuthenticationService();
            service.RegisterUser(10, "operator1", "pass123", "Operator");
            var loginResult = service.Login("operator1", "pass123");
            var currentUser = service.GetCurrentUser();

            Assert.IsTrue(loginResult);
            Assert.IsNotNull(currentUser);
            Assert.AreEqual("operator1", currentUser.Username);
            Assert.AreEqual("Operator", currentUser.Role);
        }

        [TestMethod]
        public void AuthenticationService_ChangeRole_WithFilePersistence_IntegrationTest()
        {
            var authService = new AuthenticationService();
            var updater = new UserUpdater(authService, TestStoragePath);

            updater.Update(20, "engineer1", "engPass", "Engineer");

            authService.RegisterUser(1, "admin", "admin123", "Admin");
            authService.Login("admin", "admin123");

            authService.ChangeUserRole("engineer1", "SeniorEngineer");
            var updatedUser = authService.GetUserByUsername("engineer1");

            Assert.AreEqual("SeniorEngineer", updatedUser.Role);

            var filePath = Path.Combine(TestStoragePath, "engineer1.json");
            Assert.IsTrue(File.Exists(filePath));
        }

        // ==================== ТЕСТЫ ДЛЯ UserUpdater ====================

        [TestMethod]
        public void UserUpdater_NewUserRegistration_IntegrationTest()
        {
            var authService = new AuthenticationService();
            var updater = new UserUpdater(authService, TestStoragePath);

            var result = updater.Update(30, "newuser", "newPass456", "Guest");

            Assert.IsTrue(result);
            var user = authService.GetUserByUsername("newuser");
            Assert.IsNotNull(user);
            Assert.AreEqual("Guest", user.Role);

            Assert.IsTrue(File.Exists(Path.Combine(TestStoragePath, "newuser.json")));
        }

        [TestMethod]
        public void UserUpdater_ExistingUserUpdate_IntegrationTest()
        {
            var authService = new AuthenticationService();
            var updater = new UserUpdater(authService, TestStoragePath);

            // Сначала регистрируем
            updater.Update(40, "updater_test", "oldPass", "User");

            // Авторизуемся как админ для обновления роли
            authService.RegisterUser(1, "admin", "admin123", "Admin");
            authService.Login("admin", "admin123");

            // Act: обновляем пароль и роль
            var result = updater.Update(40, "updater_test", "newPass789", "Premium");

            // Assert
            Assert.IsTrue(result);
            var user = authService.GetUserByUsername("updater_test");
            Assert.AreEqual("Premium", user.Role);
            Assert.IsTrue(user.ValidatePassword("newPass789"));
        }

        // ==================== BIG BANG МЕТОД ====================

        [TestMethod]
        public void BigBang_FullWorkflow_IntegrationTest()
        {
            var authService = new AuthenticationService();
            var updater = new UserUpdater(authService, TestStoragePath);
            var simulator = new WorkshopSimulator();

            // 1. Регистрация админа
            updater.Update(1, "admin", "adminRoot", "Admin");
            authService.Login("admin", "adminRoot");

            // 2. Регистрация оператора
            updater.Update(2, "operator", "op123", "Operator");

            // 3. Оператор авторизуется и работает с симулятором
            authService.Login("operator", "op123");
            var loadFactor = simulator.CalculateLoadFactor(45, 15);
            var canProcess = simulator.CanProcessPart(3, 10);
            var batchTime = simulator.CalculateBatchTime(20, 5);

            // Assert
            Assert.AreEqual(0.75, loadFactor, 0.001);
            Assert.IsTrue(canProcess);
            Assert.AreEqual(100, batchTime);

            Assert.IsTrue(File.Exists(Path.Combine(TestStoragePath, "admin.json")));
            Assert.IsTrue(File.Exists(Path.Combine(TestStoragePath, "operator.json")));
        }

        // ==================== BOTTOM-UP МЕТОД ====================

        [TestMethod]
        public void BottomUp_UserLayer_Test()
        {
            var user = new User(1, "test", "pwd", "Role");
            Assert.IsTrue(user.ValidatePassword("pwd"));
        }

        [TestMethod]
        public void BottomUp_AuthServiceLayer_Test()
        {
            var authService = new AuthenticationService();
            authService.RegisterUser(1, "test", "pwd", "User");
            Assert.IsTrue(authService.Login("test", "pwd"));
        }

        [TestMethod]
        public void BottomUp_UserUpdaterLayer_Test()
        {
            var authService = new AuthenticationService();
            var updater = new UserUpdater(authService, TestStoragePath);

            var result = updater.Update(1, "final_test", "finalPwd", "FinalRole");

            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(Path.Combine(TestStoragePath, "final_test.json")));
        }
        // ==================== НАГРУЗОЧНОЕ ТЕСТИРОВАНИЕ ====================

        [TestMethod]
        public async Task LoadTest_100Users_10OperationsEach()
        {
            // Arrange
            var loadGenerator = new LoadTestGenerator();
            var userCount = 100;
            var operationsPerUser = 10;

            // Act
            var result = await loadGenerator.RunLoadTest(userCount, operationsPerUser);

            // Assert
            Assert.IsTrue(result.SuccessCount > 0, "Должны быть успешные операции");
            Assert.AreEqual(result.TotalOperations, result.SuccessCount + result.FailCount);

            // Вывод результатов в консоль
            Console.WriteLine($"=== LOAD TEST (100 users) ===");
            Console.WriteLine($"Total Operations: {result.TotalOperations}");
            Console.WriteLine($"Success: {result.SuccessCount}");
            Console.WriteLine($"Failed: {result.FailCount}");
            Console.WriteLine($"Total Time: {result.TotalTimeMs} ms");
            Console.WriteLine($"Avg Time/Op: {result.AvgTimePerOperation:F2} ms");
            Console.WriteLine($"Ops/Second: {result.OperationsPerSecond:F2}");

            // Cleanup
            loadGenerator.CleanupTestFiles();
        }

        [TestMethod]
        public async Task LoadTest_500Users_10OperationsEach()
        {
            // Arrange
            var loadGenerator = new LoadTestGenerator();
            var userCount = 500;
            var operationsPerUser = 10;

            // Act
            var result = await loadGenerator.RunLoadTest(userCount, operationsPerUser);

            // Assert
            Assert.IsTrue(result.SuccessCount > 0);

            Console.WriteLine($"\n=== LOAD TEST (500 users) ===");
            Console.WriteLine($"Success: {result.SuccessCount}/{result.TotalOperations}");
            Console.WriteLine($"Time: {result.TotalTimeMs} ms");
            Console.WriteLine($"Ops/Sec: {result.OperationsPerSecond:F2}");

            loadGenerator.CleanupTestFiles();
        }

        [TestMethod]
        public async Task LoadTest_1000Users_10OperationsEach()
        {
            // Arrange
            var loadGenerator = new LoadTestGenerator();
            var userCount = 1000;
            var operationsPerUser = 10;

            // Act
            var result = await loadGenerator.RunLoadTest(userCount, operationsPerUser);

            // Assert
            Assert.IsTrue(result.SuccessCount > 0);

            Console.WriteLine($"\n=== LOAD TEST (1000 users) ===");
            Console.WriteLine($"Success: {result.SuccessCount}/{result.TotalOperations}");
            Console.WriteLine($"Time: {result.TotalTimeMs} ms");
            Console.WriteLine($"Ops/Sec: {result.OperationsPerSecond:F2}");

            loadGenerator.CleanupTestFiles();
        }
    }
}