
namespace RemoteDesktop
{
	partial class ServerWindow
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
			this.LogBox = new System.Windows.Forms.GroupBox();
			this.ServerWindow_Log = new System.Windows.Forms.TextBox();
			this.ServerWindow_StopSharing = new System.Windows.Forms.Button();
			this.RespondButton = new System.Windows.Forms.Button();
			this.LogBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// LogBox
			// 
			this.LogBox.Controls.Add(this.ServerWindow_Log);
			this.LogBox.Location = new System.Drawing.Point(13, 13);
			this.LogBox.Name = "LogBox";
			this.LogBox.Size = new System.Drawing.Size(538, 328);
			this.LogBox.TabIndex = 0;
			this.LogBox.TabStop = false;
			this.LogBox.Text = "Log";
			// 
			// ServerWindow_Log
			// 
			this.ServerWindow_Log.BackColor = System.Drawing.SystemColors.MenuBar;
			this.ServerWindow_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ServerWindow_Log.Location = new System.Drawing.Point(7, 20);
			this.ServerWindow_Log.Multiline = true;
			this.ServerWindow_Log.Name = "ServerWindow_Log";
			this.ServerWindow_Log.ReadOnly = true;
			this.ServerWindow_Log.Size = new System.Drawing.Size(525, 302);
			this.ServerWindow_Log.TabIndex = 0;
			// 
			// ServerWindow_StopSharing
			// 
			this.ServerWindow_StopSharing.Location = new System.Drawing.Point(557, 19);
			this.ServerWindow_StopSharing.Name = "ServerWindow_StopSharing";
			this.ServerWindow_StopSharing.Size = new System.Drawing.Size(87, 23);
			this.ServerWindow_StopSharing.TabIndex = 1;
			this.ServerWindow_StopSharing.Text = "Stop sharing";
			this.ServerWindow_StopSharing.UseVisualStyleBackColor = true;
			this.ServerWindow_StopSharing.Click += new System.EventHandler(this.ServerWindow_StopSharing_Click);
			// 
			// RespondButton
			// 
			this.RespondButton.Location = new System.Drawing.Point(558, 49);
			this.RespondButton.Name = "RespondButton";
			this.RespondButton.Size = new System.Drawing.Size(86, 23);
			this.RespondButton.TabIndex = 2;
			this.RespondButton.Text = "Respond";
			this.RespondButton.UseVisualStyleBackColor = true;
			this.RespondButton.Click += new System.EventHandler(this.RespondButton_Click);
			// 
			// ServerWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(656, 353);
			this.Controls.Add(this.RespondButton);
			this.Controls.Add(this.ServerWindow_StopSharing);
			this.Controls.Add(this.LogBox);
			this.Name = "ServerWindow";
			this.Text = "ServerWindow";
			this.LogBox.ResumeLayout(false);
			this.LogBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox LogBox;
		private System.Windows.Forms.TextBox ServerWindow_Log;
		private System.Windows.Forms.Button ServerWindow_StopSharing;
		private System.Windows.Forms.Button RespondButton;
	}
}