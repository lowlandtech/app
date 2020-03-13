using System.Collections.Generic;

namespace Spotacard.Core.Contracts
{
	public interface IShell
	{
		List<ICommand> Commands { get; set; }
		string Execute();
	}
}
