using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum UnitNames
{
    BARBARIAN,
    Count
}

[CreateAssetMenu(fileName = "UnitCollection", menuName = "ScriptableObjects/UnitCollection")]

public class UnitCollection : ScriptableObject
{
    [Serializable]
    public class UnitBlueprint
    {
        public GameObject prefab;
        public int cost;
        public UnitNames name;

        public GameObject GetPrefab() {
            return prefab;
        }

        public int GetCost() {
            return cost;
        }
    }

    private Dictionary<UnitNames, UnitBlueprint> blueprintDict;

    public UnitBlueprint[] unitBlueprints;

    public UnitBlueprint this[UnitNames name] {
        get {
            return blueprintDict[name];
        }
    }

    public void Initialize() {
        Init();
    }

    private void Init() {
        if (blueprintDict != null)
            return;

        blueprintDict = new Dictionary<UnitNames, UnitBlueprint>();

        foreach (var blueprint in unitBlueprints) {
            blueprintDict[blueprint.name] = blueprint;
        }
    }
    

    
}
