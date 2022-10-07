using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropHit : MonoBehaviour
{
    public GameObject camera;
    private bool hassplashed = false;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (!hassplashed){
            int layermask = ~192;  //Ignore layers 6 & 7
            Vector3 campos = camera.transform.position;
            Vector3 selfpos = gameObject.transform.position + (0*gameObject.transform.lossyScale.x) * Random.onUnitSphere;
            Vector3 raydir = selfpos - campos;

            RaycastHit hit;
            if (Physics.Raycast(campos, raydir, out hit, Mathf.Infinity, layermask))
            {
                hassplashed = true;
               // Debug.Log(hit.collider.gameObject.name);
                gameObject.transform.position = hit.point+new Vector3(0,.3f,0);
               // Debug.Log(gameObject.transform.position);
                GetComponent<Collider>().enabled = false;
                GetComponentInChildren<ParticleSystem>().Play();
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject, 2);

            }
            //render drop in front of stuff it's about to hit
            for (int framesahead = 0; framesahead < 3; framesahead++)
            {
                Vector3 nextFramePos = gameObject.transform.position + framesahead*Time.deltaTime * GetComponent<Rigidbody>().velocity;
                raydir = nextFramePos - campos;
                if (Physics.Raycast(campos, raydir, out hit, Mathf.Infinity, layermask))
                {
                    float distToCamera = raydir.sqrMagnitude;
                    float newDist = (hit.point - campos).sqrMagnitude;
                    float scaleFactor = Mathf.Sqrt(newDist / distToCamera);
                    gameObject.transform.localScale *= scaleFactor;
                    Vector3 curPosToCamera = gameObject.transform.position - campos;
                    gameObject.transform.position = campos + curPosToCamera * scaleFactor;
                    GetComponent<Rigidbody>().velocity *= scaleFactor;
                    break;
                }
            }
        }

    }


}
