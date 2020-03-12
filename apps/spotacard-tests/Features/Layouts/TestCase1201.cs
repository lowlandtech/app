using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Layouts
{
    public class TestCase1201 : ISeeder
    {
        private readonly GraphContext _context;

        public TestCase1201(GraphContext context)
        {
            _context = context;
        }


        public static Layout Layout0 =>
            new Layout
            {
                Id = new Guid("187436af-e24b-4e14-b4c5-a5d7c5fea2ac"),
                Name = "TestLayout0",
                Packages = "Packages0",
                Wiring = "Wiring0",
                CodeBehind = "CodeBehind0",
                Styling = "Styling0",
                Markup = "Markup0",
            };

        public static Layout Layout1 =>
            new Layout
            {
                Id = new Guid("7ec4641e-f12b-4c34-a644-ac5b33757520"),
                Name = "TestLayout1",
                Packages = "Packages1",
                Wiring = "Wiring1",
                CodeBehind = "CodeBehind1",
                Styling = "Styling1",
                Markup = "Markup1",
            };

        public static Layout Layout2 =>
            new Layout
            {
                Id = new Guid("a1b99bb4-e0bb-43fc-a71a-ca3f7e44249f"),
                Name = "TestLayout2",
                Packages = "Packages2",
                Wiring = "Wiring2",
                CodeBehind = "CodeBehind2",
                Styling = "Styling2",
                Markup = "Markup2",
            };

        public static Layout Layout3 =>
            new Layout
            {
                Id = new Guid("5acba65d-67b2-4069-925c-56f42cd48523"),
                Name = "TestLayout3",
                Packages = "Packages3",
                Wiring = "Wiring3",
                CodeBehind = "CodeBehind3",
                Styling = "Styling3",
                Markup = "Markup3",
            };

        public static Layout Layout4 =>
            new Layout
            {
                Id = new Guid("521dc902-4e4a-4094-966d-c2a1e05ee62c"),
                Name = "TestLayout4",
                Packages = "Packages4",
                Wiring = "Wiring4",
                CodeBehind = "CodeBehind4",
                Styling = "Styling4",
                Markup = "Markup4",
            };

        public static Layout Layout5 =>
            new Layout
            {
                Id = new Guid("48a4f967-f79f-4cf3-8c69-b0229cc030d5"),
                Name = "TestLayout5",
                Packages = "Packages5",
                Wiring = "Wiring5",
                CodeBehind = "CodeBehind5",
                Styling = "Styling5",
                Markup = "Markup5",
            };

        public static Layout Layout6 =>
            new Layout
            {
                Id = new Guid("6412373a-a8e6-47f2-96ef-41a003eb1921"),
                Name = "TestLayout6",
                Packages = "Packages6",
                Wiring = "Wiring6",
                CodeBehind = "CodeBehind6",
                Styling = "Styling6",
                Markup = "Markup6",
            };

        public static Layout Layout7 =>
            new Layout
            {
                Id = new Guid("18227050-dbd8-498d-883f-2ac0aa03c3b4"),
                Name = "TestLayout7",
                Packages = "Packages7",
                Wiring = "Wiring7",
                CodeBehind = "CodeBehind7",
                Styling = "Styling7",
                Markup = "Markup7",
            };

        public static Layout Layout8 =>
            new Layout
            {
                Id = new Guid("ab021c83-0588-4aa6-b6f6-ca2f907682f9"),
                Name = "TestLayout8",
                Packages = "Packages8",
                Wiring = "Wiring8",
                CodeBehind = "CodeBehind8",
                Styling = "Styling8",
                Markup = "Markup8",
            };

        public static Layout Layout9 =>
            new Layout
            {
                Id = new Guid("57b3c4de-6bb6-4c45-8b6b-78f63e3846dc"),
                Name = "TestLayout9",
                Packages = "Packages9",
                Wiring = "Wiring9",
                CodeBehind = "CodeBehind9",
                Styling = "Styling9",
                Markup = "Markup9",
            };

        public void Execute()
        {
            _context.Layouts.Add(Layout0.Copy());
            _context.Layouts.Add(Layout1.Copy());
            _context.Layouts.Add(Layout2.Copy());
            _context.Layouts.Add(Layout3.Copy());
            _context.Layouts.Add(Layout4.Copy());
            _context.Layouts.Add(Layout5.Copy());
            _context.Layouts.Add(Layout6.Copy());
            _context.Layouts.Add(Layout7.Copy());
            _context.Layouts.Add(Layout8.Copy());
            _context.Layouts.Add(Layout9.Copy());
            _context.SaveChanges();
        }
    }
}
