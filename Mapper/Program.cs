using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityToDtoMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var ef = new EntityClass()
            {
                Id = 1,
                Name = "df",
                Bob = "asdf"
            };

            var dto = DtoMapper.Map<EntityClass, DtoClass>(ef);

            Console.WriteLine(dto.Id.ToString());
        }
    }
} 