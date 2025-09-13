using UnityEngine;
using UnityEngine.Tilemaps;

public class Hub : BuildingBH
{
    private Inventory inventory;
    private OverlaySC overlay;
    public Hub(Vector2 worldPosition, int rotation, Tilemap tilemap) : base(worldPosition, rotation, tilemap)
    {

    }

    public override void BuidlingStart()
    {
        inventory = ReferenceHolder.instance.inventorySC;
        overlay = ReferenceHolder.instance.inGameOverlay;
        uIScript = ReferenceHolder.instance.hubUI;
        buildingType = BuildingManager.buildingType.Hub;

    }

    public override void BuildingAction(ItemBH item, Vector2 useless, BuildingBH useless2)
    {
        if (inventory.GetTotalItemCount() < inventory.inventoryCapacity)
        {
            inventory.Add(item.itemType, 1);

            playerStats.IncrementFloatStat(Statistics.statType.StoredResources, 1f);

            ItemManager.itemToRemove.Add(item);
            overlay.UpdateObjectiveText();
            overlay.UpdateStorageText();
        }
    }
    
}
