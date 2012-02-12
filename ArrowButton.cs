// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace BlueOnion
{
	public class ArrowButton : System.Windows.Forms.Button
	{
        private Point[] arrow;
        private ArrowButtonDirection direction = ArrowButtonDirection.Left;
		private System.ComponentModel.Container components = null;

        public enum ArrowButtonDirection { Up = 0, Down, Left, Right };

        // ---------------------------------------------------------------------
		public ArrowButton()
		{
			InitializeComponent();
		}

        // ---------------------------------------------------------------------
        protected override void Dispose( bool disposing )
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
        public ArrowButtonDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Color color = (this.ForeColor == Color.White) 
                ? Color.BlanchedAlmond
                : ControlPaint.Light(this.ForeColor);

            Graphics g = this.CreateGraphics();
            this.DrawArrow(color, g);
            g.Dispose();
        }

        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pe)
		{
            DrawArrow(this.ForeColor, pe.Graphics);
		}

        // ---------------------------------------------------------------------
        protected override void OnSizeChanged(EventArgs e)
        {
            int width = this.Width;
            int height = this.Height;

            switch (this.direction)
            {
                case ArrowButtonDirection.Left:
                    this.arrow = new Point[]
                    {
                        new Point(0, height/2),
                        new Point(width, 0),
                        new Point(width, height),
                        new Point(0, height/2)
                    };
                    break;

                case ArrowButtonDirection.Right:
                    this.arrow = new Point[]
                    {
                        new Point(width, height/2),
                        new Point(0, 0),
                        new Point(0, height),
                        new Point(width, height/2)
                    };
                    break;

                case ArrowButtonDirection.Up:
                    this.arrow = new Point[]
                    {
                        new Point(width/2, 0),
                        new Point(0, height),
                        new Point(width, height),
                        new Point(width/2, 0)
                    };
                    break;

                case ArrowButtonDirection.Down:
                    this.arrow = new Point[]
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
            };

            GraphicsPath graphics  = new GraphicsPath();
            graphics.AddLines(arrow);

            Region old = this.Region;
            this.Region = new Region(graphics);
            
            if (old != null)
            {
                old.Dispose();
            }

            base.OnSizeChanged (e);
        }

        // ---------------------------------------------------------------------
        private void DrawArrow(Color color, Graphics graphics)
        {
            Brush b = new SolidBrush(color);
            graphics.FillRegion(b, this.Region);
            b.Dispose();

            int width = this.Width;
            int height = this.Height;

            Pen p = new Pen(this.BackColor);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawLines(p, this.arrow);
            p.Dispose();
        }
	}
}
