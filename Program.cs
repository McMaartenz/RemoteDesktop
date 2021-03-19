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
			Bitmap img = Utility.CaptureScreen();
			Byte[] outp = Utility.BitmapToByteArr(img);
			Bitmap img2 = Utility.ByteArrToBitmap(outp);

			Console.WriteLine(outp.Length);
			img2.Save("myfile.jpeg", ImageFormat.Jpeg);
			StartWindow.Start();
		}
	}
}
