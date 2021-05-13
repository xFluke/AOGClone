using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitMovement : MonoBehaviour
{
    Unit unit;
    public List<Tile> path;
    Tile currentTargetTile;
    bool moving = false;

    public List<Tile> copyOfPath;

    public void SetPath(List<Tile> path) {
        FindObjectOfType<Grid>().GetTileAt(unit.X, unit.Y).Walkable = true;

        this.path = path;
        moving = true;
        currentTargetTile = path[0];

        GetComponent<Animator>().SetBool("Moving", true);

        copyOfPath.Clear();
        foreach (var item in path) {
            copyOfPath.Add(item);
        }
    }

    private void Start() {
        unit = GetComponent<Unit>();   
    }

    private void Update() {
        if (moving) {
            Vector3 targetTilePosition = currentTargetTile.transform.position + new Vector3(0, 1, 0);

            transform.position = Vector3.MoveTowards(transform.position, targetTilePosition, 0.01f);
            transform.rotation = Quaternion.LookRotation(currentTargetTile.transform.position + new Vector3(0, 1, 0) - transform.position);

            if (Vector3.Distance(transform.position, targetTilePosition) <= 0.01f) {
                path.RemoveAt(0);

                if (path.Count > 0) {
                    currentTargetTile = path[0];
                }
                else {
                    moving = false;

                    transform.position = new Vector3(Mathf.Round(transform.position.x), 1, Mathf.Round(transform.position.z));
                    GetComponent<Unit>().SetCoordinate((int)transform.position.x / 2, (int)transform.position.z / 2);

                    FindObjectOfType<Grid>().GetTileAt(unit.X, unit.Y).Walkable = false;

                    GetComponent<Animator>().SetBool("Moving", false);
                }
            }
        }
    }
}
