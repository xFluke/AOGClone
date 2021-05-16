using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int x;
    [SerializeField] int y;

    [SerializeField] bool highlighted = false;

    Material originalMaterial;

    public UnityEvent<Tile> onTileSelected;

    // For Pathfinding
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    public int costModifier;
    [HideInInspector] public Tile parent;

    public int fCost {
        get { return gCost + hCost + costModifier; }
    }
    [SerializeField] bool walkable = true;
    public bool Walkable { get { return walkable; } set { walkable = value; } }

    [SerializeField] bool occupiedByUnit = false;
    public bool OccupiedByUnit {  
        get { return occupiedByUnit; } 
        set { 
            occupiedByUnit = value;
            walkable = !value;
        } 
    }

    private void Awake() {
        onTileSelected.AddListener(FindObjectOfType<GameManager>().SelectTile);
    }

    private void Start() {
        FindObjectOfType<Grid>().SetTileAt(x, y, this);

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
        // Give tile red glow
        else {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material_TileInvalidHighlight");
            highlighted = true;
        }
    }

    public void UnhighlightTile() {
        GetComponent<MeshRenderer>().material = originalMaterial;
        highlighted = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!highlighted) return;

        Debug.Log("Help");
        onTileSelected.Invoke(this);
    }
}
