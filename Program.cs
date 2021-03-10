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
			IOHandler IOH = new IOHandler();
			IOH.HandleEvent("INPTEVname=mouse,x=250,y=250,button=left");
			//StartWindow.Start();
		}
	}
}
