using System.Collections.Generic;
using UnityEngine;

public class RessourceDictionnary : MonoBehaviour
{
    private Dictionary<RessourceBehaviour.RessourceType, GameObject> ressourcePrefab = new();

    private GameObject ironPrefab;
    private GameObject copperPrefab;
    private GameObject coalPrefab;
    private GameObject ironIngotPrefab;
    private GameObject copperIngotPrefab;






    private void Start()
    {
        ironPrefab = ReferenceHolder.instance.ironPrefab;
        ironIngotPrefab = ReferenceHolder.instance.ironIngotPrefab;
        coalPrefab = ReferenceHolder.instance.coalPrefab;
        copperPrefab = ReferenceHolder.instance.copperPrefab;
        copperIngotPrefab = ReferenceHolder.instance.copperIngotPrefab;



        ressourcePrefab.Add(RessourceBehaviour.RessourceType.Iron, ironPrefab);
        ressourcePrefab.Add(RessourceBehaviour.RessourceType.Copper, copperPrefab);
        ressourcePrefab.Add(RessourceBehaviour.RessourceType.Coal, coalPrefab);
        ressourcePrefab.Add(RessourceBehaviour.RessourceType.IronIngot, ironIngotPrefab);
        ressourcePrefab.Add(RessourceBehaviour.RessourceType.CopperIngot, copperIngotPrefab);
    }



    public GameObject GetRessourcePrefab(RessourceBehaviour.RessourceType type)
    {
        return ressourcePrefab.TryGetValue(type, out var prefab) ? prefab : null;
    }





}
