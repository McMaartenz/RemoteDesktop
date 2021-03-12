using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace RemoteDesktop
{
	public partial class ClientWindow : Form
	{
		internal bool stopSharing = false;
		internal Queue<int> KeyCodes = new Queue<int>();

		Graphics WG;
		Bitmap screen;
		private object drawImage;

		public ClientWindow()
		{
			InitializeComponent();

			// This way it is not null
			screen = new Bitmap(1, 1);

		}

		private void Render_Click(object sender, EventArgs e)
		{
			WG = CreateGraphics();

			lock (screen)
			{
				screen = Utility.Capture();
				screen.Save("output.bmp", ImageFormat.Bmp);
				WG.DrawImage(screen, 0, 0);
				byte[] outp = Utility.BitmapToByteArr(screen);
			}

			WG = null;
		}

		private void CloseCon_Click(object sender, EventArgs e)
		{
			stopSharing = true;
		}

		private void ClientWindow_KeyDown(object sender, KeyEventArgs e)
		{
			lock (KeyCodes)
			{
				KeyCodes.Enqueue(e.KeyValue);
			}
		}
	}
}
