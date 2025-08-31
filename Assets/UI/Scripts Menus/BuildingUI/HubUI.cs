using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.Collections;
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

    private Button sortButton;

    public enum SortMode
    {
        Default,
        Amount,
        AmountDescending,
        Alphabetical,
        AlphabeticalDescending
    }

    public SortMode currentSortMode = SortMode.Default;
    private SortMode[] sortModes = new SortMode[]
    {   
        SortMode.Default,
        SortMode.Amount,
        SortMode.AmountDescending,
        SortMode.Alphabetical,
        SortMode.AlphabeticalDescending
    };
    private int sortModeIndx = 0;

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

        sortButton = uI.rootVisualElement.Q<Button>("SortButton");

        sortButton.clicked -= SwitchSorMode;
        sortButton.clicked += SwitchSorMode;

        inventoryData = new List<KeyValuePair<RessourceBehaviour.RessourceType, int>>(inventory.GetInventory()
            .Where(kv => kv.Key != RessourceBehaviour.RessourceType.Empty)
            .Where(kv => kv.Value > 0)
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
    public void RefreshInventoryUI(SortMode sortMode)
    {
        switch (sortMode)
        {
            case SortMode.Default:
                SortDefault();
                break;
            case SortMode.Amount:
                SortByAmount();
                break;
            case SortMode.AmountDescending:
                SortByAmountDesending();
                break;
            case SortMode.Alphabetical:
                SortAlphabetical();
                break;
            case SortMode.AlphabeticalDescending:
                SortAlphabeticalDescending();
                break;
        }
    }

    private void SwitchSorMode()
    {
        sortModeIndx = (sortModeIndx + 1) % sortModes.Length;
        currentSortMode = sortModes[sortModeIndx];
        RefreshInventoryUI(currentSortMode);
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

    private void SortByAmount()
    {
        sortButton.style.backgroundImage = new StyleBackground(TextureHolder.instance.sortNum);
        inventoryData.Clear();
        inventoryData.AddRange(inventory.GetInventory()
            .Where(Kv => Kv.Key != RessourceBehaviour.RessourceType.Empty && Kv.Value > 0)
            .OrderByDescending(Kv => Kv.Value)
        );
        inventoryList.RefreshItems();
    }

    private void SortByAmountDesending()
    {   
        sortButton.style.backgroundImage = new StyleBackground(TextureHolder.instance.sortNumDescending);
        inventoryData.Clear();
        inventoryData.AddRange(inventory.GetInventory()
            .Where(Kv => Kv.Key != RessourceBehaviour.RessourceType.Empty && Kv.Value > 0)
            .OrderBy(Kv => Kv.Value)
        );
        inventoryList.RefreshItems();
    }

    private void SortDefault()
    {   
        sortButton.style.backgroundImage = new StyleBackground(TextureHolder.instance.defaultSort);
        inventoryData.Clear();
        inventoryData.AddRange(
        inventory.GetInventory()
            .Where(kv => kv.Key != RessourceBehaviour.RessourceType.Empty && kv.Value > 0)
        );

        inventoryList.RefreshItems();
    }

    private void SortAlphabetical()
    {   
        sortButton.style.backgroundImage = new StyleBackground(TextureHolder.instance.sortAlpha);
        inventoryData.Clear();
        inventoryData.AddRange(
        inventory.GetInventory()
            .Where(kv => kv.Key != RessourceBehaviour.RessourceType.Empty && kv.Value > 0)
            .OrderBy(kv => kv.Key.ToString())
        );

        inventoryList.RefreshItems();
    }

    private void SortAlphabeticalDescending()
    {   
        sortButton.style.backgroundImage = new StyleBackground(TextureHolder.instance.sortAlphaDescending);
        inventoryData.Clear();
        inventoryData.AddRange(
        inventory.GetInventory()
            .Where(kv => kv.Key != RessourceBehaviour.RessourceType.Empty && kv.Value > 0)
            .OrderByDescending(kv => kv.Key.ToString())
        );

        inventoryList.RefreshItems();
    }
}
