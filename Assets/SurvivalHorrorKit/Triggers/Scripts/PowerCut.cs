    using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PowerCut : MonoBehaviour
{
    public List<GameObject> FuseBoxes = new List<GameObject>();
    public int fusesToRemove = 2;
    public bool oneTimeUse = true;
    public bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            for (int i = 0; i < FuseBoxes.Count; i++)
            {
                InteractableFuseBox fusebox = FuseBoxes[i].GetComponentInChildren<InteractableFuseBox>();
                if (fusebox != null)
                {
                    fusebox.CutPowerOff(fusesToRemove);
                }
            }
            CheckUses();
        }
    }

    void CheckUses()
    {
        if (oneTimeUse)
        {
            isActive = false;
        }
    }
}