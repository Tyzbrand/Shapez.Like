using UnityEngine;

public class Conveyor : BuildingBH
{
    public Conveyor(Vector2 worldPosition) : base(worldPosition)
    {

    }
    public override void BuildingUpdate()
    {
        Debug.Log("Conveyor");
    }
}
