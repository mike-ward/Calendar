using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace BlueOnion
{
    public class EventsCollection : IEnumerable
    {
        private readonly ArrayList eventsList = new ArrayList();
        private char[] delimiters = {',', '='};
        private readonly string version = Event.Version2;

        // -------------------------------------------------------------
        public void Serialize(TextWriter textWriter)
        {
            textWriter.WriteLine(version);

            foreach (Event ev in eventsList)
            {
                ev.Serialize(textWriter);
            }
        }

        // -------------------------------------------------------------
        public void Deserialize(TextReader textReader)
        {
            var version = textReader.ReadLine();

            if (version != Event.Version1 &&
                version != Event.Version2)
            {
                var message = string.Format(CultureInfo.CurrentCulture,
                    "Unrecognized event file version {0}", version);

                Log.Error(message);
                System.Windows.Forms.MessageBox.Show(message);
                return;
            }

            Event anEvent = null;

            while ((anEvent = Event.Deserialize(textReader, version)) != null)
            {
                eventsList.Add(anEvent);
            }
        }

        // ---------------------------------------------------------------------
        /// <summary>
        /// Returns a hashtable keyed by date. Each value contains a string
        /// containing the event description. If there are multiple events for
        /// the given date, they are combined with newlines seperating the
        /// multiple descriptions.
        /// </summary>
        public Hashtable GetDateDescriptions(DateTime start, DateTime end)
        {
            var events = new Hashtable();

            for (var year = start.Year; year <= end.Year; ++year)
            {
                foreach (Event ev in eventsList)
                {
                    var dates = ev.GetAllDates(year);

                    if (dates == null)
                    {
                        continue;
                    }

                    foreach (var date in dates)
                    {
                        if (date >= start && date <= end)
                        {
                            var description = ev.Description;

                            try
                            {
                                description = string.Format
                                    (CultureInfo.CurrentCulture, description, date);
                            }
                            catch (FormatException e)
                            {
                                Log.Error(e.ToString());
                            }
                            catch (ArgumentNullException e)
                            {
                                Log.Error(e.ToString());
                            }

                            // Strip off hours, minutes, seconds
                            var dateNormalized = date.Date;

                            if (events.Contains(dateNormalized))
                            {
                                events[dateNormalized] += Environment.NewLine + description;
                            }
                            else
                            {
                                events.Add(dateNormalized.Date, description);
                            }
                        }
                    }
                }
            }

            return events;
        }

        // ---------------------------------------------------------------------
        public DateTime[] GetColoredDates(DateTime start, DateTime end)
        {
            var dateList = new ArrayList();

            for (var year = start.Year; year <= end.Year; ++year)
            {
                foreach (Event ev in eventsList)
                {
                    if (ev.HighlightColor == false)
                    {
                        continue;
                    }

                    var dates = ev.GetAllDates(year);

                    if (dates == null)
                    {
                        continue;
                    }

                    foreach (var date in dates)
                    {
                        if (date >= start && date <= end)
                        {
                            dateList.Add(date);
                        }
                    }
                }
            }

            var dateArray = new DateTime[dateList.Count];
            dateList.CopyTo(dateArray);
            return dateArray;
        }

        // ---------------------------------------------------------------------
        public static int ToInt(string number)
        {
            if (number.Trim().Length == 0)
            {
                return 0;
            }

            return int.Parse(number, NumberStyles.Integer,
                CultureInfo.InvariantCulture);
        }

        // ---------------------------------------------------------------------
        public IEnumerator GetEnumerator()
        {
            return eventsList.GetEnumerator();
        }

        // ---------------------------------------------------------------------
        public Event[] GetEvents()
        {
            // Returns a sorted copy of what's here, not a reference to the
            // original list items. This is intended so dialogs can manipulate
            // the event list without touching the original.

            var count = eventsList.Count;
            var dates = new DateTime[count];
            var events = new Event[count];

            for (var i = 0; i < count; ++i)
            {
                var ev = eventsList[i] as Event;
                dates.SetValue(ev.Date(), i);
                events.SetValue(ev.Clone(), i);
            }

            Array.Sort(dates, events);
            return events;
        }

        public void SetEvents(Event[] events)
        {
            eventsList.Clear();
            eventsList.AddRange(events);
        }
    }

    // ---------------------------------------------------------------------
#if false
        public void ReadEventsFile(TextReader textReader)
        {
            string line;

            while ((line = textReader.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.Length == 0 || line[0] == '[')
                {
                    continue;
                }

                // Ignore the [Fixed]/[Movable] tags in the file. Ini file API's
                // are not CLR compliant. It's easy to tell which format is
                // used. Fixed  formats are always "nnnn="

                if (Char.IsNumber(line[0]) && Char.IsNumber(line[1]) &&
                    Char.IsNumber(line[2]) && Char.IsNumber(line[3]) &&
                    line[4] == '=')
                {
                    Event fixedEvent = new Event();
                    fixedEvent.Month = ToInt(line.Substring(0, 2));
                    fixedEvent.Day = ToInt(line.Substring(2, 2));
                    fixedEvent.Description = line.Substring(5);
                    this.eventsList.Add(fixedEvent);
                }
                else
                {
                    switch (line[0])
                    {
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            string[] tokens = line.Split(delimiters, 4);
                            Event movableEvent = new Event();
                            movableEvent.Month = ToInt(tokens[0]);
                            movableEvent.Day = ToInt(tokens[1]) - 1;
                            movableEvent.Nth = ToInt(tokens[2]);
                            movableEvent.Description = tokens[3];
                            this.eventsList.Add(movableEvent);
                            break;

                        case 'E':
                        case 'A':
                            Event ecclesiasticalEvent = new Event();

                            ecclesiasticalEvent.Special = (line[0] == 'E')
                                ? SpecialEventEnum.Easter : SpecialEventEnum.Advent;

                            tokens = line.Substring(1).Split(delimiters, 2);
                            ecclesiasticalEvent.Day = ToInt(tokens[0]);
                            ecclesiasticalEvent.Description = tokens[1];
                            this.eventsList.Add(ecclesiasticalEvent);
                            break;

                        case 'S':
                        case 'F':
                        case 'W':
                            Event seasonEvent = new Event();
                            string season = line.Substring(0, 2);

                        switch (season)
                        {
                            case "SP":
                                seasonEvent.Special = SpecialEventEnum.Spring;
                                break;

                            case "SU":
                                seasonEvent.Special = SpecialEventEnum.Summer;
                                break;

                            case "FA":
                                seasonEvent.Special = SpecialEventEnum.Autumn;
                                break;

                            case "WI":
                                seasonEvent.Special = SpecialEventEnum.Winter;
                                break;

                            default:
                                continue;
                        }

                            tokens = line.Substring(2).Split(delimiters, 2);
                            seasonEvent.Day = ToInt(tokens[0]);
                            seasonEvent.Description = tokens[1];
                            this.eventsList.Add(seasonEvent);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
#endif
}