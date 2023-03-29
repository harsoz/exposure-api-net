using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_core.Database
{
    public class DatabasePayload<T, M> : IDatabasePayload<T, M>
    {
        public string Name { get; set; }
        public T Type { get; set; }
        public List<Parameter<M>> Parameters { get; set; }
    }
}
