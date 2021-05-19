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

    [SerializeField] bool movedThisTurn = false;
    public bool MovedThisTurn { get { return movedThisTurn; } set { movedThisTurn = value; } }

    public List<Tile> copyOfPath;

    float yOffset;

    float moveSpeed;

    public void SetPath(List<Tile> path) {
        FindObjectOfType<Grid>().GetTileAt(unit.X, unit.Y).OccupiedByUnit = false;

        this.path = path;

        Debug.Log("Cost of path I'm moving on : " + FindObjectOfType<Pathfinding>().GetCostOfPath(path));

        moving = true;
        currentTargetTile = path[0];

        GetComponent<Animator>().SetBool("Moving", true);

        copyOfPath.Clear();
        foreach (var item in path) {
            copyOfPath.Add(item);
        }

        movedThisTurn = true;

        UpdateRotation();
    }

    private void Start() {
        unit = GetComponent<Unit>();

        copyOfPath = new List<Tile>();

        yOffset = FindObjectOfType<Grid>().GetYSpawnPosition();

        moveSpeed = transform.localScale.x / 100f;
    }

    private void Update() {
        if (moving) {
            Vector3 targetTilePosition = currentTargetTile.transform.position + new Vector3(0, yOffset, 0);

            transform.position = Vector3.MoveTowards(transform.position, targetTilePosition, moveSpeed);

            if (Vector3.Distance(transform.position, targetTilePosition) <= 0.01f) {
                path.RemoveAt(0);

                if (path.Count > 0) {
                    currentTargetTile = path[0];

                    UpdateRotation();
                }
                else {
                    moving = false;

                    transform.position = new Vector3(Mathf.Round(transform.position.x), yOffset, Mathf.Round(transform.position.z));
                    GetComponent<Unit>().SetCoordinates(currentTargetTile.X, currentTargetTile.Y);

                    FindObjectOfType<Grid>().GetTileAt(unit.X, unit.Y).OccupiedByUnit = true;

                    GetComponent<Animator>().SetBool("Moving", false);
                }
            }
        }
    }

    private void UpdateRotation() {
        Vector3 resultantPosition = currentTargetTile.transform.position + new Vector3(0, yOffset, 0) - transform.position;
        transform.rotation = Quaternion.LookRotation(resultantPosition);
    }
}
