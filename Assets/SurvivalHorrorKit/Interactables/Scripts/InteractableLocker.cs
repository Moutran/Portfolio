using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableLocker : MonoBehaviour, Interactable
{
    [Header("References")]
    private Animator animator;
    private HidingSpot hidingSpot;

    [Header("Booleans and Debounces")]
    private bool isOpen = false;
    private bool canOpen = true;
    private bool canInteract = true;

    [Header("UI")]
    private string interactionText_Show;
    public string interactionText_Open;
    public string interactionText_Close;

    void Start()
    {
        animator = GetComponent<Animator>();
        hidingSpot = transform.parent.GetComponentInChildren<HidingSpot>();
        SetUserInterface();
    }

    void SetUserInterface()
    {
        if (isOpen)
        {
            interactionText_Show = interactionText_Close;
        }
        else
        {
            interactionText_Show = interactionText_Open;
        }
    }

    public void Interact()
    {
        if ((canOpen) && (canInteract))
        {
            LockerDoor();
        }
    }

    void LockerDoor()
    {
        if (!isOpen)
        {
            animator.SetBool("LockerInteraction", true);
            canInteract = false;
            isOpen = true;
            interactionText_Show = interactionText_Close;
            SetHidingSpot(false);
        }
        else
        {
            animator.SetBool("LockerInteraction", false);
            canInteract = false;
            isOpen = false;
            interactionText_Show = interactionText_Open;
            SetHidingSpot(true);
        }
    }
    void SetInteractionDebounce()
    {
        if (!canInteract)
        {
            canInteract = true;
        }
    }

    void SetHidingSpot(bool set)
    {
        if(hidingSpot != null)
        {
            hidingSpot.isActive = set;
        }
    }

    public string InteractionText()
    {
        return interactionText_Show;
    }
}
