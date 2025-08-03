using UnityEngine;

[CreateAssetMenu(fileName = "ExtractorData", menuName = "Scriptable Objects/ExtractorData")]
public class ExtractorData : ScriptableObject
{
    public int price;
    public float ressourcesPerSecond;
    public int capacity;
}

