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
		internal Queue<MouseEventArgs> MouseEvents = new Queue<MouseEventArgs>();

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
			WG = CreateGraphics();
			WG.DrawImage(screen, 0, 0, Width, Height);
			WG = null;
		}

		private void ClientWindow_MouseDown(object sender, MouseEventArgs e)
		{
			lock (MouseEvents)
			{
				MouseEvents.Enqueue(e);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void closeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ClientWindow_Load(object sender, EventArgs e)
		{

		}

		private void closeConnectionToolStripMenuItem_Click_1(object sender, EventArgs e)
		{

		}

		private void button1_Click_1(object sender, EventArgs e)
		{

		}

		private void button1_Click_2(object sender, EventArgs e)
		{

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