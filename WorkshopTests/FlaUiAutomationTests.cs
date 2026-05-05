using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.UIA3;
using WorkshopTests.PageObjects;
using System.Diagnostics;
using System.Threading;

namespace WorkshopTests
{
    [TestClass]
    public class FlaUiAutomationTests
    {
        private UIA3Automation _automation;
        private Process _appProcess;

        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine("=== Инициализация теста ===");
            _automation = new UIA3Automation();

            string exePath = @"..\..\..\..\WorkshopWinForms\bin\Debug\net7.0-windows\WorkshopWinForms.exe";

            _appProcess = Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true,
                CreateNoWindow = false
            });

            Console.WriteLine($"Приложение запущено с PID: {_appProcess.Id}");
            Thread.Sleep(2000);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Console.WriteLine("=== Завершение теста ===");
            if (_appProcess != null && !_appProcess.HasExited)
            {
                _appProcess.Kill();
                _appProcess.Dispose();
                Console.WriteLine("Приложение завершено");
            }
            _automation?.Dispose();
        }

        // ==================== СЦЕНАРИЙ 1: Успешная авторизация ====================
        [TestMethod]
        public void FlaUiTest_Login_Success()
        {
            Console.WriteLine("Тест: Успешная авторизация");

            var loginPage = new LoginPageObject(_automation);

            // Сначала регистрируем пользователя
            Console.WriteLine("Регистрация нового пользователя...");
            loginPage.EnterUsername("testadmin");
            loginPage.EnterPassword("AdminPass1");
            loginPage.SelectRole("Admin");
            loginPage.ClickRegister();
            Thread.Sleep(2000);

            // Теперь входим
            Console.WriteLine("Вход в систему...");
            loginPage.EnterUsername("testadmin");
            loginPage.EnterPassword("AdminPass1");
            loginPage.ClickLogin();
            Thread.Sleep(3000);

            var mainPage = new MainPageObject(_automation);

            string welcomeText = mainPage.GetWelcomeText();
            Assert.IsTrue(!string.IsNullOrEmpty(welcomeText),
                "Приветствие должно отображаться");
        }

        // ==================== СЦЕНАРИЙ 2: Неверный пароль ====================
        [TestMethod]
        public void FlaUiTest_Login_WrongPassword()
        {
            Console.WriteLine("Тест: Неверный пароль");

            var loginPage = new LoginPageObject(_automation);

            // Сначала регистрируем пользователя
            loginPage.EnterUsername("testuser2");
            loginPage.EnterPassword("CorrectPass1");
            loginPage.SelectRole("Operator");
            loginPage.ClickRegister();
            Thread.Sleep(2000);

            // Пытаемся войти с неверным паролем
            loginPage.EnterUsername("testuser2");
            loginPage.EnterPassword("WrongPassword");
            loginPage.ClickLogin();
            Thread.Sleep(2000);

            string errorMessage = loginPage.GetErrorMessage();
            Assert.IsTrue(!string.IsNullOrEmpty(errorMessage),
                "Должно появиться сообщение об ошибке");
        }

        // ==================== СЦЕНАРИЙ 3: Регистрация + вход ====================
        [TestMethod]
        public void FlaUiTest_Register_And_Login()
        {
            Console.WriteLine("Тест: Регистрация + вход");

            var loginPage = new LoginPageObject(_automation);
            string testUsername = $"autotest_{System.DateTime.Now.Second}";
            string testPassword = "AutoTest1";

            // Регистрация
            Console.WriteLine("Регистрация...");
            loginPage.EnterUsername(testUsername);
            loginPage.EnterPassword(testPassword);
            loginPage.SelectRole("Operator");
            loginPage.ClickRegister();
            Thread.Sleep(3000);

            // Вход
            Console.WriteLine("Вход...");
            loginPage.EnterUsername(testUsername);
            loginPage.EnterPassword(testPassword);
            loginPage.ClickLogin();
            Thread.Sleep(3000);

            var mainPage = new MainPageObject(_automation);

            string welcomeText = mainPage.GetWelcomeText();
            Assert.IsTrue(!string.IsNullOrEmpty(welcomeText),
                "Пользователь должен успешно войти после регистрации");
        }
    }
}