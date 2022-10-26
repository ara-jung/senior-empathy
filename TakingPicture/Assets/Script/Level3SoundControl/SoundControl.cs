using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
       this.gameObject.SetActive(false);
    }
}
