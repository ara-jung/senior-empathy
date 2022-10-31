using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
    private RayCasting cast;
    void OnTriggerEnter(Collider other)
    {
        cast = other.GetComponent<RayCasting>();
        cast.isCorrectPosition = true;
    }
    void OnTriggerExit(Collider other)
    {
        cast = other.GetComponent<RayCasting>();
        cast.isCorrectPosition = false;
    }

    void DisableObject()
    {
        this.enabled = false;
    }
}
