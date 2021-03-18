using Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.MVC.Test
{
    public class CompanyManagerControllerTests
    {
        private Mock<IRepositoryManager> mockRepo;
        [SetUp]
        public void Initialize()
        {
            mockRepo = new Mock<IRepositoryManager>();
            mockRepo.Setup(repo => repo.Company.GetAllCompanies(false))
                .Returns(SeedTestData.GetTestCompanies());
        }
        [Test]
        public void Index_ReturnsAViewResult_WithListOfCompanies()
        { 
            //Oefening: maak aan het voorbeeld van de EmployeeMangerControllerTest
            //de test methode voor Index van CompanyManagerController
        }
    }
}
