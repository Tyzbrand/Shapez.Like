using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBuildingBHDictionnary : MonoBehaviour
{
    private Dictionary<GameObject, Type> buildPrefabType = new();

    private GameObject extractorPrefab;
    private GameObject ConveyorPrefab;
    private GameObject marketplacePrefab;
    private GameObject foundryPrefab;

    private void Start()
    {
        extractorPrefab = ReferenceHolder.instance.extractorPrefab;
        ConveyorPrefab = ReferenceHolder.instance.conveyorPrefab;
        marketplacePrefab = ReferenceHolder.instance.marketplacePrefab;
        foundryPrefab = ReferenceHolder.instance.foundryPrefab;

        buildPrefabType.Add(extractorPrefab, typeof(Extractor));
        buildPrefabType.Add(ConveyorPrefab, typeof(Conveyor));

    }

    public Type GetPrefabType(GameObject prefab)
    {
        buildPrefabType.TryGetValue(prefab, out Type type);
        return type;
        
 
        
    }
}
