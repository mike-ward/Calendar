// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace BlueOnion
{
    public class MonthCalendarEx : System.Windows.Forms.Control
    {
        private Size months = new Size(1, 1);
        private DateTime today = DateTime.Today;
        private DateTime currentDate = DateTime.Today;
        private DateTime firstMonth = DateTime.Today;
        private DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;
        private DateTime[] boldedDates;
        private DateTime[] coloredDates;
        private Hashtable boldedDays;
        private Hashtable coloredDays;
        private bool showToday = true;
        private bool showWeekNumbers = false;
        private bool showTodayCircle = true;

        private Color titleForeColor = SystemColors.ActiveCaptionText;
        private Color titleBackColor = SystemColors.ActiveCaption;
        private Color weekdayForeColor = SystemColors.ActiveCaption;
        private Color weekdayBackColor = SystemColors.ActiveCaptionText;
        private Color weekdayBarColor = SystemColors.WindowText;
        private Color weeknumberForeColor = SystemColors.ActiveCaption;
        private Color weeknumberBackColor = SystemColors.ActiveCaptionText;
        private Color trailingForeColor = SystemColors.GrayText;
        private Color highlightDayTextColor = Color.DarkRed;
        private Color gridlinesColor = SystemColors.GrayText;

        private SizeF cellSize;
        private SizeF cellPadding = new Size(0, 0);
        private SizeF monthPadding = new Size(1, 1);
        private SizeF border = new SizeF(0, 0);
        private Size singleMonthSize;

        private RectangleF titleRectangle;
        private RectangleF weekdaysRectangle;
        private RectangleF weekNumbersRectangle;
        private RectangleF daysRectangle;
        private RectangleF todayRectangle;
        private RectangleF displayTodayRectangle;

        private struct CalendarArea
        {
            public DateTime date;
            public RectangleF days;
            public int dayOffset;
        }

        private CalendarArea[] CalendarAreas;

        private RectangleF[] MonthNameAreas;
        private RectangleF[] YearAreas;
        private ArrowButton previousButton;
        private ArrowButton nextButton;
        private System.Windows.Forms.ContextMenu monthMenu;
        private System.Windows.Forms.ContextMenu yearMenu;
        private System.ComponentModel.Container components = null;

        public event DateRangeEventHandler DateChanged;

        private enum Trailing
        {
            None = 0,
            First,
            Last,
            Both
        }

        private bool gridlines;

        // ---------------------------------------------------------------------
        public MonthCalendarEx()
        {
            InitializeComponent();
            OnFontChanged(EventArgs.Empty);

            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.nextButton);

            SetStyle(ControlStyles.ResizeRedraw, true);

            foreach (string month in
                CultureInfo.CurrentCulture.DateTimeFormat.MonthNames)
            {
                if (month.Length > 0)
                {
                    this.monthMenu.MenuItems.Add(month,
                        new EventHandler(OnSelectMonth));
                }
            }

            UpdateYearMenu();
        }

        // ---------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (this.monthMenu != null)
                {
                    this.monthMenu.Dispose();
                    this.monthMenu = null;
                }

                if (this.monthMenu != null)
                {
                    this.yearMenu.Dispose();
                    this.yearMenu = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code
        // ---------------------------------------------------------------------
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previousButton = new BlueOnion.ArrowButton();
            this.nextButton = new BlueOnion.ArrowButton();
            this.monthMenu = new System.Windows.Forms.ContextMenu();
            this.yearMenu = new System.Windows.Forms.ContextMenu();
            // 
            // previousButton
            // 
            this.previousButton.BackColor = System.Drawing.SystemColors.Control;
            this.previousButton.Direction = BlueOnion.ArrowButton.ArrowButtonDirection.Left;
            this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.previousButton.ForeColor = System.Drawing.Color.White;
            this.previousButton.Location = new System.Drawing.Point(17, 17);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(23, 23);
            this.previousButton.TabIndex = 0;
            this.previousButton.TabStop = false;
            this.previousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Direction = BlueOnion.ArrowButton.ArrowButtonDirection.Right;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.nextButton.ForeColor = System.Drawing.Color.White;
            this.nextButton.Location = new System.Drawing.Point(148, 17);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(23, 23);
            this.nextButton.TabIndex = 0;
            this.nextButton.TabStop = false;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);

        }
        #endregion

        // ---------------------------------------------------------------------
        protected override void OnClick(EventArgs e)
        {
            Point point = this.PointToClient(Control.MousePosition);

            if (this.displayTodayRectangle.Contains(point))
            {
                this.GoToToday();
                base.OnClick(e);
                return;
            }

            foreach (RectangleF test in this.MonthNameAreas)
            {
                if (test.Contains(point))
                {
                    this.monthMenu.Show(this, point);
                    base.OnClick(e);
                    return;
                }
            }

            foreach (RectangleF test in this.YearAreas)
            {
                if (test.Contains(point))
                {
                    this.yearMenu.Show(this, point);
                    base.OnClick(e);
                    return;
                }
            }

            base.OnClick(e);
        }

        // ---------------------------------------------------------------------
        protected void OnSelectMonth(Object sender, System.EventArgs e)
        {
            this.firstMonth = new DateTime(
                this.firstMonth.Year, ((MenuItem)sender).Index + 1, 1);

            this.DateChange();
        }

        // ---------------------------------------------------------------------
        protected void OnSelectYear(Object sender, System.EventArgs e)
        {
            this.firstMonth = new DateTime(
                this.firstMonth.Year + ((MenuItem)sender).Index - 5,
                this.firstMonth.Month, this.firstMonth.Day);

            this.DateChange();
        }

        // ---------------------------------------------------------------------
        protected virtual void OnDateChanged(DateRangeEventArgs e)
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }

        // ---------------------------------------------------------------------
        protected override void OnFontChanged(EventArgs e)
        {
            this.MonthSize();
            this.months = this.Months();
            this.BuildRegions();
            this.PositionButtons();
            base.OnFontChanged(e);
        }

        // ---------------------------------------------------------------------
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            this.firstMonth = this.firstMonth.AddMonths((e.Delta < 0) ? 1 : -1);
            DateChange();
        }

        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.displayTodayRectangle.Contains(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
                base.OnMouseMove(e);
                return;
            }

            foreach (RectangleF area in this.MonthNameAreas)
            {
                if (area.Contains(e.X, e.Y) == true)
                {
                    this.Cursor = Cursors.Hand;
                    base.OnMouseMove(e);
                    return;
                }
            }

            foreach (RectangleF area in this.YearAreas)
            {
                if (area.Contains(e.X, e.Y) == true)
                {
                    this.Cursor = Cursors.Hand;
                    base.OnMouseMove(e);
                    return;
                }
            }

            this.Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }

        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pe)
        {
            DateTime month = FirstOfMonth(this.firstMonth);
            PointF coordinates = this.border.ToPointF();

            Brush textBrush = new SolidBrush(this.ForeColor);
            Brush backBrush = new SolidBrush(this.BackColor);
            Brush titleTextBrush = new SolidBrush(this.TitleForeColor);
            Brush titleBackBrush = new SolidBrush(this.TitleBackColor);
            Brush weekdayTextBrush = new SolidBrush(this.WeekdayForeColor);
            Brush weekdayBackBrush = new SolidBrush(this.WeekdayBackColor);
            Brush trailingTextBrush = new SolidBrush(this.TrailingForeColor);
            Brush highlightDayTextBrush = new SolidBrush(this.HighlightDayTextColor);
            Brush weeknumberTextBrush = new SolidBrush(this.WeeknumberForeColor);
            Brush weeknumberBackBrush = new SolidBrush(this.WeeknumberBackColor);
            Font boldFont = new Font(this.Font, FontStyle.Bold);
            Pen divider = new Pen(this.WeekdayBarColor);
            string[] weekdays = AbbreviatedDayNames();

            float cellMiddle = this.cellSize.Width * 0.5F;
            float[] cellMiddles = new float[weekdays.Length];

            for (int w = 0; w < weekdays.Length; ++w)
            {
                cellMiddles[w] = cellMiddle;
                cellMiddle += this.cellSize.Width;
            }

            for (int y = 0; y < months.Height; ++y)
            {
                for (int x = 0; x < this.months.Width; ++x)
                {
                    Trailing drawTrailing = Trailing.None;

                    if (this.months.Width * this.months.Height == 1)
                    {
                        drawTrailing = Trailing.Both;
                    }

                    else if (y == 0 && x == 0)
                    {
                        drawTrailing = Trailing.First;
                    }

                    else if (y == (months.Height - 1) &&
                        x == (this.months.Width - 1))
                    {
                        drawTrailing = Trailing.Last;
                    }

                    DrawMonth(
                        pe.Graphics,
                        month,
                        coordinates,
                        weekdays,
                        boldFont,
                        textBrush,
                        backBrush,
                        titleTextBrush,
                        titleBackBrush,
                        weekdayTextBrush,
                        weekdayBackBrush,
                        trailingTextBrush,
                        highlightDayTextBrush,
                        weeknumberTextBrush,
                        weeknumberBackBrush,
                        drawTrailing,
                        divider,
                        cellMiddles);

                    coordinates.X += SingleMonthSize.Width;
                    month = month.AddMonths(1);
                }

                coordinates.X = this.border.Width;
                coordinates.Y += SingleMonthSize.Height;
            }

            // Draw today string at bottom of calendar
            if (this.ShowToday == true)
            {
                displayTodayRectangle = this.todayRectangle;

                displayTodayRectangle.Offset(0,
                    (this.months.Height - 1) * this.singleMonthSize.Height);

                StringFormat todayFormat = new StringFormat();
                todayFormat.Alignment = StringAlignment.Center;
                todayFormat.LineAlignment = StringAlignment.Center;

                pe.Graphics.DrawString(
                    "Today: " + this.TodayDate.ToShortDateString(),
                    boldFont, textBrush, displayTodayRectangle, todayFormat);
            }

            else
            {
                displayTodayRectangle = RectangleF.Empty;
            }

            // Dispose of resources
            textBrush.Dispose();
            backBrush.Dispose();
            titleTextBrush.Dispose();
            titleBackBrush.Dispose();
            weekdayTextBrush.Dispose();
            weekdayBackBrush.Dispose();
            trailingTextBrush.Dispose();
            highlightDayTextBrush.Dispose();
            weeknumberTextBrush.Dispose();
            weeknumberBackBrush.Dispose();
            boldFont.Dispose();
            divider.Dispose();

            base.OnPaint(pe);
        }

        // ---------------------------------------------------------------------
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    if (e.Control == true && e.Shift == false && e.Alt == false)
                    {
                        this.firstMonth = this.firstMonth.AddYears(1);
                    }

                    else
                    {
                        this.firstMonth = this.firstMonth.AddMonths(1);
                    }

                    DateChange();
                    break;

                case Keys.PageDown:
                    if (e.Control == true && e.Shift == false && e.Alt == false)
                    {
                        this.firstMonth = this.firstMonth.AddYears(-1);
                    }

                    else
                    {
                        this.firstMonth = this.firstMonth.AddMonths(-1);
                    }

                    DateChange();
                    break;

                case Keys.Home:
                    this.GoToToday();
                    break;

                default:
                    break;
            }

            base.OnKeyDown(e);
        }

        // ---------------------------------------------------------------------
        protected override void OnSizeChanged(EventArgs e)
        {
            // Figure out if hittest regions need to be rebuilt
            Size months = this.Months();

            if (this.months != months || this.CalendarAreas == null)
            {
                this.months = months;
                PositionButtons();
                BuildRegions();
            }

            base.OnSizeChanged(e);
        }

        // ---------------------------------------------------------------------
        public DateTime[] BoldedDates
        {
            get { return this.boldedDates; }
            set { this.boldedDates = value; }
        }

        // ---------------------------------------------------------------------
        public DateTime[] ColoredDates
        {
            get { return this.coloredDates; }
            set { this.coloredDates = value; }
        }

        // ---------------------------------------------------------------------
        public DayOfWeek FirstDayOfWeek
        {
            get
            {
                return this.firstDayOfWeek;
            }

            set
            {
                if (this.FirstDayOfWeek != value)
                {
                    this.firstDayOfWeek = value;
                    BuildRegions();
                    this.Invalidate();
                }
            }
        }

        // ---------------------------------------------------------------------
        public SelectionRange GetDisplayRange()
        {
            DateTime start = FirstOfMonth(this.firstMonth);
            DateTime end = start.AddMonths(this.months.Width * this.months.Height).AddDays(-1);
            return new SelectionRange(start, end);
        }

        // ---------------------------------------------------------------------
        public bool Gridlines
        {
            get
            {
                return this.gridlines;
            }

            set
            {
                this.gridlines = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public HitTestInfoEx HitTest(Point point)
        {
            int totalMonths = this.months.Width * this.months.Height;

            for (int m = 0; m < totalMonths; ++m)
            {
                if (this.CalendarAreas[m].days.Contains(point) == true)
                {
                    int x = (int)((point.X - this.CalendarAreas[m].days.Left) /
                        this.cellSize.Width) + 1;

                    int y = (int)((point.Y - this.CalendarAreas[m].days.Top) /
                        this.cellSize.Height);

                    DateTime time = this.CalendarAreas[m].date.AddDays
                        ((x + (y * 7)) - this.CalendarAreas[m].dayOffset - 1);

                    if (time.Month != this.CalendarAreas[m].date.Month)
                    {
                        time = DateTime.MinValue;
                    }

                    return new HitTestInfoEx(point, time);
                }
            }

            return new HitTestInfoEx(point, DateTime.MinValue);
        }

        // ---------------------------------------------------------------------
        public int MaxSelectionCount
        {
            get { return 1; }
            set { }
        }

        // ---------------------------------------------------------------------
        public void SetDate(DateTime date)
        {
            this.currentDate = date;
        }

        // ---------------------------------------------------------------------
        public bool ShowToday
        {
            get
            {
                return this.showToday;
            }

            set
            {
                this.showToday = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public bool ShowTodayCircle
        {
            get
            {
                return this.showTodayCircle;
            }

            set
            {
                this.showTodayCircle = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public bool ShowWeekNumbers
        {
            get
            {
                return this.showWeekNumbers;
            }

            set
            {
                if (this.showWeekNumbers != value)
                {
                    this.showWeekNumbers = value;
                    MonthSize();
                    BuildRegions();
                    PositionButtons();
                    this.Invalidate();
                }
            }
        }

        // ---------------------------------------------------------------------
        [Browsable(false)]
        public Size SingleMonthSize
        {
            get { return this.singleMonthSize; }
        }

        // ---------------------------------------------------------------------
        public virtual Color TitleForeColor
        {
            get { return this.titleForeColor; }

            set
            {
                this.titleForeColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color TitleBackColor
        {
            get
            {
                return this.titleBackColor;
            }

            set
            {
                this.titleBackColor = value;
                this.previousButton.BackColor = value;
                this.nextButton.BackColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeekdayForeColor
        {
            get { return this.weekdayForeColor; }

            set
            {
                this.weekdayForeColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeekdayBackColor
        {
            get
            {
                return this.weekdayBackColor;
            }

            set
            {
                this.weekdayBackColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeekdayBarColor
        {
            get
            {
                return this.weekdayBarColor;
            }

            set
            {
                this.weekdayBarColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeeknumberForeColor
        {
            get { return this.weeknumberForeColor; }

            set
            {
                this.weeknumberForeColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeeknumberBackColor
        {
            get
            {
                return this.weeknumberBackColor;
            }

            set
            {
                this.weeknumberBackColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color TrailingForeColor
        {
            get { return this.trailingForeColor; }

            set
            {
                this.trailingForeColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color HighlightDayTextColor
        {
            get { return this.highlightDayTextColor; }

            set
            {
                this.highlightDayTextColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color GridlinesColor
        {
            get { return this.gridlinesColor; }

            set
            {
                this.gridlinesColor = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        [Browsable(false)]
        public DateTime TodayDate
        {
            get { return this.today; }

            set
            {
                this.today = value;
                this.Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        [Browsable(false)]
        public int TodayHeight
        {
            get { return (int)this.todayRectangle.Height; }
        }

        // ---------------------------------------------------------------------
        public void UpdateBoldedDates()
        {
            this.boldedDays = new Hashtable();

            if (this.boldedDates != null)
            {
                foreach (DateTime date in this.boldedDates)
                {
                    if (this.boldedDays.Contains(date.Date) == false)
                    {
                        this.boldedDays.Add(date.Date, null);
                    }
                }
            }

            this.coloredDays = new Hashtable();

            if (this.coloredDates != null)
            {
                foreach (DateTime date in this.coloredDates)
                {
                    if (this.coloredDays.Contains(date.Date) == false)
                    {
                        this.coloredDays.Add(date.Date, null);
                    }
                }
            }
        }

        // ---------------------------------------------------------------------
        private static string[] AbbreviatedDayNames()
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
        }

        // ---------------------------------------------------------------------
        private void BuildRegions()
        {
            int totalMonths = this.months.Width * this.months.Height;
            this.CalendarAreas = new CalendarArea[totalMonths];
            this.MonthNameAreas = new RectangleF[totalMonths];
            this.YearAreas = new RectangleF[totalMonths];
            Graphics g = this.CreateGraphics();

            StringFormat titleFormat = new StringFormat();
            titleFormat.Alignment = StringAlignment.Center;
            titleFormat.LineAlignment = StringAlignment.Center;

            RectangleF days = this.daysRectangle;
            int m = 0;

            for (int y = 0; y < this.months.Height; ++y)
            {
                for (int x = 0; x < this.months.Width; ++x, ++m)
                {
                    DateTime date = FirstOfMonth(this.firstMonth.AddMonths(m));
                    this.CalendarAreas[m].date = date;

                    this.CalendarAreas[m].dayOffset =
                        ((int)this.CalendarAreas[m].date.DayOfWeek + 7 -
                        (int)this.FirstDayOfWeek) % 7;

                    float xMonth = x * this.singleMonthSize.Width;
                    float yMonth = y * this.SingleMonthSize.Height;
                    this.CalendarAreas[m].days = days;
                    this.CalendarAreas[m].days.Offset(xMonth, yMonth);


                    SizeF titleSize = g.MeasureString(
                        date.ToString("MMMM, yyyy", CultureInfo.CurrentCulture),
                        this.Font, this.titleRectangle.Size, titleFormat);

                    SizeF monthSize = g.MeasureString(
                        date.ToString("MMMM, ", CultureInfo.CurrentCulture),
                        this.Font);

                    this.MonthNameAreas[m] = this.titleRectangle;

                    this.MonthNameAreas[m].Inflate(
                        (titleSize.Width - this.titleRectangle.Width) / 2,
                        (titleSize.Height - this.titleRectangle.Height) / 2);

                    this.MonthNameAreas[m].Width = monthSize.Width;
                    this.MonthNameAreas[m].Offset(xMonth, yMonth);

                    this.YearAreas[m] = this.MonthNameAreas[m];
                    this.YearAreas[m].X += monthSize.Width;
                    this.YearAreas[m].Width = titleSize.Width - monthSize.Width;
                }
            }

            g.Dispose();
        }

        // ---------------------------------------------------------------------
        private void DrawMonth(
            Graphics graphics,
            DateTime month,
            PointF start,
            string[] weekdays,
            Font boldFont,
            Brush textBrush,
            Brush backBrush,
            Brush titleTextBrush,
            Brush titleBackBrush,
            Brush weekdayTextBrush,
            Brush weekdayBackBrush,
            Brush trailingTextBrush,
            Brush highlightDayTextBrush,
            Brush weeknumberTextBrush,
            Brush weeknumberBackBrush,
            Trailing drawTrailing,
            Pen divider,
            float[] cellMiddles)
        {
            DrawTitle(
                graphics,
                month.ToString("MMMM, yyyy", CultureInfo.CurrentCulture),
                start,
                this.titleRectangle,
                boldFont,
                titleTextBrush,
                titleBackBrush);

            DrawWeekNames(
                graphics,
                weekdays,
                start,
                this.weekdaysRectangle,
                this.Font,
                weekdayTextBrush,
                weekdayBackBrush,
                divider,
                cellMiddles);

            DrawDays(
                graphics,
                month,
                start,
                this.daysRectangle,
                this.Font,
                boldFont,
                textBrush,
                trailingTextBrush,
                highlightDayTextBrush,
                drawTrailing,
                this.HighlightDayTextColor,
                cellMiddles);

            if (this.ShowWeekNumbers == true)
            {
                DrawWeekNumbers(
                    graphics,
                    month,
                    start,
                    this.weekNumbersRectangle,
                    this.Font,
                    weeknumberTextBrush,
                    weeknumberBackBrush,
                    divider);
            }
        }

        // ---------------------------------------------------------------------
        private void DrawTitle(Graphics graphics, string title, PointF start,
            RectangleF rectangle, Font font, Brush textBrush, Brush backBrush)
        {
            rectangle.Offset(start);
            graphics.FillRectangle(backBrush, rectangle);

            StringFormat titleFormat = new StringFormat();
            titleFormat.Alignment = StringAlignment.Center;
            titleFormat.LineAlignment = StringAlignment.Center;

            graphics.DrawString(title, font, textBrush, rectangle,
                titleFormat);
        }

        // ---------------------------------------------------------------------
        private void DrawWeekNames(
            Graphics graphics,
            string[] weekdays,
            PointF start,
            RectangleF rectangle,
            Font font,
            Brush textBrush,
            Brush backBrush,
            Pen divider,
            float[] cellMiddles)
        {
            rectangle.Offset(start);
            graphics.FillRectangle(backBrush, rectangle);
            StringFormat weekdayFormat = new StringFormat();
            weekdayFormat.Alignment = StringAlignment.Center;

            for (int w = 0; w < weekdays.Length; ++w)
            {
                int dayOffset = (w + (int)this.FirstDayOfWeek) % 7;

                graphics.DrawString(weekdays[dayOffset], font, textBrush,
                    rectangle.X + cellMiddles[w], rectangle.Top, weekdayFormat);
            }

            float margin = this.cellSize.Width * 0.14F;
            float verticalOffset = rectangle.Bottom - 2;

            graphics.DrawLine(divider, rectangle.X + margin,
                verticalOffset, rectangle.Right - margin, verticalOffset);
        }

        // ---------------------------------------------------------------------
        private void DrawDays(
            Graphics graphics,
            DateTime month,
            PointF start,
            RectangleF rectangle,
            Font font,
            Font boldFont,
            Brush textBrush,
            Brush trailingTextBrush,
            Brush highlightDayTextBrush,
            Trailing drawTrailing,
            Color todayColor,
            float[] cellMiddles)
        {
            rectangle.Offset(start);
            StringFormat weekdayFormat = new StringFormat();
            weekdayFormat.Alignment = StringAlignment.Center;

            if (this.gridlines == true)
            {
                DrawGridLines(graphics, rectangle);
            }

            int daysInMonth = GetDaysInMonth(month.Year, month.Month);
            DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            float verticalOffset = rectangle.Y;
            int weeksDrawn = 0;

            int dayOffset =
                ((int)firstDayOfMonth.DayOfWeek + 7 - (int)this.FirstDayOfWeek) % 7;

            if (drawTrailing == Trailing.First || drawTrailing == Trailing.Both)
            {
                // Gray days...
                DateTime previousMonth = month.AddMonths(-1);

                int daysInPreviousMonth =
                    GetDaysInMonth(previousMonth.Year, previousMonth.Month);

                for (int day = daysInPreviousMonth - dayOffset + 1; day <= daysInPreviousMonth; ++day)
                {
                    graphics.DrawString(day.ToString(), font, trailingTextBrush,
                        rectangle.X + cellMiddles[dayOffset - 1 - (daysInPreviousMonth - day)],
                        verticalOffset, weekdayFormat);
                }
            }

            // Regular days...
            for (int day = 1; day <= daysInMonth; ++day)
            {
                DateTime date = firstDayOfMonth.AddDays(day - 1);

                if (this.showTodayCircle && date.Date == this.TodayDate.Date)
                {
                    float width = this.cellSize.Width;
                    Pen todayPen = new Pen(todayColor);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    graphics.DrawEllipse(todayPen,
                        rectangle.X + cellMiddles[dayOffset] - (width / 2),
                        verticalOffset - 1,
                        width,
                        this.cellSize.Height);

                    todayPen.Dispose();
                    graphics.SmoothingMode = SmoothingMode.None;
                }

                Font dayFont = font;

                if (this.boldedDates != null && this.boldedDates != null &&
                    this.boldedDays.Contains(date) == true)
                {
                    dayFont = boldFont;
                }

                Brush dayBrush = textBrush;

                if (this.coloredDates != null && this.coloredDays != null &&
                    this.coloredDays.Contains(date) == true)
                {
                    dayBrush = highlightDayTextBrush;
                }

                graphics.DrawString(day.ToString(), dayFont, dayBrush,
                    rectangle.X + cellMiddles[dayOffset],
                    verticalOffset, weekdayFormat);

                dayOffset += 1;
                dayOffset %= 7;

                if (dayOffset == 0)
                {
                    verticalOffset += this.cellSize.Height;
                    weeksDrawn += 1;
                }
            }

            // Gray days again...

            if (drawTrailing == Trailing.Last || drawTrailing == Trailing.Both)
            {
                DateTime nextMonth = month.AddMonths(-1);

                int daysInNextMonth =
                    GetDaysInMonth(nextMonth.Year, nextMonth.Month);

                for (int day = 1; day <= daysInNextMonth; ++day)
                {
                    graphics.DrawString(day.ToString(), font, trailingTextBrush,
                        rectangle.X + cellMiddles[dayOffset],
                        verticalOffset, weekdayFormat);

                    dayOffset += 1;
                    dayOffset %= 7;

                    if (dayOffset == 0)
                    {
                        verticalOffset += this.cellSize.Height;
                        weeksDrawn += 1;
                    }

                    if (weeksDrawn > 5)
                    {
                        break;
                    }
                }
            }
        }

        // ---------------------------------------------------------------------
        private void DrawGridLines(Graphics graphics, RectangleF rectangle)
        {
            Pen pen = new Pen(Color.Gray);

            float offset = rectangle.Y + this.cellSize.Height - 1;

            for (int i = 0; i < 5; ++i)
            {
                graphics.DrawLine(pen, rectangle.Left, offset,
                    rectangle.Right, offset);

                offset += this.cellSize.Height;
            }

            offset = rectangle.Left + this.cellSize.Width - 1;

            for (int i = 0; i < 6; ++i)
            {
                graphics.DrawLine(pen, offset, rectangle.Top,
                    offset, rectangle.Bottom - 1);

                offset += this.cellSize.Width;
            }
        }

        // ---------------------------------------------------------------------
        private void DrawWeekNumbers(
            Graphics graphics,
            DateTime month,
            PointF start,
            RectangleF rectangle,
            Font font,
            Brush textBrush,
            Brush backBrush,
            Pen divider)
        {
            rectangle.Offset(start);
            graphics.FillRectangle(backBrush, rectangle);
            StringFormat weekdayFormat = new StringFormat();
            weekdayFormat.Alignment = StringAlignment.Center;

            float x = rectangle.X + rectangle.Width / 2;
            float y = rectangle.Top;

            DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            int daysInMonth = GetDaysInMonth(month.Year, month.Month);

            int dayOffset = ((int)firstDayOfMonth.DayOfWeek + 7 -
                (int)this.FirstDayOfWeek) % 7;

            DateTime date = firstDayOfMonth;
            int weeks = (daysInMonth + dayOffset + 7) / 7;
            System.Globalization.CultureInfo ci = new CultureInfo("en-US");

            for (int w = 0; w < weeks; ++w)
            {
                int weekOfYear = ci.Calendar.GetWeekOfYear(date,
                    System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                    this.FirstDayOfWeek);

                graphics.DrawString(
                    weekOfYear.ToString(CultureInfo.CurrentCulture),
                    font, textBrush, x, y, weekdayFormat);

                date = date.AddDays(7);
                y += this.cellSize.Height;
            }

            graphics.DrawLine(divider,
                rectangle.Right,
                rectangle.Top,
                rectangle.Right,
                rectangle.Top + (weeks * this.cellSize.Height));

        }

        // ---------------------------------------------------------------------
        private void MonthSize()
        {
            Graphics g = this.CreateGraphics();
            PointF point = new PointF(0, 0);
            StringFormat stringFormat = StringFormat.GenericTypographic;
            stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            // Width determined by longest label or number
            this.cellSize = g.MeasureString("00 ", this.Font, point,
                StringFormat.GenericTypographic);

            foreach (string day in AbbreviatedDayNames())
            {
                SizeF measured = g.MeasureString(day + " ", this.Font, point,
                    stringFormat);

                if (measured.Width > this.cellSize.Width)
                {
                    this.cellSize = measured;
                }
            }

            this.cellSize.Height = this.Font.GetHeight(g);
            this.cellSize.Height *= 1.2F;
            g.Dispose();

            this.cellSize += this.cellPadding;
            SizeF size = new SizeF(cellSize.Width * 7, cellSize.Height * 9.7F);
            size += this.monthPadding;
            size += this.monthPadding;

            float weekNumbers = 0;

            if (this.ShowWeekNumbers == true)
            {
                weekNumbers = this.cellSize.Width;
                size.Width += weekNumbers;
            }

            // titleRectangle
            this.titleRectangle = new RectangleF(
                this.monthPadding.Width,
                this.monthPadding.Height,
                size.Width - (this.monthPadding.Width * 2),
                this.cellSize.Height * 1.5F);

            // weekdays
            this.weekdaysRectangle = new RectangleF(
                weekNumbers + this.monthPadding.Width,
                (1.6f * this.cellSize.Height) + this.monthPadding.Height,
                this.titleRectangle.Width - weekNumbers - this.monthPadding.Width,
                this.cellSize.Height);

            // days
            this.daysRectangle = new RectangleF(
                weekNumbers + this.monthPadding.Width,
                (2.6f * this.cellSize.Height) + this.monthPadding.Height,
                this.titleRectangle.Width, this.cellSize.Height * 6);

            // weeknumbers
            this.weekNumbersRectangle = new RectangleF(
                this.monthPadding.Width,
                this.daysRectangle.Top,
                this.cellSize.Width - 2, this.daysRectangle.Height);

            // today
            this.todayRectangle = new RectangleF(
                this.monthPadding.Width,
                this.daysRectangle.Bottom,
                this.titleRectangle.Width,
                this.cellSize.Height);

            // correct for truncation
            size += new SizeF(0.5F, 0.5F);
            this.singleMonthSize = size.ToSize();
        }

        // ---------------------------------------------------------------------
        public static int GetDaysInMonth(int year, int month)
        {
            System.Globalization.Calendar calendar =
                CultureInfo.CurrentCulture.DateTimeFormat.Calendar;

            return calendar.GetDaysInMonth(year, month);
        }

        // ---------------------------------------------------------------------
        private static DateTime FirstOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        // ---------------------------------------------------------------------
        private void PreviousButton_Click(object sender, System.EventArgs e)
        {
            this.firstMonth = this.firstMonth.AddMonths(-1);
            DateChange();
            this.Focus();
        }

        // ---------------------------------------------------------------------
        private void NextButton_Click(object sender, System.EventArgs e)
        {
            this.firstMonth = this.firstMonth.AddMonths(1);
            DateChange();
            this.Focus();
        }

        // ---------------------------------------------------------------------
        private void DateChange()
        {
            BuildRegions();
            this.Invalidate();
            UpdateYearMenu();
            DateRangeEventArgs e = new DateRangeEventArgs(this.firstMonth,
                this.firstMonth);

            OnDateChanged(e);
        }

        // ---------------------------------------------------------------------
        private void UpdateYearMenu()
        {
            this.yearMenu.MenuItems.Clear();

            for (int year = this.firstMonth.Year - 5;
                 year < this.firstMonth.Year + 5;
                 ++year)
            {
                this.yearMenu.MenuItems.Add(
                    year.ToString(CultureInfo.CurrentCulture),
                    new EventHandler(OnSelectYear));
            }
        }

        // ---------------------------------------------------------------------
        private Size Months()
        {
            int border = this.border.ToSize().Width;

            int width = Math.Max(1, (this.Width + border) /
                this.singleMonthSize.Width);

            return new Size(width,
                Math.Max(1, this.Height / this.SingleMonthSize.Height));
        }

        // ---------------------------------------------------------------------
        private void PositionButtons()
        {
            int width = (int)this.cellSize.Width;
            int height = (int)this.cellSize.Height;
            this.previousButton.Size = new Size(width / 2, height);
            this.nextButton.Size = this.previousButton.Size;
            int margin = (int)(this.cellSize.Width * .20F);

            float titleMiddle = this.titleRectangle.Height / 2.0F;
            int y = (int)(titleMiddle - (height * 0.5F) + 0.5F);

            this.previousButton.Location = new Point(margin, y);

            this.nextButton.Location = new Point(
                (int)(this.SingleMonthSize.Width * this.months.Width) -
                this.nextButton.Size.Width - margin, y);

            this.previousButton.Font = this.Font;
            this.nextButton.Font = this.Font;
        }

        // ---------------------------------------------------------------------
        public void GoToToday()
        {
            this.firstMonth = DateTime.Today;
            DateChange();
        }

        // ---------------------------------------------------------------------
        public void GoToDate(DateTime date)
        {
            this.firstMonth = date;
            DateChange();
        }

        // ---------------------------------------------------------------------
        static GraphicsPath RoundRect(float x, float y, float w, float h, float r)
        {
            GraphicsPath gp = new GraphicsPath();
            float x2 = x + w;
            float y2 = y + h;

            PointF[] points = new PointF[]
                {
                    new PointF(x + r, y),
                    new PointF(x2 - r, y),
                    new PointF(x2 - r, y + r),
                    new PointF(x2, y + r),
                    new PointF(x2, y2 - r),
                    new PointF(x2 - r, y2 - r),
                    new PointF(x2 - r, y2),
                    new PointF(x + r, y2),
                    new PointF(x + r, y2 - r),
                    new PointF(x, y2 - r),
                    new PointF(x, y + r),
                    new PointF(x + r, y + r),
                    new PointF(x + r, y)
                };

            gp.AddLines(points);
            gp.CloseFigure();

            return gp;
        }

        // =====================================================================
        public class HitTestInfoEx
        {
            private Point test;
            private DateTime date;

            // -----------------------------------------------------------------
            public HitTestInfoEx(Point test, DateTime date)
            {
                this.test = test;
                this.date = date;
            }

            // -----------------------------------------------------------------
            public DateTime Time
            {
                get { return date; }
            }

            // -----------------------------------------------------------------
            public Point Point
            {
                get { return test; }
            }
        }
    }
}