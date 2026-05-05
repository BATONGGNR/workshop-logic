using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Threading;
using System.Linq;

namespace WorkshopTests.PageObjects
{
    public class LoginPageObject
    {
        private readonly UIA3Automation _automation;
        private readonly AutomationElement _mainWindow;

        // === ИДЕНТИФИКАТОРЫ ЭЛЕМЕНТОВ (AutomationId) ===
        private const string WINDOW_NAME = "Вход в систему";
        private const string TEXTBOX_USERNAME = "textBoxUsername";
        private const string TEXTBOX_PASSWORD = "textBoxPassword";
        private const string BUTTON_LOGIN = "buttonLogin";
        private const string BUTTON_REGISTER = "buttonRegister";
        private const string LABEL_ERROR = "labelError";
        private const string RADIO_OPERATOR = "radioButtonOperator";
        private const string RADIO_ENGINEER = "radioButtonEngineer";
        private const string RADIO_ADMIN = "radioButtonAdmin";

        public LoginPageObject(UIA3Automation automation)
        {
            _automation = automation;

            Console.WriteLine("Ожидание появления окна...");

            for (int i = 0; i < 40; i++)
            {
                Thread.Sleep(500);

                _mainWindow = _automation.GetDesktop()
                    .FindFirstChild(cf => cf.ByName(WINDOW_NAME));

                if (_mainWindow != null)
                {
                    Console.WriteLine($"Окно найдено: '{_mainWindow.Name}'");
                    return;
                }
            }

            throw new Exception($"Окно '{WINDOW_NAME}' не найдено после 20 секунд ожидания");
        }

        // === МЕТОДЫ ВЗАИМОДЕЙСТВИЯ ===

        /// <summary>
        /// Заполнить форму входа (удобный метод)
        /// </summary>
        public void FillLoginForm(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
        }

        public void EnterUsername(string username)
        {
            Console.WriteLine($"Ввод имени пользователя: {username}");

            var usernameField = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(TEXTBOX_USERNAME));

            if (usernameField != null)
            {
                var textBox = usernameField.AsTextBox();
                if (textBox != null)
                {
                    textBox.Text = username;
                    Console.WriteLine("Имя пользователя введено");
                }
            }
            else
            {
                Console.WriteLine("Поле не найдено по AutomationId");
                var allEdits = _mainWindow.FindAllDescendants()
                    .Where(e => e.ControlType == FlaUI.Core.Definitions.ControlType.Edit);

                if (allEdits.Count() >= 1)
                {
                    allEdits.First().AsTextBox().Text = username;
                    Console.WriteLine("Введено в первое поле Edit");
                }
            }
        }

        public void EnterPassword(string password)
        {
            var passwordField = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(TEXTBOX_PASSWORD));
            if (passwordField != null)
            {
                var textBox = passwordField.AsTextBox();
                if (textBox != null)
                {
                    textBox.Text = password;
                }
            }
            else
            {
                var allEdits = _mainWindow.FindAllDescendants()
                    .Where(e => e.ControlType == FlaUI.Core.Definitions.ControlType.Edit);

                if (allEdits.Count() >= 2)
                {
                    allEdits.ElementAt(1).AsTextBox().Text = password;
                }
            }
        }

        public void ClickLogin()
        {
            var loginButton = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(BUTTON_LOGIN));
            if (loginButton != null)
            {
                var button = loginButton.AsButton();
                if (button != null)
                {
                    button.Click();
                    Console.WriteLine("Кнопка 'Войти' нажата");
                }
            }
        }

        public void ClickRegister()
        {
            var registerButton = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(BUTTON_REGISTER));
            if (registerButton != null)
            {
                var button = registerButton.AsButton();
                if (button != null)
                {
                    button.Click();
                }
            }
        }

        public string GetErrorMessage()
        {
            var errorLabel = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(LABEL_ERROR));
            if (errorLabel != null)
            {
                var label = errorLabel.AsLabel();
                if (label != null)
                {
                    return label.Text ?? "";
                }
            }
            return "";
        }

        public void SelectRole(string role)
        {
            Console.WriteLine($"Выбор роли: {role}");

            if (role == "Operator")
            {
                var radioButton = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(RADIO_OPERATOR));
                if (radioButton != null)
                {
                    radioButton.AsButton().Click();
                    Console.WriteLine("Выбрана роль Operator");
                }
            }
            else if (role == "Engineer")
            {
                var radioButton = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(RADIO_ENGINEER));
                if (radioButton != null)
                {
                    radioButton.AsButton().Click();
                    Console.WriteLine("Выбрана роль Engineer");
                }
            }
            else if (role == "Admin")
            {
                var radioButton = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(RADIO_ADMIN));
                if (radioButton != null)
                {
                    radioButton.AsButton().Click();
                    Console.WriteLine("Выбрана роль Admin");
                }
            }
        }
    }
}