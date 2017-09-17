using AutoMapper;
using clu.books.library.Mapping;

namespace clu.books.library.Search
{
    public class BookSearchMapper : ObjectMapper, IBookSearchMapper
    {
        protected override void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<model.Book, dto.Book>()
                //.ForMember(P => P.Information, q => q.Ignore());
                .ForMember(p => p.Information, q => q.MapFrom(r => r.ToString()));
        }
    }
}
