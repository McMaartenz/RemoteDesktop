using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
			if (!UInt16.TryParse(Port, out UInt16 RemoteHostPort))
			{
				Console.WriteLine("Invalid port");
				MessageBox.Show("Please enter a valid port 0-65535.", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			// Prevent clicking again
			CreateHost_Start.Enabled = false;
			RemoteHost_Connect.Enabled = false;

			ServerCode((UInt16)port);


			CreateHost_Start.Enabled = true;
			RemoteHost_Connect.Enabled = true;
		}

		private void ServerCode(UInt16 port)
		{
			IPAddress selfIP = IPAddress.Parse("127.0.0.1");
			IPEndPoint localEP = new IPEndPoint(selfIP, (Int32)port);
			// Connect
			try
			{
				Socket server = new Socket(selfIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				server.Bind(localEP);

				server.Listen(1); // Allow 1 client.

				Console.WriteLine("Waiting for a connection to take place");
				Socket handler = server.Accept();

				string data = "";
				byte[] bytes = null;

				while (data.IndexOf("EOF:;") > -1) // EOF
				{
					bytes = new byte[1024];
					int bytesRec = handler.Receive(bytes);
					data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
				}

				Console.WriteLine("Text received from client: '{0}'", data);

				data = "C:OK;"; // C(onnect) is OK
				byte[] msg = Encoding.ASCII.GetBytes(data); // Send response
				handler.Send(msg);
				Console.WriteLine("Sent answer to client.");

				#region Close socket, remove this later on. We want a continuous connection.
				handler.Shutdown(SocketShutdown.Both);
				handler.Close();
				server.Close();
				server = null;
				#endregion

			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.ToString(), "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
