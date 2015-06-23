using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace BlueOnion
{
    public class MonthCalendarEx : Control
    {
        private Size months = new Size(1, 1);
        private DateTime today = DateTime.Today;
        private DateTime currentDate = DateTime.Today;
        private DateTime firstMonth = DateTime.Today;
        private DayOfWeek firstDayOfWeek = DayOfWeek.Sunday;
        private Hashtable boldedDays;
        private Hashtable coloredDays;
        private bool showToday = true;
        private bool showWeekNumbers;
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
        private readonly SizeF cellPadding = new Size(0, 0);
        private SizeF monthPadding = new Size(1, 1);
        private SizeF border = new SizeF(0, 0);

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
        private ContextMenu monthMenu;
        private ContextMenu yearMenu;
        private readonly Container components = null;

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

            Controls.Add(previousButton);
            Controls.Add(nextButton);

            SetStyle(ControlStyles.ResizeRedraw, true);

            foreach (var month in
                CultureInfo.CurrentCulture.DateTimeFormat.MonthNames)
            {
                if (month.Length > 0)
                {
                    monthMenu.MenuItems.Add(month,
                        OnSelectMonth);
                }
            }

            UpdateYearMenu();
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

                if (monthMenu != null)
                {
                    monthMenu.Dispose();
                    monthMenu = null;
                }

                if (monthMenu != null)
                {
                    yearMenu.Dispose();
                    yearMenu = null;
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
            this.previousButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
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
            this.nextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
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
            var point = PointToClient(MousePosition);

            if (displayTodayRectangle.Contains(point))
            {
                GoToToday();
                base.OnClick(e);
                return;
            }

            foreach (var test in MonthNameAreas)
            {
                if (test.Contains(point))
                {
                    monthMenu.Show(this, point);
                    base.OnClick(e);
                    return;
                }
            }

            foreach (var test in YearAreas)
            {
                if (test.Contains(point))
                {
                    yearMenu.Show(this, point);
                    base.OnClick(e);
                    return;
                }
            }

            base.OnClick(e);
        }

        // ---------------------------------------------------------------------
        protected void OnSelectMonth(object sender, EventArgs e)
        {
            firstMonth = new DateTime(
                firstMonth.Year, ((MenuItem) sender).Index + 1, 1);

            DateChange();
        }

        // ---------------------------------------------------------------------
        protected void OnSelectYear(object sender, EventArgs e)
        {
            firstMonth = new DateTime(
                firstMonth.Year + ((MenuItem) sender).Index - 5,
                firstMonth.Month, firstMonth.Day);

            DateChange();
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
            MonthSize();
            months = Months();
            BuildRegions();
            PositionButtons();
            base.OnFontChanged(e);
        }

        // ---------------------------------------------------------------------
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            firstMonth = firstMonth.AddMonths((e.Delta < 0) ? 1 : -1);
            DateChange();
        }

        // ---------------------------------------------------------------------
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (displayTodayRectangle.Contains(e.X, e.Y))
            {
                Cursor = Cursors.Hand;
                base.OnMouseMove(e);
                return;
            }

            foreach (var area in MonthNameAreas)
            {
                if (area.Contains(e.X, e.Y))
                {
                    Cursor = Cursors.Hand;
                    base.OnMouseMove(e);
                    return;
                }
            }

            foreach (var area in YearAreas)
            {
                if (area.Contains(e.X, e.Y))
                {
                    Cursor = Cursors.Hand;
                    base.OnMouseMove(e);
                    return;
                }
            }

            Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }

        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs pe)
        {
            var month = FirstOfMonth(firstMonth);
            var coordinates = border.ToPointF();

            Brush textBrush = new SolidBrush(ForeColor);
            Brush backBrush = new SolidBrush(BackColor);
            Brush titleTextBrush = new SolidBrush(TitleForeColor);
            Brush titleBackBrush = new SolidBrush(TitleBackColor);
            Brush weekdayTextBrush = new SolidBrush(WeekdayForeColor);
            Brush weekdayBackBrush = new SolidBrush(WeekdayBackColor);
            Brush trailingTextBrush = new SolidBrush(TrailingForeColor);
            Brush highlightDayTextBrush = new SolidBrush(HighlightDayTextColor);
            Brush weeknumberTextBrush = new SolidBrush(WeeknumberForeColor);
            Brush weeknumberBackBrush = new SolidBrush(WeeknumberBackColor);
            var boldFont = new Font(Font, FontStyle.Bold);
            var divider = new Pen(WeekdayBarColor);
            var weekdays = AbbreviatedDayNames();

            var cellMiddle = cellSize.Width*0.5F;
            var cellMiddles = new float[weekdays.Length];

            for (var w = 0; w < weekdays.Length; ++w)
            {
                cellMiddles[w] = cellMiddle;
                cellMiddle += cellSize.Width;
            }

            for (var y = 0; y < months.Height; ++y)
            {
                for (var x = 0; x < months.Width; ++x)
                {
                    var drawTrailing = Trailing.None;

                    if (months.Width*months.Height == 1)
                    {
                        drawTrailing = Trailing.Both;
                    }
                    else if (y == 0 && x == 0)
                    {
                        drawTrailing = Trailing.First;
                    }
                    else if (y == (months.Height - 1) &&
                        x == (months.Width - 1))
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

                coordinates.X = border.Width;
                coordinates.Y += SingleMonthSize.Height;
            }

            // Draw today string at bottom of calendar
            if (ShowToday)
            {
                displayTodayRectangle = todayRectangle;

                displayTodayRectangle.Offset(0,
                    (months.Height - 1)*SingleMonthSize.Height);

                var todayFormat = new StringFormat();
                todayFormat.Alignment = StringAlignment.Center;
                todayFormat.LineAlignment = StringAlignment.Center;

                pe.Graphics.DrawString(
                    "Today: " + TodayDate.ToShortDateString(),
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
                    if (e.Control && e.Shift == false && e.Alt == false)
                    {
                        firstMonth = firstMonth.AddYears(1);
                    }
                    else
                    {
                        firstMonth = firstMonth.AddMonths(1);
                    }

                    DateChange();
                    break;

                case Keys.PageDown:
                    if (e.Control && e.Shift == false && e.Alt == false)
                    {
                        firstMonth = firstMonth.AddYears(-1);
                    }
                    else
                    {
                        firstMonth = firstMonth.AddMonths(-1);
                    }

                    DateChange();
                    break;

                case Keys.Home:
                    GoToToday();
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
            var months = Months();

            if (this.months != months || CalendarAreas == null)
            {
                this.months = months;
                PositionButtons();
                BuildRegions();
            }

            base.OnSizeChanged(e);
        }

        // ---------------------------------------------------------------------
        public DateTime[] BoldedDates { get; set; }

        // ---------------------------------------------------------------------
        public DateTime[] ColoredDates { get; set; }

        // ---------------------------------------------------------------------
        public DayOfWeek FirstDayOfWeek
        {
            get { return firstDayOfWeek; }

            set
            {
                if (FirstDayOfWeek != value)
                {
                    firstDayOfWeek = value;
                    BuildRegions();
                    Invalidate();
                }
            }
        }

        // ---------------------------------------------------------------------
        public SelectionRange GetDisplayRange()
        {
            var start = FirstOfMonth(firstMonth);
            var end = start.AddMonths(months.Width*months.Height).AddDays(-1);
            return new SelectionRange(start, end);
        }

        // ---------------------------------------------------------------------
        public bool Gridlines
        {
            get { return gridlines; }

            set
            {
                gridlines = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public HitTestInfoEx HitTest(Point point)
        {
            var totalMonths = months.Width*months.Height;

            for (var m = 0; m < totalMonths; ++m)
            {
                if (CalendarAreas[m].days.Contains(point))
                {
                    var x = (int) ((point.X - CalendarAreas[m].days.Left)/
                        cellSize.Width) + 1;

                    var y = (int) ((point.Y - CalendarAreas[m].days.Top)/
                        cellSize.Height);

                    var time = CalendarAreas[m].date.AddDays
                        ((x + (y*7)) - CalendarAreas[m].dayOffset - 1);

                    if (time.Month != CalendarAreas[m].date.Month)
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
            currentDate = date;
        }

        // ---------------------------------------------------------------------
        public bool ShowToday
        {
            get { return showToday; }

            set
            {
                showToday = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public bool ShowTodayCircle
        {
            get { return showTodayCircle; }

            set
            {
                showTodayCircle = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public bool ShowWeekNumbers
        {
            get { return showWeekNumbers; }

            set
            {
                if (showWeekNumbers != value)
                {
                    showWeekNumbers = value;
                    MonthSize();
                    BuildRegions();
                    PositionButtons();
                    Invalidate();
                }
            }
        }

        // ---------------------------------------------------------------------
        [Browsable(false)]
        public Size SingleMonthSize { get; private set; }

        // ---------------------------------------------------------------------
        public virtual Color TitleForeColor
        {
            get { return titleForeColor; }

            set
            {
                titleForeColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color TitleBackColor
        {
            get { return titleBackColor; }

            set
            {
                titleBackColor = value;
                previousButton.BackColor = value;
                nextButton.BackColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeekdayForeColor
        {
            get { return weekdayForeColor; }

            set
            {
                weekdayForeColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeekdayBackColor
        {
            get { return weekdayBackColor; }

            set
            {
                weekdayBackColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeekdayBarColor
        {
            get { return weekdayBarColor; }

            set
            {
                weekdayBarColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeeknumberForeColor
        {
            get { return weeknumberForeColor; }

            set
            {
                weeknumberForeColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color WeeknumberBackColor
        {
            get { return weeknumberBackColor; }

            set
            {
                weeknumberBackColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color TrailingForeColor
        {
            get { return trailingForeColor; }

            set
            {
                trailingForeColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color HighlightDayTextColor
        {
            get { return highlightDayTextColor; }

            set
            {
                highlightDayTextColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        public virtual Color GridlinesColor
        {
            get { return gridlinesColor; }

            set
            {
                gridlinesColor = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        [Browsable(false)]
        public DateTime TodayDate
        {
            get { return today; }

            set
            {
                today = value;
                Invalidate();
            }
        }

        // ---------------------------------------------------------------------
        [Browsable(false)]
        public int TodayHeight
        {
            get { return (int) todayRectangle.Height; }
        }

        // ---------------------------------------------------------------------
        public void UpdateBoldedDates()
        {
            boldedDays = new Hashtable();

            if (BoldedDates != null)
            {
                foreach (var date in BoldedDates)
                {
                    if (boldedDays.Contains(date.Date) == false)
                    {
                        boldedDays.Add(date.Date, null);
                    }
                }
            }

            coloredDays = new Hashtable();

            if (ColoredDates != null)
            {
                foreach (var date in ColoredDates)
                {
                    if (coloredDays.Contains(date.Date) == false)
                    {
                        coloredDays.Add(date.Date, null);
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
            var totalMonths = months.Width*months.Height;
            CalendarAreas = new CalendarArea[totalMonths];
            MonthNameAreas = new RectangleF[totalMonths];
            YearAreas = new RectangleF[totalMonths];
            var g = CreateGraphics();

            var titleFormat = new StringFormat();
            titleFormat.Alignment = StringAlignment.Center;
            titleFormat.LineAlignment = StringAlignment.Center;

            var days = daysRectangle;
            var m = 0;

            for (var y = 0; y < months.Height; ++y)
            {
                for (var x = 0; x < months.Width; ++x, ++m)
                {
                    var date = FirstOfMonth(firstMonth.AddMonths(m));
                    CalendarAreas[m].date = date;

                    CalendarAreas[m].dayOffset =
                        ((int) CalendarAreas[m].date.DayOfWeek + 7 -
                            (int) FirstDayOfWeek)%7;

                    float xMonth = x*SingleMonthSize.Width;
                    float yMonth = y*SingleMonthSize.Height;
                    CalendarAreas[m].days = days;
                    CalendarAreas[m].days.Offset(xMonth, yMonth);

                    var titleSize = g.MeasureString(
                        date.ToString("MMMM, yyyy", CultureInfo.CurrentCulture),
                        Font, titleRectangle.Size, titleFormat);

                    var monthSize = g.MeasureString(
                        date.ToString("MMMM, ", CultureInfo.CurrentCulture),
                        Font);

                    MonthNameAreas[m] = titleRectangle;

                    MonthNameAreas[m].Inflate(
                        (titleSize.Width - titleRectangle.Width)/2,
                        (titleSize.Height - titleRectangle.Height)/2);

                    MonthNameAreas[m].Width = monthSize.Width;
                    MonthNameAreas[m].Offset(xMonth, yMonth);

                    YearAreas[m] = MonthNameAreas[m];
                    YearAreas[m].X += monthSize.Width;
                    YearAreas[m].Width = titleSize.Width - monthSize.Width;
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
                titleRectangle,
                boldFont,
                titleTextBrush,
                titleBackBrush);

            DrawWeekNames(
                graphics,
                weekdays,
                start,
                weekdaysRectangle,
                Font,
                weekdayTextBrush,
                weekdayBackBrush,
                divider,
                cellMiddles);

            DrawDays(
                graphics,
                month,
                start,
                daysRectangle,
                Font,
                boldFont,
                textBrush,
                trailingTextBrush,
                highlightDayTextBrush,
                drawTrailing,
                HighlightDayTextColor,
                cellMiddles);

            if (ShowWeekNumbers)
            {
                DrawWeekNumbers(
                    graphics,
                    month,
                    start,
                    weekNumbersRectangle,
                    Font,
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

            var titleFormat = new StringFormat();
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
            var weekdayFormat = new StringFormat();
            weekdayFormat.Alignment = StringAlignment.Center;

            for (var w = 0; w < weekdays.Length; ++w)
            {
                var dayOffset = (w + (int) FirstDayOfWeek)%7;

                graphics.DrawString(weekdays[dayOffset], font, textBrush,
                    rectangle.X + cellMiddles[w], rectangle.Top, weekdayFormat);
            }

            var margin = cellSize.Width*0.14F;
            var verticalOffset = rectangle.Bottom - 2;

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
            var weekdayFormat = new StringFormat();
            weekdayFormat.Alignment = StringAlignment.Center;

            if (gridlines)
            {
                DrawGridLines(graphics, rectangle);
            }

            var daysInMonth = GetDaysInMonth(month.Year, month.Month);
            var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            var verticalOffset = rectangle.Y;
            var weeksDrawn = 0;

            var dayOffset =
                ((int) firstDayOfMonth.DayOfWeek + 7 - (int) FirstDayOfWeek)%7;

            if (drawTrailing == Trailing.First || drawTrailing == Trailing.Both)
            {
                // Gray days...
                var previousMonth = month.AddMonths(-1);

                var daysInPreviousMonth =
                    GetDaysInMonth(previousMonth.Year, previousMonth.Month);

                for (var day = daysInPreviousMonth - dayOffset + 1; day <= daysInPreviousMonth; ++day)
                {
                    graphics.DrawString(day.ToString(), font, trailingTextBrush,
                        rectangle.X + cellMiddles[dayOffset - 1 - (daysInPreviousMonth - day)],
                        verticalOffset, weekdayFormat);
                }
            }

            // Regular days...
            for (var day = 1; day <= daysInMonth; ++day)
            {
                var date = firstDayOfMonth.AddDays(day - 1);

                if (showTodayCircle && date.Date == TodayDate.Date)
                {
                    var width = cellSize.Width;
                    var todayPen = new Pen(todayColor);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    graphics.DrawEllipse(todayPen,
                        rectangle.X + cellMiddles[dayOffset] - (width/2),
                        verticalOffset - 1,
                        width,
                        cellSize.Height);

                    todayPen.Dispose();
                    graphics.SmoothingMode = SmoothingMode.None;
                }

                var dayFont = font;

                if (BoldedDates != null && BoldedDates != null &&
                    boldedDays.Contains(date))
                {
                    dayFont = boldFont;
                }

                var dayBrush = textBrush;

                if (ColoredDates != null && coloredDays != null &&
                    coloredDays.Contains(date))
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
                    verticalOffset += cellSize.Height;
                    weeksDrawn += 1;
                }
            }

            // Gray days again...

            if (drawTrailing == Trailing.Last || drawTrailing == Trailing.Both)
            {
                var nextMonth = month.AddMonths(-1);

                var daysInNextMonth =
                    GetDaysInMonth(nextMonth.Year, nextMonth.Month);

                for (var day = 1; day <= daysInNextMonth; ++day)
                {
                    graphics.DrawString(day.ToString(), font, trailingTextBrush,
                        rectangle.X + cellMiddles[dayOffset],
                        verticalOffset, weekdayFormat);

                    dayOffset += 1;
                    dayOffset %= 7;

                    if (dayOffset == 0)
                    {
                        verticalOffset += cellSize.Height;
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
            var pen = new Pen(Color.Gray);

            var offset = rectangle.Y + cellSize.Height - 1;

            for (var i = 0; i < 5; ++i)
            {
                graphics.DrawLine(pen, rectangle.Left, offset,
                    rectangle.Right, offset);

                offset += cellSize.Height;
            }

            offset = rectangle.Left + cellSize.Width - 1;

            for (var i = 0; i < 6; ++i)
            {
                graphics.DrawLine(pen, offset, rectangle.Top,
                    offset, rectangle.Bottom - 1);

                offset += cellSize.Width;
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
            var weekdayFormat = new StringFormat();
            weekdayFormat.Alignment = StringAlignment.Center;

            var x = rectangle.X + rectangle.Width/2;
            var y = rectangle.Top;

            var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            var daysInMonth = GetDaysInMonth(month.Year, month.Month);

            var dayOffset = ((int) firstDayOfMonth.DayOfWeek + 7 -
                (int) FirstDayOfWeek)%7;

            var date = firstDayOfMonth;
            var weeks = (daysInMonth + dayOffset + 7)/7;
            var ci = new CultureInfo("en-US");

            for (var w = 0; w < weeks; ++w)
            {
                var weekOfYear = ci.Calendar.GetWeekOfYear(date,
                    CalendarWeekRule.FirstFourDayWeek,
                    FirstDayOfWeek);

                graphics.DrawString(
                    weekOfYear.ToString(CultureInfo.CurrentCulture),
                    font, textBrush, x, y, weekdayFormat);

                date = date.AddDays(7);
                y += cellSize.Height;
            }

            graphics.DrawLine(divider,
                rectangle.Right,
                rectangle.Top,
                rectangle.Right,
                rectangle.Top + (weeks*cellSize.Height));
        }

        // ---------------------------------------------------------------------
        private void MonthSize()
        {
            var g = CreateGraphics();
            var point = new PointF(0, 0);
            var stringFormat = StringFormat.GenericTypographic;
            stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            // Width determined by longest label or number
            cellSize = g.MeasureString("00 ", Font, point,
                StringFormat.GenericTypographic);

            foreach (var day in AbbreviatedDayNames())
            {
                var measured = g.MeasureString(day + " ", Font, point,
                    stringFormat);

                if (measured.Width > cellSize.Width)
                {
                    cellSize = measured;
                }
            }

            cellSize.Height = Font.GetHeight(g);
            cellSize.Height *= 1.2F;
            g.Dispose();

            cellSize += cellPadding;
            var size = new SizeF(cellSize.Width*7, cellSize.Height*9.7F);
            size += monthPadding;
            size += monthPadding;

            float weekNumbers = 0;

            if (ShowWeekNumbers)
            {
                weekNumbers = cellSize.Width;
                size.Width += weekNumbers;
            }

            // titleRectangle
            titleRectangle = new RectangleF(
                monthPadding.Width,
                monthPadding.Height,
                size.Width - (monthPadding.Width*2),
                cellSize.Height*1.5F);

            // weekdays
            weekdaysRectangle = new RectangleF(
                weekNumbers + monthPadding.Width,
                (1.6f*cellSize.Height) + monthPadding.Height,
                titleRectangle.Width - weekNumbers - monthPadding.Width,
                cellSize.Height);

            // days
            daysRectangle = new RectangleF(
                weekNumbers + monthPadding.Width,
                (2.6f*cellSize.Height) + monthPadding.Height,
                titleRectangle.Width, cellSize.Height*6);

            // weeknumbers
            weekNumbersRectangle = new RectangleF(
                monthPadding.Width,
                daysRectangle.Top,
                cellSize.Width - 2, daysRectangle.Height);

            // today
            todayRectangle = new RectangleF(
                monthPadding.Width,
                daysRectangle.Bottom,
                titleRectangle.Width,
                cellSize.Height);

            // correct for truncation
            size += new SizeF(0.5F, 0.5F);
            SingleMonthSize = size.ToSize();
        }

        // ---------------------------------------------------------------------
        public static int GetDaysInMonth(int year, int month)
        {
            var calendar =
                CultureInfo.CurrentCulture.DateTimeFormat.Calendar;

            return calendar.GetDaysInMonth(year, month);
        }

        // ---------------------------------------------------------------------
        private static DateTime FirstOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        // ---------------------------------------------------------------------
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            firstMonth = firstMonth.AddMonths(-1);
            DateChange();
            Focus();
        }

        // ---------------------------------------------------------------------
        private void NextButton_Click(object sender, EventArgs e)
        {
            firstMonth = firstMonth.AddMonths(1);
            DateChange();
            Focus();
        }

        // ---------------------------------------------------------------------
        private void DateChange()
        {
            BuildRegions();
            Invalidate();
            UpdateYearMenu();
            var e = new DateRangeEventArgs(firstMonth,
                firstMonth);

            OnDateChanged(e);
        }

        // ---------------------------------------------------------------------
        private void UpdateYearMenu()
        {
            yearMenu.MenuItems.Clear();

            for (var year = firstMonth.Year - 5;
                year < firstMonth.Year + 5;
                ++year)
            {
                yearMenu.MenuItems.Add(
                    year.ToString(CultureInfo.CurrentCulture),
                    OnSelectYear);
            }
        }

        // ---------------------------------------------------------------------
        private Size Months()
        {
            var border = this.border.ToSize().Width;

            var width = Math.Max(1, (Width + border)/
                SingleMonthSize.Width);

            return new Size(width,
                Math.Max(1, Height/SingleMonthSize.Height));
        }

        // ---------------------------------------------------------------------
        private void PositionButtons()
        {
            var width = (int) cellSize.Width;
            var height = (int) cellSize.Height;
            previousButton.Size = new Size(width/2, height);
            nextButton.Size = previousButton.Size;
            var margin = (int) (cellSize.Width*.20F);

            var titleMiddle = titleRectangle.Height/2.0F;
            var y = (int) (titleMiddle - (height*0.5F) + 0.5F);

            previousButton.Location = new Point(margin, y);

            nextButton.Location = new Point(
                SingleMonthSize.Width*months.Width -
                    nextButton.Size.Width - margin, y);

            previousButton.Font = Font;
            nextButton.Font = Font;
        }

        // ---------------------------------------------------------------------
        public void GoToToday()
        {
            firstMonth = DateTime.Today;
            DateChange();
        }

        // ---------------------------------------------------------------------
        public void GoToDate(DateTime date)
        {
            firstMonth = date;
            DateChange();
        }

        // ---------------------------------------------------------------------
        private static GraphicsPath RoundRect(float x, float y, float w, float h, float r)
        {
            var gp = new GraphicsPath();
            var x2 = x + w;
            var y2 = y + h;

            PointF[] points =
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
            // -----------------------------------------------------------------
            public HitTestInfoEx(Point test, DateTime date)
            {
                Point = test;
                Time = date;
            }

            // -----------------------------------------------------------------
            public DateTime Time { get; set;  }

            // -----------------------------------------------------------------
            public Point Point { get; set;  }
        }
    }
}