using UnityEngine;
using UnityEngine.Tilemaps;

public class Hub : BuildingBH
{
    private HubUI hubUI;
    public Hub(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        hubUI = ReferenceHolder.instance.hubUI;
        buildingType = BuildingManager.buildingType.Hub;
        buildingLibrary.RegisterBuildingUI(buildingType, hubUI.panel);
        buildingLibrary.RegisterBuildingOnShow(BuildingManager.buildingType.Hub, () => hubUI.HubUIOnShow());
        buildingLibrary.RegisterBuildingOnHide(BuildingManager.buildingType.Hub, () => hubUI.HubUIOnHide());
    }
    
}
