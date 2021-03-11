using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace RemoteDesktop
{
    class SchreenToByteArray
    {
        private void Capture(object)
        {
            Bitmap Capture = new Bitmap(Schreen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height PixelFormat.Format32bppArgb);
            return Capture
                
        }
    }
    public static byte[] BitmapToByte2(Capture img)
    {
        using (var stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
