using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Users
{
  public class LoginTests : SliceFixture
  {
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
      await InsertAsync(person);

      var command = new Login.Command
      {
        User = new Login.UserData
        {
          Email = "email",
          Password = "password"
        }
      };

      var user = await SendAsync(command);

      Assert.That(user?.User, Is.Not.Null);
      Assert.That(user.User.Email, Is.EqualTo(command.User.Email));
      Assert.That("username", Is.EqualTo(user.User.Username));
      Assert.That(user.User.Token, Is.Not.Null);
    }
  }
}
