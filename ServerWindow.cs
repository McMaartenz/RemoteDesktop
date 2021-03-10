using System;
using System.Windows.Forms;


namespace RemoteDesktop
{
	internal partial class ServerWindow : Form
	{
		internal bool stopSharing = false;

		internal ServerWindow()
		{
			InitializeComponent();
		}

		internal void LogMessage(string msg)
		{
			BeginInvoke(new Action(() => // Send to GUI thread
			{
				ServerWindow_Log.AppendText(DateTime.Now.ToString("HH:mm:ss") + "\t" + msg + Environment.NewLine);
			}));
		}

		private void ServerWindow_StopSharing_Click(object sender, EventArgs e)
		{
			stopSharing = true;
		}

		private void RespondButton_Click(object sender, EventArgs e)
		{

		}
	}
}
