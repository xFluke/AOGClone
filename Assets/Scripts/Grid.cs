using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject grassTilePrefab;

    [SerializeField] int xSize;
    [SerializeField] int ySize;

    private float tileSize = 2.0f;

    Tile[,] grid;

    List<Tile> highlightedTiles;

    // Start is called before the first frame update
    void Awake() {
        grid = new Tile[xSize, ySize];
        highlightedTiles = new List<Tile>();

        GenerateGrid();
    }

    private void GenerateGrid() {
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                GameObject tile = Instantiate(grassTilePrefab, new Vector3(x, 0, y) * tileSize, Quaternion.identity, transform);
                tile.name = x + " " + y;
                tile.GetComponent<Tile>().SetCoordinate(x, y);
                
                grid[x, y] = tile.GetComponent<Tile>();
            }
        }
    }

    public void HighlightUnitWalkableAreas(int unitX, int unitY, int unitMoveDistance) {

        for (int tileX = unitX - unitMoveDistance; tileX <= unitX + unitMoveDistance; tileX++) {

            if (tileX < 0 || tileX >= xSize)
                continue;

            for (int tileY = unitY - unitMoveDistance; tileY <= unitY + unitMoveDistance; tileY++) {

                if (tileY < 0 || tileY >= ySize)
                    continue;

                if ((Mathf.Abs(tileX - unitX) + Mathf.Abs(tileY - unitY)) <= unitMoveDistance) {
                    if (!grid[tileX, tileY])
                        continue;
                    grid[tileX, tileY].HighlightTile(true);
                    highlightedTiles.Add(grid[tileX, tileY]);
                }

            }
        }
    }

    public void UnhighlightTiles() {
        foreach (var tile in highlightedTiles) {
            tile.UnhighlightTile();
        }

        highlightedTiles.Clear();
    }

    public List<Tile> GetNeighbourTiles(Tile tile) {
        List<Tile> neighbourTiles = new List<Tile>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0)
                    continue;

                int checkX = tile.X + x;
                int checkY = tile.Y + y;

                if (checkX >= 0 && checkX < xSize && checkY >= 0 && checkY < ySize) {
                    neighbourTiles.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbourTiles;
    }

    public Tile GetTileAt(int x, int y) {
        return grid[x, y];
    }

    public void SetTileAt(int x, int y, Tile tile) {
        grid[x, y] = tile;
    }

    public void FindAvailableTilesForUnit(Unit unit) {
        Tile startingTile = GetTileAt(unit.X, unit.Y);

        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(startingTile);

        while (openSet.Count > 0) {
            Tile currentTile = openSet[0];
            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            foreach (Tile neighbour in GetNeighbourTiles(currentTile)) {
                if (!neighbour)
                    continue;

                if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                if (!openSet.Contains(neighbour)) {

                    List<Tile> pathToTile = FindObjectOfType<Pathfinding>().FindPath(startingTile, neighbour);
                    int costOfPath = FindObjectOfType<Pathfinding>().GetCostOfPath(pathToTile);

                    if (costOfPath <= unit.MoveDistance) {
                        openSet.Add(neighbour);

                        neighbour.HighlightTile(true);
                    }
                }

            }
        }
    }
}
