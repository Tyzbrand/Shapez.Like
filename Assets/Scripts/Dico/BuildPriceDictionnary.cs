using System.Collections.Generic;
using UnityEngine;

public class BuildPriceDictionnary : MonoBehaviour
{
    private Dictionary<GameObject, int> buildPrice = new();



    //Prefabs
    private GameObject extractor;
    private GameObject conveyor;
    private GameObject marketplace;
    private GameObject foundry;
    private GameObject builder;



    private void Start()
    {
        extractor = ReferenceHolder.instance.extractorPrefab;
        conveyor = ReferenceHolder.instance.conveyorPrefab;
        marketplace = ReferenceHolder.instance.marketplacePrefab;
        foundry = ReferenceHolder.instance.foundryPrefab;
        builder = ReferenceHolder.instance.builderPrefab;







        buildPrice.Add(extractor, 100);
        buildPrice.Add(conveyor, 10);
        buildPrice.Add(marketplace, 0);
        buildPrice.Add(foundry, 500);
        buildPrice.Add(builder, 750);
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
