using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    private List<ItemBH> itemReferencer = new();
    private List<GameObject> freeVisual = new();
    private List<ItemBH> itemToRemove = new();
    private Dictionary<ItemBH, GameObject> itemVisualReferencer = new();


    private BuildingManager buildingManager;
    private PlayerVariables player;

    private RessourceDictionnary ressourceDictionnary;
    private RessourceData data;

    private GameObject itemPrefab;


    private const float itemSpacing = 0.5f;


    //---------------Méthodes Implémentées---------------
    //Logique

    public void AddItem(ItemBH item, Vector2 worldPos)
    {
        if (!itemReferencer.Contains(item))
        {
            itemReferencer.Add(item);
            AddVisualToItem(item, worldPos);
        }
    }

    public void RemoveItem(ItemBH item)
    {
        if (itemReferencer.Contains(item))
        {
            RemoveVisualToItem(item);
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

    public bool IsSpaceFree(Vector2 position)
    {
        foreach (var other in itemReferencer)
        {
            if (Vector2.Distance(other.worldPosition, position) < itemSpacing)
            {
                return false;
            }
        }
        return true;
    }


    //Visuel

    private void AddVisualToItem(ItemBH item, Vector2 worldPos)
    {

        if (!itemVisualReferencer.ContainsKey(item))
        {
            Sprite spriteToUse = ressourceDictionnary.GetRessourceSprite(item.itemType);

            if (freeVisual.Count >= 1)
            {
                GameObject visual = freeVisual[0];

                freeVisual.Remove(visual);
                itemVisualReferencer.Add(item, visual);

                visual.GetComponent<SpriteRenderer>().sprite = spriteToUse;
                visual.transform.position = worldPos;
                visual.SetActive(true);

            }
            else
            {
                GameObject visual = Instantiate(itemPrefab, worldPos, Quaternion.identity);
                itemVisualReferencer.Add(item, visual);
                visual.GetComponent<SpriteRenderer>().sprite = spriteToUse;

            }


        }


    }

    private void RemoveVisualToItem(ItemBH item)
    {
        if (itemVisualReferencer.TryGetValue(item, out GameObject visual))
        {

            freeVisual.Add(visual);
            itemVisualReferencer.Remove(item);
            visual.SetActive(false);

        }
    }


    private void UpdateVisual()
    {
        foreach (var kvp in itemVisualReferencer)
        {
            ItemBH item = kvp.Key;
            GameObject visual = kvp.Value;

            visual.transform.position = item.worldPosition;
        }
    }


    //---------------Méthodes Unity---------------


    private void Start()
    {
        buildingManager = ReferenceHolder.instance.buildingManager;
        ressourceDictionnary = ReferenceHolder.instance.ressourceDictionnary;
        itemPrefab = ReferenceHolder.instance.itemPrefab;
        player = ReferenceHolder.instance.playervariable;
        data = ReferenceHolder.instance.ressourceData;
    }

    private void Update()
    {
        itemToRemove.Clear();
        foreach (var item in itemReferencer)
        {
            BuildingBH currentBuilding = buildingManager.GetBuildingOnTile(item.worldPosition);

            if (currentBuilding == null)
            {
                continue;
            }
            Vector2 currentDirection = currentBuilding.GetDirection();

            Vector2 nextPos = item.worldPosition + currentDirection * Time.deltaTime;

            BuildingBH nextBuilding = buildingManager.GetBuildingOnTile(nextPos);


            if (nextBuilding is Marketplace)
            {   
                player.Money += data.GetPrice(item.itemType);
                itemToRemove.Add(item);
                
            }

            if (nextBuilding is Conveyor && IsSpaceFree(nextPos, item))
            {
                item.worldPosition = nextPos;

            }
        }

        foreach (ItemBH itemToRemove in itemToRemove)
        {
            RemoveItem(itemToRemove);
        }

    }

    private void LateUpdate()
    {
        UpdateVisual();
    }


}
