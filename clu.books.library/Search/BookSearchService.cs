using System;
using System.Linq;
using System.Threading.Tasks;
using clu.books.library.converters;
using clu.books.library.extensions;
using clu.books.library.model;
using clu.books.library.settings;
using clu.books.library.Search;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace clu.books.library.search
{
    public class BookSearchService : IBookSearchService
    {
        private readonly string apiKey;

        private const int startIndex = 0;
        private readonly int maxResults;

        private readonly string[] preferredLanguages;

        private readonly VolumesResource.ListRequest.OrderByEnum orderBy;

        private readonly BooksService bookService;

        private readonly IBookSearchMapper bookSearchMapper;

        public BookSearchService(IConfigurationSettings configurationSettings, IBookSearchMapper bookSearchMapper)
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

            this.bookSearchMapper = bookSearchMapper;
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
            volumes?.Items?.RemoveAll(v => !preferredLanguages
                .Contains(v.VolumeInfo?.Language?.ToUpper()));
            return volumes;
        }

        public async Task<BookSearchResponse> SearchBookAsync(BookSearchRequest searchRequest)
        {
            string searchTerm = searchRequest.SearchTerm;
            SearchOption searchOption = searchRequest.SearchOption;

            Volumes volumes = await SearchVolumesAsync(searchOption, searchTerm);
            Book book = new Book(volumes?.Items?.FirstOrDefault());

            BookSearchResponse searchResponse = new BookSearchResponse(
                bookSearchMapper.Map<dto.Book>(book));
            return searchResponse;
        }

        public async Task<BooksSearchResponse> SearchBooksAsync(BooksSearchRequest searchRequest)
        {
            string searchTerm = searchRequest.SearchTerm;
            SearchOption searchOption = searchRequest.SearchOption;

            Volumes volumes = await SearchVolumesAsync(searchOption, searchTerm);
            Books books = new Books(volumes?.Items?.ToList());

            BooksSearchResponse searchResponse = new BooksSearchResponse(
                bookSearchMapper.Map<dto.Books>(books));
            return searchResponse;
        }
    }
}
