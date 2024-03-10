using System;
using System.Collections.Generic;
using System.Linq;
using TravelRouteAPI.Models;

namespace TravelRouteAPI.Services
{
    public class RouteService
    {
        private readonly List<Route> _routes;

        public RouteService()
        {
            _routes = new List<Route>
            {
                new Route { Origem = "GRU", Destino = "BRC", Valor = 10 },
                new Route { Origem = "BRC", Destino = "SCL", Valor = 5 },
                new Route { Origem = "GRU", Destino = "CDG", Valor = 75 },
                new Route { Origem = "GRU", Destino = "SCL", Valor = 20 },
                new Route { Origem = "GRU", Destino = "ORL", Valor = 56 },
                new Route { Origem = "ORL", Destino = "CDG", Valor = 5 },
                new Route { Origem = "SCL", Destino = "ORL", Valor = 20 }
            };
        }

        public List<Route> GetRoutes()
        {
            return _routes;
        }

        public List<Route> FindCheapestRoute(string origin, string destination)
        {
            Dictionary<string, int> distanceTable = new Dictionary<string, int>();
            Dictionary<string, string> previousNode = new Dictionary<string, string>();
            HashSet<string> unvisitedNodes = new HashSet<string>();

            // Inicialize a distância para todos os nós como infinito
            foreach (var route in _routes)
            {
                distanceTable[route.Origem] = int.MaxValue;
                distanceTable[route.Destino] = int.MaxValue;
                previousNode[route.Origem] = null;
                previousNode[route.Destino] = null;
                unvisitedNodes.Add(route.Origem);
                unvisitedNodes.Add(route.Destino);
            }

            // Defina a distância para o nó de origem como 0
            distanceTable[origin] = 0;

            while (unvisitedNodes.Count > 0)
            {
                string currentNode = null;
                int shortestDistance = int.MaxValue;

                // Encontre o nó não visitado com a menor distância conhecida
                foreach (var node in unvisitedNodes)
                {
                    if (distanceTable[node] < shortestDistance)
                    {
                        shortestDistance = distanceTable[node];
                        currentNode = node;
                    }
                }

                unvisitedNodes.Remove(currentNode);

                // Se o nó de destino for alcançado, pare
                if (currentNode == destination)
                    break;

                // Atualize as distâncias conhecidas dos vizinhos do nó atual
                foreach (var route in _routes.Where(r => r.Origem == currentNode))
                {
                    int alternativeDistance = distanceTable[currentNode] + route.Valor;
                    if (alternativeDistance < distanceTable[route.Destino])
                    {
                        distanceTable[route.Destino] = alternativeDistance;
                        previousNode[route.Destino] = currentNode;
                    }
                }
            }

            // Reconstrua o caminho mais curto
            List<Route> shortestPath = new List<Route>();
            string current = destination;
            while (previousNode[current] != null)
            {
                string previous = previousNode[current];
                shortestPath.Add(_routes.First(r => r.Origem == previous && r.Destino == current));
                current = previous;
            }
            shortestPath.Reverse();

            return shortestPath;
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route);
        }

        public void UpdateRoute(Route route)
        {
            var existingRoute = _routes.FirstOrDefault(r => r.Origem == route.Origem && r.Destino == route.Destino);
            if (existingRoute != null)
            {
                existingRoute.Valor = route.Valor;
            }
        }

        public Route GetRoute(string origin, string destination)
        {
            return _routes.FirstOrDefault(r => r.Origem == origin && r.Destino == destination);
        }

        public void DeleteRoute(string origin, string destination)
        {
            var routeToRemove = _routes.FirstOrDefault(r => r.Origem == origin && r.Destino == destination);
            if (routeToRemove != null)
            {
                _routes.Remove(routeToRemove);
            }
        }
    }
}
