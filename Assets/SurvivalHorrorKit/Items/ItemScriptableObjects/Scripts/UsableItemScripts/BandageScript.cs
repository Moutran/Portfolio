using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageScript : MonoBehaviour
{
    [Header("References")]
    private PlayerInventoryScript inventory;
    private FirstPersonController player;

    [Header("Pills Settings")]
    public float healthGain;
    public float useTime;

    void Awake()
    {
        inventory = FindAnyObjectByType<PlayerInventoryScript>();
        player = FindAnyObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseBandage();
        }
    }

    void UseBandage()
    {
        if (inventory != null && player != null && player.currentHealth < player.maxHealth)
        {
            StartCoroutine(Consume());
        }
    }

    IEnumerator Consume()
    {
        yield return new WaitForSeconds(useTime);
        player.Heal(healthGain);
        player.StopBleeding();
        inventory.RemoveItemHolding(true);
    }
}
