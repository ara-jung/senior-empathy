using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2To3Transition : MonoBehaviour
{
    // Start is called before the first frame update
   

    public List<VoidEventChannelSO> playAnimation;
    public VoidEventChannelSO DonePlayingAnimation;
    int play = -1;
    int playCount = 0;

    public int animationToPlay = 4;
    void Start()
    {
        StartCoroutine("waitForPlay");
        startPlaying();
    }
    void OnEnable()
    {
        DonePlayingAnimation.OnEventRaised += playNext;
    }

    IEnumerator waitForPlay()
    {
        yield return new WaitForSeconds(3.0f);
    }


    private void playNext()
    {   
        playCount += 1;
        Debug.Log(playCount);
        if (playCount % animationToPlay == 0)
        {
            Debug.Log("Started Playing: " + play);
            play += 1;

            if (play < playAnimation.Count)
            {
                playAnimation[play].RaiseEvent();
            }
        }
    }

    private void startPlaying() 
    {

        if(play == -1)
        {
            Debug.Log("Started Playing");
            playAnimation[0].RaiseEvent();
            play += 1;
        }    
    }
}
