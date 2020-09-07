using AutoMapper;
using System;
using System.Xml.Xsl;

namespace Auto_Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //create mapper configuration
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Source, Destination>();
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = new Mapper(configuration);

            var source2 = new Source2
            {
                Date = new DateTime(2018, 3, 3, 12, 45, 56)
            };

            var destination = mapper.Map<Destination2>(source2);

            Console.WriteLine(destination.Hour.Equals(source2.Date.Hour));


        }
    }

    public class Source
    {
        public int Value { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
    }

    public class Source2
    {
        public DateTime Date { get; set; }
    }

    public class Destination2
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Source2, Destination2>()
                .ForMember(dest => dest.Hour, option => option.MapFrom(src => src.Date.Hour))
                .ForMember(dest => dest.Minute, option => option.MapFrom(src => src.Date.Minute))
                .ForMember(dest => dest.Second, option => option.MapFrom(src => src.Date.Second));
        }
    }


}
