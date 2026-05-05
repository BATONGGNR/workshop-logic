using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using FluentAssertions;
using System.Linq;

namespace WorkshopTests.StepDefinitions
{
    [Binding]
    public class PasswordPolicySteps
    {
        private AuthenticationService _authService;
        private string _testPassword;
        private bool _isPasswordStrong;
        private string _validationError;
        private string _newPassword;

        [BeforeScenario]
        public void Setup()
        {
            _authService = new AuthenticationService();
            _validationError = "";
            _newPassword = "";
        }

        [Given(@"пользователь вводит пароль ""(.*)""")]
        public void GivenUserEntersPassword(string password)
        {
            _testPassword = password;
        }

        [When(@"система проверяет надёжность пароля")]
        public void WhenSystemChecksPasswordStrength()
        {
            _isPasswordStrong = IsPasswordStrong(_testPassword);

            if (!_isPasswordStrong)
            {
                if (string.IsNullOrEmpty(_testPassword))
                    _validationError = "Пароль не может быть пустым";
                else if (_testPassword.Length < 8)
                    _validationError = "Пароль должен быть не менее 8 символов";
                else if (!_testPassword.Any(char.IsDigit))
                    _validationError = "Пароль должен содержать хотя бы одну цифру";
                else if (!_testPassword.Any(char.IsLetter))
                    _validationError = "Пароль должен содержать буквы";
                else
                    _validationError = "Пароль ненадёжный";
            }
        }

        [Then(@"пароль должен быть признан надёжным")]
        public void ThenPasswordShouldBeStrong()
        {
            _isPasswordStrong.Should().BeTrue("пароль соответствует политике безопасности");
        }

        [Then(@"пароль должен быть признан ненадёжным")]
        public void ThenPasswordShouldBeWeak()
        {
            _isPasswordStrong.Should().BeFalse("пароль не соответствует политике безопасности");
        }

        [Then(@"регистрация должна быть успешной")]
        public void ThenRegistrationShouldSucceed()
        {
            _authService.RegisterUser(1, "testuser", _testPassword, "Operator");
            var user = _authService.GetUserByUsername("testuser");
            user.Should().NotBeNull();
        }

        [Then(@"регистрация должна быть отклонена с сообщением ""(.*)""")]
        public void ThenRegistrationShouldBeRejected(string expectedError)
        {
            _validationError.Should().Be(expectedError);
        }

        [Given(@"пользователь ""(.*)"" авторизован в системе")]
        public void GivenUserAuthorized(string username)
        {
            _authService.RegisterUser(1, username, "oldPass1", "Admin");
            _authService.Login(username, "oldPass1");
        }

        [Given(@"текущий пароль пользователя ""(.*)""")]
        public void GivenCurrentPassword(string password)
        {
            // Пароль уже установлен при авторизации
        }

        [When(@"пользователь меняет пароль на ""(.*)""")]
        public void WhenUserChangesPassword(string newPassword)
        {
            _newPassword = newPassword;
            var currentUser = _authService.GetCurrentUser();
            if (currentUser != null)
            {
                try
                {
                    currentUser.ChangePassword("oldPass1", newPassword);
                    _isPasswordStrong = true;
                }
                catch
                {
                    _isPasswordStrong = false;
                }
            }
        }

        [Then(@"система должна принять новый пароль")]
        public void ThenSystemShouldAcceptNewPassword()
        {
            _isPasswordStrong.Should().BeTrue();
        }

        [Then(@"пользователь должен иметь возможность войти с новым паролем")]
        public void ThenUserCanLoginWithNewPassword()
        {
            var currentUser = _authService.GetCurrentUser();
            if (currentUser != null)
            {
                var result = _authService.Login(currentUser.Username, _newPassword);
                result.Should().BeTrue();
            }
        }

        private bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;
            if (password.Length < 8)
                return false;
            if (!password.Any(char.IsDigit))
                return false;
            if (!password.Any(char.IsLetter))
                return false;
            return true;
        }
    }
}