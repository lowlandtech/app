using System;
using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Features.Apps;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Pages
{
    public class TestCase1003 : ISeeder
    {
        private readonly GraphContext _context;
        private readonly TestCase1001 _testCase1001;

        public TestCase1003(GraphContext context)
        {
            _context = context;
            _testCase1001 = new TestCase1001(_context);
        }


        public static Page Page0 =>
            new Page
            {
                Id = new Guid("ada65c01-a952-4846-991c-7b5f64ec2736"),
                Name = "TestPage0",
                AppId = TestCase1001.App1.Id
            };

        public static Page Page1 =>
            new Page
            {
                Id = new Guid("c2683606-ead9-4f63-ba45-3b7eae8a2dab"),
                Name = "TestPage1",
                AppId = TestCase1001.App1.Id
            };

        public static Page Page2 =>
            new Page
            {
                Id = new Guid("cb5f9da0-301e-42a1-9527-7b7fad40f9ed"),
                Name = "TestPage2",
                AppId = TestCase1001.App1.Id
            };

        public static Page Page3 =>
            new Page
            {
                Id = new Guid("4d0776a7-1fad-49b3-b24e-09fb11773961"),
                Name = "TestPage3",
                AppId = TestCase1001.App1.Id
            };

        public static Page Page4 =>
            new Page
            {
                Id = new Guid("8eee90fb-b53f-4038-8e31-276a45b207c4"),
                Name = "TestPage4",
                AppId = TestCase1001.App2.Id
            };

        public static Page Page5 =>
            new Page
            {
                Id = new Guid("9009178a-52a9-4309-888f-5d5aa733c892"),
                Name = "TestPage5",
                AppId = TestCase1001.App2.Id
            };

        public static Page Page6 =>
            new Page
            {
                Id = new Guid("a729e1eb-1fb4-415f-8959-a82d215a216e"),
                Name = "TestPage6",
                AppId = TestCase1001.App2.Id
            };

        public static Page Page7 =>
            new Page
            {
                Id = new Guid("d0949999-9304-42df-8f62-3599c8bb65a7"),
                Name = "TestPage7",
                AppId = TestCase1001.App2.Id
            };

        public static Page Page8 =>
            new Page
            {
                Id = new Guid("d04c5b57-e984-4fd3-8a90-fcde021efe98"),
                Name = "TestPage8",
                AppId = TestCase1001.App2.Id
            };

        public static Page Page9 =>
            new Page
            {
                Id = new Guid("571066a5-f85c-40d5-876c-ffe786b9987d"),
                Name = "TestPage9",
                AppId = TestCase1001.App2.Id
            };

        public void Execute()
        {
            _testCase1001.Execute();
            _context.Pages.Add(Page0.Copy());
            _context.Pages.Add(Page1.Copy());
            _context.Pages.Add(Page2.Copy());
            _context.Pages.Add(Page3.Copy());
            _context.Pages.Add(Page4.Copy());
            _context.Pages.Add(Page5.Copy());
            _context.Pages.Add(Page6.Copy());
            _context.Pages.Add(Page7.Copy());
            _context.Pages.Add(Page8.Copy());
            _context.Pages.Add(Page9.Copy());
            _context.SaveChanges();
        }
    }
}
