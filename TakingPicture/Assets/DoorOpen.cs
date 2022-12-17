using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject hinge;
    public GameObject player;
    public float openAngle = 90f;
    const float speed = 1f;
    private Vector3 hingecoords;
    private float targetRot=0;
    // Start is called before the first frame update
    void Start()
    {
        hingecoords = hinge.transform.position;
       /* gameObject.transform.position += hingecoords;
        foreach (Transform childt in gameObject.GetComponentsInChildren<Transform>()){
            childt.position -= hingecoords;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Abs( ( targetRot -  gameObject.transform.localEulerAngles.z )) % 360 > 10){
            Debug.Log(gameObject.transform.localEulerAngles.z);
            float angle = (targetRot - gameObject.transform.localEulerAngles.z) * Time.deltaTime * speed;
            gameObject.transform.RotateAround(hingecoords, Vector3.up, angle );
                     
        }
        //player gets close
       // Debug.Log(player.transform.position.ToString() +" "+ hingecoords.ToString());
        if (System.Math.Abs(targetRot - openAngle) > .01f && (new Vector2(player.transform.position.x, player.transform.position.z) - new Vector2(hingecoords.x, hingecoords.z)).sqrMagnitude <64){
            Debug.Log("open");
            this.targetRot = openAngle;
        }
    }
}
