using System;
using UnityEngine;
using UnityEngine.Tilemaps;

//Définit le comportement/données de TOUT les batiments
public abstract class BuildingBH
{
    public Vector2 worldPosition;
    public int rotation;
    public ItemManager ItemManager;
    public BuildingManager buildingManager;
    public RessourceDictionnary RessourceDictionnary;
    public Tilemap tilemap;
    public bool IsActive = true;


    public BuildingBH(Vector2 wordlPosition, int rotation, Tilemap tilemap)
    {
        this.worldPosition = wordlPosition;
        this.rotation = rotation;
        this.tilemap = tilemap;
    }

    public virtual void BuildingUpdate() //Appelé toute les frames par le "BuildingManager"
    {

    }

    public virtual void BuidlingStart()
    {

    }

    public virtual void BuildingOnDestroy()
    {

    }


    public void SetManagers(ItemManager itemManager, BuildingManager buildingManager)
    {
        this.ItemManager = itemManager;
        this.buildingManager = buildingManager;
    }

    public void SetDico(RessourceDictionnary ressourceDictionnary)
    {
        this.RessourceDictionnary = ressourceDictionnary;
    }

    public Vector2 GetDirection()
    {
        switch (rotation)
        {
            case 0:
                return Vector2.down;
            case 90:
                return Vector2.right;
            case 180:
                return Vector2.up;
            case 270:
                return Vector2.left;
        }
        return Vector2.right;
    }

    


}
