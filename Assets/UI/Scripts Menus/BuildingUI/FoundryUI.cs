using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FoundryUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;

    private UIManager uIManager;
    private PlayerVariables player;

    private ProgressBar process;
    private Label productionTimeText, storageText, inputStorage1Text, inputStorage2Text, currentRecipeText;
    private Button recipeChangerBtn;
    private Toggle foundryToggle;

    private Foundry activeFoundry = null;


    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;

        panel = uI.rootVisualElement.Q<VisualElement>("FoundryUI");
        uIManager.RegisterPanel(panel);

        recipeChangerBtn = panel.Q<Button>("FORecipeChangerBtn");

        process = panel.Q<ProgressBar>("FOProgressBar");

        storageText = panel.Q<Label>("FOStorageTxt");
        productionTimeText = panel.Q<Label>("FOProductionSpeedTxt");
        inputStorage1Text = panel.Q<Label>("FOCurrentIn1Txt");
        inputStorage2Text = panel.Q<Label>("FOCurrentIn2Txt");
        currentRecipeText = panel.Q<Label>("FOCurrentRecipeTxt");

        foundryToggle = panel.Q<Toggle>("FOToggle");
        foundryToggle.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
            {
                ActiveBuilding();
            }
            else DisableBuilding();
        });

        recipeChangerBtn.clicked += ChangeRecipe;
     

    }

    private void DisableBuilding()
    {
        activeFoundry.IsActive = false;
    }

    private void ActiveBuilding()
    {
        activeFoundry.IsActive = true;
    }

    private void Update()
    {
        if (activeFoundry != null) UpdateUI();
    }

    private void UpdateUI()
    {
        storageText.text = activeFoundry.currentStorageOutput + "/" + activeFoundry.capacity;
        productionTimeText.text = (1 / activeFoundry.currentRecipe.craftSpeed).ToString("0.0") + "/s";
        inputStorage1Text.text = activeFoundry.currentStorageInput1 + "/1";
        inputStorage2Text.text = activeFoundry.currentStorageInput2 + "/1";
        

        process.highValue = activeFoundry.currentRecipe.craftSpeed;
        process.value = activeFoundry.processTimer % process.highValue;
    }

    public void refreshUI(Foundry foundry)
    {
        activeFoundry = foundry;
        foundryToggle.SetValueWithoutNotify(activeFoundry.IsActive);
        currentRecipeText.text = activeFoundry.currentRecipe.input1 + " + " + activeFoundry.currentRecipe.input2 + " => " + activeFoundry.currentRecipe.output;
    }









    public void FoundryUIOnShow(Foundry foundry)
    {
        activeFoundry = foundry;
        currentRecipeText.text = activeFoundry.currentRecipe.input1 + " + " + activeFoundry.currentRecipe.input2 + " => " + activeFoundry.currentRecipe.output;
        player.isInUI = true;
        foundryToggle.SetValueWithoutNotify(activeFoundry.IsActive);
    }

    public void FoundryUIOnHide()
    {
        activeFoundry = null;
        player.isInUI = false;
    }

    private void ChangeRecipe()
    {
        activeFoundry.currentRecipeIndex = (activeFoundry.currentRecipeIndex + 1) % activeFoundry.recipe.foundryRecipes.Count;
        activeFoundry.currentRecipe = activeFoundry.recipe.foundryRecipes[activeFoundry.currentRecipeIndex];
        activeFoundry.currentStorageInput2 = 0;
        activeFoundry.currentStorageOutput = 0;
        activeFoundry.processTimer = 0f;
        activeFoundry.isProcessing = false;

        currentRecipeText.text = activeFoundry.currentRecipe.input1 + " + " + activeFoundry.currentRecipe.input2 + " => " + activeFoundry.currentRecipe.output; 
    }
}
