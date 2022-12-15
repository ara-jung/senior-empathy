using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    const  float fadetime = 10f;
    public GameObject water;
    public GameObject button2text;
    public Button button1;
    public Button button2;
    public GameObject player;
    public GameObject target;
    public GameObject playerCamera;
    public GameObject targetCamera;
    public List<GameObject> cracks;
    public AudioSource crackingnoise;
    public AudioSource intensemusic;
    public AudioSource bgm;
    public GameObject invisibleWall;
    public GameObject DrowningAnimation;
    private int crackind = -1;
    private bool showing = false;
    private float timer=0;
    private float flashtimer=0;
    private float fadetimer = 0;
    bool movingforward;
    private bool animationstarted = false;
    public VoidEventChannelSO startAnimation;
    public VoidEventChannelSO startTransition;

    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().enabled = false;
        button1.onClick.AddListener(()=>buttonPressed());
        button2.onClick.AddListener(()=> buttonPressed());
        player.GetComponent<PlayerMotor>().speed *= 1.5f;
        
       
    }

    void buttonPressed(){
        crackind++;
        if (crackind >= cracks.Count){
            gameObject.GetComponent<Canvas>().enabled = false;
            movingforward = true;
            player.transform.localPosition = targetCamera.transform.localPosition;
            playerCamera.SetActive(true);
            targetCamera.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerMotor>().speed *= 2;
            player.GetComponent<PlayerMotor>().gravity *= .3f;
            timer = 0;
            fadetimer = 0;
            invisibleWall.SetActive(false);




        }
        cracks[crackind].GetComponent<RawImage>().enabled = true;
        crackingnoise.Play();


    }

    // Update is called once per frame
    void Update()
    {
        if (movingforward){
            timer += Time.deltaTime;
            fadetimer += Time.deltaTime;
            intensemusic.volume = Mathf.Clamp(1-(fadetimer / 2f), 0, .6f);
            if (timer > 1f){
                timer = 0;
                //  StartCoroutine(LoadYourAsyncScene());
                startTransition.RaiseEvent();
                print("going");

            }

            player.GetComponent<PlayerMotor>().ProcessMove(new Vector2(0, 1));
            return;

        }
        if ((player.gameObject.transform.position - target.gameObject.transform.position).sqrMagnitude < 50 * 50)
        {
            if (!intensemusic.isPlaying){
                intensemusic.Play();
            }
            fadetimer += Time.deltaTime;
            
            bgm.volume = Mathf.Clamp(1-(fadetimer / fadetime), 0, .7f);
            intensemusic.volume = Mathf.Clamp(fadetimer / fadetime, 0, .6f);



        }
        if ((player.gameObject.transform.position - target.gameObject.transform.position).sqrMagnitude < 25 * 25)
        {
            if (!animationstarted)
            {
                animationstarted = true;
                startAnimation.RaiseEvent();
            }

        }

        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.E)){
          //  print("woohoo");
            if ((player.gameObject.transform.position - target.gameObject.transform.position).sqrMagnitude < 25*25)
            {

                //End Animation
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


    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/FinalCutscene/Final");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
