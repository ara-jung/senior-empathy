using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    [SerializeField] private Animator risingAnimation;

    [SerializeField] private AudioSource risingSound;

    private string[] anaimations = new string[3]{"WaterRising", "WaterRising2", "WaterRising3"};
    public int waterAnimationToPlay = -1;
    public void PlayWaterRisingAnimmation()
    {
       waterAnimationToPlay += 1;
       if(waterAnimationToPlay < anaimations.Length)
       {
            Debug.Log(anaimations[waterAnimationToPlay]);
            this.risingSound.Play();
            this.risingAnimation.Play(anaimations[waterAnimationToPlay]);
       }
    }
}
