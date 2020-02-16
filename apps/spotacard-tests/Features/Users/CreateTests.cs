using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Infrastructure.Security;

namespace Spotacard.Features.Users
{
  public class CreateTests : SliceFixture
  {
    [Test]
    public async Task Expect_Create_User()
    {
      var command = new Create.Command
      {
        User = new Create.UserData
        {
          Email = "email",
          Password = "password",
          Username = "username"
        }
      };

      await SendAsync(command);

      var created = await ExecuteDbContextAsync(db =>
        db.Persons.Where(d => d.Email == command.User.Email).SingleOrDefaultAsync());

      Assert.That(created, Is.Not.Null);
      Assert.That(new PasswordHasher().Hash("password", created.Salt), Is.EqualTo(created.Hash));
    }
  }
}
