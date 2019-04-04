// ***********************************************************************
// Assembly         : Zeroit.Framework.Components
// Author           : ZEROIT
// Created          : 11-01-2017
//
// Last Modified By : ZEROIT
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="DaggerBackgroundSleeper.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Zeroit.Framework.Components
{
    /// <summary>
    /// A class collection for rendering a background sleeper.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    public class ZeroitBackgroundSleeper : Component
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBackgroundSleeper" /> class.
        /// </summary>
        public ZeroitBackgroundSleeper()
		{

		}

        /// <summary>
        /// Sleeps for a specified time in milliseconds.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        public void Sleep(int milliseconds)
		{
			DateTime dateTime = DateTime.Now.AddMilliseconds((double)milliseconds);
			while (DateTime.Now < dateTime)
			{
				Application.DoEvents();
			}
		}

	}
}