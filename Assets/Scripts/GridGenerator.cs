using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject grassTilePrefab;

    [SerializeField] int xSize;
    [SerializeField] int ySize;

    private float tileSize = 2.0f;

    // Start is called before the first frame update
    void Start() {
        GenerateGrid();
    }

    private void GenerateGrid() {
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                GameObject tile = Instantiate(grassTilePrefab, new Vector3(x, 0, y) * tileSize, Quaternion.identity, transform);
                tile.GetComponent<Tile>().SetCoordinate(x, y);
            }
        }
    }
}
