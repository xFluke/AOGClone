using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public List<Tile> path;
    Tile currentTargetTile;
    bool moving = false;

    public void SetPath(List<Tile> path) {
        this.path = path;
        moving = true;
        currentTargetTile = path[0];
        
    }

    private void Update() {
        if (moving) {
            Vector3 targetTilePosition = currentTargetTile.transform.position + new Vector3(0, 1, 0);

            transform.position = Vector3.MoveTowards(transform.position, targetTilePosition, 0.2f);

            if (Vector3.Distance(transform.position, targetTilePosition) <= 0.01f) {
                path.RemoveAt(0);

                if (path.Count > 0) {
                    currentTargetTile = path[0];
                }
                else {
                    moving = false;

                    transform.position = new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z));
                    GetComponent<Unit>().SetCoordinate((int)transform.position.x / 2, (int)transform.position.z / 2);
                    
                }
            }
        }
    }
}
