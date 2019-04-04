// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="TransfromHelper.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Windows.Forms.VisualStyles;

#endregion

namespace Zeroit.Framework.Components
{
    #region TransfromHelper

    /// <summary>
    /// Implements image transformations
    /// </summary>
    public static class TransfromHelper
    {
        /// <summary>
        /// The bytes per pixel
        /// </summary>
        const int bytesPerPixel = 4;

        /// <summary>
        /// The random
        /// </summary>
        static Random rnd = new Random();

        /// <summary>
        /// Calculates the difference.
        /// </summary>
        /// <param name="bmp1">The BMP1.</param>
        /// <param name="bmp2">The BMP2.</param>
        public static void CalcDifference(Bitmap bmp1, Bitmap bmp2)
        {
            PixelFormat pxf = PixelFormat.Format32bppArgb;
            Rectangle rect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);

            BitmapData bmpData1 = bmp1.LockBits(rect, ImageLockMode.ReadWrite, pxf);
            IntPtr ptr1 = bmpData1.Scan0;

            BitmapData bmpData2 = bmp2.LockBits(rect, ImageLockMode.ReadOnly, pxf);
            IntPtr ptr2 = bmpData2.Scan0;

            int numBytes = bmp1.Width * bmp1.Height * bytesPerPixel;
            byte[] pixels1 = new byte[numBytes];
            byte[] pixels2 = new byte[numBytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr1, pixels1, 0, numBytes);
            System.Runtime.InteropServices.Marshal.Copy(ptr2, pixels2, 0, numBytes);

            for (int i = 0; i < numBytes; i += bytesPerPixel)
            {
                if (pixels1[i + 0] == pixels2[i + 0] &&
                    pixels1[i + 1] == pixels2[i + 1] &&
                    pixels1[i + 2] == pixels2[i + 2])
                {
                    pixels1[i + 0] = 255;
                    pixels1[i + 1] = 255;
                    pixels1[i + 2] = 255;
                    pixels1[i + 3] = 0;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(pixels1, 0, ptr1, numBytes);
            bmp1.UnlockBits(bmpData1);
            bmp2.UnlockBits(bmpData2);
        }

        /// <summary>
        /// Does the bottom mirror.
        /// </summary>
        /// <param name="e">The e.</param>
        public static void DoBottomMirror(NonLinearTransfromNeededEventArg e)
        {
            var source = e.SourcePixels;
            var output = e.Pixels;

            var s = e.Stride;
            var dy = 1;
            var beginY = e.SourceClientRectangle.Bottom + dy;
            var sy = e.ClientRectangle.Height;
            var beginX = e.SourceClientRectangle.Left;
            var endX = e.SourceClientRectangle.Right;
            var d = sy - beginY;

            for (int x = beginX; x < endX; x++)

            for (int y = beginY; y < sy; y++)
            {
                var sourceY = (int)(beginY - 1 - dy - (y - beginY));
                if (sourceY < 0)
                    break;
                var sourceX = x;
                int sourceI = sourceY * s + sourceX * bytesPerPixel;
                int outI = y * s + x * bytesPerPixel;
                output[outI + 0] = source[sourceI + 0];
                output[outI + 1] = source[sourceI + 1];
                output[outI + 2] = source[sourceI + 2];
                output[outI + 3] = (byte)((1 - 1f * (y - beginY) / d) * 90);
            }
        }

        /*
        internal static void DoBottomShadow(NonLinearTransfromNeededEventArg e)
        {
            var source = e.SourcePixels;
            var output = e.Pixels;

            var s = e.Stride;
            var dy = 1;
            var beginY = e.SourceClientRectangle.Bottom + dy;
            var sy = e.ClientRectangle.Height;
            var beginX = e.SourceClientRectangle.Left;
            var endX = e.SourceClientRectangle.Right;
            var d = sy - beginY;

            var bgG = source[0];
            var bgB = source[1];
            var bgR = source[2];

            for (int x = beginX; x < endX; x++)
                for (int y = beginY; y < sy; y++)
                {
                    var sourceY = (int)(beginY - 1 - dy - (y - beginY)*6);
                    if (sourceY < 0)
                        break;
                    var sourceX = x;
                    int sourceI = sourceY * s + sourceX * bytesPerPixel;
                    int outI = y * s + x * bytesPerPixel;
                    if (source[sourceI + 0] != bgG && source[sourceI + 1] != bgB && source[sourceI + 2] != bgR)
                    {
                        output[outI + 0] = 0;
                        output[outI + 1] = 0;
                        output[outI + 2] = 0;
                        output[outI + 3] = (byte) ((1 - 1f*(y - beginY)/d)*90);
                    }
                }
        }*/

        /// <summary>
        /// Does the blur.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="r">The r.</param>
        public static void DoBlur(NonLinearTransfromNeededEventArg e, int r)
        {
            var output = e.Pixels;
            var source = e.SourcePixels;

            var s = e.Stride;
            var sy = e.ClientRectangle.Height;
            var sx = e.ClientRectangle.Width;
            var maxI = source.Length - bytesPerPixel;

            for (int x = r; x < sx - r; x++)

            for (int y = r; y < sy - r; y++)
            {
                int outI = y * s + x * bytesPerPixel;

                int R = 0, G = 0, B = 0, A = 0;
                int counter = 0;

                for (int xx = x - r; xx < x + r; xx++)

                for (int yy = y - r; yy < y + r; yy++)
                {
                    int srcI = yy * s + xx * bytesPerPixel;
                    if (srcI >= 0 && srcI < maxI)
                        if (source[srcI + 3] > 0)
                        {
                            B += source[srcI + 0];
                            G += source[srcI + 1];
                            R += source[srcI + 2];
                            A += source[srcI + 3];
                            counter++;
                        }
                }

                if (outI < maxI && counter > 5)
                {
                    output[outI + 0] = (byte)(B / counter);
                    output[outI + 1] = (byte)(G / counter);
                    output[outI + 2] = (byte)(R / counter);
                    output[outI + 3] = (byte)(A / counter);
                    //output[outI + 3] = 255; //(byte)((1 - 1f * (y - beginY) / d) * 90);
                }

            }

        }

        /// <summary>
        /// Does the flip.
        /// </summary>
        /// <param name="e">The e.</param>
        public static void DoFlip(TransfromNeededEventArg e)
        {
            var cy = e.ClientRectangle.Height / 5;

            var sy = 1 - 2 * e.CurrentTime;
            if (sy < 0.01f && sy > -0.01f)
                sy = 0.01f;

            e.Matrix.Translate(0, cy);
            e.Matrix.Scale(1, sy);
            e.Matrix.Translate(0, -cy);
        }

    }
    
    #endregion
}
