using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;

    public void SetCoordinate(int _x, int _y) {
        x = _x;
        y = _y;
    }
}
