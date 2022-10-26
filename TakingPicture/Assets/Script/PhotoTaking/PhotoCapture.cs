using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PhotoCapture : MonoBehaviour
{
    [Header("PhotoTaker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;
    


    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;
    
    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Camera Audio")]
    [SerializeField] private AudioSource cameraAudio;
    private RayCasting cast;

    private WaterRising waterRise;

    private bool enterCameraScope = false;



    private Texture2D screenCapture;
    private bool viewingPhoto = false;


    private void Start()
    {
        cast = GetComponent<RayCasting>();
        waterRise = GetComponent<WaterRising>();
        Debug.Log(waterRise);
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        cameraUI.SetActive(false);
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         TakePhoto();
    //     }
    // }
    public void EnterOrExitCamera()
    {   
        if (!viewingPhoto){
            enterCameraScope = !enterCameraScope;
            if(enterCameraScope)
            {
                cameraUI.SetActive(true);
            }
            else
            {
                cameraUI.SetActive(false);
            }
        }
    }


    public void TakePhoto()
    {
        if (enterCameraScope) 
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
                if (cast.checkAlign)
                {
                    Debug.Log("object appear");
                    cast.hit.collider.gameObject.SetActive(false);
                    waterRise.PlayWaterRisingAnimmation();
                }
            }
            else
            {
                RemovePhoto();
            }
        }
    }


    IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect (0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        StartCoroutine(CameraFlashEffect());
        
        ShowPhoto();
    }

    private void ShowPhoto()
    {   
        
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        fadingAnimation.Play("PhotoFade");
    }

    IEnumerator CameraFlashEffect()
    {
        //play some audio
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
    }
    
}
