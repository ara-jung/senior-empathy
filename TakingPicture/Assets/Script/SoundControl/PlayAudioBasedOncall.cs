using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioBasedOncall : MonoBehaviour
{
    public VoidEventChannelSO playAudiCall;
    public AudioSource sound;

    private void OnEnable()
    {
        playAudiCall.OnEventRaised += PlayAduio;
    }


    private void PlayAduio()
    {
        sound.Play();
    }

}
