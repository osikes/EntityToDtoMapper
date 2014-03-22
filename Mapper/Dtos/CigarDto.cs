using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper.Dtos
{
    public class CigarDto
    {
        [MapMe]
        public int CigarId { get;set; }

        [MapMe]
        public string Name { get; set; }

        [MapMe]
        public int HumidorId { get; set; }

        [MapMe]
        public HumidorDto Humidor { get; set; }

        [MapMe("Humidor","Name")]
        public string HumidorName { get; set; }
    }
}
