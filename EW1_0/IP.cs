using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace EW1_0
{
    public class IP //IP = Image processing.
    {
        public static Rectangle ScreenRectangle = Screen.GetBounds(Screen.GetBounds(Point.Empty));
        //Define a rectangle with the size of screen.

        //Create Blank bitmap for start.
        public static Bitmap CreateBlank(Bitmap BPP8)
        {
            unsafe
            {
                BitmapData bitmapData1 = BPP8.LockBits(new Rectangle(0, 0, BPP8.Width, BPP8.Height), ImageLockMode.ReadWrite, BPP8.PixelFormat);
                //Lock the entranced bitmap into the ram for processing.
                byte* PtrFirstPixel1 = (byte*)bitmapData1.Scan0;
                //Get first pixle's byte address of the bitmap
                for (int y = 0; y < bitmapData1.Height; y++)
                {
                    byte* currentLine = PtrFirstPixel1 + (y * bitmapData1.Stride);
                    //Stride = the width of a byte's row of a bitmap
                    for (int x = 0; x < bitmapData1.Width; x++)
                    {
                        currentLine[x] = 128;
                        //Fill the byte for the white bitmap
                    }
                }
                BPP8.UnlockBits(bitmapData1);
                return BPP8;
            }
        }
        //Take ScreenShot.
        public static Bitmap TakeScreenShot(Bitmap ScreenBitmap)
        {
            using (Graphics g = Graphics.FromImage(ScreenBitmap))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, ScreenRectangle.Size);
                return ScreenBitmap;
            }

        }
        //Convert a 24bpp bitmap to 8bpp bitmap.
        public static Bitmap To8BPP(Bitmap bt1)
        {
            unsafe
            {
                BitmapData bitmapData1 = bt1.LockBits(new Rectangle(0, 0, bt1.Width, bt1.Height), ImageLockMode.ReadWrite, bt1.PixelFormat);
                //Define a 8bpp bitmap to convert entranced bitmap to 8bpp.
                Bitmap bpp8 = new Bitmap(bt1.Width, bt1.Height, PixelFormat.Format8bppIndexed);
                BitmapData bitmapData2 = bpp8.LockBits(new Rectangle(0, 0, bpp8.Width, bpp8.Height), ImageLockMode.ReadWrite, bpp8.PixelFormat);
                int bytesPerPixel1 = Bitmap.GetPixelFormatSize(bt1.PixelFormat) / 8;
                int heightInPixels1 = bitmapData1.Height;
                int widthInBytes1 = bitmapData1.Width * bytesPerPixel1;
                int widthInBytes2 = bitmapData2.Width;
                byte* PtrFirstPixel1 = (byte*)bitmapData1.Scan0;
                byte* PtrFirstPixel2 = (byte*)bitmapData2.Scan0;
                for (int y = 0; y < heightInPixels1; y++)
                {
                    //Define 2 pointers for point the pixles.
                    byte* currentLine = PtrFirstPixel1 + (y * bitmapData1.Stride);
                    byte* currentLine2 = PtrFirstPixel2 + (y * bitmapData2.Stride);
                    for (int x = 0; x < widthInBytes1; x = x + bytesPerPixel1)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                        //the formula for make a colored pixel to gray one.
                        int av = (30 * oldRed + 59 * oldGreen + 11 * oldBlue) / 100;
                        currentLine2[x / 3] = (byte)av;
                    }
                }
                bt1.UnlockBits(bitmapData1);
                bpp8.UnlockBits(bitmapData2);
                return bpp8;
            }
        }
        //Get a size and a bitmap and split the bitmap to the cutsize pieces.
        public static int Counter = 0;
        public static Bitmap[] SplitImage(int Size, Bitmap img)
        {
            Counter++;
            int Width = img.Width, Height = img.Height;

            int WidthPiece = Width / Size;
            int HeightPiece = Height / Size;

            if (Width % Size != 0)
            {
                WidthPiece++;
            }
            if (Height % Size != 0)
            {
                HeightPiece++;
            }

            int[] x = new int[WidthPiece + 1];
            int[] y = new int[HeightPiece + 1];

            for (int i = 1; i < WidthPiece; i++)
            {
                x[i] = (i) * Size;
            }
            x[WidthPiece] = Width;
            x[0] = 0;
            for (int j = 1; j < HeightPiece; j++)
            {
                y[j] = (j) * Size;
            }
            y[HeightPiece] = Height;
            y[0] = 0;

            Bitmap[] Pieces = new Bitmap[HeightPiece * WidthPiece];

            int counter = 0;

            for (int i = 0; i < WidthPiece; i++)
            {
                for (int j = 0; j < HeightPiece; j++)
                {
                    Pieces[counter] = img.Clone(new Rectangle(x[i], y[j], x[i + 1] - x[i], y[j + 1] - y[j]), img.PixelFormat);
                    counter++;
                }
            }
            return Pieces;
        }
        //find the Points of the splited bitmaps(different).
        public static Point[] DiffPoints(Bitmap[] bmp1, Bitmap[] bmp2, int Bios, int OriginalHeight, int CutSize)
        {
            int counter = 0;
            bool equals = true;
            int Different = ((Bios * bmp1[0].Width * bmp1[0].Height) / 100);//different = Percentage of differences between 2 blocks. Max = 100
            int bh = OriginalHeight / CutSize;
            if (OriginalHeight % CutSize != 0) bh++;
            int count = 0;
            int x = 0;
            int y = 0;
            int PointCounter = 0;
            Point[] Points = new Point[bmp1.Length];
            for (int m = 0; m < Points.Length; m++)
            {
                Points[m].X = -1; Points[m].Y = -1;
            }//-1 like as null value
            unsafe
            {
                for (int BlockNumber = 0; BlockNumber < bmp1.Length; BlockNumber++)//Move into Blocks.
                {
                    counter = 0;
                    Rectangle rect = new Rectangle(0, 0, bmp1[BlockNumber].Width, bmp1[BlockNumber].Height);
                    BitmapData bmpData1 = bmp1[BlockNumber].LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                    BitmapData bmpData2 = bmp2[BlockNumber].LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

                    byte* ptr1 = (byte*)bmpData1.Scan0.ToPointer();
                    byte* ptr2 = (byte*)bmpData2.Scan0.ToPointer();
                    for (int HeightLevel = 0; equals && HeightLevel < rect.Height; HeightLevel++)
                    {

                        for (int WidthLevel = 0; equals && WidthLevel < rect.Width; WidthLevel++)
                        {
                            if (*ptr1 != *ptr2)
                            {
                                counter++;
                                if (counter > Different)
                                {
                                    count = BlockNumber / bh;

                                    x = count * CutSize;

                                    y = (BlockNumber % bh) * CutSize;

                                    Points[PointCounter] = new Point(x, y);
                                    PointCounter++;
                                    counter = 0;
                                    equals = false;
                                }
                            }
                            ptr1++;
                            ptr2++;
                        }

                        ptr1 += bmpData1.Stride - rect.Width;
                        ptr2 += bmpData2.Stride - rect.Width;
                    }
                    equals = true;
                    bmp1[BlockNumber].UnlockBits(bmpData1);
                    bmp2[BlockNumber].UnlockBits(bmpData2);                    
                }
            }


            return Points;
        }
        //Clone the original image with the entranced point array.
        public static List<byte[]> ImageClones(Bitmap Img, Point[] Points, int CutSize)
        {            
            int ImagePiecesCounts = 0;
            for (int i = 0; i < Points.Length + 1 ; i++)//find the count of different blocks.
            {
                ImagePiecesCounts = i;
                if (i < Points.Length && Points[i].X == -1)
                {
                    break;
                }
            }
            List<byte[]> List_Byte = new List<byte[]>();
            Bitmap[] Pieces = new Bitmap[ImagePiecesCounts];

            int WCut = CutSize;
            int HCut = CutSize;

            int WPieceToEdge = (Img.Width / CutSize) * CutSize;
            int EdgeX = Img.Width % CutSize;

            int HPieceToEdge = (Img.Height / CutSize) * CutSize;
            int EdgeY = Img.Height % CutSize;

            for (int i = 0; i < ImagePiecesCounts; i++)
            {
                if (Points[i].X == WPieceToEdge)
                {
                    WCut = EdgeX;
                }
                if (Points[i].Y == HPieceToEdge)
                {
                    HCut = EdgeY;
                }                
                Pieces[i] = Img.Clone(new Rectangle(Points[i].X, Points[i].Y, WCut, HCut), Img.PixelFormat);
                List_Byte.Add(ChangeQuality.QualityChange(Pieces[i], 100));
                HCut = CutSize;
                WCut = CutSize;
            }
            return List_Byte;
        }
        //convert a byte array to bitmap.
        public static Bitmap ConvertByteToBitmap(byte[] Bitm)
        {
            using (MemoryStream ms = new MemoryStream(Bitm))
            {
                return (Bitmap)Bitmap.FromStream(ms);
            }

        }

    }
}
