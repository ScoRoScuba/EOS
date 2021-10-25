namespace EOS.Common.Tests.Model 
{
    using System.Diagnostics.CodeAnalysis;

    using EOS2.Identity.Model;
    using NUnit.Framework;

    [TestFixture]
    public class EOS2IdentityModelTests
    {
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fullname", Justification = "Test Function Naming")]
        [Test]
        public void UserWhenNameAndFamilyNameReturnsFullnameCorrectly()
        {
            var user = new User { Name = "Steve", FamilyName = "Smith" };

            Assert.That(user.FullName, Is.EqualTo("Steve Smith"));
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fullname", Justification = "Test Function Naming")]
        [Test]
        public void UserWhenNameMiddleNameAndFamilyNameReturnsFullnameCorrectly()
        {
            var user = new User { Name = "Steve", MiddleName = "B", FamilyName = "Smith" };

            Assert.That(user.FullName, Is.EqualTo("Steve B Smith"));        
        }
    }
}
