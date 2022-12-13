using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropper : MonoBehaviour
{
    public GameObject droplet;
    public GameObject camera;
    public float delay = 10f;
    private float timesincedrop = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timesincedrop += Time.deltaTime;
        if (timesincedrop > delay){
            timesincedrop = 0;
            GameObject dropletInstance = Instantiate(droplet, transform.position, transform.rotation);
            dropletInstance.GetComponent<WaterDropHit>().camera = camera;
            dropletInstance.GetComponent<WaterDropHit>().dropper = gameObject;
            // ParticleSystem.EmissionModule e = dropletInstance.GetComponentInChildren<ParticleSystem>().emission;
            //  e.rateOverTimeMultiplier=1;
            //dropletInstance.GetComponentInChildren<ParticleSystem>().emission = e;
            dropletInstance.GetComponent<WaterDropHit>().filllevel = .1f * delay;
            dropletInstance.transform.localScale = gameObject.transform.localScale;
        }
    }
}
