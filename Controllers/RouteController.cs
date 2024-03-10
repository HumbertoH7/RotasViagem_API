using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TravelRouteAPI.Models;
using TravelRouteAPI.Services;

namespace TravelRouteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly RouteService _routeService;

        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        // GET
        [HttpGet]
        public IEnumerable<Route> GetRoutes()
        {
            return _routeService.GetRoutes();
        }

        // GET
        [HttpGet("cheapest")]
        public ActionResult<string> GetCheapestRoute(string origin, string destination)
        {
            var cheapestRoute = _routeService.FindCheapestRoute(origin, destination);
            if (cheapestRoute == null)
                return NotFound("Rota não encontrada.");

            return Ok($"Rota mais barata: {string.Join(" - ", cheapestRoute.Select(r => r.Origem))} - {destination} ao custo de ${cheapestRoute.Sum(r => r.Valor)}");
        }

        // POST
        [HttpPost]
        public ActionResult<Route> PostRoute(Route route)
        {
            _routeService.AddRoute(route);
            return CreatedAtAction(nameof(GetRoutes), new { id = route.Origem }, route);
        }

        // PUT
        [HttpPut("{origin}-{destination}")]
        public IActionResult PutRoute(string origin, string destination, Route route)
        {
            if (origin != route.Origem || destination != route.Destino)
            {
                return BadRequest();
            }

            _routeService.UpdateRoute(route);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{origin}-{destination}")]
        public ActionResult<Route> DeleteRoute(string origin, string destination)
        {
            var route = _routeService.GetRoute(origin, destination);

            if (route == null)
            {
                return NotFound();
            }

            _routeService.DeleteRoute(origin, destination);
            return route;
        }
    }
}
