using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{
    /// <summary>
    /// Data class for saving information about Journals
    /// </summary>
    public class Journal : Publication
    {
        public double ISBN { get; set; }
        public int ReleaseNumber { get; set; }
        public int ReleaseMonth { get; set; }

        public Journal(double iSBN, int releaseNumber, int releaseMonth, string title, string type, string publisher, int releaseYear, int pageCount, int copies)
            :base(title, type, publisher, releaseYear, pageCount, copies)
        {
            ISBN = iSBN;
            ReleaseNumber = releaseNumber;
            ReleaseMonth = releaseMonth;
        }
        /// <summary>
        /// Compares by release year and month
        /// </summary>
        /// <param name="other">other Journal</param>
        /// <returns>returns by standart compare values</returns>
        public override int CompareTo(Publication other)
        {
            DateTime date1 = new DateTime(ReleaseYear, ReleaseMonth, 1);
            DateTime date2 = new DateTime(((Journal)other).ReleaseMonth, ((Journal)other).ReleaseMonth, 1);

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
        /// Check if journal is not new
        /// </summary>
        /// <returns>is true when journal is more than 30 days old</returns>
        public override bool IsNotNew()
        {
            return TaskUtils.PublicationAgeInDays(ReleaseYear, ReleaseMonth, 1) > 30;

        }
        /// <summary>
        /// Formated string for csv
        /// </summary>
        /// <returns>formated string</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format($";{ReleaseNumber,14};{ReleaseMonth,10};{"-",13};{"-",-20};{ISBN, 12};");
        }
        /// <summary>
        /// Formated string for txt output
        /// </summary>
        /// <returns>formated string</returns>
        public override string ToStringTxt()
        {
            return ToString().Replace(';', '|');
        }
    }
}