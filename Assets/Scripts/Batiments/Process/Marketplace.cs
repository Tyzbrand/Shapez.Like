using UnityEngine;
using UnityEngine.Tilemaps;

public class Marketplace : BuildingBH
{
    private OverlaySC overlay;
    private PlayerVariables player;
    private RessourceData data;
    public Marketplace(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        overlay = ReferenceHolder.instance.inGameOverlay;
        player = ReferenceHolder.instance.playervariable;
        data = ReferenceHolder.instance.ressourceData;

        buildingType = BuildingManager.buildingType.marketplace;
        Debug.Log("MarketPlace !");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("MArketPlace Détruit");
    }

    public override void BuildingAction(ItemBH item, Vector2 usless, BuildingBH useless)
    {   
        int price = data.GetPrice(item.itemType);
        Debug.Log(price);

        player.Money += price;
        ItemManager.itemToRemove.Add(item);
        overlay.UpdateMoneyText();

        playerStats.IncrementFloatStat(Statistics.statType.MoneyAllTime, price);
        playerStats.IncrementFloatStat(Statistics.statType.SoldResources, 1f);
    }
}   

