using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private int levelToLoad;
    private int transitionCondition = 0;

    public VoidEventChannelSO touchedTrans;
    
    public List<VoidEventChannelSO> animationsFinished;
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }

    void OnEnable()
    {
        foreach(VoidEventChannelSO playAnimation in animationsFinished)
        {
            playAnimation.OnEventRaised += incrementWin;
        }
        touchedTrans.OnEventRaised += fade;
    }

    void incrementWin()
    {
        transitionCondition += 1;
        Debug.Log("+1");
        if (transitionCondition == 3)
        {
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    
    void fade()
    {
        Debug.Log("tran");
        if (transitionCondition == 3)
        {
            FadeToLevel(1);
        }
    }
    public void FadeToLevel(int levelIndex) 
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
