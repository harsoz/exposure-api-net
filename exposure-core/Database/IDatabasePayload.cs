using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_core.Database
{
    public interface IDatabasePayload<T,M>
    {
        string Name { get; set; }
        T Type { get; set; }
        List<Parameter<M>> Parameters { get; set; }
    }

    public class Parameter<T>
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public T Type { get; set; }
    }
}
