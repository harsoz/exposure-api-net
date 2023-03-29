using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_core.Database
{
    public interface IDatabase
    {
        void SetConnectionString(string connectionString);
        DatabaseResponse<T1> Execute<T1, T2, T3>(IDatabasePayload<T2, T3> param, bool AsList = true, bool fromJson = false);
    }
}
