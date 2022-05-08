using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{
    public abstract class Publication : IComparable<Publication>, IEquatable<Publication>
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Publisher { get; set; }
        public int ReleaseYear { get; set; }
        public int PageCount { get; set; }
        public int Copies { get; set; }

        public Publication(string title, string type, string publisher, int releaseYear, int pageCount, int copies)
        {
            Title = title;
            Type = type;
            Publisher = publisher;
            ReleaseYear = releaseYear;
            PageCount = pageCount;
            Copies = copies;
        }
                
        public bool Equals(Publication other)
        {
            return (Title.CompareTo(other.Title) == 0) &&
                   (Type.CompareTo(other.Type) == 0) &&
                   (Publisher.CompareTo(other.Publisher) == 0) &&
                   (ReleaseYear.CompareTo(other.ReleaseYear) == 0) &&
                   (PageCount == other.PageCount) &&
                   (Copies == other.Copies);
        }
        public override int GetHashCode()
        {
            var hashCode = -782363195;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Publisher);
            hashCode = hashCode * -1521134295 + ReleaseYear.GetHashCode();
            hashCode = hashCode * -1521134295 + PageCount.GetHashCode();
            hashCode = hashCode * -1521134295 + Copies.GetHashCode();
            return hashCode;
        }
        public abstract bool IsNotNew();

        public virtual int CompareTo(Publication other)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Format($"{Title,-30};{Type,-15};{Publisher,-15};{ReleaseYear,-15};{PageCount,5};{Copies,8}");
        }
        
        public virtual string ToStringTxt()
        {
            return ToString().Replace(';', '|');
        }

    }
}