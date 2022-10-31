using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public Camera camera;
    public bool checkAlign = false;
    public bool isCorrectPosition = false;
    // Update is called once per frame
    void Update()
    {
       int layerMask = 1 << 3; 
        RaycastHit[] hits;
        hits = Physics.RaycastAll(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), 20f, layerMask, QueryTriggerInteraction.Ignore);
        if (hits.Length == 2) 
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            checkAlign = true;
        }
        else 
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            checkAlign = false;
        }
    }
}
