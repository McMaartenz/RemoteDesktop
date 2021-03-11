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
			IOHandler IOH = new IOHandler(); // Virtual key codes!!
			IOH.HandleEvent("INPTEVname=key,keycode=41/name=key,keycode=42/name=key,keycode=43/name=key,keycode=44/name=key,keycode=45/name=key,keycode=46");
			//StartWindow.Start();
		}
	}
}
