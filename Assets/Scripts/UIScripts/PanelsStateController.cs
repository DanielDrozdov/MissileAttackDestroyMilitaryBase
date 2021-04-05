using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsStateController : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private GameObject playerCanvasPart;
    private static PanelsStateController Instance;

    private PanelsStateController() { }

    private void Awake() {
        Instance = this;
    }

    public static PanelsStateController GetInstance() {
        return Instance;
    }

    public void ActivateWinPanel() {
        playerCanvasPart.SetActive(false);
        winPanel.SetActive(true);
    }

    public void ActivateDiePanel() {
        if(!RocketLifesStateController.GetInstance().GetPlayerWinState()) {
            playerCanvasPart.SetActive(false);
            diePanel.SetActive(true);
        }
    }

    public void ActivatePlayerCanvas() {
        playerCanvasPart.SetActive(true);
    }
}
