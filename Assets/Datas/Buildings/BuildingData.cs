using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [Header("Extractor")]
    public float extractorRessourcesPerSecond;
    public int extractorCapacity;

    [Header("Foundry")]
    public int foundryCapacity;

    [Header("Builder")]
    public int builderCapacity;

}
