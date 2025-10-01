using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, Interactable
{
    private DayNightManager dayNightManager;

    [Header("UI")]
    public string interactionText_Show;

    void Start()
    {
        dayNightManager = FindObjectOfType<DayNightManager>();
    }

    public void Interact()
    {
        dayNightManager.Sleep();
    }

    public string InteractionText()
    {
        return interactionText_Show;
    }
}
