using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("Extractor")]
    public float extractorRessourcesPerSecond;
    public int extractorCapacity;
    public float advancedExtractorRessourcesPerSecond;
    public int advancedExtractorCapacity;
    public float advancedExtractorConsomationPerSec;

    [Header("Foundry")]
    public int foundryCapacity;
    public int foundryInput1Capacity;
    public int foundryInput2Capacity;

    [Header("Builder")]
    public int builderCapacity;
    public int builderInputCapacity;

    [Header("Coal Generator")]
    public int CoalGeneratorCapacity;
    public float CoalGeneratorkWhPerSec;

}
