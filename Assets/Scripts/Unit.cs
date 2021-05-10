using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] int moveDistance;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public int MoveDistance { get { return moveDistance; } }

    bool moving = false;
    Vector2 moveDestination;

    public UnityEvent<Unit> onUnitSelected;

    private void Start() {
        // Temporary just to move the unit to the correct spots
        transform.position = new Vector3(2 * x, 1, 2 * y);
    }

    private void Update() {
        if (moving) {
            transform.position = Vector3.MoveTowards(transform.position, moveDestination, 0.5f);

            if (Vector3.Distance(transform.position, moveDestination) <= 1f) {
                moving = false;
            }
        }
    }

    public void SetCoordinate(int _x, int _y) {
        x = _x;
        y = _y;
    }

    public void SetDestination(Vector2 destinationCoordinates) {
        moving = true;
        moveDestination = new Vector3(2 * destinationCoordinates.x, 1, 2 * destinationCoordinates.y);
    }

    private void OnMouseDown() {
        onUnitSelected.Invoke(this);
    }
}
