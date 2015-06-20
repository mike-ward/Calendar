using System;
using System.Globalization;
using System.IO;

namespace BlueOnion
{
    // -------------------------------------------------------------------------
    public enum EventType
    {
        Recurring = 0,
        Movable = 1,
        Special = 2
    }

    // -------------------------------------------------------------------------
    public class Event : ICloneable
    {
        private EventType eventType = EventType.Recurring;
        private int day; // 1-31
        private int month; // 1-12
        private int year; // 4 digit
        private int week = 1; // 1-5
        private int days; // -365 to 365
        private DayOfWeek weekDay = DayOfWeek.Sunday;
        private SpecialEvent special = SpecialEvent.Spring;
        private RecurringEvent recurring = RecurringEvent.Annually;

        public static readonly string Version1 = "V1";
        public static readonly string Version2 = "V2";

        private static readonly string[] weekNumbers =
            new string[7] {"1st", "2nd", "3rd", "4th", "Last", "1st full", "Last full"};

        // ---------------------------------------------------------------------
        public Event()
        {
            var today = DateTime.Today;

            Day = today.Day;
            Month = today.Month;
            Year = today.Year;
        }

        // ---------------------------------------------------------------------
        public EventType EventType
        {
            get
            {
                return Enum.IsDefined(typeof (EventType), eventType)
                    ? eventType
                    : EventType.Recurring;
            }

            set { eventType = value; }
        }

        // ---------------------------------------------------------------------
        public int Month
        {
            get { return month; }

            set
            {
                if (value < 1 || value >
                    CultureInfo.CurrentCulture.Calendar.GetMonthsInYear(Year))
                {
                    throw new ArgumentOutOfRangeException("Month", value,
                        "Value outside of current culture calendar range");
                }

                month = value;
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
                return Enum.IsDefined(typeof (DayOfWeek), weekDay)
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
        public string Description { get; set; }

        // ---------------------------------------------------------------------
        public SpecialEvent Special
        {
            get
            {
                return
                    Enum.IsDefined(typeof (SpecialEvent), special)
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
                    Enum.IsDefined(typeof (RecurringEvent), recurring)
                        ? recurring
                        : RecurringEvent.Annually;
            }

            set { recurring = value; }
        }

        // ---------------------------------------------------------------------
        public bool HighlightColor { get; set; }

        // ---------------------------------------------------------------------
        public static string[] WeekNumbers()
        {
            return weekNumbers;
        }

        // ---------------------------------------------------------------------
        public void Serialize(TextWriter textWriter)
        {
            textWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                Enum.Format(typeof (EventType), EventType, "d"),
                Month.ToString(CultureInfo.InvariantCulture),
                Day.ToString(CultureInfo.InvariantCulture),
                Year.ToString(CultureInfo.InvariantCulture),
                Week.ToString(CultureInfo.InvariantCulture),
                Days.ToString(CultureInfo.InvariantCulture),
                Enum.Format(typeof (DayOfWeek), Weekday, "d"),
                Enum.Format(typeof (SpecialEvent), Special, "d"),
                Enum.Format(typeof (RecurringEvent), Recurring, "d"),
                HighlightColor.ToString(CultureInfo.InvariantCulture),
                Description);
        }

        // ---------------------------------------------------------------------
        public static Event Deserialize(TextReader textReader, string version)
        {
            var text = textReader.ReadLine();

            if (text == null)
            {
                return null; // end of the stream, baby...
            }

            Event theEvent = null;

            try
            {
                char[] delimiter = {','};
                var length = (version == Version1) ? 10 : 11;
                var eventParts = text.Split(delimiter, length);

                if (eventParts.Length != length)
                {
                    Log.Error("Event.Deserialize: version and length to not match");
                    return null;
                }

                theEvent = new Event();

                theEvent.EventType = (EventType)
                    Enum.Parse(typeof (EventType), eventParts[0]);

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
                    Enum.Parse(typeof (DayOfWeek), eventParts[6]);

                theEvent.Special = (SpecialEvent)
                    Enum.Parse(typeof (SpecialEvent), eventParts[7]);

                theEvent.Recurring = (RecurringEvent)
                    Enum.Parse(typeof (RecurringEvent), eventParts[8]);

                if (version == Version1)
                {
                    theEvent.Description = eventParts[9];
                }
                else if (version == Version2)
                {
                    theEvent.HighlightColor = Convert.ToBoolean(eventParts[9],
                        CultureInfo.InvariantCulture);

                    theEvent.Description = eventParts[10];
                }
            }
            catch (Exception e)
            {
                Log.Error("Event.Deserialize exception caught" + Environment.NewLine + e);
            }

            return theEvent;
        }

        // ---------------------------------------------------------------------
        public override string ToString()
        {
            var date = string.Empty;
            var recurring = string.Empty;

            switch (EventType)
            {
                case EventType.Recurring:
                    var year = (Year == 0) ? DateTime.Today.Year : Year;
                    var d = new DateTime(year, Month, Day);
                    date = d.ToShortDateString();

                    if (Recurring != RecurringEvent.Annually)
                    {
                        recurring = "(" + RecurringEventNameValue.GetName(Recurring) + ")";
                    }

                    break;

                case EventType.Movable:
                    var week =
                        (Week > 0 && Week <= WeekNumbers().Length)
                            ? WeekNumbers()[Week - 1]
                            : "Out of range";

                    date = string.Format(CultureInfo.CurrentCulture,
                        "{0} {1} {2}",
                        week,
                        DateTimeFormatInfo.CurrentInfo.GetAbbreviatedDayName(Weekday),
                        GetAbbreviatedMonthName(Month));

                    break;

                case EventType.Special:
                    date = SpecialEventNameValue.GetName(Special) +
                        ((Days < 0) ? " - " : " + ") +
                        Math.Abs(Days).ToString(CultureInfo.CurrentCulture);

                    break;

                default:
                    break;
            }

            return string.Format(CultureInfo.CurrentCulture,
                "{0,-15}\t {1} {2}", date, Description, recurring);
        }

        // ---------------------------------------------------------------------
        public DateTime Date()
        {
            return Date(Year);
        }

        // ---------------------------------------------------------------------
        public DateTime Date(int year)
        {
            if (year <= 0)
            {
                year = DateTime.Today.Year;
            }

            var date = DateTime.MinValue;

            switch (EventType)
            {
                case EventType.Recurring:
                    date = new DateTime(year, Month, Day);
                    break;

                case EventType.Movable:
                    var day = DateComputations.NthWeekday(Week,
                        Convert.ToInt32(Weekday, CultureInfo.InvariantCulture),
                        Month, year);

                    date = new DateTime(year, Month, day);
                    break;

                case EventType.Special:
                    switch (Special)
                    {
                        case SpecialEvent.Easter:
                            var easter = DateComputations.Easter(year);
                            date = easter.AddDays(Days);
                            break;

                        case SpecialEvent.Advent:
                            var advent = DateComputations.Advent(year);
                            date = advent.AddDays(Days);
                            break;

                        case SpecialEvent.Spring:
                            var spring = DateComputations.EquinoxSolstice(year, Season.Spring);
                            date = spring.AddDays(Days);
                            break;

                        case SpecialEvent.Summer:
                            var summer = DateComputations.EquinoxSolstice(year, Season.Summer);
                            date = summer.AddDays(Days);
                            break;

                        case SpecialEvent.Autumn:
                            var autumn = DateComputations.EquinoxSolstice(year, Season.Autumn);
                            date = autumn.AddDays(Days);
                            break;

                        case SpecialEvent.Winter:
                            var winter = DateComputations.EquinoxSolstice(year, Season.Winter);
                            date = winter.AddDays(Days);
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

            var dateList =
                new System.Collections.ArrayList();

            if (EventType == EventType.Recurring)
            {
                switch (Recurring)
                {
                    case RecurringEvent.None:
                        if (year == this.year)
                        {
                            dateList.Add(new DateTime(year, Month, Day));
                        }
                        break;

                    case RecurringEvent.Annually:
                        dateList.Add(new DateTime(year, Month, Day));
                        break;

                    case RecurringEvent.Biweekly:
                        var date = new DateTime(year, 1, 1);

                        if (date < Date())
                        {
                            date = Date();
                        }

                        var timeSpan = Date() - date;
                        var offset = (timeSpan.Days%14);

                        for (date = date.AddDays(offset);
                            date.Year == year;
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
            else if (EventType == EventType.Movable &&
                Recurring == RecurringEvent.Monthly)
            {
                // Clone a copy so we don't mess up the original event.
                var ev = (Event) Clone();
                var date = Date(year);

                if (date < ev.Date())
                {
                    date = ev.Date();
                }

                ev.Month = date.Month;

                var maxMonths =
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
                dateList.Add(Date(year));
            }

            var dates = new DateTime[dateList.Count];
            dateList.CopyTo(dates);
            return dates;
        }

        // ---------------------------------------------------------------------
        private System.Collections.ArrayList Monthly(int months, int year)
        {
            var dateList =
                new System.Collections.ArrayList();

            var date = Date(year);

            if (date < Date())
            {
                date = Date();
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
            return MemberwiseClone();
        }

        // ---------------------------------------------------------------------
        public static string[] AbbreviatedMonthNames
        {
            get
            {
                var monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
                var names = new string[monthNames.Length + 1];
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