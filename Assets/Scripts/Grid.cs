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
    void Start() {
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
}
