using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Threading;
using System.Linq;

namespace WorkshopTests.PageObjects
{
    public class MainPageObject
    {
        private readonly UIA3Automation _automation;
        private readonly AutomationElement _mainWindow;

        // === ИДЕНТИФИКАТОРЫ ЭЛЕМЕНТОВ (AutomationId) ===
        private const string WINDOW_NAME = "Главное меню";
        private const string LABEL_WELCOME = "labelWelcome";
        private const string LABEL_USER_INFO = "labelUserInfo";
        private const string BUTTON_LOGOUT = "buttonLogout";
        private const string BUTTON_CHANGE_PASSWORD = "buttonChangePassword";
        private const string PANEL_ADMIN = "panelAdmin";

        public MainPageObject(UIA3Automation automation)
        {
            _automation = automation;

            Console.WriteLine("Ожидание главного окна...");

            for (int i = 0; i < 40; i++)
            {
                Thread.Sleep(500);

                _mainWindow = _automation.GetDesktop()
                    .FindFirstChild(cf => cf.ByName(WINDOW_NAME));

                if (_mainWindow != null)
                {
                    Console.WriteLine($"Главное окно найдено: '{_mainWindow.Name}'");
                    return;
                }
            }

            var allWindows = _automation.GetDesktop()
                .FindAllChildren()
                .Where(w => w.ControlType == FlaUI.Core.Definitions.ControlType.Window);

            Console.WriteLine($"Окно не найдено. Доступные окна:");
            foreach (var window in allWindows)
            {
                Console.WriteLine($"  - '{window.Name}'");
            }

            throw new Exception($"Окно '{WINDOW_NAME}' не найдено");
        }

        // === МЕТОДЫ ВЗАИМОДЕЙСТВИЯ ===

        public string GetWelcomeText()
        {
            var welcomeLabel = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(LABEL_WELCOME));
            if (welcomeLabel != null)
            {
                var label = welcomeLabel.AsLabel();
                if (label != null)
                {
                    return label.Text ?? "";
                }
            }

            var allLabels = _mainWindow.FindAllDescendants()
                .Where(e => e.ControlType == FlaUI.Core.Definitions.ControlType.Text);

            if (allLabels.Any())
            {
                var firstLabel = allLabels.First();
                var label = firstLabel.AsLabel();
                if (label != null)
                {
                    return label.Text ?? "";
                }
            }

            return "";
        }

        public string GetUserInfo()
        {
            var userInfoLabel = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(LABEL_USER_INFO));
            if (userInfoLabel != null)
            {
                var label = userInfoLabel.AsLabel();
                if (label != null)
                {
                    return label.Text ?? "";
                }
            }
            return "";
        }

        public void ClickLogout()
        {
            var logoutButton = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(BUTTON_LOGOUT));
            if (logoutButton != null)
            {
                var button = logoutButton.AsButton();
                if (button != null)
                {
                    button.Click();
                }
            }
        }

        public void ClickChangePassword()
        {
            var changePasswordButton = _mainWindow.FindFirstChild(
                cf => cf.ByAutomationId(BUTTON_CHANGE_PASSWORD));
            if (changePasswordButton != null)
            {
                var button = changePasswordButton.AsButton();
                if (button != null)
                {
                    button.Click();
                }
            }
        }

        public bool IsAdminPanelVisible()
        {
            var adminPanel = _mainWindow.FindFirstChild(cf => cf.ByAutomationId(PANEL_ADMIN));
            return adminPanel != null;
        }
    }
}