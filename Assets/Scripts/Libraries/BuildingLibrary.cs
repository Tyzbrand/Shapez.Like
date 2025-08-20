using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingLibrary : MonoBehaviour
{
    private Dictionary<BuildingManager.buildingType, int> buildPrice;
    private Dictionary<BuildingBH, VisualElement> buildingUILib;
    private Dictionary<BuildingBH, Action> buildingOnShowLib;
    private Dictionary<BuildingBH, Action> buildingOnHideLib;

    private Placement placementSC;





    private void Awake()
    {


        buildPrice = new Dictionary<BuildingManager.buildingType, int>() { { BuildingManager.buildingType.Extractor, 100 }, { BuildingManager.buildingType.Conveyor, 10 }, { BuildingManager.buildingType.marketplace, 0 }, { BuildingManager.buildingType.Foundry, 500 },
        { BuildingManager.buildingType.builder, 750 }, { BuildingManager.buildingType.CoalGenerator, 1500 } };



    }


    public int GetBuildingPrice(BuildingManager.buildingType buildingType)
    {
        if (buildPrice.TryGetValue(buildingType, out int price))
        {
            return price;
        }
        else
            return 0;
        
    }
}
