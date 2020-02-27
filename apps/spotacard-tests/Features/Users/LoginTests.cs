using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Infrastructure.Security;

namespace Spotacard.Features.Users
{
    public class LoginTests
    {
        private readonly TestFixture _fixture = new TestFixture();

        [Test]
        public async Task Expect_Login()
        {
            var salt = Guid.NewGuid().ToByteArray();
            var person = new Person
            {
                Username = "username",
                Email = "email",
                Hash = new PasswordHasher().Hash("password", salt),
                Salt = salt
            };
            await _fixture.InsertAsync(person);

            var command = new Login.Command
            {
                User = new Login.UserData
                {
                    Email = "email",
                    Password = "password"
                }
            };

            var user = await _fixture.SendAsync(command);

            Assert.That(user?.User, Is.Not.Null);
            Assert.That(user.User.Email, Is.EqualTo(command.User.Email));
            Assert.That("username", Is.EqualTo(user.User.Username));
            Assert.That(user.User.Token, Is.Not.Null);
        }
    }
}
