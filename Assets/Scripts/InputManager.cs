using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private TouchControl touch;
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent onStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent onEndTouch;

    private void Awake()
    {
        touch = new TouchControl();
    }
    private void OnEnable()
    {
        
        touch.Enable();
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();


        touch.Touches.PressOn.started +=  OnPressOnStarted;
        touch.Touches.PressOn.canceled +=  OnPressOnCanceled;

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;

    }

    private void OnDisable()
    {
        touch.Disable();
        TouchSimulation.Disable();

        touch.Touches.PressOn.started -= OnPressOnStarted;
        touch.Touches.PressOn.canceled -= OnPressOnCanceled;
        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
   
    }

    private void OnPressOnStarted(InputAction.CallbackContext context)
    {
        StartTouch(context);
    }

    private void OnPressOnCanceled(InputAction.CallbackContext context)
    {
        EndTouch(context);
    }
    private void Start()
    {
        
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("touched");
        if (onStartTouch != null) onStartTouch(touch.Touches.Touchpos.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        

        if (onEndTouch != null) onEndTouch(touch.Touches.Touchpos.ReadValue<Vector2>(), (float)context.time);
    }
    
    private void FingerDown(Finger finger)
    {
        if (onStartTouch != null) onStartTouch(finger.screenPosition, Time.time);
    }

    private void Update()
    {
        
    }

}
