using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    private PlayerInventoryScript inventory;
    private UserInterfaceManager userInterfaceManager;

    [Header("Flashlight Player Item")]
    private FlashlightScript flashlight;
    void Awake()
    {
        userInterfaceManager = FindAnyObjectByType<UserInterfaceManager>();
        inventory = FindAnyObjectByType<PlayerInventoryScript>();
        flashlight = inventory.GetPlayerItemGameObject("Flashlight").GetComponent<FlashlightScript>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseBattery();
        }
    }

    void UseBattery()
    {
        if (inventory != null)
        {
            bool searchForFlashlight = inventory.SearchInventory("Flashlight");
            if (searchForFlashlight)
            {
                AddBatteryInFlashlight();
            }
            else
            {
                userInterfaceManager.ShowMessage("You have no Flashlight");
            }
        }
    }

    void AddBatteryInFlashlight()
    {
        if (flashlight.battery < 100)
        {
            flashlight.battery = 100f;
            inventory.RemoveItemHolding(true);
        }
        else
        {
            userInterfaceManager.ShowMessage("Battery is Full");
        }
    }
}
