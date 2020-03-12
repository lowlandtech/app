using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Features.Tables;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Fields
{
    public class TestCase1004 : ISeeder
    {
        private readonly GraphContext _context;
        private readonly TestCase1002 _testCase1002;

        public TestCase1004(GraphContext context)
        {
            _context = context;
            _testCase1002 = new TestCase1002(_context);
        }


        public static Field Field0 =>
            new Field
            {
                Index = 0,
                Id = new Guid("7f32824c-2b94-4a0a-a21c-35e2b04e8ef0"),
                Name = "TestField0",
                TableId = TestCase1002.Table1.Id
            };

        public static Field Field1 =>
            new Field
            {
                Index = 1,
                Id = new Guid("bac0062a-0b22-4172-a702-0bb53da6b70f"),
                Name = "TestField1",
                TableId = TestCase1002.Table1.Id
            };

        public static Field Field2 =>
            new Field
            {
                Index = 2,
                Id = new Guid("7d19a9e3-14f8-45f7-897a-4149ee7b1e3d"),
                Name = "TestField2",
                TableId = TestCase1002.Table1.Id
            };

        public static Field Field3 =>
            new Field
            {
                Index = 3,
                Id = new Guid("5cbe587f-bdab-4e6f-95e2-a5ad2195bdd2"),
                Name = "TestField3",
                TableId = TestCase1002.Table1.Id
            };

        public static Field Field4 =>
            new Field
            {
                Index = 4,
                Id = new Guid("4dc8d929-4a86-4d48-9bc1-9c252cfcb74a"),
                Name = "TestField4",
                TableId = TestCase1002.Table2.Id
            };

        public static Field Field5 =>
            new Field
            {
                Index = 5,
                Id = new Guid("18dfcd49-e3d3-45ec-8e4f-43baef0da9c5"),
                Name = "TestField5",
                TableId = TestCase1002.Table2.Id
            };

        public static Field Field6 =>
            new Field
            {
                Index = 6,
                Id = new Guid("31e95caf-0132-4ae0-bfa0-20929e83392b"),
                Name = "TestField6",
                TableId = TestCase1002.Table2.Id
            };

        public static Field Field7 =>
            new Field
            {
                Index = 7,
                Id = new Guid("f53e5d24-c4ed-435d-b046-2ba3fc014101"),
                Name = "TestField7",
                TableId = TestCase1002.Table2.Id
            };

        public static Field Field8 =>
            new Field
            {
                Index = 8,
                Id = new Guid("bc48c387-c41b-48fb-96ea-efe754042606"),
                Name = "TestField8",
                TableId = TestCase1002.Table2.Id
            };

        public static Field Field9 =>
            new Field
            {
                Index = 9,
                Id = new Guid("b2e2b289-9dc9-46f6-a9d8-1e4de9732b36"),
                Name = "TestField9",
                TableId = TestCase1002.Table2.Id
            };

        public void Execute()
        {
            _testCase1002.Execute();
            _context.Fields.Add(Field0.Copy());
            _context.Fields.Add(Field1.Copy());
            _context.Fields.Add(Field2.Copy());
            _context.Fields.Add(Field3.Copy());
            _context.Fields.Add(Field4.Copy());
            _context.Fields.Add(Field5.Copy());
            _context.Fields.Add(Field6.Copy());
            _context.Fields.Add(Field7.Copy());
            _context.Fields.Add(Field8.Copy());
            _context.Fields.Add(Field9.Copy());
            _context.SaveChanges();
        }
    }
}
