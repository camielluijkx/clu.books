using Google.Apis.Books.v1.Data;

namespace clu.books.library.model
{
    public class Book
    {
        private readonly int index;

        private readonly Volume volume;

        public int Index => index;

        public string Author => volume?.VolumeInfo?.Authors != null
            ? string.Join(", ", volume.VolumeInfo?.Authors)
            : "UNDEFINED";

        public string Title => volume?.VolumeInfo?.Title;

        public string PublishedDate => volume?.VolumeInfo?.PublishedDate;

        public string LanguageCode => volume?.VolumeInfo?.Language.ToUpper();

        public Book(Volume volume, int index = 1)
        {
            this.volume = volume;
            this.index = index;
        }

        public override string ToString()
        {
            return $"{index}) {Title} - {Author} - {PublishedDate} ({LanguageCode})";
        }
    }
}
