using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace FreeThumbnail.Library
{
    public class Creator
    {
        public Image CreateThumbnail(int height, int weight, Stream file)
        {
            Bitmap bitmapFile = new Bitmap(file);
            ImageFormat rawFormat = bitmapFile.RawFormat;
            int sourceHeight;
            int truncate;
            int heightRound;
            int num4;
            if (bitmapFile.Width > bitmapFile.Height)
            {
                Decimal d2 = Decimal.Divide(new Decimal(height), new Decimal(bitmapFile.Width));
                sourceHeight = height;
                truncate = Convert.ToInt32(Math.Truncate(Decimal.Multiply(new Decimal(bitmapFile.Height), d2)));
                heightRound = 0;
                num4 = checked((int)Math.Round(unchecked((double)checked(weight - truncate) / 2.0)));
            }
            else
            {
                Decimal d2 = Decimal.Divide(new Decimal(weight), new Decimal(bitmapFile.Height));
                truncate = weight;
                sourceHeight = Convert.ToInt32(Math.Truncate(Decimal.Multiply(new Decimal(bitmapFile.Width), d2)));
                num4 = 0;
                heightRound = checked((int)Math.Round(unchecked((double)checked(height - sourceHeight) / 2.0)));
            }
            Bitmap result = new Bitmap(height, weight);
            Graphics graphics = Graphics.FromImage((Image)result);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.FillRectangle(Brushes.White, 0, 0, height, weight);
            graphics.DrawImage((Image)bitmapFile, (float)heightRound, (float)num4, (float)sourceHeight, (float)truncate);

            return result;



        }
    }
}
