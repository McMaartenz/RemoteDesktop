using System;
using System.Drawing;
using System.Windows.Forms;

namespace RemoteDesktop
{
	public partial class ClientWindow : Form
	{
		internal bool stopSharing = false;

		Graphics WG;
		Bitmap screen;

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
				WG.DrawImage(screen, 0, 0, Width, Height);
			}

			WG = null;
		}

		private void CloseCon_Click(object sender, EventArgs e)
		{
			stopSharing = true;
		}
	}
}
