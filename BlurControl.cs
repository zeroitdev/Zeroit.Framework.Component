// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 12-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="BlurControl.cs" company="Zeroit Dev Technologies">
//    This program is program that contains helping components.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Zeroit.Framework.Components
{
    /// <summary>
    /// A class for rendering a gaussian blur on a control.
    /// </summary>
    public partial class ZeroitBlurControl 
    {

        #region Private Fields

        /// <summary>
        /// This is the blur strength. 
        /// Setting it to a negative value gives a different look. 
        /// Play with the values
        /// </summary>
        private int blurStrength = 0;

        /// <summary>
        /// This is the control to be blurred
        /// </summary>
        private Control control = new Control();

        /// <summary>
        /// This makes the control inactive
        /// </summary>
        private bool makeInactive = false;

        /// <summary>
        /// Set to blur and unblur the control
        /// </summary>
        private bool blurred = true;

        /// <summary>
        /// The magic picturebox to render the blur
        /// </summary>
        System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Target"/> is blurred.
        /// </summary>
        /// <value><c>true</c> if blurred; otherwise, <c>false</c>.</value>
        public bool Blurred
        {
            get { return blurred; }
            set
            {
                if (value)
                {
                    RenderBlur();
                }
                else
                {
                    UnBlur();

                }

                blurred = value;
                control.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the blur strength.
        /// </summary>
        /// <value>The blur strength.</value>
        public int BlurStrength
        {
            get { return blurStrength; }
            set
            {
                blurStrength = value;
                control.Invalidate();
                RenderBlur();
            }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public Control Target
        {
            get
            {
                return control;
            }
            set
            {
                control = value;
                control.Invalidate();
                RenderBlur();
                RenderBlur();
                RenderBlur();
                RenderBlur();
                RenderBlur();
                RenderBlur();
                RenderBlur();
                RenderBlur();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is active.
        /// </summary>
        /// <value><c>true</c> if [make target inactive]; otherwise, <c>false</c>.</value>
        public bool MakeTargetInactive
        {
            get { return makeInactive; }
            set
            {
                makeInactive = value;
                control.Invalidate();
                RenderBlur();
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBlurControl"/> class.
        /// </summary>
        public ZeroitBlurControl()
        {
            InitializeComponent();
            control.Resize += Control_Resize;
            pb.MouseDown += DragForm_MouseDown;
            pb.MouseMove += DragForm_MouseMove;

        }

        /// <summary>
        /// Handles the Resize event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Control_Resize(object sender, EventArgs e)
        {
            pb.Size = control.Size;
            pb.Invalidate();
            RenderBlur();
        }


        #endregion

        #region Blur Methods

        /// <summary>
        /// Renders the blur.
        /// </summary>
        public void RenderBlur()
        {
            Bitmap bitmap = TakeSnapshot();
            GaussianBlur(bitmap, BlurStrength);
            
            control.Controls.Add(pb);
            pb.Image = bitmap;
            pb.Dock = DockStyle.Fill;
            pb.BringToFront();

            control.Enabled = MakeTargetInactive;
            GC.Collect();
        }

        /// <summary>
        /// Uns the blur.
        /// </summary>
        public void UnBlur()
        {
            pb.Image = null;
            pb.Dock = DockStyle.None;
            pb.SendToBack();
           
            control.Enabled = true;
        }

        /// <summary>
        /// Conv3x3s the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="m">The m.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            IntPtr Scan0 = bmData.Scan0;
            IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride + 6 - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }

        /// <summary>
        /// Gaussians the blur.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="nWeight">The n weight.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool GaussianBlur(Bitmap b, int nWeight /* default to 4*/)
        {
            ConvMatrix m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;

            return Conv3x3(b, m);
        }

        /// <summary>
        /// Takes the snapshot.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap TakeSnapshot()
        {

            Bitmap bmp = new Bitmap(Target.Width, Target.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(
                    control.PointToScreen(control.ClientRectangle.Location),
                    new Point(0, 0), control.ClientRectangle.Size
                );
            }
            return bmp;

        }

        #endregion

        #region Move Control

        /// <summary>
        /// The last click
        /// </summary>
        private Point lastClick;
        /// <summary>
        /// Handles the MouseDown event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public void DragForm_MouseDown(object sender, MouseEventArgs e)
        {

            lastClick = new Point(e.X, e.Y); //We'll need this for when the Form starts to move
        }

        /// <summary>
        /// Handles the MouseMove event of the DragForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public void DragForm_MouseMove(object sender, MouseEventArgs e)
        {

            //Point newLocation = new Point(e.X - lastE.X, e.Y - lastE.Y);
            if (e.Button == MouseButtons.Left) //Only when mouse is clicked
            {
                //Move the Form the same difference the mouse cursor moved;
                if (control != null)
                {
                    control.FindForm().Left += e.X - lastClick.X;
                    control.FindForm().Top += e.Y - lastClick.Y;
                }
            }
        }


        #endregion
        
    }

    #region Blur Classes

    /// <summary>
    /// Class ConvMatrix.
    /// </summary>
    public class ConvMatrix
    {
        /// <summary>
        /// The top left
        /// </summary>
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        /// <summary>
        /// The mid left
        /// </summary>
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        /// <summary>
        /// The bottom left
        /// </summary>
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        /// <summary>
        /// The factor
        /// </summary>
        public int Factor = 1;
        /// <summary>
        /// The offset
        /// </summary>
        public int Offset = 0;
        /// <summary>
        /// Sets all.
        /// </summary>
        /// <param name="nVal">The n value.</param>
        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }
    }
    
    #endregion

}
