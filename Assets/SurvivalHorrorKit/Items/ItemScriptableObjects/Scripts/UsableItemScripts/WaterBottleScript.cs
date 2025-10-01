using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBottleScript : MonoBehaviour
{
    [Header("References")]
    private PlayerInventoryScript inventory;
    private FirstPersonController player;
    private Animator animator;

    [Header("Pills Settings")]
    public float thirstGain;
    public float consumptionTime;

    void Awake()
    {
        animator = GetComponent<Animator>();
        inventory = FindAnyObjectByType<PlayerInventoryScript>();
        player = FindAnyObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrinkWater();
        }
    }

    void DrinkWater()
    {
        if (inventory != null && player != null && player.currentThirst < player.maxThirst)
        {
            animator.SetTrigger("Use");
            StartCoroutine(Consume());
        }
    }

    IEnumerator Consume()
    {
        yield return new WaitForSeconds(consumptionTime);
        player.currentThirst += thirstGain;
        inventory.RemoveItemHolding(true);
    }
}
