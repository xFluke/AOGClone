using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Unit currentlySelectedUnit;

    Grid grid;

    private void Start() {
        grid = FindObjectOfType<Grid>();
    }

    public void SelectUnit(Unit unit) {
        // If clicked on the same unit
        if (currentlySelectedUnit == unit) {
            grid.UnhighlightTiles();

            currentlySelectedUnit = null;
        }
        else {
            //grid.HighlightUnitWalkableAreas(unit.X, unit.Y, unit.MoveDistance);
            grid.FindAvailableTilesForUnit(unit);

            currentlySelectedUnit = unit;
        }
    }

    public void SelectTile(Tile tileSelected) {

        // Move selected unit to target tile
        if (currentlySelectedUnit != null) {
            Tile tileUnitIsOn = FindObjectOfType<Grid>().GetTileAt(currentlySelectedUnit.X, currentlySelectedUnit.Y);

            if (tileUnitIsOn != tileSelected) {
                List<Tile> path = FindObjectOfType<Pathfinding>().FindPath(tileUnitIsOn, tileSelected);
                currentlySelectedUnit.GetComponent<UnitMovement>().SetPath(path);

                grid.UnhighlightTiles();
            }
        }
    }

    public void SelectPortal() {
        Debug.Log("Clicked on Portal");
    }
}
