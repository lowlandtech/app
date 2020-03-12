using System;
using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Features.Apps;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Tables
{
    public class TestCase1002 : ISeeder
    {
        private readonly GraphContext _context;
        private readonly TestCase1001 _testCase1001;

        public TestCase1002(GraphContext context)
        {
            _context = context;
            _testCase1001 = new TestCase1001(_context);
        }


        public static Table Table0 =>
            new Table
            {
                Id = new Guid("b18e9cde-2df2-4156-ac1f-55ddadd5cd17"),
                Name = "TestTable0",
                AppId = TestCase1001.App1.Id
            };

        public static Table Table1 =>
            new Table
            {
                Id = new Guid("0adc528c-b573-415c-9243-f7633c76328d"),
                Name = "TestTable1",
                AppId = TestCase1001.App1.Id
            };

        public static Table Table2 =>
            new Table
            {
                Id = new Guid("088ee577-2e73-4052-84ac-e1d56bbf3ab2"),
                Name = "TestTable2",
                AppId = TestCase1001.App1.Id
            };

        public static Table Table3 =>
            new Table
            {
                Id = new Guid("338cd671-cfb1-4f65-92a4-c4d6d2f6c2a4"),
                Name = "TestTable3",
                AppId = TestCase1001.App1.Id
            };

        public static Table Table4 =>
            new Table
            {
                Id = new Guid("d788c64c-1d52-4c33-8a91-a49bfa7cbcaa"),
                Name = "TestTable4",
                AppId = TestCase1001.App2.Id
            };

        public static Table Table5 =>
            new Table
            {
                Id = new Guid("4da6111a-140e-499f-ba7f-71dec9640c1e"),
                Name = "TestTable5",
                AppId = TestCase1001.App2.Id
            };

        public static Table Table6 =>
            new Table
            {
                Id = new Guid("49222d84-8683-4276-942d-49d6478901c2"),
                Name = "TestTable6",
                AppId = TestCase1001.App2.Id
            };

        public static Table Table7 =>
            new Table
            {
                Id = new Guid("c0821719-70d8-40d0-8c89-17a3d718fdbd"),
                Name = "TestTable7",
                AppId = TestCase1001.App2.Id
            };

        public static Table Table8 =>
            new Table
            {
                Id = new Guid("46f4de02-ad1b-47da-a3bd-bc9ab0a7ae5d"),
                Name = "TestTable8",
                AppId = TestCase1001.App2.Id
            };

        public static Table Table9 =>
            new Table
            {
                Id = new Guid("571066a5-f85c-40d5-876c-ffe786b9987d"),
                Name = "TestTable9",
                AppId = TestCase1001.App2.Id
            };

        public void Execute()
        {
            _testCase1001.Execute();
            _context.Tables.Add(Table0.Copy());
            _context.Tables.Add(Table1.Copy());
            _context.Tables.Add(Table2.Copy());
            _context.Tables.Add(Table3.Copy());
            _context.Tables.Add(Table4.Copy());
            _context.Tables.Add(Table5.Copy());
            _context.Tables.Add(Table6.Copy());
            _context.Tables.Add(Table7.Copy());
            _context.Tables.Add(Table8.Copy());
            _context.Tables.Add(Table9.Copy());
            _context.SaveChanges();
        }
    }
}
