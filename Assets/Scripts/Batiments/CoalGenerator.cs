using UnityEngine;
using UnityEngine.Tilemaps;

public class CoalGenerator : BuildingBH
{
    private BuildingData data;


    private float ElectricityProductionPerSec;
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
        ElectricityManager.RegisterProducter(this);

        if (data != null)
        {
            ElectricityProductionPerSec = data.CoalGeneratorkWhPerSec;
            capacity = data.CoalGeneratorCapacity;
        }

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
            electricityProduction = ElectricityProductionPerSec;

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
