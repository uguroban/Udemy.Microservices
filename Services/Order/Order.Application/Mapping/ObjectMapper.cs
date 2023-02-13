using AutoMapper;

namespace Order.Application.Mapping;

public static class ObjectMapper
{
    //Sadece istenilen anda initialize edilir
    private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AllProfiles>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => _lazy.Value;
}