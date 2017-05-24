using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    float cameraSensitivityX = 5, cameraSensitivityY = 3, movementSpeed = 5, jumpForce = 300;
    
    Rigidbody myRigidbody;

    const float minimumLookAngle = -45, maximumLookAngle = 45;
    float rotationX, rotationY;
    bool jumping;

    void Start()
    {
        // if anybody's lookin' for the player, that's us
        tag = "Player";
        
        myRigidbody = GetComponent<Rigidbody>();

        // freeze the player's x and z rotation so we don't topple over
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // this script is a simple, general purpose convenience script, and its most frustrating bug is in how its rigidbody behaves when it brushes up against something (it spins a little). to alleviate this quirk, the angular drag is set to 0
        myRigidbody.angularDrag = 0;

        // the cursor is annoying. to get it back in play mode, press esc (it'll still be invisible until you move it off the game screen)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // looking left and right turns the whole player, not the camera
        LookHorizontal();

        // looking up and down turns the camera, not the player
        LookVertical();

        // if you press the jump button and you're not already jumping, jump
        //if (Input.GetButtonDown("Jump") && !jumping) Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter(Collision other)
    {
        // if we're touching anything, we aren't jumping (this is a lazy way to do this, but you can wall jump!)
        jumping = false;
    }

    #region custom functions

    void LookHorizontal()
    {
        // left and right look direction is input direction (default is the x axis on the mouse and right thumbstick) times horizontal camera sensitivity
        rotationX = Input.GetAxis("Mouse X") * cameraSensitivityX;

        // look left and right
        transform.Rotate(0, rotationX, 0);
    }

    void LookVertical()
    {
        // up and down look direction is inverted input direction (default is the y axis on the mouse and right thumbstick) times vertical camera sensitivity
        rotationY -= Input.GetAxis("Mouse Y") * cameraSensitivityY;

        // you can't look up or down further than your neck will allow
        rotationY = Mathf.Clamp(rotationY, minimumLookAngle, maximumLookAngle);

        // look up and down
        Camera.main.transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    void Move()
    {
        Vector3 movementDirection = new Vector3
            (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed;
        
        movementDirection = transform.TransformDirection
            (Vector3.ClampMagnitude(movementDirection, movementSpeed) * Time.deltaTime);
        
        myRigidbody.MovePosition(transform.position + movementDirection);
    }

    void Jump()
    {
        myRigidbody.AddForce(Vector3.up * jumpForce);
        jumping = true;
    }

    #endregion
}
