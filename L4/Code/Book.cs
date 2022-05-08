using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{

    /// <summary>
    /// Data class for saving information about book
    /// </summary>
    public class Book : Publication
    { 
        public string Author { get; set; }
        public double ISBN { get; set; }

        public Book(string author, double iSBN, string title, string type, string publisher, int releaseYear, int pageCount, int copies)
            :base(title, type, publisher, releaseYear, pageCount, copies)
        {
            Author = author;
            ISBN = iSBN;
        }
        /// <summary>
        /// Compares by release date
        /// </summary>
        /// <param name="other">Other publication to compare</param>
        /// <returns>-1 if value precedes other, 0 if equals and 1 if value is after other</returns>
        public override int CompareTo(Publication other)
        {
            if (ReleaseYear > ((Book)other).ReleaseYear)
            {
                return -1;
            }
            else if (ReleaseYear == ((Book)other).ReleaseYear)
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// Compares by base and local fields
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Book other)
        {
            return base.Equals(other) && (Author.CompareTo(other.Author)==0 ) &&(ISBN.CompareTo(other.ISBN)==0);
        }
        /// <summary>
        /// Checks if publication is not new
        /// </summary>
        /// <returns></returns>
        public override bool IsNotNew()
        {
            return TaskUtils.PublicationAgeInDays(ReleaseYear, 1, 1) > 365;

        }
        /// <summary>
        /// Returns formated publication's string
        /// </summary>
        /// <returns>Formated string</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format($";{"-", 14};{"-", 10};{"-", 13};{Author,-20};{ISBN, 12};");
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