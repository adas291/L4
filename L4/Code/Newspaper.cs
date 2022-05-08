using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{
    /// <summary>
    /// Data class for storing information about newspaper
    /// </summary>
    public class Newspaper:Publication
    {
        public int ReleaseNumber { get; set; }
        public int ReleaseMonth { get; set; }
        public int ReleaseDay { get; set; }

        public Newspaper(int releaseNumber, int releaseMonth, int releaseDay, string title, string type, string publisher, int releaseYear, int pageCount, int copies) :base(title, type, publisher, releaseYear, pageCount, copies)
        {
            ReleaseNumber = releaseNumber;
            ReleaseMonth = releaseMonth;
            ReleaseDay = releaseDay;
        }
        
        /// <summary>
        /// Compares two newspapers by standart compare to values
        /// </summary>
        /// <param name="other">other newspaper for comparison</param>
        /// <returns></returns>
        public override int CompareTo(Publication other)
        {
            DateTime date1 = new DateTime(ReleaseYear, ReleaseMonth, ReleaseDay);
            DateTime date2 = new DateTime(((Newspaper)other).ReleaseMonth, ((Newspaper)other).ReleaseMonth, ((Newspaper)other).ReleaseDay);

            if (date1 > date2)
            {
                return -1;
            }
            if (date1 == date2)
            {
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// Check if objects are equal
        /// </summary>
        /// <param name="other">other newspaper</param>
        /// <returns>return true if objects are equal by all fields</returns>
        public bool Equals(Newspaper other)
        {
            return base.Equals(other) && ReleaseNumber == other.ReleaseNumber && ReleaseMonth 
                                                       == other.ReleaseMonth && ReleaseDay 
                                                       == other.ReleaseDay;
        }
        /// <summary>
        /// Check if objects are not new
        /// </summary>
        /// <returns>True if newspaper is older than 7 days</returns>
        public override bool IsNotNew()
        {
            return (TaskUtils.PublicationAgeInDays(ReleaseYear, ReleaseMonth, ReleaseDay) > 7);

        }
        /// <summary>
        /// Formated string for csv format
        /// </summary>
        /// <returns>formated string</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format($";{ReleaseNumber,14};{ReleaseMonth,10};{ReleaseDay,13};{"-",-20};{"-",12};");
        }
        /// <summary>
        /// Formated string for text format
        /// </summary>
        /// <returns>formated string</returns>
        public override string ToStringTxt()
        {
            return ToString().Replace(';', '|');
        }
    }
}