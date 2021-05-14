using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Portal : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Tile myTile;
    [SerializeField] GameObject unitsCanvas;

    public UnityEvent<Portal> onPortalSelected;

    public int X { get { return myTile.X; } }
    public int Y { get { return myTile.Y; } }

    private void Awake() {
        onPortalSelected.AddListener(FindObjectOfType<GameManager>().SelectPortal);
    }

    public void OnPointerClick(PointerEventData eventData) {
        // Prevent player from clicking on portal if there is already a unit standing on the same tile
        if (!myTile.Walkable) return;

        onPortalSelected.Invoke(this);

        unitsCanvas.SetActive(!unitsCanvas.activeSelf);
    }

    public void HideCanvas() {
        unitsCanvas.SetActive(false);
    }

}
