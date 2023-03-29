using exposure_modules.ShapeModule.Model;
using exposure_core.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace exposure_modules.ShapeModule.Interface
{
    public interface IShapeLogic
    {
        double ExecuteAlgorithm(string shapeName, HttpRequest req);
        DatabaseResponse<DatabaseDefaultResponse> RegisterAlgorithm(HttpRequest req);
        DatabaseResponse<Shape> GetShapes();
        DatabaseResponse<ShapeForApi> GetShapesForApi();
        DatabaseResponse<DatabaseDefaultResponse> RemoveShape(string shapeName);
    }
}
