using System.Collections.Generic;
using System.Linq;
using Google.Apis.Books.v1.Data;

namespace clu.books.library.model
{
    public class Books : List<Book>
    {
        private readonly List<Volume> volumes;

        private void Initialize()
        {
            if (volumes == null || !volumes.Any())
            {
                return;
            }

            int index = 1;

            foreach (Volume volume in volumes)
            {
                Add(new Book(volume, index++));
            }
        }

        public Books(List<Volume> volumes)
        {
            this.volumes = volumes;

            Initialize();
        }
    }
}
