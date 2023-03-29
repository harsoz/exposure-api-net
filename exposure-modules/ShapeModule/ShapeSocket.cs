using exposure_core.Database;
using exposure_modules.ShapeModule.Logic;
using exposure_modules.ShapeModule.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_api.Modules.ShapeModule
{
    public class ShapeSocket: Hub
    {
        private readonly ShapeLogic _service = new ShapeLogic();

        public async Task ShapeHasBeenRegister()
        {
            var response = _service.GetShapes();
            await Clients.All.SendAsync("GetShapes", response );
        }

        public async Task ShapeHasBeenDeleted(string shapeName)
        {
            var result = new DatabaseResponse<Shape>();
            var response = _service.RemoveShape(shapeName);
            if (response.Data.Status == DatabaseStatus.Ok)
            {
                result = _service.GetShapes();
            }
            else
            {
                result.Status = response.Data.Status;
                result.Message = response.Message;
            }

            await Clients.All.SendAsync("GetShapes", result);
        }
    }
}
