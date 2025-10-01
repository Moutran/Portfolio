using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableFuseBoxDoor : MonoBehaviour, Interactable
{
    [Header("References")]
    private Animator animator;
    private InteractableFuseBox fuseBox;
    public AudioSource soundOpen;
    public AudioSource soundClose;

    [Header("Booleans and Debounces")]
    public bool isOpen = false;
    public bool canOpen = true;
    private bool canInteract = true;

    [Header("UI")]
    private string interactionText_Show;
    public string interactionText_Open;
    public string interactionText_Close;    

    void Start()
    {
        animator = GetComponent<Animator>();
        fuseBox = GetComponentInParent<InteractableFuseBox>();
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
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        fuseBox.UpdateFuseBoxUI();
        if (!isOpen)
        {
            animator.SetBool("FuseInteraction", true);
            //sound.Play();
            canInteract = false;
            isOpen = true;
            interactionText_Show = interactionText_Close;
            soundOpen.Play();
        }
        else
        {
            animator.SetBool("FuseInteraction", false);
            canInteract = false;
            isOpen = false;
            interactionText_Show = interactionText_Open;
            soundClose.Play();
        }
    }
    void SetInteractionDebounce()
    {
        if (!canInteract)
        {
            canInteract = true;
        }
    }

    public string InteractionText()
    {
        return interactionText_Show;
    }
}
