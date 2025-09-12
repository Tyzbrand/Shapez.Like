
using System;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Extractor : BuildingBH
{
    public float ejectTimer = 0f;
    public int currentStorage = 0;
    private float storageBuffer = 0.0f;
    private float ejectInterval = 0.5f;

    public int capacity;
    public float ressourcesPerSecond;
    private TileType underTile;
    private bool canExtract = false;
    private String ressourceType;
    private BuildingData data;

    public Extractor(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }


    public override void BuidlingStart()
    {
        Debug.Log("Extractor !");
        data = ReferenceHolder.instance.buildingData;
        buildingLibrary = ReferenceHolder.instance.buildingLibrary;

        buildingType = BuildingManager.buildingType.Extractor;
        

        if (data != null)
        {
            capacity = data.extractorCapacity;
            ressourcesPerSecond = data.extractorRessourcesPerSecond;

        }


        Vector2Int underTIle2D = buildingManager.GetUnderTile(worldPosition);
        Vector3Int underTile3D = new Vector3Int(underTIle2D.x, underTIle2D.y, 0);
        underTile = tilemap.GetTile<TileType>(underTile3D);

        if (underTile != null)
        {
            ressourceType = RessourceDictionnary.GetRessourceSpriteName(underTile.tileType);

            if (ressourceType != null)
            {
                canExtract = true;
            }
        }
    }

    public override void BuildingLateStart()
    {
        if (underTile != null)
        {
            Sprite newTexture = buildingLibrary.GetExtractorStateSprite(underTile.tileType);
            if (visualSpriteRenderer != null && newTexture != null) visualSpriteRenderer.sprite = newTexture;
        }
        OutlineChild = visual.transform.Find("Outline").gameObject;
        
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Extractor Détruit !");
    }

    public override void BuildingUpdate()
    {   

        if (canExtract && currentStorage < capacity) //Gestion de la production logique
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

        if (currentStorage >= 1 && ejectTimer >= ejectInterval)//Gestion libération items
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


}
