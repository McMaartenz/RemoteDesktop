using System;
using System.Drawing;
using System.Windows.Forms;

namespace RemoteDesktop
{
	public partial class ClientWindow : Form
	{
		Graphics G;

		public ClientWindow()
		{
			InitializeComponent();
			G = CreateGraphics();
		}
	}
}
