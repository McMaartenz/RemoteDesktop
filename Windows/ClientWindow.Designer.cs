
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientWindow));
			this.SuspendLayout();
			// 
			// ClientWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.ClientSize = new System.Drawing.Size(796, 467);
			this.DoubleBuffered = true;
			this.Name = "ClientWindow";
			this.Text = "ClientWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientWindow_FormClosing);
			this.ResizeEnd += new System.EventHandler(this.ClientWindow_ResizeEnd);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientWindow_KeyDown_1);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClientWindow_MouseDown);
			this.ResumeLayout(false);

        }

        #endregion
    }
}