using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class Grid : MonoBehaviour
{
    [SerializeField] int xSize;
    [SerializeField] int ySize;

    private float tileSize = 2.0f;

    Tile[,] grid;
    [SerializeField] TileCollection tileCollection;

    List<Tile> highlightedTiles;

    // Start is called before the first frame update
    void Awake() {
        highlightedTiles = new List<Tile>();

        // Map already created
        if (transform.childCount > 0) {
            InitializeGrid();
        }
        else {
            CreateGridFromMapFile("Assets/Resources/Map.txt");
        }
    }

    private void InitializeGrid() {
        grid = new Tile[xSize, ySize];

        foreach (Transform child in transform) {
            Tile tile = child.GetComponent<Tile>();

            grid[tile.X, tile.Y] = tile;
        }
    }

    public void CreateGridFromMapFile(string mapFilePath = "Assets/Resources/Map.txt") {
        if (transform.childCount > 0)
            return;

        tileCollection.Initialize();

        StreamReader sr = new StreamReader(mapFilePath);
        var mapFileContent = sr.ReadToEnd();
        sr.Close();

        var mapFileLines = Regex.Split(mapFileContent, "\r\n|\r|\n");
        xSize = ySize = mapFileLines.Length;

        grid = new Tile[xSize, ySize];

        int x = 0;
        int y = 0;

        for (int i = ySize - 1; i >= 0; i--) {
            string row = mapFileLines[i];

            x = 0;
            foreach (var tileChar in row) {
                InstantiateTile(tileChar, x, y);

                x++;
            }
            y++;
        }
    }

    private void InstantiateTile(char c, int x, int y) {
        GameObject newTile = Instantiate(tileCollection[c].GetPrefab(), new Vector3(x, 0, y) * tileSize, Quaternion.identity, transform);
        newTile.name = x + " " + y;
        newTile.GetComponent<Tile>().SetCoordinate(x, y);
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
                // support diagonal movement
                //if (x == 0 && y == 0)
                //    continue;

                if (Mathf.Abs(x) == Mathf.Abs(y))
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
        if (grid != null)
            grid[x, y] = tile;
    }

    public void FindAvailableTilesForUnit(Unit unit) {
        Tile startingTile = GetTileAt(unit.X, unit.Y);

        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(startingTile);

        bool unitCanMove = !unit.GetComponent<UnitMovement>().MovedThisTurn;

        while (openSet.Count > 0) {
            Tile currentTile = openSet[0];
            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            foreach (Tile neighbour in GetNeighbourTiles(currentTile)) {
                if (!neighbour)
                    continue;


                if (closedSet.Contains(neighbour)) {
                    continue;
                }

                if (!openSet.Contains(neighbour)) {

                    List<Tile> pathToTile = FindObjectOfType<Pathfinding>().FindPath(startingTile, neighbour);
                    int costOfPath = FindObjectOfType<Pathfinding>().GetCostOfPath(pathToTile);

                    if (costOfPath <= unit.MoveDistance && costOfPath != -1) {
                        openSet.Add(neighbour);

                        // If tile is within unit's move distance and is also valid
                        neighbour.HighlightTile(unitCanMove);
                        highlightedTiles.Add(neighbour);
                    }
                    else {
                        closedSet.Add(neighbour);
                        neighbour.HighlightTile(false);
                        highlightedTiles.Add(neighbour);
                    }
                }


            }
        }
    }

    public void DeleteMap() {
        if (Application.isPlaying) {
            Array.Clear(grid, 0, grid.Length);
        }

        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
