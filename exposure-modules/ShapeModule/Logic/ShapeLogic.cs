using exposure_modules.ShapeModule.Interface;
using exposure_modules.ShapeModule.Model;
using exposure_core.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using ExpBuilder;
using exposure_extensions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace exposure_modules.ShapeModule.Logic
{
    public class ShapeLogic : IShapeLogic
    {
        private readonly SqlConnection _db;

        public ShapeLogic()
        {
            _db = new SqlConnection();
        }

        public double ExecuteAlgorithm(string shapeName, HttpRequest req)
        {
            var result = GetShapes();

            if (result.Status == DatabaseStatus.Exception)
                throw new AggregateException(result.Message);

            StringValues parameters;
            req.Headers.TryGetValue("Params", out parameters);
            string Parameters = parameters;

            var parms = JsonConvert.DeserializeObject<List<Param>>(Parameters);

            var algorithm = result.List.FirstOrDefault(a => a.Name == shapeName);
            if (algorithm == null)
                throw new ArgumentException("Algorithm not registered");

            algorithm.Params = parms;
            byte[] binary = Convert.FromBase64String(algorithm.Binary);
            Assembly testAssembly = Assembly.Load(binary);
            Type calcType = testAssembly.GetType(algorithm.Name);
            IShape calcInstance = (IShape)Activator.CreateInstance(calcType);

            double[] param = algorithm.Params.OrderBy(a => a.Order).Select(a => a.Value).ToArray();
            var area = calcInstance.GetArea(param);
            return area;
        }

        public DatabaseResponse<DatabaseDefaultResponse> RegisterAlgorithm(HttpRequest req)
        {

            StringValues name;
            req.Headers.TryGetValue("Name", out name);
            string Name = name;

            StringValues parameters;
            req.Headers.TryGetValue("Params", out parameters);
            string Parameters = parameters;

            // get file
            MemoryStream fileStream;
            using (fileStream = new MemoryStream())
            {
                req.Form.Files[0].CopyTo(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);
            }

            DataTable parms = JsonConvert.DeserializeObject<List<Param>>(Parameters).ConvertToDatatable("Id", "Order", "Name", "Value");

            DatabasePayload<CommandType, SqlDbType> data = new DatabasePayload<CommandType, SqlDbType>
            {
                Name = "shp.procCreateShape",
                Type = CommandType.StoredProcedure,
                Parameters = new List<Parameter<SqlDbType>>
                {
                    new Parameter<SqlDbType>
                    {
                        Name = "name",
                        Type = SqlDbType.VarChar,
                        Value = Name
                    },
                    new Parameter<SqlDbType>
                    {
                        Name = "binary",
                        Type = SqlDbType.VarChar,
                        Value = Convert.ToBase64String(fileStream.ToArray())
                    },
                    new Parameter<SqlDbType>
                    {
                        Name = "params",
                        Type = SqlDbType.Structured,
                        Value = parms
                    }
                }
            };

            return _db.Execute<DatabaseDefaultResponse, CommandType, SqlDbType>(data, false);
        }

        public DatabaseResponse<Shape> GetShapes()
        {
            DatabasePayload<CommandType, SqlDbType> data = new DatabasePayload<CommandType, SqlDbType>
            {
                Name = "shp.procGetShapes",
                Type = CommandType.StoredProcedure
            };

            var response = _db.Execute<Shape, CommandType, SqlDbType>(data, true, true);
            return response;
        }

        public DatabaseResponse<ShapeForApi> GetShapesForApi()
        {
            DatabaseResponse<ShapeForApi> result = new DatabaseResponse<ShapeForApi>();
            var shapes = GetShapes();
            result.Status = shapes.Status;
            result.Message = shapes.Message;    
            result.List = shapes.List.Select(x => new ShapeForApi { Name = x.Name, Params = x.Params }).ToList();
            return result;
        }

        public DatabaseResponse<DatabaseDefaultResponse> RemoveShape(string shapeName)
        {
            DatabasePayload<CommandType, SqlDbType> data = new DatabasePayload<CommandType, SqlDbType>
            {
                Name = "shp.procRemoveShape",
                Type = CommandType.StoredProcedure,
                Parameters = new List<Parameter<SqlDbType>>
                {
                    new Parameter<SqlDbType>
                    {
                        Name = "shapeName",
                        Type = SqlDbType.VarChar,
                        Value = shapeName
                    }
                }
            };

            return _db.Execute<DatabaseDefaultResponse, CommandType, SqlDbType>(data, false);
        }
    }
}
