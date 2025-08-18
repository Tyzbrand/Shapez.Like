using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HubUI : MonoBehaviour
{
    private UIDocument uI;
    private VisualElement panel;
    private ListView inventoryList;
    private Inventory inventory;
    private PlayerVariables player;

    private List<KeyValuePair<RessourceBehaviour.RessourceType, int>> inventoryData;

    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        inventory = ReferenceHolder.instance.inventorySC;
        player = ReferenceHolder.instance.playervariable;

        panel = uI.rootVisualElement.Q<VisualElement>("InventoryUI");
        inventoryList = panel.Q<ListView>("InventoryList");

        inventoryData = new List<KeyValuePair<RessourceBehaviour.RessourceType, int>>(inventory.GetInventory()
            .Where(kv => kv.Key != RessourceBehaviour.RessourceType.Empty)
            .ToList()
        );

        inventoryList.makeItem = () =>
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Column;

            var label = new Label();
            label.style.height = 20;   // hauteur du texte
            container.Add(label);

            container.style.height = 35; // 20px texte + 15px espace
            return container;
        };

        inventoryList.fixedItemHeight = 35;

        inventoryList.bindItem = (element, index) =>
        {
            var kv = inventoryData[index];
            var label = element.Q<Label>();
            label.text = $"{kv.Key} : {kv.Value}";
        };

        inventoryList.itemsSource = inventoryData;
        inventoryList.selectionType = SelectionType.None;
    }



    //Rafraichir la liste d'items
    public void RefreshInventoryUI()
    {
        inventoryData.Clear();
        inventoryData.AddRange(
        inventory.GetInventory()
            .Where(kv => kv.Key != RessourceBehaviour.RessourceType.Empty)
        );

        inventoryList.RefreshItems();
    }




    //----------Méthodes d'affichage de l'ui----------
    public void HubUIToggle()
    {
        if (panel.resolvedStyle.display == DisplayStyle.None) panel.style.display = DisplayStyle.Flex;
        else panel.style.display = DisplayStyle.None;
    }

    public void HubUIOn()
    {
        panel.style.display = DisplayStyle.Flex;
        player.isInUI = true;
    }

    public void HubUIOff()
    {
        panel.style.display = DisplayStyle.None; 
        player.isInUI = false;
    }
}
