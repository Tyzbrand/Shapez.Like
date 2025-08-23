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

    [Header("Builder")]
    public int builderCapacity;

    [Header("Coal Generator")]
    public int CoalGeneratorCapacity;
    public float CoalGeneratorkWhPerSec;

}
