using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper
{
    public class EntityClass
    {
        [MapMe]
        public int Id { get; set; }

        [MapMe]
        public string Name { get; set; }

        public string Bob { get; set; }
    }
}
