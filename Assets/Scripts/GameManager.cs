using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Unit currentlySelectedUnit;
    [SerializeField] Unit lastSelectedUnit;

    Grid grid;

    private void Start() {
        grid = FindObjectOfType<Grid>();
    }

    public void SelectUnit(Unit unit) {
        currentlySelectedUnit = unit;

        // If clicked on the same unit
        if (currentlySelectedUnit == lastSelectedUnit) {
            grid.UnhighlightTiles();
        }
        else {
            grid.HighlightUnitWalkableAreas(unit.X, unit.Y, unit.MoveDistance);
        }

        lastSelectedUnit = currentlySelectedUnit;
    }

    public void MoveUnit(Tile targetTile) {
        currentlySelectedUnit.SetDestination(targetTile.GetCoordinates());
    }
}
