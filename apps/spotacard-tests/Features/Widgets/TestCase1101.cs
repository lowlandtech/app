using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Widgets
{
    public class TestCase1101 : ISeeder
    {
        private readonly GraphContext _context;

        public TestCase1101(GraphContext context)
        {
            _context = context;
        }


        public static Widget Widget0 =>
            new Widget
            {
                Id = new Guid("187436af-e24b-4e14-b4c5-a5d7c5fea2ac"),
                Name = "TestWidget0",
                Packages = "Packages0",
                Wiring = "Wiring0",
                CodeBehind = "CodeBehind0",
                Styling = "Styling0",
                Markup = "Markup0",
            };

        public static Widget Widget1 =>
            new Widget
            {
                Id = new Guid("7ec4641e-f12b-4c34-a644-ac5b33757520"),
                Name = "TestWidget1",
                Packages = "Packages1",
                Wiring = "Wiring1",
                CodeBehind = "CodeBehind1",
                Styling = "Styling1",
                Markup = "Markup1",
            };

        public static Widget Widget2 =>
            new Widget
            {
                Id = new Guid("a1b99bb4-e0bb-43fc-a71a-ca3f7e44249f"),
                Name = "TestWidget2",
                Packages = "Packages2",
                Wiring = "Wiring2",
                CodeBehind = "CodeBehind2",
                Styling = "Styling2",
                Markup = "Markup2",
            };

        public static Widget Widget3 =>
            new Widget
            {
                Id = new Guid("5acba65d-67b2-4069-925c-56f42cd48523"),
                Name = "TestWidget3",
                Packages = "Packages3",
                Wiring = "Wiring3",
                CodeBehind = "CodeBehind3",
                Styling = "Styling3",
                Markup = "Markup3",
            };

        public static Widget Widget4 =>
            new Widget
            {
                Id = new Guid("521dc902-4e4a-4094-966d-c2a1e05ee62c"),
                Name = "TestWidget4",
                Packages = "Packages4",
                Wiring = "Wiring4",
                CodeBehind = "CodeBehind4",
                Styling = "Styling4",
                Markup = "Markup4",
            };

        public static Widget Widget5 =>
            new Widget
            {
                Id = new Guid("48a4f967-f79f-4cf3-8c69-b0229cc030d5"),
                Name = "TestWidget5",
                Packages = "Packages5",
                Wiring = "Wiring5",
                CodeBehind = "CodeBehind5",
                Styling = "Styling5",
                Markup = "Markup5",
            };

        public static Widget Widget6 =>
            new Widget
            {
                Id = new Guid("6412373a-a8e6-47f2-96ef-41a003eb1921"),
                Name = "TestWidget6",
                Packages = "Packages6",
                Wiring = "Wiring6",
                CodeBehind = "CodeBehind6",
                Styling = "Styling6",
                Markup = "Markup6",
            };

        public static Widget Widget7 =>
            new Widget
            {
                Id = new Guid("18227050-dbd8-498d-883f-2ac0aa03c3b4"),
                Name = "TestWidget7",
                Packages = "Packages7",
                Wiring = "Wiring7",
                CodeBehind = "CodeBehind7",
                Styling = "Styling7",
                Markup = "Markup7",
            };

        public static Widget Widget8 =>
            new Widget
            {
                Id = new Guid("ab021c83-0588-4aa6-b6f6-ca2f907682f9"),
                Name = "TestWidget8",
                Packages = "Packages8",
                Wiring = "Wiring8",
                CodeBehind = "CodeBehind8",
                Styling = "Styling8",
                Markup = "Markup8",
            };

        public static Widget Widget9 =>
            new Widget
            {
                Id = new Guid("57b3c4de-6bb6-4c45-8b6b-78f63e3846dc"),
                Name = "TestWidget9",
                Packages = "Packages9",
                Wiring = "Wiring9",
                CodeBehind = "CodeBehind9",
                Styling = "Styling9",
                Markup = "Markup9",
            };

        public void Execute()
        {
            _context.Widgets.Add(Widget0.Copy());
            _context.Widgets.Add(Widget1.Copy());
            _context.Widgets.Add(Widget2.Copy());
            _context.Widgets.Add(Widget3.Copy());
            _context.Widgets.Add(Widget4.Copy());
            _context.Widgets.Add(Widget5.Copy());
            _context.Widgets.Add(Widget6.Copy());
            _context.Widgets.Add(Widget7.Copy());
            _context.Widgets.Add(Widget8.Copy());
            _context.Widgets.Add(Widget9.Copy());
            _context.SaveChanges();
        }
    }
}
