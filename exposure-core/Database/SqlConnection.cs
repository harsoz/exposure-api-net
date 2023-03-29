using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace exposure_core.Database
{
    public class SqlConnection : IDatabase
    {
        private string _connectionString = "Server=exposure-db.mssql.somee.com;Database=exposure-db;User Id=HarSoz_SQLLogin_1;Password=l9zuw1jc3l;";

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DatabaseResponse<T1> Execute<T1, T2, T3>(IDatabasePayload<T2, T3> param, bool AsList = true, bool fromJson = false)
        {
            DatabaseResponse<T1> result = new DatabaseResponse<T1>();
            try
            {
                DataTable response = ExecuteScript(param);
                if (AsList)
                {
                    if (fromJson)
                    {
                        result.List = ConvertValueFromJsonList<T1>(AsDynamicEnumerable(response));
                    }
                    else
                    {
                        result.List = ConvertValue<T1>(AsDynamicEnumerable(response));
                    }
                }
                else
                {
                    if (fromJson)
                    {
                        result.Data = ConvertValueFromJson<T1>(AsDynamicEnumerable(response));
                    }
                    else
                    {
                        result.Data = ConvertValue<T1>(AsDynamicEnumerable(response).First());
                    }
                }
            }
            catch (SqlException ex)
            {
                result.Status = DatabaseStatus.Exception;
                result.Message = ex.Message ?? ex.InnerException.Message;
            }

            return result;
        }


        private DataTable ExecuteScript<T, M>(IDatabasePayload<T, M> data)
        {
            DataTable outputDataTable = new DataTable();

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(data.Name, con))
                {
                    cmd.CommandType = GetValueCommand(data.Type.ToString());

                    if (data.Parameters != null)
                    {
                        data.Parameters.ForEach(delegate (Parameter<M> item)
                        {
                            string name = item.Name;
                            SqlDbType type = GetValueDb(item.Type.ToString());
                            object value = item.Value;
                            cmd.Parameters.Add(name, type).Value = value;
                        });
                    }


                    con.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    try
                    {
                        sqlDataAdapter.Fill(outputDataTable);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        // The source table is invalid.
                        throw;
                    }
                }
            }

            return outputDataTable;
        }

        private List<DynamicRow> AsDynamicEnumerable(DataTable table)
        {
            string[] columns = table.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
            return table.AsEnumerable().Select(row => new DynamicRow(row, columns)).ToList();
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;
            private readonly string[] _columns;

            internal DynamicRow(DataRow row, string[] columns) { _row = row; _columns = columns; }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }

            public T Get<T>()
            {
                string values = "";
                for(int i=0; i<_columns.Length;i++)
                    values = "\"" + _columns[i] + "\":\"" + _row.ItemArray[i] + "\", ";

                values = values.Remove(values.Length - 1, 1);
                string json = "{" + values + "}";

                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }

            public string GetValue()
            {
                return _row.ItemArray[0].ToString();
            }
        }

        private CommandType GetValueCommand(string name)
        {
            CommandType type = CommandType.Text;
            switch (name)
            {
                case "Text":
                    type = CommandType.Text;
                    break;
                case "StoredProcedure":
                    type = CommandType.StoredProcedure;
                    break;
                default:
                    break;
            }

            return type;
        }

        private SqlDbType GetValueDb(string name)
        {
            SqlDbType type = SqlDbType.VarChar;
            switch (name)
            {
                case "Int":
                    type = SqlDbType.Int;
                    break;
                case "VarBinary":
                    type = SqlDbType.VarBinary;
                    break;
                case "Structured":
                    type = SqlDbType.Structured;
                    break;
                default:
                    break;
            }

            return type;
        }

        private List<T> ConvertValue<T>(List<DynamicRow> values)
        {
            return values.Select(row => row.Get<T>()).ToList();
        }
        private T ConvertValue<T>(DynamicRow value)
        {
            return value.Get<T>();
        }

        private List<T> ConvertValueFromJsonList<T>(List<DynamicRow> values)
        {
            StringBuilder totalValue = new StringBuilder();
            foreach (var item in values)
                totalValue.Append(item.GetValue());

            return JsonConvert.DeserializeObject<List<T>>(totalValue.ToString());
        }

        private T ConvertValueFromJson<T>(List<DynamicRow> values)
        {
            StringBuilder totalValue = new StringBuilder();
            foreach (var item in values)
                totalValue.Append(item.GetValue());

            return JsonConvert.DeserializeObject<T>(totalValue.ToString());
        }
    }
}
