using WorkshopLogic;
using System;

namespace WorkshopConsoleApp
{
    class Program
    {
        static AuthenticationService authService = new AuthenticationService();
        static UserUpdater userUpdater;
        static string storagePath = "./user_data";

        static void Main(string[] args)
        {
            userUpdater = new UserUpdater(authService, storagePath);
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ ПРОИЗВОДСТВЕННЫМ ЦЕХОМ ===");
                Console.WriteLine();
                Console.WriteLine("1. Регистрация нового пользователя");
                Console.WriteLine("2. Вход в систему");
                Console.WriteLine("3. Выход из программы");
                Console.WriteLine();
                Console.Write("Выберите действие (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        LoginUser();
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Программа завершена. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("=== РЕГИСТРАЦИЯ НОВОГО ПОЛЬЗОВАТЕЛЯ ===");
            Console.WriteLine();

            Console.Write("Введите имя пользователя: ");
            string username = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = ReadPassword();

            Console.WriteLine();
            Console.WriteLine("Выберите роль:");
            Console.WriteLine("1. Operator");
            Console.WriteLine("2. Engineer");
            Console.WriteLine("3. Admin");
            Console.Write("Введите номер роли (1-3): ");

            string roleChoice = Console.ReadLine();
            string role = roleChoice switch
            {
                "2" => "Engineer",
                "3" => "Admin",
                _ => "Operator"
            };

            try
            {
                userUpdater.Update(DateTime.Now.Millisecond, username, password, role);
                Console.WriteLine();
                Console.WriteLine("✓ Пользователь успешно зарегистрирован!");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"✗ Ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void LoginUser()
        {
            Console.Clear();
            Console.WriteLine("=== ВХОД В СИСТЕМУ ===");
            Console.WriteLine();

            Console.Write("Имя пользователя: ");
            string username = Console.ReadLine();

            Console.Write("Пароль: ");
            string password = ReadPassword();

            bool success = authService.Login(username, password);

            if (success)
            {
                User currentUser = authService.GetCurrentUser();
                Console.WriteLine();
                Console.WriteLine($"✓ Вход выполнен успешно!");
                Console.WriteLine($"Добро пожаловать, {currentUser.Username}!");
                Console.WriteLine($"Ваша роль: {currentUser.Role}");
                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();

                // Показываем главное меню после входа
                ShowMainMenu();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("✗ Неверное имя пользователя или пароль!");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        static void ShowMainMenu()
        {
            bool backToLogin = false;

            while (!backToLogin)
            {
                Console.Clear();
                Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine();
                Console.WriteLine("1. Посмотреть информацию о пользователе");
                Console.WriteLine("2. Сменить пароль");
                Console.WriteLine("3. Выйти из аккаунта");
                Console.WriteLine();
                Console.Write("Выберите действие (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowUserInfo();
                        break;
                    case "2":
                        ChangePassword();
                        break;
                    case "3":
                        backToLogin = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowUserInfo()
        {
            User currentUser = authService.GetCurrentUser();
            Console.Clear();
            Console.WriteLine("=== ИНФОРМАЦИЯ О ПОЛЬЗОВАТЕЛЕ ===");
            Console.WriteLine();
            Console.WriteLine($"Имя: {currentUser.Username}");
            Console.WriteLine($"Роль: {currentUser.Role}");
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void ChangePassword()
        {
            Console.Clear();
            Console.WriteLine("=== СМЕНА ПАРОЛЯ ===");
            Console.WriteLine();

            Console.Write("Старый пароль: ");
            string oldPassword = ReadPassword();

            Console.WriteLine();
            Console.Write("Новый пароль: ");
            string newPassword = ReadPassword();

            try
            {
                User currentUser = authService.GetCurrentUser();
                currentUser.ChangePassword(oldPassword, newPassword);
                Console.WriteLine();
                Console.WriteLine("✓ Пароль успешно изменён!");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"✗ Ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        // Метод для безопасного ввода пароля (скрывает символы)
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}