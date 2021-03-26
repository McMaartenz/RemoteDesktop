using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RemoteDesktop
{
	public partial class ClientWindow : Form
	{
		internal bool stopSharing = false;
		internal Queue<int> KeyCodes = new Queue<int>();
		internal Queue<(MouseEventArgs, PointF)> MouseEvents = new Queue<(MouseEventArgs, PointF)>();

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
			BackgroundImage = screen;
		}

		private void ClientWindow_MouseDown(object sender, MouseEventArgs e)
		{
			lock (MouseEvents)
			{
				MouseEvents.Enqueue((e, new PointF(Width, Height)));
			}
		}

		private void ClientWindow_KeyDown_1(object sender, KeyEventArgs e)
		{
			lock (KeyCodes)
			{
				KeyCodes.Enqueue(e.KeyValue);
			}
		}
	}
}