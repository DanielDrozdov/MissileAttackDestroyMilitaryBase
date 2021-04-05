using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelController : MonoBehaviour
{
    [SerializeField] private GameObject rocketsLifesPanel;
    [SerializeField] private GameObject levelNumberPanel;

    private void Awake() {
        rocketsLifesPanel.SetActive(false);
        levelNumberPanel.SetActive(false);
    }

    public void OnClickButton_LoadNextLevel() {
        LoadSceneManager.GetInstance().LoadNextLevel();
    }
}
