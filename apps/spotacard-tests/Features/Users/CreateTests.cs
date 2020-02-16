using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Infrastructure.Security;
using System.Linq;
using System.Threading.Tasks;

namespace Spotacard.Features.Users
{
  public class CreateTests
  {
    private readonly SliceFixture _fixture = new SliceFixture();

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

      await _fixture.SendAsync(command);

      var created = await _fixture.ExecuteDbContextAsync(_graph => _graph.Persons
        .Where(_person => _person.Email == command.User.Email)
        .SingleOrDefaultAsync());

      Assert.That(created, Is.Not.Null);
      Assert.That(new PasswordHasher().Hash("password", created.Salt), Is.EqualTo(created.Hash));
    }
  }
}
