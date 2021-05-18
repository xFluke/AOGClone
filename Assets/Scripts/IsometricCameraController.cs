using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class IsometricCameraController : MonoBehaviour
{
    public float deltaX;
    public float deltaY;
    [SerializeField] private float panSpeed = 1f;
    [SerializeField] private float panBorderThickness = 20f;

    private Vector3 newPosition;

    [SerializeField] float maxUpDistance;
    [SerializeField] float maxDownDistance;
    [SerializeField] float maxLeftDistance;
    [SerializeField] float maxRightDistance;
    [SerializeField] float maxZoom;

    float maxRightDistancePosition;

    void Awake()
    {
        newPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        // Mouse hovering at the top
        if (Pointer.current.position.y.ReadValue() >= Screen.height - panBorderThickness) {
            if (deltaY < maxUpDistance) {
                deltaY += panSpeed;
                newPosition += transform.up * panSpeed;
            }
        }
        // Mouse hovering at the bottom
        else if (Pointer.current.position.y.ReadValue() <= panBorderThickness) {
            if (deltaY > -maxDownDistance) {
                deltaY -= panSpeed;
                newPosition -= transform.up * panSpeed;
            }
        }
        // Mouse hovering on the right
        if (Pointer.current.position.x.ReadValue() >= Screen.width - panBorderThickness) {
            if (deltaX < maxRightDistance) {
                deltaX += panSpeed;
                newPosition += transform.right * panSpeed;
            }
        }
        // Mouse hovering on the left
        else if (Pointer.current.position.x.ReadValue() <= panBorderThickness) {
            if (deltaX > -maxLeftDistance) {
                deltaX -= panSpeed;
                newPosition -= transform.right * panSpeed;
            }
        }

        transform.position = newPosition;
    }
}
