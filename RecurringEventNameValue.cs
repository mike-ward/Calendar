// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;

namespace BlueOnion
{
    // -------------------------------------------------------------------------
    public enum RecurringEvent
    {
        Annually = 0, Biweekly = 1, Monthly = 2, Quarterly = 3, Semiannually = 4, None = 5
    }

    /// <summary>
    /// Helper class to map a localized name to a recurring event type
    /// </summary>
    // ---------------------------------------------------------------------
    public class RecurringEventNameValue
    {
        private string name;
        private RecurringEvent recurringEvent;

        public RecurringEventNameValue(string name,
            RecurringEvent recurringEvent)
        {
            this.name = name;
            this.recurringEvent = recurringEvent;
        }

        public string Name
        {
            get { return this.name; }
        }

        public RecurringEvent Value
        {
            get { return this.recurringEvent; }
        }

        static public string GetName(RecurringEvent recurringEvent)
        {
            string eventName;

            switch (recurringEvent)
            {
                case RecurringEvent.Annually:
                    eventName = "Annually";
                    break;

                case RecurringEvent.Biweekly:
                    eventName = "Biweekly";
                    break;

                case RecurringEvent.Monthly:
                    eventName = "Monthly";
                    break;

                case RecurringEvent.Quarterly:
                    eventName = "Quarterly";
                    break;

                case RecurringEvent.Semiannually:
                    eventName = "Semiannually";
                    break;

                case RecurringEvent.None:
                    eventName = "None";
                    break;

                default:
                    eventName = string.Empty;
                    break;
            }

            return eventName;
        }

        static public RecurringEventNameValue[] List()
        {
            RecurringEventNameValue[] list =
            {
                new RecurringEventNameValue("Annually",
                    RecurringEvent.Annually),

                new RecurringEventNameValue("Biweekly",
                    RecurringEvent.Biweekly),

                new RecurringEventNameValue("Monthly",
                    RecurringEvent.Monthly),

                new RecurringEventNameValue("Quarterly",
                    RecurringEvent.Quarterly),

                new RecurringEventNameValue("Semi Annually",
                    RecurringEvent.Semiannually),

                new RecurringEventNameValue("None",
                    RecurringEvent.None)
            };

            return list;
        }
    }
}
