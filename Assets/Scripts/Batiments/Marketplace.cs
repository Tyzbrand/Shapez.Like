using UnityEngine;

public class Marketplace : BuildingBH
{
    public Marketplace(Vector2 worldPosition) : base(worldPosition)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("MarketPlace !");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("MArketPlace Détruit");
    }
}
