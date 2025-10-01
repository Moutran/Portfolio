using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eyesight : MonoBehaviour
{
    public Enemy_AI enemy;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemy.CheckLineOfSight();
        }
    }
}
