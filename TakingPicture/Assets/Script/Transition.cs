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

    public VoidEventChannelSO portalAppear;
    

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
        if (transitionCondition == 3)
        {
            portalAppear.RaiseEvent();
        }
    }

    
    void fade()
    {
        Debug.Log("tran");
        if (transitionCondition == 3)
        {
            FadeToLevel(2);
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
