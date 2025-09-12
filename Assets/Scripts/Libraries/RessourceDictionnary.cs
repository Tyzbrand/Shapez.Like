using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class RessourceDictionnary : MonoBehaviour
{
    private Dictionary<RessourceBehaviour.RessourceType, Sprite> ressourceIcons;
    private Dictionary<RessourceBehaviour.RessourceType, Sprite> ressoruceSprite;

    //autres
    private SpriteAtlas resourceSpriteCollection;
    //Ressources type
    private RessourceBehaviour.RessourceType stone = RessourceBehaviour.RessourceType.Stone;
    private RessourceBehaviour.RessourceType coal = RessourceBehaviour.RessourceType.Coal;
    private RessourceBehaviour.RessourceType iron = RessourceBehaviour.RessourceType.Iron;
    private RessourceBehaviour.RessourceType ironIngot = RessourceBehaviour.RessourceType.IronIngot;
    private RessourceBehaviour.RessourceType ironPlate = RessourceBehaviour.RessourceType.IronPlate;
    private RessourceBehaviour.RessourceType copper = RessourceBehaviour.RessourceType.Copper;
    private RessourceBehaviour.RessourceType copperIngot = RessourceBehaviour.RessourceType.CopperIngot;
    private RessourceBehaviour.RessourceType copperPlate = RessourceBehaviour.RessourceType.CopperPlate;
    private RessourceBehaviour.RessourceType gear = RessourceBehaviour.RessourceType.Gear;
    private RessourceBehaviour.RessourceType wireCoil = RessourceBehaviour.RessourceType.WireCoil;
    private RessourceBehaviour.RessourceType compound = RessourceBehaviour.RessourceType.Compound;
    private RessourceBehaviour.RessourceType brick = RessourceBehaviour.RessourceType.Brick;



    private void Start()
    {
        resourceSpriteCollection = ReferenceHolder.instance.resourcesSprite;

        ressourceIcons = new Dictionary<RessourceBehaviour.RessourceType, Sprite>{
        {RessourceBehaviour.RessourceType.Iron, TextureHolder.instance.iron}, {RessourceBehaviour.RessourceType.Copper, TextureHolder.instance.copper},
        {RessourceBehaviour.RessourceType.Coal, TextureHolder.instance.Coal}, {RessourceBehaviour.RessourceType.IronIngot, TextureHolder.instance.ironIngot}, {RessourceBehaviour.RessourceType.CopperIngot, TextureHolder.instance.copperIngot},
        {RessourceBehaviour.RessourceType.IronPlate, TextureHolder.instance.ironPlate},{RessourceBehaviour.RessourceType.CopperPlate, TextureHolder.instance.copperPlate}, {RessourceBehaviour.RessourceType.Stone, TextureHolder.instance.Stone},
        { RessourceBehaviour.RessourceType.Gear, TextureHolder.instance.gear}, {RessourceBehaviour.RessourceType.WireCoil, TextureHolder.instance.wireCoil}, {RessourceBehaviour.RessourceType.Compound, TextureHolder.instance.Compound},
        {RessourceBehaviour.RessourceType.Brick, TextureHolder.instance.Brick}
        };

        ressoruceSprite = new Dictionary<RessourceBehaviour.RessourceType, Sprite>{
            {stone, resourceSpriteCollection.GetSprite("Stone")}, {coal, resourceSpriteCollection.GetSprite("Coal")}, {iron, resourceSpriteCollection.GetSprite("Iron-ore")}, {ironIngot, resourceSpriteCollection.GetSprite("Iron-Ingot")},
            {ironPlate, resourceSpriteCollection.GetSprite("Iron-plate")}, {copper, resourceSpriteCollection.GetSprite("Copper-ore")}, {copperIngot, resourceSpriteCollection.GetSprite("Copper-Ingot")},
            { copperPlate, resourceSpriteCollection.GetSprite("Copper-plate")}, {gear, resourceSpriteCollection.GetSprite("Gear")}, {wireCoil, resourceSpriteCollection.GetSprite("Wire-coil")}, 
            {compound, resourceSpriteCollection.GetSprite("Compound")}, {brick, resourceSpriteCollection.GetSprite("Bricks")}
        };

    }

    public Sprite GetRessourceSprite(RessourceBehaviour.RessourceType type)
    {
        ressoruceSprite.TryGetValue(type, out Sprite sprite);
        return sprite;
    }

    public Sprite GetIcon(RessourceBehaviour.RessourceType type)
    {
        ressourceIcons.TryGetValue(type, out Sprite icon);
        return icon;
    }
}
