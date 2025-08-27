using UnityEngine;
using UnityEngine.Tilemaps;

public class Junction : BuildingBH
{

    public Junction(Vector2 worldposition, int rotation, Tilemap tilemap) : base(worldposition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        buildingType = BuildingManager.buildingType.Junction;
        Debug.Log("Junction Construite");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Junction Detruite");
    }

    public override void BuildingAction(ItemBH item, Vector2 useless, BuildingBH currentConveyor)
    {
        Vector2 currentDir = currentConveyor.GetDirection().normalized;
        Vector2 nextPos1 = item.worldPosition + currentDir * Time.deltaTime + currentDir * 1f;

        BuildingBH nextBuilding = buildingManager.GetBuildingOnTile(nextPos1);

        if (nextBuilding is null) return;

        Vector2 nextDir = nextBuilding.GetDirection();


        if (nextBuilding is Conveyor && ItemManager.IsSpaceFree(nextPos1) && currentDir == nextDir) item.worldPosition = nextPos1;
    }
}
