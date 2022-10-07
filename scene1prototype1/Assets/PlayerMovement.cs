using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 2;
    private bool belowTop, aboveBottom, goingUp, goingDown;

    public float camSens = 4f; //camera sensitivity
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //mouse look


        float lookupdown = -Input.GetAxis("Mouse Y");

        aboveBottom = gameObject.transform.rotation.eulerAngles.x < 80 || gameObject.transform.rotation.eulerAngles.x > 180;
        belowTop = gameObject.transform.rotation.eulerAngles.x > 280 || gameObject.transform.rotation.eulerAngles.x < 180;
        goingUp = lookupdown < 0;
        goingDown = lookupdown > 0;

        if ((belowTop && goingUp) || (aboveBottom && goingDown))
        {
               //rotate the camera up and down
            gameObject.transform.GetChild(0).transform.Rotate(lookupdown * camSens, 0, 0);
        }
        gameObject.transform.position += gameObject.transform.rotation * (Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ));
      // gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.rotation * new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        transform.Rotate(0, Input.GetAxis("Mouse X") * camSens, 0);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
