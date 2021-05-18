using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StructureType
{
    Tower
}

public class Structure : MonoBehaviour
{
    [SerializeField] Tile myTile;

    // Not implemented yet
    [SerializeField] int pointsToCapture;
    [SerializeField] int pointsAlreadyCaptured;

    [SerializeField] int manaIncrease;

    bool captured = false;

    private void Awake() {
        FindObjectOfType<GameManager>().onEndTurn.AddListener(AdvanceCaptureProgress);
    }

    private void AdvanceCaptureProgress() {
        if (!myTile.OccupiedByUnit)
            return;

        // TODO
        // pointsAlreadyCaptured += unit.CaptureStrength
        // check if pointsAlreadyCaptured > pointstoCapture

        // Captured
        captured = true;
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material_BlueStructure");
        FindObjectOfType<GameManager>().IncreaseManaPerTurn(manaIncrease);
        FindObjectOfType<GameManager>().onEndTurn.RemoveListener(AdvanceCaptureProgress);
    }
}
