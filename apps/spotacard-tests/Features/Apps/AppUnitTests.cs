using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Spotacard.Domain;
using Spotacard.Features.Apps.Commands;

namespace Spotacard.Features.Apps
{
    public class AppUnitTests
    {
        [Test]
        public async Task Expect_Create_App()
        {
            var fixture = new TestFixture();
            var command = new Create.Command
            {
                App = new Create.AppData
                {
                    Name = "TestApp",
                    Organization = "Organization",
                    Prefix = "TA",
                    Namingspace = "Organization.Test.App"
                }
            };

            var app = await AppHelpers.CreateApp(fixture, command);

            Assert.That(app, Is.Not.Null);
            Assert.That(app.Name, Is.EqualTo(command.App.Name));
            Assert.That(app.Organization, Is.EqualTo(command.App.Organization));
            Assert.That(app.Prefix, Is.EqualTo(command.App.Prefix));
            Assert.That(app.Namingspace, Is.EqualTo(command.App.Namingspace));
        }

        [Test]
        public async Task Expect_Edit_App()
        {
            var fixture = new TestFixture();
            try
            {
                var command = new Create.Command
                {
                    App = new Create.AppData
                    {
                        Name = "TestApp",
                        Organization = "Organization",
                        Prefix = "TA",
                        Namingspace = "Organization.Test.App"
                    }
                };

                var created = await AppHelpers.CreateApp(fixture, command);
                var editCommand = new Edit.Command
                {
                    App = new Edit.AppData
                    {
                        Name = "Updated " + created.Name,
                        Organization = "Updated" + created.Organization,
                        Prefix = "Updated" + created.Prefix,
                        Namingspace = "Updated" + created.Namingspace
                    },
                    AppId = created.Id
                };

                var handler = new Edit.Handler(fixture.GetContext());
                var edited = await handler.Handle(editCommand, new CancellationToken());

                Assert.That(edited, Is.Not.Null);
                Assert.That(edited.App.Name, Is.EqualTo(editCommand.App.Name));
                Assert.That(edited.App.Organization, Is.EqualTo(editCommand.App.Organization));
                Assert.That(edited.App.Prefix, Is.EqualTo(editCommand.App.Prefix));
                Assert.That(edited.App.Namingspace, Is.EqualTo(editCommand.App.Namingspace));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Expect_Delete_App()
        {
            // arrange
            var fixture = new TestFixture();
            var createCmd = new Create.Command
            {
                App = new Create.AppData
                {
                    Name = "TestApp",
                    Organization = "Organization",
                    Prefix = "TA"
                }
            };

            var created = await AppHelpers.CreateApp(fixture, createCmd);
            var deleteCmd = new Delete.Command(created.Id);
            var handler = new Delete.QueryHandler(fixture.GetContext());
            await handler.Handle(deleteCmd, new CancellationToken());

            // act
            var deleted = await fixture.ExecuteDbContextAsync(context => context.Apps
                .Where(app => app.Id == deleteCmd.AppId)
                .SingleOrDefaultAsync());

            // assert
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public void Expect_Create_App_Name_Is_Required()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var app = new App();
                context.Apps.Add(app);
                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public void Expect_Create_App_Should_Generate_Id()
        {
            var fixture = new TestFixture();
            try
            {
                var context = fixture.GetContext();
                var app = new App
                {
                    Name = "TestApp",
                    Organization = "Organization",
                    Prefix = "TA",
                    Namingspace = "Organization.Test.App"
                };
                context.Apps.Add(app);
                context.SaveChanges();

                Assert.That(Guid.Empty, Is.Not.EqualTo(app.Id));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
