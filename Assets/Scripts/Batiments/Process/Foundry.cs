using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Foundry : BuildingBH
{
    private BuildingData data;
    public FoundryRecipe recipe;
    public Recipe11_1 currentRecipe;
    private FoundryUI foundryUI;

    public int currentRecipeIndex = 0;
    public int capacity;
    public int currentStorageInput1 = 0;
    public int currentStorageInput2 = 0;
    public int currentStorageOutput = 0;
    private float ejectInterval = 0.5f;
    private float ejectTimer = 0f;
    public float processTimer = 0f;
    private float processInterval;

    public bool isProcessing = false;

    public Foundry(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Foundry Construite");

        data = ReferenceHolder.instance.buildingData;
        foundryUI = ReferenceHolder.instance.foundryUI;
        recipe = ReferenceHolder.instance.foundryRecipe;

        buildingType = BuildingManager.buildingType.Foundry;

        if (data != null)
        {
            capacity = data.foundryCapacity;

            currentRecipe = recipe.foundryRecipes[currentRecipeIndex];
            processInterval = currentRecipe.craftSpeed;
        }

    


    }



    public override void BuildingUpdate()
    {
        if (currentStorageInput1 >= 1 && currentStorageInput2 >= 1 && !isProcessing && capacity > currentStorageOutput)
        {
            isProcessing = true;
        }

        if (isProcessing)
        {
            processTimer += Time.deltaTime;

            if (processTimer >= processInterval)
            {
                currentStorageInput1 -= 1;
                currentStorageInput2 -= 1;

                currentStorageOutput += 1;

                playerStats.IncrementFloatStat(Statistics.statType.ProcessedResourceAllTime, 1f);

                isProcessing = false;

                processTimer = 0f;
            }
        }

        ejectTimer += Time.deltaTime;

        if (currentStorageOutput >= 1 && ejectTimer >= ejectInterval)
        {
            Vector2 spawnPos = new Vector2(Mathf.Floor(worldPosition.x) + 0.5f, Mathf.Floor(worldPosition.y) + 0.5f) + GetDirection();

            if (buildingManager.GetBuildingOnTile(spawnPos) is Conveyor && ItemManager.IsSpaceFree(spawnPos))
            {
                ItemBH outputItem = new ItemBH(currentRecipe.output, spawnPos);
                ItemManager.AddItem(outputItem, spawnPos);

                currentStorageOutput -= 1;
                ejectTimer = 0;
            }
        }

    }


    public override void BuildingOnDestroy()
    {
        Debug.Log("Foundry détruite !");
    }

    public RessourceBehaviour.RessourceType GetInput1()
    {
        return currentRecipe.input1;
    }

    public RessourceBehaviour.RessourceType GetInput2()
    {
        return currentRecipe.input2;
    }
    

}
