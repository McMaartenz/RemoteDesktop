using System.Collections.Generic;

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
