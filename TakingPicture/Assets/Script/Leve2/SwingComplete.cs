using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingComplete : MonoBehaviour
{
    public VoidEventChannelSO touchedEvent;
    public VoidEventChannelSO passedEvent;

    public VoidEventChannelSO secondTouchedEvent;
    public VoidEventChannelSO secondPassedEvent;

    public VoidEventChannelSO stopEvent;

    public VoidEventChannelSO objectToPlayAnimation;

    private int winCount = 0;
    private bool firstTocuch = false;
    private bool secondTouch = false;

    void OnEnable()
    {
        Debug.Log(winCount);
        touchedEvent.OnEventRaised += firstTouched;
        passedEvent.OnEventRaised += firstPassed;
        secondTouchedEvent.OnEventRaised += secondTouched;
        secondPassedEvent.OnEventRaised += secondPassed;
    }
    
    // void Start()
    // {
    //     Debug.Log("start");
    //     objectToPlayAnimation.RaiseEvent();
    // }

    void firstTouched()
    {
        isSolved();
        firstTocuch = true;
    }
    void firstPassed()
    {
        firstTocuch = false;
    }

    void secondTouched()
    {
        secondTouch = true;
        isSolved();
    }

    void secondPassed()
    {
        secondTouch = false;
    }


    void isSolved()
    {
        if (firstTocuch && secondTouch){
            if (stopEvent != null) 
            {
                Debug.Log("started");
                stopEvent.RaiseEvent();
                objectToPlayAnimation.RaiseEvent();
            }
        }
    }

}
