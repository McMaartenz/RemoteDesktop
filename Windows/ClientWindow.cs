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
		internal Bitmap screen;

		public ClientWindow()
		{
			InitializeComponent();
			screen = new Bitmap(1, 1);

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

		internal void UpdateScreen()
		{
			lock(screen)
			{
				WG = CreateGraphics();
				WG.DrawImage(screen, 0, 0, Width, Height);
				WG = null;
			}
		}
	}
}
