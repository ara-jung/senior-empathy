using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelOneTrans : MonoBehaviour
{

    public Animator animator;
    private int levelToLoad;
    public VoidEventChannelSO startTransLevelOne;


    public void OnEnable()
    {
        startTransLevelOne.OnEventRaised += FadeToLevel;
    }

    public void FadeToLevel()
    {
        levelToLoad = 1;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
