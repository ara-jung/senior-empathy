using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    private RaycastHit cast;
    void OnTriggerEnter(Collider other)
    {
       
    }
    void OnTriggerExit(Collider other)
    {
     
    }

    void DisableObject()
    {
        this.enabled = false;
    }
}
