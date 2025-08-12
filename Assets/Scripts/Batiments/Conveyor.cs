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
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Conveyor Détruit");
    }


}
