using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingLibrary : MonoBehaviour
{
    private Dictionary<BuildingManager.buildingType, int> buildPrice;
    private Dictionary<BuildingManager.buildingType, VisualElement> buildingUILib = new();
    private Dictionary<BuildingManager.buildingType, Action> buildingOnShowLib = new();
    private Dictionary<BuildingManager.buildingType, Action> buildingOnHideLib = new();

    private Placement placementSC;





    private void Awake()
    {


        buildPrice = new Dictionary<BuildingManager.buildingType, int>() { { BuildingManager.buildingType.Extractor, 100 }, { BuildingManager.buildingType.Conveyor, 10 }, { BuildingManager.buildingType.marketplace, 0 }, { BuildingManager.buildingType.Foundry, 500 },
        { BuildingManager.buildingType.builder, 750 }, { BuildingManager.buildingType.CoalGenerator, 1500 }, {BuildingManager.buildingType.AdvancedExtractor, 3000}, {BuildingManager.buildingType.Junction, 50} };


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


    //-----------------------------------------UI assignation-----------------------------------------

    public void RegisterBuildingUI(BuildingManager.buildingType buildingType, VisualElement uI)
    {
        if (!buildingUILib.ContainsKey(buildingType)) buildingUILib.Add(buildingType, uI);
    }

    public VisualElement GetBuildingUI(BuildingManager.buildingType buildingType)
    {
        if (buildingUILib.TryGetValue(buildingType, out VisualElement uI))
        {
            return uI;
        }
        return null;
    }


    //-----------------------------------------On Show assignation-----------------------------------------

    public void RegisterBuildingOnShow(BuildingManager.buildingType buildingType, Action onShow)
    {
        if (!buildingOnShowLib.ContainsKey(buildingType)) buildingOnShowLib.Add(buildingType, onShow);
    }

    public Action GetBuildingOnShow(BuildingManager.buildingType buildingType)
    {
        if (buildingOnShowLib.TryGetValue(buildingType, out Action onShow))
        {
            return onShow;
        }
        return null;
    }

    //-----------------------------------------On Hide assignation-----------------------------------------

    public void RegisterBuildingOnHide(BuildingManager.buildingType buildingType, Action onHide)
    {
        if (!buildingOnHideLib.ContainsKey(buildingType)) buildingOnHideLib.Add(buildingType, onHide);
    }

    public Action GetBuildingOnHide(BuildingManager.buildingType buildingType)
    {
        if (buildingOnHideLib.TryGetValue(buildingType, out Action onHide))
        {
            return onHide;
        }
        return null;
    }
}
