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
            Id = new Guid("85ccaacf-2456-4492-bb4b-0f07038e14aa"),
            Title = "Template 1",
            Slug = "template-1",
            Type = CardTypes.Template
        };

        public static Content Content0 =>
            new Content
            {
                Id = new Guid("433cdf69-f3bc-4aeb-a91c-68db151b70ac"),
                Name = "TestContent0",
                CardId = Template.Id
            };

        public static Content Content1 =>
            new Content
            {
                Id = new Guid("ebc15c49-f487-402c-b775-9c30c8996024"),
                Name = "TestContent1",
                CardId = Template.Id
            };

        public static Content Content2 =>
            new Content
            {
                Id = new Guid("e420017e-f5a0-4e58-ae30-ec8acf4e54ed"),
                Name = "TestContent2",
                CardId = Template.Id
            };

        public static Content Content3 =>
            new Content
            {
                Id = new Guid("8f6d0098-09f2-4464-9c0b-78d3db939692"),
                Name = "TestContent3",
                CardId = Template.Id
            };

        public static Content Content4 =>
            new Content
            {
                Id = new Guid("de8810a4-6c2d-467d-9dfc-b5187fd1f972"),
                Name = "TestContent4",
                CardId = Template.Id
            };

        public static Content Content5 =>
            new Content
            {
                Id = new Guid("eb034bc2-6327-4035-b6dc-c2541bcc70e8"),
                Name = "TestContent5",
                CardId = Template.Id
            };

        public static Content Content6 =>
            new Content
            {
                Id = new Guid("4b042ea1-29ae-4a54-91fa-49d1754d058a"),
                Name = "TestContent6",
                CardId = Template.Id
            };

        public static Content Content7 =>
            new Content
            {
                Id = new Guid("23c75b0a-99b2-4897-a232-a66cc88f9f5f"),
                Name = "TestContent7",
                CardId = Template.Id
            };

        public static Content Content8 =>
            new Content
            {
                Id = new Guid("1d391dc2-cd8a-4aa6-ab24-a5e67972465f"),
                Name = "TestContent8",
                CardId = Template.Id
            };

        public static Content Content9 =>
            new Content
            {
                Id = new Guid("ce895d1e-5c53-41b1-95b0-18273eb0cba2"),
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
