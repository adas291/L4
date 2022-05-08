using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Web.UI.WebControls;


namespace L4.Code
{
    /// <summary>
    /// For input and output of files
    /// </summary>
    public class InOut
    {
        /// <summary>
        /// Read data from directory
        /// </summary>
        /// <param name="directory">Directory to read from</param>
        /// <param name="table">Error table</param>
        /// <returns>Libraries in list</returns>
        /// <exception cref="Exception">Check if there are no files</exception>
        public static List<Library> ReadDataFromFiles(DirectoryInfo directory, Table table)
        {
            if (directory.GetFiles().Length == 0)
            {
                throw new Exception("No files uploaded in correct format");
            }
            List<Library> libraries = new List<Library>();


            foreach (var file in directory.GetFiles())
            {
                Library library = new Library();
                bool correctFormat = false;
                try
                {
                    using (StreamReader sr = new StreamReader(file.FullName))
                    {
                        library.LibraryTitle = sr.ReadLine();
                        library.Address = sr.ReadLine();
                        library.MobileNumber = Convert.ToDouble(sr.ReadLine());

                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            correctFormat = true;
                            string[] parts = line.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                            string title = parts[0];
                            string type = parts[1];
                            string publisher = parts[2];
                            int releaseYear = Convert.ToInt32(parts[3]);
                            int pageCount = Convert.ToInt32(parts[4]);
                            int copies = Convert.ToInt32(parts[5]);
                            switch (type)
                            {
                                case "Newspaper":
                                    {
                                        int releaseNumber = Convert.ToInt32(parts[6]);
                                        int releaseMonth = Convert.ToInt32(parts[7]);
                                        int releaseDay = Convert.ToInt32(parts[8]);
                                        Newspaper newspaper = new Newspaper(releaseNumber, releaseMonth, releaseDay, title, type, publisher, releaseYear, pageCount, copies);
                                        library.AddPublication(newspaper);
                                        break;
                                    }
                                case "Journal":
                                    {
                                        int releaseNumber = Convert.ToInt32(parts[6]);
                                        int releaseMonth = Convert.ToInt32(parts[7]);
                                        double isbn = Convert.ToDouble(parts[8]);
                                        Journal journal = new Journal(isbn, releaseNumber, releaseMonth, title, type, publisher, releaseYear, pageCount, copies);
                                        library.AddPublication(journal);
                                        break;
                                    }
                                case "Book":
                                    {
                                        string author = parts[6];
                                        double isbn = Convert.ToDouble(parts[7]);
                                        Book book = new Book(author, isbn, title, type, publisher, releaseYear, pageCount, copies);
                                        library.AddPublication(book);
                                        break;
                                    }
                                default:
                                    {
                                        throw new Exception("Publication's type is misstyped, or type is not supported by program. Please" +
                                            "fix input file and try again!");
                                    }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TableCell cell = new TableCell();
                    TableRow row = new TableRow();
                    cell.Text = $"{file.Name} file is format is corrupted";
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                    table.Visible = true;
                    continue;
                }
                if (correctFormat)
                    libraries.Add(library);
                else
                {
                    TableCell cell = new TableCell();
                    TableRow row = new TableRow();
                    cell.Text = $"{file.Name} file is empty or corrupted";
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                    table.Visible = true;
                }
            }
            return libraries;
        }

        /// <summary>
        /// Prints publication list to csv file format
        /// </summary>
        /// <param name="source">Source list</param>
        /// <param name="path">Path to print</param>
        public static void ListToCSV(List<Publication> source, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, encoding: System.Text.Encoding.Default))
            {
                sw.WriteLine($"{"Leidinio pavadinimas"};{"Tipas"};{"Leidykla"};{"Išleidimo metai"};{"psl. sk."};{"Tiražas"};{"Leidinio nr."};{"Išleidimo mėn."};{"Išleidimo d."};{"Autorius"};{"ISBN"}");
                foreach (var item in source)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.WriteLine();
            }
        }
        
        /// <summary>
        /// Prints source list to text file
        /// </summary>
        /// <param name="source">Source list</param>
        /// <param name="path">path to write data to</param>
        /// <param name="header">Header of section</param>
        public static void ListToTxt(List<Publication> source, string path, string header)
        {
            string longLine = new string('-', 167);
            using (StreamWriter sw = new StreamWriter(path, true, encoding: System.Text.Encoding.Default))
            {
                sw.WriteLine(header);

                sw.WriteLine(longLine);
                sw.WriteLine($"{"Leidinio pavadinimas", -30}|{"Tipas", -15}|{"Leidykla", -15}|{"Išleidimo metai",-15}|{"psl.", -5}|{"Tiražas", -8}|{"Leidinio nr.", -14}|{"Išl. mėn.", -10}|{"Išleidimo d.",-13 }|{"Autorius", -20}|{"ISBN", -12}|");
                sw.WriteLine(longLine);
                if(source.Count > 0)
                {
                    foreach (Publication publication in source)
                    {
                        sw.WriteLine(publication.ToStringTxt());
                    } 
                }
                else
                {
                    sw.WriteLine("Sąrašas yra tuščias");
                }
                sw.WriteLine(longLine);
                sw.WriteLine();
            }
        }
    }
}