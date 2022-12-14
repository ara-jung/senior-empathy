using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManage : MonoBehaviour
{
    public List<VoidEventChannelSO> playAnimation;
    public VoidEventChannelSO DonePlayingAnimation;

    public VoidEventChannelSO solvedPuzzle;

    public VoidEventChannelSO animationFinished;
    int play = -1;
    int playCount = 0;

    public int animationToPlay = 4;

    void OnEnable()
    {
        DonePlayingAnimation.OnEventRaised += playNext;
        solvedPuzzle.OnEventRaised += startPlaying;
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
            else 
            {
                animationFinished.RaiseEvent();
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
