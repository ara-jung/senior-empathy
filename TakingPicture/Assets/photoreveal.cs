using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photoreveal : MonoBehaviour
{
    public List<WaterDropper> sources = new List<WaterDropper>();
    public GameObject player;
    public VoidEventChannelSO startTransLevelOne;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sources.Count == 3)
        {
            if ((GetComponentInChildren<Light>().gameObject.transform.position - player.transform.position).sqrMagnitude < 250)
            {
                Debug.Log("loading level2");
                startTransLevelOne.RaiseEvent();
            }
        }
    }


    public void Splash(WaterDropper source)
    {
        if (!sources.Contains(source))
        {
            Debug.Log("new splash");
            sources.Add(source);
            source.delay = 1000000000f;
            GetComponentInChildren<ContainerFill>().maxheight += 1f;
            GetComponentInChildren<Photoreveal2>().gameObject.transform.position += new Vector3(0, .2f, 0);
            GetComponentInChildren<AudioSource>().Play();

        }

    }
  
}
