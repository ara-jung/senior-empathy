using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour
{
    public GameObject water;
    public GameObject button2text;
    public GameObject player;
    public GameObject target;
    public GameObject playerCamera;
    public GameObject targetCamera;
    private bool showing = false;
    private float timer=0;
    private float flashtimer=0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)){
            if ((player.gameObject.transform.position - target.gameObject.transform.position).sqrMagnitude < 100)
            {
                playerCamera.SetActive(false);
                targetCamera.SetActive(true);
                this.gameObject.SetActive(true);
                showing = true;
            }
            

        }
        if(showing){
            timer += Time.deltaTime;
            flashtimer += Time.deltaTime;
            water.GetComponent<RectTransform>().localScale.Set(1, timer / 10, 1);
            if (flashtimer > .3f && timer > 1 && timer < 3){
                if(button2text.GetComponent<TextMesh>().text == "Quit" ){
                    button2text.GetComponent<TextMesh>().text = "Me";
                } else{
                    button2text.GetComponent<TextMesh>().text = "Quit";
                }
            }else if(timer > 3){
                button2text.GetComponent<TextMesh>().text = "Me";

            }


        }
    }
}
