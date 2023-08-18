using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UserFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Id = 1,
                Name = "Test User 1",
                Email = "test.user.1@gmail.com",
                Address = new Address
                {
                    Street = "123 Main Street",
                    City = "SomeWhere",
                    ZipCode = "12344"
                }
            },
            new User
            {
                Id = 2,
                Name = "Test User 2",
                Email = "test.user.2@gmail.com",
                Address = new Address
                {
                    Street = "123 Second Street",
                    City = "SomeWhere T",
                    ZipCode = "345345"
                }
            },
            new User
            {
                Id = 3,
                Name = "Test User 3",
                Email = "test.user.3@gmail.com",
                Address = new Address
                {
                    Street = "123 Third Street",
                    City = "SomeWhere S",
                    ZipCode = "4335345"
                }
            }
        };
    }
}
