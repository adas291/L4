using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace L4.Code
{
    public class TaskUtils
    {
        /// <summary>
        /// Emptys directory before writing
        /// </summary>
        /// <param name="path">string path to clear</param>
        public static void ClearDirectory(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        /// <summary>
        /// Sorts all publications in list by release date
        /// </summary>
        /// <param name="publications">publications to sort</param>
        public static void Sort(List<Publication> publications)
        {
            if (publications.Count > 1)
            {
                for (int i = 0; i < publications.Count - 1; i++)
                {
                    for (int j = i + 1; j < publications.Count; j++)
                    {
                        if ((publications[i]).CompareTo(publications[j]) > 0)
                        {
                            Publication temp = publications[i];
                            publications[i] = publications[j];
                            publications[j] = temp;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Filters list by entered type to generic type brackets
        /// </summary>
        /// <typeparam name="Ttype">Type for filter</typeparam>
        /// <param name="list">list with only filtered type objects</param>
        /// <returns></returns>
        public static List<Publication> FilterByType<Ttype>(List<Publication> list) where Ttype : Publication
        {

            List<Publication> filtered = new List<Publication>();

            foreach (Publication item in list)
            {
                if (item is Ttype)
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
        /// <summary>
        /// Returns publication's age in days
        /// </summary>
        /// <param name="ReleaseYear">Publication's release year</param>
        /// <param name="ReleaseMonth">Publication's release month</param>
        /// <param name="ReleaseDay">Publication's release date</param>
        /// <returns>Publications age in days</returns>
        public static int PublicationAgeInDays(int ReleaseYear, int ReleaseMonth, int ReleaseDay)
        {
            DateTime pub = new DateTime(ReleaseYear, ReleaseMonth, ReleaseDay);
            return (DateTime.Today - pub).Days;
        }
        /// <summary>
        /// Filters not new publications
        /// </summary>
        /// <param name="source">input list for filter</param>
        /// <returns>filtered list of not new publications</returns>
        public static List<Publication> NotNewPublications(List<Library> source)
        {
            List<Publication> destination = new List<Publication>();
            foreach (var library in source)
            {
                foreach (var item in library.Books)
                {
                    switch (item.Type)
                    {
                        case "Newspaper":
                            {
                                if (((Newspaper)item).IsNotNew())
                                {
                                    destination.Add(item);
                                }
                                break;
                            }
                        case "Journal":
                            {
                                if (((Journal)item).IsNotNew())
                                {
                                    destination.Add(item);
                                }
                                break;
                            }
                        case "Book":
                            {
                                if (((Book)item).IsNotNew())
                                {
                                    destination.Add(item);
                                }
                                break;
                            }
                        default:
                            {
                                throw new Exception("Type if publication is not supported or misstyped");
                            }
                    }
                }
            }
            return destination;
        }
        /// <summary>
        /// Filters list of specific publishers books
        /// </summary>
        /// <param name="source">Input list to search</param>
        /// <param name="publisher">Publisher for filter</param>
        /// <returns>Filtered list of selected publisher's books</returns>
        public static List<Publication> SpecificPublisher(List<Library> source, string publisher)
        {
            List<Publication> destination = new List<Publication>();
            foreach (var library in source)
            {
                foreach (var item in library.Books)
                {
                    if (item.Publisher == publisher)
                    {
                        destination.Add(item);
                    }
                }
            }
            return destination;

        }
        /// <summary>
        /// Finds unique publications in all libraries
        /// </summary>
        /// <param name="libraries">List of all libraries</param>
        /// <returns>Publications list with unique releases</returns>
        public static List<Publication> UniquePublications(List<Library> libraries)
        {
            List<Publication> uniquePublicaitons = new List<Publication>();
            foreach (var library in libraries)
            {
                foreach (var publication in library.Books)
                {
                    if (uniquePublicaitons.Contains(publication))
                    {
                        uniquePublicaitons.Remove(publication);
                    }
                    else
                    {
                        uniquePublicaitons.Add(publication);
                    }
                }
            }
            return uniquePublicaitons;
        }

        /// <summary>
        /// Finds object with most copies of each type and puts into single list
        /// </summary>
        /// <param name="library">Libary for most copies</param>
        /// <returns></returns>
        public static List<Publication> MostCopies(List<Publication> library)
        {
            Newspaper maxNewspaper = null;
            Journal maxJournal = null;
            Book maxBook = null;

            for (int i = 1; i < library.Count; i++)
            {
                if (library[i] is Newspaper && (maxNewspaper == null || maxNewspaper.Copies < library[i].Copies))
                {
                    maxNewspaper = (Newspaper)library[i];
                }
                else if (library[i] is Journal && (maxJournal == null || maxJournal.Copies < library[i].Copies))
                {
                    maxJournal = (Journal)library[i];
                }
                if (library[i] is Book && (maxBook == null || maxNewspaper.Copies < library[i].Copies))
                {
                    maxBook = (Book)library[i];
                }
            }
            List<Publication> tempList = new List<Publication>();

            tempList.Add(maxNewspaper);
            tempList.Add(maxJournal);
            tempList.Add(maxBook);

            return tempList;
        }
    }

}