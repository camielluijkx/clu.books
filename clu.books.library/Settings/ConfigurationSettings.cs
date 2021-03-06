﻿namespace clu.books.library.settings
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        public string GoogleBooksPublicApiKey => AppSettings.Get<string>("GoogleBooksPublicApiKey");

        public string OutputFileForSearchResults => AppSettings.Get<string>("OutputFileForSearchResults");

        public bool OutputSearchResultsToConsole => AppSettings.Get<bool>("OutputSearchResultsToConsole");

        public bool OutputSearchResultsToFile => AppSettings.Get<bool>("OutputSearchResultsToFile");

        public string OrderSearchResultsBy => AppSettings.Get<string>("OrderSearchResultsBy");

        public int MaxSearchResults => AppSettings.Get<int>("MaxSearchResults");

        public string PreferredLanguage => AppSettings.Get<string>("PreferredLanguage");

        public bool StubSearchResults => AppSettings.Get<bool>("StubSearchResults");

        public string StubFileForSearchResults => AppSettings.Get<string>("StubFileForSearchResults");

        public string ServiceAccountPrivateKeyFile => AppSettings.Get<string>("ServiceAccountPrivateKeyFile");
    }
}
