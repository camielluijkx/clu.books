namespace clu.books.library
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
    }
}
