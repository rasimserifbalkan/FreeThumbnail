using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace FreeThumbnail.Library
{
    public class Creator
    {
        public Image CreateThumbnail(int height, int weight, Stream file)
        {
            var bitmapFile = new Bitmap(file);
            int sourceHeight;
            int truncate;
            int heightRound;
            int num4;
            if (bitmapFile.Width > bitmapFile.Height)
            {
                var d2 = decimal.Divide(new decimal(height), new decimal(bitmapFile.Width));
                sourceHeight = height;
                truncate = Convert.ToInt32(Math.Truncate(decimal.Multiply(new decimal(bitmapFile.Height), d2)));
                heightRound = 0;
                num4 = checked((int)Math.Round(checked(weight - truncate) / 2.0));
            }
            else
            {
                var d2 = decimal.Divide(new decimal(weight), new decimal(bitmapFile.Height));
                truncate = weight;
                sourceHeight = Convert.ToInt32(Math.Truncate(decimal.Multiply(new decimal(bitmapFile.Width), d2)));
                num4 = 0;
                heightRound = checked((int)Math.Round(checked(height - sourceHeight) / 2.0));
            }
            var result = new Bitmap(height, weight);
            var graphics = Graphics.FromImage(result);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.FillRectangle(Brushes.White, 0, 0, height, weight);
            graphics.DrawImage(bitmapFile, heightRound, num4, (float)sourceHeight, truncate);

            return result;
        }
    }
}
