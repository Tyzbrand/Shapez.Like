using System.Collections.Generic;
using System.Linq;
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
    private ElectricityManager electricityManager;
    private RessourceDictionnary ressourceDictionnary;
    private Statistics playerStats;
    private BuildingLibrary buildingLibrary;
    public Tilemap tilemap;


    public enum buildingType
    {
        None,
        Hub,
        Extractor,
        Junction,
        Splitter,
        Merger,
        AdvancedExtractor,
        Conveyor,
        marketplace,
        Foundry,
        builder,
        CoalGenerator
    }

    

    //---------------Méthodes Implémentées---------------
    //Logique

    public void AddBuilding(Vector2 worldPos, BuildingBH building)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (!buildingReferencer.ContainsKey(tilePos))
        {
            buildingReferencer.Add(tilePos, building);
            AddVisual(building, tilePos);
            building.SetManagers(itemManager, buildingManager, electricityManager, buildingLibrary, playerStats);
            building.SetDico(ressourceDictionnary);

            building.BuidlingStart();



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
        else return null;

    }

    public Vector2Int GetUnderTile(Vector2 pos)
    {
        Vector2Int cellPos = ConvertInt(pos);
        return cellPos;
    }

    public void HubCreation()
    {
        Vector2 basePos = new Vector2(43, 107);
        tilemap = FindFirstObjectByType<Tilemap>();

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {

                Vector2 hubPos = new Vector2(basePos.x + x, basePos.y + y);
                Hub hub = new Hub(hubPos, 0, tilemap);
                AddBuilding(ConvertInt(hubPos), hub);

            }
        }


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

    private void AddVisual(BuildingBH building, Vector2Int tilePos)
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
        ressourceDictionnary = ReferenceHolder.instance.ressourceDictionnary;
        electricityManager = ReferenceHolder.instance.electricityManager;
        buildingLibrary = ReferenceHolder.instance.buildingLibrary;
        playerStats = ReferenceHolder.instance.playerStats;


    }

    private void Update()
    {
        foreach (var building in buildingReferencer.Values)
        {
            if (building.IsActive) building.BuildingUpdate(); 
        }
    }

    //---------------Méthodes utilisaires---------------

    private Vector2Int ConvertInt(Vector2 vector2)
    {
        return new Vector2Int(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
    }

    

}
