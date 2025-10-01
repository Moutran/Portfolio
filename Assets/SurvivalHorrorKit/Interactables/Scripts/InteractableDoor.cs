using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, Interactable
{
    [Header("References")]
    private Animator animator; //Door's Animator
    public AudioSource soundOpen;
    public AudioSource soundClose;
    private PlayerInventoryScript playerInventoryScript; //Player's Inventory
    private Transform player; //Find player's position
    private UserInterfaceManager userInterfaceManager; //Player's UI Manager

    [Header("Booleans and Debounces")]
    private bool isOpen = false; //Check if door is Open
    private bool canInteract = true; //Check if door on Cooldown
    public bool isLocked; //Check if door is Locked
    public bool canOpen; //Check if door is Interactable

    [Header("UI")]
    private string interactionText_Show; //Interaction Text Shown to the player based on Door's state
    public string interactionText_Open; //Open Door Text
    public string interactionText_Close; //Close Door Text

    void Start()
    {
        animator = GetComponent<Animator>();
        player = (GameObject.FindWithTag("Player")).transform; //Get player position
        playerInventoryScript = player.GetComponent<PlayerInventoryScript>();
        userInterfaceManager = player.GetComponent<UserInterfaceManager>();

        SetInteractionText(); //Set interaction Text on scene start
    }

    public void Interact() //Function called when interacting with the object
    {
        if ((canOpen) && (canInteract)) //Check if the door is interactable and not on Cooldown
        {
            if (isLocked) //Check if the door is Locked
            {
                UnlockDoor(playerInventoryScript.CheckItemHolding("Key"));
            }
            else
            {
                DoorInteraction(CalculateDotProduct()); //Open or Close door
            }
        }
        else
        {
            Debug.Log("Door can't open or on cooldown"); 
        }
    }

    bool CalculateDotProduct() //Find which Direction the player Opens the Door
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, toPlayer);

        if (dot > 0)
        {
            return true; //If the player stands infront of the door return true
        }
        else
        {
            return false; //if the player stand behind the door return false
        }
    }

    void DoorInteraction(bool findplayerposition) //Open / Close Door
    {
        if (!isOpen)
        {
            animator.SetBool("DotProductInput", findplayerposition);
            animator.SetBool("DoorInteraction", true);
            soundOpen.Play();
            canInteract = false;
            isOpen = true;
            interactionText_Show = interactionText_Close;
        }
        else
        {
            animator.SetBool("DoorInteraction", false);
            soundClose.Play();
            canInteract = false;
            isOpen = false;
            interactionText_Show = interactionText_Open;
        }
    }

    void SetInteractionDebounce() //Prevents spamming interaction
    {
        if (!canInteract)
        {
            canInteract = true;
        }
    }

    void UnlockDoor(bool playerHasKey) //Unlock Door Function. Make public to use through other script
    {
        switch (playerHasKey)
        {
            case true:
                playerInventoryScript.RemoveItemHolding(true);
                isLocked = false;
                return;
            case false:
                userInterfaceManager.ShowMessage("Door is Locked");
                return;
        }
    }

    void SetInteractionText() //Set interaction texts
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

