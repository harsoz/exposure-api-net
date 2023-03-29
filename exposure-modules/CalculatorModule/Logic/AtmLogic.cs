using exposure_modules.CalculatorModule.Entity;
using exposure_modules.CalculatorModule.Interface;
using exposure_modules.CalculatorModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.CalculatorModule.Logic
{
    public class AtmLogic : ICalculator
    {
        private readonly Nominations _coinService;

        public AtmLogic()
        {
            _coinService = new Nominations();
        }

        public List<Nomination> Calculate(decimal amount, string country)
        {
            if (amount == 0.0m) throw new ArgumentException("The total amount is mandatory");
            if (string.IsNullOrEmpty(country)) throw new ArgumentException("The country is mandatory");

            // get coins from country configure
            var coins = _coinService.getNominations(country);

            if (!coins.Any()) throw new ArgumentException("The country denomination provided is not available");

            List<Nomination> denominations = new List<Nomination>();
            // calculate the bill and coins uses
            coins.ForEach(delegate (Nomination nomination)
            {
                if (nomination.Value <= amount)
                {
                    int qty = (int)(amount / nomination.Value);
                    amount -= qty * nomination.Value;

                    denominations.Add(new Nomination { Country = country, Type = nomination.Type, Value = nomination.Value, Quantity = qty, Src = nomination.Src });
                }
            });

            return denominations;
        }
    }
}
