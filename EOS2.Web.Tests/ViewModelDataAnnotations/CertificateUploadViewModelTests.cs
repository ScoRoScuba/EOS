namespace EOS2.Web.Tests.ViewModelDataAnnotations
{
    using System.Web.Mvc;

    using EOS2.Infrastructure.Interfaces.Services;
    using EOS2.Web.Areas.Organizations.ViewModels.Certificate;
    using EOS2.Web.Tests.TestStubs;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Mvc;

    using NUnit.Framework;

    using TestingHelpers;

    public class CertificateUploadViewModelTests
    {
        [TestFixture]
        public class CertificateEditViewModelTest
        {
            [Test]
            public static void UpdateFullModelIsTrue()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "31/12/2014" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateEmptyCertificateNumberIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "EndDate", "31/12/2014" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateEmptyEndDateIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateEmptyStartDateIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "31/12/2014" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateEmptyTypeIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "31/12/2014" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateEndDateGreaterThanStartDateIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "31/12/2013" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateEndDateEqualToStartDateIsTrue()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "01/01/2014" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.True(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateInvalidEndDateIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "abcdef" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "01/01/2014" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }

            [Test]
            public static void UpdateInvalidStartDateIsFalse()
            {
                // Arrange
                const bool CertificateNumberExistsValue = false;
                const int OrganizationIdValue = 1;
                var viewModel = new CertificateEditViewModel();
                var values = new FormCollection
                                 {
                                     { "CertificateNumber", "CN0001" },
                                     { "EndDate", "31/12/2014" },
                                     { "Id", "1" },
                                     { "InstrumentId", "1" },
                                     { "StartDate", "abcdef" },
                                     { "Type.Id", "1" },
                                     { "DetailViewModel.Id", "0" }
                                 };
                using (var container = new UnityContainer())
                {
                    container.RegisterType<ICertificateService, StubCertificateService>(new InjectionConstructor(CertificateNumberExistsValue));
                    container.RegisterType<IInstrumentService, StubInstrumentService>(new InjectionConstructor(OrganizationIdValue));
                    DependencyResolver.SetResolver(new UnityDependencyResolver(container));

                    // Act
                    var modelState = ModelStateTester.TryUpdateModel(viewModel, values);

                    // Assert
                    Assert.False(modelState.IsValid);
                }
            }
        }
    }
}
