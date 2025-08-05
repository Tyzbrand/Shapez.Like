using System.Numerics;


//Définit les données de TOUT les items (n'est pas abstract car le comportement est le meme et est définit dans "ItemManager")
public class ItemBH
{
    public RessourceBehaviour.RessourceType itemType;
    public Vector2 wordlPosition;


    public ItemBH(RessourceBehaviour.RessourceType itemType, Vector2 wordlPosition)
    {
        this.itemType = itemType;
        this.wordlPosition = wordlPosition;
    }
}
