using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BuilderUI : AbstractBuildingUI
{
    public VisualElement recipePanel;
    private RessourceDictionnary ressourceLibrary;

    private ProgressBar process;
    private Label productionTimeText, storageText, inputStorage1Text;
    private Button recipeChangerBtn, recipeListQuit, recipeUse0, recipeUse1, recipeUse2, recipeUse3, recipeUse4;
    private Toggle builderToggle;

    private Builder activeBuilder = null;


    protected override void Start()
    {
        base.Start();

        ressourceLibrary = ReferenceHolder.instance.ressourceDictionnary;

        panel = uI.rootVisualElement.Q<VisualElement>("BuilderUI");
        uIManager.RegisterPanel(panel, this);
        buildingLibrary.RegisterUIPanel(panel, UIManager.uIType.Builder);

        recipePanel = panel.Q<VisualElement>("BuilderRecipeList");

        recipeChangerBtn = panel.Q<Button>("BURecipeChangerBtn");
        recipeListQuit = recipePanel.Q<Button>("QuitBtn");
        recipeUse0 = recipePanel.Q<Button>("Recipe0Use");
        recipeUse1 = recipePanel.Q<Button>("Recipe1Use");
        recipeUse2 = recipePanel.Q<Button>("Recipe2Use");
        recipeUse3 = recipePanel.Q<Button>("Recipe3Use");
        recipeUse4 = recipePanel.Q<Button>("Recipe4Use");


        process = panel.Q<ProgressBar>("BUProgressBar");

        storageText = panel.Q<Label>("BUStorageTxt");
        productionTimeText = panel.Q<Label>("BUProductionSpeedTxt");
        inputStorage1Text = panel.Q<Label>("BUCurrentIn1Txt");

        builderToggle = panel.Q<Toggle>("BUToggle");
        builderToggle.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue) ActiveBuilding();
            else DisableBuilding();

        });

        recipeChangerBtn.clicked -= OpenRecipeList;
        recipeListQuit.clicked -= CloseRecipeList;
        recipeUse0.clicked -= () => ChangeRecipe(1);
        recipeUse1.clicked -= () => ChangeRecipe(3);
        recipeUse2.clicked -= () => ChangeRecipe(0);
        recipeUse3.clicked -= () => ChangeRecipe(2);
        recipeUse4.clicked -= () => ChangeRecipe(4);

        recipeChangerBtn.clicked += OpenRecipeList;
        recipeListQuit.clicked += CloseRecipeList;
        recipeUse0.clicked += () => ChangeRecipe(1);
        recipeUse1.clicked += () => ChangeRecipe(3);
        recipeUse2.clicked += () => ChangeRecipe(0);
        recipeUse3.clicked += () => ChangeRecipe(2);
        recipeUse4.clicked += () => ChangeRecipe(4);




    }

    private void DisableBuilding()
    {
        activeBuilder.IsActive = false;
        activeBuilder.BuildingOnDisable();
    }

    private void ActiveBuilding()
    {
        activeBuilder.IsActive = true;
        activeBuilder.BuildingOnEnable();
    }

    private void Update()
    {
        if (activeBuilder != null) UpdateUI();
    }

    private void UpdateUI()
    {
        storageText.text = activeBuilder.currentStorageOutput + "/" + activeBuilder.capacity;
        productionTimeText.text = (1 / activeBuilder.currentRecipe.craftSpeed).ToString("0.0") + "/s";
        inputStorage1Text.text = activeBuilder.currentStorageInput + "/" + activeBuilder.inputCapacity;


        process.highValue = activeBuilder.currentRecipe.craftSpeed;
        process.value = activeBuilder.processTimer % process.highValue;
    }

    public override void RefreshUI(BuildingBH building)
    {
        if (building is Builder builder)
        {
            activeBuilder = builder;
            builderToggle.SetValueWithoutNotify(activeBuilder.IsActive);
            ChangeButtonTexture(recipeChangerBtn);
        }

    }



    public override void UIOnShow(BuildingBH building)
    {   
        if (building is Builder builder)
        {
            activeBuilder = builder;
            ChangeButtonTexture(recipeChangerBtn);
            builderToggle.SetValueWithoutNotify(activeBuilder.IsActive);
            player.isInBuildingUI = true;
        }

    }

    public override void UIOnHide()
    {
        activeBuilder = null;
        player.isInBuildingUI = false;
        CloseRecipeList();
        Debug.Log("builder");
    }

    private void CloseRecipeList()
    {
        recipePanel.style.display = DisplayStyle.None;
    }

    private void OpenRecipeList()
    {
        recipePanel.style.display = DisplayStyle.Flex;
    }

    private void ChangeRecipe(int indx)
    {
        activeBuilder.currentRecipe = activeBuilder.recipe.BuilderRecipes[indx];
        activeBuilder.currentStorageInput = 0;
        activeBuilder.currentStorageOutput = 0;
        activeBuilder.processInterval = activeBuilder.currentRecipe.craftSpeed;
        activeBuilder.processTimer = 0f;
        activeBuilder.isProcessing = false;

        ChangeButtonTexture(recipeChangerBtn);

    }

    private void ChangeButtonTexture(Button button)
    {   
        button.style.backgroundImage = new StyleBackground(ressourceLibrary.GetIcon(activeBuilder.currentRecipe.Output));
    }
}   


