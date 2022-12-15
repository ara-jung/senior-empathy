using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    bool showing = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
    {
            if(showing){
                gameObject.GetComponent<Canvas>().enabled = false;
            }else{
                gameObject.GetComponent<Canvas>().enabled = true;
            }
            showing = !showing;

    }
}
}

