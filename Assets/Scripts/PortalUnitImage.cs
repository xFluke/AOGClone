using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PortalUnitImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Portal portal;
    [SerializeField] UnitNames unitName;

    public UnityEvent<UnitNames, Vector2Int> onPortalUnitImageClicked;

    private void Start() {
        onPortalUnitImageClicked.AddListener(FindObjectOfType<GameManager>().SummonUnit);
    }

    public void OnPointerClick(PointerEventData eventData) {
        //GameObject newUnit = Instantiate(unit, GetComponentInParent<Transform>().position, Quaternion.identity);
        //newUnit.GetComponent<Unit>().SetCoordinate(portal.X, portal.Y);
        portal.HideCanvas();

        onPortalUnitImageClicked.Invoke(unitName, new Vector2Int(portal.X, portal.Y));
    }
}
