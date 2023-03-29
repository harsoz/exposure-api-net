using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.CalculatorModule.Model
{
    public class Nomination
    {
        /// <summary>
        /// The country name
        /// </summary>
        /// <value>A name</value>
        public string Country { get; set; }

        /// <summary>
        /// The type of nomination, bill or coin
        /// </summary>
        /// <value></value>
        public string Type { get; set; }

        /// <summary>
        /// The value of the nomination
        /// </summary>
        /// <value>A single decimal</value>
        public decimal Value { get; set; }

        /// <summary>
        /// The quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// src path for icons
        /// </summary>
        public string Src { get; set; }
    }
}
