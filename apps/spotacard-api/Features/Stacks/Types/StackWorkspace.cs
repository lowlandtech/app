using Spotacard.Features.Stacks.Types.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spotacard.Core.Enums;
using Spotacard.Domain;

namespace Spotacard.Features.Stacks.Types
{
    public class StackWorkspace
    {
        private readonly StackContext _context;

        public List<StackItem> Items { get; } = new List<StackItem>();

        public StackWorkspace(StackContext context)
        {
            _context = context;
        }

        public async Task BeforeExecute()
        {
            foreach (var child in _context.Stack.Children)
            {
                switch (child.Data)
                {
                    case DataTypes.App:
                        Items.AddRange(await new StackItemBuilder<App>(_context.Engine)
                            .Use(child, _context.App).BuildAsync());
                        break;
                    case DataTypes.Table:
                        foreach (var table in _context.App.Tables)
                            Items.AddRange(await new StackItemBuilder<Table>(_context.Engine)
                                .Use(child, table).BuildAsync());

                        break;
                    case DataTypes.Page:
                        foreach (var page in _context.App.Pages)
                            Items.AddRange(await new StackItemBuilder<Page>(_context.Engine)
                                .Use(child, page).BuildAsync());

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public async Task Execute()
        {
            foreach (var stackItem in Items)
            {
                await stackItem.Execute();
            }
        }
    }
}
