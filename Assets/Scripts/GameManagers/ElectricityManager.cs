using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    private PlayerVariables player;
    private OverlaySC overlaySC;

    private float timer = 0f;
    private float timerInterval = 1f;
    private float lastBalance = 0f;
    private List<BuildingBH> productionBuilding = new();
    private List<BuildingBH> consomationBuilding = new();

    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        overlaySC = ReferenceHolder.instance.inGameOverlay;

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerInterval)
        {
            BalanceCalculation();
            if(player.electricityStorage < player.electricityMaxStorage) player.electricityStorage += player.electricityBalance;
            if (player.electricityStorage < 0) player.electricityStorage = 0f;
            if (player.electricityBalance != lastBalance) overlaySC.UpdateElectricityStorageText();

            timer -= timerInterval;
        }

        
    }


    private void BalanceCalculation()
    {
        float production = 0f;
        float consomation = 0f;
        foreach (var producer in productionBuilding)
        {
            production += producer.electricityProduction;
        }
        foreach (var consomer in consomationBuilding)
        {
            consomation += consomer.electricityConsomation;
        }

        player.electricityBalance = production - consomation;
        lastBalance = player.electricityBalance;
    }

    public void RegisterProducter(BuildingBH producer)
    {
        if (!productionBuilding.Contains(producer)) productionBuilding.Add(producer);
    }

    public void RegisterConsomer(BuildingBH consomer)
    {
        if (!consomationBuilding.Contains(consomer)) consomationBuilding.Add(consomer);
    }

    public void RemoveProducter(BuildingBH producer)
    {
        if (productionBuilding.Contains(producer)) productionBuilding.Remove(producer);
    }

    public void RemoveConsomer(BuildingBH consomer)
    {
        if (consomationBuilding.Contains(consomer)) consomationBuilding.Remove(consomer);
    }

    public void UpdateOverlay()
    {
        overlaySC.UpdateElectricityBalanceText();
    }




}
