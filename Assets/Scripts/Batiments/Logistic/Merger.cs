using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Merger : BuildingBH
{
    private int lastIndex = 0;
    public Merger(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        buildingType = BuildingManager.buildingType.Merger;
        Debug.Log("Merger consrtuit");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Merger détruit");
    }

    public override void BuildingAction(ItemBH item)
    {
        Vector2 exit = GetDirection();
        Vector2 enter1 = new Vector2(-exit.y, exit.x);
        Vector2 enter2 = -GetDirection();
        Vector2 enter3 = new Vector2(exit.y, -exit.x);
        List<Vector2> possibleEnter = new List<Vector2> { enter1, enter2, enter3 };

        Vector2 itemDirection = (item.worldPosition - worldPosition).normalized;

        for (int i = 0; i < possibleEnter.Count; i++)
        {
            int indx = (lastIndex + i) % possibleEnter.Count;
            Vector2 nextDir = possibleEnter[indx];
            Vector2 nextPos = worldPosition + exit;

            if (Vector2.Dot(itemDirection, nextDir) > 0.9f && buildingManager.GetBuildingOnTile(nextPos) is Conveyor && ItemManager.IsSpaceFree(nextPos))
            {
                item.worldPosition = nextPos;
                item.worldPosition = ItemManager.CenterOnPerpendicularAxis(item.worldPosition, exit);
                lastIndex = (indx + 1) % possibleEnter.Count;
                return;
            }
        }

        
    }
}
