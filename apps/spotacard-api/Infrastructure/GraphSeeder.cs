using MediatR;
using Spotacard.Core.Contracts;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotacard.Infrastructure
{
    public class PersonSeeder : ISeeder
    {
        private readonly GraphContext _context;
        private readonly IMediator _mediator;

        public PersonSeeder(GraphContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public void Execute()
        {
            if (!_context.Persons.Any())
            {
                var command = new Create.Command
                {
                    User = new Create.UserData
                    {
                        Email = "admin@spotacard.com",
                        Password = "admin",
                        Username = "admin"
                    }
                };
                _mediator.Send(command);
            };
        }
    }

    public class GraphSeeder : ISeeder
    {
        private readonly GraphContext _context;

        public GraphSeeder(GraphContext context)
        {
            _context = context;
        }

        private static App Spotacard =>
            new App
            {
                Id = new Guid("a65712b6-3108-4077-8cfc-dd739c0c2606"),
                Name = "Spotacard",
                Organization = "Spotacard",
                Prefix = "SAC",
                Namingspace = "Spotacard",
                Tables = new List<Table>
                {
                    new Table
                    {
                        Id = new Guid("a7ea2a08-a460-48a6-9cbe-ad77428b242a"),
                        Name = "App",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("36a4fecd-6b79-4b52-a9a9-81738ffc0ac7"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("6d652641-b2f4-4df8-bc6e-f3d1c08e315a") ,Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("33a4f6e9-ada8-4a96-8e74-3c1b06c4a743"), Name = "Organization", Type = FieldTypes.String, Index = 2 },
                            new Field { Id = new Guid("561e6ea4-e94e-4615-8880-83786bf69ba7"), Name = "Prefix", Type = FieldTypes.String, Index = 3 },
                            new Field { Id = new Guid("edefa3c9-8f5a-4c9d-9c52-4a1e57d9e3fb"), Name = "Namingspace", Type = FieldTypes.String, Index = 4 },
                        }
                    },
                    new Table
                    {
                        Id = new Guid("31c85356-ffe1-4de9-a3a6-4eb244c22fc9"),
                        Name = "Cell",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("9652df56-0fdb-47d8-ae88-1e62b8eb407b"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("af7949be-e36c-4767-9def-a6935d52b7fd"), Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("8da35879-be54-46cd-a6fc-98139b9480a6"), Name = "Area", Type = FieldTypes.String, Index = 2 },
                            new Field { Id = new Guid("ab68b2c7-d9de-455b-b017-b507898b3be7"), Name = "FieldId", Type = FieldTypes.String, Index = 3, IsNullable = true },
                            new Field { Id = new Guid("c2be2f78-e442-41b6-a750-78a2941a7e92"), Name = "WidgetId", Type = FieldTypes.String, Index = 4, IsNullable = true },
                            new Field { Id = new Guid("712e9add-5aa6-4830-90e2-45439250c62d"), Name = "PageId", Type = FieldTypes.String, Index = 5 },
                        }
                    },
                    new Table
                    {
                        Id = new Guid("9158723e-6217-462e-a34b-132b3e00b12a"),
                        Name = "Field",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("47fc2f9c-33a1-44ee-a304-5ecc532cf29a"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("d176d4c2-60ab-4956-ae9e-b351a960574e"), Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("6f241a7a-f464-4499-a492-75ae53938721"), Name = "Area", Type = FieldTypes.String, Index = 2 },
                            new Field { Id = new Guid("5898cd47-7ade-491c-92ba-4cc754eaff2a"), Name = "TableId", Type = FieldTypes.Guid, Index = 3, IsNullable = true },
                            new Field { Id = new Guid("f705ba92-527c-45a3-ac3d-6eb28e0415f7"), Name = "WidgetId", Type = FieldTypes.Guid, Index = 4, IsNullable = true },
                            new Field { Id = new Guid("88ce7e52-568c-4209-9b4d-f107f6c4eb47"), Name = "PageId", Type = FieldTypes.Guid, Index = 5 },
                        }
                    },
                    new Table
                    {
                        Id = new Guid("4ba0d6ee-b182-4fc8-8190-8e6d94a58cfa"),
                        Name = "Layout",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("4b16d0b1-229e-48e3-a2db-f80e6916e967"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("1bd00f06-57da-458a-8e68-c05f1c599a60"), Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("f2fcff96-ee19-4b91-96a5-7623018f5fc6"), Name = "Packages", Type = FieldTypes.String, Index = 2, IsNullable = true  },
                            new Field { Id = new Guid("b4c5dc72-9c24-4a43-885c-ed33a1123304"), Name = "Wiring", Type = FieldTypes.String, Index = 3, IsNullable = true },
                            new Field { Id = new Guid("5d5052f0-21d5-42cf-9ff9-6567216c431a"), Name = "CodeBehind", Type = FieldTypes.String, Index = 4, IsNullable = true },
                            new Field { Id = new Guid("4905bf80-2b25-4ed5-b350-c616cb31b289"), Name = "Styling", Type = FieldTypes.String, Index = 5, IsNullable = true  },
                            new Field { Id = new Guid("4a18fcd2-a4b2-4072-b3d4-353447e6b51d"), Name = "Markup", Type = FieldTypes.String, Index = 6, IsNullable = true  },
                            new Field { Id = new Guid("85e078c7-3f12-4873-8fa4-78439c803696"), Name = "Items", Type = FieldTypes.String, Index = 7  },
                        }
                    },
                    new Table
                    {
                        Id = new Guid("28437356-dc0e-4e5f-8b76-18c5e5b9af5e"),
                        Name = "Page",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("54789455-6dc1-48ac-aeaa-6c27152b817c"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("bcb2dca3-17e8-490d-850e-1f18196e6886"), Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("a2cb552f-bb45-4f9d-b333-6d6f5ad5f044"), Name = "CodeBehind", Type = FieldTypes.String, Index = 4, IsNullable = true },
                            new Field { Id = new Guid("9deaee1f-7f76-49a2-8bd4-1a8d12c2cc84"), Name = "Styling", Type = FieldTypes.String, Index = 5, IsNullable = true  },
                            new Field { Id = new Guid("bc7119b6-7844-41af-85c4-e420c9fa9bd9"), Name = "Markup", Type = FieldTypes.String, Index = 6, IsNullable = true  },
                            new Field { Id = new Guid("7595b83b-275e-4b11-98c1-ae35e09ea0ef"), Name = "AppId", Type = FieldTypes.Guid, Index = 7  },
                            new Field { Id = new Guid("d19afcc8-7069-413e-b164-2b8a3238fb15"), Name = "TableId", Type = FieldTypes.Guid, Index = 8, IsNullable = true },
                            new Field { Id = new Guid("1f772413-e945-4770-9f68-52c1861624b1"), Name = "LayoutId", Type = FieldTypes.Guid, Index = 9, IsNullable = true },
                        }
                    },
                    new Table
                    {
                        Id = new Guid("f43bf789-bcc3-4af1-b3ed-e68db9afd1b7"),
                        Name = "Table",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("cb0a7c6a-7636-4394-93d6-d08ded9286fd"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("ed3e64e2-5270-4644-bf33-5487357cd53c"), Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("c093ad2d-79fd-4767-a8e1-50125f59b391"), Name = "AppId", Type = FieldTypes.Guid, Index = 2  },
                        }
                    },
                    new Table
                    {
                        Id = new Guid("5feaf450-191c-4149-8d4e-a86cc140416b"),
                        Name = "Widget",
                        Fields = new List<Field>
                        {
                            new Field { Id = new Guid("6ea5f311-cb10-41f2-9350-f007359bb2e0"), Name = "Id", Type = FieldTypes.Guid, Index = 0 },
                            new Field { Id = new Guid("c6a153b2-a07f-466a-8950-b1fbfaf7e796"), Name = "Name", Type = FieldTypes.String, Index = 1 },
                            new Field { Id = new Guid("3e883acc-e5df-4595-a103-274996b0a950"), Name = "Packages", Type = FieldTypes.String, Index = 2, IsNullable = true  },
                            new Field { Id = new Guid("7368a55e-d469-423b-b7da-8b8f00896fda"), Name = "Wiring", Type = FieldTypes.String, Index = 3, IsNullable = true },
                            new Field { Id = new Guid("93fb381e-ff01-4622-bf8c-4f599feb7bae"), Name = "CodeBehind", Type = FieldTypes.String, Index = 4, IsNullable = true },
                            new Field { Id = new Guid("1a0ab98e-0602-4e53-b9ef-06c212bdfefd"), Name = "Styling", Type = FieldTypes.String, Index = 5, IsNullable = true  },
                            new Field { Id = new Guid("38e42a24-9490-4104-9b7a-ef7ec3313b16"), Name = "Markup", Type = FieldTypes.String, Index = 6, IsNullable = true  },
                        }
                    },
                }
            };

        public static Relation Relation0 =>
            new Relation
            {
                Id = new Guid("090f7291-6d77-4e1d-b146-c1087ecbe362"),
                Name = "[Field].[Id]=[Cell].[FieldId]",
                PkFieldId = new Guid("47fc2f9c-33a1-44ee-a304-5ecc532cf29a"),
                PkName = "Cells",
                FkFieldId = new Guid("ab68b2c7-d9de-455b-b017-b507898b3be7"),
                FkName = "Field"
            };
        public static Relation Relation1 =>
            new Relation
            {
                Id = new Guid("dda04d12-3cf6-4c0c-b753-e70802f91e5f"),
                Name = "[Widget].[Id]=[Cell].[Widget]",
                PkFieldId = new Guid("6ea5f311-cb10-41f2-9350-f007359bb2e0"),
                PkName = "Cells",
                FkFieldId = new Guid("c2be2f78-e442-41b6-a750-78a2941a7e92"),
                FkName = "Widget"
            };
        public static Relation Relation2 =>
            new Relation
            {
                Id = new Guid("3e40f8b1-9e47-4741-ba9c-d0f5e848f16e"),
                Name = "[Page].[Id]=[Cell].[PageId]",
                PkFieldId = new Guid("54789455-6dc1-48ac-aeaa-6c27152b817c"),
                PkName = "Cells",
                FkFieldId = new Guid("88ce7e52-568c-4209-9b4d-f107f6c4eb47"),
                FkName = "Page"
            };
        public static Relation Relation3 =>
            new Relation
            {
                Id = new Guid("ba09c2ab-8902-4259-9e1e-451fe2f4b047"),
                Name = "[Table].[Id]=[Field].[TableId]",
                PkFieldId = new Guid("cb0a7c6a-7636-4394-93d6-d08ded9286fd"),
                PkName = "Fields",
                FkFieldId = new Guid("5898cd47-7ade-491c-92ba-4cc754eaff2a"),
                FkName = "Table"
            };
        public static Relation Relation4 =>
            new Relation
            {
                Id = new Guid("a4c550bd-7604-48b4-ab50-b14184ffc919"),
                Name = "[Widget].[Id]=[Field].[WidgetId]",
                PkFieldId = new Guid("6ea5f311-cb10-41f2-9350-f007359bb2e0"),
                PkName = "Fields",
                FkFieldId = new Guid("f705ba92-527c-45a3-ac3d-6eb28e0415f7"),
                FkName = "Widget"
            };
        public static Relation Relation5 =>
            new Relation
            {
                Id = new Guid("9391f014-289b-42ff-a76c-8afa12da1ba2"),
                Name = "[Table].[Id]=[Field].[TableId]",
                PkFieldId = new Guid("cb0a7c6a-7636-4394-93d6-d08ded9286fd"),
                PkName = "Fields",
                FkFieldId = new Guid("5898cd47-7ade-491c-92ba-4cc754eaff2a"),
                FkName = "Table"
            };
        public static Relation Relation6 =>
            new Relation
            {
                Id = new Guid("eeb037f3-beb6-45ed-a34f-50fd52a25e01"),
                Name = "[App].[Id]=[Page].[TableId]",
                PkFieldId = new Guid("36a4fecd-6b79-4b52-a9a9-81738ffc0ac7"),
                PkName = "Pages",
                FkFieldId = new Guid("7595b83b-275e-4b11-98c1-ae35e09ea0ef"),
                FkName = "App"
            };
        public static Relation Relation7 =>
            new Relation
            {
                Id = new Guid("1fdb24f8-4492-4448-83c7-58839f6e6ab6"),
                Name = "[Table].[Id]=[Page].[TableId]",
                PkFieldId = new Guid("cb0a7c6a-7636-4394-93d6-d08ded9286fd"),
                PkName = "Pages",
                FkFieldId = new Guid("d19afcc8-7069-413e-b164-2b8a3238fb15"),
                FkName = "Table"
            };
        public static Relation Relation8 =>
            new Relation
            {
                Id = new Guid("fac51c74-b874-484e-9e97-2cfcd9686bca"),
                Name = "[Layout].[Id]=[Page].[LayoutId]",
                PkFieldId = new Guid("4b16d0b1-229e-48e3-a2db-f80e6916e967"),
                PkName = "Pages",
                FkFieldId = new Guid("1f772413-e945-4770-9f68-52c1861624b1"),
                FkName = "Layout"
            };
        public static Relation Relation9 =>
            new Relation
            {
                Id = new Guid("3a32c227-a820-46b5-9e4a-9c339f0da65c"),
                Name = "[App].[Id]=[Table].[AppId]",
                PkFieldId = new Guid("36a4fecd-6b79-4b52-a9a9-81738ffc0ac7"),
                PkName = "Tables",
                FkFieldId = new Guid("c093ad2d-79fd-4767-a8e1-50125f59b391"),
                FkName = "App"
            };
        public void Execute()
        {
            if (!_context.Apps.Any())
            {
                _context.Apps.Add(Spotacard.Copy());
                _context.Relations.Add(Relation0.Copy());
                _context.Relations.Add(Relation1.Copy());
                _context.Relations.Add(Relation2.Copy());
                _context.Relations.Add(Relation3.Copy());
                _context.Relations.Add(Relation4.Copy());
                _context.Relations.Add(Relation5.Copy());
                _context.Relations.Add(Relation6.Copy());
                _context.Relations.Add(Relation7.Copy());
                _context.Relations.Add(Relation8.Copy());
                _context.Relations.Add(Relation9.Copy());
                _context.SaveChanges();
            };
        }
    }
}
