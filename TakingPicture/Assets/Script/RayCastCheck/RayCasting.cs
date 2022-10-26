using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public Camera camera;

    public RaycastHit hit;
    public bool checkAlign = false;
    public bool isCorrectPosition = false;
    bool hits;
    // Update is called once per frame

    void Update()
    {
        int layerMask = 1 << 3; 
        hits = Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 40f, layerMask);
        if (isCorrectPosition && hits) 
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            checkAlign = true;
            Debug.Log("aligned");
        }
        else 
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            checkAlign = false;
            Debug.Log(hits);
            
        }
    }
}
