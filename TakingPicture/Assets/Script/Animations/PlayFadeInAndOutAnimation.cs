using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFadeInAndOutAnimation : MonoBehaviour
{
    [SerializeField] private Animator fadingAnimation;

    public VoidEventChannelSO playAnimation;
    public VoidEventChannelSO DonePlayingAnimation;

    void Start()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        playAnimation.OnEventRaised += play;
    }


    private void play(){
        Debug.Log("Started playing animation");
        if (gameObject == null)
        {
            Debug.Log("aaah");
            fadingAnimation = this.gameObject.GetComponent<Animator>();
        }
        this.gameObject.GetComponent<Renderer>().enabled = true;
        

        this.fadingAnimation.Play("ObjectFadeIn");
        StartCoroutine("waitForPlay");
         
      
    }

    IEnumerator waitForPlay()
    {
        yield return new WaitForSeconds(5.0f);

        this.fadingAnimation.Play("ObjectFadeOut");
        yield return new WaitForSeconds(2.0f);
        DonePlayingAnimation.RaiseEvent();
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
}
