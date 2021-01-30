using System;
using System.Net;
using System.Windows.Forms;

namespace RemoteDesktop
{
	public partial class StartWindow : Form
	{
		[STAThread]
		public static void Start()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new StartWindow());
		}

		public StartWindow()
		{
			InitializeComponent();
		}

		private (IPAddress, UInt16?) ValidateIPAndPort(string IP, string Port)
		{
			// Validate given IP address and port.
			if (!IPAddress.TryParse(IP, out IPAddress RemoteHostIP))
			{
				Console.WriteLine("Invalid IP address");
				MessageBox.Show("Please enter a valid IP address.", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return (null, null);
			}
			if (!UInt16.TryParse(Port, out UInt16 RemoteHostPort) || RemoteHostPort < 0 || RemoteHostPort > UInt16.MaxValue)
			{
				Console.WriteLine("Invalid port");
				MessageBox.Show("Please enter a valid port 0-65535.", "Valdiation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return (null, null);
			}
			return (RemoteHostIP, RemoteHostPort);
		}

		private void RemoteHost_Connect_Click(object sender, EventArgs e)
		{
			(IPAddress IP, UInt16? Port) address = ValidateIPAndPort(RemoteHost_IPInputBox.Text, RemoteHost_PortInputBox.Text);
			if (address.IP == null)
			{
				return;
			}
			// Client code
		}

		private void CreateHost_Start_Click(object sender, EventArgs e)
		{
			UInt16? port = ValidateIPAndPort("127.0.0.1", CreateHost_PortInputBox.Text).Item2;
			if (port == null)
			{
				return;
			}
			// Server code
		}
	}
}
