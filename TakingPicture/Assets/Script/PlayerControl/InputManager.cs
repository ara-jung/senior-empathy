using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public VoidEventChannelSO DownEvent;

    public VoidEventChannelSO UpEvent;

    public VoidEventChannelSO LeftEvent;

    public VoidEventChannelSO RightEvent;
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerLook look;
    private PlayerMotor motor;

    private PhotoCapture takePic;

    private MovingObject movingObject;

    private CheckIsPortalSelected clickPortal;

    private bool isCameraScopedChanged;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        takePic = GetComponent<PhotoCapture>();
        movingObject = GetComponent<MovingObject>();
        clickPortal = GetComponent<CheckIsPortalSelected>();
        onFoot.ArrowDown.performed += ctx => 
        {
            if (DownEvent != null) 
            {
                DownEvent.OnEventRaised();
            }
        };



        onFoot.ArrowUp.performed += ctx => 
        {
            if (UpEvent != null) 
            {
                UpEvent.OnEventRaised();
            }
        };
        onFoot.ArrowLeft.performed += ctx =>
         {
            if (LeftEvent != null) 
            {
                LeftEvent.OnEventRaised();
            }
        };
        onFoot.ArrowRight.performed += ctx => 
        {
            if (RightEvent != null) 
            {
                Debug.Log("right");
                RightEvent.OnEventRaised();
            }
        };
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.OpenCamera.performed += ctx => takePic.EnterOrExitCamera();
        onFoot.TakePicture.performed += ctx => clickPortal.Move();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        

    }
    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
