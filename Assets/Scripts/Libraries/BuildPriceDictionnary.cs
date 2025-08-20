using System.Collections.Generic;
using UnityEngine;

public class BuildPriceDictionnary : MonoBehaviour
{
    private Dictionary<GameObject, int> buildPrice;



    //Prefabs
    private GameObject extractor;
    private GameObject conveyor;
    private GameObject marketplace;
    private GameObject foundry;
    private GameObject builder;
    private GameObject coalGenerator;



    private void Awake()
    {

        extractor = ReferenceHolder.instance.extractorPrefab;
        conveyor = ReferenceHolder.instance.conveyorPrefab;
        marketplace = ReferenceHolder.instance.marketplacePrefab;
        foundry = ReferenceHolder.instance.foundryPrefab;
        builder = ReferenceHolder.instance.builderPrefab;
        coalGenerator = ReferenceHolder.instance.coalGeneratorPrefab;

        buildPrice = new Dictionary<GameObject, int>()
        {
            {extractor, 100},
            {conveyor, 10},
            {marketplace, 0},
            {foundry, 500},
            {builder, 750},
            {coalGenerator, 1500}
        };



    }


    public int GetPrice(GameObject building)
    {
        if (buildPrice.TryGetValue(building, out int price))
        {
            return price;
        }
        else
            return 0;
        
    }
}
