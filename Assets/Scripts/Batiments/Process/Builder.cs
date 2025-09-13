using UnityEngine;
using UnityEngine.Tilemaps;

public class Builder : BuildingBH
{
    public Recipe1_1 currentRecipe;
    public BuilderRecipe recipe;
    private BuildingData data;

    public int currentRecipeIndex = 1;
    public int capacity;
    public int inputCapacity;
    public int currentStorageInput = 0;
    public int currentStorageOutput = 0;
    private float ejectInterval = 0.5f;
    private float ejectTimer = 0f;
    public float processTimer = 0f;
    public float processInterval;

    public bool isProcessing = false;

    public Builder(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Builder Construit");

        recipe = ReferenceHolder.instance.builderRecipe;
        data = ReferenceHolder.instance.buildingData;
        uIScript = ReferenceHolder.instance.builderUI;

        buildingType = BuildingManager.buildingType.builder;

        if (data != null)
        {
            capacity = data.builderCapacity;
            currentRecipe = recipe.BuilderRecipes[currentRecipeIndex];
            processInterval = currentRecipe.craftSpeed;
            inputCapacity = data.builderInputCapacity;
        }


    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Builder Détruit");


    }

    public override void BuildingUpdate()
    {
        if (currentStorageInput >= currentRecipe.inputCount && (capacity - currentStorageOutput) >= currentRecipe.outputCount && !isProcessing)
        {
            isProcessing = true;
            processTimer = 0f;
        }

        if (isProcessing)
        {
            processTimer += Time.deltaTime;

            if (processTimer >= processInterval)
            {
                currentStorageInput-=currentRecipe.inputCount;
                currentStorageOutput+=currentRecipe.outputCount;

                playerStats.IncrementFloatStat(Statistics.statType.ProcessedResourceAllTime, currentRecipe.outputCount);

                isProcessing = false;
            }
        }

        ejectTimer += Time.deltaTime;

        if (currentStorageOutput >= 1 && ejectTimer >= ejectInterval)
        {
            Vector2 spawnPos = new Vector2(Mathf.Floor(worldPosition.x) + 0.5f, Mathf.Floor(worldPosition.y) + 0.5f) + GetDirection();

            if (buildingManager.GetBuildingOnTile(spawnPos) is Conveyor && ItemManager.IsSpaceFree(spawnPos))
            {
                ItemBH outputItem = new ItemBH(currentRecipe.Output, spawnPos);
                ItemManager.AddItem(outputItem, spawnPos);

                currentStorageOutput--;
                ejectTimer = 0f;
            }
        }
    }

    public override void BuildingAction(ItemBH item, Vector2 nextPos, BuildingBH useless)
    {
        if (!ItemManager.IsItNextBuildingExit(item, this, nextPos)) return;
        if (item.itemType == currentRecipe.Input && currentStorageInput < inputCapacity)
        {
            currentStorageInput++;
            ItemManager.itemToRemove.Add(item);
        }       
    }
    
        
    

}
