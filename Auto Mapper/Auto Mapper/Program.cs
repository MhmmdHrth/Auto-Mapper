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
                cfg.CreateMap<Source, Destination>().ValueTransformers.Add<string>(val => $"{val} Hensem");
                cfg.CreateMap<StringClass, IntClass>();
                cfg.CreateMap(typeof(GenSource<>), typeof(GenDestination<>));

                cfg.AddProfile<MappingProfile>();

                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                cfg.CreateMap<string, DateTime>().ConvertUsing<DateTimeTypeConverter>();

                cfg.CreateMap<Value, Total>()
                    .ForMember(dest => dest.total, option => option.MapFrom<CustomeResolver>());

                cfg.CreateMap<Person, PersonDto>()
                    .BeforeMap((src, dest) => src.Age = src.Age + 10)
                    .AfterMap<CallMeJohnDoe>();
            });

            var mapper = new Mapper(configuration);

            //Profile Mapping
            //var source2 = new Source2
            //{
            //    Date = new DateTime(2018, 3, 3, 12, 45, 56)
            //};

            //var destination = mapper.Map<Destination2>(source2);

            //Console.WriteLine(destination.Hour.Equals(source2.Date.Hour));

            //Converter
            //var StringClass = new StringClass
            //{
            //    Number = "5",
            //    Date = "12/02/2019" //month|Day|Year
            //};

            //var IntClass = mapper.Map<IntClass>(StringClass);

            //Console.WriteLine(IntClass.Number);
            //Console.WriteLine(IntClass.Date);


            //custom resolver
            //var value = new Value
            //{
            //    Value1 = 4,
            //    Value2 = 5
            //};

            //var result = mapper.Map<Total>(value);
            //Console.WriteLine(result.total);

            //Value transformer
            //var source = new Source
            //{
            //    Greeting = "Muhammad Harith"
            //};

            //var destination = mapper.Map<Destination>(source);
            //Console.WriteLine(destination.Greeting);

            //Before and after map action
            //var person = new Person
            //{
            //    Name = "Harith"
            //};

            //var destination = mapper.Map<PersonDto>(person);
            //Console.WriteLine($"{destination.Name} {destination.Age}");

            //generic
            var source = new GenSource<int>
            {
                Value = 123
            };

            var destination = mapper.Map<GenSource<int>>(source);
            Console.WriteLine(destination.Value);
        }
    }

    public class Source
    {
        public int Value { get; set; }
        public string Greeting { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
        public string Greeting { get; set; }
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

    public class Value
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }

    public class Total
    {
        public int total { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class PersonDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class GenSource<T>
    {
        public T Value { get; set; }
    }

    public class GenDestination<T>
    {
        public T Value { get; set; }
    }

    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return System.Convert.ToDateTime(source);
        }
    }

    public class CustomeResolver : IValueResolver<Value, Total, int>
    {
        public int Resolve(Value source, Total destination, int destMember, ResolutionContext context)
        {
            return source.Value1 + source.Value2;
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

    public class CallMeJohnDoe : IMappingAction<Person, PersonDto>
    {
        public void Process(Person source, PersonDto destination, ResolutionContext context)
        {
            destination.Name = $"John {destination.Name}";
        }
    }




}
