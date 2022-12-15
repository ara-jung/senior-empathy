using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photoreveal : MonoBehaviour
{
    public List<WaterDropper> sources = new List<WaterDropper>();
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sources.Count == 3){
            if( (GetComponentInChildren<Light>().gameObject.transform.position - player.transform.position).sqrMagnitude < 120 ){
                Debug.Log("loading level2");
                StartCoroutine(LoadYourAsyncScene());
            }
        }
    }


    public void Splash(WaterDropper source){
        if(! sources.Contains(source)){
            Debug.Log("new splash");
            sources.Add(source);
            source.delay = 1000000000f;
            GetComponentInChildren<ContainerFill>().maxheight += 1f;
            GetComponentInChildren<Photoreveal2>().gameObject.transform.position += new Vector3(0, .2f, 0);
            
        }

    }


            
            



    IEnumerator LoadYourAsyncScene()
{
    // The Application loads the Scene in the background as the current Scene runs.
    // This is particularly good for creating loading screens.
    // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
    // a sceneBuildIndex of 1 as shown in Build Settings.
        
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/Level2/Level2New");

    // Wait until the asynchronous scene fully loads
    while (!asyncLoad.isDone)
    {
        yield return null;
    }
}
}
