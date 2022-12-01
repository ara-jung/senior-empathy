using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropHit : MonoBehaviour
{
    public float filllevel;
    public GameObject camera;
    private  const int FRAMES = 30;
    private bool hassplashed = false;
    private Vector3 lastvel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

        if (!hassplashed){
            //air resistance/ terminal velocity
            GetComponent<Rigidbody>().AddForce(-.65f * GetComponent<Rigidbody>().velocity);

            while (GetComponent<Rigidbody>().velocity.sqrMagnitude > 50f)
            {
                GetComponent<Rigidbody>().velocity *= .95f;

            }


            //speed stretching

                Vector3 vel = GetComponent<Rigidbody>().velocity;


            Vector3 scalefactor = fancyVectorDivide(vel, lastvel);
            scalefactor.y = Mathf.Pow(scalefactor.y, .2f);
            //Debug.Log(scalefactor);
            Vector3 s = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3( s.x, scalefactor.y * s.y,  s.z);
            lastvel = vel;
            //collision detection
            int layermask = ~192;  //Ignore layers 6 & 7
            Vector3 campos = camera.transform.position;
            Vector3 selfpos = gameObject.transform.position + (0*gameObject.transform.lossyScale.x) * Random.onUnitSphere;
            Vector3 raydir = selfpos - campos;

            RaycastHit hit;
            if (Physics.Raycast(campos, raydir, out hit, Mathf.Infinity, layermask))
            {

                hassplashed = true;
                // Debug.Log(hit.collider.gameObject.name);
                //subtract .5 frame of velocity
                gameObject.transform.position = hit.point - GetComponent<Rigidbody>().velocity * Time.deltaTime * .5f;//+new Vector3(0,0,0);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                // Debug.Log(gameObject.transform.position);
                GetComponent<Collider>().enabled = false;
                gameObject.GetComponentInChildren<Transform>().localScale = gameObject.transform.localScale;
                GetComponentInChildren<ParticleSystem>().Play();
                GetComponent<MeshRenderer>().enabled = false;
               
                //fill container
                GameObject otherthing = hit.collider.gameObject;
               //Debug.Log(otherthing.name);
                if (otherthing.GetComponentInChildren<ContainerFill>() != null){
                    otherthing.GetComponentInChildren<ContainerFill>().ChangeFillLevel(filllevel);
                }

                Destroy(gameObject, 2);

            }
            //render drop in front of stuff it's about to hit
            for (int framesahead = 0; framesahead < FRAMES; framesahead++)
            {
                Vector3 nextFramePos = gameObject.transform.position + framesahead*Time.deltaTime * GetComponent<Rigidbody>().velocity;
                raydir = nextFramePos - campos;
                if (Physics.Raycast(campos, raydir, out hit, Mathf.Infinity, layermask))
                {
                    float distToCamera = raydir.sqrMagnitude;
                    //float newDist = (hit.point - campos).sqrMagnitude;
                    // float scaleFactor = Mathf.Sqrt(newDist / distToCamera);
                    //Vector3 curPosToCamera = gameObject.transform.position - campos;
                   // float angle = Vector3.Angle(raydir, selfpos - campos);
                  //  Vector3 axis = Vector3.Cross(raydir, selfpos - campos);
                   // Vector3 newHitPoint = Vector3.RotateTowards(hit.point, selfpos - campos,2*Mathf.PI,0);


                    Vector3 nextpos = Vector3.Lerp( hit.point, gameObject.transform.position, (FRAMES-1f)/FRAMES);
                    nextpos.y = gameObject.transform.position.y;

                    //recalculate scalefactor accounting for height diff and lerp
                    float newDist = (nextpos - campos).sqrMagnitude;
                    float scaleFactor = Mathf.Sqrt(newDist / distToCamera);

                    gameObject.transform.position = nextpos;
                    gameObject.transform.localScale /=scaleFactor;

                    gameObject.GetComponentInChildren<Transform>().localScale *= scaleFactor;
                    GetComponent<Rigidbody>().velocity *= scaleFactor;
                    break;
                }
            }
        }

    }

    static Vector3 fancyVectorDivide(Vector3 v1, Vector3 v2){
        Vector3 res = new Vector3();
        if (v1.x == 0){
            res.x = 1;
        } else if (v2.x == 0){
            res.x = 1; //ten thousand is almost infinite right?
        }else{
            res.x = v1.x / v2.x;
        }

        if (v1.y == 0)
        {
            res.y = 1;
        }
        else if (v2.y == 0)
        {
            res.y = 1;
        }
        else
        {
            res.y = v1.y / v2.y;
        }

        if (v1.z == 0)
        {
            res.z = 1;
        }
        else if (v2.z == 0)
        {
            res.z = 1;
        }
        else
        {
            res.z = v1.z / v2.z;
        }

        return res;



    }


}
