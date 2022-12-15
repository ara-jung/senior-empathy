using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingFadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator fadingAnimation;

    public VoidEventChannelSO playAnimation;

    public VoidEventChannelSO donePlaying;

    void Start()
    {
        fadingAnimation = gameObject.GetComponent<Animator>();
    }
    void OnEnable()
    {
        playAnimation.OnEventRaised += play;
        donePlaying.OnEventRaised += appear;
    }


    private void play(){
        StartCoroutine("waitForPlay");
    }

    private void appear(){
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.fadingAnimation.Play("FadeIn");
    }

    IEnumerator waitForPlay()
    {
        yield return new WaitForSeconds(5.0f);
        this.fadingAnimation.Play("FadeOut");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
}
