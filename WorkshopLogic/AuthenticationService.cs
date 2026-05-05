using System.Collections.Generic;
using System.Linq;

namespace WorkshopLogic
{
    public class AuthenticationService
    {
        private List<User> _users = new List<User>();
        private User? _currentUser;  // ← Исправлено

        // Регистрация нового пользователя
        public void RegisterUser(int id, string username, string password, string role)
        {
            if (_users.Any(u => u.Username == username))
            {
                throw new Exception("Пользователь с таким именем уже существует");
            }

            _users.Add(new User(id, username, password, role));
        }

        // Авторизация (вход в систему)
        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user != null && user.ValidatePassword(password))
            {
                _currentUser = user;
                return true;
            }

            return false;
        }

        // Смена роли пользователя (только для администраторов)
        public void ChangeUserRole(string username, string newRole)
        {
            // Проверка: пользователь должен быть админом
            if (_currentUser == null || _currentUser.Role != "Admin")
            {
                throw new Exception("Только администратор может менять роли!");
            }

            // Проверка: нельзя менять роль самому себе
            if (username.Trim().ToLower() == _currentUser.Username.Trim().ToLower())
            {
                throw new Exception("Нельзя изменить свою собственную роль!");
            }

            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new Exception("Пользователь не найден!");
            }

            user.Role = newRole;
        }

        // Метод для получения текущего пользователя (для тестов)
        public User? GetCurrentUser()  // ← Исправлено
        {
            return _currentUser;
        }

        // Метод для получения пользователя по имени (для тестов)
        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public bool IsPasswordStrong(string password)
        {
            // Проверяем длину
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return false;
            }

            // Проверяем наличие хотя бы одной цифры
            return password.Any(char.IsDigit);
        }
    }
}