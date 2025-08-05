using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Dictionary<Vector2Int, BuildingBH> buildingReferencer = new();

    //---------------Méthodes Implémentées---------------

    public void AddBuilding(Vector2 worldPos, BuildingBH building)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (!buildingReferencer.ContainsKey(tilePos))
        {
            buildingReferencer.Add(tilePos, building);
        }
        
    }

    public void RemoveBuilding(Vector2 worldPos)
    {   
        Vector2Int tilePos = ConvertInt(worldPos);
        if (buildingReferencer.ContainsKey(tilePos))
        {
            buildingReferencer.Remove(tilePos);
        }
        
    }

    public bool IsTileUsed(Vector2 worldPos)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        return buildingReferencer.ContainsKey(tilePos);
    }

    public BuildingBH GetBuildingOnTile(Vector2 worldPos)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (buildingReferencer.TryGetValue(tilePos, out BuildingBH building))
        {
            return building;
        }
        return null;
    }

    //---------------Méthodes Unity---------------

    private void Update()
    {
        foreach (var building in buildingReferencer.Values)
        {
            building.BuildingUpdate();
        }
    }

    //---------------Méthodes utilisaires---------------

    private Vector2Int ConvertInt(Vector2 vector2)
    {
        return new Vector2Int(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
    }
}
