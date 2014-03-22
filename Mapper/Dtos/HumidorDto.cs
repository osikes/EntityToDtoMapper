using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper.Dtos
{
    public class HumidorDto
    {
        public int HumidorId { get; set; }

        public string Name { get; set; }
    }
}
