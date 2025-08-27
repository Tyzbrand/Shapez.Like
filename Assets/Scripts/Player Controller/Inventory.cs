using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventoryCapacity = 10000;
    private int totalItemCount;
    private HubUI hubUI;
    private Dictionary<RessourceBehaviour.RessourceType, int> inventory = new();



    //-----------Méthodes Unity-----------
    private void Awake()
    {
        foreach (RessourceBehaviour.RessourceType type in System.Enum.GetValues(typeof(RessourceBehaviour.RessourceType)))
        {
            inventory[type] = 0;
        }
    }

    private void Start()
    {
        hubUI = ReferenceHolder.instance.hubUI;
    }


    private void Update()
    {
        if (Get(RessourceBehaviour.RessourceType.IronIngot) >= 100)
        {
            print("Felicitation Pour Ce Premier Succès !!!!!!!!!");
            Application.Quit();
        }
    }




    //-----------Getter et Setters-----------

    public void Add(RessourceBehaviour.RessourceType type, int amount)
    {
        if (amount <= 0) return;
        if (totalItemCount + amount > inventoryCapacity) return;

        inventory[type] += amount;
        totalItemCount += amount;
        hubUI.RefreshInventoryUI();
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
        totalItemCount -= amount;
        hubUI.RefreshInventoryUI();
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

    public int GetTotalItemCount()
    {
        return totalItemCount;
    }
    
}
