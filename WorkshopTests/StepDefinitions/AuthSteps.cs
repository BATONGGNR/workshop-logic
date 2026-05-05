using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using FluentAssertions;

namespace WorkshopTests.StepDefinitions
{
    [Binding]
    public class AuthSteps
    {
        private AuthenticationService _authService;
        private User _currentUser;
        private bool _loginResult;
        private string _errorMessage;

        [BeforeScenario]
        public void Setup()
        {
            _authService = new AuthenticationService();
            _errorMessage = "";
        }

        [Given(@"пользователь ""(.*)"" зарегистрирован с паролем ""(.*)""")]
        public void GivenUserRegisteredWithPassword(string username, string password)
        {
            _authService.RegisterUser(1, username, password, "Operator");
        }

        [When(@"пользователь вводит логин ""(.*)"" и пароль ""(.*)""")]
        public void WhenUserEntersCredentials(string username, string password)
        {
            _loginResult = _authService.Login(username, password);
            _currentUser = _authService.GetCurrentUser();

            if (!_loginResult)
            {
                _errorMessage = "Неверное имя пользователя или пароль";
            }
        }

        [Then(@"система должна авторизовать пользователя")]
        public void ThenSystemShouldAuthorizeUser()
        {
            _loginResult.Should().BeTrue("авторизация должна быть успешной");
        }

        [Then(@"текущий пользователь должен быть ""(.*)""")]
        public void ThenCurrentUserShouldBe(string expectedUsername)
        {
            _currentUser.Should().NotBeNull();
            _currentUser.Username.Should().Be(expectedUsername);
        }

        [Then(@"система должна отказать в авторизации")]
        public void ThenSystemShouldDenyAuthorization()
        {
            _loginResult.Should().BeFalse("авторизация должна быть отклонена");
        }

        [Then(@"должно появиться сообщение ""(.*)""")]
        public void ThenErrorMessageShouldBe(string expectedMessage)
        {
            _errorMessage.Should().Be(expectedMessage);
        }

        [Given(@"система инициализирована")]
        public void GivenSystemInitialized()
        {
            _authService = new AuthenticationService();
        }

        [When(@"новый пользователь регистрируется с именем ""(.*)"", паролем ""(.*)"" и ролью ""(.*)""")]
        public void WhenNewUserRegisters(string username, string password, string role)
        {
            _authService.RegisterUser(2, username, password, role);
            _currentUser = _authService.GetUserByUsername(username);
        }

        [Then(@"пользователь ""(.*)"" должен быть создан в системе")]
        public void ThenUserShouldBeCreated(string username)
        {
            var user = _authService.GetUserByUsername(username);
            user.Should().NotBeNull();
        }

        [Then(@"роль пользователя должна быть ""(.*)""")]
        public void ThenUserRoleShouldBe(string expectedRole)
        {
            _currentUser.Role.Should().Be(expectedRole);
        }
    }
}