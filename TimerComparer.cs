// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 01-27-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="TimerComparer.cs" company="Zeroit Dev Technologies">
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
