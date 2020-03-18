using RazorLight;
using Spotacard.Core.Enums;
using Spotacard.Domain;
using Spotacard.Features.Stacks.Contracts;
using Spotacard.Features.Stacks.Types.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Types
{
    public class StackItemBuilder<T> : IStackItemBuilder<T>
    {
        private readonly List<StackItem> _stacks = new List<StackItem>();

        private string _root;
        private readonly RazorLightEngine _engine;
        private T _data;
        private Stack _stack;

        public StackItemBuilder(RazorLightEngine engine)
        {
            _engine = engine;
        }

        public IStackItemBuilder<T> UseRoot(string root)
        {
            _root = root;
            return this;
        }

        public IStackItemBuilder<T> Use(Stack stack, T data)
        {
            _data = data;
            _stack = stack;

            return this;
        }

        private async Task UseStack<TEntity>(Stack stack, TEntity data)
        {
            var filename = string.Empty;

            if (stack.Content.FileName != null)
            {
                filename = await _engine.CompileRenderStringAsync(
                    $"{stack.Content.Id}-filename",
                    stack.Content.FileName, data);
            }

            var action = new Func<Task<string>>(() => _engine.CompileRenderStringAsync(
                $"{stack.Content.Id}-text",
                stack.Content.Text, data));

            UseStack(stack.Type, filename, action);
        }

        private void UseStack(StackTypes type, string filename, Func<Task<string>> action)
        {
            switch (type)
            {
                case StackTypes.File:
                    _stacks.Add(new StackFile(_root, filename, action));
                    break;
                case StackTypes.Folder:
                    _stacks.Add(new StackFolder(_root, filename, action));
                    break;
                case StackTypes.Command:
                    _stacks.Add(new StackCommand(_root, action));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<List<StackItem>> BuildAsync()
        {
            await UseStack(_stack, _data);
            return _stacks;
        }
    }
}
