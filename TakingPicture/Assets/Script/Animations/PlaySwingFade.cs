using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySwingFade : MonoBehaviour
{
    private Animator fadingAnimation;

    public VoidEventChannelSO playAnimation;

    public VoidEventChannelSO donePlaying;

    void Start()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        fadingAnimation = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        playAnimation.OnEventRaised += play;
    }


    private void play(){
        Debug.Log("Started playing animation");
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.fadingAnimation.Play("FadeIn");
        StartCoroutine("waitForPlay");
         
      
    }

    IEnumerator waitForPlay()
    {
        yield return new WaitForSeconds(5.0f);
        this.fadingAnimation.Play("FadeOut");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.GetComponent<Renderer>().enabled = false;
        donePlaying.RaiseEvent();
    }
}
