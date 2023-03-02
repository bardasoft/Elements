﻿using Elements.Base;
using Elements.Constants;
using Elements.Controls.Button;
using Elements.Enumerators;
using Elements.Models;
using Elements.Renders;
using Elements.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Elements.Controls.GroupBox
{
    /// <summary>
    /// The <see cref="GroupBox"/> class.
    /// </summary>
    /// <seealso cref="NestedControlBase"/>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The GroupBox")]
    [Designer(typeof(GroupBoxDesigner))]
    [ToolboxBitmap(typeof(GroupBox), "GroupBox.bmp")]
    [ToolboxItem(true)]
    public class GroupBox : NestedControlBase
    {
        private Border _border;
        //private BorderEdge _borderEdge;
        private GroupBoxStyle _boxStyle;
        private Image _image;
        private StringAlignment _textAlignment;
        private TextImageRelation _textImageRelation;
        private StringAlignment _textLineAlignment;
        private int _titleBoxHeight;
        private Rectangle _titleBoxRectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupBox"/> class.
        /// </summary>
        public GroupBox()
        {
            BackColor = Color.Transparent;

            _boxStyle = GroupBoxStyle.Default;
            _titleBoxHeight = 25;
            //_borderEdge = new BorderEdge();
            _textImageRelation = TextImageRelation.ImageBeforeText;
            _textAlignment = StringAlignment.Center;
            _textLineAlignment = StringAlignment.Center;
            Size = new Size(220, 180);
            _border = new Border();
            Padding = new Padding(5, _titleBoxHeight + _border.Thickness, 5, 5);
            //Controls.Add(_borderEdge);
        }

        /// <summary>
        /// Gets or sets the border.
        /// </summary>
        /// <value>The border.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border Border
        {
            get
            {
                return _border;
            }

            set
            {
                _border = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description("Box Style")]
        public GroupBoxStyle BoxStyle
        {
            get
            {
                return _boxStyle;
            }

            set
            {
                _boxStyle = value;

                //if (_boxStyle == GroupBoxStyle.Classic)
                //{
                //    _borderEdge.Visible = false;
                //}
                //else
                //{
                //    _borderEdge.Visible = true;
                //}

                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description("Image")]
        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description("Separator")]
        public bool Separator
        {
            //get
            //{
            //   // return _borderEdge.Visible;
            //}

            set
            {
                //_borderEdge.Visible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description("Color")]
        public Color SeparatorColor
        {
            get
            {
                return Color.Green;
                // return _borderEdge.BackColor;
            }

            //set
            //{
            //    _borderEdge.BackColor = value;
            //    Invalidate();
            //}
        }

        [Category(PropertyCategory.Appearance)]
        [Description("Alignment")]
        public StringAlignment TextAlignment
        {
            get
            {
                return _textAlignment;
            }

            set
            {
                _textAlignment = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description("Text Image Relation")]
        public TextImageRelation TextImageRelation
        {
            get
            {
                return _textImageRelation;
            }

            set
            {
                _textImageRelation = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description("Alignment")]
        public StringAlignment TextLineAlignment
        {
            get
            {
                return _textLineAlignment;
            }

            set
            {
                _textLineAlignment = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description("Size")]
        public int TitleBoxHeight
        {
            get
            {
                return _titleBoxHeight;
            }

            set
            {
                _titleBoxHeight = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.GammaCorrected;

            Size textArea = StringUtilities.MeasureText(Text, Font, graphics);
            Rectangle group = ConfigureStyleBox(textArea);
            Rectangle title = ConfigureStyleTitleBox(textArea);

            _titleBoxRectangle = new Rectangle(title.X, title.Y, title.Width - 1, title.Height);

            Rectangle _clientRectangle = new Rectangle(group.X, group.Y, group.Width, group.Height);
            var ControlGraphicsPath = Border.CreatePath(_border, _clientRectangle);
            graphics.FillRectangle(new SolidBrush(BackColor), _clientRectangle);

            Color _backColor = Enabled ? BackColorState.Enabled : BackColorState.Disabled;
            ImageRender.Render(e.Graphics, _backColor, BackgroundImage, group, Border);

            //if (_borderEdge.Visible)
            //{
            //    _borderEdge.Location = new Point(_titleBoxRectangle.X + _border.Thickness, _titleBoxRectangle.Bottom);
            //    _borderEdge.Size = new Size(Width - _border.Thickness - 1, 1);
            //}

            Border.Render(e.Graphics, _border, MouseState, ControlGraphicsPath);

            if (_boxStyle == GroupBoxStyle.Classic)
            {
                Size _newSize;
                if (_image != null)
                {
                    _newSize = _image.Size;
                }
                else
                {
                    _newSize = new Size(0, 0);
                }

                _titleBoxRectangle = new Rectangle(5, 0, title.Width - 1, title.Height);
                Point _titleBoxBackground = RelationUtilities.GetTextImageRelationLocation(graphics, _textImageRelation, new Rectangle(new Point(0, 0), _newSize), Text, Font, _titleBoxRectangle, ImageTextRelation.Text);
                graphics.FillRectangle(new SolidBrush(BackColorState.Enabled), new Rectangle(new Point(_titleBoxBackground.X, _titleBoxBackground.Y), new Size(_titleBoxRectangle.Width, _titleBoxRectangle.Height)));
            }

            if (_image != null)
            {
                GraphicsUtilities.DrawContent(e.Graphics, _titleBoxRectangle, Text, Font, ForeColor, _image, _image.Size, _textImageRelation);
            }
            else
            {
                StringFormat _stringFormat = new StringFormat { Alignment = _textAlignment, LineAlignment = _textLineAlignment };

                StringUtilities.Render(e.Graphics, _titleBoxRectangle, Text, Font, ForeColor, _stringFormat);
            }
        }

        private Rectangle ConfigureStyleBox(Size textArea)
        {
            Size groupBoxSize = new Size();
            Point groupBoxPoint = new Point(0, 0);

            switch (_boxStyle)
            {
                case GroupBoxStyle.Default:
                    {
                        groupBoxSize = new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                        break;
                    }

                case GroupBoxStyle.Classic:
                    {
                        groupBoxPoint = new Point(0, textArea.Height / 2);
                        groupBoxSize = new Size(Width - _border.Thickness, Height - (textArea.Height / 2) - _border.Thickness);

                        break;
                    }
            }

            Rectangle groupBoxRectangle = new Rectangle(groupBoxPoint, groupBoxSize);

            return groupBoxRectangle;
        }

        private Rectangle ConfigureStyleTitleBox(Size textArea)
        {
            Size titleSize = new Size(Width, _titleBoxHeight);
            Point titlePoint = new Point(0, 0);

            switch (_boxStyle)
            {
                case GroupBoxStyle.Default:
                    {
                        titlePoint = new Point(titlePoint.X, 0);

                        break;
                    }

                case GroupBoxStyle.Classic:
                    {
                        const int Spacing = 5;

                        titlePoint = new Point(titlePoint.X, 0);

                        // +1 extra whitespace in case of FontStyle=Bold
                        titleSize = new Size(textArea.Width + 2, textArea.Height);

                        switch (_textAlignment)
                        {
                            case StringAlignment.Near:
                                {
                                    titlePoint.X += 5;
                                    break;
                                }

                            case StringAlignment.Center:
                                {
                                    titlePoint.X = (Width / 2) - (textArea.Width / 2);
                                    break;
                                }

                            case StringAlignment.Far:
                                {
                                    titlePoint.X = Width - textArea.Width - Spacing;
                                    break;
                                }

                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            return new Rectangle(titlePoint, titleSize);
        }
    }
}