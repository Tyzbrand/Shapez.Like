using UnityEngine;
using UnityEngine.Tilemaps;

public class Marketplace : BuildingBH
{
    public Marketplace(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
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
