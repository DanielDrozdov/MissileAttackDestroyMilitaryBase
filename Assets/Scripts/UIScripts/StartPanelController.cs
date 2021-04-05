using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelController : MonoBehaviour
{
    public delegate void StartPanelActions();
    public static event StartPanelActions OnGameStarted;

    public void OnClick_StartGame() {
        PanelsStateController.GetInstance().ActivatePlayerCanvas();
        OnGameStarted();
        gameObject.SetActive(false);
    }
}
