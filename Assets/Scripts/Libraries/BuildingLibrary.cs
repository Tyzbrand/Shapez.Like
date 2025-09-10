using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingLibrary : MonoBehaviour
{
    private Dictionary<BuildingManager.buildingType, int> buildPrice;
    private Dictionary<BuildingManager.buildingType, Dictionary<bool, Sprite>> buildingStateSprite;
    private Dictionary<BuildingManager.buildingType, GameObject> buildingPreview;

    private Dictionary<RessourceBehaviour.RessourceType, Sprite> extractorState;

    private Placement placementSC;

    private Sprite foundryIdle;
    private Sprite foundryAction;

    private Sprite extractorIdle;
    private Sprite extractorCoal;
    private Sprite extractorStone;
    private Sprite extractorCopper;
    private Sprite extractorIron;

    private Sprite Conveyor;

    private Sprite merger;
    private Sprite splitter;
    private Sprite junction;

    private Sprite advancedExtractor;

    private Sprite builder;

    private Sprite coalGenerator;

    private Sprite depot;

    private Sprite hub;

    private Sprite wall;





    private void Awake()
    {


        buildPrice = new Dictionary<BuildingManager.buildingType, int>() { { BuildingManager.buildingType.Extractor, 100 }, { BuildingManager.buildingType.Conveyor, 10 }, { BuildingManager.buildingType.Depot, 0 }, { BuildingManager.buildingType.Foundry, 500 },
        { BuildingManager.buildingType.builder, 750 }, { BuildingManager.buildingType.CoalGenerator, 1500 }, {BuildingManager.buildingType.AdvancedExtractor, 3000}, {BuildingManager.buildingType.Junction, 50}, {BuildingManager.buildingType.Splitter, 75},
        { BuildingManager.buildingType.Merger, 75}, { BuildingManager.buildingType.Wall, 50} };


    }

    private void Start()
    {

        foundryIdle = TextureHolder.instance.foundryIdle;
        foundryAction = TextureHolder.instance.foundryAction;

        extractorIdle = TextureHolder.instance.extractorIdle;

        Conveyor = TextureHolder.instance.Conveyor;

        merger = TextureHolder.instance.merger;
        splitter = TextureHolder.instance.splitter;
        junction = TextureHolder.instance.junction;

        advancedExtractor = TextureHolder.instance.advancedExtractor;

        builder = TextureHolder.instance.builder;

        coalGenerator = TextureHolder.instance.coalGenerator;

        depot = TextureHolder.instance.depot;

        hub = TextureHolder.instance.hub;

        wall = TextureHolder.instance.wall;


        buildingStateSprite = new Dictionary<BuildingManager.buildingType, Dictionary<bool, Sprite>>
        {
            {BuildingManager.buildingType.Foundry, new Dictionary<bool, Sprite>{{true, foundryAction}, {false, foundryIdle}}},
            {BuildingManager.buildingType.Extractor, new Dictionary<bool, Sprite>{{true, extractorIdle}, {false, extractorIdle}}},
            {BuildingManager.buildingType.Conveyor, new Dictionary<bool, Sprite>{{false, Conveyor}}},
            {BuildingManager.buildingType.Merger, new Dictionary<bool, Sprite>{{false, merger}}},
            {BuildingManager.buildingType.Splitter, new Dictionary<bool, Sprite>{{false, splitter}}},
            {BuildingManager.buildingType.Junction, new Dictionary<bool, Sprite>{{false, junction}}},
            {BuildingManager.buildingType.AdvancedExtractor, new Dictionary<bool, Sprite>{{true, advancedExtractor}, {false, advancedExtractor}}},
            {BuildingManager.buildingType.builder, new Dictionary<bool, Sprite>{{true, builder}, {false, builder}}},
            {BuildingManager.buildingType.CoalGenerator, new Dictionary<bool, Sprite>{{true, coalGenerator}, {false, coalGenerator}}},
            {BuildingManager.buildingType.Depot, new Dictionary<bool, Sprite>{{false, depot}}},
            {BuildingManager.buildingType.Hub, new Dictionary<bool, Sprite>{{false, hub}}},
            {BuildingManager.buildingType.Wall, new Dictionary<bool, Sprite>{{false, wall}}}
        };

        extractorState = new Dictionary<RessourceBehaviour.RessourceType, Sprite>
        {
            {RessourceBehaviour.RessourceType.Stone, TextureHolder.instance.extractorStone}, {RessourceBehaviour.RessourceType.Iron, TextureHolder.instance.extractorIron},
            { RessourceBehaviour.RessourceType.Copper, TextureHolder.instance.extractorCopper}, {RessourceBehaviour.RessourceType.Coal, TextureHolder.instance.extractorCoal}
        };

        buildingPreview = new Dictionary<BuildingManager.buildingType, GameObject>
        {
            {BuildingManager.buildingType.Extractor, ReferenceHolder.instance.extractorPreview}, {BuildingManager.buildingType.AdvancedExtractor, ReferenceHolder.instance.advancedExtractorPreview},
            { BuildingManager.buildingType.builder, ReferenceHolder.instance.builderPrview}, {BuildingManager.buildingType.CoalGenerator, ReferenceHolder.instance.coalGeneratorPreview},
            {BuildingManager.buildingType.Conveyor, ReferenceHolder.instance.conveyorPreview}, {BuildingManager.buildingType.Foundry, ReferenceHolder.instance.foundryPreview},
            {BuildingManager.buildingType.Junction, ReferenceHolder.instance.junctionPreview}, {BuildingManager.buildingType.marketplace, ReferenceHolder.instance.marketplacePreview},
            {BuildingManager.buildingType.Merger, ReferenceHolder.instance.mergerPreview}, {BuildingManager.buildingType.Splitter, ReferenceHolder.instance.splitterPreview},
        };
    }



    public int GetBuildingPrice(BuildingManager.buildingType buildingType)
    {
        if (buildPrice.TryGetValue(buildingType, out int price))
        {
            return price;
        }
        else
            return 0;

    }

    public Sprite GetBuildingSpriteForState(BuildingManager.buildingType type, bool state)
    {
        if (buildingStateSprite.TryGetValue(type, out var stateDict))
        {
            if (stateDict.TryGetValue(state, out Sprite buildingSprite))
            {
                return buildingSprite;
            }
        }
        return null;
    }

    public GameObject GetBuildingPreview(BuildingManager.buildingType type)
    {
        if (buildingPreview.TryGetValue(type, out var preview))
            return preview;
        else return null;
    }
    public Sprite GetExtractorStateSprite(RessourceBehaviour.RessourceType type)
    {
        if (extractorState.TryGetValue(type, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }


}
