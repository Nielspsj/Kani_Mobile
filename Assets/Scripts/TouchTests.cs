using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;


public class TouchTests : MonoBehaviour
{
    private Player touchInputs;
    //InputAction TouchPress2;
    //private bool pressed = false;
    [SerializeField]
    private GameObject circle;
    // Start is called before the first frame update
    void Awake()
    {
        touchInputs = new Player();
        touchInputs.Touch.Enable();
        touchInputs.Touch.TouchPosition.started += ProcessTouchPosition;
        touchInputs.Touch.Swipe.performed += ProcessSwipe;
        //touchInputs.Touch.TouchPress2;
        //TouchPress2.Enable();

    }

    private void ProcessTouchPosition(InputAction.CallbackContext context)
    {
        Vector2 v2 = context.ReadValue<Vector2>();
        Debug.Log("v2 touchpos: " + v2);
    }


    /*
    private void OnScreenPos(InputValue value)
    {
        Vector2 v2 = context.ReadValue<Vector2>();
        Debug.Log("screen pos: " + v2);
    }
    */

    /*
    private void OnTouchPress(InputValue value)
    {
        pressed = value.isPressed;
        //Debug.Log("pressed: " + pressed);

        Touch touch = Input.GetTouch(0); // Get the first touch
        //Debug.Log("TouchPos: " + touch.position);
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
        pos.z = circle.transform.position.z;
        circle.transform.position = pos;
    }
    */
    private void OnTouchPress2()
    {
        /*
        Vector2 v2 = Touchscreen.current.position.ReadValue();
        //Debug.Log("touchPos: " + v2);

        Vector3 pos = Camera.main.ScreenToWorldPoint(v2);
        pos.z = circle.transform.position.z;
        circle.transform.position = pos;
        */
    }

    private void OnSwipe()
    {

    }

    private void ProcessSwipe(InputAction.CallbackContext context)
    {
        Vector2 v2 = Touchscreen.current.position.ReadValue();
        //Debug.Log("touchPos: " + v2);

        Vector3 pos = Camera.main.ScreenToWorldPoint(v2);
        pos.z = circle.transform.position.z;
        circle.transform.position = pos;
    }

    private void OnTouchPosition(InputValue value)
    {
        Vector2 v2 = value.Get<Vector2>();
        Debug.Log("v2: " + v2);
        
    }

    private void OnTouchDrag()
    {
        Debug.Log("Touch drag");
        Touch touch = Input.GetTouch(0); // Get the first touch

        RaycastHit raycastHit;
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Hit the ui");
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out raycastHit, 2f))
            {
                if (raycastHit.transform != null && raycastHit.transform.tag == "Card")
                {
                    Debug.Log("Hit card");
                }
            }
        }        
    }
}
