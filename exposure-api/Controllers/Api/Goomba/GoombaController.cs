using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exposure_extensions;
using exposure_modules.GoombaModule.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exposure_api.Controllers.Api.Goomba
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoombaController : ControllerBase
    {
        private readonly IGoombaLogic _service;
        public GoombaController(IGoombaLogic service)
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
        public IActionResult generateGoomba()
        {
            return this.Run(() => _service.GenerateGoomba());
        }

        /// <summary>
        /// Generate a speficic goomba
        /// </summary>
        /// <param name="type">Type of goomba; Mini or Super</param>
        /// <returns>Goomba</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="500">If the type does not exist</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult generateSpecificGoomba(string type)
        {
            return this.Run(() => _service.GenerateSpecificGoomba(type));
        }

        /// <summary>
        /// Generate a specific goomba
        /// </summary>
        /// <param name="type">Type of goomba; Mini or Super</param>
        /// <param name="size">Size of goomba</param>
        /// <param name="attack">Attack points</param>
        /// <param name="defense">Defense points</param>
        /// <returns>Goomba</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="500">If the type does not exist</response>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult generateCustomGoomba(string type, int size, int attack, int defense)
        {
            return this.Run(() => _service.GenerateCustomGoomba(type, size, attack, defense));
        }
    }
}
