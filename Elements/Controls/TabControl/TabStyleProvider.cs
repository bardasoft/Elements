﻿using Elements.Controls.TabControl.TabStyleProviders;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Elements.Controls.TabControl
{
    /// <summary>
    /// The <see cref="TabStyleProvider"/> class.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component"/>
    [ToolboxItem(false)]
    public abstract class TabStyleProvider : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabStyleProvider"/> class.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        protected TabStyleProvider(TabControl tabControl)
        {
            _TabControl = tabControl;

            _BorderColor = Color.Empty;
            _BorderColorSelected = Color.Empty;
            _FocusColor = Color.Orange;

            if (_TabControl.RightToLeftLayout)
            {
                _ImageAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                _ImageAlign = ContentAlignment.MiddleLeft;
            }

            HotTrack = true;

            // Must set after the _Overlap as this is used in the calculations of the actual padding
            Padding = new Point(6, 3);
        }

        /// <summary>
        /// Creates the provider.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        /// <returns></returns>
        public static TabStyleProvider CreateProvider(TabControl tabControl)
        {
            TabStyleProvider provider;

            // Depending on the display style of the tabControl generate an appropriate provider.
            switch (tabControl.DisplayStyle)
            {
                case TabStyle.None:
                    provider = new TabStyleNoneProvider(tabControl);
                    break;

                case TabStyle.Default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;

                case TabStyle.Angled:
                    provider = new TabStyleAngledProvider(tabControl);
                    break;

                case TabStyle.Rounded:
                    provider = new TabStyleRoundedProvider(tabControl);
                    break;

                case TabStyle.VisualStudio:
                    provider = new TabStyleVisualStudioProvider(tabControl);
                    break;

                case TabStyle.Chrome:
                    provider = new TabStyleChromeProvider(tabControl);
                    break;

                case TabStyle.IE8:
                    provider = new TabStyleIE8Provider(tabControl);
                    break;

                case TabStyle.VS2010:
                    provider = new TabStyleVS2010Provider(tabControl);
                    break;

                default:
                    provider = new TabStyleDefaultProvider(tabControl);
                    break;
            }

            provider._Style = tabControl.DisplayStyle;
            return provider;
        }

        /// <summary>
        /// Gets the tab border.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public GraphicsPath GetTabBorder(int index)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle tabBounds = GetTabRect(index);

            AddTabBorder(path, tabBounds);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// The tab control
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected TabControl _TabControl;

        /// <summary>
        /// The padding
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected Point _Padding;

        /// <summary>
        /// The hot track
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected bool _HotTrack;

        /// <summary>
        /// The style
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected TabStyle _Style = TabStyle.Default;

        /// <summary>
        /// The image align
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected ContentAlignment _ImageAlign;

        /// <summary>
        /// The radius
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected int _Radius = 1;

        /// <summary>
        /// The overlap
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected int _Overlap;

        /// <summary>
        /// The focus track
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected bool _FocusTrack;

        /// <summary>
        /// The opacity
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected float _Opacity = 1;

        /// <summary>
        /// The show tab closer
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")] protected bool _ShowTabCloser;

        /// <summary>
        /// The border color selected
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _BorderColorSelected = Color.Empty;

        /// <summary>
        /// The border color
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _BorderColor = Color.Empty;

        /// <summary>
        /// The border color hot
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _BorderColorHot = Color.Empty;

        /// <summary>
        /// The closer color active
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _CloserColorActive = Color.Black;

        /// <summary>
        /// The closer color
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _CloserColor = Color.DarkGray;

        /// <summary>
        /// The focus color
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _FocusColor = Color.Empty;

        /// <summary>
        /// The text color
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _TextColor = Color.Empty;

        /// <summary>
        /// The text color selected
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _TextColorSelected = Color.Empty;

        /// <summary>
        /// The text color disabled
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected Color _TextColorDisabled = Color.Empty;

        /// <summary>
        /// Adds the tab border.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="tabBounds">The tab bounds.</param>
        public abstract void AddTabBorder(GraphicsPath path, Rectangle tabBounds);

        /// <summary>
        /// Gets the tab rect.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public virtual Rectangle GetTabRect(int index)
        {
            if (index < 0)
            {
                return new Rectangle();
            }

            Rectangle tabBounds = _TabControl.GetTabRect(index);
            if (_TabControl.RightToLeftLayout)
            {
                tabBounds.X = _TabControl.Width - tabBounds.Right;
            }

            bool firstTabinRow = _TabControl.IsFirstTabInRow(index);

            // Expand to overlap the tabpage
            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    tabBounds.Height += 2;
                    break;

                case TabAlignment.Bottom:
                    tabBounds.Height += 2;
                    tabBounds.Y -= 2;
                    break;

                case TabAlignment.Left:
                    tabBounds.Width += 2;
                    break;

                case TabAlignment.Right:
                    tabBounds.X -= 2;
                    tabBounds.Width += 2;
                    break;
            }

            // Greate Overlap unless first tab in the row to align with tabpage
            if ((!firstTabinRow || _TabControl.RightToLeftLayout) && _Overlap > 0)
            {
                if (_TabControl.Alignment <= TabAlignment.Bottom)
                {
                    tabBounds.X -= _Overlap;
                    tabBounds.Width += _Overlap;
                }
                else
                {
                    tabBounds.Y -= _Overlap;
                    tabBounds.Height += _Overlap;
                }
            }

            // Adjust first tab in the row to align with tabpage
            EnsureFirstTabIsInView(ref tabBounds, index);

            return tabBounds;
        }

        /// <summary>
        /// Ensures the first tab is in view.
        /// </summary>
        /// <param name="tabBounds">The tab bounds.</param>
        /// <param name="index">The index.</param>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        protected virtual void EnsureFirstTabIsInView(ref Rectangle tabBounds, int index)
        {
            // Adjust first tab in the row to align with tabpage Make sure we only reposition
            // visible tabs, as we may have scrolled out of view.

            bool firstTabinRow = _TabControl.IsFirstTabInRow(index);

            if (firstTabinRow)
            {
                if (_TabControl.Alignment <= TabAlignment.Bottom)
                {
                    if (_TabControl.RightToLeftLayout)
                    {
                        if (tabBounds.Left < _TabControl.Right)
                        {
                            int tabPageRight = _TabControl.GetPageBounds(index).Right;
                            if (tabBounds.Right > tabPageRight)
                            {
                                tabBounds.Width -= tabBounds.Right - tabPageRight;
                            }
                        }
                    }
                    else
                    {
                        if (tabBounds.Right > 0)
                        {
                            int tabPageX = _TabControl.GetPageBounds(index).X;
                            if (tabBounds.X < tabPageX)
                            {
                                tabBounds.Width -= tabPageX - tabBounds.X;
                                tabBounds.X = tabPageX;
                            }
                        }
                    }
                }
                else
                {
                    if (_TabControl.RightToLeftLayout)
                    {
                        if (tabBounds.Top < _TabControl.Bottom)
                        {
                            int tabPageBottom = _TabControl.GetPageBounds(index).Bottom;
                            if (tabBounds.Bottom > tabPageBottom)
                            {
                                tabBounds.Height -= tabBounds.Bottom - tabPageBottom;
                            }
                        }
                    }
                    else
                    {
                        if (tabBounds.Bottom > 0)
                        {
                            int tabPageY = _TabControl.GetPageBounds(index).Location.Y;
                            if (tabBounds.Y < tabPageY)
                            {
                                tabBounds.Height -= tabPageY - tabBounds.Y;
                                tabBounds.Y = tabPageY;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the tab background brush.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        protected virtual Brush GetTabBackgroundBrush(int index)
        {
            LinearGradientBrush fillBrush = null;

            // Capture the colours dependant on selection state of the tab
            Color dark = Color.FromArgb(207, 207, 207);
            Color light = Color.FromArgb(242, 242, 242);

            if (_TabControl.SelectedIndex == index)
            {
                dark = SystemColors.ControlLight;
                light = SystemColors.Window;
            }
            else if (!_TabControl.TabPages[index].Enabled)
            {
                light = dark;
            }
            else if (_HotTrack && index == _TabControl.ActiveIndex)
            {
                // Enable hot tracking
                light = Color.FromArgb(234, 246, 253);
                dark = Color.FromArgb(167, 217, 245);
            }

            // Get the correctly aligned gradient
            Rectangle tabBounds = GetTabRect(index);
            tabBounds.Inflate(3, 3);
            tabBounds.X -= 1;
            tabBounds.Y -= 1;
            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:
                    if (_TabControl.SelectedIndex == index)
                    {
                        dark = light;
                    }

                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;

                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
                    break;

                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, dark, light, LinearGradientMode.Horizontal);
                    break;

                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Horizontal);
                    break;
            }

            // Add the blend
            fillBrush.Blend = GetBackgroundBlend();

            return fillBrush;
        }

        /// <summary>
        /// Gets or sets the display style.
        /// </summary>
        /// <value>The display style.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabStyle DisplayStyle
        {
            get
            {
                return _Style;
            }

            set
            {
                _Style = value;
            }
        }

        /// <summary>
        /// Gets or sets the image align.
        /// </summary>
        /// <value>The image align.</value>
        [Category("Appearance")]
        public ContentAlignment ImageAlign
        {
            get
            {
                return _ImageAlign;
            }

            set
            {
                _ImageAlign = value;
                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        [Category("Appearance")]
        public Point Padding
        {
            get
            {
                return _Padding;
            }

            set
            {
                _Padding = value;
                // This line will trigger the handle to recreate, therefore invalidating the control
                if (_ShowTabCloser)
                {
                    if (value.X + _Radius / 2 < -6)
                    {
                        ((System.Windows.Forms.TabControl)_TabControl).Padding = new Point(0, value.Y);
                    }
                    else
                    {
                        ((System.Windows.Forms.TabControl)_TabControl).Padding = new Point(value.X + _Radius / 2 + 6, value.Y);
                    }
                }
                else
                {
                    if (value.X + _Radius / 2 < 1)
                    {
                        ((System.Windows.Forms.TabControl)_TabControl).Padding = new Point(0, value.Y);
                    }
                    else
                    {
                        ((System.Windows.Forms.TabControl)_TabControl).Padding = new Point(value.X + _Radius / 2 - 1, value.Y);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        /// <exception cref="System.ArgumentException">
        /// The radius must be greater than 1 - value
        /// </exception>
        [Category("Appearance")]
        [DefaultValue(1)]
        [Browsable(true)]
        public int Radius
        {
            get
            {
                return _Radius;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("The radius must be greater than 1", "value");
                }

                _Radius = value;
                // Adjust padding
                Padding = _Padding;
            }
        }

        /// <summary>
        /// Gets or sets the overlap.
        /// </summary>
        /// <value>The overlap.</value>
        /// <exception cref="System.ArgumentException">
        /// The tabs cannot have a negative overlap - value
        /// </exception>
        [Category("Appearance")]
        public int Overlap
        {
            get
            {
                return _Overlap;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The tabs cannot have a negative overlap", "value");
                }

                _Overlap = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [focus track].
        /// </summary>
        /// <value><c>true</c> if [focus track]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        public bool FocusTrack
        {
            get
            {
                return _FocusTrack;
            }

            set
            {
                _FocusTrack = value;
                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [hot track].
        /// </summary>
        /// <value><c>true</c> if [hot track]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        public bool HotTrack
        {
            get
            {
                return _HotTrack;
            }

            set
            {
                _HotTrack = value;
                ((System.Windows.Forms.TabControl)_TabControl).HotTrack = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show tab closer].
        /// </summary>
        /// <value><c>true</c> if [show tab closer]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        public bool ShowTabCloser
        {
            get
            {
                return _ShowTabCloser;
            }

            set
            {
                _ShowTabCloser = value;
                // Adjust padding
                Padding = _Padding;
            }
        }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>The opacity.</value>
        /// <exception cref="System.ArgumentException">
        /// The opacity must be between 0 and 1 - value
        /// </exception>
        [Category("Appearance")]
        public float Opacity
        {
            get
            {
                return _Opacity;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The opacity must be between 0 and 1", "value");
                }

                if (value > 1)
                {
                    throw new ArgumentException("The opacity must be between 0 and 1", "value");
                }

                _Opacity = value;
                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border color selected.
        /// </summary>
        /// <value>The border color selected.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        public Color BorderColorSelected
        {
            get
            {
                if (_BorderColorSelected.IsEmpty)
                {
                    return ThemedColors.ToolBorder;
                }

                return _BorderColorSelected;
            }
            set
            {
                if (value.Equals(ThemedColors.ToolBorder))
                {
                    _BorderColorSelected = Color.Empty;
                }
                else
                {
                    _BorderColorSelected = value;
                }

                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border color hot.
        /// </summary>
        /// <value>The border color hot.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        public Color BorderColorHot
        {
            get
            {
                if (_BorderColorHot.IsEmpty)
                {
                    return SystemColors.ControlDark;
                }

                return _BorderColorHot;
            }
            set
            {
                if (value.Equals(SystemColors.ControlDark))
                {
                    _BorderColorHot = Color.Empty;
                }
                else
                {
                    _BorderColorHot = value;
                }

                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get
            {
                if (_BorderColor.IsEmpty)
                {
                    return SystemColors.ControlDark;
                }

                return _BorderColor;
            }
            set
            {
                if (value.Equals(SystemColors.ControlDark))
                {
                    _BorderColor = Color.Empty;
                }
                else
                {
                    _BorderColor = value;
                }

                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        public Color TextColor
        {
            get
            {
                if (_TextColor.IsEmpty)
                {
                    return SystemColors.ControlText;
                }

                return _TextColor;
            }
            set
            {
                if (value.Equals(SystemColors.ControlText))
                {
                    _TextColor = Color.Empty;
                }
                else
                {
                    _TextColor = value;
                }

                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text color selected.
        /// </summary>
        /// <value>The text color selected.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        public Color TextColorSelected
        {
            get
            {
                if (_TextColorSelected.IsEmpty)
                {
                    return SystemColors.ControlText;
                }

                return _TextColorSelected;
            }
            set
            {
                if (value.Equals(SystemColors.ControlText))
                {
                    _TextColorSelected = Color.Empty;
                }
                else
                {
                    _TextColorSelected = value;
                }

                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text color disabled.
        /// </summary>
        /// <value>The text color disabled.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "")]
        public Color TextColorDisabled
        {
            get
            {
                if (_TextColor.IsEmpty)
                {
                    return SystemColors.ControlDark;
                }

                return _TextColorDisabled;
            }
            set
            {
                if (value.Equals(SystemColors.ControlDark))
                {
                    _TextColorDisabled = Color.Empty;
                }
                else
                {
                    _TextColorDisabled = value;
                }

                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the focus.
        /// </summary>
        /// <value>The color of the focus.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Orange")]
        public Color FocusColor
        {
            get
            {
                return _FocusColor;
            }

            set
            {
                _FocusColor = value;
                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the closer color active.
        /// </summary>
        /// <value>The closer color active.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Black")]
        public Color CloserColorActive
        {
            get
            {
                return _CloserColorActive;
            }

            set
            {
                _CloserColorActive = value;
                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the closer.
        /// </summary>
        /// <value>The color of the closer.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "DarkGrey")]
        public Color CloserColor
        {
            get
            {
                return _CloserColor;
            }

            set
            {
                _CloserColor = value;
                _TabControl.Invalidate();
            }
        }

        /// <summary>
        /// Paints the tab.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="graphics">The graphics.</param>
        public void PaintTab(int index, Graphics graphics)
        {
            using (GraphicsPath tabpath = GetTabBorder(index))
            {
                using (Brush fillBrush = GetTabBackgroundBrush(index))
                {
                    // Paint the background
                    graphics.FillPath(fillBrush, tabpath);

                    // Paint a focus indication
                    if (_TabControl.Focused)
                    {
                        DrawTabFocusIndicator(tabpath, index, graphics);
                    }

                    // Paint the closer
                    DrawTabCloser(index, graphics);
                }
            }
        }

        /// <summary>
        /// Draws the tab closer.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="graphics">The graphics.</param>
        protected virtual void DrawTabCloser(int index, Graphics graphics)
        {
            if (_ShowTabCloser)
            {
                Rectangle closerRect = _TabControl.GetTabCloserRect(index);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath closerPath = GetCloserPath(closerRect))
                {
                    if (closerRect.Contains(_TabControl.MousePosition))
                    {
                        using (Pen closerPen = new Pen(_CloserColorActive))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                    else
                    {
                        using (Pen closerPen = new Pen(_CloserColor))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the closer path.
        /// </summary>
        /// <param name="closerRect">The closer rect.</param>
        /// <returns></returns>
        protected static GraphicsPath GetCloserPath(Rectangle closerRect)
        {
            GraphicsPath closerPath = new GraphicsPath();
            closerPath.AddLine(closerRect.X, closerRect.Y, closerRect.Right, closerRect.Bottom);
            closerPath.CloseFigure();
            closerPath.AddLine(closerRect.Right, closerRect.Y, closerRect.X, closerRect.Bottom);
            closerPath.CloseFigure();

            return closerPath;
        }

        /// <summary>
        /// Draws the tab focus indicator.
        /// </summary>
        /// <param name="tabpath">The tabpath.</param>
        /// <param name="index">The index.</param>
        /// <param name="graphics">The graphics.</param>
        private void DrawTabFocusIndicator(GraphicsPath tabpath, int index, Graphics graphics)
        {
            if (_FocusTrack && _TabControl.Focused && index == _TabControl.SelectedIndex)
            {
                Brush focusBrush = null;
                RectangleF pathRect = tabpath.GetBounds();
                Rectangle focusRect = Rectangle.Empty;
                switch (_TabControl.Alignment)
                {
                    case TabAlignment.Top:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, _FocusColor, SystemColors.Window,
                            LinearGradientMode.Vertical);
                        break;

                    case TabAlignment.Bottom:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Bottom - 4, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, _FocusColor,
                            LinearGradientMode.Vertical);
                        break;

                    case TabAlignment.Left:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, _FocusColor, SystemColors.ControlLight,
                            LinearGradientMode.Horizontal);
                        break;

                    case TabAlignment.Right:
                        focusRect = new Rectangle((int)pathRect.Right - 4, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, _FocusColor,
                            LinearGradientMode.Horizontal);
                        break;
                }

                // Ensure the focus stip does not go outside the tab
                Region focusRegion = new Region(focusRect);
                focusRegion.Intersect(tabpath);
                graphics.FillRegion(focusBrush, focusRegion);
                focusRegion.Dispose();
                focusBrush.Dispose();
            }
        }

        /// <summary>
        /// Gets the background blend.
        /// </summary>
        /// <returns></returns>
        private Blend GetBackgroundBlend()
        {
            float[] relativeIntensities = { 0f, 0.7f, 1f };
            float[] relativePositions = { 0f, 0.6f, 1f };

            // Glass look to top aligned tabs
            if (_TabControl.Alignment == TabAlignment.Top)
            {
                relativeIntensities = new[] { 0f, 0.5f, 1f, 1f };
                relativePositions = new[] { 0f, 0.5f, 0.51f, 1f };
            }

            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;

            return blend;
        }

        /// <summary>
        /// Gets the page background brush.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public virtual Brush GetPageBackgroundBrush(int index)
        {
            // Capture the colours dependant on selection state of the tab
            Color light = Color.FromArgb(242, 242, 242);
            if (_TabControl.Alignment == TabAlignment.Top)
            {
                light = Color.FromArgb(207, 207, 207);
            }

            if (_TabControl.SelectedIndex == index)
            {
                light = SystemColors.Window;
            }
            else if (!_TabControl.TabPages[index].Enabled)
            {
                light = Color.FromArgb(207, 207, 207);
            }
            else if (_HotTrack && index == _TabControl.ActiveIndex)
            {
                // Enable hot tracking
                light = Color.FromArgb(234, 246, 253);
            }

            return new SolidBrush(light);
        }
    }
}