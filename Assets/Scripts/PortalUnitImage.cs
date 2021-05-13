using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PortalUnitImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Portal portal;
    [SerializeField] GameObject unit;

    public void OnPointerClick(PointerEventData eventData) {
        GameObject newUnit = Instantiate(unit, GetComponentInParent<Transform>().position, Quaternion.identity);
        newUnit.GetComponent<Unit>().SetCoordinate(portal.X, portal.Y);

        portal.HideCanvas();
    }
}
