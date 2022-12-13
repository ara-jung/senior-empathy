using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDisable : MonoBehaviour
{

    public VoidEventChannelSO startFade;

    void OnEnable()
    {
        startFade.OnEventRaised += stop;
    }


    private void stop(){
        this.gameObject.SetActive(false);
    }
}
