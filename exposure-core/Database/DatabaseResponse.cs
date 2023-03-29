using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_core.Database
{
    public class DatabaseResponse<T>
    {
        public DatabaseStatus Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<T> List { get; set; }
    }
}
