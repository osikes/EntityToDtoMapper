using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper.Entities
{
    public class Humidor
    {
        [Key, MapMe]
        public int HumidorId { get; set; }
        
        [MapMe]
        public string Name { get; set; }

    }
}
