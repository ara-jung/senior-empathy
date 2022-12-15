using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchedForTrans : MonoBehaviour
{
    public VoidEventChannelSO touchedTrans;
    public VoidEventChannelSO portalAppear;

    void Start()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }


    void OnEnable()
    {

        portalAppear.OnEventRaised += appear;
    }


    void appear()
    {
        this.gameObject.GetComponent<Renderer>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        touchedTrans.RaiseEvent();
    }
}
