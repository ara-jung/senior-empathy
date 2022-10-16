using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       int layerMask = 1 << 3; 
        Debug.Log(camera.transform.position);
       RaycastHit hit;
       if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 3f, layerMask))
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log(hit.transform.name);
        }
        else
        {
            Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
