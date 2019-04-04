// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="MakeEllipseForm.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Zeroit.Framework.Components.Helpers;

namespace Zeroit.Framework.Components
{
    /// <summary>
    /// Class ZeroitMakeEllipseForm.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    public class ZeroitMakeEllipseForm : Component
    {
        /// <summary>
        /// The container control 0
        /// </summary>
        private System.Windows.Forms.ContainerControl containerControl_0;

        /// <summary>
        /// The control 0
        /// </summary>
        private Control control_0;

        /// <summary>
        /// The int 0
        /// </summary>
        private int int_0 = 5;

        /// <summary>
        /// The icontainer 0
        /// </summary>
        private IContainer icontainer_0;

        /// <summary>
        /// The timer 0
        /// </summary>
        private System.Windows.Forms.Timer timer_0;

        /// <summary>
        /// The event handler 0
        /// </summary>
        EventHandler eventHandler_0;

        /// <summary>
        /// Gets or sets the container control.
        /// </summary>
        /// <value>The container control.</value>
        private System.Windows.Forms.ContainerControl ContainerControl
        {
            get
            {
                return this.containerControl_0;
            }
            set
            {
                this.containerControl_0 = value;
                this.ApplyElipse();
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public int Radius
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
                this.ApplyElipse();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.
        /// </summary>
        /// <value>The site.</value>
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                int num = 0;
                int num1 = 0;
                int num2;
                base.Site = value;
                if (value == null)
                {
                    return;
                }
                IDesignerHost service = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (service != null)
                {
                    IComponent rootComponent = service.RootComponent;
                    if (!(rootComponent is System.Windows.Forms.ContainerControl))
                    {
                        do
                        {
                            if (num != num1)
                            {
                                break;
                            }
                            num1 = 1;
                            num2 = num;
                            num = 1;
                        }
                        while (1 <= num2);
                        return;
                    }
                    this.ContainerControl = rootComponent as System.Windows.Forms.ContainerControl;
                    return;
                }
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
        }

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>The control.</value>
        public Control Control
        {
            get
            {
                return this.control_0;
            }
            set
            {
                this.control_0 = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMakeEllipseForm"/> class.
        /// </summary>
        public ZeroitMakeEllipseForm()
        {
            int num = 0;
            int num1 = 0;
            int num2;
            this.method_0();
            if (this.Control != null)
            {
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
            else
            {
                this.Control = this.ContainerControl;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMakeEllipseForm"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ZeroitMakeEllipseForm(IContainer container)
        {
            container.Add(this);
            this.method_0();
        }

        /// <summary>
        /// Applies the elipse.
        /// </summary>
        /// <param name="radius">The radius.</param>
        public void ApplyElipse(int radius)
        {
            int num = 0;
            int num1 = 0;
            int num2;
            if (this.control_0 == null)
            {
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
            else
            {
                Ellipse.Apply(this.control_0, radius);
            }
        }

        /// <summary>
        /// Applies the elipse.
        /// </summary>
        public void ApplyElipse()
        {
            int num = 0;
            int num1 = 0;
            int num2;
            try
            {
                if (this.control_0 != null)
                {
                    Ellipse.Apply(this.control_0, this.int_0);
                }
            }
            catch (Exception exception)
            {
            }
            do
            {
                if (num != num1)
                {
                    break;
                }
                num1 = 1;
                num2 = num;
                num = 1;
            }
            while (1 <= num2);
        }

        /// <summary>
        /// Applies the elipse.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="radius">The radius.</param>
        public void ApplyElipse(Control control, int radius)
        {
            int num = 0;
            int num1 = 0;
            int num2;
            if (control == null)
            {
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
            else
            {
                Ellipse.Apply(control, radius);
            }
        }

        /// <summary>
        /// Applies the elipse.
        /// </summary>
        /// <param name="control">The control.</param>
        public void ApplyElipse(Control control)
        {
            int num = 0;
            int num1 = 0;
            int num2;
            if (control == null)
            {
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
            else
            {
                Ellipse.Apply(control, this.int_0);
            }
        }

        /// <summary>
        /// Handles the Resize event of the control_0 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void control_0_Resize(object sender, EventArgs e)
        {
            int num = 0;
            int num1 = 0;
            int num2;
            Ellipse.Apply(this.control_0, this.int_0);
            if (this.eventHandler_0 == null)
            {
                do
                {
                    if (num != num1)
                    {
                        break;
                    }
                    num1 = 1;
                    num2 = num;
                    num = 1;
                }
                while (1 <= num2);
            }
            else
            {
                this.eventHandler_0(sender, e);
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.icontainer_0 != null)
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Methods the 0.
        /// </summary>
        private void method_0()
        {
            this.icontainer_0 = new System.ComponentModel.Container();
            this.timer_0 = new System.Windows.Forms.Timer(this.icontainer_0)
            {
                Enabled = true
            };
            this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
        }

        /// <summary>
        /// Handles the Tick event of the timer_0 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer_0_Tick(object sender, EventArgs e)
        {
            int num = 0;
            int num1 = 0;
            int num2;
            try
            {
                this.timer_0.Stop();
                if (this.control_0 == null)
                {
                    this.control_0 = this.ContainerControl;
                    this.control_0.Resize += new EventHandler(this.control_0_Resize);
                }
                else
                {
                    this.control_0.Resize += new EventHandler(this.control_0_Resize);
                }
                if (this.control_0.GetType() == typeof(Form))
                {
                    ((Form)this.control_0).FormBorderStyle = FormBorderStyle.None;
                }
                this.ApplyElipse();
            }
            catch (Exception exception)
            {
                this.timer_0.Start();
            }
            do
            {
                if (num != num1)
                {
                    break;
                }
                num1 = 1;
                num2 = num;
                num = 1;
            }
            while (1 <= num2);
        }

        /// <summary>
        /// Occurs when [target control resized].
        /// </summary>
        public event EventHandler TargetControlResized
        {
            add
            {
                EventHandler eventHandler;
                EventHandler eventHandler0 = this.eventHandler_0;
                do
                {
                    eventHandler = eventHandler0;
                    EventHandler eventHandler1 = (EventHandler)Delegate.Combine(eventHandler, value);
                    eventHandler0 = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_0, eventHandler1, eventHandler);
                }
                while ((object)eventHandler0 != (object)eventHandler);
            }
            remove
            {
                EventHandler eventHandler;
                EventHandler eventHandler0 = this.eventHandler_0;
                do
                {
                    eventHandler = eventHandler0;
                    EventHandler eventHandler1 = (EventHandler)Delegate.Remove(eventHandler, value);
                    eventHandler0 = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_0, eventHandler1, eventHandler);
                }
                while ((object)eventHandler0 != (object)eventHandler);
            }
        }
    }

}
