using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{



    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI Stockage;
    private PlayerVariables player;
    private Inventory inventory;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        inventory = ReferenceHolder.instance.inventorySC;
    }


    private void Update()
    {
        moneyText.text = player.Money.ToString() + " $";
        timeText.text = player.minutes.ToString("00") + ":" + player.seconds.ToString("00");
        dayText.text = "Day " + player.day.ToString();
        objective1.text = "Iron Ingots: " + inventory.Get(RessourceBehaviour.RessourceType.IronIngot) + "/100";
        Stockage.text = inventory.GetTotalItemCount() + "/" + inventory.inventoryCapacity;

    }


}
