using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public bool isActive = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && isActive)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
                player.isHiding = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
                player.isHiding = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (isActive)
                {
                    FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
                    player.isHiding = true;
                }
                else
                {
                    FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
                    player.isHiding = false;
                }
            }
            
        }
    }
}
