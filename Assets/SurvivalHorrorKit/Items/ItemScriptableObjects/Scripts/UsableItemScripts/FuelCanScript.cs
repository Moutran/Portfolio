using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FuelCanScript : MonoBehaviour
{
    public float fuel = 0;
    private float maxFuel = 5f;
    private UserInterfaceManager userInterfaceManager;
    public TMP_Text fuelIndicator;

    void Start()
    {
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();
    }

    private void Update()
    {
        HandleUI();
    }

    private void OnEnable()
    {
        fuelIndicator.enabled = true;
    }

    private void OnDisable()
    {
        fuelIndicator.enabled = false;
    }

    public void AddFuel(float add)
    {
        if (userInterfaceManager != null)
        {
            if (fuel + add > maxFuel)
            {
                fuel = maxFuel;
            }
            else
            {
                fuel += add;
            }
        }
    }

    public bool CanTakeFuel()
    {
        if (fuel >= maxFuel)
        {
            userInterfaceManager.ShowMessage("Fuel Can is Full");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void HandleUI()
    {
        fuelIndicator.text = "Fuel: " + fuel + "lt";
    }
}
