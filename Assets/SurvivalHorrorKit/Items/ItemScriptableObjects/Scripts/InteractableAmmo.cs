using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Net;
using Unity.VisualScripting;

public class InteractableAmmo : MonoBehaviour, Interactable
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
        inventory = FindAnyObjectByType<PlayerInventoryScript>();
        userInterfaceManager = FindAnyObjectByType<UserInterfaceManager>();
    }

    public void Interact()
    {
        if (isActive)
        {
            inventory.AddAmmo(itemInfo.name);
            Destroy(gameObject);
        }
    }

    public string InteractionText() //Do not touch
    {
        return interactionText;
    }
}
