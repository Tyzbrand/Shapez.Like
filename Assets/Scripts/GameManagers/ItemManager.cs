using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemBH> itemReferencer = new();


    private BuildingManager buildingManager;


    private const float itemSpacing = 0.5f;


    //---------------Méthodes Implémentées---------------

    public void AddItem(ItemBH item)
    {
        if (!itemReferencer.Contains(item))
        {
            itemReferencer.Add(item);
        }
    }

    public void RemoveItem(ItemBH item)
    {
        if (itemReferencer.Contains(item))
        {
            itemReferencer.Remove(item);
        }
    }

    public bool IsSpaceFree(Vector2 position, ItemBH currentItem)
    {
        foreach (var other in itemReferencer)
        {
            if (other == currentItem)
            {
                continue;
            }
            if (Vector2.Distance(other.worldPosition, position) < itemSpacing)
            {
                return false;
            }
        }
        return true;
    }

    //---------------Méthodes Unity---------------


    private void Start()
    {
        buildingManager = ReferenceHolder.instance.buildingManager;
    }

    private void Update()
    {
        foreach (var item in itemReferencer)
        {
            Vector2 nextPos = item.worldPosition + Vector2.right * Time.deltaTime;

            if (buildingManager.GetBuildingOnTile(nextPos) != null && IsSpaceFree(nextPos, item))
            {
                item.worldPosition = nextPos;
            }
        }
    }
}
