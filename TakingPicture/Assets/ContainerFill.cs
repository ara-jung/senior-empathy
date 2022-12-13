using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ContainerFill : MonoBehaviour
{
    public Vector2 center = new Vector2(0, 0);
    public float minheight = 0;
    public float maxheight = 1;
    public float height;
    public int radialres = 2;//number of rings
    public int angularres = 6; //vertices per ring
    private Vector3[] verts;
    private int[] tris;
    private Mesh mesh;
    public GameObject testcube;
    // Start is called before the first frame update
    void Start()
    {
        /*
        height -= 100;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        verts = new Vector3[radialres * angularres + 1];
        tris = new int[(radialres * angularres * 2 - angularres) * 3];//inner ring has 1 tri per angle, other rings all have 2
        verts[0] = new Vector3(center.x, minheight, center.y);
        int triindex = 0;
        for (int r = 0; r < radialres; r++)
        {
            for (int a = 0; a < angularres; a++)
            {
                float angle = a * 2 * Mathf.PI / angularres;
                verts[(r * angularres) + a + 1] = new Vector3(Mathf.Cos(angle) / (radialres - r), minheight, Mathf.Sin(angle) / (radialres - r));
                int innercur, innernext, outercur, outernext;
                if (r == 0)
                {
                    tris[triindex++] = 0; //center
                    tris[triindex++] = r * angularres + ((a + 1) % angularres) + 1; //next point around in the circle
                    tris[triindex++] = r * angularres + a + 1;//point we just added
                }
                else
                {
                    /*
                     *        outercur -------------outernext
                     *         |                _,-'   /
                     *         |           _,-'       /
                     *         |      _,-'           /
                     *         innercur -------  innernext
                     * 
                     * 
                     *//*
                    innercur = (r - 1) * angularres + a + 1;
                    innernext = (r - 1) * angularres + ((a + 1) % angularres) + 1;
                    outercur = r * angularres + a + 1;
                    outernext = r * angularres + ((a + 1) % angularres)+1;
                    tris[triindex++] = innercur;
                    tris[triindex++] = outernext;
                    tris[triindex++] = outercur;

                    tris[triindex++] = innercur;
                    tris[triindex++] = innernext;
                    tris[triindex++] = outernext;

                }


            }
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        ChangeFillLevel(100);*/
    }

    // Update is called once per frame
    void Update()
    {
       //ChangeFillLevel(.1f * Time.deltaTime);
    }

    public void ChangeFillLevel(float amount)
    {


        //Debug.Log("filling " + amount);
        float newheight = Mathf.Clamp(height + amount, minheight, maxheight);
        if (Mathf.Abs(newheight - height) < .000001f)
        {
            return;
            //if it didnt change, don't recalculate the surface
        }
        gameObject.transform.position += new Vector3(0, .1f*amount*(maxheight - minheight),0);

        height = newheight;
        return;/*
        if (testcube != null)
        {
            testcube.transform.position = gameObject.transform.position + mesh.vertices[0];
        }


       
        verts[0] = new Vector3(center.x, height, center.y);
        float[] extents = new float[angularres];
        for (int a = 0; a < angularres; a++)
        {
            float angle = a * 2 * Mathf.PI / angularres;
            Vector3 raydir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
           // Debug.DrawRay(gameObject.transform.position + new Vector3(center.x, height, center.y), raydir, new Color(255, 0, 0), 100);
            int layermask = ~192;  //Ignore layers 6 & 7
            if (Physics.Raycast(gameObject.transform.position+ new Vector3(center.x, height, center.y), raydir, out RaycastHit hit, Mathf.Infinity, layermask))
            {
                extents[a] = hit.distance*1.01f;
            }
            else
            {
                extents[a] = 0f; //Idk what to do if it doesnt hit the container
               
            }

        }
        for (int r = 0; r < radialres; r++)
        {
            for (int a = 0; a < angularres; a++)
            {
                float angle = a * 2*Mathf.PI / angularres;
                verts[(r * angularres) + a + 1] = new Vector3(extents[a]* Mathf.Cos(angle) / (radialres - r) +center.x, height+Random.Range(-.01f,.01f)/*this part should eventually be done in the vertex shader*//*, extents[a] * Mathf.Sin(angle) / (radialres - r) + center.y);
                
            }
        }
      


        mesh.vertices = verts;
        mesh.RecalculateNormals();*/



    }


}

