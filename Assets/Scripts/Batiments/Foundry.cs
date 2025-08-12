using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Foundry : BuildingBH
{
    private FoundryData data;
    private FoundryRecipe recipe;
    public Recipe currentRecipe;

    private int capacity;
    public int currentStorageInput1 = 0;
    public int currentStorageInput2 = 0;
    private int currentStorageOutput = 0;
    private float ejectInterval = 0.5f;
    private float ejectTimer = 0f;
    private float processTimer = 0f;
    private float processInterval = 3f;

    private bool isProcessing = false;

    public Foundry(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Foundry Construite");

        data = ReferenceHolder.instance.foundryData;
        recipe = ReferenceHolder.instance.foundryRecipe;

        if (data != null)
        {
            capacity = data.capacity;
            currentRecipe = recipe.foundryRecipes[0];
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
