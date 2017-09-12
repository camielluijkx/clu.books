using Google.Apis.Books.v1.Data;

namespace clu.books.library
{
    public class Book
    {
        private readonly int index;

        private readonly Volume volume;

        public Book(Volume volume, int index = 1)
        {
            this.volume = volume;
            this.index = index;
        }

        public override string ToString()
        {
            string title = volume?.VolumeInfo?.Title;
            string author = volume?.VolumeInfo?.Authors != null ? string.Join(
                ", ", volume.VolumeInfo?.Authors) : "UNDEFINED";
            string publishedDate = volume?.VolumeInfo?.PublishedDate;
            string languageCode = volume?.VolumeInfo?.Language.ToUpper();

            string information = $"{index}) {title} - {author} - {publishedDate} ({languageCode})";

            return information;
        }
    }
}
