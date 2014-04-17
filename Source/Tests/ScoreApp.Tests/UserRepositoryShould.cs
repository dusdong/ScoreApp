using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ScoreApp.Application;
using ScoreApp.Domain;
using ScoreApp.Domain.Services;
using ScoreApp.Infrastructure.Data;
using System.Linq;

namespace ScoreApp.Tests
{
    [TestClass]
    public class UserRepositoryShould
    {
        private Mock<ISettings> settings;

        [TestInitialize]
        public void Initialize()
        {
            settings = new Mock<ISettings>();
            settings.Setup(s => s.AppId).Returns("534db88a7c34a");
            settings.Setup(s => s.AuthenticationToken).Returns("fbZ-rA7zT8uAiBDOyupzng");
        }

        private IUserRepository CreateRepository()
        {
            var factory = new UserAppFactory(settings.Object);
            var search = new ImageSearch(factory);
            return new UserRepository(factory, search);
        }

        [TestMethod]
        public void Return_User_With_Image_When_Calling_GetById()
        {
            //Arrange
            var repository = CreateRepository();

            //Act
            var user = repository.GetById("JSTR_7tPTPqLJcs_33Ihgg");

            //Assert
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Image);
        }

        [TestMethod]
        public void Return_Users_With_Images_When_Calling_GetAll()
        {
            //Arrange
            var repository = CreateRepository();

            //Act
            var users = repository.GetAll();

            //Assert
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Select(u => u.Image).All(s => s != null));
        }
    }
}
