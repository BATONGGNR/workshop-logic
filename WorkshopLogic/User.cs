namespace WorkshopLogic
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public User(int id, string username, string password, string role)
        {
            Id = id;
            Username = username;
            PasswordHash = HashPassword(password);
            Role = role;
        }

        private string HashPassword(string password)
        {
            // Простое хеширование (в реальном проекте используйте BCrypt или SHA256)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool ValidatePassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (ValidatePassword(oldPassword))
            {
                PasswordHash = HashPassword(newPassword);
            }
            else
            {
                throw new Exception("Неверный старый пароль");
            }
        }
    }

}