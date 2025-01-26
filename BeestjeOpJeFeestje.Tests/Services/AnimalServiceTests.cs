using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Services.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeestjeOpJeFeestje.Tests.Services
{
    [TestFixture]
    public class AnimalServiceTests
    {
        private AnimalService _animalService;
        private Mock<BeestjeOpJeFeestjeContext> _contextMock;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BeestjeOpJeFeestjeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _contextMock = new Mock<BeestjeOpJeFeestjeContext>(options);
            _animalService = new AnimalService(_contextMock.Object);
        }

        [Test]
        public void GetAnimals_ShouldReturnAllAnimals()
        {
            // Arrange
            var animals = new List<Animal>
            {
                new Animal { Id = 1, Name = "Lion" },
                new Animal { Id = 2, Name = "Elephant" }
            };
            var animalsDbSetMock = DbSetExtensions.ReturnsDbSet(animals);
            _contextMock.Setup(c => c.Animals).Returns(animalsDbSetMock);

            // Act
            var result = _animalService.GetAnimals();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Lion"));
            Assert.That(result[1].Name, Is.EqualTo("Elephant"));
        }

        [Test]
        public void GetAnimal_ShouldReturnAnimal_WhenAnimalExists()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion" };
            var animals = new List<Animal> { animal };
            _contextMock.Setup(c => c.Animals).Returns(DbSetExtensions.ReturnsDbSet(animals));

            // Act
            var result = _animalService.GetAnimal(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Lion"));
        }

        [Test]
        public void GetAnimal_ShouldReturnNull_WhenAnimalDoesNotExist()
        {
            // Arrange
            var animals = new List<Animal>();
            _contextMock.Setup(c => c.Animals).Returns(DbSetExtensions.ReturnsDbSet(animals));

            // Act
            var result = _animalService.GetAnimal(1);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAnimalBookings_ShouldReturnAnimalBookings_WhenAnimalExists()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion" };
            var animals = new List<Animal> { animal };
            var booking1 = new Booking { Id = 1, Animals = animals };
            var booking2 = new Booking { Id = 2, Animals = animals };
            var bookings = new List<Booking> { booking1, booking2 };
            _contextMock.Setup(c => c.Bookings).Returns(DbSetExtensions.ReturnsDbSet(bookings));

            // Act
            var result = _animalService.GetAnimalBookings(animal);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Contains(booking1), Is.True);
            Assert.That(result.Contains(booking2), Is.True);
        }

        [Test]
        public void GetAnimalBookings_ShouldReturnNull_WhenAnimalDoesNotExist()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion" };
            var bookings = new List<Booking>();
            _contextMock.Setup(c => c.Bookings).Returns(DbSetExtensions.ReturnsDbSet(bookings));

            // Act
            var result = _animalService.GetAnimalBookings(animal);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAnimalTypes_ShouldReturnAllAnimalTypes()
        {
            // Act
            var result = _animalService.getAnimalTypes();

            // Assert
            Assert.That(result.Length, Is.EqualTo(Enum.GetValues(typeof(AnimalTypes)).Length));
        }

        [Test]
        public void CreateAnimal_ShouldAddAnimalToContext()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion" };

            // Act
            _animalService.CreateAnimal(animal);

            // Assert
            _contextMock.Verify(c => c.Animals.Add(animal), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateAnimal_ShouldUpdateAnimalInContext()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion" };

            // Act
            _animalService.UpdateAnimal(animal);

            // Assert
            _contextMock.Verify(c => c.Animals.Update(animal), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void DeleteAnimal_ShouldRemoveAnimalFromContext()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion" };

            // Act
            _animalService.DeleteAnimal(animal);

            // Assert
            _contextMock.Verify(c => c.Animals.Remove(animal), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }

    public static class DbSetExtensions
    {
        public static DbSet<T> ReturnsDbSet<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return dbSetMock.Object;
        }
    }
}
