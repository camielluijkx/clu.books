namespace clu.books.library.Mapping
{
    public static class ModelToDtoMapper
    {
        public static dto.Book ToDto(this model.Book value)
        {
            return new dto.Book
            {
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
