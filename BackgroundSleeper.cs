// ***********************************************************************
// Assembly         : Zeroit.Framework.Components
// Author           : ZEROIT
// Created          : 11-01-2017
//
// Last Modified By : ZEROIT
// Last Modified On : 05-18-2018
// ***********************************************************************
// <copyright file="DaggerBackgroundSleeper.cs" company="Zeroit Dev Technologies">
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