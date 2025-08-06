using System.Collections.Generic;
using UnityEngine;

public class RessourceDictionnary : MonoBehaviour
{
    private Dictionary<RessourceBehaviour.RessourceType, Sprite> ressourceSprite = new();

    private Sprite ironPrefab;
    private Sprite copperPrefab;
    private Sprite coalPrefab;
    private Sprite ironIngotPrefab;
    private Sprite copperIngotPrefab;






    private void Start()
    {
        ironPrefab = ReferenceHolder.instance.ironSprite;
        ironIngotPrefab = ReferenceHolder.instance.ironIngotSprite;
        coalPrefab = ReferenceHolder.instance.coalSprite;
        copperPrefab = ReferenceHolder.instance.copperSprite;
        copperIngotPrefab = ReferenceHolder.instance.copperIngotSprite;



        ressourceSprite.Add(RessourceBehaviour.RessourceType.Iron, ironPrefab);
        ressourceSprite.Add(RessourceBehaviour.RessourceType.Copper, copperPrefab);
        ressourceSprite.Add(RessourceBehaviour.RessourceType.Coal, coalPrefab);
        ressourceSprite.Add(RessourceBehaviour.RessourceType.IronIngot, ironIngotPrefab);
        ressourceSprite.Add(RessourceBehaviour.RessourceType.CopperIngot, copperIngotPrefab);
    }



    public Sprite GetRessourceSprite(RessourceBehaviour.RessourceType type)
    {
        ressourceSprite.TryGetValue(type, out Sprite sprite);
        return sprite;
    }





}
