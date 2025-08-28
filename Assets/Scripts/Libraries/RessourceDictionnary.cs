using System;
using System.Collections.Generic;
using UnityEngine;

public class RessourceDictionnary : MonoBehaviour
{
    private Dictionary<RessourceBehaviour.RessourceType, String> ressourceSprite;


    private void Start()
    {

        ressourceSprite = new Dictionary<RessourceBehaviour.RessourceType, string> {
        {RessourceBehaviour.RessourceType.Iron, "Iron-ore"}, {RessourceBehaviour.RessourceType.Copper, "Copper-ore"},
        {RessourceBehaviour.RessourceType.Coal, "Coal"}, {RessourceBehaviour.RessourceType.Silver, "Silver-ore"}, {RessourceBehaviour.RessourceType.IronIngot, "Iron-Ingot"},
        {RessourceBehaviour.RessourceType.CopperIngot, "Copper-Ingot"}, {RessourceBehaviour.RessourceType.SilverIngot, "Silver-Ingot"}, {RessourceBehaviour.RessourceType.IronPlate, "Iron-plate"},
        {RessourceBehaviour.RessourceType.CopperPlate, "Copper-plate"}, {RessourceBehaviour.RessourceType.Stone, "Stone"}};

    }

    public String GetRessourceSpriteName(RessourceBehaviour.RessourceType type)
    {
        ressourceSprite.TryGetValue(type, out String name);
        return name;
    }

}
