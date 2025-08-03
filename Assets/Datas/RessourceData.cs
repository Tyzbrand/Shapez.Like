using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RessourceData", menuName = "Scriptable Objects/RessourceData")]
public class RessourceData : ScriptableObject
{


    [System.Serializable]
    public struct RessourcePrice
    {
        public RessourceBehaviour.RessourceType ressourceType;
        public int price;
    }

    public List<RessourcePrice> prices;
    private Dictionary<RessourceBehaviour.RessourceType, int> priceLookUp;


    public void Init()
    {
        priceLookUp = new Dictionary<RessourceBehaviour.RessourceType, int>();

        foreach (var entry in prices)
        {
            priceLookUp[entry.ressourceType] = entry.price;
        }
    }

    public int GetPrice(RessourceBehaviour.RessourceType type)
    {
        if (priceLookUp == null || priceLookUp.Count == 0)
        {
            Init();

        }
        return priceLookUp.TryGetValue(type, out int price) ? price : 0;
    }
}
