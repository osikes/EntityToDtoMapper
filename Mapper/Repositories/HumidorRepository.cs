using EntityToDtoMapper.Dtos;
using EntityToDtoMapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper.Repositories
{
    public class HumidorRepository
    {

        HumidorContext context = new HumidorContext();

        public List<HumidorDto> GetHumidors()
        {
            var humidors = context.Humidors.ToList().Select(DtoMapper.Map<Humidor, HumidorDto>);

            return humidors.ToList();
        }
    }
}
