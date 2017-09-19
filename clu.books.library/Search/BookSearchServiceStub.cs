using System.Collections.Generic;
using System.Threading.Tasks;
using clu.books.library.model;
using clu.books.library.search;
using Google.Apis.Books.v1.Data;

namespace clu.books.library.Search
{
    public class BookSearchServiceStub : IBookSearchService
    {
        private readonly IBookSearchMapper bookSearchMapper;

        public BookSearchServiceStub(IBookSearchMapper bookSearchMapper)
        {
            this.bookSearchMapper = bookSearchMapper;
        }

        private Task<Book> GetDefaultBookAsync(int index = 1)
        {
            Volume volume = new Volume
            {
                VolumeInfo = new Volume.VolumeInfoData
                {
                    Authors = new List<string> { "Ernesto Guevara" },
                    Title = "The motorcycle diaries",
                    PublishedDate = "2003",
                    Language = "EN"
                }
            };

            Book book = new Book(volume, index);

            return Task.FromResult(book);
        }

        public async Task<BookSearchResponse> SearchBookAsync(BookSearchRequest searchRequest)
        {
            Book book = await GetDefaultBookAsync();

            return new BookSearchResponse(
                bookSearchMapper.Map<dto.Book>(book));
        }

        public async Task<BooksSearchResponse> SearchBooksAsync(BooksSearchRequest searchRequest)
        {
            int index = 1;

            dto.Books books = new dto.Books();

            for (int i = 0; i < 3; i++)
            {
                Book book = await GetDefaultBookAsync(index++);

                books.Add(bookSearchMapper.Map<dto.Book>(book));
            }

            return new BooksSearchResponse(books);
        }
    }
}
