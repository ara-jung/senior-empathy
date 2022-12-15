using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<AudioSource>().loop = false;
    }
}
