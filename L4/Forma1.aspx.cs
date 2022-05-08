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
        const string CFR1 = "~/App_Data/Reti.csv";
        const string CFR2 = "~/App_Data/Nenauji.csv";
        const string CFR3 = "~/App_Data/Technologija.csv";
        public static Library library1;
        public static Library library2;
        public static Library library3;
        public static List<Publication> maxCopy1;
        public static List<Publication> maxCopy2;
        public static List<Publication> maxCopy3;

        public static List<Publication> library4;


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Read libraries info from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label4.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
            Label7.Visible = false;
            try
            {
                if (Directory.Exists(Server.MapPath("~/App_Data/")))
                {
                    Directory.Delete(Server.MapPath("~/App_Data/"), true);
                }
            }
            catch (IOException ex)
            {
                Label7.Text = String.Format("Please close opened result files ({0})", ex.Message);
                Label7.Visible = true;
                return;
            }
           

            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/App_Data/"));

            directory.Create();
            try
            {
                SaveFilesToDirectory(directory, FileUpload1, Table1);
            }

            catch(Exception ex)
            {
                Label7.Visible = true;
                Label7.Text = ex.Message;
                return;
            }

            List<Library> allData;
            try
            {
                allData = ReadDataFromFiles(directory, Table1);
            }
            catch (Exception ex)
            {
                Label8.Visible = true;
                Label8.Text = ex.Message;
                return;
            }


            string resultPath = directory + "Rez.txt";

            foreach (var item in allData)
            {
                InOut.ListToTxt(item.Books, resultPath, item.ToString());
            }

            try
            {
                List<Table> tables = new List<Table>();
                tables.Add(Table1);
                tables.Add(Table2);
                tables.Add(Table3);

                List<Label> labels = new List<Label>();
                labels.Add(Label1);
                labels.Add(Label2);
                labels.Add(Label3);
                if(allData.Count < 3)
                {
                    for (int i = 0; i < allData.Count; i++)
                    {
                        DrawTable(allData[i].Books, tables[i]);
                        labels[i].Text = allData[i].ToString();
                        labels[i].Visible = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Label7.Text = $"Please select exatly 3 library files({ex.Message})";
                Label7.Visible = true;
                Table1.Visible = false;
                Table2.Visible = false;
                Table3.Visible = false;
                Table4.Visible = false;
                return;
            }
            //----------------------------Most Copies-------------------------

            List<Publication> MaxCopies1;
            List<Publication> MaxCopies2;
            List<Publication> MaxCopies3;
    
            try
            {
                MaxCopies1 = TaskUtils.MostCopies(allData[0].Books);
                MaxCopies2 = TaskUtils.MostCopies(allData[1].Books);
                MaxCopies3 = TaskUtils.MostCopies(allData[2].Books);
            }
            catch(Exception ex)
            {
                Label7.Visible = true;
                Label7.Text = "Please upload exatly 3 files";
                return;
            }
            

            Label7.Visible = true;
            Label7.Text = "Filtered books with most copies of each type from all libraries";

            Table1.Visible = true;
            Table2.Visible = true;
            Table3.Visible = true;
            Table4.Visible = true;

            try
            {
                DrawMostCopiesTable(MaxCopies1, Table5, Label4, allData[0].ToString());
                DrawMostCopiesTable(MaxCopies2, Table6, Label5, allData[1].ToString());
                DrawMostCopiesTable(MaxCopies3, Table7, Label6, allData[2].ToString());
            }
            catch(Exception ex)
            {
                Label9.Visible = true;
                Label9.Text = "Please deselect empty files and try again";
                return;
            }
            

            //----------------------------Unique---------------------------------

            List<Publication> unique = TaskUtils.UniquePublications(allData);

            TaskUtils.ClearDirectory(Server.MapPath(CFR1));

            InOut.ListToCSV(unique, Server.MapPath(CFR1));
            InOut.ListToTxt(unique, resultPath, "Unique publications from all libraries");

            //---------------------------Not new publications-------------------------------------
            List<Publication> NotNew = new List<Publication>();
            try
            {
                NotNew = TaskUtils.NotNewPublications(allData);
            }
            catch(Exception ex)
            {
                Label7.Text = ex.Message;
                Label7.Visible = true;
                return;
            }


            //Splits list to 3 parts by type, sorts, and combines to single list
            List<Publication> newspapers = TaskUtils.FilterByType<Newspaper>(NotNew);
            List<Publication> journal = TaskUtils.FilterByType<Journal>(NotNew);
            List<Publication> book = TaskUtils.FilterByType<Book>(NotNew);

            TaskUtils.Sort(newspapers);
            TaskUtils.Sort(journal);
            TaskUtils.Sort(book);
            List<Publication> combined = new List<Publication>();

            combined = combined.Concat(newspapers)
                               .Concat(journal)
                               .Concat(book)
                               .ToList();

            TaskUtils.ClearDirectory(Server.MapPath(CFR2));

            if (combined.Count > 0)
            {
                InOut.ListToCSV(combined, Server.MapPath(CFR2));
                InOut.ListToTxt(combined, resultPath, "Not new publications in all libraries");
            }
            else
            {
                File.AppendAllText(Server.MapPath(CFR2), "There are no not new publications");
                File.AppendAllText(resultPath, "There are no not new publicaitons");
            }

            //-------------------------------------Specific publisher-----------------------------------
            string publisher = "Technologija";
            List<Publication> specific = TaskUtils.SpecificPublisher(allData, publisher);

            TaskUtils.ClearDirectory(Server.MapPath(CFR3));

            if(specific.Count > 0)
            {
                InOut.ListToCSV(specific, Server.MapPath(CFR3));
                InOut.ListToTxt(specific, resultPath, $"{publisher}'s books in all libraries");
            }
            else
            {
                File.AppendAllText(Server.MapPath(CFR3), $"There are no {publisher}'s books");
                File.AppendAllText(Server.MapPath(resultPath), $"There are no {publisher}'s books");
            }

        }
    }
}