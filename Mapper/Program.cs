using EntityToDtoMapper.Repositories;
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
            var repository = new HumidorRepository();

            var dtos = repository.GetHumidors();
        }
    }
} 