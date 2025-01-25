using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BeestjeOpJeFeestje.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<UserManager<Account>> _userManagerMock;
        private AccountService _accountService;

        [SetUp]
        public void SetUp()
        {
            Mock<IUserStore<Account>> store = new Mock<IUserStore<Account>>();
            _userManagerMock = new Mock<UserManager<Account>>(store.Object, null, null, null, null, null, null, null, null);
            _accountService = new AccountService(_userManagerMock.Object);
        }

        [Test]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            IQueryable<Account> users = new List<Account>
            {
                new Account { UserName = "user1" },
                new Account { UserName = "user2" }
            }.AsQueryable();

            _userManagerMock.Setup(x => x.Users).Returns(users);

            // Act
            List<Account> result = _accountService.GetAllUsers();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("user1", result[0].UserName);
            Assert.AreEqual("user2", result[1].UserName);
        }

        [Test]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            Account user = new Account { Id = "1", UserName = "user1" };
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(user);

            // Act
            Account? result = await _accountService.GetUserById("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user1", result.UserName);
        }

        [Test]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync((Account) null);

            // Act
            Account? result = await _accountService.GetUserById("1");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetUserByEmail_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            Account user = new Account { Email = "user1@example.com", UserName = "user1" };
            _userManagerMock.Setup(x => x.FindByEmailAsync("user1@example.com")).ReturnsAsync(user);

            // Act
            Account? result = await _accountService.GetUserByEmail("user1@example.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user1", result.UserName);
        }

        [Test]
        public async Task GetUserByEmail_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByEmailAsync("user1@example.com")).ReturnsAsync((Account) null);

            // Act
            Account? result = await _accountService.GetUserByEmail("user1@example.com");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteUser_ShouldCallDeleteAsync()
        {
            // Arrange
            Account user = new Account { UserName = "user1" };
            _userManagerMock.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            await _accountService.DeleteUser(user);

            // Assert
            _userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task CreateUser_ShouldReturnPasswordAndIdentityResult()
        {
            // Arrange
            Account user = new Account { UserName = "user1", Email = "user1@example.com" };
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            Tuple<string, IdentityResult> result = await _accountService.CreateUser("user1", "user1@example.com", "address", "1234567890", MembershipLevel.Silver);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Item1); // Password
            Assert.IsTrue(result.Item2.Succeeded); // IdentityResult
        }

        [Test]
        public async Task AssignRole_ShouldCallAddToRoleAsync()
        {
            // Arrange
            Account user = new Account { UserName = "user1" };
            _userManagerMock.Setup(x => x.AddToRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

            // Act
            await _accountService.AssignRole(user, "Admin");

            // Assert
            _userManagerMock.Verify(x => x.AddToRoleAsync(user, "Admin"), Times.Once);
        }
    }
}
