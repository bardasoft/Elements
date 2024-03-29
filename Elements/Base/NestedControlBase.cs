﻿using Elements.Models;
using Elements.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Elements.Base
{
    /// <summary>
    /// The <see cref="NestedControlBase"/> class.
    /// </summary>
    /// <seealso cref="Elements.Base.ContainedControlBase"/>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [DesignerCategory("code")]
    [ToolboxItem(false)]
    public abstract class NestedControlBase : ContainedControlBase
    {
        private ColorState _colorState;

        /// <summary>
        /// Initializes a new instance of the <see cref="NestedControlBase"/> class.
        /// </summary>
        protected NestedControlBase()
        {
            _colorState = new ColorState();
        }

        /// <summary>
        /// Gets or sets the state of the back color.
        /// </summary>
        /// <value>The state of the back color.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorState BackColorState
        {
            get
            {
                return _colorState;
            }

            set
            {
                _colorState = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:BackColorChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            GraphicsUtilities.ApplyContainerBackColorChange(this, BackColorState.Enabled);
            base.OnBackColorChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ControlAdded"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ControlEventArgs"/> instance containing the event data.</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            GraphicsUtilities.SetControlBackColor(e.Control, BackColorState.Enabled, false);
            base.OnControlAdded(e);
        }

        /// <summary>
        /// Raises the <see cref="E:ControlRemoved"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ControlEventArgs"/> instance containing the event data.</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            GraphicsUtilities.SetControlBackColor(e.Control, Parent.BackColor, true);
            base.OnControlRemoved(e);
        }
    }
}