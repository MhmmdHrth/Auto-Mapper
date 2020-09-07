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
                cfg.CreateMap<StringClass, IntClass>();

                cfg.AddProfile<MappingProfile>();
                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                cfg.CreateMap<string, DateTime>().ConvertUsing<DateTimeTypeConverter>();
            });

            var mapper = new Mapper(configuration);

            //var source2 = new Source2
            //{
            //    Date = new DateTime(2018, 3, 3, 12, 45, 56)
            //};

            //var destination = mapper.Map<Destination2>(source2);

            //Console.WriteLine(destination.Hour.Equals(source2.Date.Hour));

            var StringClass = new StringClass
            {
                Number = "5",
                Date = "12/02/2019" //month|Day|Year
            };

            var IntClass = mapper.Map<IntClass>(StringClass);

            Console.WriteLine(IntClass.Number);
            Console.WriteLine(IntClass.Date);

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

    public class StringClass
    {
        public string Number { get; set; }
        public string Date { get; set; }
    }

    public class IntClass
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }

    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return System.Convert.ToDateTime(source);
        }
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
