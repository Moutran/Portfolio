using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableFuelPump : MonoBehaviour, Interactable
{
    private bool canInteract = true;
    public float fuel = 0.5f;
    public float fuelPerPump = 1f;
    public float fuelRegeneration = 0.01f;
    private string interactionText;

    private PlayerInventoryScript inventory;
    private UserInterfaceManager userInterfaceManager;

    void Start()
    {
        inventory = FindObjectOfType<PlayerInventoryScript>();
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();
    }

    void Update()
    {
        fuel += fuelRegeneration * Time.deltaTime;
        SetInteractionText();
    }

    public void Interact()
    {
        if (inventory != null && userInterfaceManager != null && canInteract)
        {
            if (inventory.CheckItemHolding("FuelCan"))
            {
                FuelCanScript fuelCan = inventory.GetItemHolding().GetComponent<FuelCanScript>();
                if (fuel < fuelPerPump)
                {
                    userInterfaceManager.ShowMessage("Not enough fuel to pump gas");
                }
                else
                {
                    if (fuelCan.CanTakeFuel())
                    {
                        fuel -= fuelPerPump;
                        fuelCan.AddFuel(fuelPerPump);
                        StartCoroutine(SetInteractionDebounce());
                        //play pump sound
                    }
                }
            }
        }
    }

    IEnumerator SetInteractionDebounce()
    {
        canInteract = false;
        yield return new WaitForSeconds(1.5f);
        canInteract = true;
    }

    void SetInteractionText()
    {
        interactionText = "Fuel Available: " + (Mathf.Round(fuel * 100) / 100) + "lt";
    }

    public string InteractionText()
    {
        return interactionText;
    }
}
