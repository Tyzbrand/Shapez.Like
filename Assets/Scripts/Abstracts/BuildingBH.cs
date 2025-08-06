using UnityEngine;

//Définit le comportement/données de TOUT les batiments
public abstract class BuildingBH
{
    public Vector2 worldPosition;
    public ItemManager ItemManager;
    public BuildingManager buildingManager;

    public BuildingBH(Vector2 wordlPosition)
    {
        this.worldPosition = wordlPosition;
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

    


}
