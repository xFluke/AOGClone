using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;

    Material originalMaterial;

    [SerializeField] Material glowMaterial;

    private void Start() {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update() {
        
    }

    public void SetCoordinate(int _x, int _y) {
        x = _x;
        y = _y;
    }

    public void HighlightTile(bool walkable) {
        // Gives the tile a green glow
        if (walkable)
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material_TileValidHighlight");
    }

    public void UnhighlightTile() {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }
    
}
