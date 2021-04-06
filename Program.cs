using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RemoteDesktop
{
	static class Program
	{
		public static StartWindow form;
		public static ServerWindow sw;
		public static ClientWindow cw;

		static void Main()
		{
			StartWindow.Start();
		}
	}
}


