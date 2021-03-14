using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.MVC.Test
{
    public class EmployeeRepositoryTests
    {
        //private HttpClient _client;
        //private readonly CustomWebApplicationFactory<Startup> _factory;
        //private RepositoryContext _dbContext;
        //public EmployeeRepositoryTests(RepositoryContext dbContext)
        //{

        //    _factory = new CustomWebApplicationFactory<Startup>();
        //    _dbContext = dbContext;
        //}
        //[SetUp]
        //public void Initialize()
        //{
        //    _client = _factory.CreateClient();

        //}
        public static RepositoryContext GetTestDbContext(string dbName)
        {
            // Create db context options specifying in memory database
            var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            //Use this to instantiate the db context
            return new RepositoryContext(options);

        }

        private RepositoryContext GetTestDatabase()
        {
            //Get the context
            var testContext = GetTestDbContext("TestDb");

            //Add some dummy categories to in memory db
            IEnumerable<Employee> testEmployees = SeedTestData.GetTestEmployees();
            testContext.Employees.AddRange(testEmployees);
            testContext.SaveChanges();
            return testContext;
        }

        //[Test]
        //public void AddEmployee_ShouldAddNewEmployee_ToDatabase()
        //{
        //    //Arrange
        //    Guid testCompanyId = Guid.NewGuid();
        //    DbSet<Employee> employees = new DbSet<Employee>();
        //    var employee = SeedTestData.GetTestEmployee();
        //    employee.CompanyId = testCompanyId;
        //    var mockDbContext = new Mock<RepositoryContext>();
        //    mockDbContext.Setup(x => x.Set<Employee>())
        //        .Returns(employees);
       
        
        //    //act
        //    var repository = new RepositoryManager(mockDbContext.Object);
          
        //    repository.Employee.CreateEmployeeForCompany(testCompanyId,employee);
        //    repository.Save();
        //    var employees = repository.Employee.GetAllEmployees(false);
        //    //assert
        //    Assert.AreEqual(2, employees.Count());
        //  // Assert.Contains(employee, testDbContext.Employees.ToList());
        //}
    }
}
