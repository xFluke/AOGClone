using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // A* pathfinding 
    public static List<Tile> FindPath(Grid grid, Tile startingTile, Tile targetTile, bool ignoreUnwalkableTiles = false) {
        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(startingTile);

        while (openSet.Count > 0) {
            Tile currentTile = openSet[0];

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == targetTile) {
                List<Tile> path = RetracePath(startingTile, targetTile);
                return path;
            }

            foreach (Tile neighbour in grid.GetNeighbourTiles(currentTile)) {
                if (!neighbour)
                    continue;

                if (closedSet.Contains(neighbour)) {
                    continue;
                }

                if (!ignoreUnwalkableTiles) {
                    if (!neighbour.Walkable || neighbour.OccupiedByUnit) {
                        continue;
                    }
                }

                int newMovementCostToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour) + neighbour.costModifier;

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetTile);
                    neighbour.parent = currentTile;

                    if (!openSet.Contains(neighbour)) {
                        openSet.Add(neighbour);
                    }
                    
                }
            }
        }
        return null;
    }

    static List<Tile> RetracePath(Tile startTile, Tile endTile) {
        List<Tile> path = new List<Tile>();
        Tile currentTile = endTile;

        while (currentTile != startTile) {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }

        path.Reverse();

        return path;
    }

    static int GetDistance(Tile tileA, Tile tileB) {
        int distanceX = Mathf.Abs(tileA.X - tileB.X);
        int distanceY = Mathf.Abs(tileA.Y - tileB.Y);

        if (distanceX > distanceY) {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }

    public static int GetCostOfPath(List<Tile> path) {
        if (path == null) {
            return -1;
        }

        int cost = 0;

        foreach (Tile tile in path) {
            cost += 1 + tile.costModifier;
        }
        return cost;
    }
}
