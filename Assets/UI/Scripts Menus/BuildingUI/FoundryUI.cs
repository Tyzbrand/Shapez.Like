using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FoundryUI : AbstractBuildingUI
{
    public VisualElement recipepanel;

    private RessourceDictionnary ressourceLibrary;

    private ProgressBar process;
    private Label productionTimeText, storageText, inputStorage1Text, inputStorage2Text;
    private Button recipeChangerBtn, recipeListQuit, recipeUse0, recipeUse1, recipeUse2;
    private Toggle foundryToggle;

    private Foundry activeFoundry = null;


    protected override void Start()
    {
        base.Start();
        
        ressourceLibrary = ReferenceHolder.instance.ressourceDictionnary;

        panel = uI.rootVisualElement.Q<VisualElement>("FoundryUI");
        uIManager.RegisterPanel(panel, this);
        buildingLibrary.RegisterUIPanel(panel, UIManager.uIType.Foundry);

        recipepanel = panel.Q<VisualElement>("FoundryRecipeList");

        recipeChangerBtn = panel.Q<Button>("FORecipeChangerBtn");
        recipeListQuit = recipepanel.Q<Button>("QuitBtn");
        recipeUse0 = recipepanel.Q<Button>("Recipe0Use");
        recipeUse1 = recipepanel.Q<Button>("Recipe1Use");
        recipeUse2 = recipepanel.Q<Button>("Recipe2Use");


        process = panel.Q<ProgressBar>("FOProgressBar");

        storageText = panel.Q<Label>("FOStorageTxt");
        productionTimeText = panel.Q<Label>("FOProductionSpeedTxt");
        inputStorage1Text = panel.Q<Label>("FOCurrentIn1Txt");
        inputStorage2Text = panel.Q<Label>("FOCurrentIn2Txt");

        foundryToggle = panel.Q<Toggle>("FOToggle");
        foundryToggle.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
            {
                ActiveBuilding();
            }
            else DisableBuilding();
        });

        recipeChangerBtn.clicked -= OpenRecipeList;
        recipeUse0.clicked -= () => ChooseRecipe(0);
        recipeUse1.clicked -= () => ChooseRecipe(1);
        recipeUse2.clicked -= () => ChooseRecipe(2);
        recipeListQuit.clicked -= CloseRecipeList;

        recipeChangerBtn.clicked += OpenRecipeList;
        recipeUse0.clicked += () => ChooseRecipe(0);
        recipeUse1.clicked += () => ChooseRecipe(1);
        recipeUse2.clicked += () => ChooseRecipe(2);
        recipeListQuit.clicked += CloseRecipeList;



    }

    private void DisableBuilding()
    {
        activeFoundry.IsActive = false;
        activeFoundry.BuildingOnDisable();
    }

    private void ActiveBuilding()
    {
        activeFoundry.IsActive = true;
        activeFoundry.BuildingOnEnable();
    }

    private void Update()
    {
        if (activeFoundry != null) UpdateUI();
    }

    private void UpdateUI()
    {
        storageText.text = activeFoundry.currentStorageOutput + "/" + activeFoundry.capacity;
        productionTimeText.text = (1 / activeFoundry.currentRecipe.craftSpeed).ToString("0.0") + "/s";
        inputStorage1Text.text = activeFoundry.currentStorageInput1 + "/" + activeFoundry.input1Capacity;
        inputStorage2Text.text = activeFoundry.currentStorageInput2 + "/" + activeFoundry.input2Capacity;


        process.highValue = activeFoundry.currentRecipe.craftSpeed;
        process.value = activeFoundry.processTimer % process.highValue;
    }

    public override void RefreshUI(BuildingBH building)
    {
        if (building is Foundry foundry)
        {
            activeFoundry = foundry;
            foundryToggle.SetValueWithoutNotify(activeFoundry.IsActive);
             ChangeButtonTexture(recipeChangerBtn);
        }
    }

    public override void UIOnShow(BuildingBH building)
    {
        if (building is Foundry foundry)
        {
            activeFoundry = foundry;
            ChangeButtonTexture(recipeChangerBtn);
            foundryToggle.SetValueWithoutNotify(activeFoundry.IsActive);
            player.isInBuildingUI = true;
        }

    }

    public override void UIOnHide()
    {
        activeFoundry = null;
        player.isInBuildingUI = false;
        CloseRecipeList();
        Debug.Log("Foundry");
    }

    private void OpenRecipeList()
    {
        recipepanel.style.display = DisplayStyle.Flex;
    }

    private void CloseRecipeList()
    {
        recipepanel.style.display = DisplayStyle.None;
    }

    private void ChooseRecipe(int indx)
    {
        activeFoundry.currentRecipe = activeFoundry.recipe.foundryRecipes[indx];
        activeFoundry.currentStorageInput2 = 0;
        activeFoundry.currentStorageOutput = 0;
        activeFoundry.processInterval = activeFoundry.currentRecipe.craftSpeed;
        activeFoundry.processTimer = 0f;
        activeFoundry.isProcessing = false;

        ChangeButtonTexture(recipeChangerBtn);

        if (!activeFoundry.isProcessing) activeFoundry.SetIdleTexture();
    }

    private void ChangeButtonTexture(Button button)
    {
        button.style.backgroundImage = new StyleBackground(ressourceLibrary.GetIcon(activeFoundry.currentRecipe.output));
    }
}
