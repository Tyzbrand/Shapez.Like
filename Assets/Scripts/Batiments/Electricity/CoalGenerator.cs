using UnityEngine;
using UnityEngine.Tilemaps;

public class CoalGenerator : BuildingBH
{
    private BuildingData data;
    private CoalGeneratorUI coalGeneratorUI;


    private float maxElectricityProduction;
    public int capacity;


    public int currentStorage = 0;
    public float burnTimer = 0f;
    public float burnMaxTime = 8f;



    public CoalGenerator(Vector2 worldposition, int rotation, Tilemap tilemap) : base(worldposition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        data = ReferenceHolder.instance.buildingData;
        coalGeneratorUI = ReferenceHolder.instance.coalGeneratorUI
        ;
        ElectricityManager.RegisterProducter(this);

        buildingType = BuildingManager.buildingType.CoalGenerator;

        if (data != null)
        {
            maxElectricityProduction = data.CoalGeneratorkWhPerSec;
            capacity = data.CoalGeneratorCapacity;
        }

        buildingLibrary.RegisterBuildingUI(BuildingManager.buildingType.CoalGenerator, ReferenceHolder.instance.coalGeneratorUI.panel);
        buildingLibrary.RegisterBuildingOnShow(BuildingManager.buildingType.CoalGenerator, () => coalGeneratorUI.CoalGeneratorUIOnShow(this));
        buildingLibrary.RegisterBuildingOnHide(BuildingManager.buildingType.CoalGenerator, () => coalGeneratorUI.CoalGeneratorUIOnHide());

        Debug.Log("Coal Generator Construit !");
    }

    public override void BuildingOnDestroy()
    {
        ElectricityManager.RemoveProducter(this);
        Debug.Log("Coal Generator Détruit !");
    }

    public override void BuildingUpdate()
    {
        if (burnTimer > 0f)
        {
            burnTimer -= Time.deltaTime;
            electricityProduction = maxElectricityProduction;

        }
        if (burnTimer <= 0f)
        {
            if (currentStorage > 0)
            {
                currentStorage--;
                burnTimer = burnMaxTime;
            }
            else electricityProduction = 0f;

        }
        if (burnTimer < 0f) burnTimer = 0f;

    }

    public override void BuildingOnDisable()
    {
        electricityProduction = 0f;
       
    }



}
