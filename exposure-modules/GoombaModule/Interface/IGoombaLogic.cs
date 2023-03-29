using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.GoombaModule.Interface
{
    public interface IGoombaLogic
    {
        IGoomba GenerateGoomba();
        IGoomba GenerateSpecificGoomba(string className);
        IGoomba GenerateCustomGoomba(string className, int size, int attack, int defense);
    }
}
