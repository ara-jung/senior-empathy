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
        Debug.Log("This is playcount");
        Debug.Log(playCount);
        if (playCount % animationToPlay == 0)
        {
            play += 1;

            if (play < playAnimation.Count)
            {
                playAnimation[play].RaiseEvent();
            }
            else 
            {
                Debug.Log("raised event for animation finished");
                animationFinished.RaiseEvent();
            }
        }
    }

    private void startPlaying() 
    {

        if(play == -1)
        {
            playAnimation[0].RaiseEvent();
            play += 1;
        }    
    }


}
