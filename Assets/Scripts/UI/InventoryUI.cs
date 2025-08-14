using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{


    private RectTransform inventoryUI;
    public GameObject slotPrefab;
    private Inventory inventorySC;
    private Dictionary<RessourceBehaviour.RessourceType, int> inventaire;
    public TextMeshProUGUI qte;
    public TextMeshProUGUI type;



    public void OnEnable()
    {
        inventoryUI = ReferenceHolder.instance.hubInventoryUI;
        inventorySC = ReferenceHolder.instance.inventorySC;
        inventaire = inventorySC.GetInventory();
    
        ShowInventory();
    }

    private void Update()
    {

    }

    private void ShowInventory()
    {
        foreach (RectTransform child in inventoryUI)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in inventaire)
        {

            GameObject slotGO = Instantiate(slotPrefab, inventoryUI);

            SlotUI slotui = slotGO.GetComponent<SlotUI>();

            if (slotui != null)
            {
                slotui.SetText(entry.Key, entry.Value);
            }
        }

    }






}
