using UnityEngine;
using UnityEngine.UIElements;

public class OverlaySC : MonoBehaviour
{
    private UIDocument uI;
    private VisualElement panel;

    private PlayerVariables player;
    private TimeManager timeManager;
    private BuildMenuSC buildMenuSC;
    private ItemManager itemManager;
    private Destruction destructionSC;
    private Inventory inventorySC;

    private Label moneyText, storageText, dayText, timeText, objectiveText;
    private Button buildMenuBtn, destructionBtn, itemDestructionBtn, pauseBtn, x1Btn, x2Btn, x3Btn;



    private void Awake()
    {

        player = ReferenceHolder.instance.playervariable;
        timeManager = ReferenceHolder.instance.timeManager;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
        itemManager = ReferenceHolder.instance.itemManager;
        destructionSC = ReferenceHolder.instance.destructionSC;
        inventorySC = ReferenceHolder.instance.inventorySC;
        uI = ReferenceHolder.instance.uIDocument;

        panel = uI.rootVisualElement.Q<VisualElement>("InGameOverlay");

        moneyText = panel.Q<Label>("MoneyTxt");
        storageText = panel.Q<Label>("StorageTxt");
        objectiveText = panel.Q<Label>("ObjectiveTxt");
        dayText = panel.Q<Label>("DayTxt");
        timeText = panel.Q<Label>("TimeTxt");


        buildMenuBtn = panel.Q<Button>("BuildMenuBtn");
        destructionBtn = panel.Q<Button>("DestructionBtn");
        itemDestructionBtn = panel.Q<Button>("ItemDestructionBtn");

        pauseBtn = panel.Q<Button>("PauseBtn");
        x1Btn = panel.Q<Button>("x1Btn");
        x2Btn = panel.Q<Button>("x2Btn");
        x3Btn = panel.Q<Button>("x3Btn");

        //Désabonnement
        buildMenuBtn.clicked -= buildMenuSC.BuildMenuToggle;
        destructionBtn.clicked -= destructionSC.DestructionSet;
        itemDestructionBtn.clicked -= itemManager.ClearItems;

        pauseBtn.clicked -= SetPauseBtn;
        x1Btn.clicked -= timeManager.SetPlay;
        x2Btn.clicked -= timeManager.SetX2;
        x3Btn.clicked -= timeManager.Setx3;


        //Abonnement
        buildMenuBtn.clicked += buildMenuSC.BuildMenuOn;
        destructionBtn.clicked += destructionSC.DestructionSet;
        itemDestructionBtn.clicked += itemManager.ClearItems;

        pauseBtn.clicked += SetPauseBtn;
        x1Btn.clicked += timeManager.SetPlay;
        x2Btn.clicked += timeManager.SetX2;
        x3Btn.clicked += timeManager.Setx3;

        //Assignation des textes
        moneyText.text = player.Money + " $";
        storageText.text = inventorySC.GetTotalItemCount() + "/" + inventorySC.inventoryCapacity;
        dayText.text = "Day " + player.day;
        timeText.text = player.minutes.ToString("00") + ":" + player.seconds.ToString("00");
        objectiveText.text = "Iron Ingots: " + inventorySC.Get(RessourceBehaviour.RessourceType.IronIngot) + "/100";



    }


    //----------Méthodes de mise a jour du texte----------

    public void UpdateMoneyText()
    {
        moneyText.text = player.Money + " $";
    }

    public void UpdateStorageText()
    {
        storageText.text = inventorySC.GetTotalItemCount() + "/" + inventorySC.inventoryCapacity;
    }

    public void UpdateDayText()
    {
        dayText.text = "Day " + player.day;
    }

    public void UpdateTimeText()
    {
        timeText.text = player.minutes.ToString("00") + ":" + player.seconds.ToString("00");
    }

    public void UpdateObjectiveText()
    {
        objectiveText.text = "Iron Ingots: " + inventorySC.Get(RessourceBehaviour.RessourceType.IronIngot) + "/100";
    }


    //----------Méthodes utilitaires----------
    public void SetPauseBtn()
    {
        timeManager.SetPause(true);
    }



}
