using System;
using System.Windows.Forms;
using WorkshopLogic;

namespace WorkshopWinForms
{
    public partial class LoginForm : Form
    {
        private AuthenticationService _authService;
        private UserUpdater _userUpdater;
        private string _storagePath = "./user_data";

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
            _userUpdater = new UserUpdater(_authService, _storagePath);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                labelError.Text = "Введите имя пользователя и пароль";
                return;
            }

            bool success = _authService.Login(username, password);

            if (success)
            {
                labelError.Text = "";
                User currentUser = _authService.GetCurrentUser();

                MainForm mainForm = new MainForm(currentUser, _authService, _userUpdater);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                labelError.Text = "Неверное имя пользователя или пароль";
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                labelError.Text = "Введите имя пользователя и пароль";
                return;
            }

            // Проверка надёжности пароля
            if (!_authService.IsPasswordStrong(password))
            {
                labelError.Text = "Пароль слишком слабый! Минимум 8 символов + цифра.";
                return;
            }

            // Определяем выбранную роль через RadioButton
            string selectedRole = "Operator";
            if (radioButtonEngineer.Checked)
                selectedRole = "Engineer";
            else if (radioButtonAdmin.Checked)
                selectedRole = "Admin";

            try
            {
                _userUpdater.Update(DateTime.Now.Millisecond, username, password, selectedRole);
                labelError.Text = $"Пользователь зарегистрирован как {selectedRole}! Теперь войдите.";
                textBoxUsername.Clear();
                textBoxPassword.Clear();
                radioButtonOperator.Checked = true;
            }
            catch (Exception ex)
            {
                labelError.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}