using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class FlashlightScript : MonoBehaviour
{
    public float battery = 100f; //Current battery level
    public float batteryReduction = 0.3f; //Battery lost per second
    public TMP_Text batteryIndicator; //Battery UI indication

    private Light lightSource; //Flashlight's light
    private bool isOpen = false; //Boolean used to check if flashlight is on or off

    private void Start()
    {
        lightSource = GetComponentInChildren<Light>();
    }
    void Update()
    {
        CheckInput();
        UpdateBatteryUi();
    }

    void FlashlightButton()
    {
        if ((battery > 0) && (isOpen == false))
        {
            lightSource.enabled = true;
            isOpen = true;
        }
        else
        {
            lightSource.enabled = false;
            isOpen = false;
        }
    }

    void RemoveBatteryWhenLit()
    {
        if (isOpen)
        {
            battery -= batteryReduction * Time.deltaTime;
        }
    }

    void UpdateBatteryUi()
    {
        if (isOpen)
        {
            batteryIndicator.enabled = true;
            batteryIndicator.text = (Mathf.Round(battery).ToString() + "%");
        }
        else
        {
            batteryIndicator.enabled = false;
        }
    }

    void CheckInput()
    {
        battery = Mathf.Clamp(battery, 0, 100);
        if (battery > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                FlashlightButton();

            }
            RemoveBatteryWhenLit();
        }
        else
        {
            FlashlightButton();
        }
    }

    void OnDisable()
    {
        isOpen = false;
        lightSource.enabled = false;
        UpdateBatteryUi();
    }
}