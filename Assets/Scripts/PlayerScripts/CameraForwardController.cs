using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForwardController : MonoBehaviour
{
    [HideInInspector] public Transform playerTransform;
    private static CameraForwardController Instance;

    private CameraForwardController() { }

    private void Awake() {
        Instance = this;
    }

    private void LateUpdate() {
        if(playerTransform == null) {
            return;
        }
        Vector2 lerpVector = Vector2.Lerp(transform.position, playerTransform.position, 0.8f);
        transform.position = new Vector3(lerpVector.x,lerpVector.y,-10f);
    }

    public static CameraForwardController GetInstance() {
        return Instance;
    }
}
