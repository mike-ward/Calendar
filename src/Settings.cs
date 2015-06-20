using System;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace BlueOnion
{
	/// <summary>
	/// Serializable class to save program state
	/// </summary>
	internal class Settings : ICloneable, IDisposable
	{
        private bool disposed;
        private const string topMostS = "top_most";
        private const string borderS = "border";
        private const string showWeekNumbersS = "show_week_numbers";
        private const string showTodayS = "show_today";
        private const string showTodayCircleS = "show_today_circle";
        private const string opacityS = "opacity";
        private const string positionS = "position";
        private const string firstDayS = "first_day";
        private const string fontS = "font";
        private const string colorBackS = "color_back";
        private const string colorForeS = "color_fore";
        private const string colorTitleBackS = "color_title_fore";
        private const string colorTitleForeS = "color_title_back";
        private const string colorTrailingForeS = "color_trailing_fore";
		private const string colorHighlightDayForeS = "color_highlight_day_fore";
		private const string colorGridlinesS = "color_gridlines";
		private const string trayIconS = "tray_icon";
		private const string gridlinesS = "gridlines";
		private const string colorWeekdayForeS = "color_weekday_fore";
		private const string colorWeekdayBackS = "color_weekday_back";
		private const string colorWeeknumberForeS = "color_weeknumber_fore";
		private const string colorWeeknumberBackS = "color_weeknumber_back";
		private const string colorWeekdayBarS = "color_weekday_bar";
		private const string startMonthJanuaryS = "start_month_january";
		private const string startMonthPreviousS = "start_month_previous";

        private bool topMost;
        private Calendar.BorderStyle border = Calendar.BorderStyle.Thick;
        private bool showWeekNumbers;
        private bool showToday = true;
        private bool showTodayCircle = true;
        private double opacity = 1.0;
        private Rectangle position = new Rectangle(10, 10, 0, 0);
        private System.DayOfWeek firstDay = System.DayOfWeek.Sunday;
        private Font font = new Font("Microsoft Sans Serif", 8.25f);
        private Color colorFore = SystemColors.WindowText;
        private Color colorBack = SystemColors.Window;
        private Color colorTitleFore = SystemColors.ActiveCaptionText;
        private Color colorTitleBack = SystemColors.ActiveCaption;
        private Color colorTrailingFore = SystemColors.AppWorkspace;
		private Color colorHighlightDayFore = Color.DarkRed;
		private Color colorGridlines = SystemColors.GrayText;
		private Color colorWeekdayFore = SystemColors.ActiveCaption;
		private Color colorWeekdayBack = SystemColors.ActiveCaptionText;
		private Color colorWeeknumberFore = SystemColors.ActiveCaption;
		private Color colorWeeknumberBack = SystemColors.ActiveCaptionText;
		private Color colorWeekdayBar = SystemColors.WindowText;
	    private bool trayIcon;
		private bool gridlines;
		private bool startMonthJanuary;
		private int  startMonthPrevious;

        // ---------------------------------------------------------------------
		public Settings()
		{
			startMonthPrevious = 0;
        }

        // ---------------------------------------------------------------------
        public bool Topmost
        {
            get { return topMost; }
            set { topMost = value; }
        }

        // ---------------------------------------------------------------------
        public Calendar.BorderStyle Border
        {
            get { return border; }
            set { border = value; }
        }

        // ---------------------------------------------------------------------
        public bool ShowWeekNumbers
        {
            get { return showWeekNumbers; }
            set { showWeekNumbers = value; }
        }

        // ---------------------------------------------------------------------
        public bool ShowToday
        {
            get { return showToday; }
            set { showToday = value; }
        }

        // ---------------------------------------------------------------------
        public bool ShowTodayCircle
        {
            get { return showTodayCircle; }
            set { showTodayCircle = value; }
        }

        // ---------------------------------------------------------------------
        public double Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        // ---------------------------------------------------------------------
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        // ---------------------------------------------------------------------
        public System.DayOfWeek FirstDay
        {
            get { return firstDay; }
            set { firstDay = value; }
        }

        // ---------------------------------------------------------------------
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        // ---------------------------------------------------------------------
        public Color ColorFore
        {
            get { return colorFore; }
            set { colorFore = value; }
        }

        // ---------------------------------------------------------------------
        public Color ColorBack
        {
            get { return colorBack; }
            set { colorBack = value; }
        }

        // ---------------------------------------------------------------------
        public Color ColorTitleFore
        {
            get { return colorTitleFore; }
            set { colorTitleFore = value; }
        }

        // ---------------------------------------------------------------------
        public Color ColorTitleBack
        {
            get { return colorTitleBack; }
            set { colorTitleBack = value; }
        }

        // ---------------------------------------------------------------------
        public Color ColorTrailingFore
        {
            get { return colorTrailingFore; }
            set { colorTrailingFore = value; }
        }

		// ---------------------------------------------------------------------
		public Color ColorHighlightDayFore
		{
			get { return colorHighlightDayFore; }
			set { colorHighlightDayFore = value; }
		}

		// ---------------------------------------------------------------------
		public Color ColorGridlines
		{
			get { return colorGridlines; }
			set { colorGridlines = value; }
		}

		// ---------------------------------------------------------------------
		public Color ColorWeekdayFore
		{
			get { return colorWeekdayFore; }
			set { colorWeekdayFore = value; }
		}

		// ---------------------------------------------------------------------
		public Color ColorWeekdayBack
		{
			get { return colorWeekdayBack; }
			set { colorWeekdayBack = value; }
		}

		// ---------------------------------------------------------------------
		public Color ColorWeeknumberFore
		{
			get { return colorWeeknumberFore; }
			set { colorWeeknumberFore = value; }
		}

		// ---------------------------------------------------------------------
		public Color ColorWeeknumberBack
		{
			get { return colorWeeknumberBack; }
			set { colorWeeknumberBack = value; }
		}

		// ---------------------------------------------------------------------
		public Color ColorWeekdayBar
		{
			get { return colorWeekdayBar; }
			set { colorWeekdayBar = value; }
		}

		// ---------------------------------------------------------------------
        public bool TrayIcon
        {
            get { return trayIcon; }
            set { trayIcon = value; }
        }

		// ---------------------------------------------------------------------
		public bool Gridlines
		{
			get { return this.gridlines;  }
			set { this.gridlines = value; }
		}

		// ---------------------------------------------------------------------
		public bool StartMonthJanuary
		{
			get { return this.startMonthJanuary;  }
			set { this.startMonthJanuary = value; }
		}

		// ---------------------------------------------------------------------
		public int StartMonthPrevious
		{
			get { return this.startMonthPrevious;  }
			set { this.startMonthPrevious = value; }
		}

		// ---------------------------------------------------------------------
        public object Clone()
        {
            // Shallow copy works since everything is a value type
            return this.MemberwiseClone();
        }

        // ---------------------------------------------------------------------
        public override int GetHashCode()
        {
			int counter = 10000;

            return (this.topMost.GetHashCode() + counter++)
                 ^ (this.border.GetHashCode() + counter++)
                 ^ (this.showWeekNumbers.GetHashCode() + counter++)
                 ^ (this.showToday.GetHashCode() + counter++)
                 ^ (this.showTodayCircle.GetHashCode() + counter++)
                 ^ this.opacity.GetHashCode()
                 ^ this.position.GetHashCode()
                 ^ this.firstDay.GetHashCode()
                 ^ this.font.GetHashCode()
                 ^ this.colorFore.GetHashCode()
                 ^ this.colorBack.GetHashCode()
                 ^ this.colorTitleFore.GetHashCode()
                 ^ this.colorTitleBack.GetHashCode()
                 ^ this.colorTrailingFore.GetHashCode()
				 ^ this.colorHighlightDayFore.GetHashCode()
				 ^ this.colorGridlines.GetHashCode()
				 ^ this.colorWeekdayFore.GetHashCode()
				 ^ this.colorWeekdayBack.GetHashCode()
				 ^ this.colorWeeknumberFore.GetHashCode()
				 ^ this.colorWeeknumberBack.GetHashCode()
				 ^ this.colorWeekdayBar.GetHashCode()
				 ^ (this.trayIcon.GetHashCode() + counter++)
				 ^ (this.gridlines.GetHashCode() + counter++)
				 ^ (this.startMonthJanuary.GetHashCode() + counter++)
				 ^ this.StartMonthPrevious.GetHashCode();
        }

        // ---------------------------------------------------------------------
        public override bool Equals(object obj)
        {
			if (obj == null)
			{
				return false;
			}

			if ((obj is Settings) == false)
			{
				return false;
			}

			Settings objSettings = (Settings)obj;
            return this.GetHashCode() == objSettings.GetHashCode();
        }

        // ---------------------------------------------------------------------
        public static bool operator == (Settings lhs, Settings rightHandOperand)
        {
            return lhs.Equals(rightHandOperand);
        }

        // ---------------------------------------------------------------------
        public static bool operator != (Settings lhs, Settings rightHandOperand)
        {
            return !lhs.Equals(rightHandOperand);
        }

        // ---------------------------------------------------------------------
        public void Serialize(TextWriter textWriter)
        {
            textWriter.WriteLine(topMostS + "=" + topMost.ToString());
            textWriter.WriteLine(borderS + "=" + border.ToString());
            textWriter.WriteLine(showWeekNumbersS + "=" + showWeekNumbers.ToString());
            textWriter.WriteLine(showTodayS + "=" + showToday.ToString());
            textWriter.WriteLine(showTodayCircleS + "=" + showTodayCircle.ToString());
            textWriter.WriteLine(opacityS + "=" + opacity.ToString(CultureInfo.InvariantCulture));
            textWriter.WriteLine(positionS + "=" + position.ToString());
            textWriter.WriteLine(firstDayS + "=" + firstDay.ToString());
            textWriter.WriteLine(fontS + "=" + Settings.FontToString(font));
            textWriter.WriteLine(colorForeS + "=" + colorFore.ToArgb().ToString(CultureInfo.InvariantCulture));
            textWriter.WriteLine(colorBackS + "=" + colorBack.ToArgb().ToString(CultureInfo.InvariantCulture));
            textWriter.WriteLine(colorTitleForeS + "=" + colorTitleFore.ToArgb().ToString(CultureInfo.InvariantCulture));
            textWriter.WriteLine(colorTitleBackS + "=" + colorTitleBack.ToArgb().ToString(CultureInfo.InvariantCulture));
            textWriter.WriteLine(colorTrailingForeS + "=" + colorTrailingFore.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorHighlightDayForeS + "=" + colorHighlightDayFore.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorGridlinesS + "=" + colorGridlines.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorWeekdayForeS + "=" + colorWeekdayFore.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorWeekdayBackS + "=" + colorWeekdayBack.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorWeeknumberForeS + "=" + colorWeeknumberFore.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorWeeknumberBackS + "=" + colorWeeknumberBack.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(colorWeekdayBarS + "=" + colorWeekdayBar.ToArgb().ToString(CultureInfo.InvariantCulture));
			textWriter.WriteLine(trayIconS + "=" + trayIcon.ToString());
			textWriter.WriteLine(gridlinesS + "=" + gridlines.ToString());
			textWriter.WriteLine(startMonthJanuaryS + "=" + startMonthJanuary.ToString());
			textWriter.WriteLine(startMonthPreviousS + "=" + startMonthPrevious.ToString(CultureInfo.InvariantCulture));
        }

        // ---------------------------------------------------------------------
        public static Settings Deserialize(TextReader textReader)
        {
            string line;
            char[] delimiter = {'='};
            Settings settings = new Settings();

            while ((line = textReader.ReadLine()) != null)
            {
                string[] nameValue = line.Split(delimiter, 2);

                switch (nameValue[0])
                {
                    case topMostS:
                        settings.Topmost = bool.Parse(nameValue[1]);
                        break;

                    case borderS:
						try
						{
							settings.Border = (Calendar.BorderStyle)Enum.Parse
								(typeof(Calendar.BorderStyle), nameValue[1], true);
						}

						catch
						{
							settings.Border = Calendar.BorderStyle.Thick;
						}
                        break;

                    case showWeekNumbersS:
                        settings.ShowWeekNumbers = bool.Parse(nameValue[1]);
                        break;

                    case showTodayS:
                        settings.ShowToday = bool.Parse(nameValue[1]);
                        break;

                    case showTodayCircleS:
                        settings.ShowTodayCircle = bool.Parse(nameValue[1]);
                        break;

                    case opacityS:
                        settings.opacity = float.Parse(nameValue[1], 
                            NumberStyles.Float, 
                            CultureInfo.InvariantCulture);
                        break;

                    case positionS:
                        char[] delimiters = {'=', ',', '}'};
                        string[] coords = nameValue[1].Split(delimiters);
                        settings.position.X = EventsCollection.ToInt(coords[1]);
                        settings.position.Y = EventsCollection.ToInt(coords[3]);
                        settings.position.Width = EventsCollection.ToInt(coords[5]);
                        settings.position.Height = EventsCollection.ToInt(coords[7]);
                        break;

                    case firstDayS:
                        settings.FirstDay = (System.DayOfWeek)
                            Enum.Parse(typeof(System.DayOfWeek), nameValue[1]);
                        break;

                    case fontS:
                        settings.font = Settings.StringToFont(nameValue[1]);
                        break;

                    case colorForeS:
                        settings.colorFore = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
                        break;

                    case colorBackS:
                        settings.colorBack = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
                        break;

                    case colorTitleForeS:
                        settings.colorTitleFore = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
                        break;

                    case colorTitleBackS:
                        settings.colorTitleBack = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
                        break;

                    case colorTrailingForeS:
                        settings.colorTrailingFore = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
                        break;

					case colorHighlightDayForeS:
						settings.colorHighlightDayFore = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case colorGridlinesS:
						settings.colorGridlines = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case colorWeekdayForeS:
						settings.ColorWeekdayFore = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case colorWeekdayBackS:
						settings.ColorWeekdayBack = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case colorWeeknumberForeS:
						settings.ColorWeeknumberFore = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case colorWeeknumberBackS:
						settings.ColorWeeknumberBack = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case colorWeekdayBarS:
						settings.ColorWeekdayBar = Color.FromArgb(EventsCollection.ToInt(nameValue[1]));
						break;

					case trayIconS:
                        settings.trayIcon = bool.Parse(nameValue[1]);
                        break;

					case gridlinesS:
						settings.gridlines = bool.Parse(nameValue[1]);
						break;

					case startMonthJanuaryS:
						settings.startMonthJanuary = bool.Parse(nameValue[1]);
						break;

					case startMonthPreviousS:
						settings.StartMonthPrevious = int.Parse(nameValue[1], CultureInfo.InvariantCulture);
						break;
				}
            }

            return settings;
        }

        // ---------------------------------------------------------------------
        private static Font StringToFont(string font_string)
        {
            string[] font = font_string.Split(',');
            
            if (font.Length >= 3)
            {
                return new Font(font[0], 
                    Convert.ToSingle(font[1], CultureInfo.InvariantCulture), 
                    (FontStyle)Enum.Parse(typeof(FontStyle), font[2]));
            }

            if (font.Length == 2)
            {
                return new Font(font[0], 
                    Convert.ToSingle(font[1], CultureInfo.InvariantCulture));
            }
        
            return new Font("Microsoft Sans Serif",  8.25f);        
        }

        // ---------------------------------------------------------------------
        public static string FontToString(Font font)
        {
            string fontString = font.Name + ", " + 
                font.SizeInPoints.ToString(CultureInfo.InvariantCulture) + 
                ", " + font.Style;
                 
            return fontString;
        }

        #region IDisposable Members

        // ---------------------------------------------------------------------
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ---------------------------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed == false)
            {
				this.disposed = true;

				if (disposing == true)
                {
                    this.font.Dispose();
                }
            }
        }

        // ---------------------------------------------------------------------
        ~Settings()
        {
            Dispose(false);
        }

        #endregion
    }
}
