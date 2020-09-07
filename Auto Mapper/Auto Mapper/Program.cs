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
            });

            var mapper = new Mapper(configuration);
            //OR
            var mapper2 = configuration.CreateMapper();

            var source = new Source { Value = 1 };

            var destination = mapper.Map<Destination>(source);
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
}
