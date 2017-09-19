namespace clu.books.library.settings
{
    public interface IConfigurationSettings
    {
        string GoogleBooksPublicApiKey { get; }

        string OutputFileForSearchResults { get; }

        bool OutputSearchResultsToConsole { get; }

        bool OutputSearchResultsToFile { get; }

        string OrderSearchResultsBy { get; }

        int MaxSearchResults { get; }

        string PreferredLanguage { get; }

        bool StubSearchResults { get; }

        string StubFileForSearchResults { get; }
    }
}
