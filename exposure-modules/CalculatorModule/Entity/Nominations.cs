using exposure_modules.CalculatorModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.CalculatorModule.Entity
{
    public class Nominations
    {
        public List<Nomination> getNominations(string country)
        {
            return NominationConfig.Coins.Where(a => a.Country == country).OrderByDescending(a => a.Value).ToList();
        }
    }
}
