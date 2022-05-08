using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{
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

        public bool Equals(Book other)
        {
            return base.Equals(other) && (Author.CompareTo(other.Author)==0 ) &&(ISBN.CompareTo(other.ISBN)==0);
        }
        public override bool IsNotNew()
        {
            return TaskUtils.PublicationAgeInDays(ReleaseYear, 1, 1) > 365;

        }
        public override string ToString()
        {
            return base.ToString() + string.Format($";{"-", 14};{"-", 10};{"-", 13};{Author,-20};{ISBN, 12};");
        }

        public override string ToStringTxt()
        {
            return ToString().Replace(';', '|');
        }
    }
}