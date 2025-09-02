using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class BuildingManager : MonoBehaviour
{
    private Dictionary<Vector2Int, BuildingBH> buildingReferencer = new();
    public List<GameObject> freeVisual = new();
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
    public GameObject buildingPrefab;
    public Transform parent;


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
        CoalGenerator,
    }

    

    //---------------Méthodes Implémentées---------------
    //Logique

    public void AddBuilding(Vector2 worldPos, BuildingBH building)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (!buildingReferencer.ContainsKey(tilePos))
        {
            buildingReferencer.Add(tilePos, building);
            building.SetManagers(itemManager, buildingManager, electricityManager, buildingLibrary, playerStats);
            building.SetDico(ressourceDictionnary);
            building.BuidlingStart();
            AddVisual(building, tilePos);
            building.visual = GetBuildingVisual(building);
            building.visualSpriteRenderer = building.visual.GetComponent<SpriteRenderer>();
            if (building is Conveyor conveyor)
            {
                conveyor.UpdateSprite();
                conveyor.UpdateNeighbor();
            }
                

        }

    }

    public void RemoveBuilding(Vector2 worldPos)
    {
        Vector2Int tilePos = ConvertInt(worldPos);
        if (buildingReferencer.TryGetValue(tilePos, out BuildingBH building))
        {
            RemoveVisual(building);
            building.BuildingOnDestroy();
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
            freeVisual.Add(visual);
            buildingVisualReferencer.Remove(building);
            visual.SetActive(false);
        }

    }

    private void AddVisual(BuildingBH building, Vector2Int tilePos)
    {
        Sprite spriteToUse = buildingLibrary.GetBuildingSpriteForState(building.buildingType, false);
        if (spriteToUse == null) Debug.Log("Pas de sprite trouvé pour le batiment: " + building.buildingType);

        Vector3Int tilePos3D = new Vector3Int(tilePos.x, tilePos.y, 0);
        Vector3 worldPos3D = tilemap.CellToWorld(tilePos3D);
        worldPos3D += new Vector3(0.5f, 0.5f, 0f);

        if (freeVisual.Count >= 1)
        {
            GameObject visual = freeVisual[0];

            freeVisual.Remove(visual);
            buildingVisualReferencer.Add(building, visual);

            var visualSprite = visual.GetComponent<SpriteRenderer>();
            visualSprite.sprite = spriteToUse;

            if (building.buildingType == buildingType.Conveyor) visualSprite.sortingOrder = 1;
            else visualSprite.sortingOrder = 3;

            visual.transform.position = worldPos3D;
            visual.transform.rotation = Quaternion.Euler(0f, 0f, player.rotation);

            visual.SetActive(true);

        }
        else
        {
            GameObject visual = Instantiate(buildingPrefab, worldPos3D, Quaternion.Euler(0f, 0f, player.rotation), parent);
            var visualSprite = visual.GetComponent<SpriteRenderer>();

            buildingVisualReferencer.Add(building, visual);
            visualSprite.sprite = spriteToUse;
            if (building.buildingType == buildingType.Conveyor) visualSprite.sortingOrder = 1;
            else visualSprite.sortingOrder = 3;
        }
    }


    public void PreInstanciateVisual(int amount, Transform parent)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject visual = Instantiate(buildingPrefab, parent);
            visual.SetActive(false);
            freeVisual.Add(visual);
        }
    }

    public GameObject GetBuildingVisual(BuildingBH building)
    {
        buildingVisualReferencer.TryGetValue(building, out GameObject visual);
        return visual;
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
        buildingPrefab = ReferenceHolder.instance.buildingPrefab;



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
