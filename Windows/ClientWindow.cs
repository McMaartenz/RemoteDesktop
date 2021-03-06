﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RemoteDesktop
{
	public partial class ClientWindow : Form
	{
		internal bool stopSharing = false;
		internal Queue<int> KeyCodes = new Queue<int>();
		internal Queue<((MouseEventArgs, Point), Point)> MouseEvents = new Queue<((MouseEventArgs, Point), Point)>();
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
			UpdateResize();
			BackgroundImage = screen;
		}

		private void ClientWindow_MouseDown(object sender, MouseEventArgs e)
		{
			lock (MouseEvents)
			{
				Point relativePoint = this.PointToClient(Cursor.Position);
				MouseEvents.Enqueue(((e, relativePoint), new Point(ClientSize.Width, ClientSize.Height)));
			}
		}

		private void ClientWindow_KeyDown_1(object sender, KeyEventArgs e)
		{
			lock (KeyCodes)
			{
				KeyCodes.Enqueue(e.KeyValue);
			}
		}

		private void ClientWindow_ResizeEnd(object sender, EventArgs e)
		{
			UpdateResize(true); // Update regardless of ratio check
		}

		private void UpdateResize(bool force = false)
		{
			Invoke(new Action(() => // Thread safe
			{
				float ratio_client = ClientSize.Width / ClientSize.Height;
				float ratio_image = screen.Width / screen.Height;
				if (ratio_client != ratio_image || force)
				{
					ClientSize = new Size(ClientSize.Width, (int)(ClientSize.Width / 1.6));
				}
			}));
		}

		private void ClientWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			stopSharing = true;
			System.Threading.Thread.Sleep(250); // Assert a fine exit
		}
	}
}