using UnityEngine;
using UnityEngine.Tilemaps;

public class AdvancedExtractor : BuildingBH
{
    public float ejectTimer = 0f;
    public int currentStorage = 0;
    private float storageBuffer = 0.0f;
    private float ejectInterval = 0.5f;

    public int capacity;
    public float ressourcesPerSecond;
    public float maxElectricityConsomation;
    private TileType underTile;
    private bool canExtract = false;
    private Sprite ressourceType;
    private BuildingData data;
    private AdvancedExtractorUI extractorUI;


    public AdvancedExtractor(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        buildingType = BuildingManager.buildingType.AdvancedExtractor;

        data = ReferenceHolder.instance.buildingData;
        extractorUI = ReferenceHolder.instance.advancedExtractorUI;
        uIScript = ReferenceHolder.instance.advancedExtractorUI;

        if (data != null)
        {
            capacity = data.advancedExtractorCapacity;
            ressourcesPerSecond = data.advancedExtractorRessourcesPerSecond;
            maxElectricityConsomation = data.advancedExtractorConsomationPerSec;
        }

        ElectricityManager.RegisterConsomer(this);

        Vector2Int underTIle2D = buildingManager.GetUnderTile(worldPosition);
        Vector3Int underTile3D = new Vector3Int(underTIle2D.x, underTIle2D.y, 0);
        underTile = tilemap.GetTile<TileType>(underTile3D);

        if (underTile != null) canExtract = true;

    

        Debug.Log("AdvancedExtractor Construit !");
    }

    public override void BuildingOnDestroy()
    {
        ElectricityManager.RemoveConsomer(this);
        Debug.Log("Advanced extractor Détruit !");
    }


    public override void BuildingUpdate()
    {
        if (!IsActive)
        {
            electricityConsomation = 0f;
            return;
        }

        electricityConsomation = maxElectricityConsomation;

        if (canExtract && currentStorage < capacity && enoughtElectricity)
        {
            storageBuffer += ressourcesPerSecond * Time.deltaTime;
            int unitToAdd = Mathf.FloorToInt(storageBuffer);


            if (storageBuffer >= 1.0f)
            {
                int availableStorage = capacity - currentStorage;
                int Amount = Mathf.Min(unitToAdd, availableStorage);

                storageBuffer -= Amount;
                currentStorage += Amount;

                playerStats.IncrementFloatStat(Statistics.statType.RawResourcesAllTime, 1f);
            }

        }

        ejectTimer += Time.deltaTime;

        if (currentStorage >= 1 && ejectTimer >= ejectInterval)
        {
            Vector2 spawnPos = new Vector2(Mathf.Floor(worldPosition.x) + 0.5f, Mathf.Floor(worldPosition.y) + 0.5f) + GetDirection();

            if (buildingManager.GetBuildingOnTile(spawnPos) is Conveyor && ItemManager.IsSpaceFree(spawnPos))
            {
                ItemManager.AddItem(new ItemBH(underTile.tileType, spawnPos), spawnPos);

                currentStorage -= 1;
                ejectTimer = 0;
            }
        }





    }

    public float GetProgress()
    {
        if (!canExtract || currentStorage >= capacity) return 0f;

        float progress = storageBuffer % 1f;
        return Mathf.Clamp01(progress);
    }

    public override void BuildingOnDisable()
    {
        electricityConsomation = 0f;
        storageBuffer = 0f;

    }

}
