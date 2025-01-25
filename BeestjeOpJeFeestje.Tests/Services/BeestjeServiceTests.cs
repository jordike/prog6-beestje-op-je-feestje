using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Services.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeestjeOpJeFeestje.Tests
{
    [TestFixture]
    public class BeestjeServiceTests : IDisposable
    {
        private BeestjeService _beestjeService;
        private BeestjeOpJeFeestjeContext _context;

        [SetUp]
        public void SetUp()
        {
            // Use a unique database name for each test
            var options = new DbContextOptionsBuilder<BeestjeOpJeFeestjeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BeestjeOpJeFeestjeContext(options);
            _beestjeService = new BeestjeService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the context to free resources
            _context.Dispose();
        }

        public void Dispose()
        {
            // Dispose of the context if it hasn't been disposed already
            _context?.Dispose();
        }

        [Test]
        public void GetAnimals_ShouldReturnAllAnimals()
        {
            // Arrange
            var animals = new List<Animal>
            {
                new Animal { Id = 1, Name = "Lion", Type = "Mammal", Price = 100.0f, ImageURL = "http://example.com/lion.jpg" },
                new Animal { Id = 2, Name = "Tiger", Type = "Mammal", Price = 150.0f, ImageURL = "http://example.com/tiger.jpg" }
            };

            _context.Animals.AddRange(animals);
            _context.SaveChanges();

            // Act
            var result = _beestjeService.GetAnimals();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Lion", result[0].Name);
            Assert.AreEqual("Tiger", result[1].Name);
        }

        [Test]
        public void GetAnimal_ShouldReturnAnimal_WhenAnimalExists()
        {
            // Arrange
            var animal = new Animal { Id = 1, Name = "Lion", Type = "Mammal", Price = 100.0f, ImageURL = "http://example.com/lion.jpg" };
            _context.Animals.Add(animal);
            _context.SaveChanges();

            // Act
            var result = _beestjeService.GetAnimal(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Lion", result.Name);
        }

        [Test]
        public void GetAnimal_ShouldReturnNull_WhenAnimalDoesNotExist()
        {
            // Act
            var result = _beestjeService.GetAnimal(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CreateAnimal_ShouldAddAnimal()
        {
            // Arrange
            var animal = new Animal
            {
                Id = 1,
                Name = "Lion",
                Type = "Mammal",
                Price = 100.0f,
                ImageURL = "http://example.com/lion.jpg"
            };

            // Act
            _beestjeService.CreateAnimal(animal);

            // Assert
            var result = _context.Animals.Find(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Lion", result.Name);
            Assert.AreEqual("Mammal", result.Type);
            Assert.AreEqual(100.0f, result.Price);
            Assert.AreEqual("http://example.com/lion.jpg", result.ImageURL);
        }

        [Test]
        public void UpdateAnimal_ShouldUpdateAnimal()
        {
            // Arrange
            var animal = new Animal
            {
                Id = 1,
                Name = "Lion",
                Type = "Mammal",
                Price = 100.0f,
                ImageURL = "http://example.com/lion.jpg"
            };
            _context.Animals.Add(animal);
            _context.SaveChanges();

            animal.Name = "Tiger";
            animal.Type = "Mammal";
            animal.Price = 150.0f;
            animal.ImageURL = "http://example.com/tiger.jpg";

            // Act
            _beestjeService.UpdateAnimal(animal);

            // Assert
            var updatedAnimal = _context.Animals.Find(1);
            Assert.IsNotNull(updatedAnimal);
            Assert.AreEqual("Tiger", updatedAnimal.Name);
            Assert.AreEqual("Mammal", updatedAnimal.Type);
            Assert.AreEqual(150.0f, updatedAnimal.Price);
            Assert.AreEqual("http://example.com/tiger.jpg", updatedAnimal.ImageURL);
        }

        [Test]
        public void DeleteAnimal_ShouldRemoveAnimal()
        {
            // Arrange
            var animal = new Animal
            {
                Id = 1,
                Name = "Lion",
                Type = "Mammal",
                Price = 100.0f,
                ImageURL = "http://example.com/lion.jpg"
            };
            _context.Animals.Add(animal);
            _context.SaveChanges();

            // Act
            _beestjeService.DeleteAnimal(animal);

            // Assert
            var result = _context.Animals.Find(1);
            Assert.IsNull(result);
        }
    }
}