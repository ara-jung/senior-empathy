using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public VoidEventChannelSO activate;
    public GameObject puzzleToActivate;


    void OnEnable()
    {
        activate.OnEventRaised += activatePuzzle;
    }

    void activatePuzzle()
    {
        puzzleToActivate.SetActive(true);
    }
}
