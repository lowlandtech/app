using System;
using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Apps
{
    public class TestCase1001 : ISeeder
    {
        private readonly GraphContext _context;

        public TestCase1001(GraphContext context)
        {
            _context = context;
        }


        public static App App0 =>
            new App
            {
                Id = new Guid("56bc9113-3cb5-483c-b61e-eea54420551f"),
                Name = "TestApp0",
                Organization = "Organization0",
                Prefix = "TA0",
                Namingspace = "Organization.Test.App0"
            };

        public static App App1 =>
            new App
            {
                Id = new Guid("7c61d035-b246-402a-a312-da13279fe4b9"),
                Name = "TestApp1",
                Organization = "Organization1",
                Prefix = "TA1",
                Namingspace = "Organization.Test.App1"
            };

        public static App App2 =>
            new App
            {
                Id = new Guid("446fb80f-86b0-4da6-a028-4058fee0492a"),
                Name = "TestApp2",
                Organization = "Organization2",
                Prefix = "TA2",
                Namingspace = "Organization.Test.App2"
            };

        public static App App3 =>
            new App
            {
                Id = new Guid("96c445b2-d7fe-4462-b5d4-03b9d45803fe"),
                Name = "TestApp3",
                Organization = "Organization3",
                Prefix = "TA3",
                Namingspace = "Organization.Test.App3"
            };

        public static App App4 =>
            new App
            {
                Id = new Guid("085b5ac7-ad4b-4c34-9cf4-4254abebf446"),
                Name = "TestApp4",
                Organization = "Organization4",
                Prefix = "TA4",
                Namingspace = "Organization.Test.App4"
            };

        public static App App5 =>
            new App
            {
                Id = new Guid("7598eccb-aa89-4578-946e-69c99a008326"),
                Name = "TestApp5",
                Organization = "Organization5",
                Prefix = "TA5",
                Namingspace = "Organization.Test.App5"
            };

        public static App App6 =>
            new App
            {
                Id = new Guid("f1aa962a-21a2-4fb3-a21a-09369119f4ec"),
                Name = "TestApp6",
                Organization = "Organization6",
                Prefix = "TA6",
                Namingspace = "Organization.Test.App6"
            };

        public static App App7 =>
            new App
            {
                Id = new Guid("6bd946a9-d222-46b5-8d0c-f6a4ad97d006"),
                Name = "TestApp7",
                Organization = "Organization7",
                Prefix = "TA7",
                Namingspace = "Organization.Test.App7"
            };

        public static App App8 =>
            new App
            {
                Id = new Guid("0438f249-1660-4b94-a27c-a2c6062b2b0f"),
                Name = "TestApp8",
                Organization = "Organization8",
                Prefix = "TA8",
                Namingspace = "Organization.Test.App8"
            };

        public static App App9 =>
            new App
            {
                Id = new Guid("29a17a34-8b2a-466f-8913-9171c6fe758b"),
                Name = "TestApp9",
                Organization = "Organization9",
                Prefix = "TA9",
                Namingspace = "Organization.Test.App9"
            };

        public void Execute()
        {
            _context.Apps.Add(App0.Copy());
            _context.Apps.Add(App1.Copy());
            _context.Apps.Add(App2.Copy());
            _context.Apps.Add(App3.Copy());
            _context.Apps.Add(App4.Copy());
            _context.Apps.Add(App5.Copy());
            _context.Apps.Add(App6.Copy());
            _context.Apps.Add(App7.Copy());
            _context.Apps.Add(App8.Copy());
            _context.Apps.Add(App9.Copy());
            _context.SaveChanges();
        }
    }
}
