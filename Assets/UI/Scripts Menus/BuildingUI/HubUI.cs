using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HubUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;
    private ListView inventoryList;
    private Inventory inventory;
    private PlayerVariables player;
    private UIManager uIManager;

    private List<KeyValuePair<RessourceBehaviour.RessourceType, int>> inventoryData;

    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        inventory = ReferenceHolder.instance.inventorySC;
        player = ReferenceHolder.instance.playervariable;
        uIManager = ReferenceHolder.instance.uIManager;

        panel = uI.rootVisualElement.Q<VisualElement>("InventoryUI");
        uIManager.RegisterPanel(panel);
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
    public void HubUIOnShow()
    {
        player.isInUI = true;
    }

    public void HubUIOnHide()
    {
        player.isInUI = false;
    }
}
