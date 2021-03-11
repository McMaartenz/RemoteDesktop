using System.Threading;

namespace RemoteDesktop
{
	static class Program
	{
		public static StartWindow form;
		public static ServerWindow sw;
		public static ClientWindow cw;

		static void Main()
		{
			//IOHandler IOH = new IOHandler(); // Virtual key codes!!
			//IOH.HandleEvent("INPTEVname=key,keycode=0xA2/name=key,keycode=0x53");
			StartWindow.Start();
		}
	}
}
