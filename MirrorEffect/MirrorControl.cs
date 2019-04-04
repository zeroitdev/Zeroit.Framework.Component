// ***********************************************************************
// Assembly         : Zeroit.Framework.Component
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="MirrorControl.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Zeroit.Framework.Components
{
    /// <summary>
    /// Class ZeroitMirrorEffect.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    /// <seealso cref="System.ComponentModel.IExtenderProvider" />
    [ProvideProperty("Mirror", typeof(Control))]
    public partial class ZeroitMirrorEffect : Component, IExtenderProvider
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMirrorEffect"/> class.
        /// </summary>
        public ZeroitMirrorEffect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMirrorEffect"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ZeroitMirrorEffect(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        #region IExtenderProvider

        /// <summary>
        /// Gets the mirror.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>DecorationType.</returns>
        public DecorationType GetMirror(Control control)
        {
            if (DecorationByControls.ContainsKey(control))
                return DecorationByControls[control].DecorationType;
            else
                return DecorationType.None;
        }

        /// <summary>
        /// Sets the mirror.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="decoration">The decoration.</param>
        public void SetMirror(Control control, DecorationType decoration)
        {
            var wrapper = DecorationByControls.ContainsKey(control) ? DecorationByControls[control] : null;
            if (decoration == DecorationType.None)
            {
                if (wrapper != null)
                    wrapper.Dispose();
                DecorationByControls.Remove(control);
            }
            else
            {
                if (wrapper == null)
                    wrapper = new DecorationControl(decoration, control);
                wrapper.DecorationType = decoration;
                DecorationByControls[control] = wrapper;
            }
        }

        /// <summary>
        /// The decoration by controls
        /// </summary>
        private readonly Dictionary<Control, DecorationControl> DecorationByControls = new Dictionary<Control, DecorationControl>();

        /// <summary>
        /// Specifies whether this object can provide its extender properties to the specified object.
        /// </summary>
        /// <param name="extendee">The <see cref="T:System.Object" /> to receive the extender properties.</param>
        /// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
        public bool CanExtend(object extendee)
        {
            return extendee is Control;
        }

        #endregion
    }
}
