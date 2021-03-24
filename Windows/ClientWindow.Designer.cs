
namespace RemoteDesktop
{
	partial class ClientWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CloseCon = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// CloseCon
			// 
			this.CloseCon.Location = new System.Drawing.Point(-1, -1);
			this.CloseCon.Name = "CloseCon";
			this.CloseCon.Size = new System.Drawing.Size(119, 23);
			this.CloseCon.TabIndex = 1;
			this.CloseCon.Text = "Close connection";
			this.CloseCon.UseVisualStyleBackColor = true;
			this.CloseCon.Click += new System.EventHandler(this.CloseCon_Click);
			// 
			// ClientWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 681);
			this.Controls.Add(this.CloseCon);
			this.KeyPreview = true;
			this.Name = "ClientWindow";
			this.Text = "Client";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientWindow_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button CloseCon;
	}
}