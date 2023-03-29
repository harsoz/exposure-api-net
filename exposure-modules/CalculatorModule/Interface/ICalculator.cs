using exposure_modules.CalculatorModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.CalculatorModule.Interface
{
    /// <summary>
    /// Calculator interface
    /// </summary>
    public interface ICalculator
    {
        List<Nomination> Calculate(decimal amount, string country);
    }
}
