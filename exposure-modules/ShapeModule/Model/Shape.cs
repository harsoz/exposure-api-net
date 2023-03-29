using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.ShapeModule.Model
{
    public class Shape
    {
        public string Name { get; set; }
        public string Binary { get; set; }
        public List<Param> Params { get; set; }
    }

    public class Param
    {
        public int Id { get; set; } // it will exist only when created
        public int Order { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
