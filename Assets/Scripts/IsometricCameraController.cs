using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class IsometricCameraController : MonoBehaviour
{

    [SerializeField] private float panSpeed = 1f;
    [SerializeField] private float panSpeedJoystick = 0.05f;
    [SerializeField] private float panBorderThickness = 20f;

    private Vector3 newPosition;
    private Vector3 startingPosition;

    [SerializeField] float maxUpDistance;
    [SerializeField] float maxDownDistance;
    [SerializeField] float maxLeftDistance;
    [SerializeField] float maxRightDistance;
    [SerializeField] float maxZoom;

    void Awake()
    {
        newPosition = transform.position;
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;


        
            // Mouse hovering at the top
            if (Pointer.current.position.y.ReadValue() >= Screen.height - panBorderThickness)
            {
                if (!((newPosition + transform.up * panSpeed - startingPosition).magnitude > maxUpDistance))
                {
                    newPosition += transform.up * panSpeed;
                }
            }
            // Mouse hovering at the bottom
            else if (Pointer.current.position.y.ReadValue() <= panBorderThickness)
            {
                if (!((newPosition - transform.up * panSpeed - startingPosition).magnitude > maxDownDistance))
                {
                    newPosition -= transform.up * panSpeed;
                }
            }

            // Mouse hovering on the right
            if (Pointer.current.position.x.ReadValue() >= Screen.width - panBorderThickness)
            {
                if (!((newPosition + transform.right * panSpeed - startingPosition).magnitude > maxRightDistance))
                {
                    newPosition += transform.right * panSpeed;
                }
            }
            // Mouse hovering on the left
            else if (Pointer.current.position.x.ReadValue() <= panBorderThickness)
            {
                if (!((newPosition - transform.right * panSpeed - startingPosition).magnitude > maxLeftDistance))
                {
                    newPosition -= transform.right * panSpeed;
                }
            }

            //if (Input.mouseScrollDelta.y > 0)
            //{
            //    if (Camera.main.orthographicSize > maxZoom)
            //    {
            //        Camera.main.orthographicSize--;
            //        //maxDownDistance += 0.5f;
            //        //maxLeftDistance += 0.5f;
            //        //maxRightDistance += 0.5f;
            //        //maxUpDistance += 0.5f;
            //    }
            //}

            //if (Input.mouseScrollDelta.y < 0)
            //{
            //    if (Camera.main.orthographicSize < 10)
            //    {
            //        Camera.main.orthographicSize++;
            //        //maxDownDistance -= 0.5f;
            //        //maxLeftDistance -= 0.5f;
            //        //maxRightDistance -= 0.5f;
            //        //maxUpDistance -= 0.5f;
            //    }
            //}
        
        transform.position = newPosition;




    }
}
