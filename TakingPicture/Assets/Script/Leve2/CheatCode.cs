using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    public VoidEventChannelSO cheatCode;
    public VoidEventChannelSO first;
    public VoidEventChannelSO second;
    public VoidEventChannelSO third;


    public void Start()
    {
        StartCoroutine("LateStart");
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(2f);
        cheatCode.RaiseEvent();
        first.RaiseEvent();
        second.RaiseEvent();
        third.RaiseEvent();
        //Your Function You Want to Call
    }
    
    
}
