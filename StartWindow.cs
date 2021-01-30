using System;
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
	}
}
