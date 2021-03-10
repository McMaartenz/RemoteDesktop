using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoteDesktop
{
	public partial class StartWindow : Form
	{
		[STAThread]
		internal static void Start()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(Program.form = new StartWindow());
		}

		internal StartWindow()
		{
			InitializeComponent();
			(Program.sw = new ServerWindow()).Hide();
			(Program.cw = new ClientWindow()).Hide();
		}
		
		private void ExceptionMsg(Exception e)
		{
			MessageBox.Show(e.ToString(), "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			// Prevent clicking again
			CreateHost_Start.Enabled = false;
			RemoteHost_Connect.Enabled = false;
			Visible = false;
			Program.cw.Show();

			Thread clientThread = new Thread(new ParameterizedThreadStart(ClientCode));
			clientThread.Start(address);
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
			Visible = false;
			Program.sw.Show();

			Thread serverThread = new Thread(new ParameterizedThreadStart(ServerCode));
			serverThread.Start(port);
		}

		private void ServerCode(object obj)
		{
			UInt16 port = (UInt16)obj;
			IPAddress selfIP = IPAddress.Parse("127.0.0.1");
			IPEndPoint localEP = new IPEndPoint(selfIP, port);
			// Connect
			try
			{

				// TODO do rewrite
				Socket server = new Socket(selfIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				server.Bind(localEP);

				server.Listen(1); // Allow 1 client.

				Program.sw.LogMessage("Waiting for a connection to take place");
				Socket handler = server.Accept();
				Program.sw.LogMessage("Connected to a client");

				bool shouldStayConnected = true;
				// temporary
				while (!Program.sw.stopSharing)
				{
					string data = "";
					byte[] bytes = null;

					do
					{
						bytes = new byte[1024];
						int bytesRec = handler.Receive(bytes);
						data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
					}
					while (data.IndexOf("<EOF>") == -1);

					Console.WriteLine();
					Program.sw.LogMessage("Text received from client: '" + data + '\'');


					// 200 is HTTP Status code for OK
					byte[] msg = Encoding.ASCII.GetBytes("200"); // Send response
					handler.Send(msg);
					Program.sw.LogMessage("Sent answer to client.");
					Thread.Sleep(500);
					shouldStayConnected = !Program.sw.stopSharing; // TODO check data and handle it
				}

				handler.Send(Encoding.ASCII.GetBytes("CLOSE"));
				Thread.Sleep(100);
				handler.Shutdown(SocketShutdown.Both);
				handler.Close();
				server.Close();
				server = null;

			}
			catch (Exception exc)
			{
				ExceptionMsg(exc);
			}
			finally
			{
				MessageBox.Show("The remote host session ended.", "Session ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
				BeginInvoke(new Action(() => // Send to GUI thread
				{
					CreateHost_Start.Enabled = true;
					RemoteHost_Connect.Enabled = true;
					Visible = true;
					Program.sw.Hide();
				}));
			}
		}

		private void ClientCode(object obj)
		{
			(IPAddress IP, UInt16? Port) address = ((IPAddress, UInt16?)) obj;
			byte[] bytes = new byte[1024];

			try
			{
				IPEndPoint remoteEP = new IPEndPoint(address.IP, (Int32)address.Port);
				Socket client = new Socket(address.IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				client.Connect(remoteEP);
				Console.WriteLine("Connected to remote server");

				string received;
				int bytesSent, bytesRec;
				byte[] msg;

				while (client.Connected)
				{
					msg = Encoding.ASCII.GetBytes("Hello World!<EOF>"); // TODO sends code, should include metadata e.g. screen resolution
					bytesSent = client.Send(msg);
					
					bytesRec = client.Receive(bytes);
					received = Encoding.ASCII.GetString(bytes, 0, bytesRec);
					
					Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\tReceived from remote server: " + received, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
					if (received == "CLOSE")
					{
						MessageBox.Show("Connection closed by host.", "Connection terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					}
				}

				client.Shutdown(SocketShutdown.Both);
				client.Close();
				client = null;
			}
			catch (Exception exc)
			{
				ExceptionMsg(exc);
			}
			finally
			{
				BeginInvoke(new Action(() => // Send to GUI thread
				{
					CreateHost_Start.Enabled = true;
					RemoteHost_Connect.Enabled = true;
					Visible = true;
					Program.cw.Hide();
				}));
			}
		}
	}
}
