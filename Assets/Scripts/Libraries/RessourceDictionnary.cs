using System;
using System.Collections.Generic;
using UnityEngine;

public class RessourceDictionnary : MonoBehaviour
{
    private Dictionary<RessourceBehaviour.RessourceType, String> ressourceSpriteName;
    private Dictionary<RessourceBehaviour.RessourceType, Sprite> ressourceIcons;


    private void Start()
    {

        ressourceSpriteName = new Dictionary<RessourceBehaviour.RessourceType, string> {
        {RessourceBehaviour.RessourceType.Iron, "Iron-ore"}, {RessourceBehaviour.RessourceType.Copper, "Copper-ore"},
        {RessourceBehaviour.RessourceType.Coal, "Coal"}, {RessourceBehaviour.RessourceType.Silver, "Silver-ore"}, {RessourceBehaviour.RessourceType.IronIngot, "Iron-Ingot"},
        {RessourceBehaviour.RessourceType.CopperIngot, "Copper-Ingot"}, {RessourceBehaviour.RessourceType.SilverIngot, "Silver-Ingot"}, {RessourceBehaviour.RessourceType.IronPlate, "Iron-plate"},
        {RessourceBehaviour.RessourceType.CopperPlate, "Copper-plate"}, {RessourceBehaviour.RessourceType.Stone, "Stone"}, {RessourceBehaviour.RessourceType.Gear, "Gear"}, {RessourceBehaviour.RessourceType.WireCoil, "Wire-coil"},
        {RessourceBehaviour.RessourceType.Compound, "Compound"}, {RessourceBehaviour.RessourceType.Brick, "Bricks"}};

        ressourceIcons = new Dictionary<RessourceBehaviour.RessourceType, Sprite>{
        {RessourceBehaviour.RessourceType.Iron, TextureHolder.instance.iron}, {RessourceBehaviour.RessourceType.Copper, TextureHolder.instance.copper},
        {RessourceBehaviour.RessourceType.Coal, TextureHolder.instance.Coal}, {RessourceBehaviour.RessourceType.IronIngot, TextureHolder.instance.ironIngot}, {RessourceBehaviour.RessourceType.CopperIngot, TextureHolder.instance.copperIngot},
        {RessourceBehaviour.RessourceType.IronPlate, TextureHolder.instance.ironPlate},{RessourceBehaviour.RessourceType.CopperPlate, TextureHolder.instance.copperPlate}, {RessourceBehaviour.RessourceType.Stone, TextureHolder.instance.Stone},
        { RessourceBehaviour.RessourceType.Gear, TextureHolder.instance.gear}, {RessourceBehaviour.RessourceType.WireCoil, TextureHolder.instance.wireCoil}, {RessourceBehaviour.RessourceType.Compound, TextureHolder.instance.Compound},
        {RessourceBehaviour.RessourceType.Brick, TextureHolder.instance.Brick}
        };

    }

    public String GetRessourceSpriteName(RessourceBehaviour.RessourceType type)
    {
        ressourceSpriteName.TryGetValue(type, out String name);
        return name;
    }

    public Sprite GetIcon(RessourceBehaviour.RessourceType type)
    {
        ressourceIcons.TryGetValue(type, out Sprite icon);
        return icon;
    }
}
