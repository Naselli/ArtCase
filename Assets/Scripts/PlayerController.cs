using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private float      walkSpeed;
    [SerializeField] private float      mouseSensitivity;
    [SerializeField] private float      smoothness;
    [SerializeField] private Camera     playerCamera;
    [SerializeField] private Rigidbody  rB;
    [SerializeField] private GameObject playerAnchor;

    private float   hor, ver;
    private float   xRotInput, yRotInput, xRotSmooth, yRotSmooth, maxRot = 90;
    private Vector2 mouseAbs;
    private Vector2 clampInDegrees = new Vector2( 360 , 100 );
    private Vector2 targetDirection;
    private Vector3 lookDir;
    private Vector3 movement;

    void Start() {
        targetDirection = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    private void Update() {
        
        var targetOrientation = Quaternion.Euler(targetDirection);
        Debug.DrawRay(transform.position, movement * 5, Color.red);

        hor = Input.GetAxisRaw( "Horizontal" );
        ver = Input.GetAxisRaw( "Vertical" );

        lookDir = Camera.main.transform.forward;
        lookDir = ExtensionMethods.GetLookDirection( lookDir );
        Debug.DrawRay(transform.position, lookDir * 50, Color.red);
        
        xRotInput = Input.GetAxisRaw( "Mouse X" );
        yRotInput = Input.GetAxisRaw( "Mouse Y" );

        xRotSmooth = Mathf.Lerp( xRotSmooth , xRotInput , 1f / smoothness );
        yRotSmooth = Mathf.Lerp( yRotSmooth , yRotInput , 1f / smoothness );
        
        mouseAbs += new Vector2( xRotSmooth , yRotSmooth );
        
        if (clampInDegrees.x < 360)
            mouseAbs.x = Mathf.Clamp(mouseAbs.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);
        if (clampInDegrees.y < 360)
            mouseAbs.y = Mathf.Clamp(mouseAbs.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.rotation = Quaternion.AngleAxis(-mouseAbs.y, targetOrientation * Vector3.right) * targetOrientation;
        var yRotation = Quaternion.AngleAxis(mouseAbs.x, transform.InverseTransformDirection(Vector3.up)); 
        transform.localRotation *= yRotation;
    }

    private void FixedUpdate( ) {
        playerAnchor.transform.position += movement;
    }
    public float Hor => hor;
    public float Ver => ver;
}
