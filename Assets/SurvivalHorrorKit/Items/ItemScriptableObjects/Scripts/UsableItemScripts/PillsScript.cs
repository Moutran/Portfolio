using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    [Header("References")]
    private PlayerInventoryScript inventory;
    private FirstPersonController player;
    private Animator animator;

    [Header("Pills Settings")]
    public float healthGain;
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
            UsePills();
        }
    }

    void UsePills()
    {
        if (inventory != null && player != null && player.currentHealth < player.maxHealth)
        {
            animator.SetTrigger("Use");
            StartCoroutine(Consume());
        }
    }

    IEnumerator Consume()
    {
        yield return new WaitForSeconds(consumptionTime);
        player.Heal(healthGain);
        inventory.RemoveItemHolding(true);
    }
}
