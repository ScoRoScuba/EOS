namespace EOS2.Common.Tests
{
    using System;
    using System.Linq;

    using EOS2.Common.Validation;
    
    using NUnit.Framework;

    public class ErrorStateCollectionTests
    {
        [TestFixture]
        public class CollectionExceptionHandling
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Condition of Test")]
            [Test]
            public static void AddsExceptionToCollection()
            {
                var errorCollection = new ErrorStateCollection();

                errorCollection.Add(new Exception("general exception"));

                Assert.That(errorCollection.Any(), Is.True);

                var result = errorCollection.SingleOrDefault(err => err.Exception.Message == "general exception");
                Assert.That(result, Is.Not.Null);
                // ReSharper disable once PossibleNullReferenceException
                Assert.That(result.ErrorMessage, Is.Empty);
            }
        }

        [TestFixture]
        public class AddMethodWithMessage
        {
            [Test]
            public static void AddsCollectionToCollection()
            {
                var errorCollection = new ErrorStateCollection();

                errorCollection.Add("error message");

                Assert.That(errorCollection.Any(), Is.True);
                Assert.That(errorCollection.SingleOrDefault(err => err.ErrorMessage == "error message"), Is.Not.Null);
            }
        }
    }
}
