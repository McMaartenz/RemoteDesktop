using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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

		public string GetLocalIpAddress()
		{
			UnicastIPAddressInformation mostSuitableIp = null;

			var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

			foreach (var network in networkInterfaces)
			{
				if (network.OperationalStatus != OperationalStatus.Up)
					continue;

				var properties = network.GetIPProperties();

				if (properties.GatewayAddresses.Count == 0)
					continue;

				foreach (var address in properties.UnicastAddresses)
				{
					if (address.Address.AddressFamily != AddressFamily.InterNetwork)
						continue;

					if (IPAddress.IsLoopback(address.Address))
						continue;

					if (!address.IsDnsEligible)
					{
						if (mostSuitableIp == null)
							mostSuitableIp = address;
						continue;
					}

					// The best IP is the IP got from DHCP server
					if (address.PrefixOrigin != PrefixOrigin.Dhcp)
					{
						if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
							mostSuitableIp = address;
						continue;
					}

					return address.Address.ToString();
				}
			}

			return (mostSuitableIp != null) ? mostSuitableIp.Address.ToString() : "";
		}

		private void ServerCode(object obj)
		{
			UInt16 port = (UInt16)obj;
			IPAddress selfIP = IPAddress.Parse(GetLocalIpAddress());//GetLocalIPv4(NetworkInterfaceType.Ethernet));
			IPEndPoint localEP = new IPEndPoint(selfIP, port);
			IOHandler IOH = new IOHandler();

			// Connect
			try
			{

				// TODO do rewrite
				Socket server = new Socket(selfIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				server.Bind(localEP);
				server.Listen(1); // Allow 1 client.

				Program.sw.LogMessage("Waiting for a connection to take place on " + selfIP.ToString() + ":" + port);
				Socket handler = server.Accept();
				handler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
				Program.sw.LogMessage("Connected to a client");

				// temporary
				while (!Program.sw.stopSharing)
				{
					string data = "";
					byte[] bytes = null;

					do
					{
						bytes = new byte[450000];
						int bytesRec = handler.Receive(bytes);
						data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
					}
					while (data.IndexOf("<EOF>") == -1);

					// Remove <EOF>
					data = data.Substring(0, data.Length - 5);


					Console.WriteLine();
					Program.sw.LogMessage("Text received from client: '" + data + '\'');

					if (data.Substring(0, 5) == "CLOSE")
					{
						Program.sw.stopSharing = true;
					}
					else
					{
						IOH.HandleEvent(data);
					}

					byte[] msg = Utility.BitmapToByteArr(Utility.CaptureScreen());

					//Console.WriteLine(outp.Length);
					handler.Send(msg);
					Program.sw.LogMessage("Sent answer to client.");
					Thread.Sleep(500);
				}

				handler.Send(Encoding.ASCII.GetBytes("CLOSE<EOF>"));
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
			byte[] bytes = new byte[450000];

			try
			{
				IPEndPoint remoteEP = new IPEndPoint(address.IP, (Int32)address.Port);
				Socket client = new Socket(address.IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				client.Connect(remoteEP);
				client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
				Console.WriteLine("Connected to remote server");

				string received;
				int bytesSent, bytesRec;
				byte[] msg;
				string sendData;

				while (client.Connected)
				{
					sendData = "";

					if (Program.cw.stopSharing)
					{
						sendData = "CLOSE<EOF>";
					}
					else
					{
						lock (Program.cw.KeyCodes)
						{
							if (Program.cw.KeyCodes.Count > 0)
							{
								sendData = "INPTEV";
								while (Program.cw.KeyCodes.Count > 0)
								{
									sendData += "name=key,keycode=" + Program.cw.KeyCodes.Dequeue().ToString("X");
									if (Program.cw.KeyCodes.Count > 0)
									{
										sendData += "/";
									}
								}
							}
							else
							{
								sendData = "MSGIEVdata=No keys pressed";
							}
						}

						lock (Program.cw.MouseEvents)
						{
							if (Program.cw.MouseEvents.Count > 0)
							{
								sendData = "INPTEV";
								while (Program.cw.KeyCodes.Count > 0)
								{
									MouseEventArgs v = Program.cw.MouseEvents.Dequeue();
									sendData += "name=mouse,x=" + v.X + ",y=" + v.Y;
									if (Program.cw.KeyCodes.Count > 0)
									{
										sendData += "/";
									}
								}
							}
							else if (sendData == "")
							{
								sendData = "MSGIEVdata=No keys pressed";
							}
						}

						sendData += "<EOF>"; // End of stream
					}
					msg = Encoding.ASCII.GetBytes(sendData);
					bytesSent = client.Send(msg);
					
					bytesRec = client.Receive(bytes);


					try
					{
						//set screen
						Program.cw.screen = Utility.ByteArrToBitmap(bytes);
						Program.cw.UpdateScreen();
						received = "200";
					}
					catch (Exception e)
					{
						try
						{
							received = Encoding.ASCII.GetString(bytes, 0, bytesRec);
							Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\tReceived from remote server: " + received, "Message");
						}
						catch (Exception e2)
						{
							Console.WriteLine("Unknown crap sent by host");
							received = "500";
						}
					}

					if (Program.cw.stopSharing)
					{
						client.Send(Encoding.ASCII.GetBytes("CLOSE<EOF>"));
						MessageBox.Show("Connection closed.", "Connection terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					}
					else if (received == "CLOSE")
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
