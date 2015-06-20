using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BlueOnion
{
    public class ArrowButton : Button
    {
        private Point[] arrow;
        private Container components;

        public enum ArrowButtonDirection
        {
            Up = 0,
            Down,
            Left,
            Right
        };

        // ---------------------------------------------------------------------
        public ArrowButton()
        {
            InitializeComponent();
        }

        // ---------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        // ---------------------------------------------------------------------
        public ArrowButtonDirection Direction { get; set; } = ArrowButtonDirection.Left;

        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var color = (ForeColor == Color.White)
                ? Color.BlanchedAlmond
                : ControlPaint.Light(ForeColor);

            var g = CreateGraphics();
            DrawArrow(color, g);
            g.Dispose();
        }

        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawArrow(ForeColor, pe.Graphics);
        }

        // ---------------------------------------------------------------------
        protected override void OnSizeChanged(EventArgs e)
        {
            var width = Width;
            var height = Height;

            switch (Direction)
            {
                case ArrowButtonDirection.Left:
                    arrow = new[]
                    {
                        new Point(0, height/2),
                        new Point(width, 0),
                        new Point(width, height),
                        new Point(0, height/2)
                    };
                    break;

                case ArrowButtonDirection.Right:
                    arrow = new[]
                    {
                        new Point(width, height/2),
                        new Point(0, 0),
                        new Point(0, height),
                        new Point(width, height/2)
                    };
                    break;

                case ArrowButtonDirection.Up:
                    arrow = new[]
                    {
                        new Point(width/2, 0),
                        new Point(0, height),
                        new Point(width, height),
                        new Point(width/2, 0)
                    };
                    break;

                case ArrowButtonDirection.Down:
                    arrow = new[]
                    {
                        new Point(width/2, height),
                        new Point(0, 0),
                        new Point(width, 0),
                        new Point(width/2, height)
                    };
                    break;

                default:
                    throw new InvalidEnumArgumentException
                        ("Invalid ArrowButtonDirection");
            }
            ;

            var graphics = new GraphicsPath();
            graphics.AddLines(arrow);

            var old = Region;
            Region = new Region(graphics);

            if (old != null)
            {
                old.Dispose();
            }

            base.OnSizeChanged(e);
        }

        // ---------------------------------------------------------------------
        private void DrawArrow(Color color, Graphics graphics)
        {
            Brush b = new SolidBrush(color);
            graphics.FillRegion(b, Region);
            b.Dispose();

            var width = Width;
            var height = Height;

            var p = new Pen(BackColor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawLines(p, arrow);
            p.Dispose();
        }
    }
}