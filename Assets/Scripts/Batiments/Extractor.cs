using UnityEngine;

public class Extractor : BuildingBH
{
    public float ejectTimer = 3f;
    public float timer = 0f;

    public Extractor(Vector2 worldPosition) : base(worldPosition)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Extractor !");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Extractor Détruit !");
    }

}
