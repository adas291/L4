using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Web.UI.WebControls;


namespace L4.Code
{
    public class InOut
    {
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
        

        public static void ListToTxt(List<Publication> source, string path, string header)
        {
            string longLine = new string('-', 155);
            using (StreamWriter sw = new StreamWriter(path, true, encoding: System.Text.Encoding.Default))
            {
                sw.WriteLine(header);

                sw.WriteLine(longLine);
                sw.WriteLine($"{"Leidinio pavadinimas", -30}|{"Tipas", -15}|{"Leidykla", -15}|{"Išleidimo metai",-15}|{"psl.", -5}|{"Tiražas", -8}|{"Leidinio nr.", -14}|{"Išl. mėn.", -10}|{"Išleidimo d.",-13 }|{"Autorius", -20}|{"ISBN", -12}|");
                sw.WriteLine(longLine);

                foreach (Publication publication in source)
                {
                    sw.WriteLine(publication.ToStringTxt());
                }

                sw.WriteLine(longLine);
                sw.WriteLine();
            }
        }
        public static void ReadFromFile(string path, Library library)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    library.LibraryTitle = sr.ReadLine();
                    library.Address = sr.ReadLine();
                    library.MobileNumber = Convert.ToDouble(sr.ReadLine());
                }
                catch (Exception ex)
                {

                    throw new Exception($"The library info of {path} is not correct");
                }
                if(library.LibraryTitle == "")
                {
                    throw new Exception($"{path} file is empty.");
                    return;
                }
                
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
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
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("File is not formatted correctly please fix and try again.");
                    }
                }
            }
        }
    }
}