using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MT3CardTools.Src.Helpers
{
    static class DrawingExtensions
    {
        public static PointF Add(this PointF a, PointF b) => new PointF(a.X + b.X, a.Y + b.Y);
        public static PointF Add(this PointF a, float b) => new PointF(a.X + b, a.Y + b);
        public static PointF Subtract(this PointF a, PointF b) => new PointF(a.X - b.X, a.Y - b.Y);
        public static PointF Subtract(this PointF a, float b) => new PointF(a.X - b, a.Y - b);
        public static PointF Multiply(this PointF a, PointF b) => new PointF(a.X * b.X, a.Y * b.Y);
        public static PointF Multiply(this PointF a, float b) => new PointF(a.X * b, a.Y * b);
        public static PointF Zero(this PointF a, PointF b) => new PointF(a.X > 0 ? a.X - b.X : a.X + b.X, a.Y > 0 ? a.Y - b.Y : a.Y + b.Y);
        public static PointF Zero(this PointF a, float b) => new PointF(a.X > 0 ? a.X - b : a.X + b, a.Y > 0 ? a.Y - b : a.Y + b);
        public static Point ToPoint(this PointF a) => new Point((int)Math.Floor(a.X), (int)Math.Floor(a.Y));

        public static Bitmap RotateImage(this Bitmap img, float rotationAngle)
        {
            var bmp = new Bitmap(img.Width, img.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                g.RotateTransform(rotationAngle);
                g.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(img, new Point(0, 0));
                return bmp;
            }
        }

        //TODO: Is there a safe way to do this?
        public static Bitmap To1Bpp(this Bitmap img, float contrast = 0.85f)
        {
#if DEBUG
            unsafe
            {
                var result = new Bitmap(img.Width, img.Height, PixelFormat.Format1bppIndexed);

                // Lock source and destination in memory for unsafe access
                var bmbo = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly,
                                         img.PixelFormat);
                var bmdn = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.ReadWrite,
                                         result.PixelFormat);

                var imgScan0 = bmbo.Scan0;
                var resultScan0 = bmdn.Scan0;

                var imgStride = bmbo.Stride;
                var resultStride = bmdn.Stride;

                byte* sourcePixels = (byte*)(void*)imgScan0;
                byte* destPixels = (byte*)(void*)resultScan0;

                var imgLineIdx = 0;
                var resultLineIdx = 0;
                var hmax = img.Height - 1;
                var wmax = img.Width - 1;
                for (int y = 0; y < hmax; y++)
                {
                    // find indexes for source/destination lines

                    // use addition, not multiplication?
                    imgLineIdx += imgStride;
                    resultLineIdx += resultStride;

                    var imgIdx = imgLineIdx;
                    for (int x = 0; x < wmax; x++)
                    {
                        // index for source pixel (32bbp, rgba format)
                        imgIdx += 4;
                        var r = sourcePixels[imgIdx + 2];
                        var g = sourcePixels[imgIdx + 1];
                        var b = sourcePixels[imgIdx];

                        // could just check directly?
                        if (Color.FromArgb(r,g,b).GetBrightness() > contrast)
                        {
                            // destination byte for pixel (1bpp, ie 8pixels per byte)
                            var idx = resultLineIdx + (x >> 3);
                            // mask out pixel bit in destination byte
                            destPixels[idx] |= (byte)(0x80 >> (x & 0x7));
                        }
                    }
                }
                img.UnlockBits(bmbo);
                result.UnlockBits(bmdn);

                return result;
            }
#else
            return null;
#endif
        }
    }
}
