using clu.books.library.search;
using clu.books.library.settings;

namespace clu.books.library.Search
{
    public class BookSearchServiceFactory: IBookSearchServiceFactory
    {
        private readonly IConfigurationSettings configurationSettings;

        private readonly IBookSearchMapper bookSearchMapper;

        public BookSearchServiceFactory(IConfigurationSettings configurationSettings, IBookSearchMapper bookSearchMapper)
        {
            this.configurationSettings = configurationSettings;

            this.bookSearchMapper = bookSearchMapper;
        }

        public IBookSearchService Create()
        {
            if (configurationSettings.StubSearchResults)
            {
                return new StubbedBookSearchService(configurationSettings, bookSearchMapper);
            }

            return new GoogleBookSearchService(configurationSettings, bookSearchMapper);
        }
    }
}
