using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteDesktop
{
	internal class IOHandler
	{
		// P/Invoke mouse_event() from user32.dll

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		internal static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

		// P/Invoke SetCursorPos() from user32.dll

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetCursorPos(int x, int y);

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
		// Example instruction: INPTEVname=mouse,x=5,y=10/name=key,keycode=5
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
			Queue<byte> keyStrokes = new Queue<byte>();

			// Execute instructions
			foreach (IOH_Instruction instruction in instructions_list)
			{
				switch (instruction.name)
				{
					case "INPTEV":
						foreach (IOH_Section section in instruction.sections)
						{
							switch (section.innerArgs["name"])
							{
								case "mouse":
									{
										(uint, uint) mousePos = ((uint)Int32.Parse(section.innerArgs["x"]), (uint)Int32.Parse(section.innerArgs["y"]));
										string button = section.innerArgs["button"];
										(uint, uint) action = GetMouseButton(button);
										SetCursorPos((int)mousePos.Item1, (int)mousePos.Item2);
										mouse_event(action.Item1 | action.Item2, mousePos.Item1, mousePos.Item2, 0, 0);
									}
									break;

								case "key":
									{
										keyStrokes.Enqueue((byte)Convert.ToInt32(section.innerArgs["keycode"], 16));
									}
									break;

								default:
									Console.WriteLine("Unrecognized name " + section.innerArgs["name"]);
									break;
							}
							
						}
						break;

					default:
						Console.WriteLine("Unrecognized instruction " + instruction.name);
						break;
				}
			}

			Queue<byte> keyStrokesUp = new Queue<byte>();
			IOH_Key.INPUT[] inputs = new IOH_Key.INPUT[keyStrokes.Count];

			// Handle keystrokes
			int i = 0;
			while (keyStrokes.Count > 0)
			{
				byte keyStroke = keyStrokes.Dequeue();

				inputs[i++] = new IOH_Key.INPUT
				{
					type = (int)IOH_Key.InputType.Keyboard,
					u = new IOH_Key.InputUnion
					{
						ki = new IOH_Key.KeyboardInput
						{
							wVk = keyStroke,
							wScan = 0,
							dwFlags = 0,//(uint)(IOH_Key.KEYEVENTF.KeyDown | IOH_Key.KEYEVENTF.Scancode),
							dwExtraInfo = IOH_Key.GetMessageExtraInfo()
						}
					} 
				};
				keyStrokesUp.Enqueue(keyStroke);
			}

			if (inputs.Length > 0)
			{
				IOH_Key.SendKeys(inputs);
			}

		}

	}
}