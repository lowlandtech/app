using Spotacard.Core.Contracts;
using Spotacard.Domain;
using Spotacard.Features.Pages;
using Spotacard.Infrastructure;
using System;

namespace Spotacard.Features.Cells
{
    public class TestCase1005 : ISeeder
    {
        private readonly GraphContext _context;
        private readonly TestCase1003 _testCase1003;

        public TestCase1005(GraphContext context)
        {
            _context = context;
            _testCase1003 = new TestCase1003(_context);
        }


        public static Cell Cell0 =>
            new Cell
            {
                Id = new Guid("177707b4-e412-431e-8996-aded94f24181"),
                Name = "TestCell0",
                Area = "area1",
                PageId = TestCase1003.Page1.Id
            };

        public static Cell Cell1 =>
            new Cell
            {
                Id = new Guid("6ffd6c49-cd8d-4584-aca2-754f11a54d86"),
                Name = "TestCell1",
                Area = "area1",
                PageId = TestCase1003.Page1.Id
            };

        public static Cell Cell2 =>
            new Cell
            {
                Id = new Guid("d440bb32-356f-4209-b314-a2a2b80444b9"),
                Name = "TestCell2",
                Area = "area1",
                PageId = TestCase1003.Page1.Id
            };

        public static Cell Cell3 =>
            new Cell
            {
                Id = new Guid("d67f45ac-0c1a-4e44-b60c-a188780a8a72"),
                Name = "TestCell3",
                Area = "area1",
                PageId = TestCase1003.Page1.Id
            };

        public static Cell Cell4 =>
            new Cell
            {
                Id = new Guid("5956e101-f2b6-4b32-b0e3-fd65792015d6"),
                Name = "TestCell4",
                Area = "area1",
                PageId = TestCase1003.Page2.Id
            };

        public static Cell Cell5 =>
            new Cell
            {
                Id = new Guid("8c5b3637-88eb-4d7b-b1f6-6dfe73988f24"),
                Name = "TestCell5",
                Area = "area1",
                PageId = TestCase1003.Page2.Id
            };

        public static Cell Cell6 =>
            new Cell
            {
                Id = new Guid("5293f8a1-9c78-4871-9715-628bd7687ca1"),
                Name = "TestCell6",
                Area = "area1",
                PageId = TestCase1003.Page2.Id
            };

        public static Cell Cell7 =>
            new Cell
            {
                Id = new Guid("69b47411-549f-4c7b-8205-bcd8df0f78a3"),
                Name = "TestCell7",
                Area = "area1",
                PageId = TestCase1003.Page2.Id
            };

        public static Cell Cell8 =>
            new Cell
            {
                Id = new Guid("2352c04b-f068-4247-ad1d-334e1b87a388"),
                Name = "TestCell8",
                Area = "area1",
                PageId = TestCase1003.Page2.Id
            };

        public static Cell Cell9 =>
            new Cell
            {
                Id = new Guid("361901c8-70b5-43ac-978b-8faf98aebbc6"),
                Name = "TestCell9",
                Area = "area1",
                PageId = TestCase1003.Page2.Id
            };

        public void Execute()
        {
            _testCase1003.Execute();
            _context.Cells.Add(Cell0.Copy());
            _context.Cells.Add(Cell1.Copy());
            _context.Cells.Add(Cell2.Copy());
            _context.Cells.Add(Cell3.Copy());
            _context.Cells.Add(Cell4.Copy());
            _context.Cells.Add(Cell5.Copy());
            _context.Cells.Add(Cell6.Copy());
            _context.Cells.Add(Cell7.Copy());
            _context.Cells.Add(Cell8.Copy());
            _context.Cells.Add(Cell9.Copy());
            _context.SaveChanges();
        }
    }
}
