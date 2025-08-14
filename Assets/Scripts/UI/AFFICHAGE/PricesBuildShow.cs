
using TMPro;
using UnityEngine;


public class PricesBuildShow : MonoBehaviour
{
    public TextMeshProUGUI extractorPrice;
    public TextMeshProUGUI conveyorPrice;
    public TextMeshProUGUI foundryPrice;
    public TextMeshProUGUI marketplacePrice;
    public TextMeshProUGUI builderPrice;
    

    private BuildPriceDictionnary priceIndex;




    private void Start()
    {

        priceIndex = ReferenceHolder.instance.buildPriceDictionary;


        extractorPrice.text = priceIndex.GetPrice(ReferenceHolder.instance.extractorPrefab).ToString() + " $";
        conveyorPrice.text = priceIndex.GetPrice(ReferenceHolder.instance.conveyorPrefab).ToString() + " $";
        foundryPrice.text = priceIndex.GetPrice(ReferenceHolder.instance.foundryPrefab).ToString() + " $";
        marketplacePrice.text = "FREE";
        builderPrice.text = priceIndex.GetPrice(ReferenceHolder.instance.builderPrefab).ToString() + " $";
        
    }
}
