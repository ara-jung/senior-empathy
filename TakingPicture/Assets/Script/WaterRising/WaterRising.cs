using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    [SerializeField] private Animator risingAnimation;

    [SerializeField] private AudioSource risingSound;

    public List<VoidEventChannelSO> puzzleSolved;

    private string[] anaimations = new string[3]{"WaterRising", "WaterRising2", "WaterRising3"};
    private int waterAnimationToPlay = -1;

    void OnEnable()
    {
        foreach(VoidEventChannelSO playAnimation in puzzleSolved)
        {
            playAnimation.OnEventRaised += PlayWaterRisingAnimmation;
        }
        
    }
    public void PlayWaterRisingAnimmation()
    {
        
       waterAnimationToPlay += 1;
       Debug.Log(waterAnimationToPlay);
       if(waterAnimationToPlay < anaimations.Length)
       {
            Debug.Log(anaimations[waterAnimationToPlay]);
            this.risingSound.Play();
            this.risingAnimation.Play(anaimations[waterAnimationToPlay]);
       }
    }
}
