using exposure_extensions;
using exposure_modules.CalculatorModule.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_api.Controllers.Api.ChangeCalculator
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChangeCalculatorController : ControllerBase
    {
        private readonly ICalculator _service;
        public ChangeCalculatorController(ICalculator service)
        {
            _service = service;
        }


        /// <summary>
        /// Generate a dynamic goomba
        /// </summary>
        /// <returns>Goomba</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="500">If the type does not exist (almost impossible)</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult calculateAtm(decimal amount, string country)
        {
            return this.Run(() => _service.Calculate(amount, country));
        }
    }
}
