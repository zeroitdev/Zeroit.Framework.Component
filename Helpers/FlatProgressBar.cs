// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 11-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-23-2018
// ***********************************************************************
// <copyright file="FlatProgressBar.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.Components.Helpers
{
    /// <summary>
    /// Class ZeroitFlatProgressBar.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ToolboxItem(false)]
	public class ZeroitFlatProgressBar : Control
	{
        /// <summary>
        /// The buffered graphics
        /// </summary>
        private BufferedGraphics bufferedGraphics;

        /// <summary>
        /// The bar style
        /// </summary>
        private ZeroitFlatProgressBar.Style barStyle = ZeroitFlatProgressBar.Style.Material;

        /// <summary>
        /// The value
        /// </summary>
        private int @value = 50;

        /// <summary>
        /// The complete color
        /// </summary>
        private Color completeColor = Color.FromArgb(1, 119, 215);

        /// <summary>
        /// The incompleted color
        /// </summary>
        private Color incompletedColor = Color.White;

        /// <summary>
        /// The bar thickness
        /// </summary>
        private int barThickness = 5;

        /// <summary>
        /// The maximum value
        /// </summary>
        private int maxValue = 100;

        /// <summary>
        /// Gets or sets the bar style.
        /// </summary>
        /// <value>The bar style.</value>
        [Browsable(true)]
		[Category("Zeroit.Framework.DaggerControls")]
		[Description("The progress bar style")]
		public ZeroitFlatProgressBar.Style BarStyle
		{
			get
			{
				return this.barStyle;
			}
			set
			{
				this.barStyle = value;
				base.Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the bar thickness.
        /// </summary>
        /// <value>The bar thickness.</value>
        [Browsable(true)]
		[Category("Zeroit.Framework.DaggerControls")]
		[Description("The bar thickness")]
		public int BarThickness
		{
			get
			{
				return this.barThickness;
			}
			set
			{
				this.barThickness = value;
				base.Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the color of the complete.
        /// </summary>
        /// <value>The color of the complete.</value>
        [Browsable(true)]
		[Category("Zeroit.Framework.DaggerControls")]
		[Description("The progress complete color")]
		public Color CompleteColor
		{
			get
			{
				return this.completeColor;
			}
			set
			{
				this.completeColor = value;
				base.Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the color of the inocmpleted.
        /// </summary>
        /// <value>The color of the inocmpleted.</value>
        [Browsable(true)]
		[Category("Zeroit.Framework.DaggerControls")]
		[Description("The progress incompleted color")]
		public Color InocmpletedColor
		{
			get
			{
				return this.incompletedColor;
			}
			set
			{
				this.incompletedColor = value;
				base.Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [Browsable(true)]
		[Category("Zeroit.Framework.DaggerControls")]
		[Description("The maximum value")]
		public int MaxValue
		{
			get
			{
				return this.maxValue;
			}
			set
			{
				this.maxValue = value;
				if (this.Value > this.maxValue)
				{
					this.Value = this.maxValue;
				}
				base.Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Browsable(true)]
		[Category("Zeroit.Framework.DaggerControls")]
		[Description("The progress value")]
		public int Value
		{
			get
			{
				return this.@value;
			}
			set
			{
				this.@value = value;
				if (this.@value < 0)
				{
					this.@value = 0;
				}
				if (this.@value > this.maxValue)
				{
					this.@value = this.maxValue;
				}
				base.Invalidate();
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFlatProgressBar"/> class.
        /// </summary>
        public ZeroitFlatProgressBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			base.Size = new System.Drawing.Size(300, 5);
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
		{
			BufferedGraphicsContext current = BufferedGraphicsManager.Current;
			current.MaximumBuffer = new System.Drawing.Size(base.Width + 1, base.Height + 1);
			this.bufferedGraphics = current.Allocate(base.CreateGraphics(), base.ClientRectangle);
			this.bufferedGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			this.bufferedGraphics.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
			this.bufferedGraphics.Graphics.CompositingQuality = CompositingQuality.HighQuality;
			this.bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			this.bufferedGraphics.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			this.bufferedGraphics.Graphics.Clear(this.BackColor);
			if (this.barStyle == ZeroitFlatProgressBar.Style.Flat)
			{
				this.bufferedGraphics.Graphics.FillRectangle(new SolidBrush(this.incompletedColor), 0, 0, base.Width, base.Height);
				this.bufferedGraphics.Graphics.FillRectangle(new SolidBrush(this.completeColor), 0, 0, this.@value * base.Width / this.maxValue, base.Height);
			}
			if (this.barStyle == ZeroitFlatProgressBar.Style.IOS)
			{
				this.bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 180, 180)), 0, 0, base.Width, base.Height);
				this.bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 120, 250)), 0, 0, this.@value * base.Width / this.maxValue, base.Height);
			}
			if (this.barStyle == ZeroitFlatProgressBar.Style.Material)
			{
				LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height), Color.Black, Color.Black, 0f, false);
				ColorBlend colorBlend = new ColorBlend()
				{
					Positions = new float[] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1f },
					Colors = new Color[] { Color.FromArgb(76, 217, 100), Color.FromArgb(85, 205, 205), Color.FromArgb(2, 124, 255), Color.FromArgb(130, 75, 180), Color.FromArgb(255, 0, 150), Color.FromArgb(255, 45, 85) }
				};
				linearGradientBrush.InterpolationColors = colorBlend;
				linearGradientBrush.RotateTransform(1f);
				this.bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, 0, base.Width, base.Height));
				this.bufferedGraphics.Graphics.FillRectangle(new SolidBrush(this.incompletedColor), this.@value * base.Width / this.maxValue, 0, base.Width - this.@value * base.Width / this.maxValue, base.Height);
			}
			this.bufferedGraphics.Render(e.Graphics);
			base.OnPaint(e);
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
		{
			base.Invalidate();
		}

        /// <summary>
        /// Enum Style
        /// </summary>
        public enum Style
		{
            /// <summary>
            /// The flat
            /// </summary>
            Flat,
            /// <summary>
            /// The material
            /// </summary>
            Material,
            /// <summary>
            /// The ios
            /// </summary>
            IOS
        }
	}
}