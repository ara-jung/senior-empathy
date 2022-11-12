using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsPortalSelected : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;

    [HideInInspector]
    public RaycastHit hit;

    [HideInInspector]
    public bool checkAlign = false;

    [HideInInspector]
    public bool isCorrectPosition = false;
    bool hits;


    [HideInInspector]
    public Transform currentPortal = null;
    void Update()
    {
        int layerMask = 1 << 6; 
        hits = Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 40f, layerMask);
        if(hits) 
        {
            currentPortal = hit.transform;
        }
        else
        {
            currentPortal = null;
        }
        
    }

    public void Move() 
    {
        if(currentPortal)
        {
            currentPortal.GetComponent<PortalTrigger>().MoveDirection();
        }
    }
}
