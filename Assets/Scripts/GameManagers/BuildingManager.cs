using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    private Dictionary<Vector2Int, BuildingBH> buildingReferencer = new();
    private Dictionary<BuildingBH, GameObject> buildingVisualReferencer = new();

    private Placement placement;
    private PlayerVariables player;
    private ItemManager itemManager;
    private BuildingManager buildingManager;

    //---------------Méthodes Implémentées---------------
    //Logique

    public void AddBuilding(Vector2 worldPos, BuildingBH building, Tilemap tilemap)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (!buildingReferencer.ContainsKey(tilePos))
        {
            buildingReferencer.Add(tilePos, building);
            AddVisual(building, tilePos, tilemap);
            building.BuidlingStart();

            building.SetManagers(itemManager, buildingManager);
        }

    }

    public void RemoveBuilding(Vector2 worldPos)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (buildingReferencer.TryGetValue(tilePos, out BuildingBH building))
        {
            RemoveVisual(building);
            buildingReferencer.Remove(tilePos);
            building.BuildingOnDestroy();
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

    //Visuel

    private void RemoveVisual(BuildingBH building)
    {
        if (buildingVisualReferencer.TryGetValue(building, out GameObject visual))
        {
            Destroy(visual);
            buildingVisualReferencer.Remove(building);
        }
    }

    private void AddVisual(BuildingBH building, Vector2Int tilePos, Tilemap tilemap)
    {
        if (!buildingVisualReferencer.ContainsKey(building))
        {   


            Vector3Int tilePos3D = new Vector3Int(tilePos.x, tilePos.y, 0);
            Vector3 worldPos3D = tilemap.CellToWorld(tilePos3D);
            worldPos3D += new Vector3(0.5f, 0.5f, 0f);

            buildingVisualReferencer.Add(building, Instantiate(placement.currentBuild, worldPos3D, Quaternion.Euler(0, 0, player.rotation)));
        }
    }



    //---------------Méthodes Unity---------------

    private void Start()
    {
        placement = ReferenceHolder.instance.placementSC;
        player = ReferenceHolder.instance.playervariable;
        itemManager = ReferenceHolder.instance.itemManager;
        buildingManager = ReferenceHolder.instance.buildingManager;
    }

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
