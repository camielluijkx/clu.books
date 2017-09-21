using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using clu.books.library.model;
using clu.books.library.settings;
using Google.Apis.Books.v1.Data;
using Newtonsoft.Json;

namespace clu.books.library.Search
{
    public class StubbedBookSearchService : IStubbedBookSearchService
    {
        private readonly IBookSearchMapper bookSearchMapper;

        private readonly string stubFile;

        public StubbedBookSearchService(IConfigurationSettings configurationSettings, IBookSearchMapper bookSearchMapper)
        {
            this.bookSearchMapper = bookSearchMapper;

            stubFile = configurationSettings.StubFileForSearchResults;
        }

        private List<Volume> GetVolumesFromStubFile()
        {
            try
            {
                Volumes volumes;

                using (StreamReader file = File.OpenText(stubFile))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    volumes = (Volumes)serializer.Deserialize(file, typeof(Volumes));
                }

                return volumes?.Items?.ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw; // intended
            }
        }

        private Books GetBooksFromStubFile()
        {
            List<Volume> volumes = GetVolumesFromStubFile();

            Books books = new Books(volumes);

            return books;
        }

        public Task<BookSearchResponse> SearchBookAsync(BookSearchRequest searchRequest)
        {
            Books books = GetBooksFromStubFile();

            BookSearchResponse searchResponse = new BookSearchResponse(
                bookSearchMapper.Map<dto.Book>(books.FirstOrDefault()));

            return Task.FromResult(searchResponse);
        }

        public Task<BooksSearchResponse> SearchBooksAsync(BooksSearchRequest searchRequest)
        {
            Books books = GetBooksFromStubFile();

            BooksSearchResponse searchResponse = new BooksSearchResponse(
                bookSearchMapper.Map<dto.Books>(books));

            return Task.FromResult(searchResponse);
        }
    }
}
