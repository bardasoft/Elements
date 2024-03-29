﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Elements.Controls.TabControl.TabStyleProviders
{
    /// <summary>
    /// The <see cref="TabStyleChromeProvider"/> class.
    /// </summary>
    /// <seealso cref="Elements.Controls.TabControl.TabStyleProvider"/>
    [ToolboxItem(false)]
    public class TabStyleChromeProvider : TabStyleProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabStyleChromeProvider"/> class.
        /// </summary>
        /// <param name="tabControl">The tab control.</param>
        public TabStyleChromeProvider(TabControl tabControl) : base(tabControl)
        {
            _Overlap = 16;
            _Radius = 16;
            _ShowTabCloser = true;
            _CloserColorActive = Color.White;

            // Must set after the _Radius as this is used in the calculations of the actual padding
            Padding = new Point(7, 5);
        }

        /// <summary>
        /// Adds the tab border.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="tabBounds">The tab bounds.</param>
        public override void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            int spread;
            int eigth;
            int sixth;
            int quarter;

            if (_TabControl.Alignment <= TabAlignment.Bottom)
            {
                spread = (int)Math.Floor((decimal)tabBounds.Height * 2 / 3);
                eigth = (int)Math.Floor((decimal)tabBounds.Height * 1 / 8);
                sixth = (int)Math.Floor((decimal)tabBounds.Height * 1 / 6);
                quarter = (int)Math.Floor((decimal)tabBounds.Height * 1 / 4);
            }
            else
            {
                spread = (int)Math.Floor((decimal)tabBounds.Width * 2 / 3);
                eigth = (int)Math.Floor((decimal)tabBounds.Width * 1 / 8);
                sixth = (int)Math.Floor((decimal)tabBounds.Width * 1 / 6);
                quarter = (int)Math.Floor((decimal)tabBounds.Width * 1 / 4);
            }

            switch (_TabControl.Alignment)
            {
                case TabAlignment.Top:

                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.X, tabBounds.Bottom),
                        new Point(tabBounds.X + sixth, tabBounds.Bottom - eigth),
                        new Point(tabBounds.X + spread - quarter, tabBounds.Y + eigth),
                        new Point(tabBounds.X + spread, tabBounds.Y)
                    });
                    path.AddLine(tabBounds.X + spread, tabBounds.Y, tabBounds.Right - spread, tabBounds.Y);
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.Right - spread, tabBounds.Y),
                        new Point(tabBounds.Right - spread + quarter, tabBounds.Y + eigth),
                        new Point(tabBounds.Right - sixth, tabBounds.Bottom - eigth),
                        new Point(tabBounds.Right, tabBounds.Bottom)
                    });
                    break;

                case TabAlignment.Bottom:
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.Right, tabBounds.Y),
                        new Point(tabBounds.Right - sixth, tabBounds.Y + eigth),
                        new Point(tabBounds.Right - spread + quarter, tabBounds.Bottom - eigth),
                        new Point(tabBounds.Right - spread, tabBounds.Bottom)
                    });
                    path.AddLine(tabBounds.Right - spread, tabBounds.Bottom, tabBounds.X + spread, tabBounds.Bottom);
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.X + spread, tabBounds.Bottom),
                        new Point(tabBounds.X + spread - quarter, tabBounds.Bottom - eigth),
                        new Point(tabBounds.X + sixth, tabBounds.Y + eigth), new Point(tabBounds.X, tabBounds.Y)
                    });
                    break;

                case TabAlignment.Left:
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.Right, tabBounds.Bottom),
                        new Point(tabBounds.Right - eigth, tabBounds.Bottom - sixth),
                        new Point(tabBounds.X + eigth, tabBounds.Bottom - spread + quarter),
                        new Point(tabBounds.X, tabBounds.Bottom - spread)
                    });
                    path.AddLine(tabBounds.X, tabBounds.Bottom - spread, tabBounds.X, tabBounds.Y + spread);
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.X, tabBounds.Y + spread),
                        new Point(tabBounds.X + eigth, tabBounds.Y + spread - quarter),
                        new Point(tabBounds.Right - eigth, tabBounds.Y + sixth), new Point(tabBounds.Right, tabBounds.Y)
                    });

                    break;

                case TabAlignment.Right:
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.X, tabBounds.Y), new Point(tabBounds.X + eigth, tabBounds.Y + sixth),
                        new Point(tabBounds.Right - eigth, tabBounds.Y + spread - quarter),
                        new Point(tabBounds.Right, tabBounds.Y + spread)
                    });
                    path.AddLine(tabBounds.Right, tabBounds.Y + spread, tabBounds.Right, tabBounds.Bottom - spread);
                    path.AddCurve(new[]
                    {
                        new Point(tabBounds.Right, tabBounds.Bottom - spread),
                        new Point(tabBounds.Right - eigth, tabBounds.Bottom - spread + quarter),
                        new Point(tabBounds.X + eigth, tabBounds.Bottom - sixth),
                        new Point(tabBounds.X, tabBounds.Bottom)
                    });
                    break;
            }
        }

        /// <summary>
        /// Draws the tab closer.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="graphics">The graphics.</param>
        protected override void DrawTabCloser(int index, Graphics graphics)
        {
            if (_ShowTabCloser)
            {
                Rectangle closerRect = _TabControl.GetTabCloserRect(index);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if (closerRect.Contains(_TabControl.MousePosition))
                {
                    using (GraphicsPath closerPath = GetCloserButtonPath(closerRect))
                    {
                        using (SolidBrush closerBrush = new SolidBrush(Color.FromArgb(193, 53, 53)))
                        {
                            graphics.FillPath(closerBrush, closerPath);
                        }
                    }

                    using (GraphicsPath closerPath = GetCloserPath(closerRect))
                    {
                        using (Pen closerPen = new Pen(_CloserColorActive))
                        {
                            graphics.DrawPath(closerPen, closerPath);
                        }
                    }
                }
                else
                {
                    using (GraphicsPath closerPath = GetCloserPath(closerRect))
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
        /// Gets the closer button path.
        /// </summary>
        /// <param name="closerRect">The closer rect.</param>
        /// <returns></returns>
        private static GraphicsPath GetCloserButtonPath(Rectangle closerRect)
        {
            GraphicsPath closerPath = new GraphicsPath();
            closerPath.AddEllipse(new Rectangle(closerRect.X - 2, closerRect.Y - 2, closerRect.Width + 4,
                closerRect.Height + 4));
            closerPath.CloseFigure();
            return closerPath;
        }
    }
}