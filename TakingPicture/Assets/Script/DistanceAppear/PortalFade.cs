using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFade : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator fadingAnimation;

    public VoidEventChannelSO startFade;


    void OnEnable()
    {
        startFade.OnEventRaised += play;
    }


    private void play(){
        Debug.Log("Started playing animation");
        this.fadingAnimation.Play("FadeOut");
        StartCoroutine("waitForPlay");
    }

    IEnumerator waitForPlay()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }
    

}
