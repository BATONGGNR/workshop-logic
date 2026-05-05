using System;
using System.IO;
using System.Text.Json;
using System.Reflection;

namespace WorkshopLogic
{
    public class UserUpdater
    {
        private readonly AuthenticationService _authService;
        private readonly string _storagePath;

        public UserUpdater(AuthenticationService authService, string storagePath)
        {
            _authService = authService;
            _storagePath = storagePath;
        }

        public bool Update(int id, string username, string password, string newRole)
        {
            var existingUser = _authService.GetUserByUsername(username);

            if (existingUser == null)
            {
                // Регистрируем нового пользователя
                _authService.RegisterUser(id, username, password, newRole);
                SaveUserToFile(username, id, password, newRole);
                return true;
            }
            else
            {
                // ОБНОВЛЯЕМ существующего пользователя
                // Обновляем роль через AuthService (если текущий пользователь - админ)
                var currentUser = _authService.GetCurrentUser();
                if (currentUser != null && currentUser.Role == "Admin")
                {
                    _authService.ChangeUserRole(username, newRole);
                }

                // Обновляем пароль пользователя через рефлексию (так как нет публичного метода)
                UpdateUserPassword(existingUser, password);

                // Сохраняем в файл
                SaveUserToFile(username, id, password, newRole);
                return true;
            }
        }

        private void UpdateUserPassword(User user, string newPassword)
        {
            // Используем рефлексию для обновления PasswordHash
            var property = typeof(User).GetProperty("PasswordHash");
            var hashMethod = typeof(User).GetMethod("HashPassword",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (hashMethod != null)
            {
                var newHash = hashMethod.Invoke(user, new object[] { newPassword }) as string;
                property.SetValue(user, newHash);
            }
        }

        private void SaveUserToFile(string username, int id, string password, string role)
        {
            Directory.CreateDirectory(_storagePath);
            var userData = new
            {
                Id = id,
                Username = username,
                PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password)),
                Role = role,
                UpdatedAt = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(userData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path.Combine(_storagePath, $"{username}.json"), json);
        }

        public void CleanupTestFiles(string username)
        {
            var filePath = Path.Combine(_storagePath, $"{username}.json");
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}