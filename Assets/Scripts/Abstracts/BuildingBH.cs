using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

//Définit le comportement/données de TOUT les batiments
public abstract class BuildingBH
{
    public Vector2 worldPosition;
    public int rotation;
    public ItemManager ItemManager;
    public BuildingManager buildingManager;
    public ElectricityManager ElectricityManager;
    public RessourceDictionnary RessourceDictionnary;
    public BuildingLibrary buildingLibrary;
    public Statistics playerStats;
    public Tilemap tilemap;
    public bool IsActive = true;
    public bool enoughtElectricity = false;
    public VisualElement buildingUI = null;
    public BuildingManager.buildingType buildingType;


    public float electricityProduction = 0f;
    public float electricityConsomation = 0f;


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

    public virtual void BuildingOnEnable()
    {

    }

    public virtual void BuildingOnDisable()
    {

    }


    public void SetManagers(ItemManager itemManager, BuildingManager buildingManager, ElectricityManager electricityManager, BuildingLibrary buildingLibrary, Statistics playerStats)
    {
        this.ItemManager = itemManager;
        this.buildingManager = buildingManager;
        this.ElectricityManager = electricityManager;
        this.buildingLibrary = buildingLibrary;
        this.playerStats = playerStats;

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
