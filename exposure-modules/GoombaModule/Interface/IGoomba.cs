using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.GoombaModule.Interface
{
    public interface IGoomba
    {
        int size { get; set; }
        int attack { get; set; }
        int defense { get; set; }
    }
}
