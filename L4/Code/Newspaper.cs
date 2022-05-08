using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{
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

        public bool Equals(Newspaper other)
        {
            throw new NotImplementedException();
        }

        public override bool IsNotNew()
        {
            return (TaskUtils.PublicationAgeInDays(ReleaseYear, ReleaseMonth, ReleaseDay) > 7);

        }

        public override string ToString()
        {
            return base.ToString() + string.Format($";{ReleaseNumber,14};{ReleaseMonth,10};{ReleaseDay,13};{"-",-20};{"-",12};");
        }
        public override string ToStringTxt()
        {
            return ToString().Replace(';', '|');
        }
    }
}