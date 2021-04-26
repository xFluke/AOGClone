using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject grassTilePrefab;

    [SerializeField] int xSize;
    [SerializeField] int ySize;

    private float tileSize = 2.0f;

    Tile[,] grid;

    // Start is called before the first frame update
    void Start() {
        grid = new Tile[xSize, ySize];

        GenerateGrid();
    }

    private void GenerateGrid() {
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                GameObject tile = Instantiate(grassTilePrefab, new Vector3(x, 0, y) * tileSize, Quaternion.identity, transform);
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
                }

            }
        }
    }
}
