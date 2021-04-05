using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseLocationController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject generalGuy;
    private Camera mainCamera;
    private float iconIndentValue;

    private void Awake() {
        mainCamera = Camera.main;
        iconIndentValue = 1.5f;
    }

    private void LateUpdate() {
        UpdateMetersBetweenBaseAndPlayer();
        UpdateBaseIconPos();
    }

    private void UpdateMetersBetweenBaseAndPlayer() {
        int meters = (int)Vector2.Distance(generalGuy.transform.position, transform.position);
        text.text = meters.ToString() + "m";
    }

    private void UpdateBaseIconPos() {
        Vector3 targetPos = generalGuy.transform.position;
        Vector2 screenLeftDownBorderPos = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenRightUpBorderPos = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        targetPos.x = Mathf.Clamp(targetPos.x, screenLeftDownBorderPos.x + iconIndentValue, screenRightUpBorderPos.x - iconIndentValue);
        targetPos.y = Mathf.Clamp(targetPos.y, screenLeftDownBorderPos.y + iconIndentValue, screenRightUpBorderPos.y - iconIndentValue);
        if(generalGuy.transform.position.x != transform.position.x || generalGuy.transform.position.y != transform.position.y) {
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
    }
}
