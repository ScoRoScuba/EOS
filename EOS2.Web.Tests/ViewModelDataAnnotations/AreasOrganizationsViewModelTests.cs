namespace EOS2.Web.Tests.ViewModelDataAnnotations
{
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Customers;
    using EOS2.Web.Areas.Organizations.ViewModels.Equipments;
    using EOS2.Web.Areas.Organizations.ViewModels.Instruments;
    using EOS2.Web.Areas.Organizations.ViewModels.PlantAreas;
    using EOS2.Web.Areas.Organizations.ViewModels.Schedules;
    using EOS2.Web.Areas.Organizations.ViewModels.Site;
    using EOS2.Web.Tests.TestStubs;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Mvc;

    using NUnit.Framework;

    using TestingHelpers;

    public class AreasOrganizationsViewModelTests
    {
        [TestFixture]
        public class CustomerEditViewModelTest
        {
            [Test]
            public static void EmptyNameAndPostalCodeModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", string.Empty },
                    { "PostalCode", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);                
            }

            [Test]
            public static void PopulatedNameEmptyPostalCodeModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", "Test Company" },
                    { "PostalCode", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);
            }

            [Test]
            public static void EmptyNamePopulatedPostalCodeModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", "Test Company" },
                    { "PostalCode", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);
            }

            [Test]
            public static void PopulatedNameAndPostalCodeModelIsTrue()
            {
                // Arrange
                var viewModel = new CustomerEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", "Test Company" },
                    { "PostalCode", "Postal Code" }
                };

                const bool CustomerExistsResult = false;

                // We have to do the following because the validation process loads all validation attributes
                // and runs them all in order.   
                // Field level attributes first and then class.  Our class makes use of UniqueOrganization, which 
                // which requires a call to the OrganizationService, so we need that to return true for this test
                using (var container = new UnityContainer())
                {
                    container.RegisterType<IOrganizationsService, StubOrganizationService>(
                        new InjectionConstructor(CustomerExistsResult));

                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);
                    
                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void NameLongerThan120ModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891" },
                    { "PostalCode", "Postal Code" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);
            }
        }

        [TestFixture]
        public class CustomerSiteEditViewModelTests
        {
            [Test]
            public static void EmptyNameAndPostalCodeModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerSiteEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", string.Empty },
                    { "PostalCode", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);                     
            }

            [Test]
            public static void PopulatedNameEmptyPostalCodeModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerSiteEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", "Test Site" },
                    { "PostalCode", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);                     
            }

            [Test]
            public static void EmptyNamePopulatedPostalCodeModelIsFalse()
            {   
                // Arrange
                var viewModel = new CustomerSiteEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", string.Empty },
                    { "PostalCode", "postal code" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);                                     
            }

            [Test]
            public static void PopulatedNameAndPostalCodeModelIsTrue()
            {
                // Arrange
                var viewModel = new CustomerSiteEditViewModel();
                var addressValues = new FormCollection { { "Name", "Test Company" }, { "PostalCode", "Postal Code" } };

                const bool SiteExistsValue = false;

                // We have to do the following because the validation process loads all validation attributes
                // and runs them all in order.   
                // Field level attributes first and then class.  Our class makes use of UniqueOrganization, which 
                // which requires a call to the OrganizationService, so we need that to return true for this test
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ISiteService, StubSiteService>(new InjectionConstructor(SiteExistsValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void NameLongerThan120ModelIsFalse()
            {
                // Arrange
                var viewModel = new CustomerSiteEditViewModel();
                var addressValues = new FormCollection
                {
                    { "Name", "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891" },
                    { "PostalCode", "Postal Code" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, addressValues);

                // Assert
                Assert.False(modelState.IsValid);                
            }
        }

        [TestFixture]
        public class PlantAreaEditViewModelTests
        {
            [Test]
            public static void EmptyNameAndDescriptionModelIsFalse()
            {
                // Arrange
                var viewModel = new PlantAreaEditViewModel();
                var values = new FormCollection
                {
                    { "Name", string.Empty },
                    { "Description", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }

            [Test]
            public static void PopulatedNameEmptyDescriptionModelIsFalse()
            {
                // Arrange
                var viewModel = new PlantAreaEditViewModel();
                var values = new FormCollection
                {
                    { "Name", "Test Plant Area" },
                    { "Description", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }

            [Test]
            public static void EmptyNamePopulatedDescriptionModelIsFalse()
            {
                // Arrange
                var viewModel = new PlantAreaEditViewModel();
                var values = new FormCollection
                {
                    { "Name", string.Empty },
                    { "Description", "Test Description" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }

            [Test]
            public static void PopulatedNameAndDescriptionModelIsTrue()
            {
                // Arrange
                var viewModel = new PlantAreaEditViewModel();
                var values = new FormCollection { { "Name", "Test Plant Area" }, { "Description", "Test Description" } };

                const bool PlantAreaExistsValue = false;

                // We have to do the following because the validation process loads all validation attributes
                // and runs them all in order.   
                // Field level attributes first and then class.  Our class makes use of UniquePlantArea, which 
                // which requires a call to the PlantAreaService, so we need that to return true for this test
                using (var container = new UnityContainer())
                {
                    container.RegisterType<IPlantAreaService, StubPlantAreaService>(new InjectionConstructor(PlantAreaExistsValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void NameLongerThan120ModelIsFalse()
            {
                // Arrange
                var viewModel = new PlantAreaEditViewModel();
                var values = new FormCollection
                {
                    { "Name", "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891" },
                    { "Description", "Test Description" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }
        }

        [TestFixture]
        public class EquipmentEditViewModelTests
        {
            [Test]
            public static void EmptyNameModelIsFalse()
            {
                // Arrange
                var viewModel = new EquipmentEditViewModel();
                var values = new FormCollection
                {
                    { "Name", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }

            [Test]
            public static void PopulatedNameModelIsTrue()
            {
                // Arrange
                var viewModel = new EquipmentEditViewModel();
                var values = new FormCollection { { "Name", "Test Equipment" } };

                const bool EquipmentExistsValue = false;

                // We have to do the following because the validation process loads all validation attributes
                // and runs them all in order.   
                // Field level attributes first and then class.  Our class makes use of UniquePlantArea, which 
                // which requires a call to the PlantAreaService, so we need that to return true for this test
                using (var container = new UnityContainer())
                {
                    container.RegisterType<IEquipmentService, StubEquipmentService>(new InjectionConstructor(EquipmentExistsValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void NameLongerThan120ModelIsFalse()
            {
                // Arrange
                var viewModel = new EquipmentEditViewModel();
                var values = new FormCollection
                {
                    { "Name", "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }
        }

        [TestFixture]
        public class InstrumentEditViewModelTests
        {
            [Test]
            public static void EmptyNameModelIsFalse()
            {
                // Arrange
                var viewModel = new InstrumentEditViewModel();
                var values = new FormCollection
                {
                    { "Name", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }

            public static void PopulatedNameModelIsTrue()
            {
                // Arrange
                var viewModel = new InstrumentEditViewModel();
                var values = new FormCollection { { "Name", "Test Instrument" } };

                const bool ItemExistsValue = false;

                // We have to do the following because the validation process loads all validation attributes
                // and runs them all in order.   
                // Field level attributes first and then class.  Our class makes use of UniquePlantArea, which 
                // which requires a call to the PlantAreaService, so we need that to return true for this test
                using (var container = new UnityContainer())
                {
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(ItemExistsValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void NameLongerThan120ModelIsFalse()
            {
                // Arrange
                var viewModel = new InstrumentEditViewModel();
                var values = new FormCollection
                {
                    { "Name", "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }
        }

        [TestFixture]
        public class ScheduleEditViewModelTests
        {
            [Test]
            public static void EmptyNameModelIsFalse()
            {
                // Arrange
                var viewModel = new ScheduleEditViewModel();
                var values = new FormCollection
                {
                    { "Name", string.Empty }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }

            public static void PopulatedNameModelIsTrue()
            {
                // Arrange
                var viewModel = new ScheduleEditViewModel();
                var values = new FormCollection { { "Name", "Test Schedule" } };

                const bool ItemExistsValue = false;

                // We have to do the following because the validation process loads all validation attributes
                // and runs them all in order.   
                // Field level attributes first and then class.  Our class makes use of UniquePlantArea, which 
                // which requires a call to the PlantAreaService, so we need that to return true for this test
                using (var container = new UnityContainer())
                {
                    container.RegisterType<IScheduleService, StubScheduleService>(new InjectionConstructor(ItemExistsValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void NameLongerThan120ModelIsFalse()
            {
                // Arrange
                var viewModel = new ScheduleEditViewModel();
                var values = new FormCollection
                {
                    { "Name", "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891" }
                };

                // Act
                var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                // Assert
                Assert.False(modelState.IsValid);
            }
        }
    }
}
