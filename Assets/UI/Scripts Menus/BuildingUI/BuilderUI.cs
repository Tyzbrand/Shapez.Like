using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BuilderUI : MonoBehaviour
{
    private UIDocument uI;
    public VisualElement panel;
    public VisualElement recipePanel;

    private UIManager uIManager;
    private PlayerVariables player;
    private RessourceDictionnary ressourceLibrary;

    private ProgressBar process;
    private Label productionTimeText, storageText, inputStorage1Text;
    private Button recipeChangerBtn, recipeListQuit, recipeUse0, recipeUse1, recipeUse2, recipeUse3;
    private Toggle builderToggle;

    private Builder activeBuilder = null;


    private void Start()
    {
        uI = ReferenceHolder.instance.uIDocument;
        uIManager = ReferenceHolder.instance.uIManager;
        player = ReferenceHolder.instance.playervariable;
        ressourceLibrary = ReferenceHolder.instance.ressourceDictionnary;

        panel = uI.rootVisualElement.Q<VisualElement>("BuilderUI");
        uIManager.RegisterPanel(panel);

        recipePanel = panel.Q<VisualElement>("BuilderRecipeList");

        recipeChangerBtn = panel.Q<Button>("BURecipeChangerBtn");
        recipeListQuit = recipePanel.Q<Button>("QuitBtn");
        recipeUse0 = recipePanel.Q<Button>("Recipe0Use");
        recipeUse1 = recipePanel.Q<Button>("Recipe1Use");
        recipeUse2 = recipePanel.Q<Button>("Recipe2Use");
        recipeUse3 = recipePanel.Q<Button>("Recipe3Use");


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

        recipeChangerBtn.clicked += OpenRecipeList;
        recipeListQuit.clicked += CloseRecipeList;
        recipeUse0.clicked += () => ChangeRecipe(1);
        recipeUse1.clicked += () => ChangeRecipe(3);
        recipeUse2.clicked += () => ChangeRecipe(0);
        recipeUse3.clicked += () => ChangeRecipe(2);




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

    public void refreshUI(Builder builder)
    {
        activeBuilder = builder;
        builderToggle.SetValueWithoutNotify(activeBuilder.IsActive);
        ChangeButtonTexture(recipeChangerBtn, 75, 55);
    }



    public void BuilderUIOnShow(Builder builder)
    {
        activeBuilder = builder;
        player.isInUI = true;
        ChangeButtonTexture(recipeChangerBtn, 75, 55);
        builderToggle.SetValueWithoutNotify(activeBuilder.IsActive);
    }

    public void BuilderUIOnHide()
    {
        activeBuilder = null;
        player.isInUI = false;
        CloseRecipeList();
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

        ChangeButtonTexture(recipeChangerBtn, 75, 55);

    }

    private void ChangeButtonTexture(Button button, int width, int height)
    {
        button.style.backgroundImage = new StyleBackground(ressourceLibrary.GetIcon(activeBuilder.currentRecipe.Output));
        button.style.backgroundSize = new BackgroundSize(new Length(width, LengthUnit.Percent), new Length(height, LengthUnit.Percent));
    }
}   


