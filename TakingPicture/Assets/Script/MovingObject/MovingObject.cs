using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public VoidEventChannelSO downEvent;
    public VoidEventChannelSO upEvent;
    public VoidEventChannelSO rightEvent;
    public VoidEventChannelSO leftEvent;

    public VoidEventChannelSO stopEvent;

    public Rigidbody rigidbody;


    float speed = 0;
    bool isMoving = false;
   

    int collisionTrackingNumber = 0;
    Vector3 direction = Vector3.up;
    // Update is called once per frame
    void Update()
    { 
        
        if (speed > 0)
        {
            Vector3 locVel = direction * speed;
            rigidbody.velocity = transform.TransformDirection(locVel);
            StartCoroutine(CheckMoving());
        }

    }

    void OnEnable()
    {
        upEvent.OnEventRaised += moveUp;
        downEvent.OnEventRaised += moveDown;
        leftEvent.OnEventRaised += moveRight;
        rightEvent.OnEventRaised += moveLeft;
        stopEvent.OnEventRaised += stop;
    }

    void OnDisable()
    {
        downEvent.OnEventRaised -= moveDown;
        upEvent.OnEventRaised -= moveUp;
        leftEvent.OnEventRaised -= moveRight;
        rightEvent.OnEventRaised -= moveLeft;
    }

    public void moveDirection(Vector3 direction) 
    {
        Debug.Log(this.direction);
         Debug.Log(direction);
        if (this.direction != direction && speed == 0)
        {
            speed = 10;
            this.direction = direction;
        }
        
    }

    public void stop()
    {   
        Debug.Log("stopped");
        rigidbody.velocity = new Vector3(0, 0, 0);
        speed = 0;
    }

    public void moveDown()
    {
     
        moveDirection(Vector3.forward);
    }
    public void moveUp()
    {
       
        moveDirection(Vector3.back);
    }

    public void moveRight()
    {
        moveDirection(Vector3.right);
    }

    public void moveLeft()
    {
      
        moveDirection(Vector3.left);
    }
    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.5f);

        Vector3 finalPos = transform.position;
        if(startPos.x == finalPos.x && startPos.z == finalPos.z)
        {
            speed = 0;
            yield break;
        }
           
    }
    
}
