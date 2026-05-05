using System;
using System.Windows.Forms;
using WorkshopLogic;

namespace WorkshopWinForms
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private AuthenticationService _authService;
        private UserUpdater _userUpdater;

        public MainForm(User user, AuthenticationService authService, UserUpdater userUpdater)
        {
            InitializeComponent();
            _currentUser = user;
            _authService = authService;
            _userUpdater = userUpdater;

            labelWelcome.Text = $"Добро пожаловать, {_currentUser.Username}!";
            labelUserInfo.Text = $"Пользователь: {_currentUser.Username} | Роль: {_currentUser.Role}";

            // Показываем панель администратора только для админов
            panelAdmin.Visible = (_currentUser.Role == "Admin");
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            foreach (Form form in Application.OpenForms)
            {
                if (form is LoginForm)
                {
                    form.Show();
                    break;
                }
            }
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changeForm = new ChangePasswordForm(_currentUser);
            changeForm.ShowDialog();
        }

        private void buttonChangeRole_Click(object sender, EventArgs e)
        {
            if (_currentUser.Role != "Admin")
            {
                MessageBox.Show("Только АДМИНИСТРАТОР может менять роли!", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите имя пользователя для смены роли:",
                "Смена роли",
                "");

            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            // 🔒 Проверка на смену своей роли
            if (username.Trim().ToLower() == _currentUser.Username.Trim().ToLower())
            {
                MessageBox.Show("Нельзя изменить свою собственную роль!", "Ошибка безопасности",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ← Важно: выход из метода
            }

            string newRole = Microsoft.VisualBasic.Interaction.InputBox(
                $"Введите новую роль для {username}:\n(Operator, Engineer, Admin)",
                "Смена роли пользователя",
                "Operator");

            if (string.IsNullOrEmpty(newRole))
            {
                return;
            }

            try
            {
                _authService.ChangeUserRole(username, newRole);
                MessageBox.Show($"Роль пользователя {username} изменена на {newRole}",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var targetUser = _authService.GetUserByUsername(username);
            if (targetUser != null && targetUser.Role == "Admin")
            {
                MessageBox.Show("Нельзя изменять роль другого администратора!",
                    "Ошибка безопасности", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}