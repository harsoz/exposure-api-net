using FastMember;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_extensions
{

    /// <summary>
    /// Extension reference for all services
    /// </summary>
    public static class Service 
    {

        /// <summary>
        /// General extension to avoid service layer
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public static IActionResult Run<T>(this ControllerBase ctl, Func<T> method)
        {
            try
            {
                var result = method();
                return ctl.StatusCode(StatusCodes.Status200OK,
                    new { status = "ok", message = "ok", props = result });
            }
            catch (Exception e)
            {
                return ctl.StatusCode(StatusCodes.Status500InternalServerError,
                    new { status = "fail", message = e.Message });
            }
        }
    }
}
