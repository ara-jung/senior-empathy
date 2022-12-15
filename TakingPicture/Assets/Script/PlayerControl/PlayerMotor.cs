using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [SerializeField] private AudioSource walking;
    public float speed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    public List<VoidEventChannelSO> stopPlayer;
    public List<VoidEventChannelSO> startPlayer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        foreach (VoidEventChannelSO stop in stopPlayer)
        {
            stop.OnEventRaised += SetZero;
        }

        foreach (VoidEventChannelSO start in startPlayer)
        {
            start.OnEventRaised += ChangeSpeed;
        }
    }



    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    void ChangeSpeed()
    {
        speed = 10f;
    }

    void SetZero()
    {
        speed = 0f;
    }

    //gets the inputs from our InputManager.cs and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if((input.y != 0 || input.x != 0) && isGrounded) 
        {
            walking.enabled = true;
        }
        else 
        {
            walking.enabled = false;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
