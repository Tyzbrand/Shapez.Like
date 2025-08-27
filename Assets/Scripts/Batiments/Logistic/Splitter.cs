using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Splitter : BuildingBH
{
    private int lastIndex = 0;
    public Splitter(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        buildingType = BuildingManager.buildingType.Splitter;
        Debug.Log("Splitter construit");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Splitter détruit");
    }

    public override void BuildingAction(ItemBH item, Vector2 useless, BuildingBH useless2)
    {

        Vector2 entryDir = -GetDirection();

        Vector2 forward = GetDirection().normalized;
        Vector2 left = new Vector2(-forward.y, forward.x);
        Vector2 right = new Vector2(forward.y, -forward.x);

        List<Vector2> possibleDir = new List<Vector2> { left, forward, right };


        for (int i = 0; i < possibleDir.Count; i++)
        {
            int indx = (lastIndex + i) % possibleDir.Count;
            Vector2 nextDir = possibleDir[indx];

            Vector2 nextPos = worldPosition + nextDir.normalized;
            BuildingBH nextBuilding = buildingManager.GetBuildingOnTile(nextPos);

            if (nextBuilding is Conveyor && ItemManager.IsSpaceFree(nextPos))
            {   
                item.worldPosition = nextPos;
                item.worldPosition = ItemManager.CenterOnPerpendicularAxis(item.worldPosition, nextDir);
                lastIndex = (indx + 1) % possibleDir.Count;
                return;
            }
        }
    }
}
