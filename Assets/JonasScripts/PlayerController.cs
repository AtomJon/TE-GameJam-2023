using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [SerializeField, InspectorLabel("Movement")]
    float movementSpeed = 10;
    [SerializeField]
    float strafeSpeed = 5;
    [SerializeField]
    float rotationSpeed = 2;
    [SerializeField]
    float minYRotation = -45, maxYRotation = 60;

    [SerializeField, InspectorLabel("References")]
    Transform playerCameraTransform;
    [SerializeField]
    Transform followTargetTransform;

    float rotationX = 0;
    float rotationY = 0;

    Vector3 cameraForwardNoY
    {
        get
        {
            var rot = playerCameraTransform.forward;
            return new Vector3(rot.x, 0, rot.z);
        }
    }
    Vector3 cameraRightNoY
    {
        get
        {
            var rot = playerCameraTransform.right;
            return new Vector3(rot.x, 0, rot.z);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = Vector3.zero;

        var inputX = Input.GetAxis("Vertical");
        mov += cameraForwardNoY * inputX * movementSpeed;

        var inputY = Input.GetAxis("Horizontal");
        mov += cameraRightNoY * inputY * strafeSpeed;

        transform.position += mov * Time.deltaTime;

        var verticalMouseInput = Input.GetAxis("Mouse X");
        rotationX += verticalMouseInput * rotationSpeed;


        var horizontalMouseInput = -Input.GetAxis("Mouse Y");
        rotationY += horizontalMouseInput * rotationSpeed * .75f;
        rotationY = Mathf.Clamp(rotationY, minYRotation, maxYRotation);

        followTargetTransform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}