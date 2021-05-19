using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnitNames unitName;
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] int moveDistance;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public int MoveDistance { get { return moveDistance; } }

    [HideInInspector]
    public UnityEvent<Unit> onUnitSelected;

    [SerializeField] UnitInformationUI unitInformationUI;

    private void Start() {
        gameObject.AddComponent<UnitMovement>();

        unitInformationUI.Initialize(unitName);
        
        onUnitSelected.AddListener(FindObjectOfType<GameManager>().SelectUnit);
        FindObjectOfType<GameManager>().onEndTurn.AddListener(ResetforNewTurn);

        SetPosition();
    }

    public void SetCoordinates(int _x, int _y) {
        x = _x;
        y = _y;

        SetPosition();        
    }

    private void SetPosition() {
        FindObjectOfType<Grid>().GetTileAt(x, y).OccupiedByUnit = true;

        float tileSize = FindObjectOfType<Grid>().GetTileObjectSize();
        float ySpawnPosition = FindObjectOfType<Grid>().GetYSpawnPosition();
        transform.position = new Vector3(x * tileSize, ySpawnPosition, y * tileSize);
    }

    public void OnPointerClick(PointerEventData eventData) {
        onUnitSelected.Invoke(this);
    }

    public void ResetforNewTurn() {
        GetComponent<UnitMovement>().MovedThisTurn = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        unitInformationUI.ShowUnitInformation();
    }

    public void OnPointerExit(PointerEventData eventData) {
        unitInformationUI.HideUnitInformation();
    }
}
