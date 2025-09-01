using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Conveyor : BuildingBH
{
    public Conveyor(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Conveyor !");
        buildingType = BuildingManager.buildingType.Conveyor;
        conveyorSpeed = 2f;
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Conveyor Détruit");
    }

    public void UpdateSprite()
    {
        bool hasForward = false;
        bool hasBackward = false;
        bool hasLeft = false;
        bool hasRight = false;

        Vector2 forwardDir = GetDirection();
        Vector2 backwardDir = -forwardDir;
        Vector2 leftDir = new Vector2(-forwardDir.y, forwardDir.x);
        Vector2 rightDir = new Vector2(forwardDir.y, -forwardDir.x);

        Vector2 forwardPos = worldPosition + forwardDir;
        Vector2 backwardPos = worldPosition + backwardDir;
        Vector2 leftPos = worldPosition + leftDir;
        Vector2 rightPos = worldPosition + rightDir;


        if (buildingManager.GetBuildingOnTile(forwardPos) is Conveyor) hasForward = true;
        if (buildingManager.GetBuildingOnTile(backwardPos) is Conveyor) hasBackward = true;
        if (buildingManager.GetBuildingOnTile(leftPos) is Conveyor) hasLeft = true;
        if (buildingManager.GetBuildingOnTile(rightPos) is Conveyor) hasRight = true;

        int connection = 0;
        if (hasForward) connection++;
        if (hasBackward) connection++;
        if (hasLeft) connection++;
        if (hasRight) connection++;

        Debug.Log("Conveyor connectés : " + connection);

        if (visualSpriteRenderer == null) return;

        if (connection == 2)
        {
            if ((hasForward && hasBackward) || (hasLeft && hasRight))
            {
                visualSpriteRenderer.sprite = TextureHolder.instance.Conveyor;
                visual.transform.rotation = Quaternion.Euler(0, 0, rotation);
            }
            else
            {
                visualSpriteRenderer.sprite = TextureHolder.instance.conveyorTurn;

                float turnRotation = GetTurnRotation(hasForward, hasBackward, hasLeft, hasRight);

                visual.transform.rotation = Quaternion.Euler(0, 0, turnRotation);
            }
        }   
        

    }
    
    public void UpdateNeighborSprites()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        
        foreach (Vector2 dir in directions)
        {
            Vector2 neighborPos = worldPosition + dir;
            if (buildingManager.GetBuildingOnTile(neighborPos) is Conveyor neighbor)
            {
                neighbor.UpdateSprite();
            }
        }
    }
    
    private float GetTurnRotation(bool hasForward, bool hasBackward, bool hasLeft, bool hasRight)
    {

        if (hasForward && hasRight) return -90f;
        if (hasRight && hasBackward) return 0f;
        if (hasBackward && hasLeft) return 90f;
        if (hasLeft && hasForward) return 180f;


        if (hasForward && hasLeft) return 90f;
        if (hasLeft && hasBackward) return 0f;
        if (hasBackward && hasRight) return -90f;
        if (hasRight && hasForward) return 180f;

        return 0f;
    }


}
