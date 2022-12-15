using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Level3ToFinal : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private int levelToLoad;

    public VoidEventChannelSO trans;





    void OnEnable()
    {
        trans.OnEventRaised += fade;
    }



    void fade()
    {
            FadeToLevel(3);
      
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
