
using UnityEngine;

//Définit les données de TOUT les items (n'est pas abstract car le comportement est le meme et est définit dans "ItemManager")
public class ItemBH
{
    public RessourceBehaviour.RessourceType itemType;
    public Vector2 worldPosition;
    public Vector2 lastWorldPosition;
    public float idleTime = 0f;
    public float maxInactivityTime = 10f;
    public BuildingBH lastBuilding = null;


    public ItemBH(RessourceBehaviour.RessourceType itemType, Vector2 worldPosition)
    {
        this.itemType = itemType;
        this.worldPosition = worldPosition;
    }
}
