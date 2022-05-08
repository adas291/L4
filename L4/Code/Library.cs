using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace L4.Code
{
    public class Library
    {
        public string LibraryTitle { get; set; }
        public string Address { get; set; }
        public double MobileNumber { get; set; }
        public List<Publication> Books { get; set; }

        public Library(string libraryTitle, string address, double mobileNumber)
        {
            LibraryTitle = libraryTitle;
            Address = address;
            MobileNumber = mobileNumber;
            Books = new List<Publication>();
        }
        
        public Library()
        {
            Books = new List<Publication>();
        }
        public void AddPublication(Publication publication)
        {
            Books.Add(publication);
        }
        public override string ToString()
        {
            return String.Format($"{LibraryTitle} {Address} {MobileNumber}");
        }
    }
}