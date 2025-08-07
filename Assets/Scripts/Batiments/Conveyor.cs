using UnityEngine;

public class Conveyor : BuildingBH
{

    public Conveyor(Vector2 worldPosition, int rotation) : base(worldPosition, rotation)
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
