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

    public DirectionEventChannelSO move;

    public Rigidbody rigidbody;


    float speed = 0;
    bool isMoving = false;
    bool isChecking = false;
    private Vector3 lastUpdatePos = Vector3.zero;
    private Vector3 dist;
    int collisionTrackingNumber = 0;
    Vector3 direction = Vector3.up;
    HashSet<Collider> touched = new HashSet<Collider>();
    // Update is called once per frame
    void Update()
    { 
          

        if (isMoving)
        {   
            Vector3 locVel = direction * speed;
            rigidbody.velocity = transform.TransformDirection(locVel);
        }
   
    }

    void OnEnable()
    {
        upEvent.OnEventRaised += moveUp;
        downEvent.OnEventRaised += moveDown;
        leftEvent.OnEventRaised += moveRight;
        rightEvent.OnEventRaised += moveLeft;
        stopEvent.OnEventRaised += stop;
        move.OnEventRaised += moveDirection;
    }

    void OnDisable()
    {
        downEvent.OnEventRaised -= moveDown;
        upEvent.OnEventRaised -= moveUp;
        leftEvent.OnEventRaised -= moveRight;
        rightEvent.OnEventRaised -= moveLeft;
        stopEvent.OnEventRaised -= stop;
    }

    public void moveDirection(Vector3 direction) 
    {
        if (this.direction != direction && !isMoving)
        {
            speed = 10;
            this.direction = direction;
            isMoving = true;
        }
        
    }
    public void OnTriggerEnter(Collider collider) 
    {
        int prevSize = touched.Count;
        if (collider.transform.name != "Floor") {
            touched.Add(collider);

            if (touched.Count != prevSize) 
            {
                // Debug.Log("added" + collider.transform.name);
                touched.Clear();
                touched.Add(collider);
                isMoving = false;
                // Debug.Log("stopped");
            }
            // Debug.Log(touched);
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
    // }
    // private IEnumerator CheckMoving()
    // {   isChecking = true;
    //     Vector3 startPos = transform.position;
    //     yield return new WaitForSeconds(0.5f);

    //     Vector3 finalPos = transform.position;
    //     if(startPos.x == finalPos.x && startPos.z == finalPos.z)
    //     {
    //         speed = 0;
    //         yield break;
    //     }
    //     isChecking = false;
           
    // }
    
}
