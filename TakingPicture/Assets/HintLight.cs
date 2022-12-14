using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintLight : MonoBehaviour
{
    float time = 0;
    public GameObject arrow;
    public GameObject dropper;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Light>().intensity = 0;
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 
        if(dropper.GetComponent<WaterDropper>().delay < 1000){
            if(time > 7){
                gameObject.GetComponent<Light>().intensity = .5f;
                   


            }
            if (time > 155)
            {
                gameObject.GetComponent<Light>().intensity = Mathf.Sin(time * 3);


            }
            if (time >20){
                arrow.SetActive(true);
            }

        }
    }
}
