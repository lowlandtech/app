using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Contents
{
    public class TestCase8101 : ISeeder
    {
        private readonly GraphContext _context;

        public TestCase8101(GraphContext context)
        {
            _context = context;
        }

        public static readonly Card Template = new Card
        {
            Id = new Guid("00c04b8e-7f13-4d61-a21b-6de03eced910"),
            Title = "Template 1",
            Slug = "template-1",
            Type = CardTypes.Template
        };


        public static Content Content0 =>
            new Content
            {
                Id = new Guid("b18e9cde-2df2-4156-ac1f-55ddadd5cd17"),
                Name = "TestContent0",
                CardId = Template.Id
            };

        public static Content Content1 =>
            new Content
            {
                Id = new Guid("0adc528c-b573-415c-9243-f7633c76328d"),
                Name = "TestContent1",
                CardId = Template.Id
            };

        public static Content Content2 =>
            new Content
            {
                Id = new Guid("088ee577-2e73-4052-84ac-e1d56bbf3ab2"),
                Name = "TestContent2",
                CardId = Template.Id
            };

        public static Content Content3 =>
            new Content
            {
                Id = new Guid("338cd671-cfb1-4f65-92a4-c4d6d2f6c2a4"),
                Name = "TestContent3",
                CardId = Template.Id
            };

        public static Content Content4 =>
            new Content
            {
                Id = new Guid("d788c64c-1d52-4c33-8a91-a49bfa7cbcaa"),
                Name = "TestContent4",
                CardId = Template.Id
            };

        public static Content Content5 =>
            new Content
            {
                Id = new Guid("4da6111a-140e-499f-ba7f-71dec9640c1e"),
                Name = "TestContent5",
                CardId = Template.Id
            };

        public static Content Content6 =>
            new Content
            {
                Id = new Guid("49222d84-8683-4276-942d-49d6478901c2"),
                Name = "TestContent6",
                CardId = Template.Id
            };

        public static Content Content7 =>
            new Content
            {
                Id = new Guid("c0821719-70d8-40d0-8c89-17a3d718fdbd"),
                Name = "TestContent7",
                CardId = Template.Id
            };

        public static Content Content8 =>
            new Content
            {
                Id = new Guid("46f4de02-ad1b-47da-a3bd-bc9ab0a7ae5d"),
                Name = "TestContent8",
                CardId = Template.Id
            };

        public static Content Content9 =>
            new Content
            {
                Id = new Guid("571066a5-f85c-40d5-876c-ffe786b9987d"),
                Name = "TestContent9",
                CardId = Template.Id
            };

        public void Execute()
        {
            _context.Cards.Add(Template.Copy());
            _context.Contents.Add(Content0.Copy());
            _context.Contents.Add(Content1.Copy());
            _context.Contents.Add(Content2.Copy());
            _context.Contents.Add(Content3.Copy());
            _context.Contents.Add(Content4.Copy());
            _context.Contents.Add(Content5.Copy());
            _context.Contents.Add(Content6.Copy());
            _context.Contents.Add(Content7.Copy());
            _context.Contents.Add(Content8.Copy());
            _context.Contents.Add(Content9.Copy());
            _context.SaveChanges();
        }
    }
}
