using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace clu.books.library
{
    public class BookSearchService : IBookSearchService
    {
        private readonly string apiKey;

        private const int startIndex = 0;
        private readonly int maxResults;

        private string[] preferredLanguages;

        private readonly VolumesResource.ListRequest.OrderByEnum orderBy;

        private readonly BooksService bookService;

        public BookSearchService(IConfigurationSettings configurationSettings)
        {
            apiKey = configurationSettings.GoogleBooksPublicApiKey;

            maxResults = configurationSettings.MaxSearchResults;

            preferredLanguages = configurationSettings.PreferredLanguage?.Split('|');

            orderBy = configurationSettings.OrderSearchResultsBy
                .ConvertToEnum<VolumesResource.ListRequest.OrderByEnum>();

            bookService = new BooksService(
                new BaseClientService.Initializer
                {
                    ApiKey = apiKey,
                    ApplicationName = "Books"
                });
        }

        private enum SearchOption
        {
            ByAnything,
            ByAuthor,
            ByIsn,
            ByTitle
        }


        private async Task<Volumes> SearchVolumesAsync(SearchOption searchOption, string searchValue)
        {
            string searchTerm = searchValue;

            switch (searchOption)
            {
                case SearchOption.ByAnything:
                    searchTerm = $"{searchValue}";
                    break;
                case SearchOption.ByAuthor:
                    searchTerm = $"inauthor:{searchValue}";
                    break;
                case SearchOption.ByIsn:
                    searchTerm = $"isbn:{searchValue}";
                    break;
                case SearchOption.ByTitle:
                    searchTerm = $"intitle:{searchValue}";
                    break;
            }

            VolumesResource.ListRequest listRequest = bookService.Volumes.List(searchTerm);
            listRequest.OrderBy = orderBy;
            listRequest.StartIndex = startIndex;
            listRequest.MaxResults = maxResults;

            Console.WriteLine($"Executing a book search request using {searchTerm}.");
            Volumes volumes = await listRequest.ExecuteAsync();
            volumes.Items.RemoveAll(v => !preferredLanguages.Contains(v.VolumeInfo?.Language?.ToUpper()));
            return volumes;
        }

        public async Task<Books> SearchBooksByAnythingAsync(string anything)
        {
            Volumes volumes = await SearchVolumesAsync(SearchOption.ByAnything, anything);
            return new Books(volumes?.Items?.ToList());
        }

        public async Task<Books> SearchBooksByAuthorAsync(string author)
        {
            Volumes volumes = await SearchVolumesAsync(SearchOption.ByAuthor, author);
            return new Books(volumes?.Items?.ToList());
        }

        public async Task<Book> SearchBookByIsbnAsync(string isbn)
        {
            // [TODO] validate with regex?
            Volumes volumes = await SearchVolumesAsync(SearchOption.ByIsn, isbn);
            Volume volume = volumes?.Items?.FirstOrDefault();
            return new Book(volume);
        }

        public async Task<Books> SearchBooksByIsbnAsync(string isbn)
        {
            // [TODO] validate with regex?
            Volumes volumes = await SearchVolumesAsync(SearchOption.ByIsn, isbn);
            return new Books(volumes?.Items?.ToList());
        }

        public async Task<Books> SearchBooksByTitleAsync(string title)
        {
            Volumes volumes = await SearchVolumesAsync(SearchOption.ByTitle, title);
            return new Books(volumes?.Items?.ToList());
        }
    }
}
