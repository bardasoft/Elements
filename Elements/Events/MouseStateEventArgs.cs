﻿using Elements.Enumerators;
using System;
using System.ComponentModel;

namespace Elements.Events
{
    /// <summary>
    /// The <see cref="MouseStateEventArgs"/> class.
    /// </summary>
    /// <seealso cref="System.EventArgs"/>
    public class MouseStateEventArgs : EventArgs
    {
        private MouseStates _mouseStates;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseStateEventArgs"/> class.
        /// </summary>
        public MouseStateEventArgs()
        {
            _mouseStates = MouseStates.Normal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseStateEventArgs"/> class.
        /// </summary>
        /// <param name="mouseStates">The mouse states.</param>
        public MouseStateEventArgs(MouseStates mouseStates) : this()
        {
            _mouseStates = mouseStates;
        }

        /// <summary>
        /// Gets or sets the mouse states.
        /// </summary>
        /// <value>The mouse states.</value>
        [Description("The mouse state.")]
        public MouseStates MouseStates
        {
            get
            {
                return _mouseStates;
            }

            set
            {
                _mouseStates = value;
            }
        }
    }
}