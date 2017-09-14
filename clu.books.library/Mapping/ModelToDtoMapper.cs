using System.Linq.Expressions;

namespace clu.books.library.Mapping
{
    public static class ModelToDtoMapper
    {
        public static dto.Book ToDto(this model.Book value)
        {
            return new dto.Book
            {
                Index = value.Index,
                Author = value.Author,
                Title = value.Title,
                PublishedDate = value.PublishedDate,
                LanguageCode = value.LanguageCode,

                Description = value.ToString()
            };
        }

        public static dto.Books ToDto(this model.Books value)
        {
            dto.Books books = new dto.Books();

            foreach (model.Book book in value)
            {
                books.Add(book.ToDto());
            }

            return books;
        }
    }
}
