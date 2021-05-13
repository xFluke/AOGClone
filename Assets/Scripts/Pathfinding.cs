using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;

    private void Awake() {
        grid = GetComponent<Grid>();
    }

    // A* pathfinding 
    public List<Tile> FindPath(Tile startingTile, Tile targetTile) {
        Debug.Log("Starting Tile: " + startingTile.name);
        Debug.Log("Target Tile: " + targetTile.name);

        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(startingTile);

        while (openSet.Count > 0) {
            Tile currentTile = openSet[0];
            //for (int i = 1; i < openSet.Count; i++) {
            //    if (openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost) {
            //        currentTile = openSet[i];
            //    }
            //}

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == targetTile) {
                List<Tile> path = RetracePath(startingTile, targetTile);
                return path;
            }

            foreach (Tile neighbour in grid.GetNeighbourTiles(currentTile)) {
                if (!neighbour)
                    continue;

                if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
                    continue;
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

        Debug.Log("Could not find a path");
        return new List<Tile>();
    }

    

    List<Tile> RetracePath(Tile startTile, Tile endTile) {
        List<Tile> path = new List<Tile>();
        Tile currentTile = endTile;

        while (currentTile != startTile) {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }

        path.Reverse();

        return path;
    }

    int GetDistance(Tile tileA, Tile tileB) {
        int distanceX = Mathf.Abs(tileA.X - tileB.X);
        int distanceY = Mathf.Abs(tileA.Y - tileB.Y);

        if (distanceX > distanceY) {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }

    public int GetCostOfPath(List<Tile> path) {
        int cost = 0;

        foreach (Tile tile in path) {
            cost += 1 + tile.costModifier;
        }

        return cost;
    }
}
