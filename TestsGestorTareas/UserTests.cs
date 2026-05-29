using GestorDeTareas.Domain.Entities;

namespace TestsGestorTareas
{
    internal class UserTests
    {

        private User _userTest;

        [SetUp]
        public void Setup()
        {
            _userTest = new User("Ivan", "hashed_password_123", "ivan@gmail.com", new DateOnly(1990, 05, 15), false);
        }

        [Test]
        public void Constructor_FutureBirthdate_ThrowsArgumentException()
        {
            //Arrange
            var birthdate = DateOnly.FromDateTime(DateTime.Today).AddDays(5);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new User("Ivan", "hashed_password_123", "ivan@gmail.com", birthdate, false));
        }

        [Test]
        public void Constructor_StringEmpty_ThrowsArgumentException()
        {
            // Arrange
            var name = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new User(name, "hashed_password_123", "ivan@gmail.com", new DateOnly(1990, 5, 15), false));
        }

        [Test]
        public void Constructor_StringWhiteSpace_ThrowsArgumentException()
        {
            // Arrange
            var name = " ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new User(name, "hashed_password_123", "ivan@gmail.com", new DateOnly(1990, 5, 15), false));
        }

        [Test]
        public void ChangeName_ValidName_UpdateName()
        {
            // Act
            _userTest.ChangeName("Pedro");

            // Assert
            Assert.That(_userTest.Username, Is.EqualTo("Pedro"));
        }

        [Test]
        public void ChangeName_EmptyName_ThrowsArgumentException()
        {
            // Arrange
            var name = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _userTest.ChangeName(name));
        }

        [Test]
        public void Constructor_InvalidEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            var email = "invalidmail.com";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new User("Ivan", "hashed_password_123", email, new DateOnly(1990, 5, 15), false));

        }

        [Test]
        public void ChangeEmail_InvalidEmail_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _userTest.ChangeEmail("invalid.com"));
        }
    }
}
