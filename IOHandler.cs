using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteDesktop
{
	internal class IOHandler
	{

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetCursorPos(int x, int y);

		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;

		private (uint, uint) GetMouseButton(string button)
		{
			switch (button)
			{
				case "left": return (MOUSEEVENTF_LEFTDOWN, MOUSEEVENTF_LEFTUP);
				case "right": return (MOUSEEVENTF_RIGHTDOWN, MOUSEEVENTF_RIGHTUP);
				default: throw new ArgumentException();
			}
		}


		// Instruction is always 6 characters in length
		// Example instruction: INPTEV/name=mouse,x=5,y=10/
		internal void HandleEvent(string data)
		{
			string[] instructions = data.Split(';'); // Split instructions

			List<IOH_Instruction> instructions_list = new List<IOH_Instruction>();

			foreach (string instr in instructions)
			{
				string name = instr.Substring(0, 6);
				string[] sections = instr.Substring(6).Split('/'); // Split sections

				List<IOH_Section> sections_list = new List<IOH_Section>();

				foreach (string sect in sections)
				{
					string[] arguments = sect.Split(','); // Split inner arguments inside sections

					Dictionary<string, string> innerArgs = new Dictionary<string, string>();

					foreach (string arg in arguments)
					{
						innerArgs.Add(arg.Substring(0, arg.IndexOf('=')).ToLower(), arg.Substring(arg.IndexOf('=') + 1).ToLower());
					}

					sections_list.Add(new IOH_Section(innerArgs));

				}

				instructions_list.Add(new IOH_Instruction(name.ToUpper(), sections_list));
			}

			// Execute instructions
			foreach (IOH_Instruction instruction in instructions_list)
			{
				switch (instruction.name)
				{
					case "INPTEV":
						foreach (IOH_Section section in instruction.sections)
						{
							if (section.innerArgs["name"] == "mouse")
							{
								(uint, uint) mousePos = ((uint)Int32.Parse(section.innerArgs["x"]), (uint)Int32.Parse(section.innerArgs["y"]));
								string button = section.innerArgs["button"];
								(uint, uint) action = GetMouseButton(button);
								SetCursorPos((int)mousePos.Item1, (int)mousePos.Item2);
								mouse_event(action.Item1 | action.Item2, mousePos.Item1, mousePos.Item2, 0, 0);
							}
						}
						break;

					default:
						Console.WriteLine("Unrecognized instruction " + instruction.name);
						break;
				}
			}
		}

	}
}