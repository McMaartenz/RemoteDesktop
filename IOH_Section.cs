using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
