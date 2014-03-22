using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper.Entities
{
    public class Cigar
    {
        [Key]
        public int CigarId { get; set; }

        public string Name { get; set; }

        public int HumidorId { get; set; }

        public virtual Humidor Humidor { get; set; }
    }
}
