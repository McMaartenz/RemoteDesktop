
namespace RemoteDesktop
{
	partial class StartWindow
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RemoteHost_Connect = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.RemoteHost_IPInputBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.RemoteHost_PortInputBox = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CreateHost_PortInputBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.CreateHost_Start = new System.Windows.Forms.Button();
			this.CreateHost_AllowOutsideConnections = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.RemoteHost_PortInputBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.RemoteHost_IPInputBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.RemoteHost_Connect);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(249, 164);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connect to a remote host";
			// 
			// RemoteHost_Connect
			// 
			this.RemoteHost_Connect.Location = new System.Drawing.Point(6, 135);
			this.RemoteHost_Connect.Name = "RemoteHost_Connect";
			this.RemoteHost_Connect.Size = new System.Drawing.Size(75, 23);
			this.RemoteHost_Connect.TabIndex = 0;
			this.RemoteHost_Connect.Text = "Connect";
			this.RemoteHost_Connect.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "IP Address";
			// 
			// RemoteHost_IPInputBox
			// 
			this.RemoteHost_IPInputBox.Location = new System.Drawing.Point(96, 31);
			this.RemoteHost_IPInputBox.Name = "RemoteHost_IPInputBox";
			this.RemoteHost_IPInputBox.Size = new System.Drawing.Size(147, 20);
			this.RemoteHost_IPInputBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Port";
			// 
			// RemoteHost_PortInputBox
			// 
			this.RemoteHost_PortInputBox.Location = new System.Drawing.Point(96, 57);
			this.RemoteHost_PortInputBox.Name = "RemoteHost_PortInputBox";
			this.RemoteHost_PortInputBox.Size = new System.Drawing.Size(147, 20);
			this.RemoteHost_PortInputBox.TabIndex = 4;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CreateHost_AllowOutsideConnections);
			this.groupBox2.Controls.Add(this.CreateHost_PortInputBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.CreateHost_Start);
			this.groupBox2.Location = new System.Drawing.Point(285, 13);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(226, 164);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Create a host";
			// 
			// CreateHost_PortInputBox
			// 
			this.CreateHost_PortInputBox.Location = new System.Drawing.Point(88, 31);
			this.CreateHost_PortInputBox.Name = "CreateHost_PortInputBox";
			this.CreateHost_PortInputBox.Size = new System.Drawing.Size(73, 20);
			this.CreateHost_PortInputBox.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 34);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Port";
			// 
			// CreateHost_Start
			// 
			this.CreateHost_Start.Location = new System.Drawing.Point(6, 135);
			this.CreateHost_Start.Name = "CreateHost_Start";
			this.CreateHost_Start.Size = new System.Drawing.Size(75, 23);
			this.CreateHost_Start.TabIndex = 0;
			this.CreateHost_Start.Text = "Start";
			this.CreateHost_Start.UseVisualStyleBackColor = true;
			// 
			// CreateHost_AllowOutsideConnections
			// 
			this.CreateHost_AllowOutsideConnections.AutoSize = true;
			this.CreateHost_AllowOutsideConnections.Location = new System.Drawing.Point(6, 112);
			this.CreateHost_AllowOutsideConnections.Name = "CreateHost_AllowOutsideConnections";
			this.CreateHost_AllowOutsideConnections.Size = new System.Drawing.Size(215, 17);
			this.CreateHost_AllowOutsideConnections.TabIndex = 5;
			this.CreateHost_AllowOutsideConnections.Text = "Allow connections outside local network";
			this.CreateHost_AllowOutsideConnections.UseVisualStyleBackColor = true;
			// 
			// StartWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 192);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "StartWindow";
			this.Text = "Remote Desktop";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button RemoteHost_Connect;
		private System.Windows.Forms.TextBox RemoteHost_PortInputBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox RemoteHost_IPInputBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox CreateHost_AllowOutsideConnections;
		private System.Windows.Forms.TextBox CreateHost_PortInputBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button CreateHost_Start;
	}
}

