using System;
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
            player.electricityStorage = Mathf.Clamp(player.electricityStorage, 0f, player.electricityMaxStorage);
            overlaySC.UpdateElectricityStorageText();

            timer -= timerInterval;
        }
    }


    private void BalanceCalculation()//A revoir très vite
    {
        float totalDemand = 0f;
        float totalProduction = 0f;
        float actualConsumption = 0f;

        foreach (var consomer in consomationBuilding)
        {
            totalDemand += consomer.electricityConsomation;
        }

        foreach (var producter in productionBuilding)
        {
            totalProduction += producter.electricityProduction;
        }

        player.electricityStorage += totalProduction;
        float availableElectricity = player.electricityStorage;

        foreach (var consumer in consomationBuilding)
        {
            float consumerAmount = consumer.electricityConsomation;

            if (availableElectricity >= consumerAmount)
            {
                consumer.enoughtElectricity = true;
                availableElectricity -= consumerAmount;
                actualConsumption += consumerAmount;
            }
            else
            {   
                consumer.electricityConsomation = 0f;
                consumer.enoughtElectricity = false;
                
            } 
        }

        player.electricityStorage = availableElectricity;
        player.electricityBalance = totalProduction - totalDemand;
        player.realElectricityBalance = totalDemand - actualConsumption;

        if (Mathf.Abs(player.electricityBalance - lastBalance) > 0.01f)
        {
            overlaySC.UpdateElectricityBalanceText();
        }
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
        productionBuilding.Remove(producer);
    }

    public void RemoveConsomer(BuildingBH consomer)
    {
        consomationBuilding.Remove(consomer);
    }

}
