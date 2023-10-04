using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void KeyAction();
    public static event KeyAction OnWPressed;
    public static event KeyAction OnAPressed;
    public static event KeyAction OnSPressed;
    public static event KeyAction OnDPressed;
    public static event KeyAction OnMouseLeftPressed;
    public static event KeyAction OnMouseRightPressed;
    public static event KeyAction OnEPressed;
    public static event KeyAction OnSpacePressed;

    public static event KeyAction OnWReleased;
    public static event KeyAction OnAReleased;
    public static event KeyAction OnSReleased;
    public static event KeyAction OnDReleased;
    public static event KeyAction OnMouseLeftReleased;
    public static event KeyAction OnMouseRightReleased;
    public static event KeyAction OnEReleased;
    public static event KeyAction OnSpaceReleased;
    
    public delegate void MousePositionAction(Vector2 position);
    public static event MousePositionAction OnMousePositionChanged;
    

    void Update()
    {
        CheckKeyboardInputs();

    }

    private void CheckKeyboardInputs()
    {
        if (Input.GetKey(KeyCode.W) && OnWPressed != null) OnWPressed();
        if (Input.GetKey(KeyCode.A) && OnAPressed != null) OnAPressed();
        if (Input.GetKey(KeyCode.S) && OnSPressed != null) OnSPressed();
        if (Input.GetKey(KeyCode.D) && OnDPressed != null) OnDPressed();

        if (Input.GetMouseButton(0) && OnMouseLeftPressed != null) OnMouseLeftPressed(); 
        if (Input.GetMouseButton(1) && OnMouseRightPressed != null) OnMouseRightPressed(); 
        if (Input.GetKey(KeyCode.E) && OnEPressed != null) OnEPressed();
        if (Input.GetKey(KeyCode.Space) && OnSpacePressed != null) OnSpacePressed(); 

        if (Input.GetKeyUp(KeyCode.W) && OnWReleased != null) OnWReleased();
        if (Input.GetKeyUp(KeyCode.A) && OnAReleased != null) OnAReleased();
        if (Input.GetKeyUp(KeyCode.S) && OnSReleased != null) OnSReleased();
        if (Input.GetKeyUp(KeyCode.D) && OnDReleased != null) OnDReleased();

        if (Input.GetMouseButtonUp(0) && OnMouseLeftReleased!= null) OnMouseLeftReleased(); 
        if (Input.GetMouseButtonUp(1) && OnMouseRightReleased != null) OnMouseRightReleased(); 
        if (Input.GetKeyUp(KeyCode.E) && OnEReleased != null) OnEReleased();
        if (Input.GetKeyUp(KeyCode.Space) && OnSpaceReleased != null) OnSpaceReleased(); 

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OnMousePositionChanged?.Invoke(mousePosition);
    }
}

