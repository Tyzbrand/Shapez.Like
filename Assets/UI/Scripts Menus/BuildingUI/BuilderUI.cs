using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BuilderUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;

    private UIManager uIManager;
    private PlayerVariables player;

    private ProgressBar process;
    private Label productionTimeText, storageText, inputStorage1Text, currentRecipeText;
    private Button recipeChangerBtn;
    private Toggle builderToggle;

    private Builder activeBuilder = null;


    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;

        panel = uI.rootVisualElement.Q<VisualElement>("BuilderUI");
        uIManager.RegisterPanel(panel);

        recipeChangerBtn = panel.Q<Button>("BURecipeChangerBtn");

        process = panel.Q<ProgressBar>("BUProgressBar");

        storageText = panel.Q<Label>("BUStorageTxt");
        productionTimeText = panel.Q<Label>("BUProductionSpeedTxt");
        inputStorage1Text = panel.Q<Label>("BUCurrentIn1Txt");
        currentRecipeText = panel.Q<Label>("BUCurrentRecipeTxt");

        builderToggle = panel.Q<Toggle>("BUToggle");
        builderToggle.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue) ActiveBuilding();
            else DisableBuilding();
            
        });

        recipeChangerBtn.clicked += ChangeRecipe;
     

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
        inputStorage1Text.text = activeBuilder.currentStorageInput + "/1";
        

        process.highValue = activeBuilder.currentRecipe.craftSpeed;
        process.value = activeBuilder.processTimer % process.highValue;
    }

    public void refreshUI(Builder builder)
    {
        activeBuilder = builder;
        builderToggle.SetValueWithoutNotify(activeBuilder.IsActive);
        currentRecipeText.text = activeBuilder.currentRecipe.Input + " => " + activeBuilder.currentRecipe.Output;

    }



    public void BuilderUIOnShow(Builder builder)
    {
        activeBuilder = builder;
        currentRecipeText.text = activeBuilder.currentRecipe.Input + " => " + activeBuilder.currentRecipe.Output;
        player.isInUI = true;
        builderToggle.SetValueWithoutNotify(activeBuilder.IsActive);
    }

    public void BuilderUIOnHide()
    {
        activeBuilder = null;
        player.isInUI = false;
    }

    private void ChangeRecipe()
    {
        activeBuilder.currentRecipeIndex = (activeBuilder.currentRecipeIndex + 1) % activeBuilder.recipe.BuilderRecipes.Count;
        activeBuilder.currentRecipe = activeBuilder.recipe.BuilderRecipes[activeBuilder.currentRecipeIndex];
        activeBuilder.currentStorageInput = 0;
        activeBuilder.currentStorageOutput = 0;
        activeBuilder.processTimer = 0f;
        activeBuilder.isProcessing = false;

        currentRecipeText.text = activeBuilder.currentRecipe.Input + " => " + activeBuilder.currentRecipe.Output; 
    }
}

