using System.Numerics;



//Définit le comportement/données de TOUT les batiments
public abstract class BuildingBH
{
    public Vector2 worldPosition;

    public BuildingBH(Vector2 wordlPosition)
    {
        this.worldPosition = wordlPosition;
    }

    public virtual void BuildingUpdate() //Appelé toute les frames par le "BuildingManager"
    {

    }


}
