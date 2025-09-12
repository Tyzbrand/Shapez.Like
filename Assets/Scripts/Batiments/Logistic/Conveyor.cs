using UnityEngine;
using UnityEngine.Tilemaps;

public class Conveyor : BuildingBH
{
    public int level = 1;
    public float speed = 1f;

    public Conveyor(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Conveyor !");
        buildingType = BuildingManager.buildingType.Conveyor;
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Conveyor Détruit");
    }


    public void UpgradeLevel()
    {
        if (level > 2) return;

        if (level == 1)
        {
            speed = 2f;
            visualSpriteRenderer.sprite = TextureHolder.instance.advancedConveyor;
            level++;
        }
        else if (level == 2)
        {   
            speed = 3f;
            visualSpriteRenderer.sprite = TextureHolder.instance.utlimateConveyor;
            level++;
        }
    }



    /* public void UpdateSprite()
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

        var forwardBuilding = buildingManager.GetBuildingOnTile(forwardPos);
        var backwardBuilding = buildingManager.GetBuildingOnTile(backwardPos);
        var leftBuilding = buildingManager.GetBuildingOnTile(leftPos);
        var rightBuilding = buildingManager.GetBuildingOnTile(rightPos);


        if (forwardBuilding is Conveyor)
        {
            Vector2 forwardBuildingDir = forwardBuilding.GetDirection();
            if (forwardBuildingDir == forwardDir || forwardBuildingDir == -forwardDir) hasForward = true;
        }
        if (backwardBuilding is Conveyor)
        {
            Vector2 backBuildingDir = backwardBuilding.GetDirection();
            if (backBuildingDir == backwardDir || backBuildingDir == -backwardDir) hasBackward = true;
        }
        if (leftBuilding is Conveyor)
        {
            Vector2 leftBuildingDir = leftBuilding.GetDirection();
            if (leftBuildingDir == leftDir || leftBuildingDir == -leftDir) hasLeft = true;
        }
        if (rightBuilding is Conveyor)
        {
            Vector2 rightBuildingDir = rightBuilding.GetDirection();
            if (rightBuildingDir == rightDir || rightBuildingDir == -rightDir) hasRight = true;
        }


        int connection = 0;
        if (hasForward) connection++;
        if (hasBackward) connection++;
        if (hasLeft) connection++;
        if (hasRight) connection++;

        Debug.Log("Conveyor connectés : " + connection);

        if (connection == 2)
        {
            if (hasRight && hasBackward) visualSpriteRenderer.sprite = TextureHolder.instance.conveyorTurn;
        }

    }

    public void UpdateNeighbor()
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
    */
    


}
