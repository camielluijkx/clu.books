using System;
using System.IO;
using System.Linq;
using clu.books.library.model;
using clu.books.library.settings;

namespace clu.books.library.output
{
    public class BookOutputService : IBookOutputService
    {
        private readonly bool logToConsoleEnabled;

        private readonly bool logToFileEnabled;
        private readonly string outputFile;

        public BookOutputService(IConfigurationSettings configurationSettings)
        {
            logToConsoleEnabled = configurationSettings.OutputSearchResultsToConsole;

            outputFile = configurationSettings.OutputFileForSearchResults;
            logToFileEnabled = configurationSettings.OutputSearchResultsToFile;
        }

        private void LogToFile(string bookInformation, TextWriter writer)
        {
            writer.Write("\r\nLog Entry : ");
            writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            writer.WriteLine("  :");
            writer.WriteLine("  :{0}", bookInformation);
            writer.WriteLine("-------------------------------");
        }

        private void LogToConsole(string bookInformation)
        {
            if (logToConsoleEnabled)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(bookInformation);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void LogToFile(string bookInformation)
        {
            if (logToFileEnabled)
            {
                using (StreamWriter writer = File.AppendText(outputFile))
                {
                    LogToFile(bookInformation, writer);
                }
            }
        }

        public void LogBookInformation(Book book)
        {
            if (book == null)
            {
                return;
            }

            string bookInformation = book.ToString();

            LogToConsole(bookInformation);
            LogToFile(bookInformation);
        }

        public void LogBooksInformation(Books books)
        {
            if (books == null || !books.Any())
            {
                return;
            }
            books.ForEach(LogBookInformation);
        }
    }
}
