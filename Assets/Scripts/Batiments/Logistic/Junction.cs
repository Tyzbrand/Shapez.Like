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

}
