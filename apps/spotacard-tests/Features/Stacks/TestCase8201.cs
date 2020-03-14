using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Stacks
{
    public class TestCase8201 : ISeeder
    {
        private readonly GraphContext _context;

        public TestCase8201(GraphContext context)
        {
            _context = context;
        }

        public static readonly Card ContentCard = new Card
        {
            Id = new Guid("6aeffec2-c7d9-4415-b4c2-ffbf4914b100"),
            Title = "Content 1",
            Slug = "content-1",
            Type = CardTypes.Content
        };

        public static readonly Card StackCard = new Card
        {
            Id = new Guid("d107bdb0-0b9c-45b6-b022-e290c996e36f"),
            Title = "Stack 1",
            Slug = "stack-1",
            Type = CardTypes.Stack
        };

        public static readonly Content Content = new Content
        {
            Id = new Guid("ce36eebc-3cd7-496d-b41d-35c0ff1106d7"),
            Name = "Content Template 1",
            Text = "Hello, @Model.Name",
            CardId = TestCase8201.ContentCard.Id
        };

        public static Stack Stack0 =>
            new Stack
            {
                Id = new Guid("92ef418e-97e8-4b35-b1c8-684b28ad9569"),
                Name = "TestStack0",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack1 =>
            new Stack
            {
                Id = new Guid("56b45874-6bea-4956-b82e-59166187ae11"),
                Name = "TestStack1",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack2 =>
            new Stack
            {
                Id = new Guid("d6b2be83-da47-494b-b3c0-64b1b008360a"),
                Name = "TestStack2",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack3 =>
            new Stack
            {
                Id = new Guid("9a46086b-c133-4d8b-8a9b-24c0cd169630"),
                Name = "TestStack3",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack4 =>
            new Stack
            {
                Id = new Guid("7809f67b-118a-4241-accd-fcb947438383"),
                Name = "TestStack4",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack5 =>
            new Stack
            {
                Id = new Guid("b9926927-73cc-4846-81b1-4a87be093306"),
                Name = "TestStack5",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack6 =>
            new Stack
            {
                Id = new Guid("80087296-ed75-4c3c-ab32-9721bb9e5c1b"),
                Name = "TestStack6",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack7 =>
            new Stack
            {
                Id = new Guid("3236de2e-1da2-479a-91cc-30aac170fc78"),
                Name = "TestStack7",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack8 =>
            new Stack
            {
                Id = new Guid("194db9d0-8d7b-4211-af9c-dc809c30f5c4"),
                Name = "TestStack8",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public static Stack Stack9 =>
            new Stack
            {
                Id = new Guid("1690a079-95f9-4924-872a-413284c9dd2e"),
                Name = "TestStack9",
                ContentId = TestCase8201.Content.Id,
                CardId = TestCase8201.StackCard.Id
            };

        public void Execute()
        {
            _context.Cards.Add(ContentCard.Copy());
            _context.Cards.Add(StackCard.Copy());
            _context.Contents.Add(Content.Copy());
            _context.Stacks.Add(Stack0.Copy());
            _context.Stacks.Add(Stack1.Copy());
            _context.Stacks.Add(Stack2.Copy());
            _context.Stacks.Add(Stack3.Copy());
            _context.Stacks.Add(Stack4.Copy());
            _context.Stacks.Add(Stack5.Copy());
            _context.Stacks.Add(Stack6.Copy());
            _context.Stacks.Add(Stack7.Copy());
            _context.Stacks.Add(Stack8.Copy());
            _context.Stacks.Add(Stack9.Copy());
            _context.SaveChanges();
        }
    }
}
