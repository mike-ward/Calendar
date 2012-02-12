// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;
using System.IO;
using System.Globalization;

namespace BlueOnion
{
    // -------------------------------------------------------------------------
    public enum EventType
    {
        Recurring=0, Movable=1, Special=2
    }

    // -------------------------------------------------------------------------
    public class Event : System.ICloneable
    {
        private EventType eventType = EventType.Recurring;
        private int day;        // 1-31
        private int month;      // 1-12
        private int year;       // 4 digit
        private int week = 1;   // 1-5
        private int days;       // -365 to 365
        private DayOfWeek weekDay = DayOfWeek.Sunday;
        private SpecialEvent special = SpecialEvent.Spring;
        private RecurringEvent recurring = RecurringEvent.Annually;
        private string description;
		private bool highlightColor;

		public static readonly string Version1 = "V1";
		public static readonly string Version2 = "V2";

        static private string[] weekNumbers = 
            new string[7] {"1st", "2nd", "3rd", "4th", "Last", "1st full", "Last full"};

        // ---------------------------------------------------------------------
        public Event()
        {
            DateTime today = DateTime.Today;

            this.Day = today.Day;
            this.Month = today.Month;
            this.Year = today.Year;
        }

        // ---------------------------------------------------------------------
        public EventType EventType
        {
            get 
            { 
                return (Enum.IsDefined(typeof(EventType), eventType) == true)
                    ? eventType
                    : EventType.Recurring; 
            }

            set { eventType = value; }
        }

        // ---------------------------------------------------------------------
        public int Month
        {
            get 
			{ 
				return this.month;
			}

            set 
			{ 
				if (value < 1 || value > 
					CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(this.Year))
				{
					throw new System.ArgumentOutOfRangeException("Month", value, 
						"Value outside of current culture calendar range");
				}

				this.month = value;
			}
        }

        // ---------------------------------------------------------------------
        public int Day
        {
            get { return (day > 0 && day < 32) ? day : 1; }
            set { day = value; }
        }

        // ---------------------------------------------------------------------
        public int Week
        {
            get { return (week >= 1 && week <= weekNumbers.Length) ? week : 1; }
            set { week = value; }
        }

        // ---------------------------------------------------------------------
        public int Year
        {
            get 
            { 
                return (year > 0 && year < 9999) 
                    ? year 
                    : DateTime.Today.Year; 
            }

            set { year = value; }
        }

        // ---------------------------------------------------------------------
        public DayOfWeek Weekday
        {
            get 
            { 
                return (Enum.IsDefined(typeof(DayOfWeek), weekDay) == true)
                    ? weekDay
                    : DayOfWeek.Sunday;
            }

            set { weekDay = value; }
        }

        // ---------------------------------------------------------------------
        public int Days
        {
            get { return (days >= -365 && days <= 365) ? days : 0; }
            set { days = value; }
        }

        // ---------------------------------------------------------------------
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // ---------------------------------------------------------------------
        public SpecialEvent Special
        {
            get 
            { 
                return 
                    (Enum.IsDefined(typeof(SpecialEvent), special) == true) 
                    ? special 
                    : SpecialEvent.Spring;               
            }

            set { special = value; }
        }

        // ---------------------------------------------------------------------
        public RecurringEvent Recurring
        {
            get 
            { 
                return
                    (Enum.IsDefined(typeof(RecurringEvent), recurring) == true)
                    ? recurring
                    : RecurringEvent.Annually;
            }

            set { recurring = value; }
        }

		// ---------------------------------------------------------------------
		public bool HighlightColor
		{
			get { return this.highlightColor; }
			set { this.highlightColor = value; }
		}

		// ---------------------------------------------------------------------
        public static string[] WeekNumbers()
        {
            return Event.weekNumbers;
        }

        // ---------------------------------------------------------------------
        public void Serialize(TextWriter textWriter)
        {
            textWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                Enum.Format(typeof(EventType), this.EventType, "d"),
                Month.ToString(CultureInfo.InvariantCulture),
                Day.ToString(CultureInfo.InvariantCulture),
                Year.ToString(CultureInfo.InvariantCulture),
                Week.ToString(CultureInfo.InvariantCulture),
                Days.ToString(CultureInfo.InvariantCulture),
                Enum.Format(typeof(DayOfWeek), this.Weekday, "d"),
                Enum.Format(typeof(SpecialEvent), this.Special, "d"),
                Enum.Format(typeof(RecurringEvent), this.Recurring, "d"),
				HighlightColor.ToString(CultureInfo.InvariantCulture),
                Description);
        }

        // ---------------------------------------------------------------------
        public static Event Deserialize(TextReader textReader, string version)
        {
			string text = textReader.ReadLine();

			if (text == null)
			{
				return null; // end of the stream, baby...
			}

			Event theEvent = null;

			try
			{
				char[] delimiter = {','};
				int length = (version == Event.Version1) ? 10 : 11;
				string[] eventParts = text.Split(delimiter, length);

				if (eventParts.Length != length)
				{
                    Log.Error("Event.Deserialize: version and length to not match");
					return null;
				}

				theEvent = new Event();

				theEvent.EventType = (EventType)
					Enum.Parse(typeof(EventType), eventParts[0]);

				theEvent.Month = Convert.ToInt32(eventParts[1], 
					CultureInfo.InvariantCulture);

				theEvent.Day = Convert.ToInt32(eventParts[2], 
					CultureInfo.InvariantCulture);

				theEvent.Year = Convert.ToInt32(eventParts[3], 
					CultureInfo.InvariantCulture);

				theEvent.Week = Convert.ToInt32(eventParts[4], 
					CultureInfo.InvariantCulture);

				theEvent.Days = Convert.ToInt32(eventParts[5],
					CultureInfo.InvariantCulture);

				theEvent.Weekday = (DayOfWeek)
					Enum.Parse(typeof(DayOfWeek), eventParts[6]);

				theEvent.Special = (SpecialEvent)
					Enum.Parse(typeof(SpecialEvent), eventParts[7]);

				theEvent.Recurring = (RecurringEvent)
					Enum.Parse(typeof(RecurringEvent), eventParts[8]);

				if (version == Event.Version1)
				{
					theEvent.Description = eventParts[9];
				}

				else if (version == Event.Version2)
				{
					theEvent.HighlightColor = Convert.ToBoolean(eventParts[9], 
						CultureInfo.InvariantCulture);

					theEvent.Description = eventParts[10];
				}
			}

			catch (System.Exception e)
			{
				Log.Error("Event.Deserialize exception caught" + Environment.NewLine + e.ToString());
			}

			return theEvent;
        }

        // ---------------------------------------------------------------------
        public override string ToString()
        {
            string date = string.Empty;
            string recurring = string.Empty;

            switch (this.EventType)
            {
                case EventType.Recurring:
                    int year = (this.Year == 0) ? DateTime.Today.Year : this.Year;
                    DateTime d = new DateTime(year, this.Month, this.Day);
                    date = d.ToShortDateString();

                    if (this.Recurring != RecurringEvent.Annually)
                    {
                        recurring = "(" + RecurringEventNameValue.GetName(this.Recurring) + ")";
                    }

                    break;

                case EventType.Movable:
                    string week = 
                        (this.Week > 0 && this.Week <= Event.WeekNumbers().Length)
                        ? Event.WeekNumbers()[this.Week - 1]
                        : "Out of range";

                    date =  String.Format(CultureInfo.CurrentCulture, 
                        "{0} {1} {2}", 
                        week,
                        DateTimeFormatInfo.CurrentInfo.GetAbbreviatedDayName(this.Weekday),
                        Event.GetAbbreviatedMonthName(this.Month));

                    break;

                case EventType.Special:
                    date = SpecialEventNameValue.GetName(this.Special) + 
                        ((this.Days < 0) ? " - " : " + ") +
                        Math.Abs(this.Days).ToString(CultureInfo.CurrentCulture);

                    break;

                default:
                    break;
            }

            return String.Format(CultureInfo.CurrentCulture,
                "{0,-15}\t {1} {2}", date, this.Description, recurring);
        }

        // ---------------------------------------------------------------------
        public DateTime Date()
        {
            return this.Date(this.Year);
        }

        // ---------------------------------------------------------------------
        public DateTime Date(int year)
        {
            if (year <= 0)
            {
                year = DateTime.Today.Year;
            }

            DateTime date = DateTime.MinValue;

            switch (this.EventType)
            {
                case EventType.Recurring:
                    date = new DateTime(year, this.Month, this.Day);
                    break;

                case EventType.Movable:
                    int day = DateComputations.NthWeekday(this.Week, 
                        Convert.ToInt32(this.Weekday, CultureInfo.InvariantCulture), 
                        this.Month, year);

                    date = new DateTime(year, this.Month, day);
                    break;

                case EventType.Special:
                    switch (this.Special)
                    {
                        case SpecialEvent.Easter:
                            DateTime easter = DateComputations.Easter(year);
                            date = easter.AddDays(this.Days);
                            break;

                        case SpecialEvent.Advent:
                            DateTime advent = DateComputations.Advent(year);
                            date = advent.AddDays(this.Days);
                            break;

                        case SpecialEvent.Spring:
                            DateTime spring = DateComputations.EquinoxSolstice(year, Season.Spring);
                            date = spring.AddDays(this.Days);
                            break;

                        case SpecialEvent.Summer:
                            DateTime summer = DateComputations.EquinoxSolstice(year, Season.Summer);
                            date = summer.AddDays(this.Days);
                            break;

                        case SpecialEvent.Autumn:
                            DateTime autumn = DateComputations.EquinoxSolstice(year, Season.Autumn);
                            date = autumn.AddDays(this.Days);
                            break;

                        case SpecialEvent.Winter:
                            DateTime winter = DateComputations.EquinoxSolstice(year, Season.Winter);
                            date = winter.AddDays(this.Days);
                            break;

                        default:
                            break;
                    }
                    break;
            }

            return date;
        }

        // ---------------------------------------------------------------------
        public DateTime[] GetAllDates(int year)
        {
            if (year <= 0)
            {
                year = DateTime.Today.Year;
            }

            System.Collections.ArrayList dateList = 
                new System.Collections.ArrayList();

            if (this.EventType == EventType.Recurring)
            {
                switch (this.Recurring)
                {
                    case RecurringEvent.None:
                        if (year == this.year)
                        {
                            dateList.Add(new DateTime(year, this.Month, this.Day));
                        }
                        break;

                    case RecurringEvent.Annually:
                        dateList.Add(new DateTime(year, this.Month, this.Day));
                        break;

                    case RecurringEvent.Biweekly:
                        DateTime date = new DateTime(year, 1, 1);

                        if (date < this.Date())
                        {
                            date = this.Date();
                        }

                        TimeSpan timeSpan = this.Date() - date;
                        int offset = (timeSpan.Days % 14);

                        for (date = date.AddDays(offset) ; 
                            date.Year == year ;
                            date = date.AddDays(14))
                        {
                            dateList.Add(date);
                        }

                        break;

                    case RecurringEvent.Monthly:
                        dateList.AddRange(Monthly(1, year));
                        break;

                    case RecurringEvent.Quarterly:
                        dateList.AddRange(Monthly(3, year));
                        break;

                    case RecurringEvent.Semiannually:
                        dateList.AddRange(Monthly(6, year));
                        break;
                }
            }

			// A zero month indicates that the movable event repeats for all
			// months

			else if (this.EventType == EventType.Movable && 
				this.Recurring == RecurringEvent.Monthly)
			{
				// Clone a copy so we don't mess up the original event.
				Event ev = (Event)this.Clone();
				DateTime date = this.Date(year);

				if (date < ev.Date())
				{
					date = ev.Date();
				}

				ev.Month = date.Month;

				int maxMonths = 
					CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(year);

				for (;;)
				{
					dateList.Add(ev.Date());

					if ((ev.Month + 1) > maxMonths)
					{
						break;
					}

					ev.Month += 1;
				}
			}

			else
			{
				dateList.Add(this.Date(year));
			}

            DateTime[] dates = new DateTime[dateList.Count];
            dateList.CopyTo(dates);
            return dates;
        }

        // ---------------------------------------------------------------------
        private System.Collections.ArrayList Monthly(int months, int year)
        {
            System.Collections.ArrayList dateList = 
                new System.Collections.ArrayList();

            DateTime date = this.Date(year);

            if (date < this.Date())
            {
                date = this.Date();
            }

            while (date.Year == year)
            {
                dateList.Add(date);
                date = date.AddMonths(months);
            }

            return dateList;
        }

		// ---------------------------------------------------------------------
        public object Clone()
        {
            // A shallow copy works here
            return this.MemberwiseClone();
        }

		// ---------------------------------------------------------------------
		public static string[] AbbreviatedMonthNames
		{
			get
			{
				string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
				string[] names = new string[monthNames.Length + 1];
				names[0] = "Monthly";
				monthNames.CopyTo(names, 1);
				return names;
			}
		}

		private static string GetAbbreviatedMonthName(int month)
		{
			return (month == 0)
				? "Monthly"
				: DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
		}
	}
}
