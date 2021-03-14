﻿using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.MVC.Test
{
    public class SeedTestData
    {
        public static void PopulateTestData(RepositoryContext dbContext)
        {
            Employee testEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                CompanyId = Guid.NewGuid(),
                Name = "Jos"
            };
            dbContext.Employees.Add(testEmployee);
            //dbContext.Players.Add(new Player("Jos", "De Klos", 183, 84, new DateTime(1961, 1, 26)) { Id = 7, Created = DateTime.UtcNow });
            //dbContext.Players.Add(new Player("Joke", "De Klos", 193, 91, new DateTime(1965, 11, 5)) { Id = 8, Created = DateTime.UtcNow });
            dbContext.SaveChanges();
        }
        public static IEnumerable<Employee> GetTestEmployees()
        {
            return new List<Employee>()
                {
                    new Employee()
                    {
                        Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                        CompanyId = Guid.NewGuid(),
                        Name = "John"
                    },
                    new Employee()
                    {
                        Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                        CompanyId = Guid.NewGuid(),
                        Name = "Doe"
                    }
                };
        }
        public static Employee GetTestEmployee()
        {
            return SeedTestData.GetTestEmployees().FirstOrDefault();
        }
        //private IEnumerable<Employee> GetTestEmployees()
        //{
        //    return new List<Employee>()
        //        {
        //            new Employee()
        //            {
        //                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
        //                CompanyId = Guid.NewGuid(),
        //                Name = "John"
        //            },
        //            new Employee()
        //            {
        //                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
        //                CompanyId = Guid.NewGuid(),
        //                Name = "Doe"
        //            }
        //        };
        //}
        public static IEnumerable<Company> GetTestCompanies()
        {
            return new List<Company>()
            {
                new Company
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "IT_Solutions Ltd",
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                    Country = "USA"
                },
                 new Company
                 {
                     Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                     Name = "Admin_Solutions Ltd",
                     Address = "312 Forest Avenue, BF 923",
                     Country = "USA"
                 }
            };
        }
    }
}
