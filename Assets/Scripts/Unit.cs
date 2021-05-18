using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] int moveDistance;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public int MoveDistance { get { return moveDistance; } }

    public UnityEvent<Unit> onUnitSelected;

    private void Start() {
        gameObject.AddComponent<UnitMovement>();

        onUnitSelected.AddListener(FindObjectOfType<GameManager>().SelectUnit);
        FindObjectOfType<GameManager>().onEndTurn.AddListener(ResetforNewTurn);

        transform.position = new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z));
        x = (int)transform.position.x / 2;
        y = (int)transform.position.z / 2;

        FindObjectOfType<Grid>().GetTileAt(x, y).OccupiedByUnit = true;
    }

    public void SetCoordinate(int _x, int _y) {
        x = _x;
        y = _y;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onUnitSelected.Invoke(this);
    }

    public void ResetforNewTurn() {
        GetComponent<UnitMovement>().MovedThisTurn = false;
    } 
}
