using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePositionCheck : MonoBehaviour
{
    public VoidEventChannelSO touchedEvent;
    public VoidEventChannelSO passedEvent;


    void OnTriggerEnter(Collider item) 
    {
        if (touchedEvent != null) 
        {
            touchedEvent.RaiseEvent();
        }
    }

    void OnTriggerExit(Collider item) 
    {
        if (passedEvent != null) 
        {
            passedEvent.RaiseEvent();
        }
    }

    
}
