using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EventTicketingSystem.Application.DTOs;
using EventTicketingSystem.Application.Services;
using EventTicketingSystem.Domain.Entites;
using EventTicketingSystem.Domain.Enum;
using EventTicketingSystem.Infrastructure.Repositories.IRepositories;

namespace EventTicketingSystem.Tests.Services
{
    [TestFixture]
    public class AdminServicesTests
    {
        private Mock<IAdminRepository> _mockAdminRepository;
        private AdminServices _adminServices;

        [SetUp]
        public void Setup()
        {
            _mockAdminRepository = new Mock<IAdminRepository>();

            _adminServices = new AdminServices(_mockAdminRepository.Object);
        }

        [Test]
        public async Task AssignRole_ShouldReturnTrue_WhenUserAndRoleAreValid()
        {
            // Arrange — fake user
            var fakeUser = new User
            {
                Id = 1,
                Email = "example@gmail.com",
                UserRoles = new List<UserRole>() // no roles assigned yet
            };

            var fakeRole = new Role
            {
                Id = 2,
                RoleName = RoleType.Attendee
            };

            _mockAdminRepository.Setup(r => r.FindUserExistAsync("example@gmail.com"))
                                .ReturnsAsync(fakeUser);
            _mockAdminRepository.Setup(r => r.GetRoleByNameAsync(RoleType.Attendee))
                                .ReturnsAsync(fakeRole);
            _mockAdminRepository.Setup(r => r.AddUserRoleAsync(It.IsAny<UserRole>()))
                                .ReturnsAsync(true);

            var dto = new AssignRoleDto
            {
                Email = "example@gmail.com",
                RoleName = "Attendee"
            };

            // Act
            var result = await _adminServices.AssignRole(dto);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AssignRole_ShouldReturnFalse_WhenUserNotFound()
        {
            // Arrange — null user
            _mockAdminRepository.Setup(r => r.FindUserExistAsync(It.IsAny<string>()))
                                .ReturnsAsync((User)null);

            var dto = new AssignRoleDto
            {
                Email = "missing@gmail.com",
                RoleName = "Attendee"
            };

            // Act
            var result = await _adminServices.AssignRole(dto);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
