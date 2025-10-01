using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableFuseBox : MonoBehaviour, Interactable
{
    public string interactionText_RemoveFuse; //Text Shown when player can remove a fuse
    public string interactionText_AddFuse; //Text Show when player can add a fuse
    private string interactionText_Show; //Do not Touch

    public int fusesNeeded = 2; //Starting Fuses Needed
    public List<GameObject> lights = new List<GameObject>(); //List of all Lights depended on the Fuse Box
    public GameObject[] fuseModels = new GameObject[2]; //Do not Touch

    private PlayerInventoryScript playerInventory; //PLayer inventory
    private UserInterfaceManager userInterfaceManager; //PLayer UI

    private InteractableFuseBoxDoor fuseBoxDoor; //FuseBoxe's Door

    public AudioSource soundOn;
    public AudioSource soundOff;

    private bool canInteract = true; //Debounce for player interactions

    void Start()
    {
        userInterfaceManager = FindAnyObjectByType<UserInterfaceManager>();
        fuseBoxDoor = GetComponentInChildren<InteractableFuseBoxDoor>();
        playerInventory = FindAnyObjectByType<PlayerInventoryScript>();
        fusesNeeded = Mathf.Clamp(fusesNeeded, 0, 2); //Clamp fuses needed

        //Update Settings Based on Scene view Input
        UpdateFuseBoxUI();
        UpdateFuseboxModel();
        ManagePowerSource();
    }

    public void Interact()
    {
        if (fuseBoxDoor.isOpen)
        {
            HandleInteraction();
        }
        else
        {
            userInterfaceManager.ShowMessage("Open the Box");
        }
    }

    void HandleInteraction()
    {
        switch (fusesNeeded)
        {
            case 0:
                RemoveFuse();
                break;
            case 1:
                if (playerInventory.CheckItemHolding("Fuse"))
                {
                    AddFuse();
                    break;
                }
                else
                {
                    RemoveFuse();
                    break;
                }
            case 2:
                if (playerInventory.CheckItemHolding("Fuse"))
                {
                    AddFuse();
                    break;
                }
                else
                {
                    userInterfaceManager.ShowMessage("You need a Fuse");
                    break;
                }
        }
    }

    void AddFuse()
    {
        if (canInteract)
        {
            Debug.Log("Added Fuse");
            fusesNeeded -= 1;
            playerInventory.RemoveItemHolding(false);
            UpdateFuseBoxUI();
            UpdateFuseboxModel();
            ManagePowerSource();
            StartCoroutine(InteractionCooldown());
        }
    }

    void RemoveFuse()
    {
        if (canInteract)
        {
            Debug.Log("Removed Fuse");
            fusesNeeded += 1;
            playerInventory.GiveItemByName("Fuse");
            UpdateFuseBoxUI();
            UpdateFuseboxModel();
            ManagePowerSource();
            StartCoroutine(InteractionCooldown());
        }
    }

    void ManagePowerSource()
    {
        if (fusesNeeded == 0)
        {
            foreach (GameObject light in lights)
            {
                InteractableLightSwitch lightSwitch = light.GetComponentInChildren<InteractableLightSwitch>();
                lightSwitch.SetPower(true);
                soundOn.Play();
            }
        }
        else
        {
            foreach (GameObject light in lights)
            {
                InteractableLightSwitch lightSwitch = light.GetComponentInChildren<InteractableLightSwitch>();
                lightSwitch.SetPower(false);
                soundOff.Play();
            }
        }
    }

    public void UpdateFuseBoxUI()
    {
        switch (fusesNeeded)
        {
            case 0:
                interactionText_Show = interactionText_RemoveFuse;
                break;
            default:
                interactionText_Show = interactionText_AddFuse;
                break;
        }
    }

    void UpdateFuseboxModel()
    {

        switch (fusesNeeded)
        {
            case 0:
                fuseModels[0].SetActive(true);
                fuseModels[1].SetActive(true);
                break;
            case 1:
                fuseModels[0].SetActive(true);
                fuseModels[1].SetActive(false);
                break;
            case 2:
                fuseModels[0].SetActive(false);
                fuseModels[1].SetActive(false);
                break;
        }
        if (!fuseBoxDoor.isOpen) interactionText_Show = "";
    }

    public string InteractionText()
    {
        if (fuseBoxDoor.isOpen)
        {
            return interactionText_Show;
        }
        else
        {
            return "";
        }
    }

    public void CutPowerOff(int fusesToRemove)
    {
        fusesNeeded += fusesToRemove;
        UpdateFuseboxModel();
        UpdateFuseBoxUI();
        ManagePowerSource();
    }

    IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(1);
        canInteract = true;
    }
}
