using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FoundryUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;
    public VisualElement recipepanel;

    private UIManager uIManager;
    private PlayerVariables player;
    private RessourceDictionnary ressourceLibrary;

    private ProgressBar process;
    private Label productionTimeText, storageText, inputStorage1Text, inputStorage2Text;
    private Button recipeChangerBtn, recipeListQuit, recipeUse0, recipeUse1;
    private Toggle foundryToggle;

    private Foundry activeFoundry = null;


    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;
        ressourceLibrary = ReferenceHolder.instance.ressourceDictionnary;

        panel = uI.rootVisualElement.Q<VisualElement>("FoundryUI");
        uIManager.RegisterPanel(panel);

        recipepanel = panel.Q<VisualElement>("FoundryRecipeList");

        recipeChangerBtn = panel.Q<Button>("FORecipeChangerBtn");
        recipeListQuit = recipepanel.Q<Button>("QuitBtn");
        recipeUse0 = recipepanel.Q<Button>("Recipe0Use");
        recipeUse1 = recipepanel.Q<Button>("Recipe1Use");


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
        recipeListQuit.clicked -= CloseRecipeList;

        recipeChangerBtn.clicked += OpenRecipeList;
        recipeUse0.clicked += () => ChooseRecipe(0);
        recipeUse1.clicked += () => ChooseRecipe(1);
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

    public void refreshUI(Foundry foundry)
    {
        activeFoundry = foundry;
        foundryToggle.SetValueWithoutNotify(activeFoundry.IsActive);

        ChangeButtonTexture(recipeChangerBtn, 75, 55);

    }

    public void FoundryUIOnShow(Foundry foundry)
    {
        activeFoundry = foundry;
        player.isInUI = true;
        ChangeButtonTexture(recipeChangerBtn, 75, 55);
        foundryToggle.SetValueWithoutNotify(activeFoundry.IsActive);
    }

    public void FoundryUIOnHide()
    {
        activeFoundry = null;
        player.isInUI = false;
        CloseRecipeList();
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

        ChangeButtonTexture(recipeChangerBtn, 75, 55);

        if (!activeFoundry.isProcessing) activeFoundry.SetIdleTexture();
    }

    private void ChangeButtonTexture(Button button, int width, int height)
    {
        button.style.backgroundImage = new StyleBackground(ressourceLibrary.GetIcon(activeFoundry.currentRecipe.output));
        button.style.backgroundSize = new BackgroundSize(new Length(width, LengthUnit.Percent), new Length(height, LengthUnit.Percent));
    }
}
