using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    public class ScreenShot
    {
        public static bool Create(string name, Size size, Rect offset)
        {
            try
            {

                var bmpScreenshot = new Bitmap(size.Width,
                               size.Height,
                               PixelFormat.Format32bppArgb);

                // Create a graphics object from the bitmap.
                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(offset.Left,
                                            offset.Top,
                                            0,
                                            0,
                                            size,
                                            CopyPixelOperation.SourceCopy);

                // Save the screenshot to the specified path that the user has chosen.
                bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
