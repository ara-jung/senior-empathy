using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchedForTrans : MonoBehaviour
{
    public VoidEventChannelSO touchedTrans;
    void OnTriggerEnter(Collider other)
    {
        touchedTrans.RaiseEvent();
    }
}
