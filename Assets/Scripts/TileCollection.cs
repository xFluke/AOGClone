using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TileCollection", menuName = "ScriptableObjects/TileCollection")]

public class TileCollection : ScriptableObject 
{ 
    [Serializable]
    public class TileBlueprint
    {
        public GameObject prefab;
        public char identifier;

        public GameObject GetPrefab() {
            return prefab;
        }
    }

    private Dictionary<char, TileBlueprint> blueprintDict;

    public TileBlueprint[] tileBlueprints;

    public TileBlueprint this[char identifier] {
        get {
            return blueprintDict[identifier];
        }
    }

    public void Initialize() {
        Init();
    }

    private void Init() {
        if (blueprintDict != null)
            return;

        blueprintDict = new Dictionary<char, TileBlueprint>();

        foreach (var blueprint in tileBlueprints) {
            blueprintDict[blueprint.identifier] = blueprint;
        }
    }
}
