using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : BuildingBH
{
    public Wall(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        buildingType = BuildingManager.buildingType.Wall;
        Debug.Log("Wall construit");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Wall Détruit");
    }
}
