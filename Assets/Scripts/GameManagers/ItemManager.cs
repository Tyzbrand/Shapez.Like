using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;


public class ItemManager : MonoBehaviour
{
    private List<ItemBH> itemReferencer = new();
    private List<GameObject> freeVisual = new();
    public HashSet<ItemBH> itemToRemove = new();
    private Dictionary<ItemBH, GameObject> itemVisualReferencer = new();


    private BuildingManager buildingManager;
    private PlayerVariables player;
    private Inventory inventory;
    private OverlaySC overlay;
    private Statistics playerStats;
    private SpriteAtlas resourcesSprite;

    private RessourceDictionnary ressourceDictionnary;
    private RessourceData data;

    private GameObject itemPrefab;
    public Transform parent;


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

            Vector2 delta = other.worldPosition - position;

            if (Mathf.Abs(delta.x) < itemSpacing && Mathf.Abs(delta.y) < itemSpacing)
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
            Vector2 delta = other.worldPosition - position;

            if (Mathf.Abs(delta.x) < itemSpacing && Mathf.Abs(delta.y) < itemSpacing)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearItems()
    {
        foreach (ItemBH item in itemReferencer.ToList())
        {
            RemoveItem(item);
        }
    }


    //Visuel

    private void AddVisualToItem(ItemBH item, Vector2 worldPos)
    {

        if (!itemVisualReferencer.ContainsKey(item))
        {
            Sprite spriteToUse = resourcesSprite.GetSprite(ressourceDictionnary.GetRessourceSpriteName(item.itemType));
            if (spriteToUse == null) Debug.Log("Pas de sprite trouvé");

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
                GameObject visual = Instantiate(itemPrefab, worldPos, Quaternion.identity, parent);
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


    public void PreInstanciateVisuals(int amount, Transform parent)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject visual = Instantiate(itemPrefab, parent);
            visual.SetActive(false);
            freeVisual.Add(visual);
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
        inventory = ReferenceHolder.instance.inventorySC;
        overlay = ReferenceHolder.instance.inGameOverlay;
        playerStats = ReferenceHolder.instance.playerStats;
        resourcesSprite = ReferenceHolder.instance.resourcesSprite;


    }

    private void Update()
    {   
        itemToRemove.Clear();
        foreach (var item in itemReferencer)
        {

            BuildingBH currentBuilding = buildingManager.GetBuildingOnTile(item.worldPosition);

            if (currentBuilding != null)
            {
                Vector2 currentDirection = currentBuilding.GetDirection();
                Vector2 nextPos = item.worldPosition + currentDirection * Time.deltaTime;

                BuildingBH nextBuilding = buildingManager.GetBuildingOnTile(nextPos);

                if (nextBuilding != null && !(nextBuilding.buildingType is BuildingManager.buildingType.None) && !(nextBuilding is Conveyor)) nextBuilding.BuildingAction(item, nextPos, currentBuilding);
                else if (nextBuilding is Conveyor && IsSpaceFree(nextPos, item))
                {
                    if (item.lastBuilding != null && item.lastBuilding != nextBuilding)
                    {
                        Vector2 nextDirection = nextBuilding.GetDirection();
                        Vector2 nextCenter = CenterOnPerpendicularAxis(nextPos, nextDirection);

                        if (currentDirection != nextDirection && IsSpaceFree(nextCenter, item))
                        {
                            item.worldPosition = nextCenter;
                        }
                        else if (IsSpaceFree(nextCenter, item))
                        {
                            item.worldPosition = nextPos;
                        }

                    }
                    else
                    {
                        item.worldPosition = nextPos;
                    }
                    item.lastBuilding = nextBuilding;
                }
                else
                {
                    item.lastBuilding = currentBuilding;
                }

            }

            if (item.lastWorldPosition == item.worldPosition && !(currentBuilding is Conveyor))
            {
                item.idleTime += Time.deltaTime;
            }
            if (item.idleTime >= item.maxInactivityTime)
            {
                itemToRemove.Add(item);
            }

            item.lastWorldPosition = item.worldPosition;

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

    //---------------Méthodes Utilitaires---------------

    public Vector2 CenterOnPerpendicularAxis(Vector2 currentPos, Vector2 direction)
    {
        Vector2 centeredPos = currentPos;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            centeredPos.y = Mathf.FloorToInt(currentPos.y) + 0.5f;
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            centeredPos.x = Mathf.FloorToInt(currentPos.x) + 0.5f;
        }
        return centeredPos;
    }


    public bool IsItNextBuildingExit(ItemBH item, BuildingBH nextBuilding, Vector2 nextPos)
    {
        Vector2 buildingDir = nextBuilding.GetDirection();
        Vector2 fromDir = (item.worldPosition - nextPos).normalized;

        return Vector2.Dot(fromDir, buildingDir) <= 0;

    }


}
