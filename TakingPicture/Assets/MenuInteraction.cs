using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuInteraction : MonoBehaviour
{
    public GameObject water;
    public GameObject button2text;
    public Button button1;
    public Button button2;
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
        // this.gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().enabled = false;
        button1.onClick.AddListener(()=>buttonPressed());
        button2.onClick.AddListener(()=> buttonPressed());
    }

    void buttonPressed(){
        print("Add transition to final scene here");
        //Add transition to final scene here

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.E)){
          //  print("woohoo");
            if ((player.gameObject.transform.position - target.gameObject.transform.position).sqrMagnitude < 100000*50*50)
            {
                playerCamera.SetActive(false);
                targetCamera.SetActive(true);
                gameObject.GetComponent<Canvas>().enabled = true;
              // gameObject.GetComponent<RectTransform>().localScale.Set(1, 1, 1);
                showing = true;
                Cursor.lockState = CursorLockMode.None;
            }


        }
        if(showing){
            if(Input.GetMouseButtonDown(0)){
                buttonPressed();
            }
            timer += Time.deltaTime;
            flashtimer += Time.deltaTime;
           // print(timer / 5f);
            water.transform.localScale = new Vector3(1f, timer / 12f, 1f);
            if (flashtimer > .3f && timer > 1 && timer < 3){
                if(button2text.GetComponent<TMPro.TextMeshProUGUI>().text == "Quit" ){
                    button2text.GetComponent<TMPro.TextMeshProUGUI>().text = "Me";
                    flashtimer = 0;
                } else{
                    button2text.GetComponent<TMPro.TextMeshProUGUI>().text = "Quit";
                    flashtimer = 0;
                }
            }else if(timer > 3){
                button2text.GetComponent<TMPro.TextMeshProUGUI>().text = "Me";

            }


        }
    }
}
