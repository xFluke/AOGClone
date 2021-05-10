using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;

    bool highlighted = false;

    Material originalMaterial;

    public UnityEvent<Tile> onTileSelected;

    // For Pathfinding
    public int gCost;
    public int hCost;
    public Tile parent;
    public int fCost {
        get { return gCost + hCost; }
    }
    [SerializeField] bool walkable = true;
    public bool Walkable { get { return walkable; } }

    private void Awake() {
        onTileSelected.AddListener(FindObjectOfType<GameManager>().SelectTile);
    }

    private void Start() {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    public void SetCoordinate(int _x, int _y) {
        x = _x;
        y = _y;
    }

    public Vector2 GetCoordinates() {
        return new Vector2(x, y);
    }

    public int X {  get { return this.x; } }
    public int Y { get { return this.y; } }

    public void HighlightTile(bool walkable) {
        // Gives the tile a green glow
        if (walkable) {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material_TileValidHighlight");
            highlighted = true;
        }
    }

    public void UnhighlightTile() {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }

    private void OnMouseDown() {
        if (!highlighted) return;

        onTileSelected.Invoke(this);
    }
}
