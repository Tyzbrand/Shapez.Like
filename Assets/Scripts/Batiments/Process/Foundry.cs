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
    public int input1Capacity;
    public int input2Capacity;
    public int currentStorageInput1 = 0;
    public int currentStorageInput2 = 0;
    public int currentStorageOutput = 0;
    private float ejectInterval = 0.5f;
    private float ejectTimer = 0f;
    public float processTimer = 0f;
    public float processInterval;

    //state texture
    private Sprite idle;
    private Sprite action;

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
            input1Capacity = data.foundryInput1Capacity;
            input2Capacity = data.foundryInput2Capacity;
        }

        idle = buildingLibrary.GetBuildingSpriteForState(buildingType, false);
        action = buildingLibrary.GetBuildingSpriteForState(buildingType, true);


    }



    public override void BuildingUpdate()
    {
        if (currentStorageInput1 >= currentRecipe.input1Count && currentStorageInput2 >= currentRecipe.input2Count && !isProcessing && (capacity - currentStorageOutput) >= currentRecipe.outputCount)
        {
            isProcessing = true;
            processTimer = 0f;
            SetActionTexture();
        }

        if (isProcessing)
        {
            processTimer += Time.deltaTime;

            if (processTimer >= processInterval)
            {
                currentStorageInput1 -= currentRecipe.input1Count;
                currentStorageInput2 -= currentRecipe.input2Count;

                currentStorageOutput += currentRecipe.outputCount;

                playerStats.IncrementFloatStat(Statistics.statType.ProcessedResourceAllTime, currentRecipe.outputCount);

                isProcessing = false;
                SetIdleTexture();

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

    public override void BuildingAction(ItemBH item, Vector2 nextPos, BuildingBH useless)
    {
        if (!ItemManager.IsItNextBuildingExit(item, this, nextPos)) return;
        if (item.itemType == currentRecipe.input1 && currentStorageInput1 < input1Capacity)
        {
            currentStorageInput1 += 1;
            ItemManager.itemToRemove.Add(item);
        }
        if (item.itemType == currentRecipe.input2 && currentStorageInput2 < input2Capacity)
        {
            currentStorageInput2 += 1;
            ItemManager.itemToRemove.Add(item);
        }
    }

    public override void BuildingOnDisable()
    {
        SetIdleTexture();
    }

    public override void BuildingOnEnable()
    {
        if (isProcessing) SetActionTexture();
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

    public void SetActionTexture()
    {
        visualSpriteRenderer.sprite = action;
    }

    public void SetIdleTexture()
    {
        visualSpriteRenderer.sprite = idle;
    }
    

}
