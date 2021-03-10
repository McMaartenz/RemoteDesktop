using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktop
{
	class IOH_Instruction
	{
		internal string name;
		internal List<IOH_Section> sections;

		internal IOH_Instruction(string name, List<IOH_Section> sections)
		{
			this.name = name;
			this.sections = sections;
		}
	}
}
