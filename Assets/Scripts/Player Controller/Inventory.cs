using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<RessourceBehaviour.RessourceType, int> inventory = new();

    private void Awake()
    {
        foreach (RessourceBehaviour.RessourceType type in System.Enum.GetValues(typeof(RessourceBehaviour.RessourceType)))
        {
            inventory[type] = 0;
        }
    }


    private void Update()
    {
        if (Get(RessourceBehaviour.RessourceType.IronIngot) >= 100)
        {
            print("Felicitation Pour Ce Premier Succès !!!!!!!!!");
            Application.Quit();
        }
    }

   
   
   
   
   
    public void Add(RessourceBehaviour.RessourceType type, int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        inventory[type] += amount;
    }

    public bool Remove(RessourceBehaviour.RessourceType type, int amount)
    {
        if (amount <= 0)
        {
            return false;
        }
        if (inventory[type] < amount)
        {
            return false;
        }
        inventory[type] -= amount;
        return true;
    }


    public int Get(RessourceBehaviour.RessourceType type)
    {
        return inventory.ContainsKey(type) ? inventory[type] : 0;
    }

    public Dictionary<RessourceBehaviour.RessourceType, int> GetInventory()
    {
        return new Dictionary<RessourceBehaviour.RessourceType, int>(inventory);
    }
    
}
