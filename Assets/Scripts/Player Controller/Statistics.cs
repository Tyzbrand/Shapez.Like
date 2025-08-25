using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public float trackedStat1 = 0f;

    public enum statType
    {
        MoneyAllTime,
        RawResourcesAllTime,
        ProcessedResourceAllTime,
        StoredResources,
        SoldResources,
        ProducedElec,
        UsedElec,
    

        
    }



    private Dictionary<statType, float> floatStats = new();

    private void Awake()
    {
        foreach (statType type in System.Enum.GetValues(typeof(statType)))
        {
            floatStats[type] = 0f;
        }
    }

    public void IncrementFloatStat(statType type, float amount)
    {
        floatStats[type] += amount;
    }

    public float GetFloatStat(statType type)
    {
        floatStats.TryGetValue(type, out float value);
        return value;
        
    }

    void Update()
    {
        trackedStat1 = GetFloatStat(statType.UsedElec);
    }
}
