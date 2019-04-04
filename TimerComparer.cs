// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 01-27-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="TimerComparer.cs" company="Zeroit Dev Technologies">
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
    /// Class TimeComparer.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Timer" />
    public class TimeComparer : Timer
    {

        #region Private Fields
        private int startValue = 0; //Seconds
        private int endValue = 60; //Seconds
        private float minutes = 1f; 
        #endregion

        #region Public Properties

        [Browsable(false)]
        public new int Interval
        {
            get { return base.Interval; }
            set
            {
                base.Interval = value;
            }
        }

        public int Value
        {
            get { return startValue; }
            set
            {
                startValue = value;

            }
        }


        public float Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;

            }
        } 
        #endregion


        protected virtual void OnTimeElapsed(EventArgs e)
        {
            OnTick(e);
        }

        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);

            if (Value + 1 > Convert.ToInt32(Minutes * endValue))
            {
                this.Stop();
                this.Enabled = false;

            }
            else
            {
                Value += 1;

            }
        }

    }
}
