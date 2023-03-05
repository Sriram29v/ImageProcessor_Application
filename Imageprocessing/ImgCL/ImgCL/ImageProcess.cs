using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImgCL
{
    public class ImageProcess:MarshalByRefObject
    {
        /// <summary>
        /// FlipHorizontal & FlipVeritcal
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap FlipHorizontal(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate180FlipX);
            return new Bitmap(image);
        }

        public static Bitmap FlipVertical(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate180FlipY);
            return new Bitmap(image);
        }

        public static Bitmap RotateLeft(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return new Bitmap(image);
        }

        public static Bitmap RotateRight(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return new Bitmap(image);
        }


        public static Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            rotatedImage.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }

        /// <summary>
        /// Image Resize
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>


        public static Bitmap ResizeAsNew(Bitmap image, int width, int height)
        {
            return new Bitmap(image, width, height);
        }


        public static void ResizeOriginal(ref Bitmap image, int width, int height)
        {
            // Retain pointer to original for disposal
            Bitmap resizedBitmap = new Bitmap(image, width, height);

            // Dispose of original image
            image.Dispose();

            // Assign to original ref
            image = resizedBitmap;
        }


        public static Bitmap ResizeAsNew(Bitmap image, double sizeRatio)
        {
            // Maintain aspect ratio
            int newWidth = (int)Math.Round(image.Width * sizeRatio);
            int newHeight = (int)Math.Round(image.Height * sizeRatio);

            return ResizeAsNew(image, newWidth, newHeight);
        }

        public static void ResizeOriginal(ref Bitmap image, double sizeRatio)
        {
            // Maintain aspect ratio
            int newWidth = (int)Math.Round(image.Width * sizeRatio);
            int newHeight = (int)Math.Round(image.Height * sizeRatio);

            ResizeOriginal(ref image, newWidth, newHeight);
        }

        /// <summary>
        /// Image GrayScale
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>

        public static Bitmap ConvertToGray(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// Image ThumbNail
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>

        public static Image CreateThumb(Image src)
        {
            // Compute thumbnail size.
            Size thumbnailSize = GetThumbnailSize(src);

            // Get thumbnail.
            Image thumbnail = src.GetThumbnailImage(thumbnailSize.Width,
                thumbnailSize.Height, null, IntPtr.Zero);

            // Save thumbnail.
            return thumbnail;
        }


        static Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 40;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Return original size if image is smaller than maxPixels
            if (originalWidth <= maxPixels || originalHeight <= maxPixels)
            {
                return new Size(originalWidth, originalHeight);
            }

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
    }
}
