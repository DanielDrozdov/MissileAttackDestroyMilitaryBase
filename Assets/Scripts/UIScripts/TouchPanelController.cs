using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanelController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [HideInInspector] public Vector2 moveDirection;
    private static TouchPanelController Instance;
    private bool IsPressed;
    private int _touchId;
    private Vector2 oldTouchPos;
    private Vector2 zeroVector = Vector2.zero;

    private void Awake() {
        Instance = this;
        RocketStateController.OnPlayerDie += ResetMoveDirection;
        moveDirection = new Vector2(45, 45);
    }

    private void Update() {
        CalculateMoveDirection();
    }

    public static TouchPanelController GetInstance() {
        return Instance;
    }

    public void OnPointerDown(PointerEventData eventData) {
        IsPressed = true;
        _touchId = eventData.pointerId;
        oldTouchPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData) {
        IsPressed = false;
    }

    private void CalculateMoveDirection() {
        if(IsPressed) {
            if(_touchId >= 0 && _touchId < Input.touches.Length) {
                Vector2 currentTouchPos = Input.touches[_touchId].position;
                moveDirection = currentTouchPos - oldTouchPos;
            } else {
                Vector2 currentTouchPos = Input.mousePosition;
                moveDirection = currentTouchPos - oldTouchPos;
            }
        }
    }

    private void ResetMoveDirection() {
        moveDirection = zeroVector;
    }
}
