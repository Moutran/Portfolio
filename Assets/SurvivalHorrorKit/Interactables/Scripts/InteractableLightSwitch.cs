using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableLightSwitch : MonoBehaviour, Interactable
{
    public Light lamp;

    public bool isOpen = false;
    public bool hasPower = true;

    private Animator animator;

    private UserInterfaceManager userInterfaceManager;

    public string interactionText_Open;
    public string interactionText_Close;
    public string interactionText_NoPower;
    private string interactionText_Show;

    void Awake()
    {
        animator = GetComponent<Animator>();
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();
    }

    void Start()
    {
        SetLightOnSceneStart();
    }

    public void Interact()
    {
        if (hasPower)
        {
            LightSwitch(isOpen);
        }
        else
        {
            userInterfaceManager.ShowMessage("No Power");
            interactionText_Show = interactionText_NoPower;
        }
    }

    void LightSwitch(bool debounce)
    {
        switch (debounce)
        {   
            case true:
                lamp.enabled = false;
                animator.SetBool("LightSwitchInteraction", false);
                isOpen = false;
                interactionText_Show = interactionText_Open;
                break;
            case false:
                lamp.enabled = true;
                animator.SetBool("LightSwitchInteraction", true);
                isOpen = true;
                interactionText_Show = interactionText_Close;
                break;
        }
    }

    void SetLightOnSceneStart()
    {
        if (hasPower) isOpen = true; else isOpen = false; interactionText_Show = interactionText_NoPower; 

        if (isOpen)
        {
            lamp.enabled = true;
            animator.SetBool("LightSwitchInteraction", true);
            interactionText_Show = interactionText_Close;
        }
        else
        {
            lamp.enabled = false;
            animator.SetBool("LightSwitchInteraction", false);
            interactionText_Show = interactionText_Open;
        }
    }

    public void SetPower(bool power)
    {
        switch (power) 
        { 
            case false:
                hasPower = false;
                LightSwitch(true);
                break;
            case true:
                hasPower = true;
                LightSwitch(true);
                break;
        }
    }

    public string InteractionText()
    {
        return interactionText_Show;
    }
}
