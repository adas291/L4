using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using L4.Code;
using System.IO;

namespace L4
{
    public partial class Forma1 : System.Web.UI.Page
    {
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
                catch(Exception ex)
                {
                    TableCell cell = new TableCell();
                    TableRow row = new TableRow();
                    cell.Text = $"{file.Name} file is format is corrupted";
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                    table.Visible = true;
                    continue;
                }
                if(correctFormat)
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

        public static void ErrorDisplay(Table table, List<string> errors)
        {
            table.Visible = true;
            foreach (var item in errors)
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text = item;
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }
        }

        public static void SaveFilesToDirectory(DirectoryInfo directory,
            FileUpload fileUploads, Table table)
        {
            if (fileUploads.HasFiles == false)
            {
                throw new Exception("Uploaded files are not in correct format");
            }
            List<string> errors = new List<string>();

            foreach (var fileUpload in fileUploads.PostedFiles)
            {
                
                try
                {
                    if (fileUpload.FileName.EndsWith(".txt"))
                        fileUpload.SaveAs(Path.Combine(directory.FullName +
                            fileUpload.FileName));
                    else throw new Exception("The format of " + fileUpload.FileName
                        + " is incorect");
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }
            if (errors.Count > 0)
            {
                ErrorDisplay(table, errors);
                return;
            }
        }

        public static void DrawMostCopiesTable(List<Publication> library, Table table, Label label, string header)
        {
            TableCell title = new TableCell();
            TableCell Copies = new TableCell();

            title.Text = "<b>Leidinio pavadinimas</b>";
            Copies.Text = "<b>Leidinio tiražas</b>";
            TableRow row = new TableRow();
            row.Cells.Add(title);
            row.Cells.Add(Copies);
            table.Rows.Add(row);
            label.Text = header;
            label.Visible = true;
            try
            {
                if (library.Count == 0)
                {
                    TableCell empty = new TableCell();
                    TableRow r = new TableRow();
                    empty.Text = "Sąrašas yra tuščias";
                    r.Cells.Add(empty);
                    table.Rows.Add(r);
                    return;
                }
                foreach (var item in library)
                {
                    title = new TableCell();
                    Copies = new TableCell();
                    row = new TableRow();
                    title.Text = item.Title;
                    Copies.Text = item.Copies.ToString();
                    row.Cells.Add(title);
                    row.Cells.Add(Copies);
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Uploaded file is empty please deselect it and try again.({ex.Message})");
                
            }
        }

        public static void DrawTable(List<Publication> library, Table table)
        {
            TableCell name = new TableCell();
            TableCell type = new TableCell();
            TableCell publisher = new TableCell();
            TableCell years = new TableCell();
            TableCell pageCount = new TableCell();
            TableCell copyCount = new TableCell();
            TableCell releaseNumber = new TableCell();
            TableCell releaseMonth = new TableCell();
            TableCell releaseDay = new TableCell();
            TableCell author = new TableCell();
            TableCell ISBN = new TableCell();

            name.Text = "<b>Leidinio pavadinimas</b>";
            type.Text = "<b>Leidinio tipas</b>";
            publisher.Text = "<b>Leidykla</b>";
            years.Text = "<b>Išleidimo metai</b>";
            pageCount.Text = "<b>Puslapių skaičius</b>";
            copyCount.Text = "<b>tiražas</b>";
            releaseNumber.Text = "<b>Leidinio numeris</b>";
            releaseMonth.Text = "<b>Išleidimo mėnesis</b>";
            releaseDay.Text = "<b>Išleidimo diena</b>";
            author.Text = "<b>Lieidnio autorius</b>";
            ISBN.Text = "<b>ISBN</b>";

            TableRow titleRow = new TableRow();

            titleRow.Cells.Add(name);
            titleRow.Cells.Add(type);
            titleRow.Cells.Add(publisher);
            titleRow.Cells.Add(years);
            titleRow.Cells.Add(pageCount);
            titleRow.Cells.Add(copyCount);
            titleRow.Cells.Add(releaseNumber);
            titleRow.Cells.Add(releaseMonth);
            titleRow.Cells.Add(author);
            titleRow.Cells.Add(ISBN);
            table.Rows.Add(titleRow);
            try
            {
                foreach (var item in library)
                {
                    name = new TableCell();
                    type = new TableCell();
                    publisher = new TableCell();
                    years = new TableCell();
                    pageCount = new TableCell();
                    copyCount = new TableCell();
                    releaseNumber = new TableCell();
                    releaseMonth = new TableCell();
                    releaseDay = new TableCell();
                    author = new TableCell();
                    ISBN = new TableCell();

                    TableRow row = new TableRow();

                    if (item is Newspaper)
                    {
                        name.Text = item.Title;
                        type.Text = item.Type;
                        publisher.Text = item.Publisher;
                        years.Text = item.ReleaseYear.ToString();
                        pageCount.Text = item.PageCount.ToString();
                        copyCount.Text = item.Copies.ToString();
                        releaseNumber.Text = ((Newspaper)item).ReleaseNumber.ToString();
                        releaseMonth.Text = ((Newspaper)item).ReleaseMonth.ToString();
                        releaseDay.Text = ((Newspaper)item).ReleaseDay.ToString();
                        author.Text = "-";
                        ISBN.Text = "-";
                    }
                    else if (item is Journal)
                    {
                        name.Text = item.Title;
                        type.Text = item.Type;
                        publisher.Text = item.Publisher;
                        years.Text = item.ReleaseYear.ToString();
                        pageCount.Text = item.PageCount.ToString();
                        copyCount.Text = item.Copies.ToString();
                        releaseNumber.Text = ((Journal)item).ReleaseNumber.ToString();
                        releaseMonth.Text = ((Journal)item).ReleaseMonth.ToString();
                        releaseDay.Text = ((Journal)item).ISBN.ToString();
                        author.Text = "-";
                        releaseDay.Text = "-";
                    }
                    else if (item is Book)
                    {
                        name.Text = item.Title;
                        type.Text = item.Type;
                        publisher.Text = item.Publisher;
                        years.Text = item.ReleaseYear.ToString();
                        pageCount.Text = item.PageCount.ToString();
                        copyCount.Text = item.Copies.ToString();
                        releaseNumber.Text = "-";
                        releaseMonth.Text = "-";
                        releaseDay.Text = "-";
                        author.Text = ((Book)item).Author;
                        ISBN.Text = ((Book)item).ISBN.ToString();
                    }
                    row.Cells.Add(name);
                    row.Cells.Add(type);
                    row.Cells.Add(publisher);
                    row.Cells.Add(years);
                    row.Cells.Add(pageCount);
                    row.Cells.Add(copyCount);
                    row.Cells.Add(releaseNumber);
                    row.Cells.Add(releaseMonth);
                    row.Cells.Add(author);
                    row.Cells.Add(ISBN);

                    table.Rows.Add(row);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"File is empty, or corrupted please fix it.\n{ex.Message}");
            }
            

        }
    }
}