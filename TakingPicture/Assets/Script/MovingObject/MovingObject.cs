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
    private Dictionary<Vector3, bool> isDirectionEnabled = new Dictionary<Vector3, bool>();
    private Dictionary<Vector3, Vector3> directionMap = new Dictionary<Vector3, Vector3>();


    private bool up = true;
    private bool down = true;
    private bool right = true;
    private bool left = true;
    private Vector3 currentDirection = Vector3.forward;

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

    void Start()
    {
        isDirectionEnabled.Add(Vector3.forward, true);
        isDirectionEnabled.Add(Vector3.back, true);
        isDirectionEnabled.Add(Vector3.left, true);
        isDirectionEnabled.Add(Vector3.right, true);
        directionMap.Add(Vector3.forward, Vector3.back);
        directionMap.Add(Vector3.back, Vector3.forward);
        directionMap.Add(Vector3.left, Vector3.right);
        directionMap.Add(Vector3.right, Vector3.left);
        foreach(KeyValuePair<Vector3, bool> entry in isDirectionEnabled)
        {
            Debug.Log(entry.Key);
            Debug.Log(entry.Value);
        }

    }
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
        move.OnEventRaised -= moveDirection;
    }

    public void moveDirection(Vector3 direction) 
    {   
        if (isDirectionEnabled[direction] && !isMoving)
        {
            if (this.direction != direction)
            {
                speed = 10;
                this.direction = direction;
                isMoving = true;
                currentDirection = direction;
                isDirectionEnabled[directionMap[direction]] = true;
            }
            
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
                if (collider.transform.name == "Wall")
                {
                    isDirectionEnabled[currentDirection] = false;
                }
            }
            
        }
    }

    public void stop()
    {   
        rigidbody.velocity = new Vector3(0, 0, 0);
        speed = 0;
    }

    public void moveDown()
    {
        if (down)
        {
            moveDirection(Vector3.forward);
        }
        
    }
    public void moveUp()
    {
        if (up)
        {
            moveDirection(Vector3.back);
        }
        
    }

    public void moveRight()
    {
        if (right)
        {
            moveDirection(Vector3.right);
        }
        
    }

    public void moveLeft()
    {
        if(left)
        {
            moveDirection(Vector3.left);
        }
        
    }
 
    
}
