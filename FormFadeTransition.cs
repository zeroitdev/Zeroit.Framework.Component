// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="FormFadeTransition.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Zeroit.Framework.Components
{
    /// <summary>
    /// Class ZeroitFadeForm.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    public class ZeroitFadeForm : Component
	{
        /// <summary>
        /// The form 0
        /// </summary>
        private Form form_0;

        /// <summary>
        /// The double 0
        /// </summary>
        private double double_0;

        /// <summary>
        /// The delay
        /// </summary>
        private int delay = 1;

        /// <summary>
        /// The icontainer 0
        /// </summary>
        private IContainer icontainer_0;

        /// <summary>
        /// The background worker 0
        /// </summary>
        private BackgroundWorker backgroundWorker_0;

        /// <summary>
        /// The event handler 0
        /// </summary>
        EventHandler eventHandler_0;

        /// <summary>
        /// The control
        /// </summary>
        Control control = new Control();

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>The control.</value>
        public Control Control
        {
            get { return control; }
            set
            {
                control = value;                                
            }
        }

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>The delay.</value>
        public int Delay
		{
			get
			{
				return this.delay;
			}
			set
			{
				this.delay = value;
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFadeForm"/> class.
        /// </summary>
        public ZeroitFadeForm()
		{
            
			this.method_0();
            
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFadeForm"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ZeroitFadeForm(IContainer container)
		{
			container.Add(this);
			this.method_0();
		}

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker_0 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void backgroundWorker_0_DoWork(object sender, DoWorkEventArgs e)
		{
			this.double_0 = 0;
			while (this.double_0 < 1)
			{
				this.backgroundWorker_0.ReportProgress(0);
				Thread.Sleep(this.Delay);
				this.double_0 = this.double_0 + 0.001;
			}
		}

        /// <summary>
        /// Handles the ProgressChanged event of the backgroundWorker_0 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void backgroundWorker_0_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.form_0.Opacity = this.double_0;
		}

        /// <summary>
        /// Handles the RunWorkerCompleted event of the backgroundWorker_0 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void backgroundWorker_0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			int num = 0;
			int num1 = 0;
			int num2;
			this.form_0.Opacity = 1;
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
        /// Hides the asyc.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        /// <param name="Close">if set to <c>true</c> [close].</param>
        private void HideAsyc(Form frm, bool Close)
		{
			this.form_0 = frm;
		}

        /// <summary>
        /// Methods the 0.
        /// </summary>
        private void method_0()
		{
			this.backgroundWorker_0 = new BackgroundWorker()
			{
				WorkerReportsProgress = true
			};
			this.backgroundWorker_0.DoWork += new DoWorkEventHandler(this.backgroundWorker_0_DoWork);
			this.backgroundWorker_0.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker_0_ProgressChanged);
			this.backgroundWorker_0.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_0_RunWorkerCompleted);
		}

        /// <summary>
        /// Shows the asyc.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        private void ShowAsyc(Form frm)
		{
			int num = 0;
			int num1 = 0;
			int num2;
			try
			{
				this.form_0 = frm;
				this.backgroundWorker_0.RunWorkerAsync();
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
        /// Starts the transition.
        /// </summary>
        public void StartTransition()
        {
            Form form = control as Form;

            ShowAsyc(form);
        }

        /// <summary>
        /// Occurs when [transition end].
        /// </summary>
        public event EventHandler TransitionEnd
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