using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace RemoteDesktop
{
    class Utility
    {

        internal static byte[] BitmapToByteArr(Bitmap img)
        {
            Bitmap imgScaled;
            
            if (img.Size.Height > 1920 || img.Size.Width > 1080)
			{
                imgScaled = new Bitmap(img, 1920, 1080);
			}
			else
			{
                imgScaled = img;
			}

            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        internal static Bitmap ByteArrToBitmap(byte[] b)
		{
            Bitmap boutp;
            ImageConverter ic = new ImageConverter();

            boutp = (Bitmap)ic.ConvertFrom(b);
            return boutp;
		}

        internal static Bitmap TakeScreenShot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                return bmp;
            }
        }
    }
}
