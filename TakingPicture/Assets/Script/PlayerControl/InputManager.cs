using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerLook look;
    private PlayerMotor motor;

    private PhotoCapture takePic;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        takePic = GetComponent<PhotoCapture>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.OpenCamera.performed += ctx => takePic.EnterOrExitCamera();
        onFoot.TakePicture.performed += ctx => takePic.TakePhoto();
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
