using System.Collections.Generic;

namespace RemoteDesktop
{
	class IOH_Section
	{
		internal Dictionary<string, string> innerArgs;

		internal IOH_Section(Dictionary<string, string> innerArgs)
		{
			this.innerArgs = innerArgs;
		}
	}
}
