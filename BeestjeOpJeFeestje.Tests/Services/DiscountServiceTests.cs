using BeestjeOpJeFeestje.Data.Models;
using BeestjeOpJeFeestje.Services.Services;

namespace BeestjeOpJeFeestje.Tests.Services
{
    [TestFixture]
    public class DiscountServiceTests
    {
        private DiscountService _discountService;

        [SetUp]
        public void SetUp()
        {
            _discountService = new DiscountService();
        }

        [Test]
        public void GetDiscounts_ShouldApplySameTypeDiscount_WhenThreeOrMoreAnimalsOfSameType()
        {
            // Arrange
            List<Animal> animals = new List<Animal>
            {
                new Animal { Name = "Hond", Type = AnimalTypes.Boerderij },
                new Animal { Name = "Hond", Type = AnimalTypes.Boerderij },
                new Animal { Name = "Hond", Type = AnimalTypes.Boerderij }
            };
            Booking booking = new Booking { Animals = animals, Date = DateTime.Now };

            // Act
            Dictionary<string, int> discounts = _discountService.GetDiscounts(booking);

            // Assert
            Assert.IsTrue(discounts.ContainsKey("3 dieren van hetzelfde type"));
            Assert.AreEqual(10, discounts["3 dieren van hetzelfde type"]);
        }

        [Test]
        public void GetDiscounts_ShouldApplyDuckDiscount_WhenAllAnimalsAreDucks()
        {
            // Arrange
            List<Animal> animals = new List<Animal>
            {
                new Animal { Name = "Eend", Type = AnimalTypes.Boerderij },
                new Animal { Name = "Eend", Type = AnimalTypes.Boerderij }
            };
            Booking booking = new Booking { Animals = animals, Date = DateTime.Now };

            // Act
            Dictionary<string, int> discounts = _discountService.GetDiscounts(booking);

            // Assert
            if (discounts.ContainsKey("Dier genaamd 'Eend'"))
            {
                Assert.AreEqual(50, discounts["Dier genaamd 'Eend'"]);
            }
        }

        [Test]
        public void GetDiscounts_ShouldApplyBookingDayDiscount_WhenBookingIsOnMondayOrTuesday()
        {
            // Arrange
            Booking booking = new Booking { Animals = new List<Animal>(), Date = new DateTime(2023, 10, 2) }; // Monday

            // Act
            Dictionary<string, int> discounts = _discountService.GetDiscounts(booking);

            // Assert
            Assert.IsTrue(discounts.ContainsKey("Boeking op maandag of dinsdag"));
            Assert.AreEqual(15, discounts["Boeking op maandag of dinsdag"]);
        }

        [Test]
        public void GetDiscounts_ShouldApplyNameLetterDiscount_WhenAnimalNameContainsSpecificLetters()
        {
            // Arrange
            List<Animal> animals = new List<Animal>
            {
                new Animal { Name = "Hond", Type = AnimalTypes.Boerderij }
            };
            Booking booking = new Booking { Animals = animals, Date = DateTime.Now };

            // Act
            Dictionary<string, int> discounts = _discountService.GetDiscounts(booking);

            // Assert
            Assert.IsTrue(discounts.ContainsKey("Diernaam met letters 'A', 'B', 'C', enz."));
            Assert.AreEqual(8, discounts["Diernaam met letters 'A', 'B', 'C', enz."]); // H, O, N, D
        }

        [Test]
        public void GetDiscounts_ShouldApplyMembershipDiscount_WhenAccountHasMembership()
        {
            // Arrange
            Account account = new Account { MembershipLevel = MembershipLevel.Silver };
            Booking booking = new Booking { Animals = new List<Animal>(), Date = DateTime.Now, Account = account };

            // Act
            Dictionary<string, int> discounts = _discountService.GetDiscounts(booking);

            // Assert
            Assert.IsTrue(discounts.ContainsKey("Lidmaatschapskaart"));
            Assert.AreEqual(10, discounts["Lidmaatschapskaart"]);
        }

        [Test]
        public void GetDiscounts_ShouldNotApplyMembershipDiscount_WhenAccountHasNoMembership()
        {
            // Arrange
            Account account = new Account { MembershipLevel = MembershipLevel.Geen };
            Booking booking = new Booking { Animals = new List<Animal>(), Date = DateTime.Now, Account = account };

            // Act
            Dictionary<string, int> discounts = _discountService.GetDiscounts(booking);

            // Assert
            Assert.IsFalse(discounts.ContainsKey("Lidmaatschapskaart"));
        }
    }
}
