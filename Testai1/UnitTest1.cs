using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using L4.Code;
using System.IO;
using Xunit;

namespace Testai1
{
    public class UnitTest1
    {
        
        public static List<Publication> pub1 = new List<Publication>
        {
            new Book("auth", 15, "pub1", "Book", "publ", 2022, 1111, 5),
            new Book("auth", 15, "pub1", "Book", "publ", 2017, 5, 6),

            new Newspaper(5, 5, 5, "15min", "Newspaper", "leid1", 2015, 5, 5),
            new Newspaper(5, 5, 5, "16min", "Newspaper", "leid2", 2022, 12345, 43),

            new Journal(55, 5, 5, "National1", "Journal", "publ", 2018, 189, 33333),
            new Journal(55, 5, 5, "National2", "Journal", "publ", 2022, 189, 32),

        };
        public static List<Publication> pub2 = new List<Publication>
        {
            new Book("auth", 15, "pub1", "Book", "publ", 2015, 5, 5),
            new Book("auth", 15, "pub1", "Book", "publ", 2017, 5, 5),
        };

        [Fact]
        public static void SortWithTwoValues()
        {
            
            TaskUtils.Sort(pub2);
            Assert.True(pub2[0].Equals(new Book("auth", 15, "pub1", "Book", "publ", 2017, 5, 5)));
        }
        [Fact]
        public static void test2()
        {
           List<Publication> temp =  TaskUtils.FilterByType<Journal>(pub1);
            foreach (var item in temp)
            {
                Assert.IsType(typeof(Journal), item);
            }
        }
        [Fact]
        public static void PublicationAgeWithTwoValues()
        {
            Assert.Equal(0, TaskUtils.PublicationAgeInDays(2022, 05, 8));
            Assert.NotEqual(2, TaskUtils.PublicationAgeInDays(2022, 05, 3));
        }
        [Fact]
        public static void MostCopyCount()
        {
            List<Publication> temp = TaskUtils.MostCopies(pub1);
            Assert.True(temp[0].Copies == 43 && temp[1].Copies == 33333 && temp[2].Copies == 6);
        }
        [Fact]
        public static void NotNew()
        {
            Library lib = new Library("lib1", "gatve123", 87878787);
            lib.Books = pub1;
            var temp = TaskUtils.NotNewPublications(new List<Library> { lib });
            Assert.Equal(3, temp.Count);
        }

    }
}

