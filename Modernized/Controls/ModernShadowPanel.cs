// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 11-24-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ModernShadowPanel.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Zeroit.Framework.Components.Modernized.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.Components.Modernized.Controls
{
    /// <summary>
    /// Class ZeroitShadowMaker.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Panel" />
    public class ZeroitShadowMaker : Panel
    {

        /// <summary>
        /// The shadow color
        /// </summary>
        private Color shadowColor = Color.Black;
        /// <summary>
        /// The shadow opacity
        /// </summary>
        private int shadowOpacity = 150;

        /// <summary>
        /// Enum ShapeType
        /// </summary>
        public enum ShapeType
        {
            /// <summary>
            /// The rectangle
            /// </summary>
            Rectangle,
            /// <summary>
            /// The circle
            /// </summary>
            Circle
        }

        /// <summary>
        /// Gets or sets the type of the shadow.
        /// </summary>
        /// <value>The type of the shadow.</value>
        public ShapeType ShadowType
        {
            get;
            set;
        } = ShapeType.Rectangle;

        /// <summary>
        /// Gets or sets the shadow opacity.
        /// </summary>
        /// <value>The shadow opacity.</value>
        public int ShadowOpacity
        {
            get { return shadowOpacity; }
            set
            {
                shadowOpacity = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the shadow.
        /// </summary>
        /// <value>The color of the shadow.</value>
        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                shadowColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the frozen image.
        /// </summary>
        /// <value>The frozen image.</value>
        private Bitmap FrozenImage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitShadowMaker"/> class.
        /// </summary>
        public ZeroitShadowMaker()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            DoubleBuffered = true;
        }

        /// <summary>
        /// Freezes this instance.
        /// </summary>
        public void Freeze()
        {
            FrozenImage = new Bitmap(Size.Width, Size.Height);
            using (var g = Graphics.FromImage(FrozenImage)) {
                DrawControlShadow(g);
            }
            Refresh();
        }

        /// <summary>
        /// Unfreezes this instance.
        /// </summary>
        public void Unfreeze()
        {
            FrozenImage = null;
            Refresh();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (FrozenImage != null)
                Freeze();
        }

        /// <summary>
        /// The text
        /// </summary>
        string text = string.Format("Drop a control{0}on the Shadow Box", Environment.NewLine);
        /// <summary>
        /// The below text
        /// </summary>
        private string belowText = "Zeroit Dev © 2018";
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (FrozenImage != null)
                Freeze();

            text = String.Empty;
            belowText = String.Empty;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (FrozenImage != null)
                Freeze();

            if(this.Controls.Count > 0)
            {
                text = String.Empty;
                belowText = String.Empty;
            }
            else
            {
                //text = "Add controls from the \ntoolbox to activate shadow";

                text = string.Format("Drop a control{0}on the Shadow Box", Environment.NewLine);
                belowText = "Zeroit Dev © 2018";
            }

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (FrozenImage != null) {
                e.Graphics.DrawImage(FrozenImage, Point.Empty);
                return;
            }

            //DrawControlShadow(e.Graphics);

            DrawControlShadow(e.Graphics, ShadowType);


            if (DesignMode)
            {
                //e.Graphics.DrawString(text, Font, new SolidBrush(Color.FromArgb(0, 122, 204)), new PointF(5, this.Height - 30));

                TextRenderer.DrawText(e.Graphics, text, new Font("Arial", 8F, FontStyle.Bold), new Point(20, 20), Color.DarkBlue);
                TextRenderer.DrawText(e.Graphics, belowText, new Font("Arial", 7F, FontStyle.Bold), new Point(Width - 95, Height - 17), Color.DarkGray);

            }
            
        }

        /// <summary>
        /// The shadow offset
        /// </summary>
        private const int SHADOW_OFFSET = 3;
        /// <summary>
        /// The half shadow offset
        /// </summary>
        private const int HALF_SHADOW_OFFSET = SHADOW_OFFSET / 2;
        /// <summary>
        /// The half half shadow offset
        /// </summary>
        private const int HALF_HALF_SHADOW_OFFSET = HALF_SHADOW_OFFSET / 2;
        /// <summary>
        /// Draws the control shadow.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawControlShadow(Graphics g)
        {
            using (var brush = new SolidBrush(Color.FromArgb(ShadowOpacity, ShadowColor))) {
                using (var img = new Bitmap(Width, Height)) {
                    using (var gp = Graphics.FromImage(img)) {
                        foreach (Control c in Controls) {
                            //gp.DrawRoundedRectangle(rInner, 5, Pens.Transparent, Color.Black);
                            gp.FillRectangle(brush, Rectangle.Inflate(c.Bounds, HALF_SHADOW_OFFSET, HALF_SHADOW_OFFSET));
                            //ShadowUtils.DrawOutsetShadow(gp, Color.Black, 0, 0, 20, 1, c);
                        }
                    }
                    var gaussian = new GaussianBlur(img);
                    using (var result = gaussian.Process(SHADOW_OFFSET)) {
                        g.DrawImageUnscaled(result, Point.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the control shadow.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="shapeType">Type of the shape.</param>
        private void DrawControlShadow(Graphics g, ShapeType shapeType)
        {
            using (var brush = new SolidBrush(Color.FromArgb(ShadowOpacity, ShadowColor)))
            {
                using (var img = new Bitmap(Width, Height))
                {
                    using (var gp = Graphics.FromImage(img))
                    {
                        foreach (Control c in Controls)
                        {
                            switch (shapeType)
                            {
                                case ShapeType.Rectangle:
                                    //gp.DrawRoundedRectangle(rInner, 5, Pens.Transparent, Color.Black);
                                    gp.FillRectangle(brush, Rectangle.Inflate(c.Bounds, HALF_SHADOW_OFFSET, HALF_SHADOW_OFFSET));
                                    //ShadowUtils.DrawOutsetShadow(gp, Color.Black, 0, 0, 20, 1, c);

                                    break;
                                
                                case ShapeType.Circle:
                                    //gp.DrawRoundedRectangle(rInner, 5, Pens.Transparent, Color.Black);
                                    gp.FillEllipse(brush, Rectangle.Inflate(c.Bounds, HALF_SHADOW_OFFSET, HALF_SHADOW_OFFSET));
                                    //ShadowUtils.DrawOutsetShadow(gp, Color.Black, 0, 0, 20, 1, c);

                                    break;
                                default:
                                    break;
                            }
                            
                        }
                    }
                    var gaussian = new GaussianBlur(img);
                    using (var result = gaussian.Process(SHADOW_OFFSET))
                    {
                        g.DrawImageUnscaled(result, Point.Empty);
                    }
                }
            }
        }



        


    }

    
}