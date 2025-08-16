using UnityEngine;
using UnityEngine.UIElements;

public class OverlaySC : MonoBehaviour
{
    [SerializeField] private UIDocument uI;
    private VisualElement panel;

    private PlayerVariables player;
    private PauseScript timeManagerSC;
    private BuildMenuButton buildMenuButtonSC;
    private ItemManager itemManager;
    private Destruction destructionSC;
    private Inventory inventorySC;

    private Label moneyText, storageText, dayText, timeText, objectiveText;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        timeManagerSC = ReferenceHolder.instance.timeManagerSC;
        buildMenuButtonSC = ReferenceHolder.instance.buildMenuButtonSC;
        itemManager = ReferenceHolder.instance.itemManager;
        destructionSC = ReferenceHolder.instance.destructionSC;
        inventorySC = ReferenceHolder.instance.inventorySC;

        panel = uI.rootVisualElement.Q<VisualElement>("InGameOverlay");

        var moneyTxt = panel.Q<Label>("MoneyTxt");
        var storageTxt = panel.Q<Label>("StorageTxt");
        var objectiveTxt = panel.Q<Label>("ObjectiveTxt");
        var dayTxt = panel.Q<Label>("DayTxt");
        var timeTxt = panel.Q<Label>("TimeTxt");


        var buildMenuBtn = panel.Q<Button>("BuildMenuBtn");
        var destructionBtn = panel.Q<Button>("DestructionBtn");
        var itemDestructionBtn = panel.Q<Button>("ItemDestructionBtn");

        var pauseBtn = panel.Q<Button>("PauseBtn");
        var x1Btn = panel.Q<Button>("x1Btn");
        var x2Btn = panel.Q<Button>("x2Btn");
        var x3Btn = panel.Q<Button>("x3Btn");


        //Désabonnement
        buildMenuBtn.clicked -= buildMenuButtonSC.BuildMenuToogle;
        destructionBtn.clicked -= destructionSC.DestructionSet;
        itemDestructionBtn.clicked -= itemManager.ClearItems;

        pauseBtn.clicked -= timeManagerSC.SetPause;
        x1Btn.clicked -= timeManagerSC.SetPlay;
        x2Btn.clicked -= timeManagerSC.SetX2;
        x3Btn.clicked -= timeManagerSC.Setx3;


        //Abonnement
        buildMenuBtn.clicked += buildMenuButtonSC.BuildMenuToogle;
        destructionBtn.clicked += destructionSC.DestructionSet;
        itemDestructionBtn.clicked += itemManager.ClearItems;

        pauseBtn.clicked += timeManagerSC.SetPause;
        x1Btn.clicked += timeManagerSC.SetPlay;
        x2Btn.clicked += timeManagerSC.SetX2;
        x3Btn.clicked += timeManagerSC.Setx3;

        //Assignation des textes
        moneyTxt.text = player.Money + " $";
        storageTxt.text = inventorySC.GetTotalItemCount() + "/" + inventorySC.inventoryCapacity;
        dayTxt.text = "Day " + player.day;
        timeTxt.text = player.minutes.ToString("00") + ":" + player.seconds.ToString("00");
        objectiveTxt.text = "Iron Ingots: " + inventorySC.Get(RessourceBehaviour.RessourceType.IronIngot) + "/100";


        //Assignation des variables de texte
        moneyText = moneyTxt;
        storageText = storageTxt;
        dayText = dayTxt;
        timeText = timeTxt;
        objectiveText = objectiveTxt;

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



}
