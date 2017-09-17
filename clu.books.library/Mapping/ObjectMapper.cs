using AutoMapper;

namespace clu.books.library.Mapping
{
    public abstract class ObjectMapper : IObjectMapper
    {
        private IMapper mapper;

        public void Configure()
        {
            MapperConfiguration config = new MapperConfiguration(CreateMappings);
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }

        protected abstract void CreateMappings(IMapperConfigurationExpression config);

        public TDestination Map<TDestination>(object input)
        {
            return mapper.Map<TDestination>(input);
        }

        public TDestination Map<TSource, TDestination>(TSource input)
        {
            return mapper.Map<TSource, TDestination>(input);
        }

        public TDestination Map<TSource, TDestination>(TSource input, TDestination destination)
        {
            return mapper.Map<TSource, TDestination>(input, destination);
        }
    }
}
