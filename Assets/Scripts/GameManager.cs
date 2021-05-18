using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnitCollection unitCollection;

    [SerializeField] Unit currentlySelectedUnit;
    [SerializeField] int mana;
    [SerializeField] int manaPerTurn;

    Grid grid;

    public UnityEvent<int> onManaChanged;
    public UnityEvent onEndTurn;
    
    private void Awake() {
        unitCollection.Initialize();
    }

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
            grid.UnhighlightTiles();
            grid.FindAvailableTilesForUnit(unit);

            currentlySelectedUnit = unit;
        }
    }

    public void SelectTile(Tile tileSelected) {

        // Move selected unit to target tile
        if (currentlySelectedUnit != null) {
            if (currentlySelectedUnit.GetComponent<UnitMovement>().MovedThisTurn) {
                Debug.Log("Already moved");
                return;
            }


            Tile tileUnitIsOn = FindObjectOfType<Grid>().GetTileAt(currentlySelectedUnit.X, currentlySelectedUnit.Y);

            if (tileUnitIsOn != tileSelected) {
                List<Tile> path = FindObjectOfType<Pathfinding>().FindPath(tileUnitIsOn, tileSelected);
                currentlySelectedUnit.GetComponent<UnitMovement>().SetPath(path);

                grid.UnhighlightTiles();
            }
        }
    }

    public void SelectPortal(Portal portal) {
        Debug.Log("Clicked on Portal");
    }

    public void SummonUnit(UnitNames unitName, Vector3 position) {
        int unitCost = unitCollection[UnitNames.BARBARIAN].cost;
        if (mana >= unitCost) {
            mana -= unitCost;

            GameObject newUnit = Instantiate(unitCollection[UnitNames.BARBARIAN].prefab, position, Quaternion.identity);

            onManaChanged.Invoke(mana);
        }
        else {
            FindObjectOfType<UIManager>().ShowInsufficientMana();
        }

    }

    public void EndTurn() {
        onEndTurn.Invoke();

        mana += manaPerTurn;
        onManaChanged.Invoke(mana);
    }

    public int GetCurrentMana() {
        Debug.Log(mana);
        return mana;
    }

    public void IncreaseManaPerTurn(int amt) {
        manaPerTurn += amt;
    }


  
}
