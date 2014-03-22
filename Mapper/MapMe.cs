using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityToDtoMapper
{
    public class MapMe:System.Attribute
    {
        public  string FindEntity { get; private set; }
        public  string FindProperty { get; private set; }

        public MapMe()
        {
            this.FindEntity = "";
            this.FindProperty="";
        }

        public MapMe(string findEntity, string findProperty)
        {
            this.FindEntity = findEntity;
            this.FindProperty = findProperty;
        }

    }

    public class FindThis : Attribute
    {

        private readonly string findEntity;
        private readonly string findProperty;

        public FindThis(string findEntity,string findProperty)
        {
            this.findEntity = findEntity;
            this.findProperty = findProperty;
        }

    }
}
