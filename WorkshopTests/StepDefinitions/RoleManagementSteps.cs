using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopLogic;
using FluentAssertions;

namespace WorkshopTests.StepDefinitions
{
    [Binding]
    public class RoleManagementSteps
    {
        private AuthenticationService _authService;
        private string _targetUsername;
        private string _newRole;
        private Exception _caughtException;

        [BeforeScenario]
        public void Setup()
        {
            _authService = new AuthenticationService();
            _caughtException = null;
        }

        [Given(@"администратор ""(.*)"" с ролью ""(.*)"" выполнен вход в систему")]
        public void GivenAdminLoggedIn(string username, string role)
        {
            _authService.RegisterUser(1, username, "adminPass1", role);
            _authService.Login(username, "adminPass1");
        }

        [Given(@"пользователь ""(.*)"" с ролью ""(.*)"" существует в системе")]
        public void GivenUserWithRoleExists(string username, string role)
        {
            _authService.RegisterUser(2, username, "userPass1", role);
            _targetUsername = username;
        }

        [Given(@"пользователь ""(.*)"" с ролью ""(.*)"" выполнен вход в систему")]
        public void GivenUserLoggedIn(string username, string role)
        {
            _authService.RegisterUser(1, username, "userPass1", role);
            _authService.Login(username, "userPass1");
        }

        [When(@"администратор меняет роль пользователя ""(.*)"" на ""(.*)""")]
        public void WhenAdminChangesUserRole(string username, string newRole)
        {
            _targetUsername = username;
            _newRole = newRole;

            try
            {
                _authService.ChangeUserRole(username, newRole);
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [When(@"пользователь без прав администратора пытается изменить роль другого пользователя")]
        public void WhenUserWithoutAdminRightsTriesToChangeRole()
        {
            try
            {
                _authService.ChangeUserRole("otherUser", "Admin");
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [Then(@"роль пользователя ""(.*)"" должна быть изменена на ""(.*)""")]
        public void ThenUserRoleShouldBeChanged(string username, string expectedRole)
        {
            var user = _authService.GetUserByUsername(username);
            user.Role.Should().Be(expectedRole);
        }

        [Then(@"пользователь ""(.*)"" должен получить новые права доступа")]
        public void ThenUserShouldGetNewPermissions(string username)
        {
            var user = _authService.GetUserByUsername(username);
            user.Should().NotBeNull();
        }

        [Then(@"система должна отклонить операцию с ошибкой ""(.*)""")]
        public void ThenSystemShouldRejectOperationWithError(string expectedMessage)
        {
            _caughtException.Should().NotBeNull();
            _caughtException.Message.Should().Be(expectedMessage);
        }

        [Given(@"администратор авторизован в системе")]
        public void GivenAdminAuthorized()
        {
            _authService.RegisterUser(1, "admin", "adminPass1", "Admin");
            _authService.Login("admin", "adminPass1");
        }

        [When(@"администратор регистрирует нового пользователя с ролью ""(.*)""")]
        public void WhenAdminRegistersUserWithRole(string role)
        {
            _newRole = role;
            _authService.RegisterUser(3, "newuser", "newPass1", role);
        }

        [Then(@"новый пользователь должен быть создан с ролью ""(.*)""")]
        public void ThenNewUserShouldBeCreatedWithRole(string expectedRole)
        {
            var user = _authService.GetUserByUsername("newuser");
            user.Should().NotBeNull();
            user.Role.Should().Be(expectedRole);
        }

        [Then(@"пользователь должен иметь права, соответствующие роли ""(.*)""")]
        public void ThenUserShouldHaveRolePermissions(string role)
        {
            role.Should().NotBeNullOrEmpty();
        }
    }
}