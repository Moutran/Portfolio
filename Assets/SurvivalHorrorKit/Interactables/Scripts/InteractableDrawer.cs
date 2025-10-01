using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDrawer : MonoBehaviour, Interactable
{
    private Animator animator; //Door's Animator
    //private AudioSource sound; //Door's Audio Source
    private bool isOpen = false; //Check if door is Open
    private bool canInteract = true; //Check if door on Cooldown
    public AudioSource soundOpen;
    public AudioSource soundClose;

    public bool canOpen; //Check if door is Interactable
    private string interactionText_Show; //Interaction Text Shown to the player based on Drawer's state
    public string interactionText_Open; //Open Drawe Text
    public string interactionText_Close; //Close Drawer Text

    void Start()
    {
        animator = GetComponent<Animator>();
        //sound = GetComponent<AudioSource>();
        SetInteractionText();
    }

    public void Interact()
    {
        if ((canOpen) && (canInteract))
        {
            OpenDrawer();
        }
    }

    void OpenDrawer()
    {
        if (!isOpen)
        {
            animator.SetBool("DrawerInteraction", true);
            //sound.Play();
            canInteract = false;
            isOpen = true;
            interactionText_Show = interactionText_Close;
            soundOpen.Play();
        }
        else
        {
            animator.SetBool("DrawerInteraction", false);
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
    void SetInteractionText()
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

    public string InteractionText()
    {
        return interactionText_Show;
    }
}
