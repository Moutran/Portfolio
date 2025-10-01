using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Net;
using Unity.VisualScripting;

public class InteractableItem : MonoBehaviour, Interactable
{
    [Header("Item Settings")]
    public Item itemInfo; //Item's Scriptable Object
    public bool isActive = true; //ON / OFF Item iteraction
    public string interactionText; //Text shown when interacting

    [Header("References")]
    private PlayerInventoryScript inventory; //Player Inventory Script
    private UserInterfaceManager userInterfaceManager; //Player UI

    void Awake()
    {
        inventory = PlayerInventoryScript.instance;
        userInterfaceManager = FindAnyObjectByType<UserInterfaceManager>();
    }

    public void Interact() 
    {
        if (isActive)
        {
            bool search = inventory.SearchInventory(itemInfo.name);
            if (search && !itemInfo.isStackable)
            {
                userInterfaceManager.ShowMessage("You may only hold one of this item");
            }
            else
            {
                inventory.PickUpItem(itemInfo, gameObject);
            }
        }
    }

    public string InteractionText() //Do not touch
    {
        return interactionText;
    }
}
