// Copyright 2005 Blue Onion Software, All rights reserved
//
using System;

namespace BlueOnion
{
    // ---------------------------------------------------------------------
    public enum Season
    {
        Spring = 0, Summer = 1, Autumn = 2, Winter = 3
    }

    // ---------------------------------------------------------------------
    public sealed class DateComputations
    {
        // -------------------------------------------------------------
        // nth: 1-7 (5=last, 6=1st full week, 7=last full week)
        // weekday: 0=Sunday, 1=Monday, etc.
        // month: 0-11
        // year: 4 digit
        // return: day (1-31)
        static public int NthWeekday(int nth, int weekday, int month, int year)
        {
            bool firstFullWeek = false;
			bool lastFullWeek = false;

            if (nth == 6)
            {
                nth = 1;
                firstFullWeek = true;
            }
			
			if (nth == 7)
			{
				nth = 5;
				lastFullWeek = true;
			}

			int daysInMonth = MonthCalendarEx.GetDaysInMonth(year, month);
            int day = System.Math.Min(nth * 7, daysInMonth);
            DateTime date = new DateTime(year, month, day);

            while ((int)date.DayOfWeek != weekday)
            {
                date = date.AddDays(-1);
            }

            if (firstFullWeek == true)
            {
                if ((date.Day - weekday) <= 0)
                {
                    date = date.AddDays(7);
                }
            }

			if (lastFullWeek == true)
			{
				if (((date.Day - weekday) + 6) > daysInMonth) 
				{
					date = date.AddDays(-7);
				}																						   
			}

            return date.Day;
        }

        // -------------------------------------------------------------
        static public DateTime Easter(int year)
        {
            // The method used below has been given by Spencer Jones in his book
            // "General Astronomy" (pages 73-74 of the edition of 1922). It has been
            // published again in the "Journal of the British Astronomical Association",
            // Vol.88, page 91 (December 1977) where it is said that it was devised in
            // 1876 and appeared in the Butcher's "Ecclesiastical Calendar."
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int n = h + l - 7 * m + 114;
    
            return new DateTime(year, n / 31, n % 31 + 1);
        }

        // -------------------------------------------------------------
        static public DateTime Advent(int year)
        {
            DateTime christmas = new DateTime(year, 12, 25);
            int christmas_day = (int)christmas.DayOfWeek;
    
            if (christmas_day == 0)
            {
                christmas_day = 7;
            }
    
            return christmas.AddDays(-(christmas_day + 21));
        }

        // ---------------------------------------------------------------------
        static public DateTime EquinoxSolstice(int year, Season season)
        {
            const double kDegRad = 0.0174532925199433;
            
            // Calculate equinoxes and solstices
            // (Meeus, Jean, "Astronomical Algorithms, 2nd ed." 
            //  Willmann-Bell. Inc. 1998, pp. 177)
            
            double JDE0 = 0;  // Julian Ephemeris Day
            double Y    = (year - 2000.0) / 1000.0;
            
            double[,] coeff =
            {
                {-0.00057, -0.00411,  0.05169, 365242.37404, 2451623.80984},
                {-0.00030,  0.00888,  0.00325, 365241.62603, 2451716.56767},
                { 0.00078,  0.00337, -0.11575, 365242.01767, 2451810.21715},
                { 0.00032, -0.00823, -0.06223, 365242.74049, 2451900.05952}
            };
            
            for (int i = 0; i < 5; i++)
            {
                JDE0 = coeff[(int)season,i] + Y * JDE0;
            }
            
            double T           = (JDE0 - 2451545) / 36525; // Julian Centuries
            double W           = kDegRad * (35999.373 * T - 2.47);
            double DELTAlambda = 1 + 0.0334 * Math.Cos(W) + 0.0007 * Math.Cos(2*W);
            
            double[,] c =
            {
                {485, kDegRad * 324.96, kDegRad *   1934.136},
                {203, kDegRad * 337.23, kDegRad *  32964.467},
                {199, kDegRad * 342.08, kDegRad *     20.186},
                {182, kDegRad *  27.85, kDegRad * 445267.112},
                {156, kDegRad *  73.14, kDegRad *  45036.886},
                {136, kDegRad * 171.52, kDegRad *  22518.443},
                { 77, kDegRad * 222.54, kDegRad *  65928.934},
                { 74, kDegRad * 296.72, kDegRad *   3034.906},
                { 70, kDegRad * 243.58, kDegRad *   9037.513},
                { 58, kDegRad * 119.81, kDegRad *  33718.147},
                { 52, kDegRad * 297.17, kDegRad *    150.678},
                { 50, kDegRad *  21.02, kDegRad *   2281.226},
                { 45, kDegRad * 247.54, kDegRad *  29929.562},
                { 44, kDegRad * 325.15, kDegRad *  31555.956},
                { 29, kDegRad *  60.93, kDegRad *   4443.417},
                { 18, kDegRad * 155.12, kDegRad *  67555.328},
                { 17, kDegRad * 288.79, kDegRad *   4562.452},
                { 16, kDegRad * 198.04, kDegRad *  62894.029},
                { 14, kDegRad * 199.76, kDegRad *  31436.921},
                { 12, kDegRad *  95.39, kDegRad *  14577.848},
                { 12, kDegRad * 287.11, kDegRad *  31931.756},
                { 12, kDegRad * 320.81, kDegRad *  34777.259},
                {  9, kDegRad * 227.73, kDegRad *   1222.114},
                {  8, kDegRad *  15.45, kDegRad *  16859.074}
            };
            
            double S = 0;
            
            for (int i = 0; i < c.GetLength(0); i++)
            {
                S += c[i,0] * Math.Cos(c[i,1] + c[i,2] * T);
            }
            
            return JulianDayToGregorianDate(JDE0 + (0.00001 * (S / DELTAlambda)));
        }

        // ---------------------------------------------------------------------

        static public DateTime JulianDayToGregorianDate(double julianDay)
        {
            // Convert Julian Day to Gregorian month, day and year
            // (Meeus, Jean, "Astronomical Algorithms, 2nd ed." 
            // Willmann-Bell. Inc. 1998, pp. 63)
            
            julianDay += 0.5;
            
            double Z = Math.Floor(julianDay);
            double F = julianDay - Z;
            double A;
            
            if (Z >= 2291161)
            {
                double alpha = Math.Floor((Z - 1867216.25)/36524.25);        
                A = Z + 1 + alpha - Math.Floor(alpha/4);
            }
            
            else
            {
                A = Z;
            }
            
            double B = A + 1524;
            double C = Math.Floor((B - 122.1)/365.25);
            double D = Math.Floor(365.25 * C);
            double E = Math.Floor((B - D)/30.6001);            
            double d = B - D - Math.Floor(30.6001 * E) + F;

            int day = Convert.ToInt32(Math.Floor(d));
    
            // calculate hour, minutes, and seconds
            d -= Math.Floor(d);
            int hour = Convert.ToInt32(Math.Floor(d * 24));
            int minute = Convert.ToInt32(Math.Floor(d * 1440)) % 60;
    
            int month = Convert.ToInt32(Math.Floor(E));
    
            if (month < 14)
            {
                month -= 1;
            }
    
            else if (month == 14 || month == 15)
            {
                month -= 13;
            }
    
            else
            {
                return DateTime.MinValue;
            }
    
            int year;

            if (month > 2)
            {
                year = Convert.ToInt32(Math.Floor(C - 4716));
            }
    
            else if (month == 1 || month == 2)
            {
                year = Convert.ToInt32(Math.Floor(C - 4715));
            }
    
            else
            {
                return DateTime.MinValue;
            }

            DateTime date =  new DateTime(year, month, day, hour, minute, 0, 0);
            return date.ToLocalTime();
        }

        // ---------------------------------------------------------------------
        private DateComputations()
        {
        }
    }
}
