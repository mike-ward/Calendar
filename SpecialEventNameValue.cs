// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;

namespace BlueOnion
{
    // -------------------------------------------------------------------------
    public enum SpecialEvent
    {
        None = 0, Easter=1, Advent=2, Spring=3, Summer=4, Autumn=5, Winter=6
    }

    // -------------------------------------------------------------------------
    public class SpecialEventNameValue
    {
        private string name;
        private SpecialEvent specialEvent;

        public SpecialEventNameValue(string name, SpecialEvent specialEvent)
        {
            this.name = name;
            this.specialEvent = specialEvent;
        }

        public string Name
        {
            get { return this.name; }
        }

        public SpecialEvent Value
        {
            get { return this.specialEvent; }
        }

        static public string GetName(SpecialEvent specialEvent)
        {
            string eventName;

            switch (specialEvent)
            {
                case SpecialEvent.Easter:
                    eventName = "Easter";
                    break;
                    
                case SpecialEvent.Advent:
                    eventName = "Advent";
                    break;
                    
                case SpecialEvent.Spring:
                    eventName = "Spring";
                    break;
                    
                case SpecialEvent.Summer:
                    eventName = "Summer";
                    break;
                    
                case SpecialEvent.Autumn:
                    eventName = "Autumn";
                    break;
                    
                case SpecialEvent.Winter:
                    eventName = "Winter";
                    break;
                    
                default:
                    eventName = string.Empty;
                    break;
            }

            return eventName;
        }

        static public SpecialEventNameValue[] List()
        {
            SpecialEventNameValue[] list = 
            {
                new SpecialEventNameValue("Easter", SpecialEvent.Easter),
                new SpecialEventNameValue("Advent", SpecialEvent.Advent),
                new SpecialEventNameValue("Spring", SpecialEvent.Spring),
                new SpecialEventNameValue("Summer", SpecialEvent.Summer),
                new SpecialEventNameValue("Autumn", SpecialEvent.Autumn),
                new SpecialEventNameValue("Winter", SpecialEvent.Winter)
            };

            return list;
        }
    }
}
