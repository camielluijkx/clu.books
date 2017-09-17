using System;
using System.Threading.Tasks;
using clu.books.library.output;
using clu.books.library.search;
using clu.books.library.settings;
using clu.books.library.Search;

namespace clu.books.console
{
    class Program
    {
        private static IConfigurationSettings configurationSettings;

        private static IBookSearchMapper bookSearchMapper;
        private static IBookSearchService bookSearchService;
        private static IBookOutputService bookOutputService;

        private static void Initialize()
        {
            Console.WriteLine("Initializing...");

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("****************************************************************");
            Console.WriteLine("*******************           Books          *******************");
            Console.WriteLine("****************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.Blue;

            Console.WriteLine("\t\t\t\t\t\t\t\t");
            Console.WriteLine(@"             .--.           .---.        .-.                    ");
            Console.WriteLine(@"         .---|--|   .-.     | A |  .---. |~|    .--.            ");
            Console.WriteLine(@"      .--|===|Ch|---|_|--.__| S |--|:::| |~|-==-|==|---.        ");
            Console.WriteLine(@"      |%%|NT2|oc|===| |~~|%%| C |--|   |_|~|CATS|  |___|-.      ");
            Console.WriteLine(@"      |  |   |ah|===| |==|  | I |  |:::|=| |    |GB|---|=|      ");
            Console.WriteLine(@"      |  |   |ol|   |_|__|  | I |__|   | | |    |  |___| |      ");
            Console.WriteLine(@"      |~~|===|--|===|~|~~|%%|~~~|--|:::|=|~|----|==|---|=|      ");
            Console.WriteLine(@"      ^--^---'--^---^-^--^--^---'--^---^-^-^-==-^--^---^-'      ");
            Console.WriteLine("\t\t\t\t\t\t\t\t");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static async Task FeelingLuckyAsync()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("not implemented yet");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static async Task SearchBookByIsbnAsync()
        {
            Console.WriteLine("");
            Console.WriteLine("Please enter ISBN, or part of it (example: 9781876175702)");
            string isbn = Console.ReadLine();
            BookSearchRequest searchRequest = new BookSearchRequest(isbn, SearchOption.ByIsn);
            BookSearchResponse searchResponse = await bookSearchService.SearchBookAsync(searchRequest);
            bookOutputService.LogBookInformation(searchResponse.Book);
        }

        private static async Task SearchBooksByIsbnAsync()
        {
            Console.WriteLine("");
            Console.WriteLine("Please enter ISBN, or part of it (example: 9781876175702)");
            string isbn = Console.ReadLine();
            BooksSearchRequest searchRequest = new BooksSearchRequest(isbn, SearchOption.ByIsn);
            BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);
            bookOutputService.LogBooksInformation(searchResponse.Books);
        }

        private static async Task SearchBooksByAuthorAsync()
        {
            Console.WriteLine("");
            Console.WriteLine("Please enter author, or part of it (example: Che Guevara)");
            string author = Console.ReadLine();
            BooksSearchRequest searchRequest = new BooksSearchRequest(author, SearchOption.ByAuthor);
            BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);
            bookOutputService.LogBooksInformation(searchResponse.Books);
        }

        private static async Task SearchBooksByTitleAsync()
        {
            Console.WriteLine("");
            Console.WriteLine("Please enter title, or part of it (example: The Motorcycle Diaries)");
            string title = Console.ReadLine();
            BooksSearchRequest searchRequest = new BooksSearchRequest(title, SearchOption.ByTitle);
            BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);
            bookOutputService.LogBooksInformation(searchResponse.Books);
        }

        private static async Task SearchBooksByAnythingAsync()
        {
            Console.WriteLine("");
            Console.WriteLine("Please enter anything, or part of it (example: Diary of Che)");
            string anything = Console.ReadLine();
            BooksSearchRequest searchRequest = new BooksSearchRequest(anything, SearchOption.ByAnything);
            BooksSearchResponse searchResponse = await bookSearchService.SearchBooksAsync(searchRequest);
            bookOutputService.LogBooksInformation(searchResponse.Books);
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Hello {0}, what would you like to do?", Environment.UserName);

            char choice = ' ';
            while (choice != '0')
            {
                Console.WriteLine("");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Search by ISBN (best match is returned)");
                Console.WriteLine("[2] Search by ISBN (top matches are returned)");
                Console.WriteLine("[3] Search by author (top matches are returned)");
                Console.WriteLine("[4] Search by title (top matches are returned)");
                Console.WriteLine("[?] Feeling lucky (do a random keyword search)");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[!] Search by anything <-- You want this option");
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                choice = consoleKey.KeyChar;

                if (choice == '1')
                {
                    SearchBookByIsbnAsync().Wait();
                }
                if (choice == '2')
                {
                    SearchBooksByIsbnAsync().Wait();
                }
                if (choice == '3')
                {
                    SearchBooksByAuthorAsync().Wait();
                }
                if (choice == '4')
                {
                    SearchBooksByTitleAsync().Wait();
                }
                else if (choice == '?')
                {
                    FeelingLuckyAsync().Wait();
                }
                else if (choice == '!')
                {
                    SearchBooksByAnythingAsync().Wait();
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                configurationSettings = new ConfigurationSettings();

                bookSearchMapper = new BookSearchMapper();
                bookSearchMapper.Configure();

                bookSearchService = new BookSearchService(configurationSettings, bookSearchMapper);
                bookOutputService = new BookOutputService(configurationSettings);

                Initialize();
                ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
